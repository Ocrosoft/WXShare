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
            <%
                if (Session["iden"].ToString() == "1")
                {
            %>
            <!-- 会员S -->
            <a href="/Activity.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/activity.png" alt="">
                </div>
                <p class="weui-grid__label">当前活动</p>
            </a>
            <a href="#" class="weui-grid">
                <div class="weui-grid__icon" style="width: 35.86px">
                    <img src="./images/share.png" alt="">
                </div>
                <p class="weui-grid__label">我的推荐</p>
            </a>
            <a href="#" class="weui-grid">
                <div class="weui-grid__icon" style="width: 35.86px">
                    <img src="./images/profile.png" alt="">
                </div>
                <p class="weui-grid__label">我的信息</p>
            </a>
            <!-- 会员E -->
            <%
                }
                else if (Session["iden"].ToString() == "2")
                {
            %>
            <!-- 业务员S -->
            <a href="/OrdersForYWY.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <span runat="server" id="newOrderCountYWY" class="weui-badge" style="position: absolute; display: none;"></span>
                    <img src="./images/orders.png" alt="">
                </div>
                <p class="weui-grid__label">客户订单</p>
            </a>
            <a href="#" class="weui-grid">
                <div class="weui-grid__icon" style="width: 35.86px">
                    <img src="./images/profile.png" alt="">
                </div>
                <p class="weui-grid__label">我的信息</p>
            </a>
            <!-- 业务员E -->
            <%
                }
                else if (Session["iden"].ToString() == "3")
                {
            %>
            <!-- 经销商S -->
            <!-- 经销商E -->
            <%
                }
                else if (Session["iden"].ToString() == "4")
                {
            %>
            <!-- 施工队S -->
            <a href="/OrdersForSGD.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <span runat="server" id="newOrderCountSGD" class="weui-badge" style="position: absolute; display: none;"></span>
                    <img src="./images/orders.png" alt="">
                </div>
                <p class="weui-grid__label">施工订单</p>
            </a>
            <a href="#" class="weui-grid">
                <div class="weui-grid__icon" style="width: 35.86px">
                    <img src="./images/profile.png" alt="">
                </div>
                <p class="weui-grid__label">我的信息</p>
            </a>
            <!-- 施工队E -->
            <%
                }
                else if (Session["iden"].ToString() == "5")
                {
            %>
            <!-- 管理员S -->
            <a href="/ActivityAll.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/activity.png" alt="">
                </div>
                <p class="weui-grid__label">活动管理</p>
            </a>
            <a href="/ActivitySignView.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <span runat="server" id="newActivitySign" class="weui-badge" style="position: absolute; display: none;"></span>
                    <img src="./images/signup.png" alt="">
                </div>
                <p class="weui-grid__label">报名管理</p>
            </a>
            <a href="/UserRegisterAuth.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <span runat="server" id="newRegister" class="weui-badge" style="position: absolute; display: none;"></span>
                    <img src="./images/checkout.png" alt="">
                </div>
                <p class="weui-grid__label">注册审核</p>
            </a>
            <a href="/OrdersManage.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/orders.png" alt="">
                </div>
                <p class="weui-grid__label">订单管理</p>
            </a>
            <a href="/StaffManage.aspx" class="weui-grid">
                <div class="weui-grid__icon" style="width: 55px">
                    <img src="./images/staff.png" alt="">
                </div>
                <p class="weui-grid__label">员工管理</p>
            </a>
            <a href="#" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/control.png" alt="">
                </div>
                <p class="weui-grid__label">系统设置</p>
            </a>
            <!-- 管理员E -->
            <%
                }
            %>
            <a href="/UserLogout.aspx" class="weui-grid">
                <div class="weui-grid__icon">
                    <img src="./images/logout.png" alt="">
                </div>
                <p class="weui-grid__label">退出登录</p>
            </a>
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script></script>

    </form>
</body>
</html>
