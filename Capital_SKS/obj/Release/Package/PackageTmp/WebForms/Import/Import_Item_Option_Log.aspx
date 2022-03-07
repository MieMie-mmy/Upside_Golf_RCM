<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_Item_Option_Log.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Import_Item_Option_Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
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

	<h1>オプションデータインポートログ</h1>

	<div class="dbCmnSet editBox logDb">
<!-- ItemCategory Master -->
		<h2>オプションデータ</h2>
       
		<asp:GridView ID="gvOptionData" runat="server" AutoGenerateColumns="False" CssClass="listTable itemCatIpt itemCatLog" 
		EmptyDataText="No data to display" ShowHeaderWhenEmpty="True" 
            onrowdatabound="gvOptionData_RowDataBound" AllowPaging="True" 
            PageSize="30" onpageindexchanging="gvOptionData_PageIndexChanging">
		<Columns>
			<asp:BoundField DataField="チェック" HeaderText="チェック" />
			<%--<asp:BoundField DataField="コントロールフラグ" HeaderText="コントロールフラグ" />--%>
            <asp:TemplateField>
                <HeaderTemplate>コントロール<br />フラグ</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" ID="lblControlID" Text='<%# Eval("Ctrl_ID") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
			<asp:BoundField DataField="Item_Code" HeaderText="   商品番号   " />
			<asp:BoundField DataField="Option_Name" HeaderText="項目名" />
<%--            <asp:TemplateField>
                <HeaderTemplate>項目名</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="text-align:center" ID="lblControlID" Text='<%# Eval("項目名") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>--%>
			<asp:BoundField DataField="Option_Value" HeaderText="選択肢" />			
            <asp:BoundField DataField="ErrMsg" HeaderText="エラー内容" />
		</Columns>
	</asp:GridView>
<!-- /ItemCategory Master -->

	</div>
<!-- /CategoryID list -->
	</div><!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
