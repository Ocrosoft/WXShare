<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffManageSGDW.aspx.cs" Inherits="WXShare.StaffManageSGDW" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>施工队伍</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 新建施工队 -->
        <div class="js_dialog" id="addTeamDialog" style="display: none">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">添加施工队</strong></div>
                <div class="weui-dialog__bd">
                    <input id="inputAddTeam" class="weui-input" name="inputAddTeam" placeholder="输入施工队名称" style="text-align:center;" />
                </div>
                <div class="weui-dialog__ft">
                    <a id="addTeam_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a runat="server" onserverclick="addTeam_Click" id="addTeam_ok" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">
                施工队伍
                <a id="addTeam" class="weui-btn weui-btn_mini weui-btn_primary" style="top: 5px;right: 0px;left: 5px;">添加</a>
            </h1>
        </div>
        <!-- 队伍列表 -->
        <div class="weui-cells" runat="server" id="list"></div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script>
            $('#addTeam').on('click', function () {
                $('#addTeamDialog').fadeIn(200);
            });
            $('#addTeam_cancel').on('click', function () {
                $('#addTeamDialog').fadeOut(200);
            });
        </script>
    </form>
</body>
</html>
