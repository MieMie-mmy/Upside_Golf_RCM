<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category_ExportForJisha.aspx.cs" Inherits="ORS_RCM.Category_ExportForJisha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <!DOCTYPE html>

<head>
<meta charset="UTF-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<title>商品管理システム＜キャンペーン確認＞</title>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/jscript"></script>
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>

<link href="../../Styles/base.css" rel="stylesheet" />
<link href="../../Styles/common.css" rel="stylesheet" />
<link href="../../Styles/manager-style.css" rel="stylesheet" />



</head>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CmnContents">
	<div id="ComBlock" style="margin-top: 60px;">
		<div class="setListBox confBox iconSet iconEdit">
		<h1>カテゴリエクスポート</h1>

         <dl>
         <dt>カテゴリエクスポート</dt>
         </dl>
        <div class="widthhMax iconEx operationBtn">
		<div class="operationBtn">
			<p>				
		
			    <asp:Button runat="server" ID="btnExport" Text="データエクスポート" Width="200px" onclick="btnExport_Click" />
                
				
			</p>
			
		</div>

      
        </div>

    </div>
</asp:Content>
