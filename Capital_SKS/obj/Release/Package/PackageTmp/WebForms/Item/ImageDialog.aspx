<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImageDialog.aspx.cs" Inherits="ORS_RCM.WebForms.Item.ImageDialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<link rel="stylesheet" href="../../Styles/base.css" />
	<link rel="stylesheet" href="../../Styles/common.css" /> 
	<link rel="stylesheet" href="../../Styles/manager-style.css" />
	<link rel="stylesheet" href="../../Styles/item.css" />
	<title></title>
	<script type = "text/javascript">
		function CloseWindow() {
			//close the dialog window
		    window.opener.__doPostBack();
		    window.close();
		}
	</script>
</head>
<body class="clNon">
	<div id="PopWrapper">
	<section>
		<h1>画像登録</h1>
		<div id="PopContents" class="pop1_Img inlineSet">
		<form id="form1" runat="server">

			<dl>
				<dt>画像１</dt>
				<dd><asp:FileUpload ID="upl1" runat="server" /></dd>
				<dd><asp:Label ID="lblImage1" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
				<dd><asp:Button ID="btnDelete1" Visible="false" runat="server" Text="削除" 
                        onclick="btnDelete1_Click" /></dd>
				</dl>
			<dl>
				<dt>画像２</dt>
				<dd><asp:FileUpload ID="upl2" runat="server" /></dd>
				<dd><asp:Label ID="lblImage2" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
				<dd><asp:Button ID="btnDelete2" Visible="false" runat="server" Text="削除" 
                        onclick="btnDelete2_Click" /></dd>
			</dl>
			<dl>
				<dt>画像３</dt>
				<dd><asp:FileUpload ID="upl3" runat="server" /></dd>
				<dd><asp:Label ID="lblImage3" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
				<dd><asp:Button ID="btnDelete3" Visible="false" runat="server" Text="削除" 
                        onclick="btnDelete3_Click" /></dd>
			</dl>
			<dl>
				<dt>画像４</dt>
				<dd><asp:FileUpload ID="upl4" runat="server" /></dd>
				<dd><asp:Label ID="lblImage4" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
				<dd><asp:Button ID="btnDelete4" Visible="false" runat="server" Text="削除" 
                        onclick="btnDelete4_Click" /></dd>
			</dl>
			<dl>
				<dt>画像５</dt>
				<dd><asp:FileUpload ID="upl5" runat="server" /></dd>
				<dd><asp:Label ID="lblImage5" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
				<dd><asp:Button ID="btnDelete5" Visible="false" runat="server" Text="削除" 
                        onclick="btnDelete5_Click" /></dd>
			</dl>
			<div class="btn"><asp:Button runat="server" ID="btnSave" OnClick="btnSave_Click" Width="150px" Text="決定" /></div>
		</form>
		</div>
	</section>
	</div>
</body>
</html>
