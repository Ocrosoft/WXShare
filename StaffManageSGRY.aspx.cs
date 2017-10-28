using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class staffManageSGRY : System.Web.UI.Page
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

            string html =
"<div class=\"weui-cell weui-cell_access\" onclick=\"location.href='/StaffSGRYDetail.aspx?uid=#id#';\">" +
"	<div class=\"weui-cell__bd\">#name#</div>" +
"	<div class=\"weui-cell__ft\" style=\"font-size: 0\">" +
"		<span style=\"vertical-align: middle; font-size: 17px;\">#phone#</span>#noteam#" +
"	</div>" +
"</div>";
            string noTeam = "<span class=\"weui-badge weui-badge_dot\" style=\"margin-left: 5px; margin-right: 5px; \"></span>";
            var noTeamList = DataBase.Team.UnRegisterToTeam();
            Dictionary<string, bool> unRegister = new Dictionary<string, bool>();
            foreach(var item in noTeamList)
            {
                unRegister[item.phone] = true;
            }

            var array = DataBase.User.Gets("4");
            list.InnerHtml = "";
            foreach (var item in array)
            {
                list.InnerHtml += html
                    .Replace("#id#", item.id)
                    .Replace("#name#", item.name)
                    .Replace("#phone#", item.phone)
                    .Replace("#noteam#", unRegister.ContainsKey(item.phone) ? noTeam : "");
            }
        }
    }
}