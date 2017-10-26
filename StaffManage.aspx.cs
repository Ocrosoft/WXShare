using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class StaffManage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var list = DataBase.Team.UnRegisterToTeam();
            if(list.Count != 0)
            {
                newReg.Style["display"] = "block";
            }
        }
    }
}