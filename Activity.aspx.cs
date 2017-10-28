using System;

namespace WXShare
{
    public partial class Activity : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }

            string activityHTML =
"<a href=\"/ActivityDetail.aspx?aid=#id#\" class=\"weui-media-box weui-media-box_appmsg\">" +
"    <div class=\"weui-media-box__hd\">" +
"        <img class=\"weui-media-box__thumb\" src=\"#img#\" alt=\"\">" +
"    </div>" +
"    <div class=\"weui-media-box__bd\">" +
"        <h4 class=\"weui-media-box__title\">#title#</h4>" +
"        <p class=\"weui-media-box__desc\">#brief#</p>" +
"    </div>" +
"</a>";

            var activityList = DataBase.Activity.Gets();
            activities.InnerHtml = "";
            foreach (var activity in activityList)
            {
                if (activity.imgSrc == null || activity.imgSrc == "")
                {
                    activity.imgSrc = WXManage.QRCode(Request.Url.Host + "/ActivityDetail.aspx?aid=" + activity.id);
                }
                activities.InnerHtml += activityHTML
                    .Replace("#id#", activity.id)
                    .Replace("#img#", activity.imgSrc)
                    .Replace("#title#", activity.title)
                    .Replace("#brief#", activity.brief);
            }
        }
    }
}