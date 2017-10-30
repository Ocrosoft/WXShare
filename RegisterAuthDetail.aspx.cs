using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class RegisterAuthDetail : System.Web.UI.Page
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
            if (Request.QueryString["rid"] == "")
            {
                Response.Clear();
                Response.Redirect("/RegisterAuth.aspx");
                return;
            }

            var rid = Request.QueryString["rid"];
            var rauth = DataBase.UserUnAuth.Get(new Objects.User() { id = rid });

            inputName.Value = rauth.name;
            inputPhone.Value = rauth.phone;
            var identity = "";
            switch(rauth.identity)
            {
                case "2":
                    identity = "业务员";
                    break;
                case "4":
                    identity = "施工队";
                    break;
                case "5":
                    identity = "管理员";
                    break;
            }
            inputIden.Value = identity;
            inputIDCard.Value = rauth.IDCard;
        }
        protected void AuthBtn_Click(object sender, EventArgs e)
        {
            string rid = Request.QueryString["rid"];
            if (DataBase.UserUnAuth.Auth(new Objects.User() { id = rid }))
            {
                // 发送新注册提示
                var admins = DataBase.User.Gets("5");
                foreach (var admin in admins)
                {
                    var openid = DataBase.User.GetOpenID(admin);
                    if (!string.IsNullOrEmpty(openid))
                    {
                        WXManage.SendMessage(openid, "新注册待审核！");
                    }
                }
                Response.Redirect("/UserRegisterAuth.aspx");
                return;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('审核失败，服务器错误');", true);
                return;
            }
        }

        protected void delete_Click(Object sender,EventArgs e)
        {
            string rid = Request.QueryString["rid"];
            if (DataBase.UserUnAuth.Delete(new Objects.User() { id = rid }))
            {
                Response.Clear();
                Response.Redirect("/UserRegisterAuth.aspx");
                return;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('删除失败，服务器错误');", true);
                return;
            }
        }
    }
}