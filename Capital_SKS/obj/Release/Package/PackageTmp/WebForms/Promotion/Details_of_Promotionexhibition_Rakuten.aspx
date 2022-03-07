<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_Promotionexhibition_Rakuten.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Details_of_Promotionexhibition_Rakuten" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
  <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />

 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <body>
    <div id="CmnWrapper">
  <p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">

<!-- Fixed Value -->
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
				<td>item.csv</td>
                <td>
                <asp:TextBox ID="lblitemerror" runat="server" TextMode="MultiLine" Enabled="false" Width="700px" />
                    <%--<asp:Label ID="lblitemerror" runat="server" Text=""></asp:Label>--%></td>
			</tr>
			<tr>
				<td>select.csv</td>
                <td>
                    <asp:Label ID="lblselecterror" runat="server" Text=""></asp:Label></td>
			</tr>
			<tr>
				<td>item-cat.csv</td>
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
     <asp:Label ID="lblcontrol" runat="server" Text="" ></asp:Label>
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
 
     <asp:Label ID="lblproductNo" runat="server" Text=""></asp:Label>
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
  <th id="mobilecatch" runat="server">モバイル用キャッチコピー</th>

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
  <tr id="vpointmag" runat="server" >
  <th id ="pointmag" runat="server" visible="false">ポイント変倍率</th>
  <td>
      <asp:Label ID="lblpointmag" runat="server" Text="" Visible="false"></asp:Label></td>
  </tr>
    <tr id="vpointperiod" runat="server" >
  <th id ="pointperiod" runat="server" visible="false">ポイント変倍率適用期間</th>
  <td>
      <asp:Label ID="lblpointperiod" runat="server" Text="" Visible="false"></asp:Label></td>
  </tr>
    <tr id="vdelmagno" runat="server" >
  <th id ="delmagno" runat="server" visible="false">あす楽配送管理番号</th>
  <td>
      <asp:Label ID="lbldelivemngno" runat="server" Text="" Visible="false"></asp:Label></td>
  </tr>
    </table>

  
    

       <h2 id="item2" runat="server" visible="false">item.csv</h2>
   <div class="tableCsv" id="pointcsv" runat="server" visible="false">
    <asp:GridView ID="gvpointitem" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" EmptyDataText="There is no data to display!" 
        ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" 
        Width="100%" 
        AllowPaging="False" >

          <Columns>
  
                 <asp:TemplateField HeaderText="コントロールカラム">
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
            <asp:Label ID="Label13" runat="server" Text='<%#Eval("商品番号")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="PC用キャッチコピー ">
        <ItemTemplate>
            <asp:Label ID="Label14" runat="server" Text='<%#Eval("PC用キャッチコピー")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="モバイル用キャッチコピー">
        <ItemTemplate>
            <asp:Label ID="Label15" runat="server" Text='<%#Eval("モバイル用キャッチコピー")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="商品名">
        <ItemTemplate>
            <asp:Label ID="Label16" runat="server" Text='<%#Eval("商品名")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        
            
     </Columns>
    
   
    </asp:GridView>
    </div>



  




  



  

 
 
   
   <h2 id="selectcsv" runat="server">select.csv</h2>
   <div class="tableCsv" id="campaign" runat="server">
    <asp:GridView ID="gvselect" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" EmptyDataText="There is no data to display!" 
        ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" 
    Width="100%" 
        AllowPaging="False" >

          <Columns>
  
                 <asp:TemplateField HeaderText="項目選択肢用コントロールカラム">
                 <HeaderStyle Wrap="true" />
        <ItemTemplate>
            <asp:Label ID="Label12" runat="server" Text='<%#Eval("項目選択肢用コントロールカラム")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="商品管理番号（商品URL）">
            <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label11" runat="server" Text='<%#Eval("商品管理番号（商品URL）")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
    
            <asp:TemplateField HeaderText="選択肢タイプ" >
             <HeaderStyle  Wrap="true"/>
        <ItemTemplate>
            <asp:Label ID="Label13" runat="server" Text='<%#Eval("選択肢タイプ")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
           <asp:TemplateField HeaderText="Select/Checkbox用項目名 ">
        <ItemTemplate>
            <asp:Label ID="Label14" runat="server" Text='<%#Eval("Select/Checkbox用項目名")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            <asp:TemplateField HeaderText="Select/Checkbox用選択肢">
        <ItemTemplate>
            <asp:Label ID="Label15" runat="server" Text='<%#Eval("Select/Checkbox用選択肢")%>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
            
       
     </Columns>
    
   
    </asp:GridView>
    </div>

 <h2 id="itemcat" runat="server" visible="false">item-cat.csv</h2>
   <div class="tableCsv" id="itcat" runat="server" visible="false">
    <asp:GridView ID="gvitemcat" runat="server" AutoGenerateColumns="true" 
        CellPadding="4" EmptyDataText="There is no data to display!" 
        ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" 
        Width="100%" 
        AllowPaging="True" >

         
    
   
    </asp:GridView>
    </div>



</div>
</div><!--ComBlock-->

</div><!--CmnContents-->
</div><!--CmnWrapper-->

</body>
</asp:Content>
