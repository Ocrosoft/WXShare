<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityAuthSuccess.aspx.cs" Inherits="WXShare.ActivityAuthSuccess" %>

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
                    <h2 class="weui-msg__title">报名成功</h2>
                    <p class="weui-msg__desc">请保持手机畅通，我们的工作人员将会尽快与您联系</p>
                </div>
                <div id="qrcode" class="weui-msg__opr-area">
                    <img src="./images/qrcode.png" style="height: 100%;">
                </div>
                <div class="weui-msg__text-area">
                    <p class="weui-msg__desc">关注公众号，查看更多活动<br/>
                        分享活动可获得返利</p>
                </div>
                <div class="weui-msg__extra-area">
                    <div class="weui-footer">
                        <p class="weui-footer__links">
                            <a href="javascript:void(0);" class="weui-footer__link">hzlbsx</a>
                        </p>
                        <p class="weui-footer__text">Copyright © 2017</p>
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
