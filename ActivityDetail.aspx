<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityDetail.aspx.cs" Inherits="WXShare.ActivityDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>活动</title>
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <link rel="stylesheet" type="text/css" href="./css/article.css" />
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <script src="./js/jweixin-1.2.0.js"></script>
    <style>
        #shareit {
            -webkit-user-select: none;
            display: none;
            position: fixed;
            width: 100%;
            height: 100%;
            background: rgba(0,0,0,0.85);
            text-align: center;
            top: 0;
            left: 0;
            z-index: 105;
        }

            #shareit img {
                max-width: 100%;
            }

        .arrow {
            position: absolute;
            right: 10%;
            top: 5%;
        }

        #share-text {
            margin-top: 400px;
        }
    </style>
</head>
<body>
    <div id="shareit">
        <img class="arrow" src="./images/share-it.png">
        <a href="#" id="follow">
            <img id="share-text" src="./images/share-text.png">
        </a>
    </div>
    <form id="form1" runat="server">
        <div id="js_article" class="rich_media">
            <div class="rich_media_inner">
                <div id="page-content" class="rich_media_area_primary">
                    <div id="img-content">
                        <!-- 文章标题 -->
                        <h2 class="rich_media_title" id="activity_name" runat="server">#title#</h2>
                        <div id="meta_content" class="rich_media_meta_list">
                            <em id="end_time" class="rich_media_meta rich_media_meta_text" runat="server">活动截止 #end-time#</em>
                            <em class="rich_media_meta rich_media_meta_text" style="float: right;"><a class="shareBtn" id="signBtn1" runat="server">立即分享</a></em>
                        </div>
                        <!-- 文章内容 -->
                        <div class="rich_media_content " id="js_content" runat="server">
                            #content#
                        </div>
                        <a href="javascript:;" class="shareBtn weui-btn weui-btn_primary" id="signBtn2" runat="server">立即分享</a>
                    </div>
                </div>
            </div>
        </div>

        <script src="./js/zepto.js"></script>
        <script src="./js/zepto-touch.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script>
            var uid;
            wx.config({
                debug: false,
                appId: appId,
                timestamp: timestamp,
                nonceStr: nonceStr,
                signature: signature,
                jsApiList: [
                    'onMenuShareTimeline',
                    'onMenuShareAppMessage',
                    'onMenuShareQQ',
                    'onMenuShareWeibo',
                    'onMenuShareQZone'
                ]
            });
            wx.ready(function () {
                /* 分享给朋友 */
                wx.onMenuShareAppMessage({
                    title: location.title,
                    desc: $('#activity_name').text(),
                    link: location.href.split('#')[0] + (uid ? ('&uid=' + uid) : ''),
                    imgUrl: imgUrl,
                    type: 'link',
                    dataUrl: '',
                    success: function () {
                        //
                    },
                    cancel: function () {
                        //
                    },
                    trigger: function () {
                        if (!uid) {
                            alert('分享前请先登录！');
                            location.href = "/UserLogin.aspx";
                        }
                    }
                });
                /* 分享到朋友圈 */
                wx.onMenuShareTimeline({
                    title: location.title,
                    link: location.href.split('#')[0] + (uid ? ('&uid=' + uid) : ''),
                    imgUrl: imgUrl,
                    success: function () {
                        //
                    },
                    cancel: function () {
                        //
                    },
                    trigger: function () {
                        if (!uid) {
                            alert('分享前请先登录！');
                            location.href = "/UserLogin.aspx";
                        }
                    }
                });
                /* 分享到QQ */
                wx.onMenuShareQQ({
                    title: location.title,
                    desc: $('#activity_name').text(),
                    link: location.href.split('#')[0] + (uid ? ('&uid=' + uid) : ''),
                    imgUrl: imgUrl,
                    success: function () {
                        //
                    },
                    cancel: function () {
                        //
                    }
                });
                /* 分享到微博 */
                wx.onMenuShareWeibo({
                    title: location.title,
                    desc: $('#activity_name').text(),
                    link: location.href.split('#')[0] + (uid ? ('&uid=' + uid) : ''),
                    imgUrl: imgUrl,
                    success: function () {
                        //
                    },
                    cancel: function () {
                        //
                    }
                });
                /* 分享到QQ控件 */
                wx.onMenuShareQZone({
                    title: location.title,
                    desc: $('#activity_name').text(),
                    link: location.href.split('#')[0] + (uid ? ('&uid=' + uid) : ''),
                    imgUrl: imgUrl,
                    success: function () {
                        //
                    },
                    cancel: function () {
                        //
                    }
                });
            });
            /* 错误 */
            wx.error(function (res) {
                //console.log('wx.error: ' + JSON.stringify(res));
            });
            /* 分享按钮提示 */
            $(".shareBtn").on("click", function () {
                if (uid) {
                    $("#shareit").show();
                } else {
                    location.href = '/ActivitySign.aspx?' + location.href.split('?')[1];
                }
            });
            /* 分享按钮提示隐藏 */
            $("#shareit").on("click", function () {
                $("#shareit").hide();
            })
        </script>
    </form>
</body>
</html>
