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
            // 没有登录
            if(Session["phone"] == null)
            {
                Response.Clear();
                Response.Redirect("/UserLogin.aspx");
                return;
            }
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

            if(Session["iden"].ToString() == "2")
            {
                var list = DataBase.Order.Gets(user);
                int count = 0;
                foreach(var order in list)
                {
                    if(order.status == 0)
                    {
                        count++;
                    }
                }
                if(count!=0)
                {
                    newOrderCount.Style["display"] = "";
                    newOrderCount.InnerText = count.ToString();
                }
            }
        }
    }
}