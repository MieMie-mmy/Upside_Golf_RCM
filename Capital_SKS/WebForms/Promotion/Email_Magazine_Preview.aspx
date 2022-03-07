<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Email_Magazine_Preview.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Email_Magazine_Preview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/jscript"></script>
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
<link href="../../Styles/promotion_base.css" rel="stylesheet" />
<link href="../../Styles/promotion_common.css" rel="stylesheet" />
<link href="../../Styles/promotion_manager-style.css" rel="stylesheet" />
<link href="../../Styles/promotion_promotion.css" rel="stylesheet" />
<link href="../../Styles/promotion_pagesno.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<%--<p id="toTop"><a href="#divtop">▲TOP</a></p>--%>

<div id="CmnContents">
	<div id="ComBlock" style="margin-top: 60px;">
		<div class="setListBox iconSet iconEdit">
		<h1>メールマガジン制作依頼</h1>

	<div class="prmCmnSet prmMailOrder prmMailEntry cmnEdit inlineSet">
		<div>

	<div class="block1">
		<dl>
			<dt>メールマガジンID</dt>
			<dd><asp:Label ID="lblEmailMagazineID" runat="server" Text=""></asp:Label></dd>
		</dl>

		<dl>
			<dt>配信予定日</dt>
			<dd><asp:Label ID="lblDeliveryDate" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblHour" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblMinute" runat="server" Text=""></asp:Label></dd>
		</dl>

		<dl>
			<dt>メールマガジン名（半角100byte/全角50文字）</dt>
			<dd><asp:TextBox ID="txtMailName" runat="server" ReadOnly="true"></asp:TextBox></dd>
		</dl>

	</div>

	<div class="block2">
		<dl>
			<dt>対象ショップ</dt>
			<dd><asp:Label ID="lblTarget_Shop" runat="server" Text=""></asp:Label></dd>
		</dl>
	</div>

	<div class="block3 hideBox">
		<dl>
			<dt>キャンペーン</dt>
			<dd class="piOpen_Cam">
				<ul>
					<li>ID：<asp:Label ID="lblCpg11" runat="server" Text=""></asp:Label></li>
					<li>
						<p>キャンペーン名</p>
						<asp:TextBox ID="txtCpg1" runat="server" ReadOnly="true"></asp:TextBox>
					</li>
					<li>
						<p>
                            
                            URL</p>
						<asp:TextBox ID="txtCpgURL1" runat="server" ReadOnly="true"></asp:TextBox>
					</li>
					<li>
						<p>バナー&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>楽天の場合、ファイル名のみ<br>表示されているバナーをDLして使用</span></a></p>
					<asp:TextBox ID="txtMailMagazineEvent1" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
				<ul>
					<li>ID：<asp:Label ID="lblCpg12" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpg2" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpgURL2" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent2" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
				<ul>
					<li>ID：<asp:Label ID="lblCpg13" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpg3" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpgURL3" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent3" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
				<ul>
					<li>ID：<asp:Label ID="lblCpg14" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpgURL4" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpg4" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent4" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
				<ul>
					<li>ID：<asp:Label ID="lblCpg15" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpg5" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpgURL5" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent5" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
				<ul>
					<li>ID：<asp:Label ID="lblCpg16" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpg6" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpgURL6" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent6" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
				<ul>
					<li>ID：<asp:Label ID="lblCpg17" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpg7" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpgURL7" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent7" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
				<ul>
					<li>ID：<asp:Label ID="lblCpg18" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpg8" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpgURL8" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent8" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
			<ul>
					<li>ID：<asp:Label ID="lblCpg19" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpg9" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpgURL9" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent9" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
				<ul>
					<li>ID：<asp:Label ID="lblCpg110" runat="server" Text=""></asp:Label></li>
					<li><p>キャンペーン名</p><asp:TextBox ID="txtCpg10" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li><p>URL</p><asp:TextBox ID="txtCpgURL10" runat="server" ReadOnly="true"></asp:TextBox></li>
					<li>
						<p>バナー</p>
					<asp:TextBox ID="txtMailMagazineEvent10" runat="server" ReadOnly="true"></asp:TextBox><img src="http://www.rakuten.ne.jp/gold/racket/r_event/images/header_dgp48h-150318.gif">
					</li>
				</ul>
			</dd>
		</dl>
	</div>

	<div class="block4 hideBox itemSet">
		<dl>
			<dt class="on">Pick up</dt>
			<dd class="piOpen_pu orderBox">
				<p>タイトル<textarea rows="2"readonly onClick="this.select();" >新商品が続々登場！</textarea></p>
				<div>
					<p>Pick up アイテム1</p>
					<dl>
						<dt>タイトル<span>（リンクテキスト）</span></dt>
						<dd><input type="text" readonly onClick="this.select();" value="HEAD 限定モデル入荷"></dd>
					</dl>
					<dl>
						<dt>商品情報</dt>
						<dd><textarea readonly onClick="this.select();">｢Graphene Radical MP」世界で最も強く、最も軽いHEADの新テクノロジー	</textarea></dd>
					</dl>
					<dl>
						<dt>商品ページ・検索URL</dt>
						<dd><input type="text" readonly onClick="this.select();" value="http://search.rakuten.co.jp/search/inshop-mall/%E3%82%AB%E3%82%B9%E3%82%BF%E3%83%A0%E3%82%A8%E3%83%83%E3%82%B8+V2%EF%BC%8E0/-/f.1-p.1-s.2-sf.1-sid.210943-st.A-v.2"></dd>
					</dl>
					<dl>
						<dt>カテゴリURL</dt>
						<dd><input type="text" readonly onClick="this.select();"></dd>
					</dl>
					<dl>
						<dt>備考</dt>
						<dd><textarea readonly onClick="this.select();">ピックアップバナーの制作お願いします</textarea></dd>
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

	<div class="block5 hideBox itemSet">
		<dl>
			<dt>スタッフのオススメ</dt>
			<dd class="piOpen_os orderBox">
				<div>
					<p>オススメ アイテム1</p>
					<dl>
						<dt>商品</dt>
						<dd><input type="text" readonly onClick="this.select();" placeholder="" value="ディアドラ／オールラウンド用シューズ"></dd>
					</dl>
					<dl>
						<dt>商品情報</dt>
						<dd><input type="text" readonly onClick="this.select();" placeholder="" value="【SALE】最大50%OFF"></dd>
					</dl>
					<dl>
						<dt>価格・フラグ・ポイント</dt>
						<dd>
							<p><span>販売価格</span><input type="text" readonly onClick="this.select();" placeholder="" value="123456">円</p>
							<p><span>アイコン１</span>送料無料</p>
							<p><span>アイコン２</span>SALE</p>
							<p><span>アイコン３</span>NEW</p>
							<p><span>ポイント倍率</span></p>
						</dd>
					</dl>
					<dl>
						<dt>商品ページ・検索URL</dt>
						<dd><input type="text" readonly onClick="this.select();" value="http://search.rakuten.co.jp/search/inshop-mall/%E3%82%AB%E3%82%B9%E3%82%BF%E3%83%A0%E3%82%A8%E3%83%83%E3%82%B8+V2%EF%BC%8E0/-/f.1-p.1-s.2-sf.1-sid.210943-st.A-v.2"></dd>
					</dl>
					<dl>
						<dt>カテゴリURL</dt>
						<dd><input type="text" readonly onClick="this.select();"></dd>
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
    	<div class="btn"><asp:Button ID="btnConfirm" runat="server" Text="閉じる" 
            onclick="btnConfirm_Click" /></div>

</div>

</div><!-- /.setListBox -->
<!-- /EditArea -->



</div><!--ComBlock-->
</div><!--CmnContents-->


</div><!-- #CmnWrapper -->

</form>



<script>
    $(function () {
        $(".block3 dt").on("click", function () {
            $("dd.piOpen_Cam").slideToggle();
            $(this).toggleClass("active");
        });
    });
    $(function () {
        $(".block4 dt.on").on("click", function () {
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
