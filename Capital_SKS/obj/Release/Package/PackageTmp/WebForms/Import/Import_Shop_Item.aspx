<%@ Page Title="商品管理システム＜商品マスタデータインポート＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_Shop_Item.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Shop_Import" %>
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

<title>商品管理システム＜商品マスタデータインポート＞</title>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

    <div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconImport">

	<h1>商品マスタデータインポート</h1>
    <p class="attText">下記内容で間違いなければ「更新」ボタンを押してください</p>
    <div align="center">
    <asp:ListBox runat="server" ID="lbShop" SelectionMode="Multiple" Width="300px" Height="100px"></asp:ListBox>
    </div>
<!-- Link List -->	
		<div class="dbCmnSet iptList inlineSet">
			<form action="#" method="get">
				<dl>
					<dt><asp:Label ID="Label1" runat="server" Text="ショップ商品データ"></asp:Label></dt>
					<dd><asp:FileUpload runat="server" ID="uplShopItem"/></dd>
					<dt><asp:Label ID="Label2" runat="server" Text="ショップカテゴリ"></asp:Label></dt>
					<dd><asp:FileUpload runat="server" ID="uplShopCategory"/></dd>
					<dt><asp:Label ID="Label3" Text="ショップ在庫データ" runat="server"></asp:Label></dt>
					<dd><asp:FileUpload runat="server" ID="uplShopInvertory"/></dd>
				</dl>
				<p><asp:Button ID="btnRead" runat="server" Text="Read" onclick="btnRead_Click" /><asp:Button runat="server" Text="インポート開始" ID="btnImport" onclick="btnImport_Click" /></p>
			</form>
	</div>
<!-- /Link List -->	
	</div><!--setListBox-->
	</div><!--ComBlock-->
</div><!--CmnContents-->


<div id="CmnWrapper">
<div id="CmnContents">
	<div id="ComBlock">
		<div class="setListBox iconSet iconImport">
<div class="setDetailBox iconSet iconCheck editBox">
<div class="dbCmnSet editBox">
<h2>ショップ商品データ</h2>
</div>	
</div>
</div></div></div></div>

<div>
<div style="overflow-x: auto; overflow-y: hidden;">
<asp:GridView runat="server" CssClass="listTable" ID="gvShopItem" 
		AutoGenerateColumns="false" onrowdatabound="gvShopItem_RowDataBound">
	<Columns>
		<asp:BoundField DataField="コントロールカラム" HeaderText="コントロールカラム" />
		<asp:TemplateField>
			<HeaderTemplate>
				<asp:Label runat="server" Text="商品番号" ID="lblHdItemCode"></asp:Label>
			</HeaderTemplate>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%#Eval("商品番号") %>' ID="lblItemCode"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
				<asp:TemplateField>
			<HeaderTemplate>
				<asp:Label runat="server" Text="商品名" ID="lblHdItemName"></asp:Label>
			</HeaderTemplate>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%#Eval("商品名") %>' ID="lblItemName"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<HeaderTemplate>
				<asp:Label runat="server" Text="" ID="lblHDItemAdminCode"></asp:Label>
			</HeaderTemplate>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%#Eval("商品管理番号（商品URL）") %>' ID="lblItemAdminCode"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
			<HeaderTemplate>
				<asp:Label runat="server" Text="ポイント変倍率" ID="lblHdPoint"></asp:Label>
			</HeaderTemplate>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%#Eval("ポイント変倍率") %>' ID="lblPoint"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
				<asp:TemplateField>
			<HeaderTemplate>
				<asp:Label runat="server" Text="ポイント変倍率適用期間" ID="lblHdPointTerm"></asp:Label>
			</HeaderTemplate>
			<ItemTemplate>
				<asp:Label runat="server" Text='<%#Eval("ポイント変倍率適用期間") %>' ID="lblPointTerm"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
	</Columns>
</asp:GridView>
</div>



<div id="CmnWrapper">
<div id="CmnContents">
	<div id="ComBlock">
		<div class="setListBox iconSet iconImport">
<div class="setDetailBox iconSet iconCheck editBox">
<div class="dbCmnSet editBox">
<h2>ショップカテゴリデータ</h2>
</div>	
</div>
</div></div></div></div>

<div style="overflow-x: auto; overflow-y: hidden;">
<asp:GridView runat="server" CssClass="listTable" ID="gvShopCategory" AutoGenerateColumns="false">
	<Columns>
		<asp:BoundField DataField="コントロールカラム" HeaderText="コントロールカラム" />
		<asp:BoundField DataField="商品管理番号（商品URL）" HeaderText="商品管理番号（商品URL）" />
		<asp:BoundField DataField="カテゴリセット管理番号" HeaderText="カテゴリセット管理番号" />
		<asp:BoundField DataField="カテゴリセット名" HeaderText="カテゴリセット名" />
	</Columns>
</asp:GridView>
</div>

<div id="CmnWrapper">
<div id="CmnContents">
	<div id="ComBlock">
		<div class="setListBox iconSet iconImport">
<div class="setDetailBox iconSet iconCheck editBox">
<div class="dbCmnSet editBox">
<h2>ショップ在庫データ</h2>
</div>	
</div>
</div></div></div></div>

<div style="overflow-x: auto; overflow-y: hidden;">
<asp:GridView runat="server" CssClass="listTable" ID="gvShopInventory" AutoGenerateColumns="false">
	<Columns>
		<asp:BoundField DataField="項目選択肢用コントロールカラム" HeaderText="項目選択肢用コントロールカラム" />
		<asp:BoundField DataField="商品管理番号（商品URL）" HeaderText="商品管理番号（商品URL）" />
		<asp:BoundField DataField="項目選択肢別在庫用在庫数" HeaderText="項目選択肢別在庫用在庫数" />
	</Columns>
</asp:GridView>
</div>
</div>
</asp:Content>

