﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class StaffManageSGDW : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string html =
"<div class=\"weui-cell weui-cell_access\" onclick=\"location.href='/StaffSGDWDetail.aspx?tid=#id#';\">" +
"	<div class=\"weui-cell__bd\">#name#</div>" +
"	<div class=\"weui-cell__ft\" style=\"font-size: 0\">" +
"		<span style=\"vertical-align: middle; font-size: 17px;\">#memberCount#人</span>" +
"	</div>" +
"</div>";

            var array = DataBase.Team.GetsWithMembers();
            list.InnerHtml = "";
            foreach (var item in array)
            {
                list.InnerHtml += html
                    .Replace("#id#", item.Key)
                    .Replace("#name#", item.Value.teamName)
                    .Replace("#memberCount#", item.Value.members.Count.ToString());
            }
        }
    }
}