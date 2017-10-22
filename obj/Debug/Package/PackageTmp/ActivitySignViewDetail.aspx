<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivitySignViewDetail.aspx.cs" Inherits="WXShare.ActivitySignViewDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>报名详情</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="weui-btn-area">
            <div class="weui-cell__hd">
                <h2>报名详情</h2>                
            </div>
        </div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">姓名</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputName" disabled class="weui-input" type="text">
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机号</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputPhone" class="weui-input" type="tel" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">地址</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputLocation" class="weui-input" type="text" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">报名活动</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputActivity" class="weui-input" type="text" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">推荐人</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputShare" class="weui-input" type="text" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"><label for="" class="weui-label">报名时间</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputTime" class="weui-input" type="datetime-local" disabled >
                </div>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
    </form>
</body>
</html>
