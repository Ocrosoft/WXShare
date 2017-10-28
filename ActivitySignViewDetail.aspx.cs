using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class ActivitySignViewDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["phone"] == null || Session["iden"].ToString() != "5")
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }
            if(Request.QueryString["sid"] == null)
            {
                Response.Redirect("/ActivitySignView.aspx");
                return;
            }

            var sid = Request.QueryString["sid"];
            var asign = DataBase.ActivitySign.Get(new Objects.ActivitySign() { id = sid });
            // 显示报名详情
            inputName.Value = asign.name;
            inputPhone.Value = asign.phone;
            inputLocation.Value = asign.location + " " + asign.locationDetail;
            inputActivity.Value = DataBase.Activity.Get(new Objects.Activity() { id = asign.activityID }).title;
            inputShare.Value = asign.shareSource;
            inputTime.Value = asign.signDate.ToString("yyyy-MM-ddTHH:mm:ss");

            // 获取所有业务员
            var ywys = DataBase.User.Gets("2");
            YWYSelect.Items.Clear();
            YWYSelect.Items.Add(new ListItem("请选择业务员", "0"));
            foreach(var ywy in ywys)
            {
                YWYSelect.Items.Add(new ListItem(ywy.name,ywy.id));
            }
        }

        protected void DeleteBtn_Click(Object sender, EventArgs e)
        {
            var aso = new Objects.ActivitySign()
            {
                id = Request.QueryString["sid"]
            };
            if(!DataBase.ActivitySign.Delete(aso))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('删除失败，服务器错误');", true);
                return;
            }
            Response.Redirect("/ActivitySignView.aspx");
        }

        protected void ButtonOK_Click(Object sender, EventArgs e)
        {
            var aso = new Objects.ActivitySign()
            {
                id = Request.QueryString["sid"]
            };
            aso = DataBase.ActivitySign.Get(aso);

            var order = new Objects.Order()
            {
                name = aso.name,
                phone = aso.phone,
                createTime = aso.signDate,
                location = aso.location,
                locationDetail = aso.locationDetail,
                youHuiLaiYuan = int.Parse(aso.activityID)
            };
            // 将报名转为订单
            if(!DataBase.Order.Add(order)) // 失败提示，成功不处理
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('派单失败，服务器错误');", true);
                return;
            }
            order = DataBase.Order.Get(order);
            // 删除活动报名
            if (!DataBase.ActivitySign.Delete(aso)) // 失败提示并删除订单
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('派单失败，服务器错误');", true);
                DataBase.Order.Delete(order);
                return;
            }
            order.commissioner = DataBase.User.GetByID(new Objects.User() { id = Request.Form["YWYSelect"] }).phone;
            // 将订单转给业务员
            if (!DataBase.Order.ToCommissioner(order)) // 失败提示并删除订单，成功给业务员发消息
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('派单失败，服务器错误');", true);
                DataBase.Order.Delete(order);
                return;
            }
            else
            {
                //WXManage.SendMessage("", "");
            }
            Response.Redirect("/ActivitySignView.aspx");
        }
    }
}