using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;

namespace WXShare
{
    public partial class ActivitySign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                string phone = Request.Form["tel"]; // 手机
                string name = Request.Form["name"]; // 姓名
                string code = Request.Form["code"]; // 验证码
                string location = Request.Form["location"]; // 地址
                string locationDetail = Request.Form["detailLocation"]; // 详细地址

                // 格式检查
                if (name == "" || // 姓名不空
                    !Regex.IsMatch(phone, "^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$") || // 手机号
                    !Regex.IsMatch(code, "^\\d{4}$") || // 验证码4位数字
                    location == "" // 详细地址为空
                    )
                {
                    return;
                }
                // 验证码检查
                /*if (!AuthCode.CheckAuthCode(phone, code))
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "codeError", "alterError($('input[name=code]')[0]);", true);
                    return;
                }*/

                string activityID = Request.QueryString["aid"];
                string userID = Request.QueryString["uid"]; // 即手机号
                if(activityID == "" || userID=="")
                {
                    return;
                }
                string sql = "select timeend from activity where Id = ?aid and valid = 1";
                MySqlParameter para = new MySqlParameter("?aid", activityID);
                try
                {
                    Object ret = MySQLHelper.ExecuteScalar(sql, para);
                    if(ret.ToString() == string.Empty)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "noaid", "alert('不存在此活动！');", true);
                        return;
                    }
                    DateTime endTime = DateTime.Parse(ret.ToString());
                    if(endTime <= DateTime.Now)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "ended", "alert('活动已结束！');", true);
                        return;
                    }
                }
                catch(MySqlException ex)
                {
                    //
                }
                sql = "select count(*) from users where phone = ?phone and identity = 1";
                para = new MySqlParameter("?phone", userID);
                try
                {
                    Object ret = MySQLHelper.ExecuteScalar(sql, para);
                    if(ret.ToString() == "0")
                    {
                        ScriptManager.RegisterClientScriptBlock(this, GetType(), "nouid", "alert('不存在该推荐人！');", true);
                        return;
                    }
                }
                catch(MySqlException ex)
                {
                    //
                }
                sql = "insert into activitysign(name, phone, location, locationDetail, activityID, shareSource) " +
                    "values(?name, ?phone, ?location, ?locationDetail, ?activityID, ?shareSource);";
                MySqlParameter[] paras = new MySqlParameter[6];
                paras[0] = new MySqlParameter("?name", name);
                paras[1] = new MySqlParameter("?phone", phone);
                paras[2] = new MySqlParameter("?location", location);
                paras[3] = new MySqlParameter("?locationDetail", locationDetail);
                paras[4] = new MySqlParameter("?activityID", activityID);
                paras[5] = new MySqlParameter("?shareSource", userID);
                try
                {
                    int ret = MySQLHelper.ExecuteNonQuery(sql, paras);
                    if(ret == 1)
                    {
                        Response.Redirect("/ActivityASignSuccess.aspx");
                        WXManage.SendMessage("orUOg1HDidOwnt_QS45_Ws4XHko4",
                            "有一条新报名信息！");
                        return;
                    }
                }
                catch(MySqlException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "severError", "alert('服务器错误，请稍候再试！');", true);
                    return;
                }
            }
        }

        protected void vcodeBtn_Click(object sender, EventArgs e)
        {
            if (Regex.IsMatch(tel.Value, "^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$"))
            {
                // 发送间隔校验
                if (Session["vcodeSend"] != null)
                {
                    if (WXManage.DateTimeToTimeStamp(DateTime.Now) - Int64.Parse(Session["vcodeSend"].ToString()) < 60)
                    {
                        return;
                    }
                }
                Session["vcodeSend"] = WXManage.DateTimeToTimeStamp(DateTime.Now);
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