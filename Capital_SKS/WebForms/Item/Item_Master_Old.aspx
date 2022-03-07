<%@ Page Title="商品情報" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Master_Old.aspx.cs" Inherits="ORS_RCM.Item" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--    <script type="text/javascript" src="../../Scripts/jquery.Item_Master.js"></script>--%>
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/javascript"></script>
<script src="../../Scripts/jquery.droppy.js" type="text/javascript"></script>  
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  

<script type="text/javascript">
	var windowObjectReference;
	function openRequestedPopup() {
		windowObjectReference = window.open("../Item/Item_Preview_Form.aspx",
			  "DescriptiveWindowName",
			  "menubar=yes,location=yes,resizable=yes,scrollbars=yes,status=yes");
	}
</script>

<script type="text/javascript">
	function __doPostBack(eventTarget, eventArgument) {
		if (!theForm.onsubmit || (theForm.onsubmit() != false)) {
			theForm.__EVENTTARGET.value = eventTarget;
			theForm.__EVENTARGUMENT.value = eventArgument;
			theForm.submit();
		}
	}
</script>

<script type="text/javascript">
	function isNumberKey(evt) {
		var charCode = (evt.which) ? evt.which : event.keyCode;
		if ((charCode >= 48 && charCode <= 57) || charCode == 8 || charCode == 46)
			return true;
		else return false;
	}
</script>

<%--<script type="text/javascript">
	function RadioCheck(rb) {
		var dl = document.getElementById("<%=dlPhoto.ClientID%>");
		var rbs = dl.getElementsByTagName("input");
		var row = rb.parentNode.parentNode;
		for (var i = 0; i < rbs.length; i++) {
			if (rbs[i].type == "radio") {
				if (rbs[i].checked && rbs[i] != rb) {
					rbs[i].checked = false;
					break;
				}
			}
		}
	}
</script>--%>

<script type="text/javascript">
	function ShowCatagoryList(ctrl) {
		var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
		hidSourceID.value = ctrl.id;
		var left = (screen.width / 2) - (600 / 2);
		var top = (screen.height / 2) - (500 / 2);
		//declare a string variable
		var retval = "";
		//show modal dialog box and collect its return value
		retval = window.open
		('../Item/PopupCatagoryList.aspx', window,
		 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
	}
	</script>

<script type="text/javascript">
	function ShowOption(SourceID) {
		var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
		hidSourceID.value = SourceID.id;
		var left = (screen.width / 2) - (600 / 2);
		var top = (screen.height / 2) - (500 / 2);
		//declare a string variable
		var retval = "";
		//show modal dialog box and collect its return value
		retval = window.open
	  ('../Item/Item_Option_Select1.aspx', window, 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
	}
</script>

<script type="text/javascript">
	function ShowDialog(imagetype, SourceID) {
		var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
		hidSourceID.value = SourceID.id;
		//declare a string variable
		var retval = "";
		var left = (screen.width / 2) - (600 / 2);
		var top = (screen.height / 2) - (500 / 2);
		//show modal dialog box and collect its return value
		retval = window.open
			('../Item/FileUpload_Dialog.aspx?Image_Type=' + imagetype, window, 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
	}
</script>

<script type="text/javascript">
	function ShowRelatedItem() {
		//declare a string variable
		var retval = "";
		//show modal dialog box and collect its return value
		retval = window.showModalDialog
		('../Item/Item_Choice.aspx', window,
		 'dialogHeight:500px; dialogWidth:1000px; dialogLeft:200px; dialogRight :200px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
		//check if user closed the dialog 
		//without selecting any value
		if (retval == undefined)
			retval = window.returnValue;
	}
</script>

<script type="text/javascript">
	function ShowMallCategory(mallID, ctrl) {
		var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
		hidSourceID.value = ctrl.id;
		//declare a string variable
		var left = (screen.width / 2) - (600 / 2);
		var top = (screen.height / 2) - (500 / 2);
		//declare a string variable
		var retval = "";
		//show modal dialog box and collect its return value
		retval = window.open
			('../Item/Mall_Category_Choice.aspx?Mall_ID=' + mallID, window, 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
		//check if user closed the dialog 
		//without selecting any value
	}
	</script>

<script type="text/javascript">
	function ShowYahooSpecValue(ctrl) {
		var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
		hidSourceID.value = ctrl.id;
		//declare a string variable
		var left = (screen.width / 2) - (600 / 2);
		var top = (screen.height / 2) - (500 / 2);
		var retval = "";
		var hidderValue = document.getElementById("<%= txtYahoo_CategoryID.ClientID %>").value;
		//show modal dialog box and collect its return value
		retval = window.open
		('../Item/Item_YahooSpecificValue.aspx?YahooMallCategoryID=' + hidderValue, window,
		 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
	}
</script>

<script type="text/javascript">
	$(function () {
		$("#hideBlock h2").on("click", function () {
			$(this).next().slideToggle();
			$(this).toggleClass("active"); //追加部分
		});
	});
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<input type="hidden" name="__EVENTTARGET" id="__EVENTTARGET" value="" />
<input type="hidden" name="__EVENTARGUMENT" id="__EVENTARGUMENT" value="" />
<asp:HiddenField ID="CustomHiddenField" runat="server" />
<asp:HiddenField runat="server" ID="hdfCtrl_ID"/>
<asp:HiddenField ID="hfValue" Value="" runat="server" />

<div id="CmnContents">
    <div id="ComBlock">
    <div class="setListBox iconSet iconEdit">
    <h1>商品情報編集</h1>
    <!-- EditArea -->

    <div class="itemCmnSet editPage">
    <div id="block1" class="cmnEdit inlineSet">
        <dl>
        <dt><asp:Label runat="server" ID="Label1" Text="商品番号" ToolTip="Item Code"/></dt>
        <dd><asp:Label runat="server" ID="txtItem_Code" /></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label2" Text="商品名" ToolTip="Item Name"/></dt>
        <dd><asp:Label runat="server" ID="txtItem_Name"  /></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label5" Text="定価" ToolTip="List Price"/></dt>
        <dd><asp:Label runat="server" ID="txtList_Price" /></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label6" Text="販売価格" ToolTip="Sale Prices"/></dt>
        <dd><asp:Label runat="server" ID="txtSale_Price" /></dd>
        </dl>

    <div id="block1_1">
    <dl>
    <dt><asp:Label ID="Label11" runat="server" Text="商品画像" ToolTip="Item Image" /> <input type="button" runat="server" value="画像登録" id="lnkAddPhoto" />
    <%--<asp:Button ID="lnkAddPhoto" runat="server" Text="画像登録" CausesValidation="False"  ></asp:Button>--%>
    <%--<input type="button" id="lnkAddPhoto" value="画像登録" onclick="ShowDialog(0,this.id);"  CausesValidation="False"  >--%>
    </dt>
    <dd>
	    <p><asp:Image runat="server" ID="Image1"/></p>
        <p><asp:Image runat="server" ID="Image2"/></p>
        <p><asp:Image runat="server" ID="Image3"/></p>
        <p><asp:Image runat="server" ID="Image4"/></p>
        <p><asp:Image runat="server" ID="Image5"/></p>
    </dd>
    </dl>
    <dl>
    <dt><asp:Label runat="server" ID="Label49" Text="ライブラリ画像" ToolTip="Library Image" /></dt>
    <dd><asp:TextBox runat="server" ID="txtLibraryImage1" />
            <asp:TextBox runat="server" ID="txtLibraryImage2" />
            <asp:TextBox runat="server" ID="txtLibraryImage3" />
            <asp:TextBox runat="server" ID="txtLibraryImage4" />
            <asp:TextBox runat="server" ID="txtLibraryImage5" /></dd>
    </dl>
    </div>
    </div>
    <section id="hideBlock" class="skuBlock">
    <h2>SKUデータ</h2>
    <div id="hideBox" class="skubox">
    <div style="width:auto;"float:left;">

    <p>SKU</p>
    <table style="float:left;background-color:Gray;"><tbody>
    <asp:GridView runat="server" ID="gvSKU" Width="450px"></asp:GridView>
    </tbody></table>

    <div style="width:230px">
    <p>SKU（サイズ）</p>
    <table style="float:left"><tbody>
    <asp:GridView runat="server" ID="gvSKUSize" Width="230px"></asp:GridView>
    </tbody></table>

    <div style="width:225px">
    <p>SKU（カラー）</p>
    <table style="float:left"><tbody>
    <asp:GridView runat="server" ID="gvSKUColor" Width="225px"></asp:GridView> 
    </tbody></table>
    </div>
    </section>
    <%--</div>--%>
	
    <div class="itemCmnSet editPage">
    <div class="cmnEdit inlineSet">
    <div id="scrollArea" class="cmnEdit">

    <div id="block2" class="inlineSet">
        <dl>
        <dt><asp:Label runat="server" ID="Label3" Text="PC用商品説明文" ToolTip="PC for item description"/></dt>
        <dd><asp:TextBox runat="server" ID="txtItem_Description_PC"  TextMode="MultiLine"/>
        </dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label25" Text="PC用販売説明文" ToolTip="PC for sale description"/></dt>
        <dd><asp:TextBox runat="server" ID="txtSale_Description_PC" TextMode="MultiLine"/></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label26" Text="スマートフォン用商品説明文" ToolTip="Smartphone for item description"/></dt>
        <dd><asp:TextBox runat="server" ID="txtSmart_Template" TextMode="MultiLine"/></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label27" Text="商品情報" ToolTip="Merchandise information"/></dt>
        <dd><asp:TextBox runat="server" ID="txtMerchandise_Information" TextMode="MultiLine"/></dd>
        </dl>
    </div>

    <div id="block3" class="inlineSet">
        <dl>
        <dt><asp:Label runat="server" ID="Label7" Text="オプション" ToolTip="Option" />
        <br/><%--<asp:Button runat="server" ID="btnAddOption" Text="選ぶ"  CausesValidation="False" />--%>
        <input type="button" runat="server" value="選ぶ" id="btnAddOption" />
        </dt>
        <dd>
        <p><asp:Label runat="server" ID="Label20" Text="項目名" /><asp:TextBox runat="server" ID="txtOptionName1" /></p><p><asp:Label runat="server" ID="Label18" Text="選択肢" /><asp:TextBox runat="server" ID="txtOptionValue1" /></p>
        <p><asp:Label runat="server" ID="Label19" Text="項目名" /><asp:TextBox runat="server" ID="txtOptionName2" /></p><p><asp:Label runat="server" ID="Label21" Text="選択肢" /><asp:TextBox runat="server" ID="txtOptionValue2"  /></p>
        <p><asp:Label runat="server" ID="Label22" Text="項目名" /><asp:TextBox runat="server" ID="txtOptionName3" /></p><p><asp:Label runat="server" ID="Label23" Text="選択肢" /><asp:TextBox runat="server" ID="txtOptionValue3"  /></p>
        </dd>
        </dl>
    </div>

    <div id="block4" class="inlineSet">
        <dl>
        <dt><asp:Label runat="server" ID="Label4" Text="ショップカテゴリ" ToolTip="Shop Category" />
        <br/><%--<asp:Button runat="server" ID="btnAddCatagories" Text="選ぶ" CausesValidation="False" />--%>
        <input type="button" runat="server" value="選ぶ" id="btnAddCatagories" />
        </dt>
        <dd>
        <asp:GridView ID="gvCatagories" runat="server" AutoGenerateColumns="False" 
        ShowHeader="False" CellPadding="4" ForeColor="#333333" GridLines="None">
        <Columns>
        <asp:TemplateField HeaderText="ID" Visible="false">
        <ItemTemplate>
        <asp:Label runat="server" ID="lblID" />btnAddCatagories
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <asp:TextBox runat="server" ID="txtCTGName" Width="200px" />
        <asp:Label runat="server" ID="lblCTGName" />
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        </asp:GridView>
        </dd>			
        </dl>
    <dl>
    <dt>カテゴリID</dt><%--Mall Category--%>
    <dd>
    <p><asp:Label runat="server" ID="Label13" Text="楽天" ToolTip="Rakuten Category ID" /><asp:TextBox runat="server" ID="txtRakuten_CategoryID" onkeypress="return isNumberKey(event);" ReadOnly="true"/><%--<asp:Button runat="server" ID="btnRakuten_CategoryID" Text="選ぶ" CausesValidation="False" />--%>
       <input type="button" runat="server"  value="選ぶ" id="btnRakuten_CategoryID" /></p>			
    <p><asp:Label runat="server" ID="Label14" Text="ヤフー" ToolTip="Yahoo Category ID" /><asp:TextBox runat="server" ID="txtYahoo_CategoryID" onkeypress="return isNumberKey(event);" ReadOnly="true"/><%--<asp:Button runat="server" ID="btnYahoo_CategoryID" Text="選ぶ"  CausesValidation="False" />--%>
            <input type="button" runat="server" value="選ぶ" id="btnYahoo_CategoryID" /></p>			
    <p><asp:Label runat="server" ID="Label15" Text="ポンパレ" ToolTip="Ponpare Category ID"/><asp:TextBox runat="server" ID="txtPonpare_CategoryID" onkeypress="return isNumberKey(event);" ReadOnly="true"/><%--<asp:Button runat="server" ID="btnPonpare_CategoryID" Text="選ぶ"  CausesValidation="False" />--%><input type="button" runat="server" value="選ぶ" id="btnPonpare_CategoryID" /></p>
    </dd>
    </dl>
    </div>

    <div id="block5">
        <dl>
        <dt><asp:Label runat="server" ID="Label24" Text="ヤフースペック値" ToolTip="Yahoo Specification Value"/></dt><%--Yahoo Specification Value--%>
        <dd>
        <asp:TextBox runat="server" ID="txtYahooValue1" ReadOnly="true" />
        <asp:TextBox runat="server" ID="txtYahooValue2" ReadOnly="true" />
        <asp:TextBox runat="server" ID="txtYahooValue3" ReadOnly="true" />
        <asp:TextBox runat="server" ID="txtYahooValue4" ReadOnly="true" />
        <asp:TextBox runat="server" ID="txtYahooValue5" ReadOnly="true" />
        <%--<asp:Button runat="server" ID="imgbYahooSpecValue" Text="選ぶ"  CausesValidation="False" />--%>
        <input type="button" runat="server" value="選ぶ" id="imgbYahooSpecValue" />
        </dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label8" Text="関連商品"  ToolTip="Related Item"/></dt><%--Related Item--%>
        <dd>
        <asp:TextBox runat="server" ID="txtRelated1" />
        <asp:TextBox runat="server" ID="txtRelated2" />
        <asp:TextBox runat="server" ID="txtRelated3" />
        <asp:TextBox runat="server" ID="txtRelated4" />
        <asp:TextBox runat="server" ID="txtRelated5"/>
        </dd>			
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="lblShopList" Text="出品対象ショップ" ToolTip="Exhibition object shop"/></dt>
        <dd>
        <div><asp:Label runat="server" ID="Label44" Text="ラケットプラザ" ToolTip="Racket Plaza" /></div>
        <asp:DataList ID="dlShop1" runat="server" BackColor="White" BorderStyle="None"  >
        <ItemTemplate>
        <p><asp:CheckBox runat="server" ID="ckbMall1Shop" Text='<%# Bind("Mall_Name")%>'/></p>
        <asp:Label runat="server" ID="lblMall1ShopID" Text='<%# Bind("ID")%>' Visible="false" /> 
        </ItemTemplate>
        </asp:DataList>
        </dd>
        <dd>
        <div><asp:Label runat="server" ID="Label45" Text="	ラックピース" ToolTip="Rack piece" /></div>
        <asp:DataList ID="dlShop2" runat="server" BackColor="White" BorderStyle="None" >
        <ItemTemplate>
        <p><asp:CheckBox runat="server" ID="ckbMall2Shop" Text='<%# Bind("Mall_Name")%>'/></p>
        <asp:Label runat="server" ID="lblMall2ShopID" Text='<%# Bind("ID")%>' Visible="false" /> 
        </ItemTemplate>
        </asp:DataList>
        </dd>
        <dd>
        <div><asp:Label runat="server" ID="Label46" Text="スポーツプラザ" ToolTip="Sports Plaza" /></div>
        <asp:DataList ID="dlShop3" runat="server" BackColor="White" BorderStyle="None">
        <ItemTemplate>
        <p><asp:CheckBox runat="server" ID="ckbMall3Shop" Text='<%# Bind("Mall_Name")%>'/></p>
        <asp:Label runat="server" ID="lblMall3ShopID" Text='<%# Bind("ID")%>' Visible="false" /> 
        </ItemTemplate>
        </asp:DataList>
        </dd>
        <dd>
        <div><asp:Label runat="server" ID="Label47" Text="ベースボールプラザ" ToolTip="Baseball Plaza"/></div>
        <asp:DataList ID="dlShop4" runat="server" BackColor="White" BorderColor="#666666"
        BorderStyle="None"   Font-Names="Verdana" Font-Size="Small" >
        <ItemTemplate>
        <p><asp:CheckBox runat="server" ID="ckbMall4Shop" Text='<%# Bind("Mall_Name")%>'/></p>
        <asp:Label runat="server" ID="lblMall4ShopID" Text='<%# Bind("ID")%>' Visible="false" /> 
        </ItemTemplate>
        </asp:DataList>
        </dd>
        <dd>
        <div><asp:Label runat="server" ID="Label48" Text="卓球本舗" ToolTip="Table Tennis Honpo" /></div>
        <asp:DataList ID="dlShop5" runat="server" BackColor="White" BorderColor="#666666"
        BorderStyle="None" Font-Names="Verdana" Font-Size="Small" >
        <ItemTemplate>
        <p><asp:CheckBox runat="server" ID="ckbMall5Shop" Text='<%# Bind("Mall_Name")%>'/></p>
        <asp:Label runat="server" ID="lblMall5ShopID" Text='<%# Bind("ID")%>' Visible="false" /> 
        </ItemTemplate>
        </asp:DataList>
        </dd>
        </dl>
    </div>

    <div id="block6">
        <dl>
        <dt><asp:Label runat="server" ID="Label10" Text="製品コード" ToolTip="Product Code" /></dt>
        <dd><asp:TextBox runat="server" ID="txtProduct_Code" /></dd>			
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label28" Text="送料フラグ" ToolTip="Shipping flag" /></dt>
        <dd>
        <asp:DropDownList runat="server" ID="ddlShipping_Flag" >
        <asp:ListItem Value="0">送料別</asp:ListItem>
        <asp:ListItem Value="1">送料込</asp:ListItem>
        </asp:DropDownList>
        </dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label29" Text="個別送料" ToolTip="Shipping Cost"/></dt>
        <dd><asp:TextBox runat="server" ID="txtExtra_Shipping" onkeypress="return isNumberKey(event)" MaxLength="8"/></dd>			
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label30" Text="代引料フラグ" ToolTip="Delivery Charges" /></dt>
        <dd>
        <asp:DropDownList runat="server" ID="ddlDelivery_Charges" >
        <asp:ListItem Value="0">代引料別</asp:ListItem>
        <asp:ListItem Value="1">代引料込</asp:ListItem>
        </asp:DropDownList>
        </dd>			
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label31" Text="倉庫指定" ToolTip="Warehouse specified" /></dt>
        <dd>
        <asp:DropDownList runat="server" ID="ddlWarehouse_Specified">
        <asp:ListItem Value="0">販売中</asp:ListItem>
        <asp:ListItem Value="1">倉庫</asp:ListItem>
        </asp:DropDownList>
        </dd>			
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label32" Text="闇市パスワード" ToolTip="Black market password" /></dt>
        <dd><asp:TextBox runat="server" ID="txtBlackMarket_Password" autocomplete="off"  /></dd>			
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label33" Text="二重価格文書管理番号" ToolTip="Double price Document Control Number"/></dt>
        <dd><asp:TextBox runat="server" ID="txtDoublePrice_Ctrl_No" /></dd>			
        </dl>
    <div class="inlineSet">
        <dl>
        <dt><asp:Label runat="server" ID="Label34" Text="フリースペース２" ToolTip="Free Space 2" /></dt>
        <dd><asp:TextBox runat="server" ID="txtAdditional_2"  TextMode="MultiLine"/></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label35" Text="フリースペース３" ToolTip="Free Space 3"  /></dt>
        <dd><asp:TextBox runat="server" ID="txtAdditional_3"  TextMode="MultiLine"/></dd>
        </dl>
    </div>
    </div>


    <div id="block7">
        <dl>
        <dt><asp:Label runat="server" ID="Label36" Text="発売日" ToolTip="Release Date"/></dt>
        <dd><asp:Label runat="server" ID="txtRelease_Date"/></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label37" Text="掲載可能日" ToolTip="Post Available Date"/></dt>
        <dd><asp:Label runat="server" ID="txtPost_Available_Date" /> </dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label38" Text="年度" ToolTip="Year" /></dt>
        <dd><asp:Label runat="server" ID="txtYear" /> </dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label39" Text="シーズン" ToolTip="Season" /></dt>
        <dd><asp:Label runat="server" ID="txtSeason"  /></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label40" Text="競技名" ToolTip="Competition name" /></dt>
        <dd><asp:Label runat="server" ID="txtCompetition_Name"  /></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label41" Text="分類名" ToolTip="Class name"  /></dt>
        <dd><asp:Label runat="server" ID="txtClass_Name" /></dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label42" Text="カタログ情報" ToolTip="Catalog number" /></dt>
        <dd><asp:Label runat="server" ID="txtCatalog_Information" /> </dd>
        </dl>
        <dl>
        <dt><asp:Label runat="server" ID="Label43" Text="ブランドコード" ToolTip="Brand Code" /></dt>
        <dd><asp:Label runat="server" ID="txtBrand_Code" /></dd>
        </dl>
    </div><!-- /#scrollArea -->
    </div>
    </div>

    <div class="btn">
        <asp:Button runat="server" ID="btnPreview" Text="プレビュー" onclick="btnPreview_Click" />
        <asp:Button runat="server" ID="btnSave" Text="登 録" onclick="btnSave_Click"/>
        <asp:Button runat="server" ID="btnComplete" Text="完了" onclick="btnComplete_Click"/>
    </div>
    </div>
    <!-- EditArea -->
    </div>
    </div>
    </div>
    </div>
</asp:Content>