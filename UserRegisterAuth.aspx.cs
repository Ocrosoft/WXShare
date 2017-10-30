using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class UserRegisterAuth : System.Web.UI.Page
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
        }

        protected void vcodeBtn_Click(object sender, EventArgs e)
        {
            string signItem =
"<a class=\"weui-cell weui-cell_access\" href=\"/RegisterAuthDetail.aspx?rid=#id#\">" +
"    <div class=\"weui-cell__bd\">" +
"        <p>#content#</p>" +
"   </div>" +
"   <div class=\"weui-cell__ft\">" +
"    </div>" +
"</a>";
            string iden = Request.Form["activitySelect"];
            activitySelect.Value = iden;
            List<Objects.User> list = new List<Objects.User>();
            if (iden == "0")
            {
                list = DataBase.UserUnAuth.Gets();
            }
            else
            {
                list = DataBase.UserUnAuth.Gets(new Objects.User() { identity = iden });
            }
            regList.InnerHtml = "";
            foreach (var sign in list)
            {
                regList.InnerHtml += 
                    signItem
                    .Replace("#id#", sign.id)
                    .Replace("#content#", sign.name + " " + sign.phone + " " + sign.IDCard);
            }
            if(list.Count == 0)
            {
                regList.InnerHtml = "<p style=\"text-align:center;\">找不到任何待审核记录</p>";
            }
        }
       
    }
}