using System;
using System.Web;

namespace WXShare
{
    public partial class UserLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
        }

        protected void Logout(object sender, EventArgs e)
        {
            Session["phone"] = null;
            Session["iden"] = null;

            var telc = new HttpCookie("tel", null);
            telc.Expires = DateTime.Now.AddDays(-1);
            var pasc = new HttpCookie("__p", null);
            pasc.Expires = DateTime.Now.AddDays(-1);
            var idenc = new HttpCookie("ide", null);
            idenc.Expires = DateTime.Now.AddDays(-1);
            Response.SetCookie(telc);
            Response.SetCookie(pasc);
            Response.SetCookie(idenc);
            Response.Redirect("/UserLogin.aspx");
        }
    }
}