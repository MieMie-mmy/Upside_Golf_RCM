<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Category_Import.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Item_Category_Import_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />

    <title>商品管理システム＜商品カテゴリデータインポート＞</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconImport">

	<h1>商品カテゴリデータインポート</h1>

<!-- ★ -->	
		<div class="dbCmnSet iptList itemCatIpt inlineSet">
			<form action="#" method="get">
				<dl>
					<dt>商品カテゴリデータ</dt>
					<dd><asp:FileUpload ID="uplCategory" runat="server" /></dd>
				</dl>
				<p><asp:Button runat="server" ID="btnCategoryImport" Text="インポート開始" 
                        onclick="btnCategoryImport_Click" /></p>
			</form>
	</div>
<!-- /★ -->	

	</div><!--setListBox-->

	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
