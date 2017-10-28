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
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            // 没有登录
            if (Session["phone"] == null)
            {
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
                Response.Redirect("/UserSetPassword.aspx");
                return;
            }

            if(Session["iden"].ToString() == "2")
            {
                // 新的派单订单
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
                    newOrderCountYWY.Style["display"] = "";
                    newOrderCountYWY.InnerText = count.ToString();
                }
            }
            else if (Session["iden"].ToString() == "4")
            {
                // 新的派工订单
                var team = DataBase.Team.Get(user);
                var list = DataBase.Order.Gets(team);
                int count = 0;
                foreach (var order in list)
                {
                    if (order.status == 8)
                    {
                        count++;
                    }
                }
                if (count != 0)
                {
                    newOrderCountSGD.Style["display"] = "";
                    newOrderCountSGD.InnerText = count.ToString();
                }
            }
            else if(Session["iden"].ToString() == "5")
            {
                // 新的活动报名
                var asign = DataBase.ActivitySign.Gets();
                if(asign.Count!=0)
                {
                    newActivitySign.Style["display"] = "";
                    newActivitySign.InnerText = asign.Count.ToString();
                }
                // 新的注册
                var userAuth = DataBase.UserUnAuth.Gets();
                if(userAuth.Count != 0 )
                {
                    newRegister.Style["display"] = "";
                    newRegister.InnerText = userAuth.Count.ToString();
                }
            }
        }
    }
}