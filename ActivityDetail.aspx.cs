using MySql.Data.MySqlClient;
using Ocrosoft;
using System;
using System.Linq;
using System.Web.UI;

namespace WXShare
{
    public partial class ActivityDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            /*if (!WXManage.IsWXBrowser(Request))
            {
                Response.Clear();
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }*/
            // 未登录，作为报名页面
            if(Session["phone"] == null || Session["iden"] == null)
            {
                signBtn1.InnerText = "立即报名";
                signBtn2.InnerText = "立即报名";
            }
            // 普通会员、管理员可查看
            if(Session["iden"] != null && 
                Session["iden"].ToString() != "1" &&
                Session["iden"].ToString() != "5")
            {
                Response.Clear();
                Response.Redirect("/UserIndex.aspx");
                return;
            }
            // 没有参数
            if (!Request.QueryString.AllKeys.Contains("aid"))
            {
                Response.Clear();
                Response.Redirect("/Activity.aspx");
                return;
            }
            string aid = Request.QueryString["aid"].ToString();
            string sql = "select title, timeend, content, brief from activity where Id = ?aid and valid = 1";
            MySqlParameter para = new MySqlParameter("?aid", aid);
            try
            {
                var ret = MySQLHelper.ExecuteDataSet(sql, para);
                // 活动标题
                string title = ret.Tables[0].Rows[0].ItemArray[0].ToString();
                // 活动结束时间
                string endTime = ret.Tables[0].Rows[0].ItemArray[1].ToString().Split(' ')[0].Replace('/', '-');
                // 活动内容(HTML)
                string content = ret.Tables[0].Rows[0].ItemArray[2].ToString();
                // 活动简介(显示在为网页标题)
                string brief = ret.Tables[0].Rows[0].ItemArray[3].ToString();
                activity_name.InnerText = activity_name.InnerText.Replace("#title#", title);
                end_time.InnerText = end_time.InnerText.Replace("#end-time#", endTime);
                js_content.InnerHtml = js_content.InnerHtml.Replace("#content#", content);
                Title = brief;
            }
            catch(MySqlException ex)
            {
                //
            }
            
            /*appId: appId
               timestamp: timestamp
               nonceStr: nonceStr
               signature: signature*/
            var timestamp = OSecurity.DateTimeToTimeStamp(DateTime.Now);
            String script = "var appId = '" + WXManage.appID + "';";
            script += "var timestamp = '" + timestamp.ToString() + "';";
            script += "var nonceStr = 'chenyanhong';";
            script += "var signature = '" + WXManage.WXJSSign("chenyanhong", timestamp, Request.Url.ToString().Split('#')[0]) + "';";
            if(Session["phone"] != null)
            {
                script += "var uid = '" + Session["phone"].ToString() + "';";
            }
            ScriptManager.RegisterClientScriptBlock(this, GetType(), "wxvar", script, true);
        }
    }
}