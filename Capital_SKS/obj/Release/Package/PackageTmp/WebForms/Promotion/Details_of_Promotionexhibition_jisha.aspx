<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_Promotionexhibition_jisha.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Details_of_Promotionexhibition_jisha" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
  <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />

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
				<td>item.csv</td><td>
                    <asp:Label ID="lbljitemerror" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td>select.csv</td><td>
                    <asp:Label ID="lbljselecterror" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td>item-cat.csv</td><td>
                    <asp:Label ID="lbljcaterror" runat="server" Text=""></asp:Label></td>
			</tr>
			</tbody>
		</table>

 
 
 <h2 id="item-csv">item.csv</h2>

 <table  class="editTable">

 <tbody>
 <tr>
 <th>コントロールカラム</th>
 
 <td class="td">
     <asp:Label ID="lblControlColumn" runat="server" Text=""></asp:Label>
 </td>

 </tr>

  
  <tr>
  <th>商品管理番号（商品URL）</th>
  <td >
 
     <asp:Label ID="lblProductURL" runat="server" Text=""></asp:Label>
      </td>
</tr>
  
<tr>
<th>商品番号</th>
 <td >
<asp:Label ID="lblItemCode" runat="server" Text=""></asp:Label>
</td>

</tr>   
    <tr id="pdire" runat="server">
    <th>あす楽配送管理番号</th>
<td>
    <asp:Label ID="lblProductsdirectory_ID" runat="server" Text=""></asp:Label>
</td>
</tr> 
  <tr id="vname" runat="server" >
  <th id="name" runat="server">商品名</th>
  <td>   <asp:Label ID="lblproductname" runat="server" Text=""></asp:Label></td>
  </tr>

   <tr id="vpccatch" runat="server" >
   <th id="pccatch" runat="server">PC用キャッチコピー</th>

 <td  >
 
     <asp:Label ID="lblPCcatch" runat="server"  Text=""></asp:Label>
      </td>


</tr>
  <tr id="vmobilecatch" runat="server" >
  <th id="mobilecatch" runat="server">スマートフォン用キャッチコピー</th>

 <td >
 
     <asp:Label ID="lblmobilecatch" runat="server" Text=""></asp:Label>
      </td>
 
 </tr>
 <tr id="vsmart" runat="server" >
 <th id="lblsmart" runat="server">スマートフォン用商品説明文</th>

 <td>
     <asp:Label ID="lblsmartdesc" runat="server" Text=""></asp:Label>
 </td>
 </tr>
 <tr id="vpcdesc" runat="server" >
 <th id="lblpcdesc" runat="server">PC用商品説明文</th>

 <td>
     <asp:Label ID="lblpcitdescription" runat="server" Text=""></asp:Label></td>
 </tr>
 <tr id="vsaldesc" runat="server" >
 <th id="lblsaldesc" runat="server">PC用販売説明文</th>

 <td>
     <asp:Label ID="lblsaledescpc" runat="server" Text=""></asp:Label></td>
 </tr>
 <tr id="vpassword" runat="server" >
 <th id="lblpassword" runat="server">闇市パスワード</th>

 <td >
 
     <asp:Label ID="lblblackpass" runat="server" Text=""></asp:Label>
      </td>
 
  </tr>
   </tbody>


    


    


</table>


<h2 id="option-csv">select.csv</h2>
		<div class="tableCsv">
            <asp:GridView ID="gvselect" runat="server" AutoGenerateColumns="False" 
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
                
                </Columns>
            </asp:GridView>
        </div>

	<h2 id="category" runat="server">item-cat.csv</h2>
		<div  id ="tbcat"  runat="server" class="tableCsv">
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
              <asp:TemplateField HeaderText="表示先カテゴリ">
            <ItemTemplate>
                <asp:Label ID="lblproname" runat="server" Text='<%#Eval("表示先カテゴリ")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="優先度">
            <ItemTemplate>
                <asp:Label ID="lbldestinationcat" runat="server" Text='<%#Eval("優先度")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="1ページ複数形式">
            <ItemTemplate>
                <asp:Label ID="lblpriority" runat="server" Text='<%#Eval("1ページ複数形式")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="カテゴリセット管理番号">
            <ItemTemplate>
                <asp:Label ID="lblurl" runat="server" Text='<%#Eval("カテゴリセット管理番号")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField HeaderText="カテゴリセット名">
            <ItemTemplate>
                <asp:Label ID="lblmultiplepage" runat="server" Text='<%#Eval("カテゴリセット名")%>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            </asp:GridView>

        </div>



</div>

</div>
</div>
</asp:Content>
