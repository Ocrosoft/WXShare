using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ocrosoft;
using System.IO;

namespace WXShare
{
    public partial class ActivityEditor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["phone"] = "123";
            Session["iden"] = "5";
            if (Session["phone"] == null)
            {
                Response.Clear();
                Response.Redirect("/UserLogin.aspx");
                return;
            }
            if (Session["iden"].ToString() != "5")
            {
                Response.Clear();
                Response.Redirect("/UserIndex.aspx");
                return;
            }
            if (Request.QueryString["aid"] == null)
            {
                Response.Clear();
                Response.Redirect("/Activity.aspx");
                return;
            }

            if (IsPostBack)
            {
                var id = Request.QueryString["aid"];
                DateTime timeStart = DateTime.Parse(Request.Form["timeStart"]);
                DateTime timeEnd = DateTime.Parse(Request.Form["timeEnd"]);
                var title = Request.Form["title"];
                var content = Request.Unvalidated["htmlInput"];
                int template = int.Parse(Request.Form["templateSelect"]);
                var brief = Request.Form["brief"];
                bool valid = checkValid.Checked;
                var imgSrc = "";
                var templateAddition = Request.Form["templateAdditionInput"];

                if (title == "" ||
                    content == "" ||
                    brief == "" ||
                    templateAddition == ""||
                    Request.Files.Count > 1)
                {
                    return;
                }

                if (Request.Files.Count == 1)
                {
                    var imgName = Request.Files[0].FileName;
                    string path = "/WXShare/uploads/" + DateTime.Now.ToString("yyyyMMdd");
                    if (!Directory.Exists("/WXShare/uploads"))
                    {
                        Directory.CreateDirectory("/WXShare/uploads");
                    }
                    if(!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    path += "/" + OSecurity.DateTimeToTimeStamp(DateTime.Now) +
                        "." + imgName.Substring(imgName.LastIndexOf('.') + 1);
                    Request.Files[0].SaveAs(path);
                    imgSrc = path.Substring(8);
                }

                Objects.Activity modActivity = new Objects.Activity()
                {
                    id = id,
                    timeStart = timeStart,
                    timeEnd = timeEnd,
                    title = title,
                    content = content,
                    template = template,
                    brief = brief,
                    valid = valid,
                    imgSrc = imgSrc,
                    templateAddition = templateAddition
                };
                if (DataBase.Activity.Modify(modActivity))
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(), "saveFailed", "alert('保存成功');", true);
                    return;
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, GetType(),"saveFailed", "alert('保存失败');", true);
                    return;
                }
            }

            var activityID = Request.QueryString["aid"];
            var activity = DataBase.Activity.Get(new Objects.Activity() { id = activityID });

            timeStart.Value = activity.timeStart.ToString("yyyy-MM-ddThh:mm:ss");
            timeEnd.Value = activity.timeEnd.ToString("yyyy-MM-ddThh:mm:ss");
            title.Value = activity.title;
            textarea.InnerHtml = activity.content;

            var templates = DataBase.Template.Gets();
            templateSelect.Items.Clear();
            foreach(var template in templates)
            {
                templateSelect.Items.Add(new ListItem(template.name, template.id));
                if(template.id == activity.id)
                {
                    templateSelect.SelectedIndex = templateSelect.Items.Count - 1;
                }
            }
            templateAdditionInput.Value = activity.templateAddition;

            brief.Value = activity.brief;
            checkValid.Checked = activity.valid;
            if(activity.imgSrc != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "showImgSrc", "showImgSrc('//" + Request.Url.Host + "" + activity.imgSrc + "');", true);
            }
        }
    }
}