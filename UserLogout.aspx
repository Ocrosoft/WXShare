<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogout.aspx.cs" Inherits="WXShare.UserLogout" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>登出</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 弹窗 -->
        <div class="js_dialog" id="iosDialog1">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">确认登出</strong></div>
                <div class="weui-dialog__bd">
                    退出后15天自动登录将失效<br />
                    确认退出该账号？
                </div>
                <div class="weui-dialog__ft">
                    <a href="javascript:history.go(-1);" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a href="javascript:;" class="weui-dialog__btn weui-dialog__btn_primary"
                        runat="server" id="logout" onserverclick="Logout">确定</a>
                </div>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

    </form>
</body>
</html>
