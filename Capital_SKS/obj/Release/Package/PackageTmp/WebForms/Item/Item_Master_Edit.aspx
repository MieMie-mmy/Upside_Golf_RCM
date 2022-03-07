<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_Master_Edit.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Master_Edit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"> 
<%--<!DOCTYPE html>--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<link rel="stylesheet" href="../../Styles/base.css" >
<link rel="stylesheet" href="../../Styles/common.css" >
<link rel="stylesheet" href="../../Styles/manager-style.css" >
<link rel="stylesheet" href="../../Styles/item.css" >
<link href="css/lightbox.css" rel="stylesheet" >
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<%--<script src="../js/jquery.droppy.js"></script>  
<script src="../js/jquery.page-scroller.js"></script> --%>
<script src="../../Scripts/jquery.droppy.js" type="text/javascript"></script>  
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script> 
</head>
<body>
<div id="CmnWrapper">

 
<!-- HEADER *header.html -->
<header><nav><ul id="nav"><li id="menu01"><p>商品情報管理</p><ul id="menu01-sub" class="subMenu"><li><a href="#">商品情報クイック編集</a></li><li><a href="#">商品情報一覧(ページ制作)</a></li><li><a href="#">商品情報一覧(商品管理)</a></li><li><a href="#">出品一覧</a></li><li><a href="#">在庫一覧</a></li><li><a href="#">注文一覧</a></li></ul></li><li><p><a href="#">プロモーション一覧</a></p></li><li id="menu02"><p>カテゴリ管理</p><ul id="menu02-sub" class="subMenu"><li><a href="#">ショップカテゴリ一覧</a></li><li><a href="#">楽天カテゴリ一覧</a></li><li><a href="#">ヤフーカテゴリ一覧</a></li><li><a href="#">ポンパレカテゴリ一覧</a></li></ul></li><li><p><a href="#">データベース一覧</a></p></li><li><p><a href="#">ユーザ一覧</a></p></li><li id="menu03"><p>ショップ管理</p><ul id="menu03-sub" class="subMenu"><li><a href="#">ショップ一覧</a></li><li><a href="#">スマートテンプレート一覧</a></li><li><a href="#">オプションテンプレート編集</a></li><li><a href="#">商品情報エクスポート定義</a></li></ul></li></ul></nav></header><!-- /HEADER *header.html -->



<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconEdit">
<h1>商品情報編集</h1>

<!-- EditArea -->
<div class="itemCmnSet editPage">
<form>
	<div id="block1" class="cmnEdit inlineSet">
		<dl class="itemNo">
			<dt>商品番号</dt>
			<dd>bbl-bf101208</dd>
		</dl>

		<dl class="itemName">
			<dt>商品名</dt>
			<dd>AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌﾞ ﾌﾚﾝﾁｵーﾌﾟﾝ(BF101208)AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌﾞ ﾌﾚﾝﾁｵーﾌﾟﾝ(BF101208)AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌﾞ ﾌﾚﾝﾁｵーﾌﾟﾝ(BF101208)AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌﾞ ﾌﾚﾝﾁｵーﾌﾟﾝ(BF101208)AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌﾞ ﾌﾚﾝﾁｵーﾌﾟﾝ(BF101208)AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌﾞ ﾌﾚﾝﾁｵーﾌﾟﾝ(BF101208)AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌﾞ ﾌﾚﾝﾁｵーﾌﾟﾝ(BF101208)AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌﾞ ﾌﾚﾝﾁｵーﾌﾟﾝ(BF101208)AEROPRO DRIVE FRENCH OPEN/ｱｴﾛﾌﾟﾛﾄﾞﾗｲﾌ</dd>
		</dl>
		<dl>
			<dt>ブランド名</dt>
				<dd>あいうえおabcdefghijかきくけこklmnopqrst</dd>
			<dt>競技名</dt>
				<dd>あいうえおabcdefghijかきくけこklmnopqrst</dd>
			<dt>分類名</dt>
				<dd>あいうえおabcdefghijかきくけこklmnopqrst</dd>
		</dl>


		<div id="imgEntryTechnology">
			<dl>
				<dt>商品画像<input type="button" value="画像登録" onClick="winCenter('location=no')"></dt>
				<dd>
					<p><a href="http://image.rakuten.co.jp/sportsplaza/cabinet/item_img/product-img22/bbl-bf101208-1.jpg" rel="lightbox[roadtrip]"><img src="http://image.rakuten.co.jp/sportsplaza/cabinet/item_img/product-img22/bbl-bf101208-1.jpg"></a></p>
					<p><a href="http://image.rakuten.co.jp/sportsplaza/cabinet/item_img/product-img22/bbl-bf101208-1.jpg" rel="lightbox[roadtrip]"><img src="http://image.rakuten.co.jp/sportsplaza/cabinet/item_img/product-img22/bbl-bf101208-1.jpg"></a></p>
					<p><a href="http://image.rakuten.co.jp/sportsplaza/cabinet/item_img/product-img22/bbl-bf101208-1.jpg" rel="lightbox[roadtrip]"><img src="http://image.rakuten.co.jp/sportsplaza/cabinet/item_img/product-img22/bbl-bf101208-1.jpg"></a></p>
					<p><a href="http://image.rakuten.co.jp/sportsplaza/cabinet/item_img/product-img22/bbl-bf101208-1.jpg" rel="lightbox[roadtrip]"><img src="http://image.rakuten.co.jp/sportsplaza/cabinet/item_img/product-img22/bbl-bf101208-1.jpg"></a></p>
					<p></p>
				</dd>
			</dl>
			<dl>
				<dt>テクノロジー画像</dt>
				<dd>
					<p><input type="text" value="ああああああああああ"></p>
					<p><input type="text"></p>
					<p><input type="text"></p>
					<p><input type="text"></p>
					<p><input type="text"></p>
					<p><input type="text"></p>
				</dd>
			</dl>
		</div>
	</div><!--/#block1-->


<div id="scrollArea" class="cmnEdit inlineSet">
	<div id="hideBlock" class="skuBlock">
		<h2>SKUデータ</h2>
		<div id="hideBox" class="skubox">
		
		<div class="sku1">
		<table>
			<caption>SKU</caption>
			<thead>
				<tr>
					<th scope="col">&nbsp;</th>
					<th scope="col">23.0</th>
					<th scope="col">24.0</th>
					<th scope="col">25.0</th>
					<th scope="col">26.0</th>
				</tr>
			</thead>
			<tbody>
				<tr>
					<th scope="row">カラー</th>
					<td>32byte</td>
					<td>32byte</td>
					<td>32byte</td>
					<td>32byte</td>
				</tr>
			</tbody>
		</table>
		</div>
			
		<h3>SKU サイズ&amp;カラー</h3>
		<div id="hideBox2" class="skubox">
		
			<div class="sku2">
			<table>
				<caption>SKU（サイズ）</caption>
				<thead>
					<tr>
						<th scope="col">略称</th>
						<th scope="col">正式名称</th>
					</tr>
				</thead>
				<tbody>
					<tr>
						<th scope="row">colorカラーcolorcolorカラーcolor</th>
						<td>あいうえおabcdefghijklmnopqrstかきくけこabcdefghijklmnopqrstあいうえおかきくけこ</td>
					</tr>
					<tr>
						<th scope="row">colorカラーcolor</th>
						<td>あいうえおかきくけこさしすせ</td>
					</tr>
					<tr>
						<th scope="row">colorカラーcolor</th>
						<td>あいうえおかきくけこさしすせ</td>
					</tr>
				</tbody>
			</table>
			</div>
		
			<div class="sku2 sku3">
			<table>
				<caption>SKU（カラー）</caption>
				<thead>
					<tr>
						<th scope="col">略称</th>
						<th scope="col">正式名称</th>
					</tr>
				</thead>
				<tbody>
					<tr>
						<th scope="row">colorカラーcolorcolorカラーcolor</th>
						<td>あいうえおabcdefghijklmnopqrstかきくけこabcdefghijklmnopqrstあいうえおかきくけこ</td>
					</tr>
					<tr>
						<th scope="row">colorカラーcolor</th>
						<td>あいうえおかきくけこさしすせ</td>
					</tr>
					<tr>
						<th scope="row">colorカラーcolor</th>
						<td>あいうえおかきくけこさしすせ</td>
					</tr>
				</tbody>
			</table>
			</div>
		</div><!-- /#hideBox2 -->
		
		</div><!-- /#hideBox -->
	</div ><!-- /#hideBlock -->
<!-- /SKU -->

<div id="block2">
	<div class="dBlock itemInfo">
		<dl>
			<dt>年度</dt>
				<dd>2015年度春夏秋冬</dd>
			<dt>シーズン</dt>
				<dd>度春夏秋冬</dd>
			<dt>カタログ情報</dt>
				<dd>あいうえおabcdefghijかきくけこklmnopqrst</dd>
		</dl>
	</div>

	<div class="dBlock">
		<dl class="relatedProduct">
			<dt>関連商品</dt>
			<dd><input type="text"><input type="text"><input type="text"><input type="text"><input type="text">
			</dd>
		</dl>
	</div>
	<div class="dBlock productInfo box2 inlineSet">
		<dl id="pd-Info">
			<dt>商品詳細情報</dt>
			<dd>
				<p><input type="text"><textarea></textarea></p>
				<p><input type="text" value="あいうえおかきk"><textarea></textarea></p>
			</dd>
			<dd class="piOpen">
				<p><input type="text"><textarea></textarea></p>
				<p><input type="text"><textarea></textarea></p>
			</dd>
		</dl>
		<dl id="pb-Info2">
			<dt>商品基本情報</dt>
			<dd>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
			</dd>
			<dd class="piOpen2">
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
				<p><input type="text"><input type="text"></p>
			</dd>
		</dl>
	</div>

	<div class="dBlock txtDescription box2 inlineSet">
		<dl>
			<dt>PC用商品説明文</dt>
			<dd><textarea></textarea></dd>
		</dl>
		<dl>
			<dt>PC用販売説明文</dt>
			<dd><textarea></textarea></dd>
		</dl>
	</div>


	<div class="dBlock inlineSet">
		<dl class="itemOption">
			<dt>オプション<br><input type="button" value="選 ぶ" onClick="winCenter2()"></dt>
			<dd>
				<p>項目名<input type="text"></p><p>選択肢<input type="text"></p>
				<p>項目名<input type="text"></p><p>選択肢<input type="text"></p>
				<p>項目名<input type="text"></p><p>選択肢<input type="text"></p>
			</dd>
		</dl>
	</div>

	<div class="dBlock box2 inlineSet">
		<dl class="itemCampaign">
			<dt>キャンペーン画像</dt>
			<dd>
				<p><input type="text" value="1234567890123456789012345"></p>
				<p><input type="text"></p>
				<p><input type="text"></p>
				<p><input type="text"></p>
				<p><input type="text"></p>
			</dd>
		</dl>
		<dl class="shopCategory">
			<dt>ショップカテゴリ<input type="button" value="選 ぶ" onClick="winCenter3()"></dt>
			<dd>
				<p><input type="text"></p>
				<p><input type="text"></p>
				<p><input type="text"></p>
				<p><input type="text"></p>
				<p><input type="text"></p>
			</dd>
		</dl>
	</div>
	
	<div class="dBlock itemCategory inlineSet">
		<dl>
			<dt>カテゴリID<br>Yahooスペック</dt>
			<dd>
			<p>楽天 カテゴリID<input type="text"><input type="button" value="選 ぶ" onClick="winCenter4_1()"></p>
			<p>ヤフー カテゴリID<input type="text"><input type="button" value="選 ぶ" onClick="winCenter4_2()"></p>
			<p>ポンパレ カテゴリID<input type="text"><input type="button" value="選 ぶ" onClick="winCenter4_3()"></p>
			<p>ヤフースペック値<input type="text"><input type="text"><input type="text"><input type="text"><input type="text"><input type="button" value="選 ぶ" onClick="winCenter5()"></p>
			</dd>
		</dl>
	
	
<!--		<div>
			<p>楽天 カテゴリID<input type="text"><input type="button" value="選 ぶ" onClick="winCenter4_1()"></p>
			<p>ヤフー カテゴリID<input type="text"><input type="button" value="選 ぶ" onClick="winCenter4_2()"></p>
			<p>ポンパレ カテゴリID<input type="text"><input type="button" value="選 ぶ" onClick="winCenter4_3()"></p>
			<p>ヤフースペック値<input type="text"><input type="text"><input type="text"><input type="text"><input type="text"><input type="button" value="選 ぶ" onClick="winCenter5()"></p>
		</div>
 -->
 	</div>

	<div class="dBlock Exhibit">
		<dl>
			<dt>出品対象ショップ</dt>
			<dd>
				<b>ラケットプラザ</b>
				<p><input type="checkbox" value="rpR">楽天</p>
				<p><input type="checkbox" value="rpY">Yahoo</p>
				<p><input type="checkbox" value="rpJ">自社</p>
				<p><input type="checkbox" value="rpA">amazon</p>
				<p><input type="checkbox" value="rpP">ポンパレ</p>
			</dd>
			<dd>
				<b>ラックピース</b>
				<p><input type="checkbox" value="rpR">楽天</p>
				<p><input type="checkbox" value="rpY">Yahoo</p>
			</dd>
			<dd>
				<b>スポーツプラザ</b>
				<p><input type="checkbox" value="rpR">楽天</p>
				<p><input type="checkbox" value="rpY">Yahoo</p>
			</dd>
			<dd>
				<b>ベースボールプラザ</b>
				<p><input type="checkbox" value="rpR">楽天</p>
				<p><input type="checkbox" value="rpY">Yahoo</p>
			</dd>
			<dd>
				<b>卓球本舗</b>
				<p><input type="checkbox" value="rpR">楽天</p>
				<p><input type="checkbox" value="rpY">Yahoo</p>
			</dd>
		</dl>
	</div>

	<div class="dBlock itemPriceFlag dlFloat">
		<dl>
			<dt>定価</dt><dd>999,999円</dd>
			<dt>販売価格</dt><dd>999,999円</dd>
			<dt>発売日</dt><dd>2015/02/02</dd>
			<dt>掲載可能日</dt><dd>2015/02/02</dd>
		</dl>
		<dl>
			<dt>ブランドコード</dt><dd>12345</dd>
			<dt>製品コード</dt><dd><input type="text" value="12345678901234567890123456789012345678901234567890"></dd>
			<dt>送料フラグ</dt>
			<dd>
				<select>
					<option value="soryo">送料別</option>
					<option value="soryo">送料込</option>
				</select>
			</dd>
		</dl>
		<dl>
			<dt>個別送料</dt><dd><input type="text" value="123456789"></dd>
			<dt>代引料フラグ</dt>
			<dd>
				<select>
					<option value="soryo">代引料別</option>
					<option value="soryo">代引料込</option>
				</select>
			</dd>
			<dt>倉庫指定</dt>
			<dd>
				<select>
					<option value="soryo">販売中</option>
					<option value="soryo">倉庫</option>
				</select>
			</dd>
		</dl>
		<dl class="itemFlag">
			<dt>闇市パスワード</dt><dd><input type="text" value="12345678901234567890123456789012"></dd>
			<dt>二重価格文書管理番号</dt><dd><input type="text" value="12"></dd>
		</dl>
	</div>

	<div class="dBlock itemSpace">
		<dl>
			<dt>フリースペース２</dt>
			<dd><textarea></textarea></dd>
			<dt>フリースペース３</dt>
			<dd><textarea></textarea></dd>
			<dt>ゼット用項目（PC商品説明文）</dt>
			<dd><textarea></textarea></dd>
			<dt>ゼット用項目（PC販売説明文）</dt>
			<dd><textarea></textarea></dd>
		</dl>
	</div>

	<div class="dBlock txtDescription box2 inlineSet">
		<dl>
			<dt>スマートフォン用商品説明文</dt>
			<dd><textarea></textarea></dd>
		</dl>
		<dl>
			<dt>商品情報</dt>
			<dd><textarea></textarea></dd>
		</dl>
	</div>
</div><!-- /#block2 -->

</div><!-- /#scrollArea -->
			<div class="btn"><p><input type="button" value="プレビュー"><input type="button" value="登 録"></p></div>
</form>
</div><!--/.itemCmnSet-->
<!-- /EditArea -->

</div><!--/.setListBox-->

	</div><!--ComBlock-->
</div><!--CmnContents-->

</div><!--CmnWrapper-->

<script src="js/lightbox.min.js"></script>

<script>
    $(function () {
        $("#hideBlock h2").on("click", function () {
            $(this).next().slideToggle();
            $(this).toggleClass("active");
        });
    });
    $(function () {
        $("#hideBlock h3").on("click", function () {
            $(this).next().slideToggle();
            $(this).toggleClass("active");
        });
    });
    $(function () {
        $("#pd-Info dt").on("click", function () {
            $("dd.piOpen").slideToggle();
            $(this).toggleClass("active");
        });
    });
    $(function () {
        $("#pb-Info2 dt").on("click", function () {
            $("dd.piOpen2").slideToggle();
            $(this).toggleClass("active");
        });
    });
</script>

</body>
</html>
