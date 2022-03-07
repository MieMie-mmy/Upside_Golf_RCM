<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_Preview_Edit.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Preview_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<link rel="stylesheet" href="css/preview.css">
<link rel="stylesheet" href="../css/rms_shopcustom.css">

<link type="text/css" rel="stylesheet" href="http://image.rakuten.co.jp/com/css/rms/storefront/pc/page/aroundcart-1.1.15.css">
<link href="http://www.rakuten.ne.jp/gold/racket/css/base.css" rel="stylesheet" media="all" />
<link href="http://www.rakuten.ne.jp/gold/racket/css/rp_shopcustom.css" rel="stylesheet" media="all" />


<link rel="stylesheet" href="http://www.rakuten.ne.jp/gold/racket/category/outletsale/css/ols_iframe.css">
<link rel="stylesheet" href="http://www.rakuten.ne.jp/gold/racket/category/tennis/css/tns_iframe.css">
<link rel="stylesheet" href="http://www.rakuten.ne.jp/gold/racket/category/outletsale/sale-wear/clearance/css/lp_iframe.css">
<link rel="stylesheet" href="http://www.rakuten.ne.jp/gold/racket/category/racket-wear/css/iframe.css">

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<script src="js/jquery.page-scroller.js"></script>

<title>商品管理システム＜プレビュー＞</title>
</head>

<body>
<form id="form1" runat="server">
<div id="prevBlock">
<div id="pagebody" align="center">
<!-- 03 -->
<!-- 03 -->
<table cellspacing="0" cellpadding="0" border="0">
<tbody><tr>
<td>

<!-- 04:カテゴリ -->

<asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False" 
ShowHeader="False"  CellPadding="0" ForeColor="Red" border="0" CellSpacing="0"
GridLines="None" style="text-align:justify;  font-size:11px">
<Columns>
<asp:TemplateField HeaderText="ID" Visible="false">
<ItemTemplate>
<asp:Label runat="server" ID="lblID" />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>

<ItemTemplate>
<table cellspacing="2" cellpadding="0" border="0">
<tbody>
<tr>

<td class="sdtext"><a><asp:Label runat="server" ID="Label1" Text='<%# ((string)Eval("CName")).Replace("\\", " "+">"+" ") %>' /></a></td>

</tr>
</tbody>
</table>

</ItemTemplate>
</asp:TemplateField>

</Columns>
</asp:GridView>

<%--<asp:DataList runat="server" ID="dlCategory" RepeatDirection="Horizontal" RepeatLayout="Flow">
<ItemTemplate>
<a><asp:Label runat="server" ID="Label1" Text='<%# Container.DataItem %>' /></a>>
</ItemTemplate>
</asp:DataList>--%>
<!-- /04:カテゴリ -->


<!-- 05:詳細 -->
<br/>
<table cellspacing="0" cellpadding="0" border="0">
<tbody><tr>
<td><span class="sale_desc">

<!-- ソース吐き出し-->
<div id="rmsItemSet" align="center" style="width: 765px; margin: 0 auto;">

<!-- 商品画像 -->

<asp:DataList ID="dlItemPhoto" runat="server" RepeatColumns="1"  RepeatDirection="Vertical" BorderStyle="None">
<ItemTemplate>
<p><asp:Image ID="Image" runat="server" ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>' /></p>
</ItemTemplate>
<ItemStyle Width="600px" HorizontalAlign="Center"/>
</asp:DataList>
<!-- 商品画像 -->


<%--<!-- /商品画像 --><!-- ライブラリ画像／サイズ表 -->
<div style="width: 600px; text-align: center; margin-bottom: 20px;"><img src="http://www.rakuten.ne.jp/gold/racket/lib_img/img/size-prince-ladies.gif"></div>
<!-- 使用と特徴 -->--%>
<div style="margin: 10px auto; padding: 0;">



<asp:Literal ID="Literal_Sale_Description" runat="server"></asp:Literal>

</div>
<!-- Smart Templateと統合-->
<%--<!-- Check it out -->
<div style="background-image:  url(http://www.rakuten.ne.jp/gold/racket/lib_img/img/check.gif); padding: 0px; margin: 20px auto 0 ; width: 610px; height: 40px; text-align: center;"></div><div style="background: #f5f5f5; padding: 10px 20px; margin: 0px auto 15px; width: 570px; height: auto; font-size: 13px; color: #444; line-height: 1.7; text-align: left;"><b>Princeは日本テニス連盟公認ブランドです。</b></div>

<!-- 在庫状況 -->
<br /><a href="http://link.rakuten.co.jp/1/041/957/?url=pri-wl4057" target="_blank"><img src="http://www.rakuten.ne.jp/gold/racket/lib_img/img/order-caution_btn.jpg" /></a>--%>
<br />

</div></span>
<br />
<br />
</td>
</tr>
</tbody></table>
<!-- /05:詳細 -->


<!-- 06:買い物かごエリア -->
<table cellspacing="0" cellpadding="0" border="0">
<tbody>
<tr>
<!-- ★左側：サムネ -->
<td valign="top">
<asp:DataList ID="dlItemPhoto1" runat="server" RepeatColumns="1"  RepeatDirection="Vertical" BorderStyle="None"  RepeatLayout="Flow">
<ItemTemplate>
<asp:Image ID="Image" runat="server" Width="200px" Height="200px" ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>' /><br /><br />
</ItemTemplate>
</asp:DataList>
<div style="display: none;" id="rakutenLimitedId_ImageExp-10075794" class="rakutenLimitedId_ImageExp">
<br/>
</div>
<div style="display:none;" id="rakutenLimitedId_ImageList-10075794">
<asp:Image ID="lblLibararyPhoto"  runat="server"   width="600px"  ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>'/>
</div>
</td>
<!-- /★左側：サムネ -->

<td><img width="20" src="/com/img/home/t.gif" height="1" alt=""></td>

<!-- ☆右側：商品名・価格・買い物かご -->
<td valign="top">

<!-- ☆：商品名 -->
	<table cellspacing="2" cellpadding="0" border="0">
	<tbody><tr>
		<td><a name="10075794"></a>
		<!-- ☆：キャッチコピー -->
		<span class="catch_copy"><asp:Label ID="lblcatch_copy" runat="server" /><br /></span>
		<!-- ☆：商品名 -->
		<span class="item_name"><b><asp:Label ID="lblProduct_Name" runat="server" /></b></span>
		<br>
		<br>
		</td>
	</tr>
	<tr>
	<td></td>
	</tr>
	</tbody></table>
<!-- /☆：商品名 -->

<!-- ☆：商品番号 -->
	<table cellspacing="2" cellpadding="0" border="0">
	<tbody><tr>
		<td nowrap=""><span class="item_number_title">NO.</span>&nbsp;</td>
		<td nowrap=""><span class="item_number"><asp:Label ID="lblItemcode" runat="server" /></span></td>
	</tr>
	</tbody></table>
<!-- /☆：商品番号 -->

<!-- ☆：価格 -->
	<table id="rakutenLimitedId_cart" cellspacing="2" cellpadding="0" border="0">
	<tbody><tr>
		<td valign="top" align="right"><span class="price1">価格</span></td>
		<td><span class="price2"><asp:Label ID="lblPrice" runat="server" />円</span><span class="tax_postage"> (税込<asp:Label ID="lblListPrice" runat="server" /> 円)</span><span class="tax_postage"> 送料別</span></td>
	</tr>
	<tr>
		<td colspan="2">
		<div class="pointGet riMb25">
		<a target="_blank" href="http://ad2.trafficgate.net/t/r/3976/1441/99636_99636/?L2id=jpn-item-kcapp01">お買い物の前に2,000ポイントゲット！</a>
		</div>
		</td>
	</tr>
	</tbody></table>
<!-- /☆：価格 -->

<div id="rakutenLimitedId_aroundCart" class="standard">
<table id="normal_basket_10075794" cellspacing="2" cellpadding="0" border="0">
<form method="post" data-timesale-id="10075794" action="https://basket.step.rakuten.co.jp/rms/mall/bs/cartadd/set"></form>
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
<table cellspacing="0" cellpadding="0" border="0" bgcolor="#CCCCCC">
<tbody><tr>
<td>
<div runat="server" id="divSKUTable">
</div>
</td>
</tr>
</tbody></table>
<span class="inventory_desc">※在庫については「在庫状況のご確認はコチラ」をご確認下さい。<br>
</span></td>
</tr>
<tr>
<td><span class="unit">個数&nbsp;</span><input value="1" type="text" size="4" name="units" id="units"> &nbsp;<input value="買い物かごに入れる" id="normal_basket_10075794" data-timesale-id="" style="cursor: auto !important;"></td>
</tr>

</tbody></table>


<table width="256" cellspacing="2" cellpadding="0" border="0">
<form method="post" action="https://ask.step.rakuten.co.jp/rms/mall/pa/ask/vc"></form>
<tbody><tr>
<td><font size="3"><input value="商品についての問合わせ" type="submit"><input value="PA02_000_001" type="hidden" name="__event"></font></td>
</tr>

<tr>
<td>

</td>
</tr>
</tbody></table>
<div style="padding-left:2px"></div>
</div>
</td>
<!-- /☆右側：商品名・価格・買い物かご -->

</tr>
</tbody>
</table>
<!-- /06:買い物かごエリア -->

<!-- 07:商品情報・関連商品 -->
	<table cellspacing="2" cellpadding="0" border="0">
	<tbody><tr>
	<td><span class="item_desc">
	<!-- ●商品情報 PCItem_Description -->
    <div align="center" style="width: 765px; margin: 0 auto;">
	<div style="margin: 15px auto; padding: 0;">
    
     
        <asp:Literal ID="Literal_Item_Description" runat="server"></asp:Literal>
      
 
    
    </div>
    </div>
	<!-- /●商品情報 -->

	<!-- ●関連商品 -->

   <!--  Smart Templateと統合
	<div align="center" style="width: 765px; margin: 0 auto;">
    <div id="rec-zone" class="clearfix" style="margin: 10px auto; padding: 0;">
    <h3 id="rec-ttl">関連商品</h3>

	  <div id="rec-area">
    <asp:DataList ID="RelatedItemList" runat="server" BorderStyle="None" RepeatColumns="2" RepeatDirection="Horizontal"  CssClass="rec-item">
    <ItemStyle  ForeColor="Black" BorderStyle="None"  CssClass="rec-item"/>
    <ItemTemplate>
    <div class="rec-photo"><asp:Image ID="imgEmp" runat="server"  width="80" height="80"  ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>'  CssClass="rec-photo" /></div>
    <div class="rec-text"><p class="rec-name"><asp:Label ID="lblItem_Name" runat="server"  Text=' <%# Bind("Item_name") %>'></asp:Label></p><p class="rec-price"><asp:Label ID="lblSalePrice" runat="server" Text=' <%# Bind("Sale_Price") %>' CssClass="rec-text"></asp:Label>円</p></div>
    </ItemTemplate>
    </asp:DataList>
    </div>
    </div>
    
	<div id="rec-clear" class="clearfix" style="clear: both;"> </div>
	
	</div>-->
	<!-- /●関連商品 -->
    </span><br>
	<br>
	</td>
	</tr>
	</tbody></table>
<!-- /07:商品情報 -->

<br>



</td>
</tr>
</tbody></table><!-- /03 -->
</div><!--/pagebody-->

</div><!--prevBlock-->
</form>
</body>
</html>