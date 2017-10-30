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
            // 各种金额
            var sum = order.smSum + order.mmSum + order.workSum;
            if (activity.template == 1)
            {
                if (sum >= int.Parse(activity.templateAddition.Split(',')[0]))
                {
                    sum -= int.Parse(activity.templateAddition.Split(',')[1]);
                }
            }
            cashRec.Value = sum.ToString("0.0");
            inputMMSumSubmitted.Value = order.mmSum.ToString("0.0");
            inputSMSumSubmitted.Value = order.smSum.ToString("0.0");
            inputWorkSumSubmitted.Value = order.workSum.ToString("0.0");

            if (order.status >= 9)
            {
                inputWorkDate.Value = order.workDate.ToString("yyyy-MM-dd");
                workDateDiv.Style["display"] = "";
            }
            if(order.status > 9)
            {
                inputWorkCompleteDate.Value = order.workCompleteDate.ToString("yyyy-MM-dd");
                workCompleteDateDiv.Style["display"] = "";
            }

            // 调整按钮
            if (order.status != 8)
            {
                statusBtn_8.InnerHtml = "";
                statusBtn_8.Style["display"] = "none";
            }
            if (order.status != 9)
            {
                statusBtn_9.InnerHtml = "";
                statusBtn_9.Style["display"] = "none";
            }
            // 停工继续按钮
            if(DataBase.Order.HasPaused(order))
            {
                inputStatus.Value = "临时停工";
                // 隐藏完工按钮
                statusBtn_9.InnerHtml = "";
                statusBtn_9.Style["display"] = "none";
                // 显示重新开工按钮
                statusBtn_8_5.Style["display"] = "";
            }
        }

        protected void startWork_Click(Object sender, EventArgs e)
        {
            if (!DataBase.Order.StartWork(new Objects.Order() { id = Request.QueryString["oid"] }))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('开工失败，系统错误');", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void stopWork_Click(Object sender,EventArgs e)
        {
            if(!DataBase.Order.PauseWork(new Objects.Order() { id = Request.QueryString["oid"]},DateTime.Now))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('临时停工失败，系统错误');", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void resumeWork_Click(Object sender,EventArgs e)
        {
            if(!DataBase.Order.ResumeWork(new Objects.Order() { id = Request.QueryString["oid"] }, DateTime.Now))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('继续施工失败，系统错误');", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void finishWork_Click(Object sender,EventArgs e)
        {
            var order = new Objects.Order()
            {
                id = Request.QueryString["oid"]
            };
            order = DataBase.Order.GetByID(order);
            order.workCompleteDate = DateTime.Now;
            order.timeLimit = (order.workCompleteDate - order.workDate).Days;
            if (!DataBase.Order.FinishWork(order))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('完工失败，系统错误');", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }
    }
}