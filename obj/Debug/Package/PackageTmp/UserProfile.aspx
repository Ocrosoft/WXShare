<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="WXShare.UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>个人信息</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">账号密码登录</h1>
        </div>
        <div class="weui-cells">
            <div class="weui-cell weui-cell_access">
                <div class="weui-cell__bd">姓名</div>
                <div class="weui-cell__ft" style="font-size: 0">
                    <span style="vertical-align:middle; font-size: 17px;" runat="server" id="name"></span>
                </div>
            </div>
            <div class="weui-cell weui-cell_access">
                <div class="weui-cell__bd">手机</div>
                <div class="weui-cell__ft" style="font-size: 0">
                    <span style="vertical-align:middle; font-size: 17px;" runat="server" id="phone"></span>
                </div>
            </div>
            <div class="weui-cell weui-cell_access">
                <div class="weui-cell__bd">金额</div>
                <div class="weui-cell__ft" style="font-size: 0">
                    <span style="vertical-align:middle; font-size: 17px;" runat="server" id="money"></span>
                    <span runat="server" id="moneyChange" class="weui-badge weui-badge_dot" style="margin-left: 5px;margin-right: 5px;display:none;"></span>
                </div>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

    </form>
</body>
</html>
