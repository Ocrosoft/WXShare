using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class UserProfile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if(Session["phone"] == null)
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }

            var user = DataBase.User.Get(new Objects.User()
            {
                phone = Session["phone"].ToString(),
                identity = Session["iden"].ToString()
            });
            inputName.Value = user.name;
            inputPhone.Value = user.phone;
            if(user.identity == "1") // 普通会员
            {
                IDCardDiv.InnerHtml = "";
                IDCardDiv.Style["display"] = "none";
            }
            else
            {
                inputIDCard.Value =
                    user.IDCard.Substring(0, 6) + new string('*', 8) + user.IDCard.Substring(14);
            }
            var openID = DataBase.User.GetOpenID(user);
            if (!string.IsNullOrEmpty(openID))
            {
                inputOpenID.Value = openID;
                save.InnerText = "修改消息标识";
            }
        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            var openID = Request.Form["inputOpenID"];
            var user = DataBase.User.Get(new Objects.User()
            {
                phone = Session["phone"].ToString(),
                identity = Session["iden"].ToString()
            });

            if (!DataBase.User.SaveOpenID(user,openID))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "保存失败，服务器错误", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }
    }
}