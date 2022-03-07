<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Option_Import_Confirm.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Item_Option_Import_Confirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />
    <title>商品管理システム＜オプションデータインポート確認＞</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox iconSet iconCheck editBox">

	<h1>オプションデータインポート 確認</h1>
	<p class="attText">下記内容で間違いなければ「更新」ボタンを押してください</p>

	<div class="dbCmnSet editBox">
	<form action="#" method="get">
<!-- ItemCategory Master -->
		<h2>オプションデータ</h2>
		<asp:GridView ID="gvOptionData" runat="server" AutoGenerateColumns="False" CssClass="listTable itemCatIpt" 
		EmptyDataText="No data to display" ShowHeaderWhenEmpty="True" 
        onrowdatabound="gvOptionData_RowDataBound" AllowPaging="True" 
        PageSize="30" onpageindexchanging="gvOptionData_PageIndexChanging">
		<Columns>
			<asp:BoundField DataField="チェック" HeaderText="チェック" />
			<%--<asp:BoundField DataField="コントロールフラグ" HeaderText="コントロールフラグ" />--%>
            <asp:TemplateField>
                <HeaderTemplate>コントロール<br />フラグ</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" ID="lblControlID" Text='<%# Eval("コントロールフラグ") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
			<asp:BoundField DataField="商品番号" HeaderText="   商品番号   " />
			<asp:BoundField DataField="項目名" HeaderText="項目名" />
<%--            <asp:TemplateField>
                <HeaderTemplate>項目名</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="text-align:center" ID="lblControlID" Text='<%# Eval("項目名") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>--%>            
			<asp:BoundField DataField="選択肢" HeaderText="選択肢" />			
			<asp:BoundField DataField="Type" Visible="false" />
            <asp:BoundField Visible="false" DataField="エラー内容" HeaderText="エラー内容" />
		</Columns>
	</asp:GridView>
<!-- /ItemCategory Master -->

		<div class="btn"><asp:Button runat="server" ID="btnUpdate" Text="更 新" 
                onclick="btnUpdate_Click" /></div>
	</form>
	</div>
<!-- /CategoryID list -->
	</div><!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
