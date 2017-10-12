<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserMain.aspx.cs" Inherits="WXShare.UserMain" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link rel="stylesheet" type="text/css" href="./css/weui.min.css" />
    <style>
        #header{
               width: 100%;
               padding-top: 15%;
               padding-bottom: 15%;
               background: black;
               text-align: center;
        }
        #header-img{
            width: 80%;
        }
        #main{
            text-align: center;
        }
        .item{
            width: 30%;
            height: 30%;
            
        }
        .right{
            border-right: 1px dashed black;
        }
        .bottom{
            border-bottom: 1px dashed black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="header">
                <img id="header-img" src="./logo.png" />
            </div>
            <div id="main">
                <div class="item right bottom">

                </div>
                <div class="item right bottom">

                </div>
                <div class="item bottom">

                </div>
                <div class="item right bottom">

                </div>
                <div class="item right bottom">

                </div>
                <div class="item bottom">

                </div>
                <div class="item right">

                </div>
                <div class="item right">

                </div>
                <div class="item ">

                </div>
            </div>
        </div>
    </form>
</body>
</html>
