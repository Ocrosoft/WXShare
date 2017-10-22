﻿using System;
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
            inputIden.Value = rauth.identity;
            inputIDCard.Value = rauth.IDCard;
        }
        protected void AuthBtn_Click(object sender, EventArgs e)
        {
            string rid = Request.QueryString["rid"];
            if (DataBase.UserUnAuth.Auth(new Objects.User() { id = rid }))
            {
                Response.Clear();
                Response.Redirect("/UserRegisterAuth.aspx");
                return;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('出现错误');", true);
                return;
            }
        }
    }
}