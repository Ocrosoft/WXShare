<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="ActivityEditor.aspx.cs" Inherits="WXShare.ActivityEditor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>编辑活动</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <style>
        .w-e-text * {
            max-width: 100% !important;
        }

        .w-e-text > img {
            height: auto !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" method="post" enctype="multipart/form-data">
        <!-- 保存按钮 -->
        <div class="weui-btn-area">
            <div class="weui-cell__hd">
                <h2 style="float: left;">编辑活动</h2>
                <a style="width: 33.333%; margin-left: 65%;" class="weui-btn weui-btn_primary" onclick="document.getElementById('htmlInput').value = $('#textarea')[0].childNodes[0].innerHTML;$('#form1').submit();" id="showTooltips">保存</a>
            </div>
        </div>
        <div class="weui-cells weui-cells_form">
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">标题</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="title" runat="server" class="weui-input" type="text" name="title" placeholder="填写文章标题" required />
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">简介</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="brief" runat="server" class="weui-input" type="text" name="brief" placeholder="填写文章简介" required />
                </div>
            </div>
            <div class="weui-cell weui-cell_switch">
                <div class="weui-cell__bd">是否有效</div>
                <div class="weui-cell__ft">
                    <input id="checkValid" runat="server" class="weui-switch" type="checkbox" name="checkValid">
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">开始时间</label></div>
                <div class="weui-cell__bd">
                    <input id="timeStart" runat="server" class="weui-input" type="datetime-local" value="" placeholder="" name="timeStart" required>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">结束时间</label></div>
                <div class="weui-cell__bd">
                    <input id="timeEnd" runat="server" class="weui-input" type="datetime-local" value="" placeholder="" name="timeEnd" required>
                </div>
            </div>
            <div class="weui-cell weui-cell_select" style="padding: 10px 15px">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">活动模板</label></div>
                <div class="weui-cell__bd">
                    <select id="templateSelect" runat="server" class="weui-select" name="template">
                    </select>
                </div>
            </div>
            <div class="weui-cell">
                <div class="weui-cell__hd">
                    <label class="weui-label">模板设置</label>
                </div>
                <div class="weui-cell__bd">
                    <input id="templateAdditionInput" runat="server" class="weui-input" type="text" name="templateAddition" placeholder="模板设置，详见说明书" required />
                </div>
            </div>
            <div id="toolbar" class="toolbar"></div>
            <div style="padding: 5px 0; color: #ccc"></div>
            <input type="text" id="htmlInput" name="htmlInput" style="display: none;" />
            <div id="textarea" class="con" runat="server">
            </div>
            <div class="page__bd">
                <!-- 预览图上传 -->
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
                                    <p class="weui-uploader__title">上传预览图</p>
                                    <!-- 图片数量限制 -->
                                    <div id="imgCount" runat="server" class="weui-uploader__info">0/1</div>
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
        <script src="./js/zepto.js"></script>
        <script src="./js/zepto-touch.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>
        <script type="text/javascript" src="//unpkg.com/wangeditor/release/wangEditor.min.js"></script>
        <script type="text/javascript">
            /* 编辑器 */
            $(function () {
                var E = window.wangEditor;
                var editor1 = new E('#toolbar', '#textarea');
                editor1.create();
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
            $('input').on('keydown', function () {
                $(this).parent().parent().find('.weui-icon-warn').parent().remove();
                $(this).parent().parent().removeClass('weui-cell_warn');
            });
            /* 图片上传 */
            var tmpl = '<li class="weui-uploader__file" data-name="#name#" style="background-image:url(#url#)"></li>',
                $gallery = $("#gallery"), $galleryImg = $("#galleryImg"),
                $uploaderInput = $("#uploaderInput"),
                $uploaderFiles = $("#uploaderFiles");
            $uploaderInput.on('change', function (e) {
                var src, url = window.URL || window.webkitURL || window.mozURL, files = e.target.files;
                $uploaderFiles.children().remove();
                var file = files[0];
                if (url) {
                    src = url.createObjectURL(file);
                } else {
                    src = e.target.result;
                }
                $uploaderFiles.append($(tmpl.replace('#url#', src)));
                $('.weui-uploader__info').text('1/1');
                $('.weui-uploader__input-box').hide();
            });
            /* 图片查看删除 */
            $('#uploaderFiles').on('click', 'li', function () {
                $galleryImg.attr('style', this.getAttribute('style'));
                _self = this;
                $('.weui-gallery__del').on('click', function () {
                    _self.remove();
                    $('.weui-uploader__info').text(parseInt($('.weui-uploader__info').text().split('/')[0]) - 1 + '/1');
                    var f = $("#uploaderInput")[0];
                    if (f.value) {
                        try {
                            f.value = ''; //for IE11, latest Chrome/Firefox/Opera...  
                        } catch (err) {
                        }
                        if (f.value) { //for IE5 ~ IE10  
                            var form = document.createElement('form'), ref = f.nextSibling, p = f.parentNode;
                            form.appendChild(f);
                            form.reset();
                            p.insertBefore(f, ref);
                        }
                    }
                    $('.weui-uploader__input-box').show();
                    $gallery.fadeOut(100);
                });
                $gallery.fadeIn(100);
            });
            $gallery.on('click', function () {
                $gallery.fadeOut(100);
            });
            /* 时间修改 */
            $('[name=timeEnd]').on('change', function () {
                if (this.value <= $('[name=timeStart]').val()) {
                    alert('结束时间必须大于开始时间！');
                    this.value = "";
                }
            });
            $('[name=timeStart]').on('change', function () {
                if (this.value >= $('[name=timeEnd]').val()) {
                    $('[name=timeEnd]').val("");
                }
            });
            /* 提交检查 */
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
            });
            function showImgSrc(url) {
                $uploaderFiles.append($(tmpl.replace('#url#', '\'' + url + '\'')));
                $('.weui-uploader__info').text('1/1');
            }
        </script>
    </form>
</body>
</html>
