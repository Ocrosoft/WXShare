<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffManage.aspx.cs" Inherits="WXShare.StaffManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>个人信息</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <style>
        a {
            color: #000;
        }
        .weui-badge_dot{
            padding-left: .4em;
            padding-right:.4em;
            padding-top:.42em;
            padding-bottom:.415em;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">员工管理</h1>
        </div>
        <div class="weui-cells">
            <div class="weui-cell weui-cell_access" onclick="location.href='/StaffManageYWY.aspx';">
                <div class="weui-cell__bd">业务员</div>
                <div class="weui-cell__ft" style="font-size: 0">
                    <span style="vertical-align: middle; font-size: 17px;" runat="server" id="ywy"></span>
                </div>
            </div>
            <div class="weui-cell weui-cell_access" onclick="location.href='/StaffManageSGRY.aspx';">
                <div class="weui-cell__bd">施工人员</div>
                <div class="weui-cell__ft" style="font-size: 0">
                    <span style="vertical-align: middle; font-size: 17px;" runat="server" id="sgry"></span>
                    <span runat="server" id="newReg" class="weui-badge weui-badge_dot"
                        style="margin-left: 5px; margin-right: 5px; display: none;"></span>
                </div>
            </div>
            <div class="weui-cell weui-cell_access" onclick="location.href='/StaffManageSGDW.aspx';">
                <div class="weui-cell__bd">施工队伍</div>
                <div class="weui-cell__ft" style="font-size: 0">
                    <span style="vertical-align: middle; font-size: 17px;" runat="server" id="sgdw"></span>
                </div>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
    </form>
</body>
</html>
