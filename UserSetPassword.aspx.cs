using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class UserSetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if (Session["phone"] == null)
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }

            if (IsPostBack)
            {
                var pass1 = Request.Form["password1"];
                var pass2 = Request.Form["password2"];
                if(pass1 == "" || pass2 =="")
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "empty", "alert('密码不能为空！')", true);
                    return;
                }
                if(pass1 != pass2)
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "equal", "alert('两次输入密码不一致！')", true);
                    return;
                }

                var user = DataBase.User.Get(new Objects.User()
                {
                    phone = Session["phone"].ToString(),
                    identity = Session["iden"].ToString()
                });
                user.password = pass1;
                if (DataBase.User.ModifyPassword(user))
                {
                    Response.Redirect("/UserIndex.aspx");
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('系统错误')", true);
                    return;
                }
            }
        }
    }
}