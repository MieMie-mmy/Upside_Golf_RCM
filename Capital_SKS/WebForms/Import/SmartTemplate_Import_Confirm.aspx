<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SmartTemplate_Import_Confirm.aspx.cs" Inherits="ORS_RCM.WebForms.Import.SmartTemplate_Import_Confirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />
    <style type="text/css" headerstyle-cssclass="Freezing">
　　.Freezing
　　{
　　position:relative;
　　top:expression(this.offsetParent.scrollTop);
　　z-index:10;
　　}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox iconSet iconCheck editBox">

	<h1>商品説明文データインポート 確認</h1>
	<p class="attText">下記内容で間違いなければ「更新」ボタンを押してください</p>
        <asp:HiddenField ID="hflog" runat="server" />
	<div class="dbCmnSet editBox">
	<form action="#" method="get">
<!-- ItemCategory Master -->
		<h2>商品説明文データ</h2>
        <div class="listTableOver onOver">
		<asp:GridView runat="server" CssClass="listTable itemCatIpt" ID="gvSmartTemplate"  onpageindexchanging="gvSmartTemplate_PageIndexChanging" AllowPaging="true"
        PageSize="30">
</asp:GridView>

<!-- /ItemCategory Master -->
        </div>
		<div class="btn"><asp:Button runat="server" ID="btnUpdate" Text="更 新" 
                onclick="btnUpdate_Click" /> </div>
	</form>
	</div>
<!-- /CategoryID list -->
	</div><!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
