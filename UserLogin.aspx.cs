using Ocrosoft;
using System;
using System.Web;

namespace WXShare
{
    public partial class UserLogin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if(Session["phone"] != null && Session["iden"]!= null)
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }

            if (IsPostBack)
            {
                String phone = Request.Form["tel"];
                String password = Request.Form["password"];
                int iden = Int32.Parse(Request.Form["iden"]);
                bool reme = Request.Form["autoLogin"] == "on";

                // 格式检查
                if (!OSecurity.ValidPhone(phone) || // 手机号
                    iden < 1 || iden > 5 // 身份在[1,5]
                    )
                {
                    return;
                }

                // 普通会员-业务员-施工队-管理员
                if (iden == 1 || iden == 2 || iden == 4 || iden == 5)
                {
                    if (DataBase.User.Login(new Objects.User()
                    {
                        phone = phone,
                        password = password,
                        identity = iden.ToString()
                    }))
                    {
                        Session["phone"] = phone;
                        Session["iden"] = iden;
                        if (reme)
                        {
                            var telc = new HttpCookie("tel", phone);
                            telc.Expires = DateTime.Now.AddDays(15);
                            var pasc = new HttpCookie("__p", OSecurity.AESEncrypt(password));
                            pasc.Expires = DateTime.Now.AddDays(15);
                            var idenc = new HttpCookie("ide", iden.ToString());
                            idenc.Expires = DateTime.Now.AddDays(15);
                            Response.SetCookie(telc);
                            Response.SetCookie(pasc);
                            Response.SetCookie(idenc);
                        }
                        Response.Redirect("/UserIndex.aspx");
                    }
                }
            }
            else
            {
                /* 自动登录 */
                if (Request.Cookies["tel"] != null && Request.Cookies["__p"] != null && Request.Cookies["ide"] != null)
                {
                    var phone = Request.Cookies["tel"].Value;
                    var password = Request.Cookies["__p"].Value;
                    var iden = Request.Cookies["ide"].Value;
                    if (DataBase.User.Login(new Objects.User()
                    {
                        phone = phone,
                        password = OSecurity.AESDecrypt(password),
                        identity = iden.ToString()
                    }))
                    {
                        Session["phone"] = phone;
                        Session["iden"] = iden;
                        Response.Redirect("/UserIndex.aspx");
                    }
                }
            }
        }
    }
}