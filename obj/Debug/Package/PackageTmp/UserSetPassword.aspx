<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserSetPassword.aspx.cs" Inherits="WXShare.UserSetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>设置密码</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">设置密码</h1>
        </div>
        <!-- 表单 -->
        <div class="weui-cells weui-cells_form">
            <!-- 密码 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">密码</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="tel" class="weui-input" type="password" name="password1" placeholder="请填写密码" required />
                </div>
            </div>
            <!-- 密码 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">重复密码</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="tel" class="weui-input" type="password" name="password2" placeholder="请重复密码" required />
                </div>
            </div>
            <!-- 确定设置按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" onclick="$('#form1').submit();" id="showTooltips">设置密码</a>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
    </form>
</body>
</html>
