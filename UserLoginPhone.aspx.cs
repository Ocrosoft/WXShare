using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class UserLoginPhone : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                // 手机
                var phone = Request.Form["tel"];
                // 短信验证码
                var code = Request.Form["code"];
                // 身份
                var iden = Int32.Parse(Request.Form["iden"]);

                // 格式检查
                if (!Regex.IsMatch(phone, "^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$") || // 手机号
                    !Regex.IsMatch(code, "^\\d{4}$") || // 验证码4位数字
                    iden < 1 || iden > 5 // 身份在[1,5]
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

                // 普通会员-业务员-施工队-管理员
                if (iden == 1 || iden == 2 || iden == 4 || iden == 5)
                {
                    string sql = "select count(*) from users where phone=?phone and identity=?iden;";
                    MySqlParameter[] para = new MySqlParameter[2];
                    para[0] = new MySqlParameter("?phone", phone);
                    para[1] = new MySqlParameter("?iden", iden);
                    try
                    {
                        Object ret = MySQLHelper.ExecuteScalar(sql, para);
                        if (Int32.Parse(ret.ToString()) == 1)
                        {
                            Session["phone"] = phone;
                            Session["iden"] = iden;
                            Response.Redirect("/UserIndex.aspx");
                        }
                    }
                    catch (MySqlException ex)
                    {
                        //
                    }
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