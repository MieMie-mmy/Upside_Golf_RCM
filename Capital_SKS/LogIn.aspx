<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogIn.aspx.cs" Inherits="ORS_RCM.LogIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>Login</title>
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<link rel="icon" href="images/logo.jpg">
<link href="Styles/base.css" rel="stylesheet" type="text/css" />
<link href="Styles/common.css" rel="stylesheet" type="text/css" />
<link href="Styles/manager-style.css" rel="stylesheet" type="text/css" />
<style type="text/css">
form input,form select,form textarea {
	border: 1px solid #b5b5b5;
	background: #fff;
	padding: 1px 4px;
	border-radius: 4px;
	font-size: 12px;
	box-sizing: border-box; 
	width:189px;
}
form input[type="button"],form input[type="submit"] {
	height: 25px;
	border-radius: 2px;
	background: #f2f2f2;
	background: -moz-linear-gradient(top,  #f2f2f2 0%, #e8e8e8 100%);
	background: -webkit-gradient(linear, left top, left bottom, color-stop(0%,#f2f2f2), color-stop(100%,#e8e8e8));
	background: -webkit-linear-gradient(top,  #f2f2f2 0%,#e8e8e8 100%);
	background: -o-linear-gradient(top,  #f2f2f2 0%,#e8e8e8 100%);
	background: -ms-linear-gradient(top,  #f2f2f2 0%,#e8e8e8 100%);
	background: linear-gradient(to bottom,  #f2f2f2 0%,#e8e8e8 100%);
	filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#f2f2f2', endColorstr='#e8e8e8',GradientType=0 );
	width: 100px;
	margin-left: auto !important;
}
</style>
</head>
<body>
<div id="CmnWrapper">
    <div id="CmnContents">
	<div id="ComBlock">
 	<div  id="loginBox">
	<form  runat="server">
	<p>ログイン</p>
	<div>
    <ul>
       <dl><img src="~/images/logo.jpg" class="imgLogo" alt="テスト用" runat="server"/></dl>
		<dl>
			<dt>ID <span>※必須</span></dt>
			<dd><input  id="username" class="login" type="text" runat="server" autocomplete="off" /></dd>
		</dl>	
		<dl>
			<dt>パスワード <span>※必須</span></dt>
			<dd><input class="login" id="password" type="password" runat="server" autocomplete="off"/></dd>
		</dl>
		<p><asp:Button runat="server" ID="btnlogin" OnClick="btnlogin_Click" Text="ログイン" /></p>
        </ul>
	</div>
    <div>
    </div>
</form>
</div><!--loginBox-->
</div>
</div>
</div>
</body>
</html>
