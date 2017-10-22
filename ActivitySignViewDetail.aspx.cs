using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class ActivitySignViewDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["sid"] == "")
            {
                Response.Clear();
                Response.Redirect("/ActivitySignView.aspx");
                return;
            }

            var sid = Request.QueryString["sid"];
            var asign = DataBase.ActivitySign.Get(new Objects.ActivitySign() { id = sid });

            inputName.Value = asign.name;
            inputPhone.Value = asign.phone;
            inputLocation.Value = asign.location + " " + asign.locationDetail;
            inputActivity.Value = DataBase.Activity.Get(new Objects.Activity() { id = asign.activityID }).title;
            inputShare.Value = asign.shareSource;
            inputTime.Value = asign.signDate.ToString("yyyy-MM-ddTHH:mm:ss");
        }
    }
}