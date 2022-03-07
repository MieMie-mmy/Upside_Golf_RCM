<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Email_Magazine_Entry.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Email_Magazine_Entry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../Styles/promotion_base.css" rel="stylesheet" />
<link href="../../Styles/promotion_common.css" rel="stylesheet" />
<link href="../../Styles/promotion_manager_style.css" rel="stylesheet" />
<link href="../../Styles/promotion_promotion.css" rel="stylesheet" />
<link href="../../Styles/promotion_pagesno.css" rel="stylesheet" />
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
<script src="../../Scripts/jquery.csv.js" type="text/javascript"></script>
<script src="../../Scripts/URLGenerate.js" type="text/javascript"></script>
<script type="text/javascript" language="javascript">
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
<script type="text/javascript" language="javascript">
$(document).ready(function () {
$("#<%=txtDeliveryDate.ClientID%>").datepicker(
{ 
showOn: 'button',
dateFormat: 'yy/mm/dd',
buttonImageOnly: true,
buttonImage:'../../images/calendar.gif',
changeMonth: true,
changeYear: true,
yearRange: "1900:2030" ,
}
);
$(".ui-datepicker-trigger").mouseover(function () {
$(this).css('cursor', 'pointer');
});

});
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#divtop">▲TOP</a></p>
    <asp:HiddenField ID="hfdate" runat="server" />
<div id="CmnContents">
	<div id="ComBlock" style="margin-top: 60px;">
		<div class="setListBox iconSet iconEdit">
		<h1>メールマガジン登録</h1>

	<div class="prmCmnSet prmMailEntry cmnEdit inlineSet">
		<div>

	<div class="block1">
		<dl>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                    ErrorMessage="***は必須項目です" ForeColor="Red" controltovalidate="txtEmailMagazineID" ValidationGroup="MyValidation" ></asp:RequiredFieldValidator> 
			<dt>メールマガジンID</dt>
			<dd>  <asp:TextBox runat="server" ID="txtEmailMagazineID" Width="120px"/>
                <asp:Label ID="lblEmailMagazineID" runat="server" Text=""></asp:Label>
            </dd>
		</dl>

		<dl>
         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                    ErrorMessage="***は必須項目です" ForeColor="Red" controltovalidate="txtDeliveryDate" ValidationGroup="MyValidation"></asp:RequiredFieldValidator>
			<dt>配信予定日</dt>
			<dd>
			<asp:TextBox runat="server" ID="txtDeliveryDate" ReadOnly="true" Width="120px"/>
     <asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom" />
    <asp:DropDownList ID="ddlHour" runat="server" Height="21px" Width="49px">
                    <asp:ListItem>1</asp:ListItem>
	                <asp:ListItem>2</asp:ListItem>
	                <asp:ListItem>3</asp:ListItem>
	                <asp:ListItem>4</asp:ListItem>
	                <asp:ListItem>5</asp:ListItem>
	                <asp:ListItem>6</asp:ListItem>
	                <asp:ListItem>7</asp:ListItem>
	                <asp:ListItem>8</asp:ListItem>
	                <asp:ListItem>9</asp:ListItem>
	                <asp:ListItem>10</asp:ListItem>
	                <asp:ListItem>11</asp:ListItem>
	                <asp:ListItem>12</asp:ListItem>
	                <asp:ListItem>13</asp:ListItem>
	                <asp:ListItem>14</asp:ListItem>
	                <asp:ListItem>15</asp:ListItem>
	                <asp:ListItem>16</asp:ListItem>
	                <asp:ListItem>17</asp:ListItem>
	                <asp:ListItem>18</asp:ListItem>
	                <asp:ListItem>19</asp:ListItem>
	                <asp:ListItem>20</asp:ListItem>
	                <asp:ListItem>21</asp:ListItem>
	                <asp:ListItem>22</asp:ListItem>
	                <asp:ListItem>23</asp:ListItem>
    </asp:DropDownList>
				:
				  <asp:DropDownList ID="ddlMinute" runat="server" Height="23px" Width="50px">
                    <asp:ListItem>00</asp:ListItem>
	                <asp:ListItem>15</asp:ListItem>
	                <asp:ListItem>30</asp:ListItem>
	                <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>59</asp:ListItem>
    </asp:DropDownList>
			    <asp:Label ID="lblDeliveryDate" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblHour" runat="server" Text=""></asp:Label>
                <asp:Label ID="lblMinute" runat="server" Text=""></asp:Label>
			</dd>
		</dl>

		<dl>    
			<dt>メールマガジン名（半角100byte/全角50文字）</dt>
			<dd>
                <asp:TextBox ID="txtMailName" runat="server"></asp:TextBox>
                <asp:Label ID="lblMailName" runat="server" Text=""></asp:Label>
            </dd>
		</dl>

	</div>

    <div class="block2">
		<dl>   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                  runat="server" Width="189px"
                          ControlToValidate="lstTarget_Shop" ErrorMessage="***は必須項目です"  
                  ForeColor="Red"  ValidationGroup="MyValidation"></asp:RequiredFieldValidator>
			<dt>対象ショップ&nbsp;<a href="#" class="exc hint"><img src="../../images/exclamation.png"><span>【注意】対象ショップを選びなおすと全て消えます</span></a></dt>
			<dd>
				<asp:ListBox ID="lstTarget_Shop" runat="server" SelectionMode="Single"></asp:ListBox>
&nbsp;<asp:Label ID="lblTarget_Shop" runat="server" Text=""></asp:Label>
            </dd>
		</dl>
	</div>

    <div class="block3 hideBox">
		<dl>
			<dt>キャンペーン&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>先にキャンペーンIDを入力してから反映</span></a></dt>
			<dd>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg1" Width="120px" placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg11" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL1" runat="server" placeholder="キャンペーンURL"></asp:TextBox> 
                        <asp:Label ID="lblCpgURL1" runat="server" Text=""></asp:Label>
                    </li>
					<li> <asp:TextBox ID="txtMailMagazineEvent11" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent21" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent31" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg1" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg2" Width="120px"  placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg12" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL2" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL2" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent12" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent22" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent32" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg2" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg3" Width="120px"  placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg13" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL3" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL3" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent13" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent23" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent33" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg3" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg4" Width="120px"  placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg14" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL4" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL4" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent14" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent24" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent34" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg4" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
			</dd>
			<dd class="piOpen_Cam">
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg5" Width="120px"  placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg15" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL5" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL5" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent15" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent25" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent35" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg5" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg6" Width="120px"  placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg16" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL6" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL6" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent16" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent26" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent36" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg6" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg7" Width="120px"  placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg17" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL7" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL7" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent17" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent27" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent37" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg7" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg8" Width="120px"  placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg18" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL8" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL8" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent18" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent28" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent38" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg8" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg9" Width="120px"  placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg19" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL9" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL9" runat="server" Text=""></asp:Label>
         
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent19" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent29" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent39" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li>
                        <asp:Label ID="lblCpg9" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
				<ul>
					<li> <asp:TextBox runat="server" ID="txtCpg10" Width="120px" placeholder="キャンペーンID"></asp:TextBox>
                        <asp:Label ID="lblCpg110" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtCpgURL10" runat="server" placeholder="キャンペーンURL"></asp:TextBox>
                        <asp:Label ID="lblCpgURL10" runat="server" Text=""></asp:Label>
                    </li>
					<li><asp:TextBox ID="txtMailMagazineEvent110" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent210" runat="server" Width="70px" ReadOnly="true"></asp:TextBox><asp:TextBox ID="txtMailMagazineEvent310" runat="server" Width="70px" ReadOnly="true"></asp:TextBox></li>
					<li><asp:Label ID="lblCpg10" runat="server" Text=""></asp:Label>
                    </li>
				</ul>
			</dd>
			<dd><p> <asp:Button ID="btnCampaignName" runat="server" Height="20px" Text="キャンペーン名の表示" 
              Width="177px" onclick="btnCampaignName_Click" /></p></dd>
		</dl>
	</div>

    <div class="block4 hideBox itemSet">
		<dl>
			<dt>Pick up&nbsp;<a href="#" class="exc hint"><img src="../../images/exclamation.png"><span>【注意】対象ショップを選びなおすと全て消えます</span></a></dt>
			<dd>タイトル&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>ピックアップに対してのタイトルとなります。複数行可</span></a><textarea rows="2"></textarea>
			</dd>
            <dd>
				<div>
					<p>Pick up アイテム1</p>
					<ul>
						<li>
                            <asp:TextBox ID="txtItem1" runat="server" placeholder="タイトル（リンクテキスト)"></asp:TextBox>
                            <asp:TextBox ID="txtProductInfo1" runat="server" placeholder="商品情報"></asp:TextBox>
                        </li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
									   <asp:TextBox ID="txtURL1" runat="server" placeholder="商品番号・検索ワード（カンマ区切り)"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										  <asp:DropDownList ID="ddlSearch11" runat="server" ></asp:DropDownList>
										  <asp:DropDownList ID="ddlSearch21" runat="server" ></asp:DropDownList>
									</p>
									<p>
									    <asp:DropDownList ID="ddlSearch31" runat="server" ></asp:DropDownList>
									</p>
									<p>
										    <asp:DropDownList ID="ddlSearch41" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate1" class="productPageGenerate">
									<!--	<asp:Button ID="btnCreateURL1" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL1_Click" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage1" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage1" placeholder="カテゴリURL（手入力"></asp:TextBox></li>
						<li> <asp:TextBox runat="server" ID="txtRemark1" placeholder="備考（バナーの指示等)"></asp:TextBox></li>
					</ul>
				</div>

                <div>
					<p>Pick up アイテム2</p>
					<ul>
						<li>
                            <asp:TextBox ID="txtItem2" runat="server" placeholder="タイトル（リンクテキスト)"></asp:TextBox>
                            <asp:TextBox ID="txtProductInfo2" runat="server" placeholder="商品情報"></asp:TextBox>
                        </li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
									   <asp:TextBox ID="txtURL2" runat="server" placeholder="商品番号・検索ワード（カンマ区切り)"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										  <asp:DropDownList ID="ddlSearch12" runat="server"  ></asp:DropDownList>
										  <asp:DropDownList ID="ddlSearch22" runat="server"  ></asp:DropDownList>
									</p>
									<p>
									    <asp:DropDownList ID="ddlSearch32" runat="server" ></asp:DropDownList>
									</p>
									<p>
										    <asp:DropDownList ID="ddlSearch42" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate2" class="productPageGenerate">
									<!--	<asp:Button ID="btnCreateURL2" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click2" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage2" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage2" placeholder="カテゴリURL（手入力"></asp:TextBox></li>
						<li> <asp:TextBox runat="server" ID="txtRemark2" placeholder="備考（バナーの指示等)"></asp:TextBox></li>
					</ul>
				</div>
                </dd>
                <dd class="piOpen_pu">
                <div>
					<p>Pick up アイテム3</p>
					<ul>
						<li>
                            <asp:TextBox ID="txtItem3" runat="server" placeholder="タイトル（リンクテキスト)"></asp:TextBox>
                            <asp:TextBox ID="txtProductInfo3" runat="server" placeholder="商品情報"></asp:TextBox>
                        </li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
									   <asp:TextBox ID="txtURL3" runat="server" placeholder="商品番号・検索ワード（カンマ区切り)"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										  <asp:DropDownList ID="ddlSearch13" runat="server" ></asp:DropDownList>
										  <asp:DropDownList ID="ddlSearch23" runat="server" ></asp:DropDownList>
									</p>
									<p>
									    <asp:DropDownList ID="ddlSearch33" runat="server" ></asp:DropDownList>
									</p>
									<p>
										    <asp:DropDownList ID="ddlSearch43" runat="server"></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate3" class="productPageGenerate">
									<!--	<asp:Button ID="btnCreateURL3" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click3" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage3" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage3" placeholder="カテゴリURL（手入力"></asp:TextBox></li>
						<li> <asp:TextBox runat="server" ID="txtRemark3" placeholder="備考（バナーの指示等" 
                                Height="24px" Width="231px"></asp:TextBox></li>
					</ul>
				</div>
                <div>
					<p>Pick up アイテム4</p>
					<ul>
						<li>
                            <asp:TextBox ID="txtItem4" runat="server" placeholder="タイトル（リンクテキスト)"></asp:TextBox>
                            <asp:TextBox ID="txtProductInfo4" runat="server" placeholder="商品情報"></asp:TextBox>
                        </li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
									   <asp:TextBox ID="txtURL4" runat="server" placeholder="商品番号・検索ワード（カンマ区切り"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										  <asp:DropDownList ID="ddlSearch14" runat="server" ></asp:DropDownList>
										  <asp:DropDownList ID="ddlSearch24" runat="server"  ></asp:DropDownList>
									</p>
									<p>
									    <asp:DropDownList ID="ddlSearch34" runat="server" ></asp:DropDownList>
									</p>
									<p>
										    <asp:DropDownList ID="ddlSearch44" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate4" class="productPageGenerate">
									<!--	<asp:Button ID="btnCreateURL4" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click4" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage4" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage4" placeholder="カテゴリURL（手入力"></asp:TextBox></li>
						<li> <asp:TextBox runat="server" ID="txtRemark4" placeholder="備考（バナーの指示等)"></asp:TextBox></li>
					</ul>
				</div>
             </dd>
		</dl>
	</div>

    <div class="block5 hideBox itemSet">
		<dl>
			<dt>スタッフのオススメ&nbsp;<a href="#" class="exc hint"><img src="../../images/exclamation.png"><span>【注意】対象ショップを選びなおすと全て消えます</span></a></dt>
			<dd>
				<div>
					<p>オススメ アイテム1</p>
					<ul>
						<li><asp:TextBox runat="server" ID="txtCommodity1" placeholder="商品"></asp:TextBox><asp:TextBox runat="server" ID="txtMerchandise1" placeholder="商品情報"></asp:TextBox></li>
						<li>
                             <asp:TextBox runat="server" ID="txtSellPrice1" placeholder="販売価格"></asp:TextBox>円
							 <asp:DropDownList ID="ddlSearch15" runat="server" Height="22px" Width="110px" ></asp:DropDownList>
						     <asp:DropDownList ID="ddlsearch25" runat="server" Height="22px" Width="110px"></asp:DropDownList>
							 <asp:DropDownList ID="ddlsearch35" runat="server" Height="22px" Width="110px"></asp:DropDownList>
                             <asp:DropDownList ID="ddlsearch45" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						</li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
								<asp:TextBox runat="server" ID="txtURL5" placeholder="商品番号・検索ワード（カンマ区切り）"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										<asp:DropDownList ID="ddlSearch55" runat="server" ></asp:DropDownList>
										<asp:DropDownList ID="ddlSearch65" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch75" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch85" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate5" class="productPageGenerate">
									<!--		<asp:Button ID="btnCreateURL5" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click5" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage5" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage5" placeholder="カテゴリURL（手入力）"></asp:TextBox></li>
					</ul>
				</div>
                	<div>
					<p>オススメ アイテム2</p>
					<ul>
						<li><asp:TextBox runat="server" ID="txtCommodity2" placeholder="商品"></asp:TextBox><asp:TextBox runat="server" ID="txtMerchandise2" placeholder="商品情報"></asp:TextBox></li>
						<li>
                              <asp:TextBox runat="server" ID="txtSellPrice2" placeholder="販売価格"></asp:TextBox>円
							 <asp:DropDownList ID="ddlSearch16" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						     <asp:DropDownList ID="ddlSearch26" runat="server" Height="22px" Width="110px"></asp:DropDownList>
							 <asp:DropDownList ID="ddlSearch36" runat="server" Height="22px" Width="110px"></asp:DropDownList>
                             <asp:DropDownList ID="ddlSearch46" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						</li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
								<asp:TextBox runat="server" ID="txtURL6" placeholder="商品番号・検索ワード（カンマ区切り）"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										<asp:DropDownList ID="ddlSearch56" runat="server" ></asp:DropDownList>
										<asp:DropDownList ID="ddlSearch66" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch76" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch86" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate6" class="productPageGenerate">
									<!--		<asp:Button ID="btnCreateURL6" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click6" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage6" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage6" placeholder="カテゴリURL（手入力）"></asp:TextBox></li>
					</ul>
				</div>
                </dd>
                <dd class="piOpen_os">
                <div>
					<p>オススメ アイテム3</p>
					<ul>
						<li><asp:TextBox runat="server" ID="txtCommodity3" placeholder="商品"></asp:TextBox><asp:TextBox runat="server" ID="txtMerchandise3" placeholder="商品情報"></asp:TextBox></li>
						<li>
                             <asp:TextBox runat="server" ID="txtSellPrice3" placeholder="販売価格"></asp:TextBox>円
							 <asp:DropDownList ID="ddlSearch17" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						     <asp:DropDownList ID="ddlSearch27" runat="server" Height="22px" Width="110px"></asp:DropDownList>
							 <asp:DropDownList ID="ddlSearch37" runat="server" Height="22px" Width="110px"></asp:DropDownList>
                              <asp:DropDownList ID="ddlSearch47" runat="server" Height="22px" Width="110px"></asp:DropDownList>
                             
						</li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
								<asp:TextBox runat="server" ID="txtURL7" placeholder="商品番号・検索ワード（カンマ区切り）"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										<asp:DropDownList ID="ddlSearch57" runat="server" ></asp:DropDownList>
										<asp:DropDownList ID="ddlSearch67" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch77" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch87" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate7" class="productPageGenerate">
									<!--		<asp:Button ID="btnCreateURL7" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click7" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage7" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage7" placeholder="カテゴリURL（手入力）"></asp:TextBox></li>
					</ul>
				</div>
                <div>
					<p>オススメ アイテム4</p>
					<ul>
						<li><asp:TextBox runat="server" ID="txtCommodity4" placeholder="商品"></asp:TextBox><asp:TextBox runat="server" ID="txtMerchandise4" placeholder="商品情報"></asp:TextBox></li>
						<li>
                             <asp:TextBox runat="server" ID="txtSellPrice4" placeholder="販売価格"></asp:TextBox>円
							 <asp:DropDownList ID="ddlSearch18" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						     <asp:DropDownList ID="ddlSearch28" runat="server" Height="22px" Width="110px"></asp:DropDownList>
							 <asp:DropDownList ID="ddlSearch38" runat="server" Height="22px" Width="110px"></asp:DropDownList>
                              <asp:DropDownList ID="ddlSearch48" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						</li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
								<asp:TextBox runat="server" ID="txtURL8" placeholder="商品番号・検索ワード（カンマ区切り）"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										<asp:DropDownList ID="ddlSearch58" runat="server" ></asp:DropDownList>
										<asp:DropDownList ID="ddlSearch68" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch78" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch88" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate8" class="productPageGenerate">
									<!--		<asp:Button ID="btnCreateURL8" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click8" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage8" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage8" placeholder="カテゴリURL（手入力）"></asp:TextBox></li>
					</ul>
				</div>
                <div>
					<p>オススメ アイテム5</p>
					<ul>
						<li><asp:TextBox runat="server" ID="txtCommodity5" placeholder="商品"></asp:TextBox><asp:TextBox runat="server" ID="txtMerchandise5" placeholder="商品情報"></asp:TextBox></li>
						<li>
                             <asp:TextBox runat="server" ID="txtSellPrice5" placeholder="販売価格"></asp:TextBox>円
							 <asp:DropDownList ID="ddlSearch19" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						     <asp:DropDownList ID="ddlSearch29" runat="server" Height="22px" Width="110px"></asp:DropDownList>
							 <asp:DropDownList ID="ddlSearch39" runat="server" Height="22px" Width="110px"></asp:DropDownList>
                              <asp:DropDownList ID="ddlSearch49" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						</li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
								<asp:TextBox runat="server" ID="txtURL9" placeholder="商品番号・検索ワード（カンマ区切り）"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										<asp:DropDownList ID="ddlSearch59" runat="server" ></asp:DropDownList>
										<asp:DropDownList ID="ddlSearch69" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch79" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch89" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate9" class="productPageGenerate">
									<!--		<asp:Button ID="btnCreateURL9" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click9" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage9" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage9" placeholder="カテゴリURL（手入力）"></asp:TextBox></li>
					</ul>
				</div>
                   <div>
					<p>オススメ アイテム6</p>
					<ul>
						<li><asp:TextBox runat="server" ID="txtCommodity6" placeholder="商品"></asp:TextBox><asp:TextBox runat="server" ID="txtMerchandise6" placeholder="商品情報"></asp:TextBox></li>
						<li>
                              <asp:TextBox runat="server" ID="txtSellPrice6" placeholder="販売価格"></asp:TextBox>円
							 <asp:DropDownList ID="ddlSearch110" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						     <asp:DropDownList ID="ddlSearch210" runat="server" Height="22px" Width="110px"></asp:DropDownList>
							 <asp:DropDownList ID="ddlSearch310" runat="server" Height="22px" Width="110px"></asp:DropDownList>
                              <asp:DropDownList ID="ddlSearch410" runat="server" Height="22px" Width="110px"></asp:DropDownList>
						</li>
						<li>
							<ol>
								<li>URL作成</li>
								<li>
								<asp:TextBox runat="server" ID="txtURL10" placeholder="商品番号・検索ワード（カンマ区切り）"></asp:TextBox>&nbsp;<a href="#" class="hint"><img src="../../images/hint.png"><span>完全に一致する商品番号以外で検索する場合、２つ以上のワードを入力してください。<br><br>商品番号の場合<br>【○】dia-rl5218 もしくは dia,rl5218　【×】rl5218<br><br>商品番号以外の場合<br>【○】ヨネックス,ウェア　【×】ヨネックス</span></a>
								</li>
								<li>
									<p>
										<asp:DropDownList ID="ddlSearch510" runat="server" ></asp:DropDownList>
										<asp:DropDownList ID="ddlSearch610" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch710" runat="server" ></asp:DropDownList>
									</p>
									<p>
										<asp:DropDownList ID="ddlSearch810" runat="server" ></asp:DropDownList>
									</p>
									<p><input type="button" value="URLの作成" id="productPageGenerate10" class="productPageGenerate">
									<!--		<asp:Button ID="btnCreateURL10" runat="server" Text="URLの作成" Height="19px"  Width="168px" onclick="btnCreateURL_Click10" /> --><a href="#" class="hint"><img src="../../images/hint.png"><span>検索条件は全て設定すること</span></a>
									</p>
								</li>
							</ol>
						</li>
						<li><asp:TextBox runat="server" ID="txtProductPage10" placeholder="商品ページ・検索URL"></asp:TextBox><asp:TextBox runat="server" ID="txtCategoryPage10" placeholder="カテゴリURL（手入力）"></asp:TextBox></li>
					</ul>
				</div>
                </dd>
		</dl>
	</div>
    <div class="block6">
		<dl>
			<dt>戸村さんのテニスワンポイントレッスン</dt>
			<dd><asp:TextBox runat="server" ID="txtName" placeholder="ある場合テキスト名を記入"></asp:TextBox>
    
            </dd>
		</dl>
	</div> 
	</div><!-- /.prmEntry -->

	<div class="btn"><asp:Button ID="btnConfirm" runat="server" Text="確認画面へ" 
            onclick="btnConfirm_Click"  ValidationGroup="MyValidation"/></div>

</div>

</div><!-- /.setListBox -->
</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>