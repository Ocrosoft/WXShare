<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserRegister.aspx.cs" Inherits="WXShare.UserRegister" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
</head>
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>注册</title>
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
        <!-- 弹出悬浮提示 -->
        <div id="toast" style="display: none;">
            <div class="weui-mask_transparent"></div>
            <div class="weui-toast">
                <i class="weui-icon-success-no-circle weui-icon_toast"></i>
                <p class="weui-toast__content" id="toastInfo">注册成功</p>
            </div>
        </div>
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px;padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px;font-weight: bold;">账号注册</h1>
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
            <!-- 姓名 -->
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">姓名</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="text" name="name" placeholder="请填写姓名" required />
                </div>
            </div>
            <!--  -->
            <!--<div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">日期</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="date" value="" />
                </div>
            </div>-->
            <!--  -->
            <!--<div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">时间</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="datetime-local" value="" placeholder="" />
                </div>
            </div>-->
            <!-- 手机验证码 -->
            <div class="weui-cell ">
                <div class="weui-cell__hd">
                    <label class="weui-label">验证码</label>
                </div>
                <div class="weui-cell__bd">
                    <input class="weui-input" type="number" name="code" placeholder="填写收到的验证码" required />
                </div>
            </div>
            <!-- 注册身份 -->
            <div class="weui-cell weui-cell_select weui-cell_select-after">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">注册身份</label>
                </div>
                <div class="weui-cell__bd">
                    <select class="weui-select" name="iden">
                        <option value="1">会员</option>
                        <option value="2">业务员(需审核)</option>
                        <option value="3">经销商(需审核)</option>
                        <option value="4">施工队(需审核)</option>
                        <option value="5">管理员(需审核)</option>
                    </select>
                </div>
            </div>
            <!-- 附加信息-业务员-施工队-管理员 -->
            <div id="addition-yyw" style="display: none;">
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">身份证</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" name="idcard_ywy" placeholder="请输入18位身份证">
                    </div>
                </div>
            </div>
            <!-- 附加信息-经销商 -->
            <div id="addition-jxs" style="display: none;">
                <!-- 身份信息 -->
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">身份证</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="number" name="idcard_jxs" placeholder="请输入18位身份证">
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
                <div class="weui-cell">
                    <div class="weui-cell__hd">
                        <label class="weui-label">详细地址</label>
                    </div>
                    <div class="weui-cell__bd">
                        <input class="weui-input" type="text" name="detailLocation" placeholder="请输入详细地址" />
                    </div>
                </div>
                <div class="page__bd">
                    <!-- 资质上传 -->
                    <div class="weui-gallery" id="gallery">
                        <span class="weui-gallery__img" id="galleryImg"></span>
                        <div class="weui-gallery__opr">
                            <a href="javascript:" class="weui-gallery__del">
                                <i class="weui-icon-delete weui-icon_gallery-delete"></i>
                            </a>
                        </div>
                    </div>
                    <div class="weui-cells weui-cells_form">
                        <div class="weui-cell">
                            <div class="weui-cell__bd">
                                <div class="weui-uploader">
                                    <div class="weui-uploader__hd">
                                        <p class="weui-uploader__title">图片上传</p>
                                        <!-- 图片数量限制 -->
                                        <div class="weui-uploader__info">0/5</div>
                                    </div>
                                    <div class="weui-uploader__bd">
                                        <ul class="weui-uploader__files" id="uploaderFiles">
                                        </ul>
                                        <div class="weui-uploader__input-box">
                                            <input id="uploaderInput" class="weui-uploader__input" type="file" accept="image/*" name="imgupload" multiple />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- 条款 -->
            <label for="weuiAgree" class="weui-agree">
                <span class="weui-agree__text">注册即表示同意<a href="javascript:alert('跳转到协议页面');" id="viewAgree">《注册协议》</a>
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
                        <a href="javascript:location.href='/UserLogin.aspx'">已有账号？去登录</a>
                    </span>
                </label>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
        <!--<script src = "./js/example.js"></script>-->

        <script>
            /* 动态调整附加信息 */
            $('select[name=iden]').on('change', function () {
                /* 普通会员 */
                if (this.value == 1) {
                    $('#addition-yyw,#addition-jxs').css('display', 'none');
                    $('#addition-yyw,#addition-jxs').find('input').removeAttr('required');
                    /* 业务员-施工队-管理员 */
                } else if (this.value == 2 || this.value == 4 || this.value == 5) {
                    $('#addition-yyw').css('display', '');
                    $('#addition-yyw').find('input').attr('required', 'required');
                    $('#addition-jxs').css('display', 'none');
                    $('#addition-jxs').find('input').removeAttr('required');
                    /* 经销商 */
                } else if (this.value == 3) {
                    $('#addition-yyw').css('display', 'none');
                    $('#addition-yyw').find('input').removeAttr('required');
                    $('#addition-jxs').css('display', '');
                    $('#addition-jxs').find('input').attr('required', 'required');
                }
            });
            /* 图片上传 */
            var tmpl = '<li class="weui-uploader__file" data-name="#name#" style="background-image:url(#url#)"></li>',
                $gallery = $("#gallery"), $galleryImg = $("#galleryImg"),
                $uploaderInput = $("#uploaderInput"),
                $uploaderFiles = $("#uploaderFiles");
            $uploaderInput.on('change', function (e) {
                var src, url = window.URL || window.webkitURL || window.mozURL, files = e.target.files;
                if (files.length > 5) {
                    alert('图片数量不能超过5张！');
                    return;
                }
                $uploaderFiles.children().remove();
                for (var i = 0, len = files.length; i < len; ++i) {
                    var file = files[i];
                    if (url) {
                        src = url.createObjectURL(file);
                    } else {
                        src = e.target.result;
                    }
                    $uploaderFiles.append($(tmpl.replace('#url#', src)));
                }
                $('.weui-uploader__info').text(files.length + '/5');
            });
            /* 图片查看删除 */
            $('#uploaderFiles').on('click', 'li', function () {
                $galleryImg.attr('style', this.getAttribute('style'));
                _self = this;
                $('.weui-gallery__del').on('click', function () {
                    _self.remove();
                    $('.weui-uploader__info').text(parseInt($('.weui-uploader__info').text().split('/')[0]) - 1 + '/5');
                    $gallery.fadeOut(100);
                });
                $gallery.fadeIn(100);
            });
            $gallery.on('click', function () {
                $gallery.fadeOut(100);
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
            /* 输入为空时报错 */
            $('form').on('submit', function () {
                /* 经销商不可注册 */
                if ($('input[name=iden]').val() == 3) {
                    alert('经销商暂时不提供注册！');
                    return false;
                }

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
                // 身份证
                if ($('[name=iden]').val() == 2 || $('[name=iden]').val() == 4 || $('[name=iden]').val() == 5) {
                    if (!RegExp('^(\\d{15}$|^\\d{18}$|^\\d{17}(\\d|X|x))$').test($('[name=IDCard_YWY]').val())) {
                        alterError($('input[name=IDCard_YWY]')[0]);
                        ok = false;
                    }
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
