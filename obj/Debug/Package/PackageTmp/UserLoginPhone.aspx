<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserLoginPhone.aspx.cs" Inherits="WXShare.UserLoginPhone" %>

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
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">短信验证码登录</h1>
        </div>
        <!-- 表单 -->
        <div class="weui-cells weui-cells_form">
            <!-- 手机号，发送验证码 -->
            <div class="weui-cell weui-cell_vcode">
                <div class="weui-cell__hd">
                    <label class="weui-label">手机号</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="tel" runat="server" class="weui-input" type="tel" name="tel" placeholder="请填写手机号" required />
                </div>
                <div class="weui-cell__ft">
                    <button type="button" id="vcodeBtn" class="weui-vcode-btn" runat="server" onserverclick="vcodeBtn_Click" name="vcode">获取验证码</button>
                </div>
            </div>
            <!-- 验证码 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">验证码</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="password" class="weui-input" type="number" name="password" placeholder="请填写验证码" required />
                </div>
                <div class="weui-cell__ft" style="display: none;">
                    <label class="weui-cell__bd">验证码错误</label>
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
        </div>
        <!-- 登录按钮 -->
        <div class="weui-cells">
            <div class="weui-cell__ft">
                <label for="weuiAgree" class="weui-agree">
                    <span class="weui-agree__text">
                        <a href="/UserLogin.aspx">用账号密码登录</a>
                    </span>
                </label>
            </div>
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" onclick="$('#form1').submit();" id="showTooltips">登录</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a href="/UserRegister.aspx">没有账号？去注册</a>
                    </span>
                </label>
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
                $_self.parent().parent().find('.weui-icon-warn').parent().remove();
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
            function success(iden, text, redir) {
                if (iden == 1) {
                    var $toast = $('#toast');
                    $('#toastInfo').text(text);
                    $toast.fadeIn(100);
                    setTimeout(function () {
                        $toast.fadeOut(100);
                        if (redir) {
                            location.href = '/UserLogin.aspx';
                        }
                    }, 2000);
                } else {
                    location.href = '/WaitAuth.aspx';
                }
            }
            var cd = 60; // 计时
            var it; // 计时器
            function startCountDown() {
                cd = 60;
                var vcodeBtn = $('[name=vcode]');
                vcodeBtn.attr('disabled', 'disabled');
                vcodeBtn.css('color', '#C0C0C0');
                it = setInterval(function () {
                    if (--cd == 0) {
                        clearInterval(it);
                        vcodeBtn.removeAttr('disabled');
                        vcodeBtn.css('color', '');
                        vcodeBtn.text('重新发送');
                    } else {
                        vcodeBtn.text('重新发送(' + cd + ')');
                    }
                }, 1000);
            }
        </script>
    </form>
</body>
</html>
