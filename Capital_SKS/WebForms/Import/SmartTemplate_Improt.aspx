<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SmartTemplate_Improt.aspx.cs" Inherits="ORS_RCM.WebForms.Import.SmartTemplate_Improt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />
	<title>商品説明文データインポート</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconImport">

	<h1>商品説明文データインポート</h1>

<!-- Link List -->	
		<div class="dbCmnSet iptList itemCatIpt inlineSet">
			<form action="#" method="get">
				<dl>
					<dt>商品説明文データ</dt>
					<dd><asp:FileUpload runat="server" ID="uplSmartTemplate"/></dd>
				</dl>
				<p><asp:Button runat="server" Text="インポート開始" ID="btnImport" 
						onclick="btnImport_Click"/></p>
			</form>
	</div>
<!-- /Link List -->	

	</div><!--setListBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
