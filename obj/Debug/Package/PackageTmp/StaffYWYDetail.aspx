<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffYWYDetail.aspx.cs" Inherits="WXShare.StaffYWYDetail" %>

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
        <div class="js_dialog" id="iosDialog1" style="display:none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">删除确认</strong></div>
                <div class="weui-dialog__bd">删除后不可恢复，确定要删除吗？</div>
                <div class="weui-dialog__ft">
                    <a id="dialog_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a id="dialog_ok" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">详细信息</h1>
        </div>
        <!-- 表单 -->
        <div class="weui-cells weui-cells_form">
            <!-- 姓名 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">姓名</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" name="name" runat="server" id="name" disabled required />
                </div>
            </div>
            <!-- 手机号 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机号码</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="tel" name="tel" runat="server" id="tel" disabled required />
                </div>
            </div>
            <!-- 身份证 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">身份证</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" name="idCard" runat="server" id="idCard" disabled required />
                </div>
            </div>
            <!-- 修改按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_default" id="showTooltips">修改信息</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a id="delete">删除该账号</a>
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
                // 手机号
                if (!RegExp('^((13[0-9])|(14[5|7])|(15([0-3]|[5-9]))|(18[0,5-9]))\\d{8}$').test($('input[name=tel]').val())) {
                    alterError($('input[name=tel]')[0]);
                    ok = false;
                }
                // 身份证
                if ($('[name=iden]').val() == 2 || $('[name=iden]').val() == 4 || $('[name=iden]').val() == 5) {
                    if (!RegExp('^(\\d{15}$|^\\d{18}$|^\\d{17}(\\d|X|x))$').test($('[name=idcard_ywy]').val())) {
                        alterError($('input[name=idcard_ywy]')[0]);
                        ok = false;
                    }
                }
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
        </script>
    </form>
</body>
</html>
