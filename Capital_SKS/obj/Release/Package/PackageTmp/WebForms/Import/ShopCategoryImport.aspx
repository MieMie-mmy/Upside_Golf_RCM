<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShopCategoryImport.aspx.cs" Inherits="ORS_RCM.ShopCategoryImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<title>商品管理システム＜ショップカテゴリCSVデータインポート＞</title>
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/shop_category.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
	<div id="CmnContents">
 	<div id="ComBlock">
	<div class="setDetailBox mallCate iconSet iconCheck editBox">
	<h1>ショップカテゴリCSVデータインポート 確認</h1>
	<p class="attText">下記内容で間違いなければ「更新」ボタンを押してください</p>
	


<%--	<asp:Label runat="server" Text="下記内容で間違いなければ「更新する」ボタンを押してください。" CssClass="middlepage" ID="lblNotice"></asp:Label>--%>
<div style="overflow-x: auto; overflow-y: hidden;">
	<asp:GridView ID="gvShow" runat="server" AutoGenerateColumns="False" CssClass="listTable" 
		EmptyDataText="No data to display" ShowHeaderWhenEmpty="True">
		<Columns>
			<asp:BoundField DataField="コントロールカラム" HeaderText="コントロールカラム" />
			<asp:BoundField DataField="カテゴリID" HeaderText="カテゴリID" />
			<asp:BoundField DataField="パス名" HeaderText="パス名" />
			<asp:BoundField DataField="親カテゴリID" HeaderText="親カテゴリID" />
		</Columns>
	</asp:GridView>
	</div>
	<br />
	<div style="padding-left:150px">
	<asp:Button ID="btnUpdate" runat="server" Text="更新する" 
		onclick="btnUpdate_Click" />
	</div>
    
    </div>
    </div>
</asp:Content>
