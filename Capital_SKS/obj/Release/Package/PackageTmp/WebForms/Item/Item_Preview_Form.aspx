
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_Preview_Form.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Preview_Form" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<%--<head runat="server">
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="../../Styles/preview.css"  type="text/css"/>
<link rel="stylesheet" href="../../Styles/rms_shopcustom.css" type="text/css"/>
<link type="text/css" rel="stylesheet" href="http://image.rakuten.co.jp/com/css/rms/storefront/pc/page/aroundcart-1.1.15.css">
<link href="http://www.rakuten.ne.jp/gold/racket/css/base.css" rel="stylesheet" media="all" />
<link href="http://www.rakuten.ne.jp/gold/racket/css/rp_shopcustom.css" rel="stylesheet" media="all" />
</head>--%>
<head>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

<link rel="stylesheet" href="../../Styles/preview.css">
<link rel="stylesheet" href="../../Styles/rms_shopcustom.css">

<link type="text/css" rel="stylesheet" href="http://image.rakuten.co.jp/com/css/rms/storefront/pc/page/aroundcart-1.1.15.css">
<link href="http://www.rakuten.ne.jp/gold/racket/css/base.css" rel="stylesheet" media="all" />
<link href="http://www.rakuten.ne.jp/gold/racket/css/rp_shopcustom.css" rel="stylesheet" media="all" />

<link rel="stylesheet" href="http://www.rakuten.ne.jp/gold/racket/category/outletsale/css/ols_iframe.css">
<link rel="stylesheet" href="http://www.rakuten.ne.jp/gold/racket/category/tennis/css/tns_iframe.css">
<link rel="stylesheet" href="http://www.rakuten.ne.jp/gold/racket/category/outletsale/sale-wear/clearance/css/lp_iframe.css">
<link rel="stylesheet" href="http://www.rakuten.ne.jp/gold/racket/category/racket-wear/css/iframe.css">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<script src="../../Scripts/jquery.page-scroller.js"></script>

<title>商品管理システム＜プレビュー＞</title>
</head>
<body>
<form id="form1" runat="server">


<div id="prevBlock">
<div id="pagebody" align="center">
<asp:GridView ID="gvCategory" runat="server"   AutoGenerateColumns="False" CssClass="sdtext"
ShowHeader="False"  CellPadding="0" ForeColor="Red" border="0" CellSpacing="0"
GridLines="None" style="text-align:justify;  font-size:11px">
<Columns>
<asp:TemplateField HeaderText="ID" Visible="false">
<ItemTemplate>
<table cellspacing="2" cellpadding="0" border="0">
<tr><td><asp:Label runat="server" ID="lblID" /></td></tr>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<ItemTemplate>
<tr>
<td class="sdtext"><a><asp:Label runat="server" ID="txtCTGName" Text='<%#Bind("CName") %>' /></a></td>
</tr>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>
<br />
<div id="rmsItemSet" align="center" style="width: 765px; margin: 0 auto;">
<table>
<tr>
<td align="center">
<asp:DataList ID="dlItemPhoto" runat="server" BackColor="White" BorderColor="#666666"
BorderStyle="None" CellPadding="3" CellSpacing="2" Font-Names="Verdana" Font-Size="Small" RepeatColumns="1"  RepeatDirection="Vertical">
<ItemTemplate>
<p><asp:Image ID="Image" runat="server" Width="600px" Height="600px"   ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>' /></p>
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />
</asp:DataList>
</td>
</tr>
</table>
<table><!-- ライブラリ画像／サイズ表 -->
<tr><td  align="center">
<asp:Image ID="lblLibararyPhoto"  runat="server"   width="600px"  ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>'/>
</td></tr>
</table>
</div>
<table>
<tr><td><asp:Literal ID="Sale_Literal" runat="server"></asp:Literal><br /></td></tr>
</table>
<div>
<table>
<tr>
<td><div style="float:left;">

<asp:DataList ID="dlphotoList2" runat="server" BackColor="White" BorderColor="#666666"
BorderStyle="None" CellPadding="3" CellSpacing="2"  Font-Names="Verdana" Font-Size="Small" RepeatColumns="1" RepeatDirection="Horizontal"  Height="600px">
<ItemTemplate>
<asp:Image ID="imgItem" runat="server"   Width="200px"　height="200px" ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>' 
onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='default'"  /><br />
</ItemTemplate>
<ItemStyle HorizontalAlign="Right" />
</asp:DataList>
</div></td>
<td>
<div style="float:left;" >
<!-- ☆：商品名 -->
	<table cellspacing="2" cellpadding="0" border="0">
	<tbody><tr>
		<td><a name="10075794"></a>
		<!-- ☆：キャッチコピー -->
		<span class="catch_copy"><br/></span>
		<!-- ☆：商品名 -->
		<span class="item_name"><b><asp:Label ID="lblProduct_Name" runat="server" Text="Product_Name"></asp:Label></b></span>
		<br/>
		<br/>
		</td>
	</tr>
	<tr>
	<td></td>
	</tr>
	</tbody></table>
<!-- /☆：商品名 -->

<!-- ☆：商品番号 -->
	<table cellspacing="2" cellpadding="0" border="0">
	<tbody>
    <tr>
		<td><span class="item_number_title">NO.</span>&nbsp;<span class="item_number"><asp:Label ID="lblItemcode" runat="server" ></asp:Label></span></td>
	</tr>
	</tbody></table>
<!-- /☆：商品番号 -->

<!-- ☆：価格 -->
	<table id="rakutenLimitedId_cart" cellspacing="2" cellpadding="0" border="0">
	<tbody><tr>
		<td valign="top" align="right"><span class="price1">価格</span></td>
		<td><span class="price2"><asp:Label ID="lblPrice" runat="server"> </asp:Label>円</span><span class="tax_postage"> (税込<asp:Label ID="lblListPrice" runat="server"> </asp:Label> 円)</span><span class="tax_postage"> 送料別</span></td>
	</tr>
	<tr>
		<td colspan="2">
		<div class="pointGet riMb25">
		<a target="_blank" >お買い物の前に2,000ポイントゲット！</a>
		</div>
		</td>
	</tr>
	</tbody></table>
<!-- /☆：価格 -->

<div id="rakutenLimitedId_aroundCart" class="standard">
<table id="normal_basket_10075794" cellspacing="2" cellpadding="0" border="0" width="50%">

<tbody><tr>
<td><span class="inventory_title">▼ 下記商品リストからご希望の商品をお選びください。</span></td>
</tr>
<tr>
<td>
<table cellspacing="0" cellpadding="0" border="0">
<tbody><tr>
<td valign="bottom"><span class="inventory_title">サイズ</span></td><td valign="bottom">&nbsp;<span class="inventory_title">×</span>&nbsp;</td><td valign="bottom"><span class="inventory_title">カラー</span></td>
</tr>
</tbody></table>
</td>
</tr>
<tr>
<td>
 <div runat="server" id="divSKUTable" class="stock_table">
 </div>
<span class="inventory_desc">※在庫については「在庫状況のご確認はコチラ」をご確認下さい。<br>
</span></td>
</tr>
<tr>
<td><span class="unit">個数&nbsp;</span><asp:TextBox ID="txtNumber" runat="server" Text="1"></asp:TextBox> &nbsp;<asp:TextBox ID="txtOption" runat="server" Width="150px" style="cursor: auto !important;">買い物かごに入れる</asp:TextBox></td>
</tr>

</tbody></table>
<table width="256" cellspacing="2" cellpadding="0" border="0">
<tbody><tr>
<td><asp:Button ID="Button1" runat="server" Text="商品についての問い合わせ" /></td>
</tr>

<tr>
<td>

</td>
</tr>
</tbody></table>
<div style="padding-left:2px"></div>
</div>
</div>
</td>
</tr>
</table>


<div align="center" style="width:100%; margin: 0 auto;clear:left">
<div style="margin: 15px auto; padding: 0;">
<asp:Literal ID="LiteralItemDescription" runat="server"></asp:Literal>
</div>  
<div align="center" style="width: 765px; margin: 0 auto;">
<div id="rec-zone" class="clearfix" style="margin: 10px auto; padding: 0;">
<h3 id="rec-ttl">関連商品</h3>
<asp:DataList ID="RelatedItemList" runat="server" BorderStyle="None" RepeatColumns="2" RepeatDirection="Horizontal" >
<ItemStyle  ForeColor="Black" BorderStyle="None"  CssClass="rec-item"/>
<ItemTemplate>
<table border="0">
<tr>
<td>
        <div class="rec-photo"><asp:Image ID="imgEmp" runat="server"  Width="100px"　height="100px"  ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>'  /></div>
</td>
<td>
        <div class="rec-text"><asp:Label ID="lblItem_Name" runat="server"  Text=' <%# Bind("Item_name") %>' Width="150px" BackColor="#f9f9f9"></asp:Label>
        <p class="rec-price"><asp:Label ID="lblSalePrice" runat="server" Text=' <%# Bind("Sale_Price") %>' BackColor="#f9f9f9"></asp:Label>円</p></div>
</td>
</tr>
</table>
</ItemTemplate>
</asp:DataList>
</div>
</div> 
</div>
</div>

</div>
</div>
</form>
</body>
</html>
