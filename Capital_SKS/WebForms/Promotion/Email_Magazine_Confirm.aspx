<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Email_Magazine_Confirm.aspx.cs" Inherits="ORS_RCM.Email_Magazine_Confirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/jscript"></script>
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
<link href="../../Styles/promotion_base.css" rel="stylesheet" />
<link href="../../Styles/promotion_common.css" rel="stylesheet" />
<link href="../../Styles/promotion_manager-style.css" rel="stylesheet" />
<link href="../../Styles/promotion_promotion.css" rel="stylesheet" />
<link href="../../Styles/promotion_pagesno.css" rel="stylesheet" />
<script src="js/jquery.csv.js" type="text/javascript"></script>
<script src="js/URLGEnerate.js" type="text/javascript"></script>
<script>
    $(function () {
        $(".block3 dt").on("click", function () {
            $("dd.piOpen_Cam").slideToggle();
            $(this).toggleClass("active");
        });
    });
    $(function () {
        $(".block4 dt").on("click", function () {
            $("dd.piOpen_pu").slideToggle();
            $(this).toggleClass("active");
        });
    });
    $(function () {
        $(".block5 dt").on("click", function () {
            $("dd.piOpen_os").slideToggle();
            $(this).toggleClass("active");
        });
    });
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--<p id="toTop"><a href="#divtop">▲TOP</a></p>--%>

<div id="CmnContents">
	<div id="ComBlock" style="margin-top: 60px;">
		<div class="setListBox iconSet iconEdit">
		<h1>メールマガジン確認</h1>

	<div class="prmCmnSet prmMailOrder prmMailEntry confBox cmnEdit inlineSet">
		<div>

	<div class="block1">
		<dl>
			<dt>メールマガジンID</dt>
			<dd><asp:Label ID="lblEmailMagazineID" runat="server" Text=""></asp:Label></dd>
		</dl>

		<dl>
			<dt>配信予定日</dt>
			<dd>   <asp:Label ID="lblDeliveryDate" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblHour" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblMinute" runat="server" Text=""></asp:Label></dd>
		</dl>

		<dl>
			<dt>メールマガジン名（半角100byte/全角50文字）</dt>
			<dd><asp:Label ID="lblMailName" runat="server" Text=""></asp:Label></dd>
		</dl>

	</div>

	<div class="block2">
		<dl>
			<dt>対象ショップ</dt>
			<dd><asp:Label ID="lblTarget_Shop" runat="server" Text=""></asp:Label></dd>
		</dl>
	</div>


	<div class="block3">
		<dl>
			<dt>キャンペーン</dt>
			<dd>
				<ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg11" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p>
						<span>
                   <asp:Label ID="lblCpg1" runat="server" Text=""></asp:Label></span>
                
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span> 
                      <asp:Label ID="lblCpgURL1" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
				<asp:TextBox ID="txtMailMagazineEvent11" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent21" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent31" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
			
					</li>
				</ul>
				<ul>
					<li>
						<p>ID:</p><span> <asp:Label ID="lblCpg12" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p>
						<span> <asp:Label ID="lblCpg2" runat="server" Text=""></asp:Label></span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span> <asp:Label ID="lblCpgURL2" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
					  <asp:TextBox ID="txtMailMagazineEvent12" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent22" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent32" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
	<ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg13" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p>
						<span><asp:Label ID="lblCpg3" runat="server" Text=""></asp:Label> </span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span><asp:Label ID="lblCpgURL3" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
						<asp:TextBox ID="txtMailMagazineEvent13" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent23" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent33" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
				<ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg14" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p><span> <asp:Label ID="lblCpg4" runat="server" Text=""></asp:Label></span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span>      <asp:Label ID="lblCpgURL4" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent14" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent24" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent34" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
                <ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg15" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p><span> <asp:Label ID="lblCpg5" runat="server" Text=""></asp:Label></span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span>      <asp:Label ID="lblCpgURL5" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent15" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent25" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent35" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
                <ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg16" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p><span> <asp:Label ID="lblCpg6" runat="server" Text=""></asp:Label></span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span>      <asp:Label ID="lblCpgURL6" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent16" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent26" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent36" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
                <ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg17" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p><span> <asp:Label ID="lblCpg7" runat="server" Text=""></asp:Label></span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span>      <asp:Label ID="lblCpgURL7" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent17" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent27" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent37" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
                <ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg18" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p><span> <asp:Label ID="lblCpg8" runat="server" Text=""></asp:Label></span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span>      <asp:Label ID="lblCpgURL8" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent18" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent28" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent38" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
                <ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg19" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p><span> <asp:Label ID="lblCpg9" runat="server" Text=""></asp:Label></span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span>      <asp:Label ID="lblCpgURL9" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent19" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent29" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent39" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
                <ul>
					<li>
						<p>ID:</p><span><asp:Label ID="lblCpg110" runat="server" Text=""></asp:Label></span>
					</li>
					<li>
						<p>キャンペーン名</p><span> <asp:Label ID="lblCpg10" runat="server" Text=""></asp:Label></span>
					</li>
					<li><p>URL<a href="#" target="_blank" class="checkUrl">確　認</a></p><span>      <asp:Label ID="lblCpgURL10" runat="server" Text=""></asp:Label></span></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent110" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent210" runat="server" Width="225px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent310" runat="server" Width="225px" ReadOnly="true"></asp:TextBox>
					</li>
				</ul>
   
			</dd>
		</dl>
	</div>

	<div class="block4 itemSet">
		<dl>
			<dt class="on">Pick up</dt>
			<dd><b>タイトル</b>
			<p>新商品が続々登場！
			この先もたくさん入荷予定！
			</p></dd>

			<dd class="orderBox">
				<div>
					<p>Pick up アイテム1</p>
					<dl>
						<dt>タイトル<span>（リンクテキスト）</span></dt>
						<dd>HEAD 限定モデル入荷</dd>
					</dl>
					<dl>
						<dt>商品情報</dt>
						<dd>｢Graphene Radical MP」世界で最も強く、最も軽いHEADの新テクノロジー</dd>
					</dl>
					<dl>
						<dt>商品ページ・検索URL<a href="#" target="_blank" class="checkUrl">確　認</a></dt>
						<dd>http://search.rakuten.co.jp/search/inshop-mall/%E3%82%AB%E3%82%B9%E3%82%BF%E3%83%A0%E3%82%A8%E3%83%83%E3%82%B8+V2%EF%BC%8E0/-/f.1-p.1-s.2-sf.1-sid.210943-st.A-v.2</dd>
					</dl>
					<dl>
						<dt>カテゴリURL<a href="#" target="_blank" class="checkUrl">確　認</a></dt>
						<dd></dd>
					</dl>
					<dl>
						<dt>備考</dt>
						<dd>ピックアップバナーの制作お願いします</dd>
					</dl>
				</div>

				<div>
					<p>Pick up アイテム2</p>
				</div>

				<div>
					<p>Pick up アイテム3</p>
				</div>

				<div>
					<p>Pick up アイテム4</p>
				</div>

			</dd>
		</dl>
	</div>

	<div class="block5 itemSet">
		<dl>
			<dt>スタッフのオススメ</dt>
			<dd class="orderBox">
				<div>
					<p>オススメ アイテム1</p>
					<dl>
						<dt>商品</dt>
						<dd>ディアドラ／オールラウンド用シューズ</dd>
					</dl>
					<dl>
						<dt>商品情報</dt>
						<dd>【SALE】最大50%OFF</dd>
					</dl>
					<dl>
						<dt>価格・フラグ・ポイント</dt>
						<dd>
							<p><span>販売価格</span>123456円</p>
							<p><span>アイコン１</span>送料無料</p>
							<p><span>アイコン２</span>SALE</p>
							<p><span>アイコン３</span>NEW</p>
							<p><span>ポイント倍率</span></p>
						</dd>
					</dl>
					<dl>
						<dt>商品ページ・検索URL<a href="#" target="_blank" class="checkUrl">確　認</a></dt>
						<dd>http://search.rakuten.co.jp/search/inshop-mall/%E3%82%AB%E3%82%B9%E3%82%BF%E3%83%A0%E3%82%A8%E3%83%83%E3%82%B8+V2%EF%BC%8E0/-/f.1-p.1-s.2-sf.1-sid.210943-st.A-v.2</dd>
					</dl>
					<dl>
						<dt>カテゴリURL<a href="#" target="_blank" class="checkUrl">確　認</a></dt>
						<dd></dd>
					</dl>
				</div>

				<div>
					<p>オススメ アイテム2</p>
				</div>

				<div>
					<p>オススメ アイテム3</p>
				</div>

				<div>
					<p>オススメ アイテム4</p>
				</div>
				<div>
					<p>オススメ アイテム5</p>
				</div>
				<div>
					<p>オススメ アイテム6</p>
				</div>
			</dd>
		</dl>
	</div>

	<div class="block6">
		<dl>
			<dt>戸村さんのテニスワンポイントレッスン</dt>
			<dd>第50回　C:\Users\racket210\Desktop\Design\Git\SKS\ORS_RCM\ORS_RCM\WebForms\html_Promotion</dd>
		</dl>
	</div> 
	</div><!-- /.prmEntry -->
    	<div class="btn"><asp:Button ID="btnConfirm" runat="server" Text="登　録" 
            onclick="btnConfirm_Click" /></div>
</div>

</div><!-- /.setListBox -->
<!-- /EditArea -->



</div><!--ComBlock-->
</div><!--CmnContents-->


</div><!-- #CmnWrapper -->


</asp:Content>
