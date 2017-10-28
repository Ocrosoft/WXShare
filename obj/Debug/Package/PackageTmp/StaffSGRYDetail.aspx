<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffSGRYDetail.aspx.cs" Inherits="WXShare.StaffSGRYDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>详细信息</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <style>
        select {
            width: auto;
            padding: 0 2%;
            margin: 0;
        }

        option {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 弹出悬浮提示 -->
        <div class="js_dialog" id="iosDialog1" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">删除确认</strong></div>
                <div class="weui-dialog__bd">删除后不可恢复，确定要删除吗？</div>
                <div class="weui-dialog__ft">
                    <a id="dialog_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a onserverclick="DialogOK_Click" id="dialog_ok" class="weui-dialog__btn weui-dialog__btn_primary" runat="server">确定</a>
                </div>
            </div>
        </div>
        <!-- 施工队选择 -->
        <div class="js_dialog" id="teamDialog" style="display: none">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">添加到施工队</strong></div>
                <div class="weui-dialog__bd">
                    <select id="teamSelect" runat="server" class="weui-select" name="teamSelect" style="width: auto; padding: 0px;">
                        <option value="0" selected>请选择施工队</option>
                    </select>
                </div>
                <div class="weui-dialog__ft">
                    <a id="team_cancel" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a runat="server" onserverclick="TeamOK_Click" id="team_ok" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
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
            <!-- 所在施工队 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">施工队</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" name="team" runat="server" id="team" disabled />
                </div>
            </div>
            <!-- 修改按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_default" id="modify">修改信息</a>
                <a class="weui-btn weui-btn_primary" id="save" runat="server" onserverclick="ButtonOK_Click" style="display: none;">保存修改</a>
                <a runat="server" class="weui-btn weui-btn_primary" id="addToTeam">添加到施工队</a>
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
            $('#dialog_cancel').on('click', function () {
                $('#iosDialog1').fadeOut(200);
            });
            $('#delete').on('click', function () {
                $('#iosDialog1').fadeIn(200);
            });
            $('#team_cancel').on('click', function () {
                $('#teamDialog').fadeOut(200);
            });
            $('#addToTeam').on('click', function () {
                $('#teamDialog').fadeIn(200);
            });
        </script>
    </form>
</body>
</html>
