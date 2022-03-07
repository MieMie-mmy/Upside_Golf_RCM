<%@ Page Title="商品管理システム＜オプションデータインポート＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Yahoo_SpecName_Import.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Yahoo_SpecName_Import" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

<link rel="stylesheet" href="../../Styles/base.css"/>
<link rel="stylesheet" href="../../Styles/common.css"/>
<link rel="stylesheet" href="../../Styles/manager-style.css"/>
<link rel="stylesheet" href="../../Styles/database.css"/>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<script src="../../Scripts/jquery.page-scroller.js"></script>

<title>商品管理システム＜オプションデータインポート＞</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconImport">

	<h1>オプションデータインポート</h1>

<!-- ★ -->	
		<div class="dbCmnSet iptList itemCatIpt inlineSet">
			<form action="#" method="get">
				<dl>
					<dt>オプションデータ</dt>
					<dd><asp:FileUpload ID="uplYahooSpec" runat="server" /></dd>
				</dl>
				<p><asp:Button ID="btnRead" Text="Read" runat="server" onclick="btnRead_Click" /><asp:Button ID="btnImport" Text="インポート開始" runat="server" onclick="btnImport_Click"/></p>
			</form>
            </div>
            <asp:GridView ID="gvYahooSpec" runat="server" AllowPaging="true" 
            onpageindexchanging="gvYahooSpec_PageIndexChanging"></asp:GridView>	
	
<!-- /★ -->	

	</div><!--setListBox-->

	</div><!--ComBlock-->
</div><!--CmnContents-->

</asp:Content>
