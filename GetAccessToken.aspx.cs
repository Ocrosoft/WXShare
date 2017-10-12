using Newtonsoft.Json.Linq;
using System;

namespace WXShare
{
    public partial class GetAccessToken : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            JObject jsonObject = new JObject();
            jsonObject["access_token"] = WXManage.GetAccessToken();
            Response.Write(jsonObject);
        }
    }
}