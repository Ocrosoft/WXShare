<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivitySignViewDetail.aspx.cs" Inherits="WXShare.ActivitySignViewDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>报名详情</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 弹出悬浮提示 -->
        <div class="js_dialog" id="iosDialog1" style="display:none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">删除确认</strong></div>
                <div class="weui-dialog__bd">删除后不可恢复，确定要删除吗？</div>
                <div class="weui-dialog__ft">
                    <a id="dialog_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="dialog_ok" runat="server" onserverclick="DeleteBtn_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 业务员选择 -->
        <div class="js_dialog" id="YWYDialog" style="display: none">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">派单给业务员</strong></div>
                <div class="weui-dialog__bd">
                    <select id="YWYSelect" runat="server" class="weui-select" name="YWYSelect" style="width: auto; padding: 0px;">
                        <option value="0" selected>请选择业务员</option>
                    </select>
                </div>
                <div class="weui-dialog__ft">
                    <a id="button_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a runat="server" onserverclick="ButtonOK_Click" id="button_ok" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 标题 -->
        <div class="weui-btn-area">
            <div class="weui-cell__hd">
                <h2>报名详情</h2>                
            </div>
        </div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd"><label class="weui-label">姓名</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputName" disabled class="weui-input" type="text">
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机号</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputPhone" class="weui-input" type="tel" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">地址</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputLocation" class="weui-input" type="text" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">报名活动</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputActivity" class="weui-input" type="text" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">推荐人</label>
                </div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputShare" class="weui-input" type="text" disabled>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd"><label for="" class="weui-label">报名时间</label></div>
                <div class="weui-cell__bd">
                    <input runat="server" id="inputTime" class="weui-input" type="datetime-local" disabled >
                </div>
            </div>
             <!-- 派单按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" id="toOrder">派单到业务员</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a id="delete">删除该报名信息</a>
                    </span>
                </label>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script>
            $('#toOrder').on('click', function () {
                $('#YWYDialog').fadeIn(200);
            });
            $('#button_cancel').on('click', function () {
                $('#YWYDialog').fadeOut(200);
            });
        </script>
    </form>
</body>
</html>
