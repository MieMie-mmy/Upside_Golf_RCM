<%@ Page Title="商品管理システム＜出品詳細＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_exhibition_Yahoo.aspx.cs" Inherits="ORS_RCM.Details_of_exhibition_Yahoo_" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<link href="../../Scripts/jquery.page-scroller.js" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <body>
<div id="CmnWrapper">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
 <div id="CmnContents">
	<div id="ComBlock">
    <div class="setDetailBox shopCmnSet defaultSet iconSet iconYahoo exbCmnSet">
		<h1>ヤフー出品詳細</h1>
        <ul class="pageLink">
			<li><a href="#error">エラーメッセージ</a></li>
			<li><a href="#data_add-csv">data_add.csv</a></li>
			<li><a href="#quantity-csv">quantity.csv</a></li>
		</ul>
		<h2 id="error">エラーメッセージ</h2>
		<table class="errorBox">
			<tbody>
			<%--<tr>
				<td>バッチチェック</td><td><asp:TextBox ID="lblyitemerror" runat="server" TextMode="MultiLine" Enabled="false" Width="700px" /></td>
			</tr>--%>
			<tr>
				<td>APIチェック</td><td><asp:Label ID="lblyselecterror" runat="server" Text=""></asp:Label></td>
			</tr>
            <tr>
				<td>バッチチェック</td><td><asp:TextBox ID="lblyitemerror" runat="server" TextMode="MultiLine" Enabled="false" Width="700px" /></td>
			</tr>
		<%--	<tr>
				<td>item-cat.csv</td><td> <asp:Label ID="lblycaterror" runat="server" Text=""></asp:Label></td>
			</tr>--%>
			</tbody>
		</table>
        <h2 id="data_add-csv">data_add.csv</h2>
 <table class="editTable">
<%-- <tr>
 <td>
 <asp:Label ID="Label2" runat="server" Text="ショップ名"></asp:Label>
 </td>
 <td>
     <asp:Label ID="lblShopName" runat="server" Text=""></asp:Label>
 </td>
  </tr>--%>
 <tr>
 <th>path<span>(パス)</span></th>
<%--<td>
<asp:Label ID="Label6" runat="server" Text="path(パス)"></asp:Label>
 </td>--%>
 <td>
    <%-- <asp:Label ID="lblpath" runat="server" Text=""></asp:Label>--%>
     <asp:TextBox ID="lblpath" runat="server" TextMode="MultiLine" Width="700px" Enabled="false"></asp:TextBox>
 </td>
 </tr>
 <tr>
 	<th>name<span>(商品名)</span></th>
 <td>
     <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  	<th>code<span>(商品コード)</span></th>
 <td>
     <asp:Label ID="lblItemCode" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  <th>sub-code<span>(個別商品コード)</span></th>
 <td class="td">
     <asp:Label ID="lblSubCode" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  <th>subcode_param<span>(個別商品コードパラメータ)</span></th>
 <td class="td">
     <asp:Label ID="lblsubcodeparam" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  <th>original-price<span>(定価)</span></th>
 <td class="td">
     <asp:Label ID="lblOriginalPrice" runat="server" Text="" 
         ></asp:Label>
       </td>
  </tr>
 <tr>
 <th>price<span>(通常販売価格)</span></th>
     <td class="td"><asp:Label ID="lblregularprice" runat="server" Text="" ></asp:Label> </td>
  </tr>
 <tr>
 <th>sale-price<span>(特価)</span></th>
 <td class="style1">
     <asp:Label ID="lblSaleprice" runat="server" Text=""></asp:Label>
       </td>
       </tr>
       <tr>
	<th>options<span>(オプション)</span></th>
 <td class="td">
     <asp:Label ID="lbloption" runat="server" Text="" ></asp:Label>
       </td>
       </tr>
<tr>
	<th>headline<span>(キャッチコピー)</span></th>
<td>
    <asp:Label ID="lblheadline" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
	<th>caption<span>(商品説明)</span></th>
<td>
    <asp:Label ID="lblcaption" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<th>abstract<span>(ひと言コメント)</span></th>
<td>
    <asp:Label ID="lblabstract" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
<th>explanation<span>(商品情報)</span></th>
<td>
    <asp:Label ID="lblexplanation" runat="server" Text=""></asp:Label>
</td>
</tr>
<tr>
<th>additional1<span>(フリースペース)</span></th>
 <td >
  <div  class="textarea">
<%--  <asp:TextBox ID="lbladdition1" runat="server" Enabled="false" Height="150px" width="600px" TextMode="MultiLine"></asp:TextBox>--%>
<asp:Label ID="lbladdition1" runat="server"  Height="150px" width="600px" ></asp:Label></div>  
</td>
</tr>
<tr>
 <th>additional2<span>(フリースペース)</span></th>
 <td class="td">
   <div  class="textarea">   
 <%--  <asp:TextBox ID="lbladdition2" runat="server" Enabled="false" Height="150px" width="600px" TextMode="MultiLine"></asp:TextBox>--%>
<asp:Label ID="lbladdition2" runat="server" Text=" " Height="150px" width="600px" ></asp:Label></div>
</td>
</tr>
 <tr>
 <th>additional3<span>(フリースペース)</span></th>
 <td class="td">
 <div  class="textarea">     
<%-- <asp:TextBox ID="lbladdition3" runat="server" TextMode="MultiLine" 
         Enabled="false" Height="150px" width="600px"></asp:TextBox>--%>
<asp:Label ID="lbladdition3" runat="server" Text="" Height="150px" width="600px"></asp:Label></div>
</td>
</tr> 
<tr>
<th>relevant-links<span>(おすすめ商品)</span></th>
 <td class="td">
<asp:Label ID="lblRelevantLink" runat="server" Text="" ></asp:Label>
</td>
</tr>
<tr>
 <th>ship-weight<span>(重量)</span></th>
 <td class="td">
<asp:Label ID="lblWeight" runat="server" Text=""></asp:Label>
</td>
</tr>
  <tr>
<th>taxable<span>(課税対象)</span></th>
 <td class="td">
<asp:Label ID="lblTaxable" runat="server" Text=""></asp:Label>
</td>
</tr>  
<tr>
	<th>release-date<span>(発売日)</span></th>
 <td class="td">
     <asp:Label ID="lblReleaseDate" runat="server" Text=""></asp:Label>
    </td>
</tr>
<tr>
 	<th>temporary-point-term<span>(仮ポイント期間)</span></th>
 <td class="td">
     <asp:Label ID="lblTemporarypoint" runat="server" Text=""  ></asp:Label>
    </td>
</tr>   
<tr>
 <th>point-code<span>(ポイント倍率)</span></th>
 <td class="td">
     <asp:Label ID="lblpointCode" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 <th>meta-key<span>(META keywords)</span></th>
 <td class="style3">
     <asp:Label ID="lblmetaKeyword" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
<th>meta-desc<span>(META description)</span></th>
 <td class="td">
     <asp:Label ID="lblmetaDescription" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 <th>template<span>(使用中のテンプレート)</span></th>
 <td class="style3">
     <asp:Label ID="lblTemplate" runat="server" Text=""></asp:Label>
    </td>
</tr> 
<tr>
<th>sale-period-start<span>(販売期間（開始日）)</span></th>
 <td class="style3">
     <asp:Label ID="lblSalePeriodStart" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 <th>sale-period-end<span>(販売期間（終了日）)</span></th>
 <td class="td">
     <asp:Label ID="lblSaleperiodEnd" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
  <tr>
 <th>sale-limit<span>(購入数制限)</span></th>
 <td>
   <div  class="textarea">   <asp:TextBox ID="lblSaleLimit" runat="server" TextMode="MultiLine" ReadOnly="true" Height="150px" width="600px"></asp:TextBox></div>
 <%--    <asp:Label ID="lblSaleLimit" runat="server" Text="" 
      ></asp:Label>--%>
    </td>
</tr> 
    <tr>
<th>sp-code<span>(販促コード)</span></th>
    <td>
    <div  class="textarea">     <asp:TextBox ID="lblspcode" runat="server" TextMode="MultiLine" ReadOnly="true" Height="150px" width="600px"></asp:TextBox></div>
 <%--       <asp:Label ID="lblspcode" runat="server" Text=""></asp:Label></td>--%>
    </tr>
      <tr>
 <th>brand-code<span>(ブランドコード)</span></th>
 <td class="td">
     <asp:Label ID="lblBrandCode" runat="server" Text=""></asp:Label>
    </td>
</tr> 
<tr>
 <th>person-code<span>(人物コード)</span></th>
 <td class="td">
     <asp:Label ID="lblPersonCode" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 <th>yahoo-product-code<span>(Yahoo!ショッピング製品コード)</span></th>
 <td class="td">
     <asp:Label ID="lblYahooCode" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
    <tr>
 <th>product-code<span>(製品コード)</span></th>
 <td class="td">
     <asp:Label ID="lblProductCode" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
<th>jan<span>(JANコード/ISBNコード)</span></th>
 <td class="style3">
     <asp:Label ID="lblJanCode" runat="server" Text=""></asp:Label>
    </td>
</tr>
<tr>
 	<th>isbn<span>(2013年8月「jan」に統合)</span></th>
 <td class="td">
     <asp:Label ID="lblIsbnCode" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
 <tr>
 <th>delivery<span>(送料無料)</span></th>
 <td class="td">
     <asp:Label ID="lbldelivery" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 	<th>astk-code<span>(翌日配達「あすつく」)</span></th>
 <td class="td">
     <asp:Label ID="lblastk_code" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
 <tr>
 <th>condition<span>(商品の状態)</span></th>
 <td class="td">
     <asp:Label ID="lblCondition" runat="server" Text=""></asp:Label>
    </td>
</tr> 
<tr>
<th>taojapan<span>(淘日本への掲載)</span></th>
<td>
    <asp:Label ID="lbltao" runat="server" Text=""></asp:Label></td>
</tr>
<tr>
<th>product-category<span>(プロダクトカテゴリ)</span></th>
<td>
    <asp:Label ID="lblproductcat" runat="server" Text=""></asp:Label></td>
</tr>
 <tr>
 <th>spec1<span>(スペック)</span></th>
 <td class="style3">
     <asp:Label ID="lblspec1" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 <th>spec2<span>(スペック)</span></th>
 <td class="td">
     <asp:Label ID="lblspec2" runat="server" Text=""></asp:Label>
    </td>
</tr> 
<tr>
<th>spec3<span>(スペック)</span></th>
 <td class="td">
     <asp:Label ID="lblspec3" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 	<th>spec4<span>(スペック)</span></th>
 <td class="td">
     <asp:Label ID="lblspec4" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
<th>spec5<span>(スペック)</span></th>
 <td class="td">
     <asp:Label ID="lblspec5" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 <th>display<span>(ページ公開)</span></th>
 <td class="td">
     <asp:Label ID="lblDisplay" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 <th>sort<span>(商品表示順序)</span></th>
 <td class="style3">
     <asp:Label ID="lblSort" runat="server" Text="" ></asp:Label>
    </td>
</tr> 
<tr>
 <th>sp-additional<span>(スマートフォン用フリースペース)</span></th>
 <td class="td">
     <div  class="textarea"><asp:Label ID="lblspadditional" runat="server"  Height="150px" width="600px" ></asp:Label></div>
    <%-- <asp:Label ID="lblspadditional" runat="server" Text="" ></asp:Label>--%>
    </td>
</tr> 
<%--
      <tr>
 <td class="style1">
     <asp:Label ID="Label15" runat="server" Text="購入数制限" CssClass="input"></asp:Label>
      &nbsp;Limit the number of purchases</td>
 <td class="td">
 
     <asp:Label ID="lblnoOFpurchase" runat="server" Text="" 
         ></asp:Label>
       </td>
       </tr>--%>
 <%--<tr>
 <td class="style1">
 
     <asp:Label ID="Label17" runat="server" Text="
商品説明" CssClass="input"></asp:Label>
      </td>
 <td class="style1">
     <asp:Label ID="lblproductDescri" runat="server" 
         Text="lblproductDescription" ForeColor="#FF6600"></asp:Label>
       </td>
       </tr>
   <tr>
 <td class="style1">
     <asp:Label ID="Label18" runat="server" Text="ひと言コメント" CssClass="input"></asp:Label>
     &nbsp;word_Comment</td>
 <td class="style1">
     <asp:Label ID="lblcomment" runat="server" 
         Text="lblcomment" ForeColor="#FF6600"></asp:Label>
       </td>
       </tr>
<tr>
 <td class="style1">
<asp:Label ID="Label20" runat="server" Text="商品情報			"  CssClass="input"></asp:Label>
     </td>
 <td class="td">
<asp:Label ID="lblproductInformation" runat="server" Text="lblproductinformation" 
         Visible="False" ForeColor="#FF9933"></asp:Label>
</td>
</tr>--%>
 <%-- <tr>
 <td class="style1">
     <asp:Label ID="Label31" runat="server" Text="淘日本への掲載"></asp:Label>
     &nbsp;Listings to 淘 Japan<br />
     <br />
    </td>
 <td class="td">
     <asp:Label ID="lblListToJapan" runat="server" Text=""></asp:Label>
    </td>
</tr> --%>
<%--<tr>
 <td>
     <asp:Label ID="Label8" runat="server" Text="ブランドコード"></asp:Label>
     &nbsp;<br />
     <br />
    </td>
 <td class="td">
     <asp:Label ID="Label19" runat="server" Text="lblBrandCode" 
         ForeColor="#FF6600"></asp:Label>
    </td>
</tr> --%>
</table>
<h2 id="quantity-csv">quantity.csv</h2>
		<div class="tableCsv csvYahoo">
            <asp:GridView ID="gvquantity" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True">
                <Columns>
       <%--         <asp:TemplateField HeaderText="ID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>--%>
                  <asp:TemplateField HeaderText="code(商品コード)">
                <ItemTemplate>
                    <asp:Label ID="Label17" runat="server" Text='<%#Eval("code")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="sub-code(個別商品コード)">
                <ItemTemplate>
                    <asp:Label ID="Label18" runat="server" Text='<%#Eval("sub-code")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="quantity(在庫数)">
                <ItemTemplate>
                    <asp:Label ID="Label19" runat="server" Text='<%#Eval("quantity")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                  <asp:TemplateField HeaderText="mode(種別)">
                <ItemTemplate>
                    <asp:Label ID="Label20" runat="server" Text='<%#Eval("mode")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
</div>
</div>
</div>
</div>
</body>
</asp:Content>