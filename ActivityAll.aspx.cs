using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class ActivityAll : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }

            // 只有管理员能查看所有活动
            if (Session["phone"] == null || Session["iden"].ToString() != "5")
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }

            string activityHTML =
"<a href=\"/ActivityEditor.aspx?aid=#id#\" class=\"weui-media-box weui-media-box_appmsg\">" +
"    <div class=\"weui-media-box__hd\">" +
"        <img class=\"weui-media-box__thumb\" src=\"#img#\" alt=\"\">" +
"    </div>" +
"    <div class=\"weui-media-box__bd\">" +
"        <h4 class=\"weui-media-box__title\">#title#</h4>" +
"        <p class=\"weui-media-box__desc\">#brief#</p>" +
"    </div>" +
"</a>";
            // 获取所有活动
            var activityList = DataBase.Activity.GetsAll();
            activities.InnerHtml = "";
            foreach (var activity in activityList)
            {
                activities.InnerHtml += activityHTML
                    .Replace("#id#", activity.id)
                    .Replace("#img#", "")
                    .Replace("#title#", activity.title)
                    .Replace("#brief#", activity.brief);
            }
        }
    }
}