<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivitySignView.aspx.cs" Inherits="WXShare.ActivitySignView" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>报名管理</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="weui-btn-area">
            <div class="weui-cell__hd">
                <h2>报名情况</h2>
            </div>
        </div>
        <div class="weui-cells">
            <div class="weui-cell weui-cell_select-after">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">活动</label>
                </div>
                <div class="weui-cell__bd">
                    <select id="activitySelect" runat="server" class="weui-select" name="activitySelect">
                        <option value="0" selected>所有活动</option>
                    </select>
                </div>
                <div class="weui-cell__ft">
                    <button runat="server" id="selectActivity" class="weui-vcode-btn" onserverclick="vcodeBtn_Click">筛选</button>
                </div>
            </div>
        </div>
        <!-- 报名列表 -->
        <div class="weui-cells" runat="server" id="signList">
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
    </form>
</body>
</html>
