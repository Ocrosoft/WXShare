<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityAll.aspx.cs" Inherits="WXShare.ActivityAll" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>活动管理</title>
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
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">
                所有活动
                <a runat="server" onserverclick="newActivity_Click" id="newActivity" class="weui-btn weui-btn_mini weui-btn_primary" style="top: 5px;right: 0px;left: 5px;">添加</a>
            </h1>
        </div>
        <!-- 活动列表 -->
        <div class="weui-panel weui-panel_access">
            <div class="weui-panel__bd" runat="server" id="activities">
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script src="./js/zepto-touch.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
        <script type="text/javascript"></script>

    </form>
</body>
</html>
