<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLogin.aspx.cs" Inherits="WXShare.UserLogin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>登录</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <style>
        .weui-cells:before {
            border: 0px;
        }

        .weui-cells:after {
            border: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 弹窗 -->
        <div class="js_dialog" id="iosDialog1" style="display: none;">
            <div class="weui-mask"></div>
            <div class="weui-dialog">
                <div class="weui-dialog__hd"><strong class="weui-dialog__title">自动登录提示</strong></div>
                <div class="weui-dialog__bd">
                    自动登录可能不安全<br />
                    请勿在不受信任的设备上勾选此项
                </div>
                <div class="weui-dialog__ft">
                    <a href="javascript:;" class="weui-dialog__btn weui-dialog__btn_default">取消</a>
                    <a href="javascript:;" class="weui-dialog__btn weui-dialog__btn_primary">确定</a>
                </div>
            </div>
        </div>
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">账号密码登录</h1>
        </div>
        <!-- 表单 -->
        <div class="weui-cells weui-cells_form">
            <!-- 手机号 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机号码</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="tel" name="tel" placeholder="请填写手机号码" required />
                </div>
            </div>
            <!-- 密码 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">密码</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="tel" class="weui-input" type="password" name="password" placeholder="请填写密码" required />
                </div>
                <div class="weui-cell__ft" style="display: none;">
                    <label class="weui-cell__bd">密码错误</label>
                    <i class="weui-icon-warn" style="display: inline-block;"></i>
                </div>
            </div>
            <!-- 登录身份 -->
            <div class="weui-cell weui-cell_select weui-cell_select-after">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">登录身份</label>
                </div>
                <div class="weui-cell__bd">
                    <select class="weui-select" name="iden">
                        <option value="1">会员</option>
                        <option value="2">业务员</option>
                        <option value="3">经销商</option>
                        <option value="4">施工队</option>
                        <option value="5">管理员</option>
                    </select>
                </div>
            </div>
            <!-- 15天自动登录 -->
            <div class="weui-cells weui-cells_checkbox">
                <label class="weui-cell weui-check__label" for="s11">
                    <div class="weui-cell__hd">
                        <input type="checkbox" class="weui-check" name="autoLogin" id="s11">
                        <i class="weui-icon-checked"></i>
                    </div>
                    <div class="weui-cell__bd">
                        <p>15天自动登录</p>
                    </div>
                    <div class="weui-cell__ft">
                        <label for="weuiAgree" class="weui-agree">
                            <span class="weui-agree__text">
                                <a href="/UserLoginPhone.aspx">用短信验证码登录</a>
                            </span>
                        </label>
                    </div>
                </label>
                <!-- 登录按钮 -->
                <div class="weui-btn-area">
                    <a class="weui-btn weui-btn_primary" onclick="$('#form1').submit();" id="showTooltips">登录</a>
                    <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                        <span class="weui-agree__text">
                            <a href="/UserRegister.aspx">没有账号？去注册</a>
                        </span>
                    </label>
                </div>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script>
            /**
             * 显示错误
             * @param element 要显示错误的元素
             */
            function alterError(element) {
                var $_self = $(element);
                if ($_self.parent().parent().hasClass('weui-cell_warn')) return;
                $_self.parent().parent().addClass('weui-cell_warn');
                $_self.parent().parent().find('.weui-cell__ft').remove();
                $_self.parent().parent().append($('<div class="weui-cell__ft"><i class="weui-icon-warn" style="display: inline-block;"></i></div>'));
            }
            /**
             * 显示错误，不添加元素
             * @param element
             */
            function showError(element) {
                var $_self = $(element);
                if ($_self.parent().parent().hasClass('weui-cell_warn')) return;
                $_self.parent().parent().addClass('weui-cell_warn');
                $_self.parent().parent().find('.weui-cell__ft').css('display', '');
            }
            $('input').on('keydown', function () {
                var $_self = $(this);
                $_self.parent().parent().removeClass('weui-cell_warn');
                $_self.parent().parent().find('.weui-cell__ft').css('display', 'none');
            });
            /* 表单检查 */
            $('#form1').on('submit', function () {
                var ok = true;
                /* 必填项 */
                $('input[required]').each(function (index, item) {
                    $_self = $(item);
                    if ($_self.val() === '') {
                        alterError(item);
                        ok = false;
                        return;
                    }
                });
                return ok;
            });
            /* 显示警告 */
            $('[name=autoLogin]').on('change', function () {
                var _self = this;
                if (this.checked == true) {
                    /* 关闭 */
                    $('#iosDialog1').on('click', '.weui-dialog__btn_default', function () {
                        _self.checked = false;
                        $(this).parents('.js_dialog').fadeOut(200);
                    });
                    $('#iosDialog1').on('click', '.weui-dialog__btn_primary', function () {
                        $(this).parents('.js_dialog').fadeOut(200);
                    });
                    /* 显示 */
                    $('#iosDialog1').fadeIn(200);
                }
            });
        </script>
    </form>
</body>
</html>
