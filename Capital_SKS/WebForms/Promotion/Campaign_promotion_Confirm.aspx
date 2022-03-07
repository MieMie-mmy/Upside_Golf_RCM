<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Campaign_promotion_Confirm.aspx.cs" Inherits="ORS_RCM.Campaign_promotion_Confirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

    <!DOCTYPE html>
<head>
<meta charset="UTF-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<title>商品管理システム＜キャンペーン確認＞</title>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/jscript"></script>
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>

<link href="../../Styles/base.css" rel="stylesheet" />
<link href="../../Styles/common.css" rel="stylesheet" />
<link href="../../Styles/manager-style.css" rel="stylesheet" />
<link href="../../Styles/promotion.css" rel="stylesheet" />

<link href="../../Styles/pagesno.css" rel="stylesheet" />


</head>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CmnContents">
	<div id="ComBlock" style="margin-top: 60px;">
		<div class="setListBox confBox iconSet iconEdit">
		<h1>キャンペーン確認</h1>

	<div class="prmCmnSet prmEntry cmnEdit inlineSet">
		<div>

	<div class="block1">
		<dl>
			<dt>キャンペーンID</dt>
			<dd><asp:Label ID="lblCampaignID"  runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>キャンペーン名</dt>
			<dd><asp:Label ID="lblCamName" runat="server" Text=""></asp:Label></dd>
		</dl>
		<dl>
			<dt>キャンペーン種別</dt>
			<dd><asp:Label ID="lblCampaingtype" runat="server"></asp:Label></dd>
		</dl>
	</div>

	<div class="block2">
		<dl>
			<dt>キャンペーンURL</dt>
			<dd>
				<ul>
					<li><b>PC用</b><a href="#" target="_blank">確　認</a></li>
						<li>  <asp:Label ID="lblCampaignpc"  runat="server"></asp:Label></li>
					<li><b>スマホ用</b><a href="#" target="_blank">確　認</a></li>
						<li><asp:Label ID="lblCampaign_smart"  runat="server"></asp:Label></li>
				</ul>
			</dd>
		</dl>
		<dl>
			<dt>キャンペーン内容</dt>
			<dd class="preBox"><asp:Label ID="lbltxtCam_Guideline" runat="server" Text=""></asp:Label>
</dd>
		</dl>
		<dl>
			<dt>メールマガジンイベントバナー</dt>
			<dd>
				<p><asp:Label ID="lblemailMagzine1" runat="server" Text=""/></p>
				<p><asp:Label ID="lblemailMagzine2" runat="server" Text=""/></p>
				<p><asp:Label ID="lblemailMagzine3" runat="server"></asp:Label></p>
			</dd>
		</dl>
	</div>

	<div class="block3">
		<dl>
			<dt>開催期間</dt>
			<dd style="height:50px">
				<p><asp:Label ID="lblPeriod_from" runat="server"></asp:Label><asp:Label ID="lblStart_time" runat="server"></asp:Label>～ <asp:Label ID="lblperiod_to" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblEndTime" runat="server"></asp:Label>
              </p>
			</dd>
		</dl>

		<dl>
			<dt>ステータス</dt> 
			<dd><asp:Label ID="lblstatus" runat="server" Text=""></asp:Label></dd>
		</dl>

		<dl>
			<dt>キャンペーンの強制終了</dt>
			<dd><asp:Label ID="lblproclose" runat="server"></asp:Label></dd>
		</dl>
	</div>

	<div class="block4">
		<dl>
			<dt>対象者</dt>
			<dd><asp:Label ID="lblSubject" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>対象ブランド</dt>
			<dd><asp:Label ID="lblbrand_name" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>対象指示書番号</dt>
			<dd><asp:Label ID="lblInstructionNo" runat="server" Text=""></asp:Label></dd>
		</dl>

		<dl>
			<dt>対象商品番号</dt>
			<dd class="l10"><asp:Label ID="lblshippingno" runat="server"></asp:Label>
                <asp:GridView ID="gvitem" runat="server" AutoGenerateColumns="False" 
                    Width="301px" GridLines="None">
                    <Columns>
                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblitem" runat="server" Text ='<%#Eval("Item_Code") %>'  Visible="false"></asp:Label>
                           
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>

                   </asp:GridView></dd>
		</dl>
		<dl>
			<dt>対象商品メモ</dt>
			<dd class="l10 preBox"><asp:Label ID="lblItem_memo" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>対象ショップ</dt>
			<dd>
            
                 <%--<asp:ListBox ID="lstTargetShop" runat="server" SelectionMode="Multiple" 
                            Height="200px" Width="400px" ></asp:ListBox>--%>

            
            <asp:Label ID="lblTargetshop" runat="server"></asp:Label></dd>
		</dl>

		<dl>
			<dt>応募方法</dt>
			<dd><asp:Label ID="lblApplicationMethod" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>プレゼント内容</dt>
			<dd class="l5 preBox"><asp:Label ID="lblGift_Contents" runat="server"  autocomplete ="off"></asp:Label></dd>
		</dl>
		<dl>
			<dt>プレゼント方法</dt>
			<dd><asp:Label ID="lblGiftway" runat="server"/></dd>
		</dl>

		<dl>
			<dt>制作対象</dt>
			<dd><asp:Label ID="lblProduction_target" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>関連情報参照先</dt>
			<dd class="bw"> <asp:Label ID="lblRelatedInformation" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>プレゼント</dt>
			<dd><asp:Label ID="lblchkGift"  runat="server"></asp:Label></dd>
		</dl>

		<div class="block4_1">
			<dl>
				<dt>備考</dt>
				<dd class="l10 preBox"><asp:Label ID="lblRemark" runat="server"></asp:Label></dd>
			</dl>
			<dl>
				<dt>制作詳細</dt>
				<dd><asp:Label ID="lblproduction_detail" runat="server"></asp:Label> </dd>
			</dl>
			<dl>
			
				<dt>キャンペーン画像 <asp:Button ID="lnkAddPhoto" runat="server" Text="画像登録"  Enabled="false"/></dt>
               <dd>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage1"><asp:Image runat="server" ID="Image1" />  </asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage2"><asp:Image runat="server" ID="Image2" /> </asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage3"><asp:Image runat="server" ID="Image3"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage4"><asp:Image runat="server" ID="Image4"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage5"><asp:Image runat="server" ID="Image5"/></asp:HyperLink></p>
                </dd>
				</dl>
		</div>
	</div>
	
	<div class="block5">
		<dl>
			<dt>楽天GOLD<br>FTPアップロード</dt>
			<dd>
				<p><asp:FileUpload runat="server" ID="fuRakuten_Gold1" /><asp:Label runat="server" ID="lblRakuten_Gold1"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuRakuten_Gold2" /><asp:Label runat="server" ID="lblRakuten_Gold2"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuRakuten_Gold3" /><asp:Label runat="server" ID="lblRakuten_Gold3"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuRakuten_Gold4" /><asp:Label runat="server" ID="lblRakuten_Gold4"><br /></asp:Label></p>
			</dd>
		</dl>
		<dl>
			<dt>楽天Cabinet<br>FTPアップロード</dt>
			<dd>
				<p><asp:FileUpload runat="server" ID="fuRakuten_Cabinet1" /><asp:Label runat="server" ID="lblRakuten_Cabinet1"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuRakuten_Cabinet2" /><asp:Label runat="server" ID="lblRakuten_Cabinet2"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuRakuten_Cabinet3" /><asp:Label runat="server" ID="lblRakuten_Cabinet3"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuRakuten_Cabinet4" /><asp:Label runat="server" ID="lblRakuten_Cabinet4"><br /></asp:Label></p>
			</dd>
		</dl>
		<dl>
			<dt>geocities<br>FTPアップロード</dt>
			<dd>
				<p><asp:FileUpload runat="server" ID="fuYahoo1" /><asp:Label runat="server" ID="lblYahoo1"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuYahoo2" /><asp:Label runat="server" ID="lblYahoo2"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuYahoo3" /><asp:Label runat="server" ID="lblYahoo3"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuYahoo4" /><asp:Label runat="server" ID="lblYahoo4"><br /></asp:Label></p>
			</dd>
		</dl>
		<dl>
			<dt>ポンパレ<br>FTPアップロード</dt>
			<dd>
				<p><asp:FileUpload runat="server" ID="fuPonpare1" /><asp:Label runat="server" ID="lblPonpare1"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuPonpare2" /><asp:Label runat="server" ID="lblPonpare2"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuPonpare3" /><asp:Label runat="server" ID="lblPonpare3"><br /></asp:Label></p>
				<p><asp:FileUpload runat="server" ID="fuPonpare4" /><asp:Label runat="server" ID="lblPonpare4"><br /></asp:Label></p>
			</dd>
		</dl>
	</div>

	<div class="block6">
		<dl>
			<dt>オプション</dt>
			<dd>
				<p><b>項目名</b><span><asp:Label ID="lblOp1" runat="server"></asp:Label></span><b>選択肢</b><span><asp:Label ID="lblOpVal1" runat="server" Text="" ></asp:Label></span></p>
				<p><b>項目名</b><span><asp:Label ID="lblOp2" runat="server"></asp:Label></span><b>選択肢</b><span><asp:Label ID="lblOpVal2" runat="server" Text=""></asp:Label></span></p>
				<p><b>項目名</b><span><asp:Label ID="lblOp3" runat="server"></asp:Label></span><b>選択肢</b><span><asp:Label ID="lblOpVal3" runat="server" Text=""></asp:Label></span></p>
			</dd>
		</dl>
	</div>

	<div class="block7">
		<dl>
			<dt>PC キャンペーン１</dt>
			<dd class="l5 preBox"><asp:Label ID="lblpc_campaign1" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>スマホ キャンペーン１</dt>
			<dd class="l5 preBox"><asp:Label ID="lblsmart1" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>PC キャンペーン２</dt>
			<dd class="l5 preBox"><asp:Label ID="lblpc_campaign2" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>スマホ キャンペーン２</dt>
			<dd class="l5 preBox"><asp:Label ID="lblsmart2" runat="server"></asp:Label></dd>
		</dl>
	</div>

	<div class="block8 block4">
		<dl>
			<dt>商品名装飾</dt>
			<dd class="preBox"><asp:Label ID="lblproduct_namedecoration" runat="server"/></dd>
		</dl>
		<dl>
			<dt>PCキャッチコピー装飾</dt>
			<dd class="preBox"><asp:Label ID="lblpcCatchCopy" runat="server"/></dd>
		</dl>
		<dl>
			<dt>スマホキャッチコピー装飾</dt>
			<dd class="preBox"><asp:Label ID="lblsmart_Catchcopy" runat="server"/></dd>
		</dl>
	</div>

	<div class="block9">
		<dl>
			<dt>公開</dt>
			<dd> <asp:Label ID="lblPublic" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>闇市設定</dt>
			<dd><asp:Label ID="lblBlackmarket" runat="server"></asp:Label></dd>
		</dl>
		<dl>
			<dt>シークレットID</dt>
			<dd><asp:Label ID="lblSecretID" runat="server"  autocomplete ="off"></asp:Label></dd>
		</dl>
		<dl>
			<dt>シークレットパスワード</dt>
			<dd><asp:Label ID="lblSecPassword" runat="server" Text="" /></dd>
		</dl>
		<dl>
			<dt>優先順位</dt>
			<dd> <asp:Label ID="lblPriority" runat="server"></asp:Label></dd>
		</dl>
	</div>


	</div><!-- /.prmEntry -->
    
    <div class="btn">
       
        <asp:Button runat="server" ID="btnSave" Text="登録"  onclick="btnSave_Click"/></div>



</div><!-- /.setListBox -->
<!-- /EditArea -->



</div><!--ComBlock-->
</div><!--CmnContents-->





   
</div></asp:Content>