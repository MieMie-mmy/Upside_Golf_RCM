<%@ Page Title="" Language="C#" MasterPageFile="" AutoEventWireup="true" CodeBehind="Jisha_Admin_Login.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha_Admin.Jisha_Admin_Login" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Login</title>

<meta http-equiv="X-UA-Compatible" content="IE=edge" />


	<link href="../../Styles/Jisha_base.css" rel="stylesheet" type="text/css" />
		<link href="../../Styles/Jisha_common.css" rel="stylesheet" type="text/css" />
		<link href="../../Styles/Jisha_style.css" rel="stylesheet" type="text/css" />



</head>
<body>
<div id="CmnWrapper">
<div id="CmnContents">
	<div id="ComBlock">
	
	<div  id="loginBox">
<form runat="server" >
	<p>ログイン</p>
	<div>
		<dl>
			<dt>ID <span>※必須</span></dt>
			<dd>
                <asp:TextBox ID="txtid" runat="server"></asp:TextBox>
                </dd>
		</dl>	
		<dl>
			<dt>パスワード <span>※必須</span></dt>
			<dd>
                <asp:TextBox ID="txtpassword" runat="server" TextMode="Password"></asp:TextBox></dd>
		</dl>

		<p>
            <asp:Button ID="btnlogin" runat="server" Text="ログイン" onclick="btnlogin_Click" 
                Width="104px" />
        </p>
		
	</div>
	</form>

	</div><!--inlineSet-->

</div><!--ComBlock-->
</div><!--CmnContents-->


</div><!--CmnWrapper-->
   
</body>
</html>
