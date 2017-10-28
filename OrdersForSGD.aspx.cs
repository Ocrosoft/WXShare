using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class OrdersForSGD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if (Session["phone"] == null || Session["iden"].ToString() != "4")
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }

            var statusList = new DataBase.Status().Gets();
            statusSelect.Items.Clear();
            statusSelect.Items.Add(new ListItem("所有订单", "-1"));
            foreach (var status in statusList)
            {
                statusSelect.Items.Add(new ListItem(status.view, status.id));
            }
        }

        protected void btn_Click(object sender, EventArgs e)
        {
            string item =
"<a class=\"weui-cell weui-cell_access\" href=\"/OrderDetailForSGD.aspx?oid=#id#\">" +
"    <div class=\"weui-cell__bd\">" +
"        <p>#content#</p>" +
"   </div>" +
"   <div class=\"weui-cell__ft\">#new#" +
"    </div>" +
"</a>";
            string newOrder = "<span class=\"weui-badge weui-badge_dot\" style=\"margin-left: 5px; margin-right: 5px; \"></span>";
            string aid = Request.Form["statusSelect"];
            var user = new Objects.User()
            {
                phone = Session["phone"].ToString(),
                identity = Session["iden"].ToString()
            };
            user = DataBase.User.Get(user);
            var team = DataBase.Team.Get(user);
            List<Objects.Order> list = new List<Objects.Order>();
            orderList.InnerHtml = "";
            if (aid == "-1")
            {
                list = DataBase.Order.Gets(team);
            }
            else
            {
                var listTmp = DataBase.Order.Gets(team);
                foreach (var order in listTmp)
                {
                    if (order.status == int.Parse(aid))
                    {
                        list.Add(order);
                    }
                }
            }
            foreach (var order in list)
            {
                orderList.InnerHtml += item
                    .Replace("#id#", order.id)
                    .Replace("#content#", order.name + " " + order.phone)
                    .Replace("#new#", order.status == 8 ? newOrder : "");
            }
            if (list.Count == 0)
            {
                orderList.InnerHtml = "<p style=\"text-align:center\">找不到任何订单</p>";
            }
            statusSelect.Value = aid;
        }

        protected void searchBtn_Click(Object sender, EventArgs e)
        {
            string item =
"<a class=\"weui-cell weui-cell_access\" href=\"/OrderDetailForSGD.aspx?oid=#id#\">" +
"    <div class=\"weui-cell__bd\">" +
"        <p>#content#</p>" +
"   </div>" +
"   <div class=\"weui-cell__ft\">#new#" +
"    </div>" +
"</a>";
            string newOrder = "<span class=\"weui-badge weui-badge_dot\" style=\"margin-left: 5px; margin-right: 5px; \"></span>";
            string aid = statusSelect.Value;
            string key = searchInput.Value;
            var user = new Objects.User()
            {
                phone = Session["phone"].ToString(),
                identity = Session["iden"].ToString()
            };
            var team = DataBase.Team.Get(user);
            var list = DataBase.Order.Search(key);
            orderList.InnerHtml = "";
            int validCount = 0;
            foreach (var order in list)
            {
                if (order.constructionTeam != team.id)
                {
                    continue;
                }
                validCount++;
                orderList.InnerHtml += item
                    .Replace("#id#", order.id)
                    .Replace("#content#", order.name + " " + order.phone)
                    .Replace("#new#", order.status == 8 ? newOrder : "");
            }
            if (validCount == 0)
            {
                orderList.InnerHtml = "<p style=\"text-align:center\">找不到任何订单</p>";
            }
        }
    }
}