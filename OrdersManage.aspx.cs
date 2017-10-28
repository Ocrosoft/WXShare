using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class OrdersManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if (Session["phone"] == null || Session["iden"].ToString() != "5")
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
"<a class=\"weui-cell weui-cell_access\" href=\"/OrderDetail.aspx?oid=#id#\">" +
"    <div class=\"weui-cell__bd\">" +
"        <p>#content#</p>" +
"   </div>" +
"   <div class=\"weui-cell__ft\">" +
"    </div>" +
"</a>";
            string aid = Request.Form["statusSelect"];
            List<Objects.Order> list = new List<Objects.Order>();
            orderList.InnerHtml = "";
            if (aid == "-1")
            {
                list = DataBase.Order.Gets();
            }
            else
            {
                list = DataBase.Order.Gets(new Objects.Status() { id = aid });
            }
            foreach (var sign in list)
            {
                orderList.InnerHtml += item.Replace("#id#", sign.id).Replace("#content#", sign.name + " " + sign.phone);
            }
            if(list.Count == 0)
            {
                orderList.InnerHtml = "<p style=\"text-align:center\">找不到任何订单</p>";
            }
            statusSelect.Value = aid; 
        }

        protected void searchBtn_Click(Object sender,EventArgs e)
        {
            string item =
"<a class=\"weui-cell weui-cell_access\" href=\"/OrderDetail.aspx?oid=#id#\">" +
"    <div class=\"weui-cell__bd\">" +
"        <p>#content#</p>" +
"   </div>" +
"   <div class=\"weui-cell__ft\">" +
"    </div>" +
"</a>";
            string aid = statusSelect.Value;
            string key = searchInput.Value;
            var list = DataBase.Order.Search(key);
            orderList.InnerHtml = "";
            foreach (var sign in list)
            {
                orderList.InnerHtml += item.Replace("#id#", sign.id).Replace("#content#", sign.name + " " + sign.phone);
            }
            if (list.Count == 0)
            {
                orderList.InnerHtml = "<p style=\"text-align:center\">找不到任何订单</p>";
            }
        }
    }
}