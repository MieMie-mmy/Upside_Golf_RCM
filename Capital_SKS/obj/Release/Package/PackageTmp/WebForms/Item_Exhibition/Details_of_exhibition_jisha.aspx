<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_exhibition_jisha.aspx.cs" Inherits="ORS_RCM.Details_of_exhibition_jisha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"/></script>
     <link href="../../Scripts/jquery.page-scroller.js" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#CmnContents">▲TOP</a></p>
<div id="CmnContents">
	<div id="ComBlock">
<!-- Fixed Value -->
	<div class="setDetailBox shopCmnSet defaultSet iconSet iconJisya exbCmnSet">
		<h1>自社出品詳細</h1>
		<ul class="pageLink">
			<li><a href="#error">エラーメッセージ</a></li>
			<li><a href="#item-csv">item.csv</a></li>
			<li><a href="#option-csv">select.csv</a></li>
			<li><a href="#category-csv">item-cat.csv</a></li>
		</ul>

<h2 id="error">エラーメッセージ</h2>
<table class="errorBox">
	<tbody>
	<tr>
	<td>出品結果</td><td><asp:Label ID="lbljitemerror" runat="server" Text=""></asp:Label></td>
	</tr>
	<tr>
	<td>APIチェック</td><td><asp:Label ID="lbljselecterror" runat="server" Text=""></asp:Label></td>
	</tr>
	<tr>
	<td>バッチチェック</td><td><asp:Label ID="lbljcaterror" runat="server" Text=""></asp:Label></td>
	</tr>
	</tbody>
</table>
 <h2 id="item-csv">item.csv</h2>
 <table  class="editTable">
 <tbody>
 <tr>
 <th>コントロールカラム</th>
 <td class="td"><asp:Label ID="lblControlColumn" runat="server" Text=""></asp:Label></td>
 </tr>
  <tr>
  <th>商品管理番号（商品URL)</th>
  <td ><asp:Label ID="lblProductURL" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>商品番号</th>
  <td ><asp:Label ID="lblItemCode" runat="server" Text=""></asp:Label></td>
  </tr>   
  <tr>
  <th>全商品ディレクトリID</th>
  <td><asp:Label ID="lblAll_Products_directory_ID" runat="server" Text=""></asp:Label></td>
  </tr> 
  <tr>
  <th>タグＩＤ</th>
   <td ><asp:Label ID="lblTagID" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>PC用キャッチコピー</th>
  <td><asp:Label ID="lblpccatchcopy" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>モバイル用キャッチコピー</th>
  <td><asp:Label ID="lblMobileCatchCopy" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>商品名</th>
  <td><asp:Label ID="lblItemName" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>販売価格</th>
  <td><asp:Label ID="lblSelling_price" runat="server" Text=""></asp:Label></td>
 </tr>
  <tr>
  <th>表示価格</th>
  <td><asp:Label ID="lblIndicated_price" runat="server" Text=""></asp:Label></td>
 </tr>
  <tr>
  <th>消費税</th>
 <td><asp:Label ID="lblConsumptiontax" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>送料</th>
  <td><asp:Label ID="lblPostage" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>個別送料</th>
  <td ><asp:Label ID="lblshippingCost" runat="server" Text=""></asp:Label></td>
  </tr> 
  <tr>
  <th>送料区分1</th>
  <td><asp:Label ID="lblShipping_Category_1" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>送料区分2</th>
  <td ><asp:Label ID="lblShipping_Category_2" runat="server" Text=""></asp:Label> </td>
 </tr>
  <tr>
  <th>代引料</th>
  <td ><asp:Label ID="lbl代引料" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>倉庫指定</th>
  <td ><asp:Label ID="lblwarehouse_specified" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>商品情報レイアウト</th>
  <td><asp:Label ID="lblproductInformationLayout" runat="server"></asp:Label></td>
  </tr>
  <tr>
  <th>注文ボタン</th>
  <td><asp:Label ID="lblOrderbutton" runat="server" Text="" ></asp:Label></td>
  </tr>
  <tr>
  <th>資料請求ボタン</th>
  <td><asp:Label ID="lblRequest_button" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>商品問い合わせボタン</th>
  <td><asp:Label ID="lblProduct_Inquire_Button" runat="server" Text=""></asp:Label></td>
  </tr>
   <tr>
  <th>再入荷お知らせボタン</th>
  <td><asp:TextBox ID="lblComingSoon_button" runat="server" Enabled ="false"></asp:TextBox></td>
  </tr>
  <tr>
  <th>モバイル表示</th>
  <td><asp:TextBox ID="lblmobileDisplay" runat="server" Enabled="false"></asp:TextBox></td>
  </tr>
  <tr>
  <th>のし対応</th>
  <td><asp:TextBox ID="lblworks_Corresponding" runat="server" Enabled ="false" TextMode ="MultiLine" Height="59px" Width="422px"></asp:TextBox></td>
  </tr>
  <tr>
  <th>PC用商品説明文</th>
  <td><asp:Label ID="lblPcForItem_Description" runat="server" Text=""></asp:Label></td>
  </tr>
  </tr>
  <tr>
  <th>モバイル用商品説明文</th>
  <td><asp:Label ID="lblMobileItemDescription" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>スマートフォン用商品説明文</th>
  <td><asp:Label ID="lblSmartPhoneFor_ItemDescription" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>PC用販売説明文</th>
  <td><asp:Label ID="lblPCforSale_Description" runat="server" Text=""></asp:Label></td>
  </tr>  
  <tr>
  <th>商品画像URL</th>
  <td><asp:Label ID="lblProdcutImage_URL" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>商品画像名（ALT）</th>
  <td><asp:Label ID="lblProductImage_Name" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>動画</th>
  <td ><asp:Label ID="lblAnimation" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>販売期間指定</th>
  <td><asp:Label ID="lblSalePeriod_Specified" runat="server" Text=""></asp:Label></td>
 </tr>
  <tr>
  <th>注文受付数</th>
  <td ><asp:Label ID="lblOrder_number_of_acceptances" runat="server" Text=""></asp:Label></td>
  </tr> 
  <tr>
  <th>在庫タイプ</th>
  <td><asp:Label ID="lblStock_Type" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>在庫数</th>
  <td ><asp:Label ID="lblStock_Quantity" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
 <th>在庫数表示</th>
 <td><asp:Label ID="lblStockno_Display" runat="server" Text=""></asp:Label></td>
 </tr>
<tr>
 <th>項目選択肢別在庫用横軸項目名</th>
 <td><asp:Label ID="lblhorizontal_axis_itemname" runat="server" Text=""></asp:Label></td>
</tr>    
<tr>
 <th>項目選択肢別在庫用縦軸項目名</th>
 <td><asp:Label ID="lblvertical_axis_itemname" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
 <th>項目選択肢別在庫用残り表示閾値</th>
 <td><asp:Label ID="lbl_remaining_stock_fordisplaythreshold" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
 <th>RAC番号</th>
 <td><asp:Label ID="lblRacNo" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
 <th>サーチ非表示</th>
 <td><asp:Label ID="lblSearchHide" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
 <th>闇市パスワード</th>
 <td><asp:Label ID="lblBlackMarket_Password" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
 <th>カタログID</th>
 <td><asp:Label ID="lblCatalogID" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
 <th>在庫戻しフラグ</th>
 <td>
<asp:Label ID="lblFlag_Back_Stock" runat="server" Text=""></asp:Label>
</td>
</tr>   
<tr>
 <th>在庫切れ時の注文受付</th>
 <td >
<asp:Label ID="lblOrder_accepted_outofstockatthetime" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
 <th>在庫あり時納期管理番号</th>
 <td >
<asp:Label ID="lblDeliverycontrolnumber_whenInStock" runat="server" Text=""></asp:Label>
</td>
</tr>  
<tr>
 <th>在庫切れ時納期管理番号</th>
 <td >
<asp:Label ID="lblDelivery_date_management_number_outofstockatthetime" runat="server" Text=""></asp:Label>
</td>
</tr>      
<tr>
 <th>予約商品発売日</th>
 <td >
<asp:Label ID="lblReservation_ItemReleaseDate" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
 <th>ポイント変倍率</th>
 <td >
<asp:Label ID="lblPointMagnification" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
 <th>ポイント変倍率適用期間</th>
 <td >
<asp:Label ID="lblPointmagnification_applicationperiod" runat="server" Text=""></asp:Label>
</td>
</tr>    
<tr>
 <th>ヘッダー・フッター・レフトナビ</th>
 <td >
<asp:Label ID="lblHeader_Footer" runat="server" Text=""></asp:Label>
</td>
</tr>    
<tr>
 <th>表示項目の並び順</th>
 <td>
<asp:Label ID="lblOrderoftheDisplayItem" runat="server" Text=""></asp:Label>
</td>
</tr>   
<tr>
 <th>共通説明文（小）</th>
 <td>
<asp:Label ID="lblCommonDescription_Small" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
 <th>目玉商品</th>
 <td >
<asp:Label ID="lblFeature_Product" runat="server" Text=""></asp:Label>
</td>
</tr>   
<tr>
 <th>共通説明文（大）</th>
 <td>
<asp:Label ID="lblCommonDescriptionLarge" runat="server" Text=""></asp:Label>
</td>
</tr>     
<tr>
 <th>レビュー本文表示</th>
 <td >
<asp:Label ID="lblReviewText" runat="server" Text=""></asp:Label>
</td>
</tr>    
<tr>
 <th>あす楽配送管理番号</th>
 <td >
<asp:Label ID="lblEasier_delivery_management_number_tomorrow" runat="server" Text=""></asp:Label>
</td>
</tr>  
<tr>
 <th>海外配送管理番号</th>
 <td>
<asp:Label ID="lblOverseas_deliverycontrolnumber" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
 <th>サイズ表リンク</th>
 <td>
<asp:Label ID="lblSizeChartLink" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
 <th>医薬品説明文</th>
 <td >
<asp:Label ID="lblDrugDescription" runat="server" Text=""></asp:Label>
</td>
</tr>     
<tr>
 <th>医薬品注意事項</th>
 <td >
<asp:Label ID="lblDrugNote" runat="server" Text=""></asp:Label>
</td>
</tr>
 </tbody>
</table>
<h2 id="option-csv">select.csv</h2>
		<div class="tableCsv">
            <asp:GridView ID="gvoption" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True">
                <Columns>
                 <asp:TemplateField  HeaderText="項目選択肢用コントロールカラム">
                <ItemTemplate>
                    <asp:Label ID="lblitemctrlcol" runat="server" Text='<%#Eval("項目選択肢用コントロールカラム")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="商品管理番号（商品URL）">
                <ItemTemplate>
                    <asp:Label ID="lblprourl" runat="server" Text='<%#Eval("商品管理番号（商品URL）")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="選択肢タイプ">
                <ItemTemplate>
                    <asp:Label ID="lblchoicetype" runat="server" Text='<%#Eval("選択肢タイプ")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="Select/Checkbox用項目名">
                <ItemTemplate>
                    <asp:Label ID="lblchkitem" runat="server" Text='<%#Eval("Select/Checkbox用項目名")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="Select/Checkbox用選択肢">
                <ItemTemplate>
                    <asp:Label ID="lblckchoice" runat="server" Text='<%#Eval("Select/Checkbox用選択肢")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="項目選択肢別在庫用横軸選択肢">
                <ItemTemplate>
                    <asp:Label ID="lblchsthraxis" runat="server" Text='<%#Eval("項目選択肢別在庫用横軸選択肢")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="項目選択肢別在庫用横軸選択肢子番号">
                <ItemTemplate>
                    <asp:Label ID="lblsentakoshicono" runat="server" Text='<%#Eval("項目選択肢別在庫用横軸選択肢子番号")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="項目選択肢別在庫用縦軸選択肢">
                <ItemTemplate>
                    <asp:Label ID="lblvaeraxichoice" runat="server" Text='<%#Eval("項目選択肢別在庫用縦軸選択肢")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="項目選択肢別在庫用縦軸選択肢子番号">
                <ItemTemplate>
                    <asp:Label ID="lblvertscokoshino" runat="server" Text='<%#Eval("項目選択肢別在庫用縦軸選択肢子番号")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="項目選択肢別在庫用取り寄せ可能表示">
                <ItemTemplate>
                    <asp:Label ID="lblstockforoption" runat="server" Text='<%#Eval("項目選択肢別在庫用取り寄せ可能表示")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="項目選択肢別在庫用在庫数">
                <ItemTemplate>
                    <asp:Label ID="lblitemchoiceforstockinv" runat="server" Text='<%#Eval("項目選択肢別在庫用在庫数")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="在庫戻しフラグ">
                <ItemTemplate>
                    <asp:Label ID="lblflagstock" runat="server" Text='<%#Eval("在庫戻しフラグ")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="在庫切れ時の注文受付">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("在庫切れ時の注文受付")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField  HeaderText="在庫あり時納期管理番号">
                <ItemTemplate>
                    <asp:Label ID="lblorderoutstock" runat="server" Text='<%#Eval("在庫あり時納期管理番号")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField  HeaderText="在庫切れ時納期管理番号">
                <ItemTemplate>
                    <asp:Label ID="lbldeliverydatestock" runat="server" Text='<%#Eval("在庫切れ時納期管理番号")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

	<h2 id="category-csv">item-cat.csv</h2>
		<div class="tableCsv">
            <asp:GridView ID="gvcat" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True">
            <Columns>
            <asp:TemplateField HeaderText="コントロールカラム">
            <ItemTemplate>
                <asp:Label ID="lblctrlcol" runat="server" Text='<%#Eval("コントロールカラム")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="商品管理番号（商品URL）">
            <ItemTemplate>
                <asp:Label ID="lblprourl" runat="server" Text='<%#Eval("商品管理番号（商品URL）")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <%--<asp:TemplateField HeaderText="商品名">
            <ItemTemplate>
                <asp:Label ID="lblproname" runat="server" Text='<%#Eval("商品名")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>--%>
              <asp:TemplateField HeaderText="表示先カテゴリ">
            <ItemTemplate>
                <asp:Label ID="lbldestinationcat" runat="server" Text='<%#Eval("表示先カテゴリ")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="優先度">
            <ItemTemplate>
                <asp:Label ID="lblpriority" runat="server" Text='<%#Eval("優先度")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="カテゴリセット管理番号">
            <ItemTemplate>
                <asp:Label ID="lbldestinationcat" runat="server" Text='<%#Eval("カテゴリセット管理番号")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="カテゴリセット名">
            <ItemTemplate>
                <asp:Label ID="lblpriority" runat="server" Text='<%#Eval("カテゴリセット名")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <%--<asp:TemplateField HeaderText="URL">
            <ItemTemplate>
                <asp:Label ID="lblurl" runat="server" Text='<%#Eval("商品名")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>--%>
              <%--<asp:TemplateField HeaderText="1ページ複数形式">
            <ItemTemplate>
                <asp:Label ID="lblmultiplepage" runat="server" Text='<%#Eval("1ページ複数形式")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>--%>
            </Columns>
            </asp:GridView>
        </div>
<%--
	<form action="#" method="get">
			<div class="btn"><p><input type="button" value="出品取消">&nbsp;&nbsp;<input type="button" value="編集画面へ"></p>
            </div>
		</form>--%>
</div>
</div>
</div>
</asp:Content>