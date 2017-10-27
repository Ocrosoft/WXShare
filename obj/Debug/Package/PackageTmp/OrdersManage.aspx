<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrdersManage.aspx.cs" Inherits="WXShare.OrdersManage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width,initial-scale=1,user-scalable=0">
    <title>订单管理</title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <link rel="stylesheet" type="text/css" href="./css/example.css" />
</head>
<body>
    <form id="form1" runat="server">
        <!-- 标题 -->
        <div class="page__hd" style="padding-left: 15px; padding-bottom: 15px;">
            <h1 class="page__title" style="font-size: 25px; font-weight: bold;">订单管理</h1>
        </div>
        <div class="page__bd">
            <div class="weui-search-bar" id="searchBar">
                <div class="weui-search-bar__box" style="background: #fff; border-radius: 3px;">
                    <i class="weui-icon-search"></i>
                    <input runat="server" type="search" class="weui-search-bar__input" id="searchInput" placeholder="搜索" required="">
                    <a href="javascript:" class="weui-icon-clear" id="searchClear"></a>
                </div>
                <a runat="server" onserverclick="searchBtn_Click" class="weui-search-bar__cancel-btn" id="searchCancel" style="display: block; margin-left: 10px;">搜索</a>
            </div>
        </div>
        <div class="weui-cells">
            <div class="weui-cell weui-cell_select-after">
                <div class="weui-cell__hd">
                    <label for="" class="weui-label">订单状态</label>
                </div>
                <div class="weui-cell__bd">
                    <select id="statusSelect" runat="server" class="weui-select" name="statusSelect">
                        <option value="0" selected>所有订单</option>
                    </select>
                </div>
                <div class="weui-cell__ft">
                    <button runat="server" id="selectStatus" class="weui-vcode-btn" onserverclick="btn_Click">筛选</button>
                </div>
            </div>
        </div>
        <div class="weui-cells" runat="server" id="orderList">
        </div>

        <script src="./js/zepto.js"></script>
        <script type="text/javascript" src="./js/jweixin-1.00.js"></script>
        <script src="./js/weui.min.js"></script>

        <script>
            $('#searchClear').on('click', function () {
                $('#searchInput').val('');
            });
        </script>
    </form>
</body>
</html>
