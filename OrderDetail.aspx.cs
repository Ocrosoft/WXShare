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
            // 显示业务员
            var ywy = DataBase.User.Get(new Objects.User() { phone = order.commissioner, identity = "2" });
            inputYWY.Value = ywy.name + " " + ywy.phone;
            if (order.status > 1) // 预约基检+
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
            if (order.status > 2) // 基检完成+
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
            if (order.status > 3 && order.status != 5)  // 签约成功+（签约失败没有计划）
            {
                // 显示计划施工
                inputWorkOrderDateSubmitted.Value = order.workOrderDate.ToString("yyyy-MM-dd");
                inputWorkCompleteOrderDateSubmitted.Value = order.workCompleteOrderDate.ToString("yyyy-MM-dd");
                inputContractNumberSubmitted.Value = order.contractNumber;
            }
            if(order.status > 5) // 收款完成+
            {
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
            }
            if(order.status > 7) // 派工完成+
            {
                // 显示施工队
                var team = DataBase.Team.Get(new Objects.Team() { id = order.constructionTeam });
                inputSGD.Value = team.teamName;
            }
            // 订单取消原因、签约不成功原因、退单原因
            if(order.status == 1)
            {
                reasonDiv.Style["display"] = "";
                inputReason.Value = order.canceledReason;
            }
            else if(order.status == 5)
            {
                reasonDiv.Style["display"] = "";
                inputReason.Value = order.signFailedReason;
            }
            else if(order.status == 7)
            {
                reasonDiv.Style["display"] = "";
                inputReason.Value = order.refuseReason;
            }
            // 调整按钮
            if(order.status != 6) // 收款完成
            {
                statusBtn_8.InnerHtml = "";
                statusBtn_8.Style["display"] = "none";
            }

            // 获取施工队
            var teams = DataBase.Team.Gets();
            SGDSelect.Items.Clear();
            SGDSelect.Items.Add(new ListItem("请选择施工队", "0"));
            foreach(var team in teams)
            {
                SGDSelect.Items.Add(new ListItem(team.teamName, team.id));
            }
        }

        protected void ButtonOK_Click(Object sender,EventArgs e)
        {
            var order = new Objects.Order()
            {
                id = Request.QueryString["oid"]
            };
            order = DataBase.Order.GetByID(order);
            order.constructionTeam = Request.Form["SGDSelect"];
            // 派工
            if(!DataBase.Order.Dispatch(order))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('派工失败，服务器错误');", true);
                return;
            }
            else
            {
                var team = DataBase.Team.GetWithMembers(new Objects.Team() { id = order.constructionTeam });
                foreach(var member in team.members)
                {
                    var openID = DataBase.User.GetOpenID(member);
                    if(openID != "")
                    {
                        WXManage.SendMessage(openID, "有一个新的施工订单");
                    }
                }
                // 给施工队所有成员发送消息
            }
            Response.Redirect(Request.Url.ToString());
        }
    }
}