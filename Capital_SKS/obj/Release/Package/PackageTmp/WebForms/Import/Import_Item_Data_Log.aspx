<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_Item_Data_Log.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Import_Item_Data_Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/database.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .dv
    {
     overflow:scroll;   
     }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox impirtAtt iconSet iconLog editBox">

		<h1>オプションデータインポート ログ</h1>
       
        <div class="dbCmnSet editBox">
        	<h2>オプションデータ</h2>

         <div class="dv">
            <asp:GridView ID="gvitem" runat="server" AllowPaging="True" 
             AutoGenerateColumns="False" 
            onpageindexchanging="gvitem_PageIndexChanging" PageSize="30" 
                 CssClass="itemIfoIpt listTable" EmptyDataText="There is no data to display!" 
                 ShowHeaderWhenEmpty="True" >
                <Columns>
                <asp:BoundField DataField="Ctrl_ID" HeaderText="コントロールカラム" />
                    <asp:BoundField DataField="製品コード" HeaderText="製品コード" />
                    <asp:BoundField DataField="商品番号" HeaderText="商品番号" />
                    <asp:BoundField DataField="商品名" HeaderText="商品名" />
                    <asp:BoundField DataField="定価" HeaderText="定価" />
                    <asp:BoundField DataField="販売価格" HeaderText="販売価格" />
                    <asp:BoundField DataField="原価" HeaderText="原価" />
                    <asp:BoundField DataField="発売日" HeaderText="発売日" />
                    <asp:BoundField DataField="掲載可能日" HeaderText="掲載可能日" />
                    <asp:BoundField DataField="年度" HeaderText="年度" />
                    <asp:BoundField DataField="シーズン" HeaderText="シーズン" />
                    <asp:BoundField DataField="ブランド名" HeaderText="ブランド名" />
                    <asp:BoundField DataField="ブランドコード" HeaderText="ブランドコード" />
                    <asp:BoundField DataField="送料" HeaderText="送料" />
                    <asp:BoundField DataField="競技名" HeaderText="競技名" />
                    <asp:BoundField DataField="分類名" HeaderText="分類名" />
                    <asp:BoundField DataField="仕入先名" HeaderText="仕入先名" />
                    <asp:BoundField DataField="商品情報" HeaderText="商品情報" />
                    <asp:BoundField DataField="PC用商品説明文" HeaderText="PC用商品説明文" />
                    <asp:BoundField DataField="スマートフォン用商品説明文" HeaderText="スマートフォン用商品説明文" />
                    <asp:BoundField DataField="PC用販売説明文" HeaderText="PC用販売説明文" />
                    <asp:BoundField DataField="楽天カテゴリID" HeaderText="楽天カテゴリID" />
                    <asp:BoundField DataField="ヤフーカテゴリID" HeaderText="ヤフーカテゴリID" />
                    <asp:BoundField DataField="ポンパレカテゴリID" HeaderText="ポンパレカテゴリID" />
                    <asp:BoundField DataField="個別送料" HeaderText="個別送料" />
                    <asp:BoundField DataField="倉庫指定" HeaderText="倉庫指定" />
                    <asp:BoundField DataField="闇市パスワード" HeaderText="闇市パスワード" />
                    <asp:BoundField DataField="Image_Name" HeaderText="ライブラリ画像" />
                    <asp:BoundField DataField="Related_ItemCode" HeaderText="関連商品" />
                    <asp:BoundField DataField="Shop_ID" HeaderText="出品対象ショップ" />
                    <asp:BoundField DataField="YahooエビデンスURL" HeaderText="YahooエビデンスURL" />
                </Columns>

        </asp:GridView>
       </div>
        </div>
     
        </div>
        
        <!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
