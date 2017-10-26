using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class StaffManageYWY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string html =
"<div class=\"weui-cell weui-cell_access\" onclick=\"location.href='/StaffYWYDetail.aspx?uid=#id#';\">" +
"	<div class=\"weui-cell__bd\">#name#</div>" +
"	<div class=\"weui-cell__ft\" style=\"font-size: 0\">" +
"		<span style=\"vertical-align: middle; font-size: 17px;\">#phone#</span>" +
"	</div>" +
"</div>";

            var array = DataBase.User.Gets("2");
            list.InnerHtml = "";
            foreach (var item in array)
            {
                list.InnerHtml += html
                    .Replace("#id#", item.id)
                    .Replace("#name#", item.name)
                    .Replace("#phone#", item.phone);
            }
        }
    }
}