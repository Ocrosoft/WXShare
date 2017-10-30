using Ocrosoft;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class ActivitySignView : System.Web.UI.Page
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

            // 获取所有活动，显示标题
            var activitys = DataBase.Activity.GetsAll();
            activitySelect.Items.Clear();
            activitySelect.Items.Add(new ListItem("所有活动", "0"));
            var firstActivityHasSign = "0";
            foreach (var activity in activitys)
            {
                var count = DataBase.ActivitySign.Gets(activity).Count;
                if(count != 0 && firstActivityHasSign == "0")
                {
                    firstActivityHasSign = activity.id;
                }
                // 报名数量不为0的显示数量
                activitySelect.Items.Add(new ListItem(activity.title + (count == 0 ? "" : ("(" + count + ")")), activity.id));
            }
            // 有新报名，则显示新报名信息
            if(firstActivityHasSign != "0" && !IsPostBack) // 回发时不处理
            {
                activitySelect.Value = firstActivityHasSign;
                ScriptManager.RegisterStartupScript(this, GetType(), "show", "$('#selectActivity').click();", true);
            }
        }

        protected void vcodeBtn_Click(object sender, EventArgs e)
        {
            string signItem =
"<a class=\"weui-cell weui-cell_access\" href=\"/ActivitySignViewDetail.aspx?sid=#id#\">" +
"    <div class=\"weui-cell__bd\">" +
"        <p>#content#</p>" +
"   </div>" +
"   <div class=\"weui-cell__ft\">" +
"    </div>" +
"</a>";
            string aid = Request.Form["activitySelect"];
            activitySelect.Value = aid;
            List<Objects.ActivitySign> list = new List<Objects.ActivitySign>();
            if(aid=="0")
            {
                list = DataBase.ActivitySign.Gets();
            }
            else
            {
                list = DataBase.ActivitySign.Gets(new Objects.Activity() { id = aid });
            }
            // 显示报名情况
            signList.InnerHtml = "";
            foreach(var sign in list)
            {
                signList.InnerHtml += signItem
                    .Replace("#id#", sign.id)
                    .Replace("#content#", sign.name + " " + sign.phone);
            }
            if(list.Count == 0)
            {
                signList.InnerHtml = "<p style=\"text-align:center;\">找不到任何报名记录</p>";
            }
        }
    }
}