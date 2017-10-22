using Ocrosoft;
using System;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace WXShare
{
    public partial class UserRegister : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                // 姓名
                var name = Request.Form["name"];
                // 手机
                var phone = Request.Form["tel"];
                // 短信验证码
                var code = Request.Form["code"];
                // 身份
                var iden = Request.Form["iden"];

                // 格式检查
                if (name == "" || // 姓名不空
                    !OSecurity.ValidPhone(phone) || // 手机号
                    !Regex.IsMatch(code, "^\\d{4}$") || // 验证码4位数字
                    Int32.Parse(iden) < 1 || Int32.Parse(iden) > 5 // 身份在[1,5]
                    )
                {
                    return;
                }
                // 验证码检查
                if(!AuthCode.CheckAuthCode(phone,code))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "codeError", "alterError($('input[name=code]')[0]);", true);
                    return;
                }

                // 身份证（业务员-施工队-管理员）
                String IDCardYWY = null;
                // 身份证（经销商）
                String IDCardJXS;
                // 区县（经销商）
                String location;
                // 详细地址
                String detailLocation;

                if(iden == "2" || iden == "4" || iden == "5")
                {
                    IDCardYWY = Request.Form["idcard_ywy"];
                    // 身份证检查
                    if (!OSecurity.ValidIDCard(IDCardYWY))
                    {
                        return;
                    }
                }

                if ((iden == "1" || iden == "2" || iden == "4" || iden == "5") && 
                    DataBase.User.Add(new Objects.User() {
                    phone = phone,
                    name = name,
                    identity = iden,
                    IDCard = IDCardYWY
                    }))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "success", "success(" + iden + ", '注册成功', true);", true);
                }
                // 经销商
                else if (iden == "3")
                {
                    IDCardJXS = Request.Form["idcard_jxs"];
                    location = Request.Form["location"];
                    detailLocation = Request.Form["detailLocation"];

                    // 不实现
                }
                else return;
            }
        }

        protected void vcodeBtn_Click(object sender, EventArgs e)
        {
            if (OSecurity.ValidPhone(tel.Value))
            {
                // 发送间隔校验
                if(Session["vcodeSend"] != null)
                {
                    if(OSecurity.DateTimeToTimeStamp(DateTime.Now) -  Int64.Parse(Session["vcodeSend"].ToString()) < 60)
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