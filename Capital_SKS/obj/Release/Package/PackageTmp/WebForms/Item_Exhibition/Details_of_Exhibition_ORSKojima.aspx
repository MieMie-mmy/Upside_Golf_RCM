<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_Exhibition_ORSKojima.aspx.cs" Inherits="ORS_RCM.WebForms.Item_Exhibition.Details_of_Exhibition_ORSKojima" %>
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
                        <h1>楽天出品詳細</h1>
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
                                    <th>コントロールカラム</th>
                                    <td>
                                        <asp:Label ID="lblcontrol" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>商品管理番号（商品URL)</th>
                                    <td>
                                        <asp:Label ID="lblProductURL" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(注文コード)</th>
                                    <td>
                                        <asp:Label ID="lblOrderCode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(年間出荷数もしくは売れ筋A～Dランク)</th>
                                    <td>
                                        <asp:Label ID="lblshipment" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(カテゴリコード)</th>
                                    <td>
                                        <asp:Label ID="lblCategoryCode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>              
                                <tr>
                                    <th>(カテゴリ)</th>
                                    <td>
                                        <asp:Label ID="lblCategory" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>商品グループコード</th>
                                    <td>
                                        <asp:Label ID="lblProductGroupCode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(商品グループ名)</th>
                                    <td>
                                        <asp:Label ID="lblProductGroupName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>メーカー品番</th>
                                    <td>
                                        <asp:Label ID="lblManufacturerPartNo" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>販売数</th>
                                    <td>
                                        <asp:Label ID="lblSaleVolume" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>販売単位</th>
                                    <td>
                                        <asp:Label ID="lblSaleUnit" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>内容量数1</th>
                                    <td>
                                        <asp:Label ID="lblContentQuantityNumber1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>内容量単位1</th>
                                    <td>
                                        <asp:Label ID="lblContentUnit1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>内容量数2</th>
                                    <td>
                                        <asp:Label ID="lblContentQuantityNumber2" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>内容量単位2</th>
                                    <td>
                                        <asp:Label ID="lblContentUnit2" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>JANコード</th>
                                    <td>
                                        <asp:Label ID="lblJanCode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>参考基準価格</th>
                                    <td>
                                        <asp:Label ID="lblReferencePrice" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>オプションコード</th>
                                    <td>
                                        <asp:Label ID="lblOptionCode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>配送種別</th>
                                    <td>
                                        <asp:Label ID="lblDeliveryType" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>入荷日数</th>
                                    <td>
                                        <asp:Label ID="lblDaysDelivery" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>出荷日数</th>
                                    <td>
                                        <asp:Label ID="lblDaysShipped" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                  <tr>
                                    <th>お客様組立て</th>
                                    <td>
                                        <asp:Label ID="lblCustomerAssembly" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                  <tr>
                                    <th>引渡方法</th>
                                    <td>
                                        <asp:Label ID="lblDeliveryMethod" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>代引可否</th>
                                    <td>
                                        <asp:Label ID="lblCashDeliveryFree" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>返品可否</th>
                                    <td>
                                        <asp:Label ID="lblPossibilityofReturn" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>公開種別</th>
                                    <td>
                                        <asp:Label ID="lblOpentype" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(市場売価)</th>
                                    <td>
                                        <asp:Label ID="lblMarketSellingPrice" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(サプライヤ名)</th>
                                    <td>
                                        <asp:Label ID="lblSupplierName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(メーカー名)</th>
                                    <td>
                                        <asp:Label ID="lblManufacturerName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(ブランド名)</th>
                                    <td>
                                        <asp:Label ID="lblbrandname" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>サプライヤ品番</th>
                                    <td>
                                        <asp:Label ID="lblSupplierPartNumber" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>最低発注数量</th>
                                    <td>
                                        <asp:Label ID="lblMinimumOrderQuantity" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>最低発注単位</th>
                                    <td>
                                        <asp:Label ID="lblMinimumOrderUnit" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>仕入価格</th>
                                    <td>
                                        <asp:Label ID="lblPurchasePrice" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(送料-全国)</th>
                                    <td>
                                        <asp:Label ID="lblnationwide" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(送料-北海道)</th>
                                    <td>
                                        <asp:Label ID="lblHokkaido" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(送料-沖縄)</th>
                                    <td>
                                        <asp:Label ID="lblokinawa" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(送料-離島)</th>
                                    <td>
                                        <asp:Label ID="lblRemoteIsland" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(直送時代引き)</th>
                                    <td>
                                        <asp:Label ID="lblDirectDeliveryera" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(直送時配送不可地域)</th>
                                    <td>
                                        <asp:Label ID="lblUndeliverableArea" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>梱包質量(kg)</th>
                                    <td>
                                        <asp:Label ID="lblPackingWeight" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>梱包寸法(奥行D)(mm)</th>
                                    <td>
                                        <asp:Label ID="lblPackingDimensionDepth" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>梱包寸法(幅W)(mm)</th>
                                    <td>
                                        <asp:Label ID="lblPackingDimensionWidth" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>梱包寸法(高さH)(mm)</th>
                                    <td>
                                        <asp:Label ID="lblPackingDimensionHeight" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>商品詳細登録コメント</th>
                                    <td>
                                        <asp:Label ID="lblItemDetailRegistrationComment" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(特長)</th>
                                    <td>
                                        <asp:Label ID="lblFeatures" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(用途)</th>
                                    <td>
                                        <asp:Label ID="lblApplication" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(特長)1</th>
                                    <td>
                                        <asp:Label ID="lblFeature1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(画像置き場)</th>
                                    <td>
                                        <asp:Label ID="lblImageStoragePlace" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(画像1)</th>
                                    <td>
                                        <asp:Label ID="lblImage1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(画像1キャプション)</th>
                                    <td>
                                        <asp:Label ID="lblImage1Caption" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(画像2)</th>
                                    <td>
                                        <asp:Label ID="lblImage2" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(画像2キャプション)</th>
                                    <td>
                                        <asp:Label ID="lblImage2Caption" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(画像3)</th>
                                    <td>
                                        <asp:Label ID="lblImage3" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(画像3キャプション)</th>
                                    <td>
                                        <asp:Label ID="lblImage3Caption" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(画像4)</th>
                                    <td>
                                        <asp:Label ID="lblImage4" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(画像4キャプション)</th>
                                    <td>
                                        <asp:Label ID="lblImage4Caption" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>該当法令</th>
                                    <td>
                                        <asp:Label ID="lblApplicableLaw" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>販売許可・認可・届出 </th>
                                    <td>
                                        <asp:Label ID="lblSales" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                  <tr>
                                    <th>(属性名1) </th>
                                    <td>
                                        <asp:Label ID="lblAttributeName1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値1) </th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue1" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名2) </th>
                                    <td>
                                        <asp:Label ID="lblAttributeName2" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値2)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue2" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名3)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName3" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性値3)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue3" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名4)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName4" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値4)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue4" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名5)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName5" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値5)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue5" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性名6)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName6" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値6)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue6" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名7)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName7" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値7)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue7" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名8)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName8" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性値8)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue8" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名9)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName9" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値9)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue9" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性名10)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName10" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値10)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue10" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性名11)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName11" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値11)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue11" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性名12)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName12" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値12)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue12" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名13)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName13" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値13)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue13" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名14)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName14" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値14)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue14" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名15)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName15" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値15)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue15" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性名16)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName16" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値16)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue16" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名17)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName17" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値17)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue17" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性名18)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName18" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性値18)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue18" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性名19)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName19" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(属性値19)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue19" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性名20)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeName20" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>(属性値20)</th>
                                    <td>
                                        <asp:Label ID="lblAttributeValue20" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>危険物の種別</th>
                                    <td>
                                        <asp:Label ID="lblClassification" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>危険物の品名</th>
                                    <td>
                                        <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>危険等級</th>
                                    <td>
                                        <asp:Label ID="lblRiskClass" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>危険物の含有量</th>
                                    <td>
                                        <asp:Label ID="lblContent" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <th>危険物の性質</th>
                                    <td>
                                        <asp:Label ID="lblNature" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                 <tr>
                                    <th>(使用期限)</th>
                                    <td>
                                        <asp:Label ID="lblExpirationDate" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </table>
                        </div>
                    </div>
                </div>
            </div>
</body>    
</asp:Content>
