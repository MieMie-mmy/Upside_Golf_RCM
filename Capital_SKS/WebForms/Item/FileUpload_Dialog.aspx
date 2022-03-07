<%@ Page Title="商品管理システム＜画像登録＞" Language="C#" AutoEventWireup="true" CodeBehind="FileUpload_Dialog.aspx.cs" Inherits="ORS_RCM.FileUpload_Dialog" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>商品管理システム＜画像登録＞</title>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

<link rel="stylesheet" href="../../Styles/base.css" />
<link rel="stylesheet" href="../../Styles/common.css" />
<link rel="stylesheet" href="../../Styles/manager-style.css" />
<link rel="stylesheet" href="../../Styles/item.css" />

<script src="../../Scripts/jquery.droppy.js" type="text/javascript"></script>  
<script src="../../Scripts/jquery.page-scroller.js"  type="text/javascript"></script>  


<script type = "text/javascript">
function CloseWindow() {
var Name = '<%= hfFileName.ClientID %>';

//close the dialog window
if (window.opener) {
window.opener.returnValue = document.getElementById(Name).value;
}
window.returnValue = document.getElementById(Name).value ;
window.close();
}
</script>

<style type="text/css">
#btnAdd
{
width: 60px;
}
</style>
</head>
<body class="clNon">
<div id="PopWrapper">

<section>
	<h1>画像登録</h1>
	
	<div id="PopContents" class="pop1_Img inlineSet">
	<form runat="server">
    <asp:HiddenField ID="hfFileName" Value="" runat="server" />
		<dl>
			<dt>画像１</dt><dd><asp:FileUpload ID="FileUpload1" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName1" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label1" runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete1" runat="server" Text="削除" Visible="false" onclick="btnDelete1_Click"  /></dd>
		</dl>
		<dl>
			<dt>画像２</dt><dd><asp:FileUpload ID="FileUpload2" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName2" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label2" runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete2" runat="server" Text="削除" Visible="false" onclick="btnDelete2_Click"  /></dd>
		</dl>
		<dl>
			<dt>画像３</dt><dd><asp:FileUpload ID="FileUpload3" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName3" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label3" runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete3" runat="server" Text="削除" Visible="false" onclick="btnDelete3_Click"  /></dd>
		</dl>
		<dl>
			<dt>画像４</dt><dd><asp:FileUpload ID="FileUpload4" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName4" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label4" runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete4" runat="server" Text="削除" Visible="false" onclick="btnDelete4_Click"  /></dd>
		</dl>
		<dl>
			<dt>画像５</dt><dd><asp:FileUpload ID="FileUpload5" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName5" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label5"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete5" runat="server" Text="削除" Visible="false" onclick="btnDelete5_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像6</dt><dd><asp:FileUpload ID="FileUpload6" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName6" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label6"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete6" runat="server" Text="削除" Visible="false" onclick="btnDelete6_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像7</dt><dd><asp:FileUpload ID="FileUpload7" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName7" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label7"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete7" runat="server" Text="削除" Visible="false" onclick="btnDelete7_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像8</dt><dd><asp:FileUpload ID="FileUpload8" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName8" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label8"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete8" runat="server" Text="削除" Visible="false" onclick="btnDelete8_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像9</dt><dd><asp:FileUpload ID="FileUpload9" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName9" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label9"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete9" runat="server" Text="削除" Visible="false" onclick="btnDelete9_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像10</dt><dd><asp:FileUpload ID="FileUpload10" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName10" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label10"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete10" runat="server" Text="削除" Visible="false" onclick="btnDelete10_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像11</dt><dd><asp:FileUpload ID="FileUpload11" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName11" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label11"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete11" runat="server" Text="削除" Visible="false" onclick="btnDelete11_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像12</dt><dd><asp:FileUpload ID="FileUpload12" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName12" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label12"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete12" runat="server" Text="削除" Visible="false" onclick="btnDelete12_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像13</dt><dd><asp:FileUpload ID="FileUpload13" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName13" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label13"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete13" runat="server" Text="削除" Visible="false" onclick="btnDelete13_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像14</dt><dd><asp:FileUpload ID="FileUpload14" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName14" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label14"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete14" runat="server" Text="削除" Visible="false" onclick="btnDelete14_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像15</dt><dd><asp:FileUpload ID="FileUpload15" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName15" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label15"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete15" runat="server" Text="削除" Visible="false" onclick="btnDelete15_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像16</dt><dd><asp:FileUpload ID="FileUpload16" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName16" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label16"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete16" runat="server" Text="削除" Visible="false" onclick="btnDelete16_Click"  /></dd>
        </dl>
        <dl>
			<dt>画像17</dt><dd><asp:FileUpload ID="FileUpload17" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName17" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label17"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete17" runat="server" Text="削除" Visible="false" onclick="btnDelete17_Click"  /></dd>
        </dl>
        <dl>
			<dt>画像18</dt><dd><asp:FileUpload ID="FileUpload18" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName18" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label18"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete18" runat="server" Text="削除" Visible="false" onclick="btnDelete18_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像19</dt><dd><asp:FileUpload ID="FileUpload19" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName19" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label19"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete19" runat="server" Text="削除" Visible="false" onclick="btnDelete19_Click"  /></dd>
		</dl>
        <dl>
			<dt>画像20</dt><dd><asp:FileUpload ID="FileUpload20" runat="server" /></dd>
            <dd><asp:Label ID="lblFileName20" Visible="false" runat="server" Text="ファイルが選択されていません"></asp:Label></dd>
            <dd><asp:Label ID="Label20"  runat="server" Text="先に登録されていた場合"/><asp:Button ID="btnDelete20" runat="server" Text="削除" Visible="false" onclick="btnDelete20_Click"  /></dd>
		</dl>
		
	<div class="btn"><asp:Button ID="btnOK" runat="server" Text="決定" onclick="btnOK_Click" Width="150px" /></div>

	</form>
	</div>

</section>

</div><!--PpoWrapper-->

</body>

</html>
