﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="WXShare.OrderDetail" %>

<!DOCTYPE html>

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
        <!-- 施工队选择 -->
        <div class="js_dialog" id="SGDDialog" style="display: none">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">派工给施工队</strong></div>
                <div class="weui-dialog__bd">
                    <select id="SGDSelect" runat="server" class="weui-select" name="SGDSelect" style="width: auto; padding: 0px;">
                        <option value="0" selected>请选择施工队</option>
                    </select>
                </div>
                <div class="weui-dialog__ft">
                    <a id="button_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a runat="server" onserverclick="ButtonOK_Click" id="button_ok" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
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
            <!-- 业务员 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">业务员</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputYWY" class="weui-input" name="inputYWY" type="text" disabled />
                </div>
            </div>
            <!-- 施工队 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">施工队</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputSGD" class="weui-input" name="inputSGD" type="text" disabled />
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
            <!-- 实际收款 -->
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
            <!-- 各种原因 -->
            <div runat="server" id="reasonDiv" class="weui-cell" style="display: none">
                <div class="weui-cell__hd">
                    <label class="weui-label">取消原因</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputReason" disabled class="weui-input" type="text" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label id="showAll" class="weui-label" style="color: #1AAD19">显示全部</label>
                </div>
            </div>
            <!-- 附加内容 -->
            <div runat="server" id="additionContent" style="display: none;">
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
        </div>
        <!-- 派工 -->
        <div class="weui-cells" runat="server" id="statusBtn_8">
            <!-- 开始施工按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" id="startWork">派工给施工队</a>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script>
            $('#showAll').on('click', function () {
                if (this.innerText == '显示全部') {
                    $('#additionContent').show();
                    this.innerText = '收起详情';
                } else {
                    $('#additionContent').hide();
                    this.innerText = '显示全部';
                }
            });
            $('#startWork').on('click', function () {
                $('#SGDDialog').fadeIn(200);
            });
            $('#button_cancel').on('click', function () {
                $('#SGDDialog').fadeOut(200);
            });
        </script>
    </form>
</body>
</html>