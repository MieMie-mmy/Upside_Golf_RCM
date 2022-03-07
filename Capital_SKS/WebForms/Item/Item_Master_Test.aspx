<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Master_Test.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Master_Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="UTF-8" />
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<title></title>
<link rel="stylesheet" href="../../Styles/base.css"/>
<link rel="stylesheet" href="../../Styles/common.css" />
<link rel="stylesheet" href="../../Styles/manager-style.css" />
<link rel="stylesheet" href="../../Styles/item.css" />
<link href="css/lightbox.css" rel="stylesheet" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/javascript"></script>
<%--
<script src="js/jquery.droppy.js"  type="text/javascript"></script>  
<script src="js/jquery.page-scroller.js"  type="text/javascript"></script> --%>

<script src="../../Scripts/jquery.droppy.js" type="text/javascript"></script>  
<%--<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  --%>

<script type="text/javascript">
    function isNumberKeys(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if (charCode == 13)
            return false;
        else return true;
    }
</script>

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

<script type="text/javascript">
    function SaveClick() {
        document.getElementById("<%=CustomHiddenField.ClientID%>").value = "";
    }
</script>

<script type="text/javascript">
    function ShowCatagoryList(ctrl) {
        var width = 600;
        var height = 500;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';

        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").innerHTML;

        var retval = window.open('../Item/PopupCatagoryList_Test.aspx?Item_Code=' + itemcode, window, params);

        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;

        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
	</script>

<script type="text/javascript">
    function ShowOption(ctrl) {

        var width = 600;
        var height = 500;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';

        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").innerHTML;
        var retval = window.open('../Item/Item_Option_Select1.aspx?Item_Code=' + itemcode, window, params);

        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;

        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
</script>

<script type="text/javascript">
    function ShowDialog(ctrl) {
        var width = 600;
        var height = 500;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';

        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").innerHTML;
        var retval = window.open('../Item/FileUpload_Dialog.aspx?Image_Type=0&Item_Code=' + itemcode, window, params);

        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;

        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
</script>

<script type="text/javascript">
    function ShowMallCategory(mallID, ctrl) {
        var width = 600;
        var height = 500;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';

        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").innerHTML;
        var retval = window.open('../Item/Mall_Category_Choice.aspx?Mall_ID=' + mallID + '&Item_Code=' + itemcode, window, params);

        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;

        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
	</script>

<script type="text/javascript">
    function ShowYahooSpecValue(ctrl) {
        var width = 600;
        var height = 500;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ', height=' + height;
        params += ', top=' + top + ', left=' + left;
        params += ', toolbar=no';
        params += ', menubar=no';
        params += ', resizable=yes';
        params += ', directories=no';
        params += ', scrollbars=yes';
        params += ', status=no';
        params += ', location=no';

        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").innerHTML;
        var hidderValue = document.getElementById("<%= txtYahoo_CategoryID.ClientID %>").value;

        var retval = window.open('../Item/Item_YahooSpecificValue.aspx?YahooMallCategoryID=' + hidderValue + '&Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;

        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
</script>
<%--
<script type = "text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("削除しますか？")) {
            confirm_value.value = "はい";
        } 
        else {
            confirm_value.value = "いいえ";
        }
        var val = document.forms[0].appendChild(confirm_value);
        if (val.value.toString() == "はい") {
            //document.forms[0].target = "_blank";
            location.reload();
        }
        
    }
</script>

<script type = "text/javascript">
    function Confirm1() {
        var confirm_value;
        $("#dialog-confirm").dialog({
            resizable: false,
            height: 200,
            modal: true,
            buttons: {
                "はい": function () {},
                "いいえ": function () {}
            }
        });
    }
</script>
--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<asp:HiddenField ID="CustomHiddenField" runat="server" />
<asp:HiddenField  ID="hdfCtrl_ID" runat="server"/>

<div id="CmnWrapper">
<!-- HEADER *header.html -->

<div id="CmnContents">
<div id="ComBlock">
<div class="setListBox iconSet iconEdit">
<h1>商品情報編集</h1>


<!-- EditArea -->
<div class="itemCmnSet editPage">
<%--<form>--%>

 	<div id="block1" class="cmnEdit inlineSet">
		<dl class="itemNo">
			<dt>商品番号</dt>
			<dd><asp:Label runat="server" ID="txtItem_Code" /></dd>
		</dl>

		<dl class="itemName">
			<dt>商品名</dt>
			<dd><asp:Label runat="server" ID="txtItem_Name"  /></dd>
		</dl>
		<dl>
			<dt>ブランド名</dt>
				<dd><asp:Label runat="server" ID="txtBrand_Name" /></dd>
			<dt>競技名</dt>
				<dd><asp:Label runat="server" ID="txtCompetition_Name"  /></dd>
			<dt>分類名</dt>
				<dd><asp:Label runat="server" ID="txtClass_Name" />
                </dd>
		</dl>


		<div id="imgEntryTechnology">
			<dl>
				<dt>商品画像  <input type="button" id ="lnkAddPhoto" onclick ="ShowDialog(this)" value="画像登録" runat="server"/></dt>
               <dd>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage1"><asp:Image runat="server" ID="Image1"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage2"><asp:Image runat="server" ID="Image2"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage3"><asp:Image runat="server" ID="Image3"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage4"><asp:Image runat="server" ID="Image4"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage5"><asp:Image runat="server" ID="Image5"/></asp:HyperLink></p>
                </dd>
			</dl>
			<dl>
				<dt>テクノロジー画像</dt>
				<dd>
					<p><asp:TextBox runat="server" ID="txtLibraryImage1" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage2" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage3" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage4" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage5" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
					<p><asp:TextBox runat="server" ID="txtLibraryImage6" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
				</dd>
			</dl>
		</div>
       
	</div><!--/#block1-->


<div id="scrollArea" class="cmnEdit inlineSet">
	<div id="hideBlock" class="skuBlock">
		<h2>SKUデータ </h2>
		<div id="hideBox" class="skubox">
		<div class="sku1">
         <asp:GridView runat="server" ID="gvSKU" onrowcreated="gvSKU_RowCreated" ></asp:GridView>
		</div>
			
		<h3>SKU サイズ&amp;カラー</h3>
		<div id="hideBox2" class="skubox">
			<div class="sku2">
             <asp:GridView runat="server" ID="gvSKUSize" onrowcreated="gvSKUSize_RowCreated" Width="100%"></asp:GridView>
            </div>
		    <div class="sku2 sku3">
             <asp:GridView runat="server" ID="gvSKUColor" onrowcreated="gvSKUColor_RowCreated" Width="100%"></asp:GridView> 
            </div>
            </div><!-- /#hideBox2 -->
	</div><!-- /#hideBox -->
	</div ><!-- /#hideBlock -->
<!-- /SKU -->

<div id="block2">
	<div class="dBlock itemInfo">
		<dl>
			<dt>年度</dt>
				<dd><asp:Label runat="server" ID="txtYear" /></dd>
			<dt>シーズン</dt>
				<dd><asp:Label runat="server" ID="txtSeason"  /></dd>
			<dt>カタログ情報</dt>
				<dd><asp:Label runat="server" ID="txtCatalog_Information" /></dd>
		</dl>
	</div>

	<div class="dBlock">
		<dl class="relatedProduct">
			<dt>関連商品</dt>
			<dd><asp:TextBox runat="server" ID="txtRelated1" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtRelated2" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtRelated3" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtRelated4" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtRelated5" onkeypress="return isNumberKeys(event)"/>
			</dd>
		</dl>
	</div>
	<div class="dBlock productInfo box2 inlineSet">
		<dl id="pd-Info">
			<dt>商品詳細情報</dt>
			<dd>
				<p><asp:TextBox runat="server" ID="txtDetail_Template1" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtDetail_Template_Content1"  TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtDetail_Template2" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtDetail_Template_Content2"  TextMode="MultiLine"/></p>
			</dd>
			<dd class="piOpen">
				<p><asp:TextBox runat="server" ID="txtDetail_Template3" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtDetail_Template_Content3"  TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtDetail_Template4" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtDetail_Template_Content4"  TextMode="MultiLine"/></p>
			</dd>
		</dl>
		<dl id="pb-Info2">
			<dt>商品基本情報</dt>
			<dd>
				<p><asp:TextBox runat="server" ID="txtTemplate1" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content1" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate2" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content2" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate3" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content3" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate4" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content4" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate5" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content5" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate6" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content6" TextMode="MultiLine"/></p>
			</dd>
			<dd class="piOpen2">
				<p><asp:TextBox runat="server" ID="txtTemplate7" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content7" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate8" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content8" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate9" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content9" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate10" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content10" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate11" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content11" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate12" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content12" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate13" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content13" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate14" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content14" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate15" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content15" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate16" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content16" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate17" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content17" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate18" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content18" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate19" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content19" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate20" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content20" TextMode="MultiLine"/></p>
			</dd>
		</dl>
	</div>

	<div class="dBlock txtDescription box2 inlineSet">
		<dl>
			<dt>PC用商品説明文</dt>
			<dd><asp:TextBox runat="server" ID="txtItem_Description_PC"  TextMode="MultiLine"/></dd>
		</dl>
		<dl>
			<dt>PC用販売説明文</dt>
			<dd><asp:TextBox runat="server" ID="txtSale_Description_PC" TextMode="MultiLine"/></dd>
		</dl>
	</div>


	<div class="dBlock inlineSet">
		<dl class="itemOption">
			<dt>オプション<br/><input type="button" id = "btnAddOption" onclick ="ShowOption(this)" value="選 ぶ" runat="server"/></dt>
			<dd>
				<p>項目名<asp:TextBox runat="server" ID="txtOptionName1" onkeypress="return isNumberKeys(event)"/></p><p>選択肢<asp:TextBox runat="server" ID="txtOptionValue1" onkeypress="return isNumberKeys(event)"/></p>
				<p>項目名<asp:TextBox runat="server" ID="txtOptionName2" onkeypress="return isNumberKeys(event)"/></p><p>選択肢<asp:TextBox runat="server" ID="txtOptionValue2" onkeypress="return isNumberKeys(event)"/></p>
				<p>項目名<asp:TextBox runat="server" ID="txtOptionName3" onkeypress="return isNumberKeys(event)"/></p><p>選択肢<asp:TextBox runat="server" ID="txtOptionValue3" onkeypress="return isNumberKeys(event)"/></p>
			</dd>
		</dl>
	</div>

	<div class="dBlock box2 inlineSet">
		<dl class="itemCampaign">
			<dt>キャンペーン画像</dt>
			<dd>
				<p><asp:TextBox runat="server" ID="txtCampaignImage1" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
				<p><asp:TextBox runat="server" ID="txtCampaignImage2" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
				<p><asp:TextBox runat="server" ID="txtCampaignImage3" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
				<p><asp:TextBox runat="server" ID="txtCampaignImage4" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
				<p><asp:TextBox runat="server" ID="txtCampaignImage5" MaxLength="24" onkeypress="return isNumberKeys(event)"/></p>
			</dd>
		</dl>
		<dl class="shopCategory">
			<dt>ショップカテゴリ <input type="button" id = "btnAddCatagories" onclick ="ShowCatagoryList(this)" value="選 ぶ" runat="server"/></dt>
			<dd>
            <asp:GridView ID="gvCatagories" runat="server" AutoGenerateColumns="False" ShowHeader="False" GridLines="None">
            <Columns>
            <asp:TemplateField HeaderText="ID" Visible="false">
            <ItemTemplate>
            <asp:Label runat="server" ID="lblID" />
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
            <p><asp:TextBox runat="server" ID="txtCTGName" onkeypress="return isNumberKeys(event)"/></p>
            <asp:Label runat="server" ID="lblCTGName" />
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            </asp:GridView>
			</dd>
		</dl>
	</div>
	
	<div class="dBlock itemCategory inlineSet">
		<dl>
			<dt>カテゴリID<br/>Yahooスペック</dt>
			<dd>
			<p>楽天 カテゴリID<asp:TextBox runat="server" ID="txtRakuten_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <input type="button" id = "btnRakuten_CategoryID" onclick ="ShowMallCategory(1,this)" value="選 ぶ" runat="server"/></p>
			<p>ヤフー カテゴリID<asp:TextBox runat="server" ID="txtYahoo_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <input type="button" id = "btnYahoo_CategoryID" onclick ="ShowMallCategory(2,this)" value="選 ぶ" runat="server"/></p>
			<p>ポンパレ カテゴリID<asp:TextBox runat="server" ID="txtPonpare_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <input type="button" id = "btnPonpare_CategoryID" onclick ="ShowMallCategory(3,this)" value="選 ぶ" runat="server"/></p>
			<p>ヤフースペック値<asp:TextBox runat="server" ID="txtYahooValue1" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <asp:TextBox runat="server" ID="txtYahooValue2" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <asp:TextBox runat="server" ID="txtYahooValue3" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <asp:TextBox runat="server" ID="txtYahooValue4" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <asp:TextBox runat="server" ID="txtYahooValue5" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <input type="button" id = "imgbYahooSpecValue" onclick ="ShowYahooSpecValue(this)" value="選 ぶ" runat="server"/></p>
			</dd>
		</dl>
 	</div>

	<div class="dBlock Exhibit">
	        <dl>
			<dt>出品対象ショップ</dt>
			<dd>
				<b>ラケットプラザ</b>
                <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                <asp:DataList ID="dlShop1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <ItemTemplate>
             <p> <asp:CheckBox runat="server" ID="ckbMall1Shop" Text='<%# Bind("Mall_Name")%>'/><asp:Label runat="server" ID="lblMall1ShopID" Text='<%# Bind("ID")%>' Visible="false" /></p>
                </ItemTemplate>
                </asp:DataList>
			</dd>
			<dd>
				<b>ラックピース</b>
                  <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                <asp:DataList ID="dlShop2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <ItemTemplate>
                <p><asp:CheckBox runat="server" ID="ckbMall2Shop" Text='<%# Bind("Mall_Name")%>'/> <asp:Label runat="server" ID="lblMall2ShopID" Text='<%# Bind("ID")%>' Visible="false" /> </p>
                </ItemTemplate>
                </asp:DataList>
			</dd>
			<dd>
				<b>スポーツプラザ</b>
                  <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                 <asp:DataList ID="dlShop3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <ItemTemplate>
                <p><asp:CheckBox runat="server" ID="ckbMall3Shop" Text='<%# Bind("Mall_Name")%>'/> <asp:Label runat="server" ID="lblMall3ShopID" Text='<%# Bind("ID")%>' Visible="false" /> </p>
                </ItemTemplate>
                </asp:DataList>
				</dd>
			<dd>
				<b>ベースボールプラザ</b>
                  <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                <asp:DataList ID="dlShop4" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <ItemTemplate>
                <p><asp:CheckBox runat="server" ID="ckbMall4Shop" Text='<%# Bind("Mall_Name")%>'/> <asp:Label runat="server" ID="lblMall4ShopID" Text='<%# Bind("ID")%>' Visible="false" /> </p>
                </ItemTemplate>
                </asp:DataList>
			</dd>
			<dd>
				<b>卓球本舗</b>
                  <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                <asp:DataList ID="dlShop5" runat="server"  RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Flow">
                <ItemTemplate >
                <p><asp:CheckBox runat="server" ID="ckbMall5Shop" Text='<%# Bind("Mall_Name")%>'/> <asp:Label runat="server" ID="lblMall5ShopID" Text='<%# Bind("ID")%>' Visible="false" /> </p>
                </ItemTemplate>
                </asp:DataList>
			</dd>
		</dl>
	</div>

	<div class="dBlock itemPriceFlag dlFloat">
		<dl>
			<dt>定価</dt><dd><asp:Label runat="server" ID="txtList_Price" />円</dd>
			<dt>販売価格</dt><dd><asp:Label runat="server" ID="txtSale_Price" />円</dd>
			<dt>発売日</dt><dd><asp:Label runat="server" ID="txtRelease_Date"/></dd>
			<dt>掲載可能日</dt><dd><asp:Label runat="server" ID="txtPost_Available_Date" /></dd>
		</dl>
		<dl>
			<dt>ブランドコード</dt><dd><asp:Label runat="server" ID="txtBrand_Code" /></dd>
			<dt>製品コード</dt><dd><asp:TextBox runat="server" ID="txtProduct_Code" onkeypress="return isNumberKeys(event)"/></dd>
			<dt>送料フラグ</dt>
			<dd>
             <asp:DropDownList runat="server" ID="ddlShipping_Flag" >
             <asp:ListItem Value="0">送料別</asp:ListItem>
             <asp:ListItem Value="1">送料込</asp:ListItem>
             </asp:DropDownList>
			</dd>
		</dl>
		<dl>
			<dt>個別送料</dt><dd><asp:TextBox runat="server" ID="txtExtra_Shipping" onkeypress="return isNumberKey(event)" MaxLength="8"/></dd>
			<dt>代引料フラグ</dt>
			<dd>
            <asp:DropDownList runat="server" ID="ddlDelivery_Charges" >
            <asp:ListItem Value="0">代引料別</asp:ListItem>
            <asp:ListItem Value="1">代引料込</asp:ListItem>
            </asp:DropDownList>
			</dd>
			<dt>倉庫指定</dt>
			<dd>
            <asp:DropDownList runat="server" ID="ddlWarehouse_Specified">
            <asp:ListItem Value="0">販売中</asp:ListItem>
            <asp:ListItem Value="1">倉庫</asp:ListItem>
            </asp:DropDownList>
			</dd>
		</dl>
		<dl class="itemFlag">
			<dt>闇市パスワード</dt><dd><asp:TextBox runat="server" ID="txtBlackMarket_Password" onkeypress="return isNumberKeys(event)"/></dd>
			<dt>二重価格文書管理番号</dt><dd><asp:TextBox runat="server" ID="txtDoublePrice_Ctrl_No" onkeypress="return isNumberKeys(event)"/></dd>
		</dl>
	</div>

	<div class="dBlock itemSpace">
		<dl>
			<dt>フリースペース２</dt>
			<dd><asp:TextBox runat="server" ID="txtAdditional_2"  TextMode="MultiLine"/></dd>
			<dt>フリースペース３</dt>
			<dd><asp:TextBox runat="server" ID="txtAdditional_3"  TextMode="MultiLine"/></dd>
			<dt>ゼット用項目（PC商品説明文）</dt>
			<dd><asp:TextBox runat="server" ID="txtZett_Item_Description" TextMode="MultiLine"/></dd>
			<dt>ゼット用項目（PC販売説明文）</dt>
			<dd><asp:TextBox runat="server" ID="txtZett_Sale_Description" TextMode="MultiLine"/></dd>
		</dl>
	</div>

	<div class="dBlock txtDescription box2 inlineSet">
		<dl>
			<dt>スマートフォン用商品説明文</dt>
			<dd><asp:TextBox runat="server" ID="txtSmart_Template" TextMode="MultiLine"/></dd>
		</dl>
		<dl>
			<dt>商品情報</dt>
			<dd><asp:TextBox runat="server" ID="txtMerchandise_Information" TextMode="MultiLine"/></dd>
		</dl>
	</div>
</div><!-- /#block2 -->

</div><!-- /#scrollArea -->
			<div class="btn"><p><asp:Button runat="server" ID="btnPreview" Text="プレビュー" onclick="btnPreview_Click" /><asp:Button runat="server" ID="btnSave" Text="登 録" OnClientClick="SaveClick()" onclick="btnSave_Click"/><asp:Button runat="server" ID="btnComplete" Text="完了" onclick="btnComplete_Click"/><%--<asp:Button runat="server" ID="btnDelete" Text="削除" OnClientClick ="Confirm()" onclick="btnDelete_Click"/>--%></p></div>
</div><!--/.itemCmnSet-->
<!-- /EditArea -->

</div><!--/.setListBox-->

	</div><!--ComBlock-->
 <%--   </form>--%>
</div><!--CmnContents-->

</div><!--CmnWrapper-->
</div>

<script src="js/lightbox.min.js" type="text/javascript"></script>

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
</asp:Content>
