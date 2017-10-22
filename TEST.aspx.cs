using System;
using System.Web.UI;

namespace WXShare
{
    public partial class TEST : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var obj = DataBase.User.Get(new Objects.User()
            {
                phone = "15869141870",
                identity = "1"
            });
        }
    }
}