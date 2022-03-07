<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Preview.aspx.cs" Inherits="ORS_RCM.Item_Preview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="~/Styles/base.css" rel="stylesheet" type="text/css" />


     <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

 <div id="prevBlock">
<div id="pagebody" align="center">

<table cellspacing="2" cellpadding="0" border="0">
<tbody><tr>
<td class="sdtext">
<asp:Label ID="lblCatetop1" runat="server" Text=''></asp:Label>
</td>
</tr>


<tr>
<td class="sdtext">
<asp:Label ID="lblCatetop2" runat="server" Text="カテゴリトップ"></asp:Label>

    <asp:ImageButton ID="ImageButton1" runat="server" />
    
   <asp:GridView ID="gvCategore" runat="server" AutoGenerateColumns="False" 
           ShowHeader="False" Width="300px" CellPadding="4" ForeColor="#333333" 
        GridLines="None">
	    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
	    <Columns>
		<asp:TemplateField HeaderText="ID" Visible="false">
		<ItemTemplate>
		<asp:Label runat="server" ID="lblID" />
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
		<ItemTemplate>
        <asp:TextBox runat="server" ID="txtCTGName" />
		<asp:Label runat="server" ID="lblCTGName" />
		</ItemTemplate>
		</asp:TemplateField>
		</Columns>
	    <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
	    </asp:GridView>
        </td>





</td>

</tr>


<tr>
<td class="sdtext">
    &nbsp;</td>
</tr>




</tbody></table>

<!-- 05:詳細 -->
<br>
<table cellspacing="0" cellpadding="0" border="0">
<tbody><tr>
<td><span class="sale_desc">

<!-- ソース吐き出し-->
<div id="rmsItemSet" align="center" style="width: 765px; margin: 0 auto;">

<!-- 商品画像 -->
<p><asp:Image ID="image1"     runat="server"    style="border:none;" alt="" src=""/></p>
<p><asp:Image ID="image2"  runat="server" style="border:none;" alt="" src=""/></p>
<p><asp:Image ID="image3"  runat="server"  style="border:none;" alt="" src=""/></p>
<!-- /商品画像 -->

<!-- ライブラリ画像／サイズ表 -->
<div style="width: 600px; text-align: center; margin-bottom: 20px;"><img src=""></div>





</div>

</div>

</asp:Content>
