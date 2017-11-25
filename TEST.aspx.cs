using System;
using System.Collections.Generic;
using System.Web.UI;

namespace WXShare
{
    public partial class TEST : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var open_id = Request.QueryString["oid"];
            var content = Request.QueryString["cont"];

            WXManage.SendMessage(open_id, content);
        }
    }
}