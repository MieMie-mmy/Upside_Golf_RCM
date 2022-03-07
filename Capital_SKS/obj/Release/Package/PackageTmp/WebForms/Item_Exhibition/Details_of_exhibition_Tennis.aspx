<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_exhibition_Tennis.aspx.cs" Inherits="Capital_SKS.WebForms.Item_Exhibition.Details_of_exhibition_Tennis" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
  <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<script src="../js/jquery.page-scroller.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hflist" runat="server" />
    <asp:HiddenField ID="hfshopid" runat="server" />
    <body>
<div id="CmnWrapper">
  <p id="toTop"><a href="#CmnContents">▲TOP</a></p>

   <div id="CmnContents">
	<div id="ComBlock">
        <div class="setDetailBox defaultSet iconSet iconRakuten exbCmnSet">
		<h1>Tennic出品詳細</h1>

		<ul class="pageLink">
			<li><a href="#error">エラーメッセージ</a></li>
			<li><a href="#HeaderITem-csv">HeaderITem.csv</a></li>
			<li><a href="#DetailSKU-csv">DetailSKU.csv</a></li>
		</ul>
             <h2 id="error">エラーメッセージ</h2>
		<table class="errorBox">
			<tbody>
			<tr>
				<td>出品結果</td>
                <td>
                <asp:TextBox ID="lblitemerror" runat="server" TextMode="MultiLine" Enabled="false" Width="700px" />
                    <%--<asp:Label ID="lblitemerror" runat="server" Text=""></asp:Label>--%></td>
			</tr>
			<tr>
				<td>APIチェック</td>
                <td>
                    <asp:Label ID="lblselecterror" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td>バッチチェック</td>
                <td>
                    <asp:Label ID="lblcaterror" runat="server" Text=""></asp:Label></td>
			</tr>
			</tbody>
		</table>
             <h2 id="HeaderITem-csv">HeaderITem.csv</h2>
            <table class="ComUserBlockform">
                <table  class="editTable">
                <tr>
                <th>コントロールカラム</th >
                <td >
                <asp:Label ID="lblcontrol" runat="server" Text="" ></asp:Label>
                </td>
                </tr>
                <tr>
                <th>商品グループコード</th>
                <td>
                <asp:Label ID="lblProductCode" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>商品名</th>
                <td >
                <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>販売価格</th>
                <td >
                <asp:Label ID="lblSellPrice" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                 <tr>
                <th>定価</th>
                <td >
                <asp:Label ID="lblListPrice" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                 <tr>
                <th>文言(HTML)</th>
                <td >
                <div class="textarea">
                 <asp:Label ID="txtDescription" runat="server" Text=""></asp:Label>
                </div>
                </td>
                </tr>
                 <tr>
                <th>カテゴリID</th>
                <td >
                <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                 <tr>
                <th>メーカー・ブランド</th>
                <td >
                <asp:Label ID="lblBrandName" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>オプションタイトル</th>
                <td >
                <asp:Label ID="lblOptTitle" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>オプション選択肢</th>
                <td >
                <asp:Label ID="lblOptChoice" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>送料込フラグ</th>
                <td >
                <asp:Label ID="lblShipFlag" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>メール便可否</th>
                <td >
                <asp:Label ID="lblMail" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>店舗引取可否</th>
                <td >
                <asp:Label ID="lblPick" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>直送方法CD</th>
                <td >
                <asp:Label ID="lblDeliveryCD" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                 <tr>
                <th>出品フラグ</th>
                <td >
                <asp:Label ID="lblExhibit" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                 <tr>
                <th>掲載開始日</th>
                <td >
                <asp:Label ID="lblReleaseDate" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>商品メインファイル名</th>
                <td >
                <asp:Label ID="lblmainFile" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>明細表示画像ファイル名</th>
                <td >
                <asp:Label ID="lblDetailFile" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>ポップ表示ファイル名</th>
                <td >
                <asp:Label ID="lblDisplayFile" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１ファイル名</th>
                <td >
                <asp:Label ID="lblImg1" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                 <tr>
                <th>画像２ファイル名</th>
                <td >
                <asp:Label ID="lblImg2" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                     <tr>
                <th>画像３ファイル名</th>
                <td >
                <asp:Label ID="lblImg3" runat="server" Text=""></asp:Label>
                </td>
                </tr>

                     <tr>
                <th>画像４ファイル名</th>
                <td >
                <asp:Label ID="lblImg4" runat="server" Text=""></asp:Label>
                </td>
                </tr>

                     <tr>
                <th>画像５ファイル名</th>
                <td >
                <asp:Label ID="lblImg5" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                 <tr>
                <th>画像６ファイル名</th>
                <td >
                <asp:Label ID="lblImg6" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像７ファイル名</th>
                <td >
                <asp:Label ID="lblImg7" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像８ファイル名</th>
                <td >
                <asp:Label ID="lblImg8" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像９ファイル名</th>
                <td >
                <asp:Label ID="lblImg9" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１０ファイル名</th>
                <td >
                <asp:Label ID="lblImg10" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１１ファイル名</th>
                <td >
                <asp:Label ID="lblImg11" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１２ファイル名</th>
                <td >
                <asp:Label ID="lblImg12" runat="server" Text=""></asp:Label>
                </td>
                </tr>

                <tr>
                <th>画像１３ファイル名</th>
                <td >
                <asp:Label ID="lblImg13" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１４ファイル名</th>
                <td >
                <asp:Label ID="lblImg14" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１５ファイル名</th>
                <td >
                <asp:Label ID="lblImg15" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１６ファイル名</th>
                <td >
                <asp:Label ID="lblImg16" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１７ファイル名</th>
                <td >
                <asp:Label ID="lblImg17" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１８ファイル名</th>
                <td >
                <asp:Label ID="lblImg18" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像１９ファイル名</th>
                <td >
                <asp:Label ID="lblImg19" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>画像２０ファイル名</th>
                <td >
                <asp:Label ID="lblImg20" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>タグ情報</th>
                <td >
                <asp:Label ID="lblTag" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>検索タグ情報</th>
                <td >
                <asp:Label ID="lblTagSearch" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>キャッチコピー</th>
                <td >
                <asp:Label ID="lblCatchCopy" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>ポイント率</th>
                <td >
                <asp:Label ID="lblPoint" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                <tr>
                <th>割引率</th>
                <td >
                <asp:Label ID="lblDiscount" runat="server" Text=""></asp:Label>
                </td>
                </tr>
                    </table>
                <h2 id="DetailSKU-csv">DetailSKU.csv</h2>
                <div class="tableCsv">
                <asp:GridView ID="gvSKU" runat="server" AutoGenerateColumns="False" 
                CellPadding="4" EmptyDataText="There is no data to display!" 
                ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" 
                Width="100%" 
                AllowPaging="True" OnPageIndexChanging="gvSKU_PageIndexChanging" >

                <Columns>
                <asp:TemplateField Visible ="false" >
                <ItemTemplate>
                <asp:Label ID="Label10" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品グループコード">
                 <HeaderStyle Wrap="true" />
        <ItemTemplate>
            <asp:Label ID="Label12" runat="server" Text='<%#Eval("商品グループコード")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="商品コード">
            <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label11" runat="server" Text='<%#Eval("商品コード")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="ItemAdmin" >
             <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label13" runat="server" Text='<%#Eval("ItemAdmin")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="サイズ">
        <ItemTemplate>
            <asp:Label ID="Label14" runat="server" Text='<%#Eval("サイズ")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="色">
        <ItemTemplate>
            <asp:Label ID="Label15" runat="server" Text='<%#Eval("色")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="納期">
        <ItemTemplate>
            <asp:Label ID="Label16" runat="server" Text='<%#Eval("納期")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField  HeaderText="在庫" >
            <HeaderStyle Wrap="true" />
        <ItemTemplate>
            <asp:Label ID="Label17" runat="server" Text='<%#Eval("在庫")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
                </Columns>
                </asp:GridView>
                </div>
            </div>
        </div><!--ComBlock-->
        </div><!--CmnContents-->
</div><!--CmnWrapper-->
    </body>
</asp:Content>
