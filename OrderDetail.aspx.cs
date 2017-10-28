using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class OrderDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if (Session["phone"] == null || Session["iden"].ToString() != "5")
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }
            if (Request.QueryString["oid"] == null)
            {
                Response.Redirect("/OrderManage.aspx");
                return;
            }

            // 获取订单信息
            var oid = Request.QueryString["oid"];
            var order = DataBase.Order.GetByID(new Objects.Order()
            {
                id = oid
            });
            if (order.status > 0)
            {
                // 显示预约的内容
                inputBrushType.Value = new DataBase.BrushType().Get(new Objects.BrushType() { id = order.brushType.ToString() }).view;
                inputBrushDemand.Value = "";
                foreach (var dem in order.brushDemand.Split(','))
                {
                    inputBrushDemand.Value += new DataBase.BrushDemand().Get(new Objects.BrushDemand() { id = dem }).view + ",";
                }
                inputBrushDemand.Value = inputBrushDemand.Value.Substring(0, inputBrushDemand.Value.Length - 1);
            }
            if (order.status > 2)
            {
                // 显示基检内容
                inputHousePurpose.Value = new DataBase.HousePurpose().Get(new Objects.HousePurpose() { id = order.housePurpose.ToString() }).view;
                inputHouseType.Value = new DataBase.HouseType().Get(new Objects.HouseType() { id = order.houseType.ToString() }).view;
                inputHouseStructure.Value = new DataBase.HouseStructure().Get(new Objects.HouseStructure() { id = order.houseStructure.ToString() }).view;
                inputMianJiSubmitted.Value = order.mianJi.ToString();
                inputNeiQiangSubmitted.Value = order.neiQiang.ToString();
                inputYiShuQiSubmitted.Value = order.yiShuQi.ToString();
                inputWaiQiangSubmitted.Value = order.waiQiang.ToString();
                inputYangTaiSubmitted.Value = order.yangTai.ToString();
                inputMuQiSubmitted.Value = order.muQi.ToString();
                inputTieYiSubmitted.Value = order.tieYi.ToString();
            }
            if (order.status > 3)
            {
                inputWorkOrderDateSubmitted.Value = order.workOrderDate.ToString("yyyy-MM-dd");
                inputWorkCompleteOrderDateSubmitted.Value = order.workCompleteOrderDate.ToString("yyyy-MM-dd");
                inputContractNumberSubmitted.Value = order.contractNumber;
            }
        }
    }
}