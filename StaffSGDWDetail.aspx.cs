using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class StaffSGDWDetail : System.Web.UI.Page
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
            if (Request.QueryString["tid"] == null)
            {
                Response.Redirect("/StaffManageSGDW.aspx");
                return;
            }

            string listHTML =
"<a class=\"weui-cell weui-cell_access\" href=\"/StaffSGRYDetail.aspx?uid=#id#\">" +
"	<div class=\"weui-cell__bd\">" +
"		<p>#name#</p>" +
"	</div>" +
"	<div class=\"weui-cell__ft\">" +
"	</div>" +
"</a>";

            if (Request.QueryString["tid"] == null)
            {
                Response.Clear();
                Response.Redirect("/StaffManageSGDW.aspx");
                return;
            }

            var tid = Request.QueryString["tid"];
            var team = DataBase.Team.GetWithMembers(new Objects.Team() { id = tid });
            name.Value = team.teamName;
            foreach (var user in team.members)
            {
                members.InnerHtml += listHTML.Replace("#name#", user.name).Replace("#id#", user.id);
            }
            if (team.members.Count == 0)
            {
                members.InnerHtml = "<p style=\"text-align:center;\"><a href=\"/StaffManageSGRY.aspx\">该队伍没有成员，点击前往添加</a></p>";
            }
        }

        protected void ButtonOK_Click(Object sender, EventArgs e)
        {
            string id = Request.QueryString["tid"];
            string name = Request.Form["name"];

            if (!DataBase.Team.Modify(new Objects.Team()
            {
                id = id,
                teamName = name
            }
            ))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('保存失败，系统错误');", true);
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            var team = new Objects.Team()
            {
                id = Request.QueryString["tid"]
            };

            if (!DataBase.Team.Delete(team))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('解散失败，系统错误');", true);
            }
            Response.Redirect("/StaffManageSGDW.aspx");
        }
    }
}