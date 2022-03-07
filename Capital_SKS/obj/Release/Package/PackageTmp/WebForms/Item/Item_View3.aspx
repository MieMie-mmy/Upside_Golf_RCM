<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"  CodeBehind="Item_View3.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_View3" %>
<%@ Register Src="~/UCGrid_Paging.ascx" TagPrefix="uc" TagName="UCGrid_Paging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common1.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />
	
	<script type="text/javascript">

		function NewTab(ctrl, e) {
			var check = document.getElementById("<%=lblCheck.ClientID%>").innerHTML;
			if (check == 0) {
				document.forms[0].target = "";
				alert("Please Select At least One Item!");
				return;
			}
			else
				document.forms[0].target = "_blank";

			document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
			__doPostBack('', '');
		}

		function NewTab1(ctrl, e) {
			var check = document.getElementById("<%=lblCheck.ClientID%>").innerHTML;
			if (check == 0) {
				document.forms[0].target = "";
				alert("Please Select At least One Item!");
				return;
			}
			else {
				document.forms[0].target = "";

				var idList = document.getElementById("<%=lblShopCheckArray.ClientID%>").innerHTML;
				var arr = idList.split(',');

				for (var i = 0; i < arr.length; i += 3) {
					if (arr[i + 2] == "0") {
					    alert(arr[i+1] + " has no shop");
					    document.forms[0].target = "";
					    return;
						break;
					}
					else {
						document.forms[0].target = "_blank";
					}
				 }
			}

			document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
			__doPostBack('', '');
		}

		function doPostback(ctrl, e) {
			document.forms[0].target = "";
			document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
			__doPostBack('', '');
		}

		function customOpen(url) {
			var w = window.open(url, "target='_blank'");
			w.focus();
		}

		function doPostbackForLock(ctrl) {
			document.forms[0].target = "";
			document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
			__doPostBack('', '');
		}

			function checkboxClick(ctrl, e) {
				document.getElementById("<%=hfCtrl.ClientID%>").value = "";
			}

			function NewTabPreview(ctrl, e) {
				document.forms[0].target = "_blank";
				document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
				__doPostBack('', '');
			}

			function NewTabShop(ddl, e) {
				var index = ddl.selectedIndex;
				if (index == 0)
					return;
				else {
					document.forms[0].target = "_blank";
					document.getElementById("<%=hfCtrl.ClientID%>").value = ddl.id;
					__doPostBack('', '');
				}
//			    alert('Selected Value = ' + ddl.value + '\r\nSelected Index = ' + index);
			}

			function RemoveList(ctrl) {
				document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
			}

			function Preview(id) {
				var w = window.open
			('../Item/Item_Preview_Form.aspx?ID=' + id, "target='_blank'");
			}

			function ShopPreview(url) {
				var w = window.open(url);
//                if (mallID == 1) {
//                    window.open('http://item.rakuten.co.jp/' + shopName + '/' + itemCode, "target='_blank'");
//                }
			}

			function ImagePopUp(id, ctrl) {
				document.forms[0].target = "";
				document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
				var left = (screen.width / 2) - (600 / 2);
				var top = (screen.height / 2) - (500 / 2);
				var id = id;
				var w = window.open
					('../Item/ImageDialog.aspx?ID=' + id, window,
					'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
			}

function ShopCategoryPopUp(id,row) {
    //show modal dialog box and collect its return value
    var left = (screen.width / 2) - (600 / 2);
    var top = (screen.height / 2) - (500 / 2);
    retval = window.open
			('../Item/PopupCatagoryList.aspx?ID='+id+'&row='+row, window,
			'status=1,width=800,height=500,scrollbars=1,top=' + top + ',left=' + left);
}

			function OptionPopUp(id, ctrl) {
				document.forms[0].target = "";
				//declare a string variable
				document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
				var left = (screen.width / 2) - (600 / 2);
				var top = (screen.height / 2) - (500 / 2);
				var retval = "";
				//show modal dialog box and collect its return value
				retval = window.open
			('../Item/Item_Option_Select1.aspx?ID=' + id, window, 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
			}

			function MallCategoryPopUp(mallID, id, ctrl) {
				document.forms[0].target = "";
				//declare a string variable
				document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
				var left = (screen.width / 2) - (600 / 2);
				var top = (screen.height / 2) - (500 / 2);
				var retval = "";
				//show modal dialog box and collect its return value
				retval = window.open
					('../Item/Mall_Category_Choice.aspx?Mall_ID=' + mallID + '&ID=' + id, window, 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
			}

			function SpecPopUp(id,row) {
				document.forms[0].target = "";
//				document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
//				var control = ctrl.id;
//				
//				var res = control.split("_");
//				var index = res[res.length - 1];
//				var control = "MainContent_gvItem_txtyahoo_"+ index;
//				var yahooID = document.getElementById(control);
				var left = (screen.width / 2) - (600 / 2);
				var top = (screen.height / 2) - (500 / 2);
//				var retval = "";
				retval = window.open
		('../Item/Item_YahooSpecificValue.aspx?YahooMallCategoryID=' + id + '&row=' + row, window,
		 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
			}
	</script>

	<script type="text/javascript">
		$(function () {
			$("#hideBlock p").on("click", function () {
				$(this).next().slideToggle();
				$(this).toggleClass("active"); //追加部分
			});
		});
function btnSearch_onclick() {

}

</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<asp:HiddenField ID="hfCtrl" runat="server" Value="" />
<asp:Label runat="server"  ID="hfNewTab" Text="1" style="display:none;"/>
<asp:Label runat="server"  ID="hfRemoveList" Text="0" style="display:none;"/>
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox inlineSet iconSet iconEdit">
		<h1>商品情報クイック編集</h1>
<!-- Exhibition log search -->
		<div class="quickEdit resetValue searchBox">
			<h2>商品情報一覧検索</h2>
				<dl>
					<dt>商品名</dt>
					<dd><input type="text" id="txtItemName" runat="server" /></dd>
					<dt>商品番号</dt>
					<dd><input type="text" id="txtItemCode" runat="server" /></dd>
					<dt>画像ファイル名</dt>
					<dd><input type="text" id="txtImageFileName" runat="server" /></dd>
					<dt>カタログ情報</dt>
					<dd><input type="text" id="txtCatalogInfo" runat="server" /></dd>
				</dl>
				<div id="hideBlock">
				<p>詳細検索</p>
				<div id="hideBox">
					<dl>
						<dt>ブランド名</dt>
						<dd><input type="text" id="txtBrandName" runat="server" /></dd>
						<dt>カテゴリ名</dt>
						<dd><input type="text" id="txtCategoryName" runat="server" /></dd>
						<dt>競技名</dt>
						<dd><input type="text" id="txtCompetitionName" runat="server" /></dd>
						<dt>カラー名</dt>
						<dd><input type="text" id="txtColorName" runat="server" /></dd>
						<dt>年度</dt>
						<dd><input type="text" id="txtYear" runat="server" /></dd>
						<dt>シーズン</dt>
						<dd><input type="text" id="txtSeason" runat="server" /></dd>
						<dt>SKSステータス</dt>
						<dd><asp:DropDownList runat="server" ID="ddlSksStatus">
									<asp:ListItem Value=""></asp:ListItem>
									<asp:ListItem Value="1">ページ制作</asp:ListItem>
									<asp:ListItem Value="3">出品待ち</asp:ListItem>
									<asp:ListItem Value="2">期日出品待ち</asp:ListItem>
									<asp:ListItem Value="4">出品済</asp:ListItem>
							</asp:DropDownList>
						</dd>
						<dt>ショップ<br/>ステータス</dt>
						<dd><asp:DropDownList runat="server" ID="ddlShopStatus">
									<asp:ListItem Value=""></asp:ListItem>
									<asp:ListItem Value="n">未掲載</asp:ListItem>
									<asp:ListItem Value="u">掲載済</asp:ListItem>
									<asp:ListItem Value="d">削除</asp:ListItem>
							</asp:DropDownList>
						</dd>
						<dt>特記フラグ</dt>
						<dd><asp:DropDownList runat="server" ID="ddlSpecialFlag">
                            <asp:ListItem Value=""></asp:ListItem>
							<asp:ListItem Value="0">なし</asp:ListItem>
							<asp:ListItem Value="1">【★】</asp:ListItem>
							<asp:ListItem Value="3">【■】</asp:ListItem>
							<asp:ListItem Value="5">【F】</asp:ListItem>
							<asp:ListItem Value="6">【□】</asp:ListItem>
							<asp:ListItem Value="8">送料別途</asp:ListItem>
							<asp:ListItem Value="9">送料お見積もり</asp:ListItem>
                            <asp:ListItem Value="10">【◎】</asp:ListItem>
                            <asp:ListItem Value="11">【◆】</asp:ListItem>
							</asp:DropDownList>
						</dd>
						<dt>予約フラグ</dt>
						<dd><asp:DropDownList runat="server" ID="ddlReserveFlag">
								<asp:ListItem Value=""></asp:ListItem>
			                    <asp:ListItem Value="0">なし</asp:ListItem>
			                    <asp:ListItem Value="1">通常商品</asp:ListItem>
                                <asp:ListItem Value="2">予約商品</asp:ListItem>
                                <asp:ListItem Value="3">☆即☆</asp:ListItem>
							</asp:DropDownList>
						</dd>
						<dt>担当者</dt>
						<dd><asp:DropDownList runat="server" ID="ddlContactPerson">
							</asp:DropDownList>
						</dd>
						<dt>キーワード検 索</dt>
						<dd><input type="text" id="txtkeyword" runat="server" /></dd>
					</dl>
				</div>
				</div>
				<p><input type="button" id="btnSearch" onclick="doPostback(this,event);" value="検 索" runat="server"/></p>
                

		</div>
<!-- /Exhibition log search -->
	</div><!--setListBox-->
</div><!--ComBlock-->
</div><!--CmnContents-->

<div id="CmnContents2">
<div id="ComBlock2">
	<form action="#" method="get">
<!-- Checkbox allSET -->
		<div class="widthhMax operationBtn iconEx">
				<p>
				<input runat="server" type="button" id="btnSelectAll" onclick="doPostback(this,event);" value="全てを選択" />&nbsp;
				<input runat="server" type="button" id="btnUnSelectAll" onclick="doPostback(this,event);" value="全てを解除" />&nbsp;
				<input runat="server" type="button" id="btnExhibitSelectProduct" onclick="NewTab1(this,event);" value="選択商品を出品する" />&nbsp;
				<input runat="server" type="button" id="btnSelectItemNewTab" onclick="NewTab(this,event);" value="選択商品を別タブで開く" />&nbsp;
				<input runat="server" type="button" id="btnSelectItemRemove" onclick="doPostback(this,event);" value="選択商品をリストから外す" />
				</p>
				<div class="stSet sksST shopST">
					<p class="page"></p>･･･ページ制作
					<p class="wait1"></p>･･･出品待ち
					<p class="waitL"></p>･･･期日出品待ち
					<p class="ok1"></p>･･･出品済&nbsp;／&nbsp;
					<p class="wait"></p>･･･未掲載
					<p class="ok"></p>･･･掲載中
					<p class="del"></p>･･･削除
					<p class="lockIcon"></p>･･･ロック中
				</div>
		</div>
<!-- /heckbox allSET -->

<!-- exbition list -->	
<div class="itemCmnSet resetValue editBox">
	<asp:UpdatePanel runat="server" ID="uplgv">
		<ContentTemplate>
			<asp:GridView ID="gvItem" runat="server" ForeColor="#333333" AllowSorting="True"
		GridLines="None" AutoGenerateColumns="False"  CssClass="quickEdit listTable" RowStyle-Wrap="true"
		EmptyDataText="There is no data to display" AllowPaging="true" PageSize="10"
		ShowHeaderWhenEmpty="True" Height="306px" 
			onrowdatabound="gvItem_RowDataBound">
		<Columns>
			<asp:TemplateField HeaderText="行" ControlStyle-Width="10px">
				<ItemStyle VerticalAlign="Top" />
				<ItemTemplate>
					<asp:Label ID="lblNo" runat="server" Font-Bold="true" Text='<%#Eval("No") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField Visible="false">
				<ItemTemplate>
					<asp:Label runat="server" ID="lblLock" Text='<%#Eval("IsLock") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="対象" ControlStyle-Width="20px">
				<ItemStyle VerticalAlign="Top" />
				<ItemTemplate>
					<asp:UpdatePanel runat="server" ID="uplchk">
						<ContentTemplate>
							<asp:CheckBox runat="server" ID="chkItem" AutoPostBack="true" OnCheckedChanged="chkItem_CheckedChanged" />
						</ContentTemplate>
					</asp:UpdatePanel>					
					<asp:Label runat="server" ID="lblID" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ItemStyle-CssClass="stSet sksST">
				<ItemStyle VerticalAlign="Top" />
				<HeaderTemplate>SKS <br /> ST</HeaderTemplate>
				<ItemTemplate>
					<p id="Ppage" runat="server" class="page"></p>
					<p id="PWaitSt" runat="server" class="wait1"></p>
					<p id="PWaitL" runat="server" class="waitL"></p>
					<p id="POkSt" runat="server" class="ok1"></p>
					<asp:Label ID="lblSksStatusID" runat="server" Text='<%#Eval("Export_Status") %>' Visible="false"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ItemStyle-CssClass="stSet shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
				<HeaderTemplate>SHOP <br /> ST</HeaderTemplate>
				<ItemTemplate>
					<p id="PLock" runat="server" class="lockIcon" onclick="doPostbackForLock(this);"></p>   
					<p id="PWait" runat="server" class="wait"></p>
					<p id="POk" runat="server" class="ok"></p>
					<p id="PDel" runat="server" class="del"></p>
					<asp:Label runat="server" ID="lblShop_StatusID" Text='<%#Eval("Ctrl_ID") %>' Visible="false"></asp:Label>
				</ItemTemplate>
				<ItemStyle HorizontalAlign="Center" VerticalAlign="Top" />
			</asp:TemplateField>
			<asp:TemplateField HeaderText="商品番号．商品名．画像">
				<ItemStyle VerticalAlign="Top" />
				<ItemTemplate>
					<ul>
						<li><asp:Label runat="server" CssClass="labelAsLink" ID="lnkItemNo" Text='<%#Eval("Item_Code") %>' onclick="NewTabPreview(this,event);"></asp:Label></li>
						<%--<li><asp:LinkButton style="float:left;" ID="lnkItemNo1" OnClick="lnkItemNo_Click" runat="server" Text='<%#Eval("Item_Code") %>'></asp:LinkButton><br /></li>--%>
						<%--<li><asp:Label runat="server" ID="lnkItemNo" Text='<%#Eval("Item_Code") %>' style="text-decoration:underline; cursor:pointer;" onclick="NewTabPreview(this,event);"></asp:Label></li>--%>
						<li><asp:Label runat="server" ID="lblItemName" Text='<%#Eval("Item_Name") %>'></asp:Label></li>
						<li>
							<div><input type="button" id="btnPhotoSave" runat="server" value="画像を登録する" /></div>
							<p><asp:Image Height="100px" runat="server" ID="imgItem1"/></p>
							<p><asp:Image Height="100px" runat="server" ID="imgItem2"/></p>
							<p><asp:Image Height="100px" runat="server" ID="imgItem3"/></p>
							<p><asp:Image Height="100px" runat="server" ID="imgItem4"/></p>
							<p><asp:Image Height="100px" runat="server" ID="imgItem5"/></p>
						</li>
					</ul>  
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="SKU" ItemStyle-HorizontalAlign="Left">
				<ItemStyle VerticalAlign="Top" HorizontalAlign="Left" />
				<ItemTemplate>
					<dl>
						<dt>サイズ</dt><dd><asp:ListBox runat="server" ID="lstSize" Width="140px" style="height:auto"></asp:ListBox></dd>
						<dt>カラー</dt><dd><asp:ListBox runat="server" ID="lstColor" Width="140px" style="height:auto"></asp:ListBox></dd>
					</dl>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="ライブラリ画像">
				<ItemStyle VerticalAlign="Top" />
				<ItemTemplate>
					<p><asp:TextBox runat="server" ID="txtLibraryImage1" placeholder="ライブラリ画像１" /></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage2" placeholder="ライブラリ画像２" /></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage3" placeholder="ライブラリ画像３" /></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage4" placeholder="ライブラリ画像４" /></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage5" placeholder="ライブラリ画像５" /></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage6" placeholder="ライブラリ画像６" /></p>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="販売説明文">
				<ItemTemplate>
					<span>最大文字数&nbsp;全角5120</span>
					<asp:TextBox runat="server" TextMode="MultiLine" Rows="20" ID="txtsaleDetail" Text='<%#Eval("Sale_Description_PC") %>'></asp:TextBox>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="商品説明文">				
				<ItemTemplate>
					<span>最大文字数&nbsp;全角5120</span>
					<asp:TextBox runat="server" TextMode="MultiLine" Rows="20" ID="txtItemDetail" Text='<%#Eval("Item_Description_PC") %>'></asp:TextBox>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="オプション">
				<ItemTemplate>
					<dl>
						<dt><input type="button" runat="server" value="選ぶ" id="btnOption" /></dt>
						<dd><asp:TextBox runat="server" ID="txtOptionName1" placeholder="オプション"></asp:TextBox></dd>
						<dd><asp:TextBox runat="server" ID="txtOptionValue1" placeholder="オプション"></asp:TextBox></dd>
						<dd><asp:TextBox runat="server" ID="txtOptionName2" placeholder="オプション"></asp:TextBox></dd>
						<dd><asp:TextBox runat="server" ID="txtOptionValue2" placeholder="オプション"></asp:TextBox></dd>
						<dd><asp:TextBox runat="server" ID="txtOptionName3" placeholder="オプション"></asp:TextBox></dd>
						<dd><asp:TextBox runat="server" ID="txtOptionValue3" placeholder="オプション"></asp:TextBox></dd>
					</dl>					
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ItemStyle-CssClass="inlineSet">
				<HeaderTemplate>ショップ<br />カテゴリー</HeaderTemplate>
				<ItemTemplate>
					
                    <p><asp:Button runat="server" ID="btnShopCategory" Text="選ぶ" Width="170px" OnClick="btnShopCategory_Click" style="display: inline-block; background: rgb(238,238,238); margin-bottom:5px;" /></p>
					<p><asp:TextBox runat="server" ID="txtShopCategory1" placeholder="ショップカテゴリ１"></asp:TextBox></p>
					<p><asp:TextBox runat="server" ID="txtShopCategory2" placeholder="ショップカテゴリ２"></asp:TextBox></p>
					<p><asp:TextBox runat="server" ID="txtShopCategory3" placeholder="ショップカテゴリ３"></asp:TextBox></p>
					<p><asp:TextBox runat="server" ID="txtShopCategory4" placeholder="ショップカテゴリ４"></asp:TextBox></p>
					<p><asp:TextBox runat="server" ID="txtShopCategory5" placeholder="ショップカテゴリ５"></asp:TextBox></p>
					<ul>
						<li><span>楽天</span><asp:TextBox runat="server" ID="txtrakuten" Width="100px" ></asp:TextBox><input type="button" runat="server" value="選ぶ" id="btnRakuten" /></li>
						<li><span>Yahoo</span><asp:TextBox runat="server" ID="txtyahoo" Width="100px" ></asp:TextBox><input type="button" runat="server" value="選ぶ" id="btnYahoo" /></li>
						<li><span>ポンパレ</span><asp:TextBox runat="server" ID="txtponpare" Width="100px" ></asp:TextBox><input type="button" runat="server" value="選ぶ" id="btnPonpare" /></li>
					</ul>

					<asp:label ID="lblSCID1" runat="server" Visible="false"></asp:label>
					<asp:Label ID="lblSN1" runat="server" Visible="false"></asp:Label>
					<asp:label ID="lblSCID2" runat="server" Visible="false"></asp:label>
					<asp:Label ID="lblSN2" runat="server" Visible="false"></asp:Label>                      
					<asp:label ID="lblSCID3" runat="server" Visible="false"></asp:label>
					<asp:Label ID="lblSN3" runat="server" Visible="false"></asp:Label>                       
					<asp:label ID="lblSCID4" runat="server" Visible="false"></asp:label>
					<asp:Label ID="lblSN4" runat="server" Visible="false"></asp:Label>
					<asp:label ID="lblSCID5" runat="server" Visible="false"></asp:label>
					<asp:Label ID="lblSN5" runat="server" Visible="false"></asp:Label>                    
					<asp:Label runat="server" ID="lblRakutenCategoryID" Visible="false"></asp:Label>                   
					<asp:Label runat="server" ID="lblYahooCategoryID" Visible="false"></asp:Label>                  
					<asp:Label runat="server" ID="lblPonpareCategoryID" Visible="false"></asp:Label>                    
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="スペックID">
				<ItemStyle VerticalAlign="Top" />
				<ItemTemplate>
					<p><%--<input type="button" runat="server" value="選ぶ" id="btnSpec" />--%><asp:Button runat="server" ID="btnSpec" Text="選ぶ" OnClick="btnSpec_Click" /></p>
					<p><asp:TextBox runat="server" placeholder="スペックID１" ID="txtSpec1" /></p>
					<p><asp:TextBox runat="server" placeholder="スペックID２" ID="txtSpec2" /></p>
					<p><asp:TextBox runat="server" placeholder="スペックID３" ID="txtSpec3" /></p>
					<p><asp:TextBox runat="server" placeholder="スペックID４" ID="txtSpec4" /></p>
					<p><asp:TextBox runat="server" placeholder="スペックID５" ID="txtSpec5" /></p>
                    <asp:label ID="lblName1" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ID1" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ID2" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ID3" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ID4" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ID5" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_Name1" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_Name2" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_Name3" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_Name4" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_Name5" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ValueID1" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ValueID2" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ValueID3" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ValueID4" runat="server" Visible="false"></asp:label>
                    <asp:label ID="lblSpec_ValueID5" runat="server" Visible="false"></asp:label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="関連商品">
				<ItemStyle VerticalAlign="Top" />
				<ItemTemplate>
					<p><asp:TextBox runat="server" placeholder="関連商品１" ID="txtRelatedItem1" /></p>
					<p><asp:TextBox runat="server" placeholder="関連商品２" ID="txtRelatedItem2" /></p>
					<p><asp:TextBox runat="server" placeholder="関連商品３" ID="txtRelatedItem3" /></p>
					<p><asp:TextBox runat="server" placeholder="関連商品４" ID="txtRelatedItem4" /></p>
					<p><asp:TextBox runat="server" placeholder="関連商品５" ID="txtRelatedItem5" /></p>
					<p><asp:TextBox runat="server" placeholder="関連商品６" ID="txtRelatedItem6" /></p>
					<p><asp:CheckBox runat="server" Text="楽天 エビデンス" ID="chkRakutenEvidence" checked= '<%#Convert.ToBoolean(Eval("Rakutan_Evidence"))%>' /></p>
					<p><asp:CheckBox runat="server" Text="cloudshopモード" ID="chkCloudshopMode" checked= '<%#Convert.ToBoolean(Eval("Cloudshop_Mode"))%>' /></p>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField ItemStyle-VerticalAlign="Top">
				<HeaderTemplate>操作／ショップ<br>プレビュー</HeaderTemplate>
				<ItemTemplate>
					<p><input type="button" value="ロック解除" runat="server" id="btnClearLog" onclick="doPostback(this,event);" /></p>
					<%--<p><input type="button" value="プレビュー" runat="server" id="btnPreview" onclick="NewTabPreview(this,event);"/></p> --%>
					<p><input type="button" value="プレビュー" runat="server" id="btnPreview" onclick="NewTabPreview(this,event);"/></p>
					<p><input type="button" value="更　新" runat="server" id="btnUpdate" onclick="doPostback(this,event);" /></p>
					<p><input type="button" value="完　了" runat="server" id="btnFinish" onclick="doPostback(this,event);" /></p>
					<p><asp:DropDownList ID="ddlShop" onchange="NewTabShop(this,event);" Width="100px" runat="server"></asp:DropDownList></p>
					<asp:Label runat="server" ID="lblShopExist" Text="0" Visible="false"></asp:Label>
					<%--<p><asp:DropDownList ID="ddlShop" AutoPostBack="true" OnSelectedIndexChanged="ddlShop_SelectedIndexChanged" Width="100px" runat="server"></asp:DropDownList></p>--%>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
		<PagerSettings Visible="False" />
	</asp:GridView>

			<asp:Label runat="server" ID="lblCheck" Text="0" ForeColor="White" style=" display:block;"></asp:Label>
			<asp:Label runat="server" ID="lblShopCheckArray" Text="" style="display:none;"/>
		</ContentTemplate>
	</asp:UpdatePanel>
	
	</div>
<!-- /exbition list -->
	</form>

</div><!--ComBlock2-->
</div><!--CmnContents2-->

<div class="btn">
	<uc:UCGrid_Paging runat="server" ID="gp" onclick="checkboxClick(this,event);" />
</div>
</asp:Content>
