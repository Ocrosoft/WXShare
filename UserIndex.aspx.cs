using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class UserIndex : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["phone"] = "111";
            Session["iden"] = "5";
            // 没有登录
            if(Session["phone"] == null)
            {
                Response.Clear();
                Response.Redirect("/UserLogin.aspx");
                return;
            }
            return;
            // 没有设置密码
            var user = DataBase.User.Get(new Objects.User()
            {
                phone = Session["phone"].ToString(),
                identity=Session["iden"].ToString()
            });
            if(user.password == "")
            {
                Response.Clear();
                Response.Redirect("/UserSetPassword.aspx");
                return;
            }
        }
    }
}