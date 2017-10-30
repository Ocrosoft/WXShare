<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserProfile.aspx.cs" Inherits="WXShare.UserProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>个人信息</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">个人信息</h1>
        </div>
        <div class="weui-cells">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">姓名</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputName" name="inputName" disabled class="weui-input" type="text" required/>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputPhone" name="inputPhone" disabled class="weui-input" type="text" />
                </div>
            </div>
            <div class="weui-cell" runat="server" id="IDCardDiv">
                <div class="weui-cell__hd">
                    <label class="weui-label">身份证</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputIDCard" name="inputIDCard" disabled class="weui-input" type="text" />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">消息标识</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputOpenID" name="inputOpenID" class="weui-input" type="text" 
                        placeholder="公众号回复数字-1获取" />
                </div>
            </div>
            <!--<div class="weui-cell weui-cell_access">
                <div class="weui-cell__bd">金额</div>
                <div class="weui-cell__ft" style="font-size: 0">
                    <span style="vertical-align:middle; font-size: 17px;" runat="server" id="money"></span>
                    <span runat="server" id="moneyChange" class="weui-badge weui-badge_dot" style="margin-left: 5px;margin-right: 5px;display:none;"></span>
                </div>
            </div>-->
        </div>
        <div class="weui-cells">
            <!-- 修改按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" id="save" runat="server" onserverclick="ButtonOK_Click">保存消息标识</a>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

    </form>
</body>
</html>
