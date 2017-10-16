<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ActivityDetail.aspx.cs" Inherits="WXShare.ActivityDetail" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>活动</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <link rel="stylesheet" type="text/css" href="./css/article.css" />
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
        </script>

        <script id="wxscript">
            wx.config({
                debug: true, // 开启调试模式,调用的所有api的返回值会在客户端alert出来，若要查看传入的参数，可以在pc端打开，参数信息会通过log打出，仅在pc端时才会打印。  
                appId: appId, // 必填，公众号的唯一标识  
                timestamp: timestamp, // 必填，生成签名的时间戳  
                nonceStr: nonceStr, // 必填，生成签名的随机串  
                signature: signature,// 必填，签名，见附录1  
                jsApiList: [
                    'checkJsApi',
                    'onMenuShareTimeline',
                    'onMenuShareAppMessage',
                    'onMenuShareQQ',
                    'onMenuShareWeibo'
                ] // 必填，需要使用的JS接口列表，所有JS接口列表见附录2  
            });

            wx.ready(function () {
                wx.onMenuShareAppMessage({
                    title: location.title, // 分享标题
                    desc: $('#activity_name').text(), // 分享描述
                    link: location.href.split('#')[0] + uid ? ('&uid=' + uid) : '', // 分享链接，该链接域名或路径必须与当前页面对应的公众号JS安全域名一致
                    imgUrl: '', // 分享图标
                    type: 'link', // 分享类型,music、video或link，不填默认为link
                    dataUrl: '', // 如果type是music或video，则要提供数据链接，默认为空
                    success: function () {
                        // 用户确认分享后执行的回调函数
                    },
                    cancel: function () {
                        // 用户取消分享后执行的回调函数
                    },
                    trigger: function () {
                        if (!uid) {
                            alert('没有登录，分享将无法获得分成！');
                        }
                    }
                });
            });

            wx.error(function (res) {
                alert('wx.error: ' + JSON.stringify(res));
            });
        </script>

        <script>
            $(".shareBtn").on("click", function () {
                if (uid) {
                    $("#shareit").show();
                } else {
                    location.href = '/ActivitySign.aspx?' + location.href.split('?')[1];
                }
            });

            $("#shareit").on("click", function () {
                $("#shareit").hide();
            })
        </script>
    </form>
</body>
</html>
