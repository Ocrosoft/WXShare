using System;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WXShare
{
    public partial class OrderDetailForYWY : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // 不是微信内置浏览器
            if (!WXManage.IsWXBrowser(Request))
            {
                Response.Redirect("/RequireWX.aspx?url=" + Request.Url);
                return;
            }
            if (Session["phone"] == null || Session["iden"].ToString() != "2")
            {
                Response.Redirect("/UserIndex.aspx");
                return;
            }
            if (Request.QueryString["oid"] == null)
            {
                Response.Clear();
                Response.Redirect("/OrdersForYWY.aspx");
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

            HideOthers(order.status);
            // 派单完成
            if (order.status == 0)
            {
                // 获取涂刷类型
                var brushType = new DataBase.BrushType().Gets();
                typeSelect.Items.Clear();
                typeSelect.Items.Add(new ListItem("选择涂刷类型", "0"));
                foreach (var type in brushType)
                {
                    if (type.id == "0")
                    {
                        continue;
                    }
                    typeSelect.Items.Add(new ListItem(type.view, type.id));
                }

                // 获取涂刷需求
                string demandHtml =
    "<label class=\"weui-cell weui-check__label\" for=\"#id#\">" +
    "	<div class=\"weui-cell__hd\">" +
    "		<input type=\"checkbox\" class=\"weui-check\" name=\"select_#id#\" id=\"#id#\">" +
    "		<i class=\"weui-icon-checked\"></i>" +
    "	</div>" +
    "	<div class=\"weui-cell__bd\">" +
    "		<p>#content#</p>" +
    "	</div>" +
    "</label>";
                var brushDemand = new DataBase.BrushDemand().Gets();
                demandList.InnerHtml = "";
                foreach (var demand in brushDemand)
                {
                    if (demand.id == "0")
                    {
                        continue;
                    }
                    demandList.InnerHtml += demandHtml
                        .Replace("#id#", demand.id)
                        .Replace("#content#", demand.view);
                }
            }
            // 订单取消
            else if (order.status == 1)
            {
                requiredHint.InnerHtml = "";
                requiredHint.Style["display"] = "none";
            }
            // 预约基检
            else if (order.status == 2)
            {
                // 获取房屋用途
                var housePurpose = new DataBase.HousePurpose().Gets();
                housePurposeSelect.Items.Clear();
                housePurposeSelect.Items.Add(new ListItem("选择房屋用途", "0"));
                foreach (var purpose in housePurpose)
                {
                    if (purpose.id == "0")
                    {
                        continue;
                    }
                    housePurposeSelect.Items.Add(new ListItem(purpose.view, purpose.id));
                }
                // 获取房屋类型
                var houseType = new DataBase.HouseType().Gets();
                houseTypeSelect.Items.Clear();
                houseTypeSelect.Items.Add(new ListItem("选择房屋类型", "0"));
                foreach (var type in houseType)
                {
                    if (type.id == "0")
                    {
                        continue;
                    }
                    houseTypeSelect.Items.Add(new ListItem(type.view, type.id));
                }
                // 获取房屋结构
                var houseStructure = new DataBase.HouseStructure().Gets();
                houseStructureSelect.Items.Clear();
                houseStructureSelect.Items.Add(new ListItem("选择房屋结构", "0"));
                foreach (var structure in houseStructure)
                {
                    if (structure.id == "0")
                    {
                        continue;
                    }
                    houseStructureSelect.Items.Add(new ListItem(structure.view, structure.id));
                }
                // 判断基检是否已经开始
                if (order.comCheckTime == DateTime.Parse("2017-01-01 00:00:00"))
                {
                    // 未开始
                    start.Style["display"] = "";
                    status_2.InnerHtml = "";
                    inputStatus.Value += " - " + order.comCheckOrderTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                else
                {
                    // 正在基检
                    finish.Style["display"] = "";
                    inputStatus.Value = "正在基检 - " + order.comCheckTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
            }
            // 基检完成
            else if (order.status == 3)
            {
                //
            }
            // 签约失败、签约成功、其他
            else
            {
                requiredHint.InnerHtml = "";
                requiredHint.Style["display"] = "none";
            }

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

        protected void saveBtn_Click(Object sender, EventArgs e)
        {
            var brushType = Request.Form["typeSelect"];
            if (brushType == "0")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('请选择涂刷类型')", true);
                return;
            }
            var brushDemand = "";
            foreach (var key in Request.Form.AllKeys)
            {
                if (Regex.IsMatch(key, "^select_\\d?$"))
                {
                    brushDemand += key.Split('_')[1] + ",";
                }
            }
            if (brushDemand == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('请选择涂刷需求')", true);
                return;
            }
            else
            {
                // 删除最后一个逗号
                brushDemand = brushDemand.Substring(0, brushDemand.Length - 1);
            }
            var comOrderTime = DateTime.Parse(Request.Form["comOrderTime"].Replace("T", " "));

            if (!DataBase.Order.CommissionerOrderToCustomer(
                new Objects.Order()
                {
                    id = Request.QueryString["oid"],
                    brushType = int.Parse(brushType),
                    brushDemand = brushDemand,
                    comOrderTime = comOrderTime,
                    comCheckOrderTime = comOrderTime
                }))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('保存失败，服务器错误')", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void cancelBtn_Click(Object sender, EventArgs e)
        {
            var oid = Request.QueryString["oid"];
            var cancelReason = Request.Form["cancelReason"];
            if (!DataBase.Order.CancelOrder(
                new Objects.Order()
                {
                    id = oid,
                    canceledReason = cancelReason
                }))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('取消订单失败，服务器错误')", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void startCheck_Click(Object sender, EventArgs e)
        {
            if (!DataBase.Order.CommissionerCheck(new Objects.Order()
            {
                id = Request.QueryString["oid"],
                comCheckTime = DateTime.Now
            }))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('无法开始基检，系统错误');", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }

        protected void finishCheck_Click(Object sender, EventArgs e)
        {
            if (!DataBase.Order.CommissionerCheckComplete(new Objects.Order()
            {
                id = Request.QueryString["oid"],
                housePurpose = int.Parse(Request.Form["housePurposeSelect"]),
                houseType = int.Parse(Request.Form["houseTypeSelect"]),
                houseStructure = int.Parse(Request.Form["houseStructureSelect"]),
                mianJi = int.Parse(Request.Form["inputMianji"]),
                neiQiang = int.Parse(Request.Form["inputNeiQiang"]),
                yiShuQi = int.Parse(Request.Form["inputYiShuQi"]),
                waiQiang = int.Parse(Request.Form["inputWaiQiang"]),
                yangTai = int.Parse(Request.Form["inputYangTai"]),
                muQi = int.Parse(Request.Form["inputMuQi"]),
                tieYi = int.Parse(Request.Form["inputTieYi"])
            }))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('无法完成基检，系统错误')", true);
                return;
            }
            Response.Redirect(Request.Url.ToString());
        }
        protected void orderFailed_Click(Object sender, EventArgs e)
        {

        }
        protected void signOrder_Click(Object sender, EventArgs e)
        {
            var workOrderDate = DateTime.Parse(Request.Form["workOrderDate"].Replace('T', ' '));
            var timeLimitOrder = int.Parse(Request.Form["timeLimitOrder"]);
            var workCompleteOrderDate = workOrderDate.AddDays(timeLimitOrder);
            var contractNumber = Request.Form["contractNumber"];

            if (!DataBase.Order.SignTheOrder(new Objects.Order()
            {
                id = Request.QueryString["oid"],
                workOrderDate = workOrderDate,
                timeLimitOrder = timeLimitOrder,
                workCompleteOrderDate = workCompleteOrderDate,
                contractNumber = contractNumber
            }))
            {
                ScriptManager.RegisterClientScriptBlock(this, GetType(), "error", "alert('无法完成签约，系统错误')", true);
                return;
            };
            Response.Redirect(Request.Url.ToString());
        }

        protected void HideOthers(int show)
        {
            if (show != 0)
            {
                status_0.InnerHtml = "";
                status_0.Style["display"] = "none";
                statusBtn_0.InnerHtml = "";
                statusBtn_0.Style["display"] = "none";
            }
            if (show != 2)
            {
                status_2.InnerHtml = "";
                status_2.Style["display"] = "none";
                statusBtn_2.InnerHtml = "";
                statusBtn_2.Style["display"] = "none";
            }
            if (show != 3)
            {
                status_3.InnerHtml = "";
                status_3.Style["display"] = "none";
                statusBtn_3.InnerHtml = "";
                statusBtn_3.Style["display"] = "none";
            }
        }
    }
}