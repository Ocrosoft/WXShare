using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class OrdersDetailForSGD : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if (Session["phone"] == null || Session["iden"].ToString() != "4")
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }
            if (Request.QueryString["oid"] == null)
            {
                Response.Clear();
                Response.Redirect("/OrdersForSGD.aspx");
                return;
            }

            // 获取订单信息
            var oid = Request.QueryString["oid"];
            var order = DataBase.Order.GetByID(new Objects.Order()
            {
                id = oid
            });
            inputName.Value = order.name;
            inputPhone.Value = order.phone;
            inputLocation.Value = order.location;
            inputLocationDetail.Value = order.locationDetail;
            inputStatus.Value = new DataBase.Status()
                .Get(
                new Objects.Status()
                {
                    id = order.status.ToString()
                }
                )
                .view;
            // 获取活动优惠
            var activity = DataBase.Activity.Get(new Objects.Activity() { id = order.youHuiLaiYuan.ToString() });
            if (activity.template == 1)
            {
                var addition = activity.templateAddition.Split(',');
                inputYouHui.Value = "满" + addition[0] + "减" + addition[1];
            }
            else if (activity.template == 2)
            {
                var addition = activity.templateAddition.Split(',');
                inputYouHui.Value = "满" + addition[0] + "赠送" + addition[1];
            }

            // 涂刷类型、涂刷需求
            inputBrushType.Value = new DataBase.BrushType().Get(new Objects.BrushType() { id = order.brushType.ToString() }).view;
            inputBrushDemand.Value = "";
            foreach (var dem in order.brushDemand.Split(','))
            {
                inputBrushDemand.Value += new DataBase.BrushDemand().Get(new Objects.BrushDemand() { id = dem }).view + ",";
            }
            inputBrushDemand.Value = inputBrushDemand.Value.Substring(0, inputBrushDemand.Value.Length - 1);
            // 房屋用途、房屋类型、房屋结构、面积等
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
            // 计划施工时间，计划完工时间，计划工期
            inputWorkOrderDateSubmitted.Value = order.workOrderDate.ToString("yyyy-MM-dd");
            inputWorkCompleteOrderDateSubmitted.Value = order.workCompleteOrderDate.ToString("yyyy-MM-dd");
            inputContractNumberSubmitted.Value = order.contractNumber;

            if (order.status == 8)
            {
                statusBtn_8.InnerHtml = "";
                statusBtn_8.Style["display"] = "none";
            }
            else if (order.status == 9)
            {
                statusBtn_9.InnerHtml = "";
                statusBtn_9.Style["display"] = "none";
            }
        }
    }
}