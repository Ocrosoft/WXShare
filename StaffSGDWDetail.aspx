<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffSGDWDetail.aspx.cs" Inherits="WXShare.StaffSGDWDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>详细信息</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 弹出悬浮提示 -->
        <div class="js_dialog" id="iosDialog1" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">解散确认</strong></div>
                <div class="weui-dialog__bd">解散后不可恢复，确定要解散吗？</div>
                <div class="weui-dialog__ft">
                    <a id="dialog_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="dialog_ok" runat="server" onserverclick="DeleteBtn_Click" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">详细信息</h1>
        </div>
        <!-- 表单 -->
        <div class="weui-cells weui-cells_form">
            <!-- 施工队名称 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">名称</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" name="name" runat="server" id="name" disabled required />
                </div>
            </div>
            <!-- 成员列表 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">成员</label>
                </div>
                <div class="weui-cell__ft">
                    <button type="button" id="vcodeBtn" class="weui-vcode-btn" name="vcode">显示成员</button>
                </div>
            </div>
        </div>
        <div class="weui-cells hidden" runat="server" id="members" style="display: none;">
        </div>
        <div class="weui-cells">
            <!-- 修改按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_default" id="modify">修改信息</a>
                <a class="weui-btn weui-btn_primary" id="save" runat="server" onserverclick="ButtonOK_Click" style="display: none;">保存修改</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a id="delete">解散施工队</a>
                    </span>
                </label>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script>
            $('#showTooltips').on('click', function () {
                if ($(this).hasClass('weui-btn_default')) {
                    $(this).removeClass('weui-btn_default');
                    $(this).addClass('weui-btn_primary');
                    $(this).text('保存修改');
                    $('[required]').removeAttr('disabled');
                } else {
                    $('#form1').submit();
                }
            });
            $('#modify').on('click', function () {
                $(this).css('display', 'none');
                $('#save').css('display', 'block');
                $('[required]').removeAttr('disabled');
            });
            /**
            * 显示错误
            * @param element 要显示错误的元素
            */
            function alterError(element) {
                var $_self = $(element);
                if ($_self.parent().parent().hasClass('weui-cell_warn')) return;
                $_self.parent().parent().addClass('weui-cell_warn');
                $_self.parent().parent().append($('<div class="weui-cell__ft"><i class="weui-icon-warn" style="display: inline-block;"></i></div>'));
            }
            $('#form1').on('submit', function () {
                var ok = true;
                // 必填项
                $('input[required]').each(function (index, item) {
                    $_self = $(item);
                    if ($_self.val() === '') {
                        alterError(item);
                        ok = false;
                        return;
                    }
                });
                return ok;
            })
            $('#dialog_ok').on('click', function () {
                location.href = location.href + '&del=true';
            });
            $('#dialog_cancel').on('click', function () {
                $('#iosDialog1').fadeOut(200);
            });
            $('#delete').on('click', function () {
                $('#iosDialog1').fadeIn(200);
            });

            $('#vcodeBtn').on('click', function () {
                if ($('#members').hasClass('hidden')) {
                    $('#members').show(200);
                    $('#members').removeClass('hidden');
                } else {
                    $('#members').hide(200);
                    $('#members').addClass('hidden');
                }
            });
        </script>
    </form>
</body>
</html>
