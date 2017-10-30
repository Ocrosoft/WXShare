using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class RequireWX : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Request.QueryString.AllKeys.Contains("url"))
            {
                Response.Redirect("/UserLogin.aspx");
                return;
            }
            var url = Request.Url.ToString().Substring(Request.Url.ToString().IndexOf('=') + 1);
            qrcode.Src = WXManage.QRCode(url);
        }
    }
}