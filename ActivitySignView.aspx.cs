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
            foreach (var activity in activitys)
            {
                activitySelect.Items.Add(new ListItem(activity.title, activity.id));
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
            string aid = activitySelect.Value;
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
        }
    }
}