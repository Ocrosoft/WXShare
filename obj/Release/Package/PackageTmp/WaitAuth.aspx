<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WaitAuth.aspx.cs" Inherits="WXShare.WaitAuth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>审核中</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page msg_success js_show">
            <div class="weui-msg">
                <div class="weui-msg__icon-area"><i class="weui-icon-success weui-icon_msg"></i></div>
                <div class="weui-msg__text-area">
                    <h2 class="weui-msg__title">已提交审核</h2>
                    <p class="weui-msg__desc">我们会尽快进行审核，审核结果将通过短信通知您</p>
                </div>
                <div class="weui-msg__opr-area">
                    <p class="weui-btn-area">
                        <a href="javascript:location.href='/UserLogin.aspx';" class="weui-btn weui-btn_primary">前往登录</a>
                        <!--<a href="javascript:history.back();" class="weui-btn weui-btn_default">辅助操作</a>-->
                    </p>
                </div>
                <div class="weui-msg__extra-area">
                    <div class="weui-footer">
                        <p class="weui-footer__links">
                            <a href="javascript:void(0);" class="weui-footer__link">hzlbsx</a>
                        </p>
                        <p class="weui-footer__text">Copyright © 2017 hzlbsx.com</p>
                    </div>
                </div>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

    </form>
</body>
</html>
