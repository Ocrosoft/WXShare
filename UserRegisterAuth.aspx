<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegisterAuth.aspx.cs" Inherits="WXShare.UserRegisterAuth" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>注册审核</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="weui-btn-area">
            <div class="weui-cell__hd">
                <h2>注册审核</h2>                
            </div>
        </div>
        <div class="weui-cells">
            <div class="weui-cell weui-cell_select-after">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">身份</label>
                </div>
                <div class="weui-cell__bd">
                    <select id="activitySelect" runat="server" class="weui-select" name="activitySelect">
                        <option value="0" selected>所有</option>
                        <option value="2">业务员</option>
                        <option value="3">经销商</option>
                        <option value="4">施工队</option>
                        <option value="5">管理员</option>
                    </select>
                </div>
                <div class="weui-cell__ft">
                    <button runat="server" id="selectActivity" class="weui-vcode-btn" onserverclick="vcodeBtn_Click">筛选</button>
                </div>
            </div>
        </div>
        <div class="weui-cells" runat="server" id="regList">
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
    </form>
</body>
</html>
