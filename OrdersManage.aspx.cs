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
            var statusList = new DataBase.Status().Gets();
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
            string aid = statusSelect.Value;
            List<Objects.Order> list = new List<Objects.Order>();
            if (aid == "0")
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
        }
    }
}