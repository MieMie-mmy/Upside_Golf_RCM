<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_exhibition_Wowma.aspx.cs" Inherits="Capital_SKS.WebForms.Item_Exhibition.Detail_of_Exhibition_Wowma" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
    <script src="../js/jquery.page-scroller.js"></script>
   <style type ="text/css"></style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hflist" runat="server" />
    <asp:HiddenField ID="hfshopid" runat="server" />
    <body>
         <div id="CmnWrapper"> 

<div id="CmnContents">
	<div id="ComBlock">

<!-- Fixed Value -->
	<div class="setDetailBox defaultSet iconSet iconRakuten exbCmnSet">
		<h1>Wowma 出品詳細</h1>

		<ul class="pageLink">
			<li><a href="#error">エラーメッセージ</a></li>
			<li><a href="#item-csv">item.csv</a></li>
			<li><a href="#select-csv">select.csv</a></li>
			<li><a href="#item-cat">item-cat.csv</a></li>
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


<h2 id="item-csv">item.csv</h2>
 <table class="ComUserBlockform">
<table  class="editTable">
 <tr>
 <th>コントロールカラム</th >
 <td >
     <asp:Label ID="lblcontrol" runat="server" Text=""></asp:Label>
  
 </td>
 </tr>
   <tr>
   <th>商品管理番号（商品URL)</th>
 <td >
     <asp:Label ID="lblProductURL" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  <th>商品番号</th>
 <td >
     <asp:Label ID="lblproductNo" runat="server" Text=""></asp:Label>
      </td>
  </tr>
   <tr>
   <th>全商品ディレクトリID</th>
 <td  >
     <asp:Label ID="lblproductDirectoryID" runat="server" 
         Text=""></asp:Label>
      </td>
</tr>
  <tr>
  <th>タグID</th>
 <td >
     <asp:Label ID="lblTagID" runat="server" Text=""></asp:Label>
      </td>
 </tr>
 <tr>
 <th>PC用キャッチコピー</th>
 <td>
     <asp:Label ID="lblPCCopy" runat="server" Text=""></asp:Label>
 </td>
 </tr>
 <tr>
 <th>モバイル用キャッチコピー</th>
 <td>
     <asp:Label ID="lblmobilecopy" runat="server" Text=""></asp:Label></td>
 </tr>
 <tr>
 <th>商品名</th>
 <td>
     <asp:Label ID="lblproductname" runat="server" Text=""></asp:Label></td>
 </tr>
 <tr>
 <th>販売価格</th>
 <td >
     <asp:Label ID="lblSellingPrice" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  <th>表示価格</th>
      <td>
          <asp:Label ID="lblindprice" runat="server" Text=""></asp:Label></td>
      </tr>
  <tr>
  <th>消費税</th>
 <td >
     <asp:Label ID="lblconsumptionTax" runat="server" Text=""></asp:Label>
       </td>
  </tr>

    <tr>
        <th>軽減税率設定</th>
        <td>
            <asp:Label ID="lblreducetaxRate" runat="server" Text=""></asp:Label>
        </td>
    </tr>

    <tr>
    <th>送料</th>
<td >
     <asp:Label ID="lblPostage" runat="server" Text=""></asp:Label>
       </td>
       </tr>
     <tr>
     <th>個別送料</th>
 <td>
     <asp:Label ID="lblExtrashipping" runat="server" Text=""></asp:Label>
       </td>
       </tr>
    <tr>
    <th>送料区分1</th>
 <td >
     <asp:Label ID="lblshippingcategory1" runat="server" Text=""></asp:Label>
       </td>
       </tr>
      <tr>
      <th>送料区分2</th>
 <td >
     <asp:Label ID="lblshippingcategory2" runat="server" Text=""></asp:Label>
       </td>
       </tr>
    <tr>
    <th>代引料</th>
 <td>
<asp:Label ID="lbldeliverycharges" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<th>倉庫指定 </th>
 <td >
<asp:Label ID="lblWarehousespecified" runat="server" Text=""></asp:Label>
</td>
</tr>
     <tr>
     <th>商品情報レイアウト</th>
 <td >
     <asp:Label ID="lblproductinformation" runat="server" Text=""></asp:Label>
       </td>
       </tr>
    <tr>
    <th>注文ボタン</th>
 <td >
<asp:Label ID="lblorder_button" runat="server" Text=""></asp:Label>
</td>
</tr>
 <tr>
 <th>資料請求ボタン</th>
 <td >
<asp:Label ID="lblRequestbutton" runat="server" Text=""></asp:Label>
</td>
</tr>   
 <tr>
 <th>商品問い合わせボタン</th>
 <td>
<asp:Label ID="lblProduct_inquiry_button" runat="server" Text=""></asp:Label>
</td>
</tr>     
<tr>
<th>再入荷お知らせボタン</th>
 <td >
<asp:Label ID="lblComingsoon_button" runat="server" Text=""></asp:Label>
</td>
</tr>   
<tr>
<th>モバイル表示 </th>
 <td >
<asp:Label ID="lblMobileDisplay" runat="server" Text=""></asp:Label>
</td>
</tr>    
<tr>
<th>のし対応</th>
 <td>
<asp:Label ID="lblExpandCode" runat="server" Text=""></asp:Label>
</td>
</tr> 
 <tr>
 <th>PC用商品説明文</th>
 <td>
 <div class="textarea">
     <asp:Label ID="txtpcforitem" runat="server" Text=""></asp:Label>
     </div>
    </td>
 </tr>
 <tr>
 <th>モバイル用商品説明文</th>
 <td>
     <div class="textarea">
         <asp:Label ID="txtmobiledesc" runat="server" Text=""></asp:Label>
     </div>
     </td>
 </tr>
 <tr>
 <th>スマートフォン用商品説明文</th>
 <td>
 <div class="textarea">
     <asp:Label ID="txtsmartphoneforitemdesc" runat="server" Text="" ></asp:Label>
         </div>
 </td>
 </tr>
 <tr>
 <th>PC用販売説明文</th>
 <td>
     <asp:Label ID="lblPCforsaledesc" runat="server" Text=""></asp:Label>
 </td>
 </tr>
 <tr>
 <th>商品画像URL</th>
 <td>
     <asp:Label ID="lblproimgurl" runat="server" Text=""></asp:Label>
 </td>
 </tr>
 <tr>
 <th>商品画像名（ALT）</th>
 <td>
     <asp:Label ID="lblproALT" runat="server" Text=""></asp:Label>
 </td>
 </tr>
 <tr>
 <th> 動画 </th>
 <td >
<asp:Label ID="lblAnimation" runat="server" Text=""></asp:Label>
</td>
</tr>  
<tr>
<th>販売期間指定</th>
 <td>
<asp:Label ID="lblSpecified_saleperiod" runat="server" Text=""></asp:Label>
</td>
</tr>   
<tr>
<th>注文受付数</th>
 <td>
<asp:Label ID="lblOrderNoAcceptence" runat="server" Text=""></asp:Label>
</td>
</tr> 
<tr>
<th>在庫タイプ</th>
 <td >
<asp:Label ID="lblStocktype" runat="server" Text=""></asp:Label>
</td>
</tr> 
<tr>
<th>在庫数 </th>
 <td >
<asp:Label ID="lblStockNumber" runat="server" Text=""></asp:Label>
</td>
</tr> 
<tr>
<th>在庫数表示</th>
 <td >
<asp:Label ID="lblStockNumberdisplay" runat="server" Text=""></asp:Label>
</td>
</tr> 
<tr>
<th>項目選択肢別在庫用横軸項目名</th>
 <td>
<asp:Label ID="lblhorizontal_axis_item_name" runat="server" Text=""></asp:Label>
</td>
</tr> 
<tr>
<th>項目選択肢別在庫用縦軸項目名	</th>
 <td>
<asp:Label ID="lblvertical_axis_item_name" runat="server" Text=""></asp:Label>
</td>
</tr> 
 <tr>
 <th>項目選択肢別在庫用残り表示閾値</th>
 <td >
<asp:Label ID="lblremaining_stock_for_display_threshold" runat="server" Text=""></asp:Label>
</td>
</tr> 
<tr>
<th>RAC番号</th>
 <td>
<asp:Label ID="lblRacNumber" runat="server" Text=""></asp:Label>
</td>
</tr> 
  <tr>
  <th>サーチ非表示</th>
 <td >
     <asp:Label ID="lblSearchhide" runat="server" Text=""></asp:Label>
      </td>
  </tr>
     <tr>
     <th>闇市パスワード</th>
     <td ><asp:Label ID="lblBlackmarketpassword" runat="server" Text=""></asp:Label> </td>
      </tr>
 <tr>
 <th>カタログID</th>
 <td >
<asp:Label ID="lblCategoryID" runat="server" Text=""></asp:Label>
</td>
</tr> 
  <tr>
  <th>在庫戻しフラグ</th>
  <td>
      <asp:Label ID="lblFlag" runat="server" Text=" "></asp:Label>
      </td>
 </tr>
    <tr>
    <th>在庫切れ時の注文受付</th>
  <td>
  <asp:Label ID="lblOutofStock" runat="server" Text=""></asp:Label>
  </td>
  </tr>
   <tr>
   <th>在庫あり時納期管理番号</th>
   <td >
      <asp:Label ID="lblControlNo" runat="server" Text=""></asp:Label>
     </td> 
  </tr>
  <tr>
  <th>在庫切れ時納期管理番号</th>
  <td>
      <asp:Label ID="lbldateoutofstock" runat="server" Text=""></asp:Label>
  </td>
  </tr>
   <tr>
   <th>予約商品発売日</th>
 <td >
      <asp:Label ID="lblBookOrderDate" runat="server" Text=""></asp:Label>
       </td>
  </tr>
 <tr>
 <th>ポイント変倍率</th>
 <td>
     <asp:Label ID="lblpointmagnification" runat="server" Text=""></asp:Label></td>
 </tr>
 <tr>
 <th>ポイント変倍率適用期間</th>
 <td>
     <asp:Label ID="lblpointmagperiod" runat="server" Text=""></asp:Label></td>
 </tr>
   <tr>
   <th>ヘッダー・フッター・レフトナビ</th>
 <td >
      <asp:Label ID="lblHeaderFooter" runat="server" Text=""></asp:Label>
       </td>
  </tr>
   <tr>
   <th>表示項目の並び順</th>
 <td >
      <asp:Label ID="lblDisplayItem" runat="server" Text=""></asp:Label>
       </td>
  </tr>
   <tr>
   <th>共通説明文(小)</th>
 <td >
     <asp:Label ID="lblCommonDescriptionsmall" runat="server" 
          Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  <th>目玉商品</th>
  <td>
      <asp:Label ID="lblfeatureperiod" runat="server" Text=""></asp:Label>
  </td>
  </tr>
    <tr>
    <th>共通説明文(大)</th>
 <td>
      <asp:Label ID="lblCommonDescriptionlarge" runat="server" 
          Text=""></asp:Label>
      </td>
  </tr>
    <tr>
    <th>レビュー本文表示</th>
 <td>
      <asp:Label ID="lblReviewDisplay" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  <th>あす楽配送管理番号</th>
  <td>
      <asp:Label ID="lbleasydeliverynumber" runat="server" Text=""></asp:Label></td>
  </tr>
     <tr>
     <th>海外配送管理番号</th>
 <td >
      <asp:Label ID="lblOverseaDeliveryControlNo" runat="server" 
          Text=""></asp:Label>
       </td>
  </tr>
     <tr>
     <th>サイズ表リンク</th>
 <td >
      <asp:Label ID="lblSitechartlink" runat="server" Text=""></asp:Label>
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

    <tr>
        <th>カタログIDなしの理由</th>
       <td>
           <asp:Label ID="lblcatalogid" runat="server" Text=""></asp:Label>
       </td>
    </tr>
  </table>
   <h2 id="select-csv">select.csv</h2>
   <div class="tableCsv">
    <asp:GridView ID="gvselect" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" EmptyDataText="There is no data to display!" 
        ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" 
        onpageindexchanging="gvselect_PageIndexChanging" Width="100%" 
        AllowPaging="True" >

          <Columns>
       <%-- <asp:TemplateField Visible ="false" >
        <ItemTemplate>
            <asp:Label ID="Label10" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>--%>
                 <asp:TemplateField HeaderText="項目選択肢用コントロールカラム">
                 <HeaderStyle Wrap="true" />
        <ItemTemplate>
            <asp:Label ID="Label12" runat="server" Text='<%#Eval("コントロールカラム")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="商品管理番号（商品URL）">
            <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label11" runat="server" Text='<%#Eval("商品管理番号（商品URL）")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="商品番号" >
             <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label13" runat="server" Text='<%#Eval("商品管理番号（商品URL）")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="選択肢タイプ ">
        <ItemTemplate>
            <asp:Label ID="Label14" runat="server" Text='<%#Eval("選択肢タイプ")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Select/Checkbox用項目名">
        <ItemTemplate>
            <asp:Label ID="Label15" runat="server" Text='<%#Eval("Select/Checkbox用項目名")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Select/Checkbox用選択肢">
        <ItemTemplate>
            <asp:Label ID="Label16" runat="server" Text='<%#Eval("Select/Checkbox用選択肢")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField  HeaderText="項目選択肢別在庫用横軸選択肢" >
            <HeaderStyle Wrap="true" />
        <ItemTemplate>
            <asp:Label ID="Label17" runat="server" Text='<%#Eval("項目選択肢別在庫用横軸選択肢")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="項目選択肢別在庫用横軸選択肢子番号">
             <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label18" runat="server" Text='<%#Eval("項目選択肢別在庫用横軸選択肢子番号")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="項目選択肢別在庫用縦軸選択肢">
             <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label19" runat="server" Text='<%#Eval("項目選択肢別在庫用縦軸選択肢")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="項目選択肢別在庫用縦軸選択肢子番号">
             <HeaderStyle Wrap="true" />
        <ItemTemplate>
            <asp:Label ID="Label20" runat="server" Text='<%#Eval("項目選択肢別在庫用縦軸選択肢子番号")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="項目選択肢別在庫用取り寄せ可能表示">
             <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label21" runat="server" Text='<%#Eval("項目選択肢別在庫用取り寄せ可能表示")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
              <asp:TemplateField HeaderText="項目選択肢別在庫用在庫数">
               <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label30" runat="server" Text='<%#Eval("項目選択肢別在庫用在庫数")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
              <asp:TemplateField HeaderText="在庫戻しフラグ">
               <HeaderStyle Wrap="true" />
        <ItemTemplate>
            <asp:Label ID="Label331" runat="server" Text='<%#Eval("在庫戻しフラグ")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
               <asp:TemplateField HeaderText="在庫切れ時の注文受付">
                <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label32" runat="server" Text='<%#Eval("在庫切れ時の注文受付")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
               <asp:TemplateField HeaderText="在庫あり時納期管理番号">
                <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label33" runat="server" Text='<%#Eval("在庫あり時納期管理番号")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="在庫切れ時納期管理番号">
              <HeaderStyle  />
        <ItemTemplate>
            <asp:Label ID="Label34" runat="server" Text='<%#Eval("在庫切れ時納期管理番号")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="タグID">
              <HeaderStyle  />
        <ItemTemplate>
            <asp:Label ID="Label35" runat="server" Text='<%#Eval("タグID")%>'></asp:Label>
        </ItemTemplate>
       
        </asp:TemplateField>



     </Columns>
    </asp:GridView>
    </div>
<h2 id="item-cat">item-cat.csv</h2>
		<div class="tableCsv">
    <asp:GridView ID="gvcat" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" EmptyDataText="There is no data to display!" 
        ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" 
        onpageindexchanging="gvcat_PageIndexChanging" Width="100%" 
        AllowPaging="True">
          <Columns>
    <asp:TemplateField HeaderText="コントロールカラム" HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
        <asp:Label ID="Label23" runat="server" Text='<%#Eval("コントロールカラム")%>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText="商品管理番号（商品URL)" HeaderStyle-HorizontalAlign="Left" >
    <ItemTemplate>
        <asp:Label ID="Label1" runat="server" Text='<%#Eval("商品管理番号（商品URL）")%>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField><asp:TemplateField HeaderText="表示先カテゴリ"  HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
        <asp:Label ID="Label26" runat="server" Text='<%#Eval("表示先カテゴリ")%>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
       <asp:TemplateField HeaderText="優先度"  HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
        <asp:Label ID="Label27" runat="server" Text='<%#Eval("優先度")%>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    <asp:TemplateField HeaderText=" 	1ページ複数形式" HeaderStyle-HorizontalAlign="Left" >
    <ItemTemplate>
        <asp:Label ID="Label65" runat="server" Text='<%#Eval("1ページ複数形式")%>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
         <asp:TemplateField HeaderText="カテゴリセット管理番号" HeaderStyle-HorizontalAlign="Left" >
     <ItemTemplate>
        <asp:Label ID="Label28" runat="server" Text='<%#Eval("カテゴリセット管理番号")%>'></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
         <asp:TemplateField HeaderText="カテゴリセット名" HeaderStyle-HorizontalAlign="Left">
     <ItemTemplate>
        <asp:Label ID="Label29" runat="server" Text='<%#Eval("カテゴリセット名")%>'></asp:Label>
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
