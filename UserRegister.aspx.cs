using MySql.Data.MySqlClient;
using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace WXShare
{
    public partial class UserRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                /* 上传的文件
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    HttpPostedFile file = Request.Files[0];
                }
                */
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
                    !Regex.IsMatch(phone, "^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$") || // 手机号
                    !Regex.IsMatch(code, "^\\d{4}$") || // 验证码4位数字
                    Int32.Parse(iden) < 1 || Int32.Parse(iden) > 5 // 身份在[1,5]
                    )
                {
                    return;
                }
                // 验证码检查
                if(/*AuthCode.CheckAuthCode(phone,code)*/code == "")
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "errorCallback", "alterError($('input[name=code]')[0]);", true);
                    return;
                }

                // 身份证（业务员-施工队-管理员）
                String IDCardYWY;
                // 身份证（经销商）
                String IDCardJXS;
                // 区县（经销商）
                String location;
                // 详细地址
                String detailLocation;

                // 普通会员
                if (iden == "1")
                {
                    string sql = "insert into users(name, phone, identity) values(?name, ?phone, ?iden);";
                    MySqlParameter[] para = new MySqlParameter[3];
                    para[0] = new MySqlParameter("?name", name);
                    para[1] = new MySqlParameter("?phone", phone);
                    para[2] = new MySqlParameter("?iden", iden);
                    try
                    {
                        int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                        if(ret == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "success", "success();", true);
                        }
                    }
                    catch(MySqlException ex)
                    {
                        //
                    }
                    /// ret == 1
                }
                // 业务员-施工队-管理员
                else if (iden == "2" || iden == "4" || iden == "5")
                {
                    IDCardYWY = Request.Form["idcard_ywy"];
                    string sql = "insert into users(name, phone, identity, IDCard) values(?name, ?phone, ?iden, ?IDCard);";
                    MySqlParameter[] para = new MySqlParameter[4];
                    para[0] = new MySqlParameter("?name", name);
                    para[1] = new MySqlParameter("?phone", phone);
                    para[2] = new MySqlParameter("?iden", iden);
                    para[3] = new MySqlParameter("?IDCard", IDCardYWY);
                    int ret = MySQLHelper.ExecuteNonQuery(sql, para);
                    /// ret == 1
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
            if (Regex.IsMatch(tel.Value, "^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$"))
            {
                AuthCode.SendAuthCode(tel.Value);
            }
        }
    }
}