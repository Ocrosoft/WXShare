<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetailForYWY.aspx.cs" Inherits="WXShare.OrderDetailForYWY" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>订单详情</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 保存提示 -->
        <div class="js_dialog" id="saveDialog" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">信息确认</strong></div>
                <div class="weui-dialog__bd">
                    提交后不可修改<br />
                    确认信息正确？
                </div>
                <div class="weui-dialog__ft">
                    <a id="dialog_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="dialog_ok" runat="server" onserverclick="saveBtn_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 取消提示 -->
        <div class="js_dialog" id="cancelDialog" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">取消订单确认</strong></div>
                <div class="weui-dialog__bd">
                    该操作不可取消<br />
                    确认请在下方输入取消原因<br />
                    <input style="color: #000; text-align: center; margin-top: 10px; border-bottom: 1px solid;" class="weui-input" id="cancelReason" name="cancelReason" disabled />
                </div>
                <div class="weui-dialog__ft">
                    <a id="dialogCancel_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="dialogCancel_ok" runat="server" onserverclick="cancelBtn_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 签约失败取消提示 -->
        <div class="js_dialog" id="orderFailedDialog" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">取消订单确认</strong></div>
                <div class="weui-dialog__bd">
                    该操作不可取消<br />
                    确认请在下方输入签约不成功原因<br />
                    <input style="color: #000; text-align: center; margin-top: 10px; border-bottom: 1px solid;" class="weui-input" id="orderFailedReason" name="orderFailedReason" disabled />
                </div>
                <div class="weui-dialog__ft">
                    <a id="orderFailed_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="orderFailed_ok" runat="server" onserverclick="orderFailed_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 基检完成提示 -->
        <div class="js_dialog" id="checkFinishDialog" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">基检完成确认</strong></div>
                <div class="weui-dialog__bd">
                    提交后不可修改<br />
                    确认信息正确？<br />
                </div>
                <div class="weui-dialog__ft">
                    <a id="checkFinish_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="checkFinish_ok" runat="server" onserverclick="finishCheck_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 收款完成提示 -->
        <div class="js_dialog" id="cashDialog" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">收款完成确认</strong></div>
                <div class="weui-dialog__bd">
                    提交后不可修改<br />
                    确认信息正确？<br />
                </div>
                <div class="weui-dialog__ft">
                    <a id="cash_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="cash_ok" runat="server" onserverclick="cash_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 退单提示 -->
        <div class="js_dialog" id="refuseDialog" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">退单确认</strong></div>
                <div class="weui-dialog__bd">
                    该操作不可取消<br />
                    确认请在下方输入退单原因<br />
                    <input style="color: #000; text-align: center; margin-top: 10px; border-bottom: 1px solid;" class="weui-input" id="refuseReason" name="refuseReason" disabled />
                </div>
                <div class="weui-dialog__ft">
                    <a id="refuse_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="refuce_ok" runat="server" onserverclick="refuse_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">订单详情</h1>
        </div>
        <!-- 表单 -->
        <div class="weui-cells">
            <!-- 客户姓名 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">姓名</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputName" disabled class="weui-input" type="text" />
                </div>
            </div>
            <!-- 客户手机 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputPhone" disabled class="weui-input" type="tel" />
                </div>
            </div>
            <!-- 区县 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">区县</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputLocation" disabled class="weui-input" type="text" />
                </div>
            </div>
            <!-- 详细地址 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">详细地址</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputLocationDetail" disabled class="weui-input" type="text" />
                </div>
            </div>
            <!-- 订单优惠 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">优惠</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputYouHui" class="weui-input" name="inputYouHui" type="text" disabled />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">实际收款</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="cashRec" class="weui-input" name="cashRec" type="number" disabled />
                </div>
            </div>
            <!-- 订单状态 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">订单状态</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputStatus" disabled class="weui-input" type="text" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label id="showAll" class="weui-label" style="color: #1AAD19">显示全部</label>
                </div>
            </div>
            <!-- 附加内容 -->
            <div runat="server" id="additionContent" style="display: none;">
                <!-- 涂刷类型 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">涂刷类型</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input id="inputBrushType" runat="server" class="weui-input" disabled />
                    </div>
                </div>
                <!-- 涂刷需求 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">涂刷需求</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input id="inputBrushDemand" runat="server" class="weui-input" disabled />
                    </div>
                </div>
                <!-- 房屋用途 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">房屋用途</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputHousePurpose" class="weui-input" disabled />
                    </div>
                </div>
                <!-- 房屋类型 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">房屋类型</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputHouseType" class="weui-input" disabled />
                    </div>
                </div>
                <!-- 房屋结构 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">房屋结构</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputHouseStructure" class="weui-input" disabled />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">建筑面积</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputMianJiSubmitted" class="weui-input" disabled />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">内墙</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputNeiQiangSubmitted" class="weui-input" disabled />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">艺术漆</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputYiShuQiSubmitted" class="weui-input" disabled />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">外墙</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputWaiQiangSubmitted" class="weui-input" disabled />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">阳台</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputYangTaiSubmitted" class="weui-input" disabled />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">木器</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputMuQiSubmitted" class="weui-input" disabled />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">铁艺</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputTieYiSubmitted" class="weui-input" disabled />
                    </div>
                </div>
                <!-- 计划开工日期 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">计划开工日期</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputWorkOrderDateSubmitted" class="weui-input" type="date" disabled />
                    </div>
                </div>
                <!-- 计划完工日期 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">计划完工日期</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" id="inputWorkCompleteOrderDateSubmitted" class="weui-input" type="date" disabled />
                    </div>
                </div>
                <!-- 合同号 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">合同号</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" type="text" class="weui-input" id="inputContractNumberSubmitted" disabled />
                    </div>
                </div>
                <!-- 主材金额 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">主材金额</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" type="number" class="weui-input" id="inputMMSumSubmitted" disabled />
                    </div>
                </div>
                <!-- 辅材金额 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">辅材金额</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" type="number" class="weui-input" id="inputSMSumSubmitted" disabled />
                    </div>
                </div>
                <!-- 施工金额 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">施工金额</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input runat="server" type="number" class="weui-input" id="inputWorkSumSubmitted" disabled />
                    </div>
                </div>
            </div>
            <!-- 可修改部分 -->
            <div class="weui-cell" runat="server" id="requiredHint">
                <p style="text-align: center; width: 100%; color: #1AAD19;">需填写以下内容</p>
            </div>
            <!-- 预约基检 -->
            <div runat="server" id="status_0">
                <!-- 涂刷类型 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">涂刷类型</label>
                    </div>
                    <div class="weui-cell__bd">
                        <select id="typeSelect" runat="server" class="weui-select" name="typeSelect">
                        </select>
                    </div>
                </div>
                <!-- 涂刷需求 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">涂刷需求</label>
                    </div>
                    <div class="weui-cell__bd">
                        <!-- 多选列表 -->
                        <div class="weui-cells weui-cells_checkbox" runat="server" id="demandList" style="margin-top: 0px;">
                        </div>
                    </div>
                </div>
                <!-- 预约（基检）时间 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label for="" class="weui-label">预约时间</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input id="comOrderTime" class="weui-input" type="datetime-local" value="" placeholder="" name="comOrderTime" required>
                    </div>
                </div>
            </div>
            <!-- 开始基检 -->
            <div runat="server" id="status_2">
                <!-- 房屋用途 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">房屋用途</label>
                    </div>
                    <div class="weui-cell__bd">
                        <select id="housePurposeSelect" runat="server" class="weui-select" name="housePurposeSelect">
                        </select>
                    </div>
                </div>
                <!-- 房屋类型 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">房屋类型</label>
                    </div>
                    <div class="weui-cell__bd">
                        <select id="houseTypeSelect" runat="server" class="weui-select" name="houseTypeSelect">
                        </select>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">房屋结构</label>
                    </div>
                    <div class="weui-cell__bd">
                        <select id="houseStructureSelect" runat="server" class="weui-select" name="houseStructureSelect">
                        </select>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">建筑面积</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="0" id="inputMianJi" class="weui-input" name="inputMianJi" type="number" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">内墙</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="0" id="inputNeiQiang" class="weui-input" name="inputNeiQiang" type="number" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">艺术漆</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="0" id="inputYiShuQi" class="weui-input" name="inputYiShuQi" type="number" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">外墙</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="0" id="inputWaiQiang" class="weui-input" name="inputWaiQiang" type="number" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">阳台</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="0" id="inputYangTai" class="weui-input" name="inputYangTai" type="number" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">木器</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="0" id="inputMuQi" class="weui-input" name="inputMuQi" type="number" />
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">铁艺</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="0" id="inputTieYi" class="weui-input" name="inputTieYi" type="number" />
                    </div>
                </div>
            </div>
            <!-- 基检完成 -->
            <div runat="server" id="status_3">
                <!-- 计划开工日期 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">计划开工日期</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input id="workOrderDate" class="weui-input" type="date" value="" name="workOrderDate" required>
                    </div>
                </div>
                <!-- 计划工期 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">计划工期</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="0" type="text" class="weui-input" id="timeLimitOrder" name="timeLimitOrder" required />
                    </div>
                </div>
                <!-- 计划完工日期 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">计划完工日期</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input disabled id="workCompleteOrderDate" class="weui-input" type="date" value="" name="workCompleteOrderDate" required>
                    </div>
                </div>
                <!-- 合同号 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">合同号</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input placeholder="CXXXXXXXXXXXXXXXX" type="text" class="weui-input" id="contractNumber" name="contractNumber" required />
                    </div>
                </div>
            </div>
            <div runat="server" id="status_4">
                <!-- 收款完成 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">主材金额</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input id="inputMMSum" placeholder="0" class="weui-input" type="number" value="" name="inputMMSum" required>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">辅材金额</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input id="inputSMSum" placeholder="0" class="weui-input" type="number" value="" name="inputSMSum" required>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">施工金额</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input id="inputWorkSum" placeholder="0" class="weui-input" type="number" value="" name="inputWorkSum" required>
                    </div>
                </div>
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">优惠后金额</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input id="inputSum" placeholder="先填写所有金额" class="weui-input" type="number" name="inputSum" disabled>
                    </div>
                </div>
            </div>
        </div>
        <div class="weui-cells" runat="server" id="statusBtn_0">
            <!-- 确认按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" id="save">确认预约完成</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a id="cancelOrder">取消订单</a>
                    </span>
                </label>
            </div>
        </div>
        <div class="weui-cells" runat="server" id="statusBtn_2">
            <!-- 开始基检按钮 -->
            <div class="weui-btn-area">
                <a onserverclick="startCheck_Click" runat="server" class="weui-btn weui-btn_primary" id="start" style="display: none;">开始基检</a>
            </div>
            <!-- 基检完成按钮 -->
            <div class="weui-btn-area">
                <a runat="server" class="weui-btn weui-btn_primary" id="finish" style="display: none;">基检完成</a>
            </div>
        </div>
        <div class="weui-cells" runat="server" id="statusBtn_3">
            <!-- 签约按钮 -->
            <div class="weui-btn-area">
                <a onserverclick="signOrder_Click" runat="server" class="weui-btn weui-btn_primary" id="signOrder">签约完成</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a id="orderFailed">无法签约，取消订单</a>
                    </span>
                </label>
            </div>
        </div>
        <div class="weui-cells" runat="server" id="statusBtn_4">
            <!-- 收款按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" id="cash">收款完成</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a id="refuse">申请退单</a>
                    </span>
                </label>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
        <script>
            /**
             * 获取当前时间yyyy-MM-dd HH:mm:ss
             */
            function getNowFormatDate(date) {
                if (!date) {
                    date = new Date();
                }
                var ret = date.getFullYear() + '-';
                if (date.getMonth() < 9) {
                    ret += '0';
                }
                ret += (date.getMonth() + 1) + '-';
                if (date.getDate() < 10) {
                    ret += '0';
                }
                ret += date.getDate() + ' ';
                if (date.getHours() < 10) {
                    ret += '0';
                }
                ret += date.getHours() + ':';
                if (date.getMinutes() < 10) {
                    ret += '0';
                }
                ret += date.getMinutes() + ':';
                if (date.getSeconds() < 10) {
                    ret += '0';
                }
                ret += date.getSeconds();

                return ret;
            }
            /* 时间预设为当前时间（DOM可能不存在） */
            try {
                $('#comOrderTime')[0].value = getNowFormatDate().replace(' ', 'T');
            } catch(e) {
            }
            try {
                $('#workOrderDate')[0].value = getNowFormatDate().split(' ')[0];
            } catch (e) {
            }
            /* 确认按钮 */
            $('#save').on('click', function () {
                $('#saveDialog').fadeIn(200);
            });
            /* 确认提示框取消按钮 */
            $('#dialog_cancel').on('click', function () {
                $('#saveDialog').fadeOut(200);
            });
            /* 取消订单按钮 */
            $('#cancelOrder').on('click', function () {
                $('#cancelReason').removeAttr("disabled").attr('required', 'required');
                $('#cancelReason')[0].value = '咨询订单';
                $('#cancelDialog').fadeIn(200);
            });
            /* 取消订单提示框取消按钮 */
            $('#dialogCancel_cancel').on('click', function () {
                $('#cancelReason').removeAttr('required').attr('disabled', 'disabled');
                $('#cancelDialog').fadeOut(200);
            });
            var option = $('#houseTypeSelect').find('option');
            /* 房屋用途选项改变，调整房屋类型选项 */
            $('#housePurposeSelect').on('change', function () {
                $('#houseTypeSelect').empty();
                _self = $(this);
                _val = _self.val();
                var _selected;
                _self.find('option').forEach(function (item) {
                    if (item.value == _val) {
                        _selected = item.text.substring(0, 2);
                        return;
                    }
                });
                option.forEach(function (item) {
                    if (item.innerText.indexOf(_selected) != -1) {
                        $('#houseTypeSelect').append(item);
                    }
                });
            });
            /* 签约不成功，取消订单按钮 */
            $('#orderFailed').on('click', function () {
                $('#orderFailedReason').removeAttr("disabled").attr('required', 'required');
                $('#orderFailedReason')[0].value = '咨询订单';
                $('#orderFailedDialog').fadeIn(200);
            });
             /* 签约不成功，取消订单取消按钮 */
            $('#orderFailed_cancel').on('click', function () {
                $('#orderFailedReason').removeAttr("required").attr('disabled', 'disabled');
                $('#orderFailedDialog').fadeOut(200);
            });
            /*  */
            $('#status_2').find('input').css('margin-left', '15px');
            /* 显示基检完成确认提示 */
            $('#finish').on('click', function () {
                $('#checkFinishDialog').fadeIn(200);
                $('#status_2').find('[type=number]').forEach(function (item) {
                    if(!$(item).val()){
                        $(item).val(0);
                    }
                });
            });
            $('#checkFinish_cancel').on('click', function () {
                $('#checkFinishDialog').fadeOut(200);
            });
            $('#timeLimitOrder').on('keyup', function () {
                try {
                    var st = $('#workOrderDate').val().replace('T', ' ');
                    var add = $('#timeLimitOrder').val();

                    var date = new Date(st);
                    date = date.valueOf();
                    date += parseInt(add) * 24 * 60 * 60 * 1000;
                    date = new Date(date);
                    $('#workCompleteOrderDate')[0].value = getNowFormatDate(date).split(' ')[0];
                } catch (e) {
                    // 忽略所有错误
                }
            });
            $('#showAll').on('click', function () {
                if (this.innerText == '显示全部') {
                    $('#additionContent').show();
                    this.innerText = '收起详情';
                } else {
                    $('#additionContent').hide();
                    this.innerText = '显示全部';
                }
            });
            $('#cash').on('click', function () {
                if ($('#inputMMSum').val() == "" ||
                    $('#inputSMSum').val() == "" ||
                    $('#inputWorkSum').val() == "") {
                    alert('请填写所有金额后再确认收款完成！');
                    return;
                }
                $('#cashDialog').fadeIn(200);
            });
            /* 退单 */
            $('#refuse').on('click', function () {
                $('#refuseReason').removeAttr("disabled").attr('required', 'required');
                $('#refuseReason')[0].value = '咨询订单';
                $('#refuseDialog').fadeIn(200);
            });
            $('#cash_cancel').on('click', function () {
                $('#cashDialog').fadeOut(200);
            });
            $('#refuse_cancel').on('click', function () {
                $('#refuseDialog').fadeOut(200);
            });
            function calc() {
                var mmSum = parseFloat($('#inputMMSum').val());
                var smSum = parseFloat($('#inputSMSum').val());
                var workSum = parseFloat($('#inputWorkSum').val());

                if (mmSum != "" && smSum != "" && workSum != "") {
                    var sum = mmSum + smSum + workSum;
                    var template = $('#inputYouHui').attr('data-t');
                    var addition = $('#inputYouHui').attr('data-a');
                    if (template == "1") {
                        var aa = addition.split(',')[0];
                        var ab = addition.split(',')[1];
                        if (sum >= aa) {
                            sum -= ab;
                        }
                    }
                    $('#inputSum').val(sum.toFixed(1));
                }
            }
            $('#inputMMSum').on('keyup', calc);
            $('#inputSMSum').on('keyup', calc);
            $('#inputWorkSum').on('keyup', calc);
        </script>
    </form>
</body>
</html>
