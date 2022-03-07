<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MallCategory_Import.aspx.cs" Inherits="ORS_RCM.CSV_DataImport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <title>商品管理システム＜ショップカテゴリCSVデータインポート＞</title>
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />    
    <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/mall_category.css" rel="stylesheet" type="text/css" />    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#CmnContents">▲TOP</a></p>

	<div id="CmnContents">
    <div id="ComBlock">
	    <div id="divicon" runat="server" >                              
		    <h1 id="h1Rakuten" visible="false" runat="server"><asp:Label ID="lblRak" style=" margin-left:0px; color:Black; font-size:23px;" runat="server" visible="false" Text="楽天CSVインポート 確認" ></asp:Label></h1>
		    <h1 id="h1Yahoo" visible="false" runat="server"><asp:Label ID="lblYah" style=" margin-left:0px; color:Black; font-size:23px;"   runat="server" visible="false" Text="ヤフーCSVインポート 確認"></asp:Label> </h1>		 
		    <h1 id="h1Ponpare" visible="false" runat="server"><asp:Label  ID="lblPon" style=" margin-left:0px; color:Black; font-size:23px;"   runat="server" visible="false" Text='ポンパレモールCSVインポート 確認'></asp:Label> </h1>
            <h1 id="h1Wowma" visible="false" runat="server"><asp:Label  ID="lblWowma" style=" margin-left:0px; color:Black; font-size:23px;"   runat="server" visible="false" Text='WowmaモールCSVインポート 確認'></asp:Label> </h1>
            <h1 id="h1ORS" visible="false" runat="server"><asp:Label  ID="lblORS" style=" margin-left:0px; color:Black; font-size:23px;"   runat="server" visible="false" Text='ORS自社モールCSVインポート 確認'></asp:Label> </h1>
	        <p class="attText">下記内容で間違いなければ「更新」ボタンを押してください</p>	
	        <%--<div style="overflow-x: auto; overflow-y: hidden;">--%>
	        <asp:GridView ID="gvShow" runat="server" CssClass="listTable"
		        EmptyDataText="No data to display" ShowHeaderWhenEmpty="True" AllowPaging="True"  
                    PageSize="50" onpageindexchanging="gvShow_PageIndexChanging" >
	         </asp:GridView>	
             <div class="btn">     
	            <asp:Button ID="btnUpdate" runat="server" Text="更新" onclick="btnUpdate_Click" />
                <asp:Button ID="btnBack" runat="server" Text="更新"  Visible="false" onclick="btnBack_Click" />
             </div>
         </div>
      </div>
    </div>     
</asp:Content>
