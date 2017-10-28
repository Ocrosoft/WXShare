using Ocrosoft;
using System;
using System.Linq;
using System.Web.UI;

namespace WXShare
{
    public partial class ActivityDetail : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            // 未登录，作为报名页面
            if (Session["phone"] == null || Session["iden"] == null)
            {
                signBtn1.InnerText = "立即报名";
                signBtn2.InnerText = "立即报名";
            }
            // 普通会员、管理员可查看
            if (Session["iden"] != null &&
                Session["iden"].ToString() != "1" &&
                Session["iden"].ToString() != "5")
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }
            // 没有参数
            if (!Request.QueryString.AllKeys.Contains("aid"))
            {
                Response.Redirect("/Activity.aspx");
                return;
            }
            // 活动ID
            string aid = Request.QueryString["aid"].ToString();
            // 获取活动
            var activity = DataBase.Activity.Get(new Objects.Activity() { id = aid });
            activity_name.InnerText = activity_name.InnerText.Replace("#title#", activity.title);
            end_time.InnerText = end_time.InnerText.Replace("#end-time#", activity.timeEnd.ToString("yyyy-MM-dd HH:mm:ss"));
            js_content.InnerHtml = js_content.InnerHtml.Replace("#content#", activity.content);
            Title = activity.brief;

            /*
             * appId: appId
             * timestamp: timestamp
             * nonceStr: nonceStr
             * signature: signature
             */
            var timestamp = OSecurity.DateTimeToTimeStamp(DateTime.Now);
            String script = "var appId = '" + WXManage.appID + "';";
            script += "var timestamp = '" + timestamp.ToString() + "';";
            script += "var nonceStr = 'chenyanhong';";
            script += "var signature = '" + WXManage.WXJSSign("chenyanhong", timestamp, Request.Url.ToString().Split('#')[0]) + "';";
            script += "var imgUrl = '" + WXManage.QRCode(Request.Url.ToString() + "&uid=" + Session["phone"]) + "';";
            if (Session["phone"] != null)
            {
                script += "var uid = '" + Session["phone"].ToString() + "';";
            }
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "wxvar", script, true);
        }
    }
}