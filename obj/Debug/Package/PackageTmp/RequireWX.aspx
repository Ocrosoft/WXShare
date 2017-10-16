<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RequireWX.aspx.cs" Inherits="WXShare.RequireWX" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>使用微信扫描二维码</title>
    <link rel="stylesheet" type="text/css" href="./css/requireWX.css" />
</head>
<body>
    <div class="hs-main">
        <div class="hs-content">
            <img runat="server" id="qrcode" class="hs-404-icon" src="">
            <div class="hs-error-msg">请使用微信扫描此二维码查看</div>
        </div>
    </div>
    <div class="hs-foot">
        <div>Copyright © 2017</div>
    </div>
</body>
</html>
