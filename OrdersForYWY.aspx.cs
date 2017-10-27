using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class OrdersForYWY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["phone"] == null)
            {
                Response.Clear();
                Response.Redirect("/UserLogin.aspx");
                return;
            }
            if (Session["iden"].ToString() != "2")
            {
                Response.Clear();
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
"<a class=\"weui-cell weui-cell_access\" href=\"/OrderDetailForYWY.aspx?oid=#id#\">" +
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
            List<Objects.Order> list = new List<Objects.Order>();
            orderList.InnerHtml = "";
            if (aid == "-1")
            {
                list = DataBase.Order.Gets(user);
            }
            else
            {
                var listTmp = DataBase.Order.Gets(user);
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
                    .Replace("#new#", order.status == 0 ? newOrder : "");
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
"<a class=\"weui-cell weui-cell_access\" href=\"/OrderDetail.aspx?oid=#id#\">" +
"    <div class=\"weui-cell__bd\">" +
"        <p>#content#</p>" +
"   </div>" +
"   <div class=\"weui-cell__ft\">#new#" +
"    </div>" +
"</a>";
            string newOrder = "<span class=\"weui-badge weui-badge_dot\" style=\"margin-left: 5px; margin-right: 5px; \"></span>";
            string aid = statusSelect.Value;
            string key = searchInput.Value;
            var list = DataBase.Order.Search(key);
            orderList.InnerHtml = "";
            int validCount = 0;
            foreach (var order in list)
            {
                if (order.commissioner != Session["phone"].ToString())
                {
                    continue;
                }
                validCount++;
                orderList.InnerHtml += item
                    .Replace("#id#", order.id)
                    .Replace("#content#", order.name + " " + order.phone)
                    .Replace("#new#", order.status == 0 ? newOrder : "");
            }
            if (validCount == 0)
            {
                orderList.InnerHtml = "<p style=\"text-align:center\">找不到任何订单</p>";
            }
        }

    }
}