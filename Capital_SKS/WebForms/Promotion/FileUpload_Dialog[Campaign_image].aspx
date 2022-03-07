<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FileUpload_Dialog[Campaign_image].aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.FileUpload_Dialog_Campaign_image_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>キャンペーン プロモーション画像</title>
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
        window.returnValue = document.getElementById(Name).value;
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
<body>
    
    
    <div id="PopWrapper">

<section>
	<h1>画像登録</h1>
	
	<div id="PopContents" class="pop1_Img inlineSet">
	<form id="Form2" runat="server">
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

	<div class="btn"><asp:Button ID="btnOK" runat="server" Text="決定" onclick="btnOK_Click" Width="150px" /></div>

	</form>
	</div>

</section>

</div><!--PpoWrapper-->

</body>

</html>




