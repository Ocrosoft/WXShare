<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserIndex.aspx.cs" Inherits="WXShare.UserIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>主页</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
    <style>
        .weui-cells:before {
            border: 0px;
        }

        .weui-cells:after {
            border: 0px;
        }

        .page__hd > img {
            width: 100%;
        }

        .weui-grid__label {
            font-size: 15px;
        }

        .weui-grid__icon {
            height: 45px;
            width: 45px;
            margin-bottom: 10px;
        }

        .weui-grid {
            padding: 30px 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="page__hd" style="padding: 0px;">
            <img src="http://shuaxinfuwu.nipponpaint.com.cn/cdn/media/Images/Shuaxin/Banner/PCBanner1.ashx?h=515&la=en&w=1600&hash=4A7B4C2AC762F68A62A75BB5DAB16537E951AC58" />
        </div>
        <div class="weui-grids">
            <a href="/Activity.aspx" class="weui-grid">
                <% if (Session["iden"].ToString() == "1")
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/activity.png" alt="">
                </div>
                <p class="weui-grid__label">当前活动</p>
                <%}
                    else
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
                <%} %>
            </a>
            <a href="/ActivityAll.aspx" class="weui-grid">
                <% if (Session["iden"].ToString() == "5")
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/activity.png" alt="">
                </div>
                <p class="weui-grid__label">活动管理</p>
                <%}
                    else
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
                <%} %>
            </a>
            <a href="/ActivitySignView.aspx" class="weui-grid">
                <% if (Session["iden"].ToString() == "5")
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/signup.png" alt="">
                </div>
                <p class="weui-grid__label">报名管理</p>
                <%}
                    else
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
                <%} %>
            </a>
            <a href="/UserRegisterAuth.aspx" class="weui-grid">
                <% if (Session["iden"].ToString() == "5")
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/checkout.png" alt="">
                </div>
                <p class="weui-grid__label">注册审核</p>
                <%}
                    else
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
                <%} %>
            </a>
            <a href="javascript:;" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
            </a>
            <a href="javascript:;" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
            </a>
            <a href="javascript:alert('暂未开放');" class="weui-grid">
                <% if (Session["iden"].ToString() == "1")
                    { %>
                <div class="weui-grid__icon" style="width: 35.86px">
                    <img src="./images/share.png" alt="">
                </div>
                <p class="weui-grid__label">我的推荐</p>
                <%}
                    else
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
                <%} %>
            </a>
            <a href="javascript:alert('暂未开放');" class="weui-grid">
                <% if (Session["iden"].ToString() == "1")
                    { %>
                <div class="weui-grid__icon" style="width: 35.86px">
                    <img src="./images/profile.png" alt="">
                </div>
                <p class="weui-grid__label">我的信息</p>
                <%}
                    else
                    { %>
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
                <%} %>
            </a>
            <a href="/UserLogout.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/logout.png" alt="">
                </div>
                <p class="weui-grid__label">退出登录</p>
            </a>
            <a href="javascript:;" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
            </a>
            <a href="javascript:;" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
            </a>
            <a href="javascript:;" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/icon_tabbar.png" alt="">
                </div>
                <p class="weui-grid__label">Grid</p>
            </a>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script></script>

    </form>
</body>
</html>
