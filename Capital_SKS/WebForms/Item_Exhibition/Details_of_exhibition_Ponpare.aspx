<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_exhibition_Ponpare.aspx.cs" Inherits="ORS_RCM.Details_of_exhibition__Ponpare_" %>
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

<!-- Fixed Value -->
	<div class="setDetailBox shopCmnSet defaultSet iconSet iconPon exbCmnSet">
		<h1>ポンパレ出品詳細</h1>

		<ul class="pageLink">
			<li><a href="#error">エラーメッセージ</a></li>
			<li><a href="#item-csv">item.csv</a></li>
			<li><a href="#option-csv">option.csv</a></li>
			<li><a href="#category-csv">category.csv</a></li>
		</ul>

		<h2 id="error">エラーメッセージ</h2>
		<table class="errorBox">
			<tbody>
			<tr>
				<td>出品結果</td><td><%--<asp:Label ID="lblpitemerror" runat="server" Text=""></asp:Label>--%><asp:TextBox ID="lblpitemerror" runat="server" TextMode="MultiLine" Enabled="false" Width="700px" /></td>
			</tr>
			<tr>
				<td>APIチェック</td><td><asp:Label ID="lblpoptionerror" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td>バッチチェック</td><td><asp:Label ID="lblpcaterror" runat="server" Text=""></asp:Label></td>
			</tr>
			</tbody>
		</table>
   <%-- <div id="CmnContents">--%>
 <h2 id="item-csv">item.csv</h2>
 <table  class="editTable">

 <tr>
 <th>コントロールカラム</th>


 <td class="td">
     <asp:Label ID="lblControlColumn" runat="server" Text=""></asp:Label>
 </td>

 
 </tr>

  
  <tr>
  <th>商品管理ID（商品URL）</th>



 <td >
 
     <asp:Label ID="lblProductURL" runat="server" Text=""></asp:Label>
      </td>


 

  </tr>
  
<tr>
<th>販売ステータス</th>
 <td >
 
     <asp:Label ID="lblSalestatus" runat="server" Text=""></asp:Label>
    </td>

</tr>   
    <tr>
    <th>商品ID</th>

<td>
    <asp:Label ID="lblproductID" runat="server" Text=""></asp:Label>
</td>
</tr> 
  <tr>
  <th>商品名</th>

 <td >
 
     <asp:Label ID="lblproductNo" runat="server" Text=""></asp:Label>
      </td>

  </tr>
   <tr>
   <th>キャッチコピー</th>
  
    <td>
        <asp:Label ID="lblcatchcopy" runat="server" Text=""></asp:Label>
    </td>
    </tr>
  <tr>
  <th>販売価格</th>
    
    <td>
        <asp:Label ID="lblsellingprice" runat="server" Text=""></asp:Label>
    </td>
  </tr>
  <tr>
  <th>表示価格</th>

  <td>
      <asp:Label ID="lblindicateprice" runat="server" Text=""></asp:Label>
  </td>
  </tr>
 <tr>
 <th>消費税</th>

 <td>
     <asp:Label ID="lblcontax" runat="server" Text=""></asp:Label>
 </td>
 </tr>
    <tr>
    <th>送料</th>

     <td>
         <asp:Label ID="lblpostage" runat="server" Text=""></asp:Label></td>
 </tr>
  <tr>
  <th>独自送料グループ(1)</th>


 <td class="td">
 
     <asp:Label ID="lblShippinggroup1" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr>
  <th>独自送料グループ(2)</th>


 <td class="td">
 
     <asp:Label ID="lblshippinggroup2" runat="server" Text=""></asp:Label>
       </td>

 

  </tr>
  <tr>
  <th>個別送料</th>

 <td >
 
     <asp:Label ID="lblextrashipping" runat="server" Text=""></asp:Label>
    </td>

</tr> 
 <tr>
 <th>代引料</th>

     <td>
         <asp:Label ID="lblfee" runat="server" Text=""></asp:Label>
     </td>
 </tr>
   <tr>
   <th>のし対応</th>

     <td ><asp:Label ID="lblexpanCode" runat="server" Text=""></asp:Label> </td>







 


 
  </tr>
  <tr>
  <th>注文ボタン</th>




 <td >
 
     <asp:Label ID="lblOrderButton" runat="server" Text=""></asp:Label>
       </td>

       </tr>
  <tr>
  <th>商品問い合わせボタン</th>

<td >
 
     <asp:Label ID="lblInquirebutton" runat="server" Text=""></asp:Label>
       </td>

       </tr>
<tr>
<th>販売期間指定</th>

    <td>
        <asp:Label ID="lblsaleperiod" runat="server" Text=""></asp:Label>
    </td>
</tr>
 <tr>
 <th>注文受付数</th>




 <td>
 
     <asp:Label ID="lblacceptnumberOforder" runat="server" 
         Text="	"></asp:Label>
       </td>

       </tr>

<tr>
<th>在庫タイプ	</th>




 <td >
 
<asp:Label ID="lblStype" runat="server" Text="	" ></asp:Label>
</td>

</tr>

<tr>
<th>在庫数</th>

 <td >
 
<asp:Label ID="lblStocknumber" runat="server" Text=" "></asp:Label>
</td>

</tr>

<tr>
<th>在庫表示</th>

 <td >
 
<asp:Label ID="lblstockDisplay" runat="server" Text=""></asp:Label>
</td>

</tr>
 <tr>
 <th>商品説明(1)</th>

     <td>
       <div  class="textarea">  
 <%--      <asp:TextBox ID="lblprodesc1" runat="server" Enabled ="false"  TextMode ="MultiLine" Height="150px" width="600px"></asp:TextBox>--%>
           <asp:Label ID="lblprodesc1" runat="server" Text="" Height="150px" width="600px"></asp:Label>
       </div>
        
     </td>
 </tr>     
 <tr>
 <th>商品説明(2)</th>

 <td>
  <div  class="textarea">   
<%--  <asp:TextBox ID="lblprodesc2" runat="server" Enabled="false" Height="150px" width="600px" TextMode ="MultiLine"></asp:TextBox>--%>
      <asp:Label ID="lblprodesc2" runat="server" Text="" Height="150px" width="600px"></asp:Label>
  </div>
     
 </td>
 </tr>
 <tr>
 <th>商品説明(テキストのみ)</th>

 <td>
     <asp:TextBox ID="lblprodesctax" runat="server" Enabled ="false" 
         TextMode ="MultiLine" Height="128px" Width="599px"></asp:TextBox>
     
 </td>
 </tr>
  <tr>
  <th>商品画像URL</th>

      <td>
          <asp:Label ID="lblimgurl" runat="server" Text=""></asp:Label></td>
  </tr>   
  <tr>
  <th>モールジャンルID</th>

  <td>
      <asp:Label ID="lblmallgID" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr>
  <th>シークレットセールパスワード	</th>

 <td class="td">
 
     <asp:Label ID="lblSecretPassoword" runat="server" Text=""></asp:Label>
    </td>

</tr>  
  <tr>
  <th>ポイント率</th>

  <td>
      <asp:Label ID="lblpointrate" runat="server" Text=""></asp:Label>
  </td>
  </tr>
  <tr>
  <th>ポイント率適用期間</th>

  <td>
      <asp:Label ID="lblpointperiod" runat="server" Text=""></asp:Label>
  </td>
  </tr>
  
 <tr>
 <th>SKU横軸項目名</th>

 <td >
 
<asp:Label ID="lblHorizontalItemName" runat="server" Text=""></asp:Label>
</td>

</tr>     
      
 <tr>
 <th>SKU縦軸項目名</th>

 <td >
 
<asp:Label ID="lblVertical" runat="server" Text=""></asp:Label>
</td>

</tr>   
<tr>
<th>SKU在庫用残り表示閾値</th>
<%-- <td class="td">
 
<asp:Label ID="Label24" runat="server" Text=" "  CssClass="input" ToolTip="SKU for stock remaining display threshold"></asp:Label>
 
    <br />
 
</td--%>
 <td >
 
<asp:Label ID="lblRemainingStock" runat="server" Text=""></asp:Label>
</td>

</tr>      
<tr>
<th>商品説明(スマートフォン用)</th>
<%--<td>
    <asp:Label ID="Label37" runat="server" Text="" ToolTip="Product Description (for smartphones)"></asp:Label>
</td>--%>
<td>
    <asp:Label ID="lblproforphone" runat="server" Text=""></asp:Label>
</td>
</tr>
 <tr>
 <th>JANコード</th>
<%-- <td class="td">
 
<asp:Label ID="Label25" runat="server" Text=""  CssClass="input" ToolTip="JAN code"></asp:Label>
 
</td>--%>
 <td >
 
<asp:Label ID="lblJunCode" runat="server" Text=""></asp:Label>
</td>

</tr>    

     <%-- <tr>
 <td class="td">
 
     <asp:Label ID="Label15" runat="server" Text="
送料区分1  Shipping category1" CssClass="input"></asp:Label>
 
      </td>



 <td class="td">
 
     <asp:Label ID="lblshippingcategory1" runat="server" Text="lblshippingcategory1"></asp:Label>
       </td>

       </tr>
        <tr>
 <td class="td">
 
     <asp:Label ID="Label17" runat="server" Text="
送料区分2  Shipping category2" CssClass="input"></asp:Label>
 
      </td>



 <td class="td">
 
     <asp:Label ID="lblshippingcategory2" runat="server" Text="lblshippingcategory2"></asp:Label>
       </td>

       </tr>--%>
<%--<tr>
 <td class="td">
 
     <asp:Label ID="lbldelivery" runat="server" Text="配達料"></asp:Label>
    </td>
 <td class="td">
 
     <asp:Label ID="lblDeliveryCharges" runat="server" Text="lbldeliveryCharges"></asp:Label>
    </td>

</tr> --%>
</table>

<h2 id="option-csv">option.csv</h2>
		<div class="tableCsv">
            <asp:GridView ID="gvoption" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True">
                <Columns>
         <%--       <asp:TemplateField HeaderText="ID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="Label15" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>--%>
             
                 <asp:TemplateField  HeaderText="コントロールカラム">
                <ItemTemplate>
                    <asp:Label ID="lblitemctrlcol" runat="server" Text='<%#Eval("コントロールカラム")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="商品管理ID（商品URL）">
                <ItemTemplate>
                    <asp:Label ID="lblprourl" runat="server" Text='<%#Eval("商品管理ID（商品URL）")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="選択肢タイプ">
                <ItemTemplate>
                    <asp:Label ID="lblchoicetype" runat="server" Text='<%#Eval("選択肢タイプ")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="購入オプション名">
                <ItemTemplate>
                    <asp:Label ID="lblchkitem" runat="server" Text='<%#Eval("購入オプション名")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="オプション項目名">
                <ItemTemplate>
                    <asp:Label ID="lblckchoice" runat="server" Text='<%#Eval("オプション項目名")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="SKU横軸項目ID">
                <ItemTemplate>
                    <asp:Label ID="lblchsthraxis" runat="server" Text='<%#Eval("SKU横軸項目ID")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="SKU横軸項目名">
                <ItemTemplate>
                    <asp:Label ID="lblsentakoshicono" runat="server" Text='<%#Eval("SKU横軸項目名")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="SKU縦軸項目ID">
                <ItemTemplate>
                    <asp:Label ID="lblvaeraxichoice" runat="server" Text='<%#Eval("SKU縦軸項目ID")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="SKU縦軸項目名">
                <ItemTemplate>
                    <asp:Label ID="lblvertscokoshino" runat="server" Text='<%#Eval("SKU縦軸項目名")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                 <asp:TemplateField  HeaderText="SKU在庫数">
                <ItemTemplate>
                    <asp:Label ID="lblstockforoption" runat="server" Text='<%#Eval("SKU在庫数")%>'></asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
                <%-- <asp:TemplateField  HeaderText="項目選択肢別在庫用在庫数">
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
                </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
<%--</div>--%>
	<h2 id="category-csv">category.csv</h2>
		<div class="tableCsv">
            <asp:GridView ID="gvcat" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True">
            <Columns>
    <%--        <asp:TemplateField HeaderText="ID" Visible="false">
            <ItemTemplate>
                <asp:Label ID="Label38" runat="server" Text='<%#Eval("ID")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>--%>

            <asp:TemplateField HeaderText="コントロールカラム">
            <ItemTemplate>
                <asp:Label ID="lblctrlcol" runat="server" Text='<%#Eval("コントロールカラム")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="商品管理ID（商品URL）">
            <ItemTemplate>
                <asp:Label ID="lblprourl" runat="server" Text='<%#Eval("商品管理ID（商品URL）")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="商品名">
            <ItemTemplate>
                <asp:Label ID="lblproname" runat="server" Text='<%#Eval("商品名")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="ショップ内カテゴリ">
            <ItemTemplate>
                <asp:Label ID="lbldestinationcat" runat="server" Text='<%#Eval("ショップ内カテゴリ")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="表示順位">
            <ItemTemplate>
                <asp:Label ID="lblpriority" runat="server" Text='<%#Eval("表示順位")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <%--<asp:TemplateField HeaderText="URL">
            <ItemTemplate>
                <asp:Label ID="lblurl" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="1ページ複数形式">
            <ItemTemplate>
                <asp:Label ID="lblmultiplepage" runat="server" Text=""></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>--%>
            </Columns>
            </asp:GridView>

        </div>

	<form action="#" method="get">
			<div class="btn"><p><input type="button" value="出品取消">&nbsp;&nbsp;<input type="button" value="編集画面へ"></p></div>
		</form>

</div>

</div>
</div>
</div>
</body>
</asp:Content>

         


 


 
 












