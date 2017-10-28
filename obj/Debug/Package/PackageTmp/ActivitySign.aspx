<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivitySign.aspx.cs" Inherits="WXShare.ActivitySign" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
</head>
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>活动报名</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <style>
        .weui-cells:before {
            border: 0px;
        }

        .weui-cells:after {
            border: 0px;
        }

        .spr:after {
            border: 1px solid #21a900;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">活动报名</h1>
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
            <!-- 手机验证码 -->
            <div class="weui-cell ">
                <div class="weui-cell__hd">
                    <label class="weui-label">验证码</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="number" name="code" placeholder="填写收到的验证码" required />
                </div>
            </div>
            <!-- 姓名 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">姓名</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" name="name" placeholder="请填写姓名" required />
                </div>
            </div>
            <!-- 区县 -->
            <div class="weui-cell weui-cell_select weui-cell_select-after">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">区县(市)</label>
                </div>
                <div class="weui-cell__bd">
                    <select class="weui-select" name="location">
                        <option value="上城区">上城区</option>
                        <option value="下城区">下城区</option>
                        <option value="拱墅区">拱墅区</option>
                        <option value="江干区">江干区</option>
                        <option value="西湖区">西湖区</option>
                        <option value="滨江区">滨江(高新)区</option>
                        <option value="萧山区">萧山区</option>
                        <option value="余杭区">余杭区</option>
                        <option value="富阳市">富阳市</option>
                        <option value="临安市">临安市</option>
                        <option value="建德市">建德市</option>
                        <option value="桐庐县">桐庐县</option>
                        <option value="淳安县">淳安县</option>
                    </select>
                </div>
            </div>
            <!-- 详细地址 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">详细地址</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" name="detailLocation" placeholder="请输入详细地址" required />
                </div>
            </div>
            <!-- 条款 -->
            <label for="weuiAgree" class="weui-agree">
                <span class="weui-agree__text">报名即表示同意<a href="javascript:alert('跳转到协议页面');" id="viewAgree">《活动报名条款》</a>
                </span>
            </label>
            <!-- 条款对话框 -->
            <!--<div id="dialogs">
                <div class="js_dialog" id="iosDialog2" style="opacity: 1; display: none;">
                    <div class="weui-mask"></div>
                    <div class="weui-dialog">
                        <div class="weui-dialog__bd">
                        </div>
                        <div class="weui-dialog__ft">
                            <a href="javascript:;" class="weui-dialog__btn weui-dialog__btn_primary">关闭</a>
                        </div>
                    </div>
                </div>
            </div>-->
            <!-- 注册按钮 -->
            <div class="weui-btn-area">
                <a class="weui-btn weui-btn_primary" onclick="$('#form1').submit();" id="showTooltips">注册</a>
                <label for="weuiAgree" class="weui-agree" style="text-align: center;">
                    <span class="weui-agree__text">
                        <a href="javascript:history.go(-1);">查看活动详情</a>
                    </span>
                </label>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
        <!--<script src = "./js/example.js"></script>-->

        <script>
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
            /* 输入为空时报错 */
            $('form').on('submit', function () {
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
                // 验证码
                if (!RegExp('^\\d{4}$').test($('input[name=code]').val())) {
                    alterError($('input[name=code]')[0]);
                    ok = false;
                }
                return ok;
            });
            $('input').on('keydown', function () {
                $(this).parent().parent().find('.weui-icon-warn').parent().remove();
                $(this).parent().parent().removeClass('weui-cell_warn');
            });
            function success(iden, text, redir) {
                if (iden == 1) {
                    var $toast = $('#toast');
                    $('#toastInfo').text(text);
                    $toast.fadeIn(100);
                    setTimeout(function () {
                        $toast.fadeOut(100);
                        if (redir) {
                            location.href = '/';
                        }
                    }, 2000);
                } else {
                    location.href = '/';
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
