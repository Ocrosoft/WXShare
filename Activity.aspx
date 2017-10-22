<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Activity.aspx.cs" Inherits="WXShare.Activity" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>当前活动</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <style>
        .weui-cells:before {
            border: 0px;
        }

        .weui-cells:after {
            border: 0px;
        }

        .con {
            width: 100%;
            height: 300px;
            border: 1px solid #ccc;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="weui-panel weui-panel_access">
            <div class="weui-panel__hd">当前活动</div>
            <div class="weui-panel__bd" runat="server" id="activities">
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script src="./js/zepto-touch.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
        <script type="text/javascript"> </script>

    </form>
</body>
</html>
