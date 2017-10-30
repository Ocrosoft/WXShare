using Ocrosoft;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace WXShare
{
    public partial class ActivitySign : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if (Request.QueryString["aid"] == null)
            {
                Response.Redirect("/Activity.aspx");
                return;
            }

            if (IsPostBack)
            {
                string phone = Request.Form["tel"]; // 手机
                string name = Request.Form["name"]; // 姓名
                string code = Request.Form["code"]; // 验证码
                string location = Request.Form["location"]; // 地址
                string locationDetail = Request.Form["detailLocation"]; // 详细地址

                // 格式检查
                if (name == "" || // 姓名不空
                    !OSecurity.ValidPhone(phone) || // 手机号
                    !Regex.IsMatch(code, "^\\d{4}$") || // 验证码4位数字
                    location == "" // 详细地址为空
                    )
                {
                    return;
                }
                // 验证码检查
                if (!AuthCode.CheckAuthCode(phone, code))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "codeError", "alterError($('input[name=code]')[0]);", true);
                    return;
                }

                string activityID = Request.QueryString["aid"];
                string userID = Request.QueryString["uid"]; // 即手机号
                if(activityID == "" || userID=="")
                {
                    return;
                }
                var activity = DataBase.Activity.Get(new Objects.Activity() { id = activityID });
                if (activity == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "noaid", "alert('不存在此活动！');", true);
                    return;
                }
                if (activity.timeEnd <= DateTime.Now)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "ended", "alert('活动已结束！');", true);
                    return;
                }

                var user = DataBase.User.Get(new Objects.User() { phone = userID, identity = "1" });
                if (user == null)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "nouid", "alert('不存在该推荐人！');", true);
                    return;
                }

                if(DataBase.ActivitySign.Add(new Objects.ActivitySign()
                {
                    name=name,
                    phone=phone,
                    location=location,
                    locationDetail=locationDetail,
                    activityID=activityID,
                    shareSource=userID
                }))
                {
                    // 发送新报名提示
                    var admins = DataBase.User.Gets("5");
                    foreach(var admin in admins)
                    {
                        var openid = DataBase.User.GetOpenID(admin);
                        if(!string.IsNullOrEmpty(openid))
                        {
                            WXManage.SendMessage(openid, "有一条新报名信息！");
                        }
                    }
                    Response.Redirect("/ActivitySignSuccess.aspx");
                    return;
                }
            }
        }

        protected void vcodeBtn_Click(object sender, EventArgs e)
        {
            if (OSecurity.ValidPhone(tel.Value))
            {
                // 发送间隔校验
                if (Session["vcodeSend"] != null)
                {
                    if (OSecurity.DateTimeToTimeStamp(DateTime.Now) - Int64.Parse(Session["vcodeSend"].ToString()) < 60)
                    {
                        return;
                    }
                }
                Session["vcodeSend"] = OSecurity.DateTimeToTimeStamp(DateTime.Now);
                AuthCode.SendAuthCode(tel.Value);
                ScriptManager.RegisterStartupScript(this, GetType(), "success", "success(1, '验证码已发送', false);", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "successcd", "startCountDown();", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "phoneError", "alterError($('input[name=tel]')[0]);", true);
            }
        }
    }
}