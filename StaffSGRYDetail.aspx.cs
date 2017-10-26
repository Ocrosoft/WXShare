using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class StaffSGRYDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["uid"] == null)
            {
                Response.Clear();
                Response.Redirect("/StaffManageSGRY.aspx");
                return;
            }

            var uid = Request.QueryString["uid"];
            var user = DataBase.User.GetByID(new Objects.User()
            {
                id = uid
            });
            name.Value = user.name;
            tel.Value = user.phone;
            idCard.Value = user.IDCard;

            var teams = DataBase.Team.Gets();
            teamSelect.Items.Clear();
            teamSelect.Items.Add(new ListItem("请选择施工队", "0"));
            foreach(var team in teams)
            {
                teamSelect.Items.Add(new ListItem(team.teamName, team.id));
            }

            var signedTeam = DataBase.Team.Get(user);
            if(signedTeam == null)
            {
                team.Value = "未加入";
            }
            else
            {
                team.Value = signedTeam.teamName;
                addToTeam.InnerText = "修改所在施工队";
                teamSelect.Items.Insert(1, new ListItem("从施工队中移除", "-1"));
            }
        }
        protected void DialogOK_Click(Object sender, EventArgs e)
        {
            if (DataBase.User.Delete(new Objects.User()
            {
                id = Request.QueryString["uid"]
            }))
            {
                Response.Redirect("/StaffManageYWY.aspx");
                return;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('删除失败，服务器错误')", true);
            }
        }

        protected void TeamOK_Click(Object sender,EventArgs e)
        {
            if(Request.Form["teamSelect"] == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('请选择要添加的施工队')", true);
                return;
            }

            var user = DataBase.User.GetByID(new Objects.User() { id = Request.QueryString["uid"] });
            var team = DataBase.Team.Get(new Objects.Team() { id = Request.Form["teamSelect"] });
            if (Request.Form["teamSelect"] == "-1")
            {
                if (DataBase.Team.RemoveFromTeam(user))
                {
                    Response.Redirect(Request.Url.ToString());
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('退出施工队失败，服务器错误')", true);
                }
                return;
            }
            if (!DataBase.Team.AddToTeam(user, team))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('加入施工队失败，服务器错误')", true);
            }
            Response.Redirect(Request.Url.ToString());
        }
        protected void ButtonOK_Click(Object sender ,EventArgs e)
        {
            var usr = new Objects.User()
            {
                id = Request.QueryString["uid"],
                name = Request.Form["name"],
                phone = Request.Form["tel"],
                IDCard = Request.Form["idCard"]
            };
            try
            {
                if (!DataBase.User.Modify(usr))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('保存失败，服务器错误')", true);
                }
            }
            catch(MySql.Data.MySqlClient.MySqlException ex)
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('保存失败，需要先退出施工队')", true);
            }
            Response.Redirect(Request.Url.ToString());
        }
    }
}