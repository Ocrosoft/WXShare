using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class StaffManageSGDW : System.Web.UI.Page
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
"<div class=\"weui-cell weui-cell_access\" onclick=\"location.href='/StaffSGDWDetail.aspx?tid=#id#';\">" +
"	<div class=\"weui-cell__bd\">#name#</div>" +
"	<div class=\"weui-cell__ft\" style=\"font-size: 0\">" +
"		<span style=\"vertical-align: middle; font-size: 17px;\">#memberCount#人</span>" +
"	</div>" +
"</div>";

            var array = DataBase.Team.GetsWithMembers();
            var allTeam = DataBase.Team.Gets();
            foreach(var team in allTeam)
            {
                if(!array.ContainsKey(team.id))
                {
                    array[team.id] = team;
                }
            }
            list.InnerHtml = "";
            foreach (var item in array)
            {
                list.InnerHtml += html
                    .Replace("#id#", item.Key)
                    .Replace("#name#", item.Value.teamName)
                    .Replace("#memberCount#", item.Value.members.Count.ToString());
            }
        }

        protected void addTeam_Click(Object sender,EventArgs e)
        {
            var teamName = Request.Form["inputAddTeam"];
            if(string.IsNullOrEmpty(teamName))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('请输入队伍名称');", true);
                return;
            }
            if(!DataBase.Team.Add(new Objects.Team() { teamName = teamName}))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('创建失败，系统错误');", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }
    }
}