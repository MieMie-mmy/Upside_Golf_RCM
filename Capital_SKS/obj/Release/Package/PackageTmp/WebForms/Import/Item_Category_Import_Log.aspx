<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Category_Import_Log.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Item_Category_Import_Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox iconSet iconLog editBox">

	<h1>商品カテゴリデータインポートログ</h1>

	<div class="dbCmnSet editBox logDb">
<!-- ItemCategory Master -->
		<h2>商品カテゴリデータ</h2>
				<asp:GridView runat="server" CssClass="listTable itemCatIpt itemCatLog" ID="gvCategoryData" 
		AutoGenerateColumns="False" onrowdatabound="gvCategoryData_RowDataBound" 
            AllowPaging="True" onpageindexchanging="gvCategoryData_PageIndexChanging" PageSize = "30">
	<Columns>
		<asp:BoundField DataField="チェック" HeaderText="チェック">
		<ControlStyle Width="100px" ForeColor="Red" />
		<HeaderStyle Width="100px" />
		<ItemStyle Width="100px" ForeColor="Red" />
		</asp:BoundField>
		<asp:TemplateField>
			<ControlStyle Width="150px" />
			<HeaderStyle Width="150px" />
			<ItemStyle Width="150px" />
			<HeaderTemplate>コントロールフラグ</HeaderTemplate>
			<ItemTemplate>
				<asp:Label runat="server" ID="lblCtrlID" Text='<%# Eval("Ctrl_ID") %>' ></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		<%--<asp:BoundField DataField="コントロールフラグ" HeaderText="コントロールフラグ" />--%>
		<asp:BoundField DataField="Item_Code" HeaderText="商品番号" >
			<ControlStyle Width="130px" />
			<HeaderStyle Width="130px" />
			<ItemStyle Width="130px" />
		</asp:BoundField>
		<asp:BoundField DataField="CategoryID" HeaderText="商品カテゴリID">
			<ControlStyle Width="130px" />
			<HeaderStyle Width="130px" />
			<ItemStyle Width="130px" />
		</asp:BoundField>
		<%--<asp:BoundField DataField="商品カテゴリ名" HeaderText="商品カテゴリ名" />--%>
		<asp:TemplateField>
			<HeaderTemplate>商品カテゴリ</HeaderTemplate>
			<ItemTemplate>
				<p><asp:Label  runat="server" ID="lblCategoryName" Text='<%# Eval("CategoryName") %>' ></asp:Label></p>
			</ItemTemplate>
		</asp:TemplateField>
		<asp:BoundField DataField="ErrMsg" HeaderText="エラー内容" />
		<asp:BoundField DataField="Type" Visible="false" />
	</Columns>
</asp:GridView>
<!-- /ItemCategory Master -->

	</div>
<!-- /CategoryID list -->
	</div><!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
