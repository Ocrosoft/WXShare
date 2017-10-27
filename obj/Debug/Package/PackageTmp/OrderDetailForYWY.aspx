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
        <!-- 弹出悬浮提示 -->
        <div class="js_dialog" id="iosDialog1" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">信息确认</strong></div>
                <div class="weui-dialog__bd">确认信息正确？<br />
                    除预约时间外其他信息可更改</div>
                <div class="weui-dialog__ft">
                    <a id="dialog_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="dialog_ok" runat="server" onserverclick="saveBtn_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">订单详情</h1>
        </div>
        <div class="weui-cells">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">姓名</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputName" disabled class="weui-input" type="text" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputPhone" disabled class="weui-input" type="tel" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">区县</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputLocation" disabled class="weui-input" type="text" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">详细地址</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputLocationDetail" disabled class="weui-input" type="text" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">订单状态</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputStatus" disabled class="weui-input" type="text" />
                </div>
            </div>
            <div class="weui-cell">
                <p style="text-align: center; width: 100%; color: #1AAD19;">需填写以下内容</p>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">涂刷类型</label></div>
                <div class="weui-cell__bd">
                    <select id="typeSelect" runat="server" class="weui-select" name="typeSelect" style="width: auto; padding: 0px;">
                    </select>
                </div>
            </div>
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
                    <label for="" class="weui-label">预约时间</label></div>
                <div class="weui-cell__bd">
                    <input id="comOrderTime" class="weui-input" type="datetime-local" value="" placeholder="" name="comOrderTime" required>
                </div>
            </div>
        </div>
        <div class="weui-cells">
            <!-- 修改按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" id="save">确认预约完成</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a id="delete">取消订单</a>
                    </span>
                </label>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
        <script>
            function getNowFormatDate() {
                var date = new Date();
                var seperator1 = "-";
                var seperator2 = ":";
                var month = date.getMonth() + 1;
                var strDate = date.getDate();
                if (month >= 1 && month <= 9) {
                    month = "0" + month;
                }
                if (strDate >= 0 && strDate <= 9) {
                    strDate = "0" + strDate;
                }
                var currentdate = date.getFullYear() + seperator1 + month + seperator1 + strDate
                    + " " + date.getHours() + seperator2 + date.getMinutes()
                    + seperator2 + date.getSeconds();
                return currentdate;
            }
            $('#comOrderTime')[0].value = getNowFormatDate().replace(' ', 'T');
            $('#save').on('click', function () {
                $('#iosDialog1').fadeIn(200);
            });
            $('#dialog_cancel').on('click', function () {
                $('#iosDialog1').fadeOut(200);
            });
        </script>
    </form>
</body>
</html>
