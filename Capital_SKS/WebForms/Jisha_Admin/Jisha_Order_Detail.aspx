<%@ Page Title="" Language="C#" MasterPageFile="~/Jisha_Admin_Master.Master" AutoEventWireup="true" CodeBehind="Jisha_Order_Detail.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Order_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../../Styles/Jisha_base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Jisha_common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Jisha_style.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
		.style2
		{
			height: 25px;
		}
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="CmnContents">
	<div id="ComBlock">
	
	<div class="oDetailSet orderSet">
	<h1><span>注文詳細</span></h1>
	<div class="blockCmn">
		<dl class="inlineSet">
			<dt>受注番号</dt>
			<dd><asp:Label runat="server" ID="lblOrderID"></asp:Label></dd>
			<dt>注文日時</dt>
			<dd><asp:Label runat="server" ID="lblOrderDate"></asp:Label></dd>
		</dl>
	</div>

	<h2><span>注文情報</span></h2>
	<div class="blockCmn">
		<asp:GridView CssClass="tableSet" runat="server" ID="gvOrder" 
			AutoGenerateColumns="False">
			<Columns>
				<asp:TemplateField>
					<HeaderStyle Width="470px" />
					<HeaderTemplate>
						<asp:Label runat="server" ID="lblItemCodeHeader" Text="商品番号"></asp:Label>
					</HeaderTemplate>
					<ItemStyle Width="470px"/>
					<ItemTemplate>
						<asp:Label runat="server" ID="lblItemCode" Text='<%#Eval("Item_Code") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<HeaderStyle Width="800px" />
					<HeaderTemplate>
						<asp:Label runat="server" ID="lblItemNameHeader" Text="商品名"></asp:Label>
					</HeaderTemplate>
					<ItemStyle Width="800px"/>
					<ItemTemplate>
						<asp:Label runat="server" ID="lblItemName" Text='<%#Eval("Item_Name") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<HeaderStyle Width="30px" />
					<HeaderTemplate>
						<asp:Label runat="server" ID="lblQuantityHeader" Text="個数"></asp:Label>
					</HeaderTemplate>
					<ItemStyle Width="30px" />
					<ItemTemplate>
						<asp:Label runat="server" ID="lblQuantity" Text='<%#Eval("Quantity") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<HeaderTemplate>
						<asp:Label runat="server" ID="lblPriceHeader" Text="単価"></asp:Label>
					</HeaderTemplate>
					<ItemTemplate>
						<asp:Label runat="server" ID="lblPrice" Text='<%#Eval("Price") %>'></asp:Label>
					</ItemTemplate>
				</asp:TemplateField>
				<asp:TemplateField>
					<HeaderTemplate>
						<asp:Label runat="server" ID="lblSkuOptionHeader" Text="項目選択肢"></asp:Label>
					</HeaderTemplate>
					<ItemTemplate>
						<div>
							<asp:Label runat="server" ID="lblSkuOption" Text='<%#Eval("SKU_Option") %>'></asp:Label>
						</div>
					</ItemTemplate>
				</asp:TemplateField>
			</Columns>
		</asp:GridView>
	</div>
	
	<div class="block1 personal blockCmn">
	<h2><span>注文者情報</span></h2>
		<table>
			<tbody>
			<tr>
				<td>名字</td><td><asp:Label runat="server" ID="lblBillLastName"></asp:Label></td>
			</tr>
			<tr>
				<td>名前</td><td><asp:Label runat="server" ID="lblBillFirstName"></asp:Label></td>
			</tr>
			<tr>
				<td>名字(フリガナ)</td><td><asp:Label runat="server" ID="lblBillLastNameKana"></asp:Label></td>
			</tr>
			<tr>
				<td>名前(フリガナ)</td><td><asp:Label runat="server" ID="lblBillFirstNameKana"></asp:Label></td>
			</tr>
			<tr>
				<td>メールアドレス</td><td><asp:Label runat="server" ID="lblBillMailAddress"></asp:Label></td>
			</tr>
			<tr>
				<td>郵便番号</td><td><asp:Label runat="server" ID="lblBillZipCode"></asp:Label></td>
			</tr>
			<tr>
				<td>都道府県</td><td><asp:Label runat="server" ID="lblBillPrefecture"></asp:Label></td>
			</tr>
			<tr>
				<td>市区町村</td><td><asp:Label runat="server" ID="lblBillCity"></asp:Label></td>
			</tr>
			<tr>
				<td>番地</td><td><asp:Label runat="server" ID="lblBillAddress1"></asp:Label></td>
			</tr>
			<tr>
				<td>建物</td><td><asp:Label runat="server" ID="lblBillAddress2"></asp:Label></td>
			</tr>
			<tr>
				<td>電話番号１</td><td><asp:Label runat="server" ID="lblBillPhoneNo1"></asp:Label></td>
			</tr>
			<tr>
				<td>電話番号２</td><td><asp:Label runat="server" ID="lblBillPhoneNo2"></asp:Label></td>
			</tr>
			</tbody>
		</table>
<!--		<dl>
			<dt>名字</dt><dd>全角３２文字全角３２文字全角３２文字全角３２文字全角３２文字あい</dd>
			<dt>名前</dt><dd>あい全角３２文字全角３２文字全角３２文字全角３２文字全角３２文字</dd>
			<dt>名字(フリガナ)</dt><dd>アイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホマミ</dd>
			<dt>名前(フリガナ)</dt><dd>マミアイウエオカキクケコサシスセソタチツテトナニヌネノハヒフヘホ</dd>
		</dl>
		<dl>
			<dt>メールアドレス</dt><dd>abcde.fghij@sample.desuyo.com</dd>
		</dl>
		<dl class="address">
			<dt>郵便番号</dt><dd>702-7021</dd>
			<dt>都道府県</dt><dd>東京都</dd>
			<dt>市区町村</dt><dd>第3新東京市地下F区</dd>
			<dt>番地</dt><dd>第6番24号</dd>
			<dt>建物</dt><dd>ネルフ本部</dd>
		</dl>
		
		<dl>
			<dt>電話番号１</dt><dd>00-0000-1345</dd>
			<dt>電話番号２</dt><dd>00-0000-1345</dd>
		</dl>
-->
	</div>

	<div class="block2 personal blockCmn">
	<h2><span>送付先情報</span></h2>
		<table>
			<tbody>
			<tr>
				<td>名字</td><td><asp:Label runat="server" ID="lblShipLastName"></asp:Label></td>
			</tr>
			<tr>
				<td>名前</td><td><asp:Label runat="server" ID="lblShipFirstName"></asp:Label></td>
			</tr>
			<tr>
				<td>名字(フリガナ)</td><td><asp:Label runat="server" ID="lblShipLastNameKana"></asp:Label></td>
			</tr>
			<tr>
				<td>名前(フリガナ)</td><td><asp:Label runat="server" ID="lblShipFirstNameKana"></asp:Label></td>
			</tr>
			<tr>
				<td>メールアドレス</td><td><asp:Label runat="server" ID="lblShipMailAddress"></asp:Label></td>
			</tr>
			<tr>
				<td>郵便番号</td><td><asp:Label runat="server" ID="lblShipZipCode"></asp:Label></td>
			</tr>
			<tr>
				<td>都道府県</td><td><asp:Label runat="server" ID="lblShipPrefecture"></asp:Label></td>
			</tr>
			<tr>
				<td>市区町村</td><td><asp:Label runat="server" ID="lblShipCity"></asp:Label></td>
			</tr>
			<tr>
				<td>番地</td><td><asp:Label runat="server" ID="lblShipAddress1"></asp:Label></td>
			</tr>
			<tr>
				<td>建物</td><td><asp:Label runat="server" ID="lblShipAddress2"></asp:Label></td>
			</tr>
			<tr>
				<td>電話番号１</td><td><asp:Label runat="server" ID="lblShipPhoneNo1"></asp:Label></td>
			</tr>
			<tr>
				<td>電話番号２</td><td><asp:Label runat="server" ID="lblShipPhoneNo2"></asp:Label></td>
			</tr>
			</tbody>
		</table>
	</div>

	<h2><span>その他情報</span></h2>
	<div class="block3 blockCmn inlineSet">
		<dl>
			<dt>決済方法</dt><dd><asp:Label runat="server" ID="lblPaymentMethod"></asp:Label></dd>
			<dt>オーダーID</dt><dd><asp:Label runat="server" ID="lblSettleID"></asp:Label></dd>
			<dt>承認番号</dt><dd><asp:Label runat="server" ID="lblAutNo"></asp:Label></dd>
		</dl>
		<dl class="oDcomment">
			<dt><span>コメント</span></dt><dd><div><asp:Label runat="server" ID="lblComment"></asp:Label></div></dd>
		</dl>
		<dl>
			<dt>団体種別</dt><dd><asp:Label runat="server" ID="lblGroupType"></asp:Label></dd>
			<dt>団体名</dt><dd><asp:Label runat="server" ID="lblGroupName"></asp:Label></dd>
		</dl>
		<dl>
			<dt>メールマガジン</dt><dd><asp:Label runat="server" ID="lblMailMagazine"></asp:Label></dd>
		</dl>
		
		<dl class="kingaku">
			<dt>商品合計</dt><dd><asp:Label runat="server" ID="lblSubTotal"></asp:Label></dd>
			<dt>送料</dt><dd><asp:Label runat="server" ID="lblShipCharge"></asp:Label></dd>
			<dt>代引手数料</dt><dd><asp:Label runat="server" ID="lblDeliveryCharge"></asp:Label></dd>
			<dt>消費税</dt><dd><asp:Label runat="server" ID="lblTax"></asp:Label></dd>
			<dt>合計金額</dt><dd><asp:Label runat="server" ID="lblTotal"></asp:Label></dd>
		</dl>
	</div>



	</div><!--inlineSet-->

</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
