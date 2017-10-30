using Ocrosoft;
using System;
using System.Web.UI;

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

        protected void newActivity_Click(Object sender, EventArgs e)
        {
            // 新建活动，标题初始化为时间 + 一随机数
            var activity = new Objects.Activity()
            {
                timeStart = DateTime.Now,
                timeEnd = DateTime.Now,
                title = OSecurity.DateTimeToTimeStamp(DateTime.Now).ToString() + new Random().Next(0, 100).ToString(),
                content = "",
                brief = "",
                template = int.Parse(DataBase.Template.Gets()[0].id),
                templateAddition = ""
            };
            // 新建
            if (!DataBase.Activity.Add(activity))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('新建失败，服务器错误')", true);
                return;
            }
            // 获取ID，转到编辑界面
            activity = DataBase.Activity.GetByTitle(activity);
            Response.Redirect("/ActivityEditor.aspx?aid=" + activity.id);
        }
    }
}