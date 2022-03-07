<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_Promotionexhibition_Ponpare.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Details_of_Promotionexhibition_Ponpare" %>
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
  <%--  <p id="toTop"><a href="#CmnContents">▲TOP</a></p>--%>


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
				<td>item.csv</td><td><%--<asp:Label ID="lblpitemerror" runat="server" Text=""></asp:Label>--%><asp:TextBox ID="lblpitemerror" runat="server" TextMode="MultiLine" Enabled="false" Width="700px" /></td>
			</tr>
			<tr>
				<td>option.csv</td><td><asp:Label ID="lblpoptionerror" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td>category.csv</td><td><asp:Label ID="lblpcaterror" runat="server" Text=""></asp:Label></td>
			</tr>
			</tbody>
		</table>
<%--  <div id="CmnContents">--%>
 <h2 id="itemcsv" runat="server">item.csv</h2>
 <table  class="editTable"  id="item" runat="server">

 <tr>
 <th>コントロールカラム</th>


 <td class="td">
     <asp:Label ID="lblControlColumn" runat="server" Text=""></asp:Label>
 </td>

 
 </tr>

  
  <tr id="vproducturl" runat="server">
  <th  id="producturl" runat="server">商品管理ID（商品URL）</th>



 <td >
 
     <asp:Label ID="lblProductURL" runat="server" Text=""></asp:Label>
      </td>


 

  </tr>
  
<tr id="vsale" runat="server">
<th id="sale" runat="server">販売ステータス</th>
 <td >
 
     <asp:Label ID="lblSalestatus" runat="server" Text=""></asp:Label>
    </td>

</tr>   
    <tr id="vproductid" runat="server">
    <th  id="productid" runat="server">商品ID</th>

<td>
    <asp:Label ID="lblproductID" runat="server" Text=""></asp:Label>
</td>
</tr> 
  <tr id="vproductno" runat="server">
  <th  id="productno" runat="server" >商品名</th>

 <td >
 
     <asp:Label ID="lblproductNo" runat="server" Text=""></asp:Label>
      </td>

  </tr>
   <tr id="vcatch" runat="server">
   <th id="catch" runat="server" >キャッチコピー</th>
  
    <td>
        <asp:Label ID="lblcatchcopy" runat="server" Text=""></asp:Label>
    </td>
    </tr>
  <tr id="vselling" runat="server">
  <th id="selling" runat="server">販売価格</th>
    
    <td>
        <asp:Label ID="lblsellingprice" runat="server" Text=""></asp:Label>
    </td>
  </tr>
  <tr id="vprice" runat="server">
  <th id="price" runat="server">表示価格</th>

  <td>
      <asp:Label ID="lblindicateprice" runat="server" Text=""></asp:Label>
  </td>
  </tr>
 <tr id="vcontax" runat="server">
 <th id="contax" runat="server">消費税</th>

 <td>
     <asp:Label ID="lblcontax" runat="server" Text=""></asp:Label>
 </td>
 </tr>
    <tr id="vpostage" runat="server">
    <th id="postage" runat="server">送料</th>

     <td>
         <asp:Label ID="lblpostage" runat="server" Text=""></asp:Label></td>
 </tr>
  <tr id="vshipping1" runat="server">
  <th id="shipping1" runat="server">独自送料グループ(1)</th>


 <td class="td">
 
     <asp:Label ID="lblShippinggroup1" runat="server" Text=""></asp:Label>
      </td>
  </tr>
  <tr id="vshipping2" runat="server">
  <th id="shipping2" runat="server">独自送料グループ(2)</th>


 <td class="td">
 
     <asp:Label ID="lblshippinggroup2" runat="server" Text=""></asp:Label>
       </td>

 

  </tr>
  <tr id="veshipping" runat="server">
  <th id="eshipping" runat="server">個別送料</th>

 <td >
 
     <asp:Label ID="lblextrashipping" runat="server" Text=""></asp:Label>
    </td>

</tr> 
 <tr id="vfee" runat="server">
 <th id="fee" runat="server">代引料</th>

     <td>
         <asp:Label ID="lblfee" runat="server" Text=""></asp:Label>
     </td>
 </tr>
   <tr id="vexpancode" runat="server">
   <th id="expancode" runat="server">のし対応</th>

     <td ><asp:Label ID="lblexpanCode" runat="server" Text=""></asp:Label> </td>







 


 
  </tr>
  <tr id="vorder" runat="server">
  <th id="order" runat="server">注文ボタン</th>




 <td >
 
     <asp:Label ID="lblOrderButton" runat="server" Text=""></asp:Label>
       </td>

       </tr>
  <tr id="vinquire" runat="server">
  <th id="inquire" runat="server">商品問い合わせボタン</th>

<td >
 
     <asp:Label ID="lblInquirebutton" runat="server" Text=""></asp:Label>
       </td>

       </tr>
<tr id="vsaleperiod" runat="server">
<th id="saleperiod" runat="server">販売期間指定</th>

    <td>
        <asp:Label ID="lblsaleperiod" runat="server" Text=""></asp:Label>
    </td>
</tr>
 <tr id="vacceptno" runat="server">
 <th id="acceptno" runat="server">注文受付数</th>




 <td>
 
     <asp:Label ID="lblacceptnumberOforder" runat="server" 
         Text="	"></asp:Label>
       </td>

       </tr>

<tr id="vstype" runat="server">
<th id="stype" runat="server">在庫タイプ	</th>




 <td >
 
<asp:Label ID="lblStype" runat="server" Text="	" ></asp:Label>
</td>

</tr>

<tr id="vstockno" runat="server">
<th id="stockno" runat="server">在庫数</th>

 <td >
 
<asp:Label ID="lblStocknumber" runat="server" Text=" "></asp:Label>
</td>

</tr>

<tr id="vstockdisplay" runat="server">
<th id="stockdisplay" runat="server">在庫表示</th>

 <td >
 
<asp:Label ID="lblstockDisplay" runat="server" Text=""></asp:Label>
</td>

</tr>
 <tr id="vprod1" runat="server">
 <th id="prod1" runat="server">商品説明(1)</th>

     <td>
       <div  class="textarea">  
 <%--      <asp:TextBox ID="lblprodesc1" runat="server" Enabled ="false"  TextMode ="MultiLine" Height="150px" width="600px"></asp:TextBox>--%>
           <asp:Label ID="lblprodesc1" runat="server" Text="" Height="150px" width="600px"></asp:Label>
       </div>
        
     </td>
 </tr>     
 <tr id="vprod2" runat="server">
 <th id="prod2" runat="server">商品説明(2)</th>

 <td>
  <div  class="textarea">   
<%--  <asp:TextBox ID="lblprodesc2" runat="server" Enabled="false" Height="150px" width="600px" TextMode ="MultiLine"></asp:TextBox>--%>
      <asp:Label ID="lblprodesc2" runat="server" Text="" Height="150px" width="600px"></asp:Label>
  </div>
     
 </td>
 </tr>
 <tr id="vprodtax" runat="server">
 <th id="prodtax" runat="server">商品説明(テキストのみ)</th>

 <td>
     <asp:TextBox ID="lblprodesctax" runat="server" Enabled ="false" 
         TextMode ="MultiLine" Height="128px" Width="599px"></asp:TextBox>
     
 </td>
 </tr>
  <tr id="vimgurl" runat="server">
  <th id="imgurl" runat="server">商品画像URL</th>

      <td>
          <asp:Label ID="lblimgurl" runat="server" Text=""></asp:Label></td>
  </tr>   
  <tr id="vmallid" runat="server">
  <th id="mallid" runat="server">モールジャンルID</th>

  <td>
      <asp:Label ID="lblmallgID" runat="server" Text=""></asp:Label></td>
  </tr>
  <tr id="vsecretpass" runat="server">
  <th id="secretpass" runat="server">シークレットセールパスワード	</th>

 <td class="td">
 
     <asp:Label ID="lblSecretPassoword" runat="server" Text=""></asp:Label>
    </td>

</tr>  
  <tr id="vpoint" runat="server">
  <th id="point" runat="server">ポイント率</th>

  <td>
      <asp:Label ID="lblpointrate" runat="server" Text=""></asp:Label>
  </td>
  </tr>
  <tr id="vpointperiod" runat="server">
  <th id="pointperiod" runat="server">ポイント率適用期間</th>

  <td>
      <asp:Label ID="lblpointperiod" runat="server" Text=""></asp:Label>
  </td>
  </tr>
  
 <tr id="vhorizontal" runat="server">
 <th id="horizontal" runat="server">SKU横軸項目名</th>

 <td >
 
<asp:Label ID="lblHorizontalItemName" runat="server" Text=""></asp:Label>
</td>

</tr>     
      
 <tr id="vvertical" runat="server">
 <th id="vertical" runat="server">SKU縦軸項目名</th>

 <td >
 
<asp:Label ID="lblVertical" runat="server" Text=""></asp:Label>
</td>

</tr>   
<tr id="vstock" runat="server">
<th id="stock" runat="server">SKU在庫用残り表示閾値</th>
<%-- <td class="td">
 
<asp:Label ID="Label24" runat="server" Text=" "  CssClass="input" ToolTip="SKU for stock remaining display threshold"></asp:Label>
 
    <br />
 
</td--%>
 <td >
 
<asp:Label ID="lblRemainingStock" runat="server" Text=""></asp:Label>
</td>

</tr>      
<tr id="vprophone" runat="server">
<th id="prophone" runat="server">商品説明(スマートフォン用)</th>
<%--<td>
    <asp:Label ID="Label37" runat="server" Text="" ToolTip="Product Description (for smartphones)"></asp:Label>
</td>--%>
<td>
    <asp:Label ID="lblproforphone" runat="server" Text=""></asp:Label>
</td>
</tr>
 <tr id="vjancode" runat="server">
 <th id="jancode" runat="server">JANコード</th>
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

<h2 id="option" runat="server">option.csv</h2>
		<div class="tableCsv" id="campaign" runat="server">
            <asp:GridView ID="gvoption" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" Width="100%" AllowPaging="false" >
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
                </Columns>
            </asp:GridView>
        </div>
<%--</div>--%>
	<h2 id="category" runat="server">category.csv</h2>
		<div class="tableCsv" id="itcat" runat="server">
            <asp:GridView ID="gvcat" runat="server" AutoGenerateColumns="False" 
                EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" ForeColor="#333333" GridLines="None"    Width="100%" 
        AllowPaging="false">
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
              <asp:TemplateField HeaderText="商品管理番号（商品URL）">
            <ItemTemplate>
                <asp:Label ID="lblprourl" runat="server" Text='<%#Eval("商品管理ID（商品URL）")%>'></asp:Label>
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
            </Columns>
            </asp:GridView>

        </div>

</div>

</div>
</div>
</div>
</body>
</asp:Content>
