using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class StaffYWYDetail : System.Web.UI.Page
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
            if (Request.QueryString["uid"] == null)
            {
                Response.Redirect("/StaffManageYWY.aspx");
                return;
            }

            if (Request.QueryString["del"] == "true")
            {
                if (DataBase.User.Delete(new Objects.User()
                {
                    id = Request.QueryString["uid"]
                }
                ))
                {
                    Response.Redirect("/StaffManageYWY.aspx");
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('删除失败，服务器错误')", true);
                }
            }

            var uid = Request.QueryString["uid"];
            var user = DataBase.User.GetByID(new Objects.User()
            {
                id = uid
            });
            name.Value = user.name;
            tel.Value = user.phone;
            idCard.Value = user.IDCard;
        }

        protected void DialogOK_Click(Object sender, EventArgs e)
        {
            var usr = new Objects.User()
            {
                id = Request.QueryString["uid"],
                name = Request.Form["name"],
                phone = Request.Form["tel"],
                IDCard = Request.Form["idCard"]
            };
            if (!DataBase.User.Modify(usr))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('保存失败，服务器错误')", true);
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            var usr = new Objects.User()
            {
                id = Request.QueryString["uid"]
            };
            if (!DataBase.User.Delete(usr))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('删除失败，服务器错误')", true);
            }
            Response.Redirect("/StaffManageYWY.aspx");
        }
    }
}