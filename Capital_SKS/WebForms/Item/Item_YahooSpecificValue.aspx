<%@ Page Title="商品管理システム＜ヤフースペック値＞" Language="C#" AutoEventWireup="true" CodeBehind="Item_YahooSpecificValue.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_PopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<link rel="stylesheet" href="../../Styles/base.css" />
<link rel="stylesheet" href="../../Styles/common.css" />
<link rel="stylesheet" href="../../Styles/manager-style.css" />
<link rel="stylesheet" href="../../Styles/item.css" />
</head>
<body class="clNon">
<div id="PopWrapper">
<section>
	<h1>ヤフースペック値</h1>
	<div id="PopContents" class="pop5_Spec">
	<form runat="server">
	
		<table>
			<tbody>
			<tr>
				<th><asp:Label ID="Label2" runat="server" Text="プロダクトカテゴリ名" ToolTip="Product category name" /></th>
				<th><asp:Label ID="lblName" runat="server"  /></th>
			</tr>
			<tr>
				<td><asp:Label ID="lblSpec_Name1" runat="server"  /><asp:HiddenField runat="server" ID="hfSpec_ID1" /></td>
				<td>
					<asp:DropDownList ID="ddlSpec_ValueName1" runat="server" />
				</td>
			</tr>
			<tr>
				<td><asp:Label ID="lblSpec_Name2" runat="server" /><asp:HiddenField runat="server" ID="hfSpec_ID2" /></td>
				<td>
					<asp:DropDownList ID="ddlSpec_ValueName2" runat="server" />
				</td>
			</tr>
			<tr>
				<td><asp:Label ID="lblSpec_Name3" runat="server" /><asp:HiddenField runat="server" ID="hfSpec_ID3" /></td>
				<td>
					<asp:DropDownList ID="ddlSpec_ValueName3" runat="server" />
				</td>
			</tr>
			<tr>
				<td><asp:Label ID="lblSpec_Name4" runat="server" /><asp:HiddenField runat="server" ID="hfSpec_ID4" /></td>
				<td>
					<asp:DropDownList ID="ddlSpec_ValueName4" runat="server" />
				</td>
			</tr>
			<tr>
				<td><asp:Label ID="lblSpec_Name5" runat="server" /><asp:HiddenField runat="server" ID="hfSpec_ID5" /></td>
				<td>
					<asp:DropDownList ID="ddlSpec_ValueName5" runat="server" />
				</td>
			</tr>
			</tbody>
		</table>	
	<div class="btn"><asp:Button runat="server" ID="btnClose" Text="決定" onclick="btnClose_Click" Width="150px" />&nbsp;&nbsp;&nbsp;
	<asp:Button ID="btnCancel" runat="server" Text="キャンセル" onclick="btnCancel_Click" Width="150px" /></div>
	</form>
	</div>
</section>
</div><!--PpoWrapper-->
</body>

</html>
