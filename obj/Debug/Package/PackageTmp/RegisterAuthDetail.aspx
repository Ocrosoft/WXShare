<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterAuthDetail.aspx.cs" Inherits="WXShare.RegisterAuthDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>注册详情</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="weui-btn-area">
            <div class="weui-cell__hd">
                <h2>注册详情</h2>
            </div>
        </div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">姓名</label>
                </div>
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
                    <label class="weui-label">身份</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputIden" class="weui-input" type="text" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">身份证</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputIDCard" class="weui-input" type="text" disabled>
                </div>
            </div>
            <!-- 审核按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" runat="server" onserverclick="AuthBtn_Click" id="showTooltips">确认审核</a>
            </div>
            <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                <span class="weui-agree__text">
                    <a runat="server" onserverclick="delete_Click" id="delete">忽略并删除</a>
                </span>
            </label>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
    </form>
</body>
</html>
