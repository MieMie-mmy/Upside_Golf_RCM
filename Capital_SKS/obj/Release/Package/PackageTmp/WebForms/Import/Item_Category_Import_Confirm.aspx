<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Category_Import_Confirm.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Item_Category_Import_Confirm" %>
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
	<div class="setDetailBox iconSet iconCheck editBox">

	<h1>商品マスタデータインポート 確認</h1>
	<p class="attText">下記内容で間違いなければ「更新」ボタンを押してください</p>

	<div class="dbCmnSet editBox">
	<form action="#" method="get">
<!-- ItemCategory Master -->
		<h2>商品カテゴリデータ</h2>
		<asp:GridView runat="server" CssClass="listTable itemCatIpt" ID="gvCategoryData" 
		AutoGenerateColumns="False" onrowdatabound="gvCategoryData_RowDataBound" 
        AllowPaging="True" PageSize="30" 
        onpageindexchanging="gvCategoryData_PageIndexChanging">
	<Columns>
		<asp:BoundField DataField="チェック" HeaderText="チェック">
        <ControlStyle Width="100px" BackColor="#FF6600" ForeColor="#FF3300" />
        <HeaderStyle Width="100px" />
        <ItemStyle Width="100px" ForeColor="#FF3300" />
        </asp:BoundField>
        <asp:TemplateField>
            <ControlStyle Width="150px" />
            <HeaderStyle Width="150px" />
            <ItemStyle Width="150px" />
            <HeaderTemplate>コントロール<br />フラグ</HeaderTemplate>
            <ItemTemplate>
                <asp:Label runat="server" ID="lblCtrlID" Text='<%# Eval("コントロールフラグ") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
		<%--<asp:BoundField DataField="コントロールフラグ" HeaderText="コントロールフラグ" />--%>
		<asp:BoundField DataField="商品番号" HeaderText="商品番号" >
            <ControlStyle Width="130px" />
            <HeaderStyle Width="130px" />
            <ItemStyle Width="130px" />
        </asp:BoundField>
		<asp:BoundField DataField="商品カテゴリID" HeaderText="商品カテゴリID">
            <ControlStyle Width="130px" />
            <HeaderStyle Width="130px" />
            <ItemStyle Width="130px" />
        </asp:BoundField>
		<%--<asp:BoundField DataField="商品カテゴリ名" HeaderText="商品カテゴリ名" />--%>
        <asp:TemplateField>
            <HeaderTemplate>商品カテゴリ</HeaderTemplate>
            <ItemTemplate>
                <p><asp:Label  runat="server" ID="lblCategoryName" Text='<%# Eval("商品カテゴリ名") %>' ></asp:Label></p>
            </ItemTemplate>
        </asp:TemplateField>
		<%--<asp:BoundField DataField="エラー内容" HeaderText="エラー内容" />--%>
		<asp:BoundField DataField="Type" Visible="false" />
	</Columns>
</asp:GridView>
<!-- /ItemCategory Master -->

		<div class="btn"><asp:Button runat="server" ID="btnUpdate" Text="更 新" 
                onclick="btnUpdate_Click" /> </div>
	</form>
	</div>
<!-- /CategoryID list -->
	</div><!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
