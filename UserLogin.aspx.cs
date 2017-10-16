using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace WXShare
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(IsPostBack)
            {
                String phone = Request.Form["tel"];
                String password = Request.Form["password"];
                int iden = Int32.Parse(Request.Form["iden"]);
                bool reme = Request.Form["autoLogin"] == "on";

                // 格式检查
                if (!Regex.IsMatch(phone, "^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$") || // 手机号
                    iden < 1 || iden > 5 // 身份在[1,5]
                    )
                {
                    return;
                }

                // 普通会员-业务员-施工队-管理员
                if(iden == 1 || iden == 2 || iden == 4 || iden == 5)
                {
                    string sql = "select count(*) from users where phone=?phone and password=?pass and identity=?iden;";
                    MySqlParameter[] para = new MySqlParameter[3];
                    para[0] = new MySqlParameter("?phone", phone);
                    para[1] = new MySqlParameter("?pass", password);
                    para[2] = new MySqlParameter("?iden", iden);
                    try
                    {
                        Object ret = MySQLHelper.ExecuteScalar(sql, para);
                        if(Int32.Parse(ret.ToString()) == 1)
                        {
                            Session["phone"] = phone;
                            Session["iden"] = iden;
                            if(reme)
                            {
                                var telc = new HttpCookie("tel", phone);
                                telc.Expires = DateTime.Now.AddDays(15);
                                var pasc = new HttpCookie("__p", WXManage.EncryptAes(password));
                                pasc.Expires = DateTime.Now.AddDays(15);
                                var idenc = new HttpCookie("ide", iden.ToString());
                                idenc.Expires = DateTime.Now.AddDays(15);
                                Response.SetCookie(telc);
                                Response.SetCookie(pasc);
                                Response.SetCookie(idenc);
                                Response.Redirect("/UserIndex.aspx");
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        //
                    }
                }
            }
            else
            {
                /* 自动登录 */
                if(Request.Cookies["tel"] != null && Request.Cookies["__p"] != null && Request.Cookies["ide"] != null)
                {
                    var phone = Request.Cookies["tel"].Value;
                    var password = Request.Cookies["__p"].Value;
                    var iden = Request.Cookies["ide"].Value;
                    string sql = "select count(*) from users where phone=?phone and password=?pass and identity=?iden;";
                    MySqlParameter[] para = new MySqlParameter[3];
                    para[0] = new MySqlParameter("?phone", phone);
                    para[1] = new MySqlParameter("?pass", WXManage.DecryptAes(password));
                    para[2] = new MySqlParameter("?iden", iden);
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
    }
}