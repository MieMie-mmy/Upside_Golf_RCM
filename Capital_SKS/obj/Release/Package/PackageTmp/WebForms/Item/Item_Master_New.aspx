<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Master_New.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Master_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="../../Styles/tab-style.css" />
<link rel="stylesheet" href="../../Styles/base.css"/>
<link rel="stylesheet" href="../../Styles/common.css" />
<link rel="stylesheet" href="../../Styles/manager-style.css" />
<link rel="stylesheet" href="../../Styles/item.css" />
<link href="css/lightbox.css" rel="stylesheet" />
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
<script src="../../Scripts/jquery.droppy.js" type="text/javascript"></script>  
<style type="text/css">
.mycheckBig input {width:25px; height:25px;}
.mycheckSmall input {width:10px; height:10px;} 
table {
    width: 100%;
    border-collapse: collapse;
}
.table th {
	height: 0px;
}
table td {
	padding: 2px 5px;
	height:	0px;
}
.lblnewstyle dl, dt{		
    font-weight: bold;		
    display: block;		
    text-align: left;		
    padding: 1px 4px;		
    border-left: 3px solid #3D3A35;		
    border-bottom: 1px solid #73c5ff;		
    background: #73C5FF;		
    box-sizing: border-box;		
    font-size: 10px;		
}
.rdostyleclass input[type=radio]:checked + label
    {
        -webkit-border-before-style: none;
        border-right: 0px solid #ddd;
        border-left: 0px solid #ddd;
    }
</style>
<script type="text/javascript" language="javascript">
    function OnCheckedChanged(p_Obj) {
        if (p_Obj == true) {
            document.getElementById('<%=txtInactive.ClientID%>').style.display = 'block';
            document.getElementById("chkact").style.display = 'block';
        }
        else if (p_Obj == false) {
            document.getElementById('<%=txtInactive.ClientID%>').value = "";
            document.getElementById('<%=txtInactive.ClientID%>').style.display = 'none';
            document.getElementById("chkact").style.display = 'none';
        }
        else if (p_Obj.checked) {
            output = confirm("Are you sure want to change inactive?");
            if (output) {
                document.getElementById('<%=txtInactive.ClientID%>').style.display = 'block';
                document.getElementById("chkact").style.display = 'block';
            }
            else
                document.getElementById('<%=chkActive.ClientID%>').checked = false;
        }
        else if (!p_Obj.checked) {
            document.getElementById('<%=txtInactive.ClientID%>').value = "";
            document.getElementById('<%=txtInactive.ClientID%>').style.display = 'none';
            document.getElementById("chkact").style.display = 'none';
        }
    } //OnCheckedChanged
</script>
<script type="text/javascript">
    function windowOnLoad() {
        var serverSideIsPostBack = '<%= IsPostBack %>';
        var isPostBack = (serverSideIsPostBack == 'True') ? true : false;
        if (isPostBack == false) {
            // This is the initial load of the page...
            if ($('#<%= chkActive.ClientID %>').is(':checked')) {
                document.getElementById('<%=txtInactive.ClientID%>').style.display = 'block';
                document.getElementById("chkact").style.display = 'block';
            }
            else {
                document.getElementById('<%=txtInactive.ClientID%>').style.display = 'none';
                document.getElementById("chkact").style.display = 'none';
            }
        }
    }
    window.onload = windowOnLoad;
</script>
<script type="text/javascript" language="javascript">
function pageLoad(sender, args) {
$(document).ready(function () {
$("#<%=txtRelease_Date.ClientID %>").datepicker(
{ 
showOn: 'button',
dateFormat: 'yy/mm/dd ',
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
 
 $(document).ready(function () {
$("#<%=txtPost_Available_Date.ClientID %>").datepicker(
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
}
</script>
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
        document.getElementById("<%=hdfPostDate.ClientID%>").value = "";
        document.getElementById("<%=hdfReleaseDate.ClientID%>").value = "";
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
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        var retval = window.open('../Item/PopupCatagoryList.aspx?Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
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
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        var retval = window.open('../Item/Item_Option_Select1.aspx?Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
</script>
<script type="text/javascript">
    function ShowDialog(ctrl) {
        var Item_Code = document.getElementById("<%=  txtItem_Code.ClientID %>");
        if (Item_Code.value == "") {
            alert("Please fill Item_Code!");
            txtItem_Code.focus();
            return false;
        } else {
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
            var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
            var retval = window.open('../Item/FileUpload_Dialog.aspx?Image_Type=0&Item_Code=' + itemcode, window, params);
            var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
            hidSourceID.value = ctrl.id;
            var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
            document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
            var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
            document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
            if (window.focus) {
                newwin.focus()
            }
            return true;
        }
    }
</script>
<script type="text/javascript">
    function ShowMallCategory(mallID, ctrl) {
        var width = 800;
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
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        var retval = window.open('../Item/Mall_Category_Choice.aspx?Mall_ID=' + mallID + '&Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
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
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        var hidderValue = document.getElementById("<%= txtYahoo_CategoryID.ClientID %>").value;
        var retval = window.open('../Item/Item_YahooSpecificValue.aspx?YahooMallCategoryID=' + hidderValue + '&Item_Code=' + itemcode, window, params);
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
</script>
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
<script type="text/javascript">
    function AddSKU(ctrl) {
        var width = 1075;
        var height = 550;
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
        var itemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
        if (itemcode.indexOf("itemcode") >= 0) {
            var txtitemcode = document.getElementById("<%=txtItem_Code.ClientID %>").value;
            var retval = window.open('../Item/AddSKU.aspx?Item_Code=' + txtitemcode, window, params);
        }
        else {
            var retval = window.open('../Item/AddSKU.aspx?Item_Code=' + itemcode, window, params);
        }
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var postdate = document.getElementById("<%=txtPost_Available_Date.ClientID%>").value;
        document.getElementById('<%=hdfPostDate.ClientID %>').value = postdate;
        var releasedate = document.getElementById("<%=txtRelease_Date.ClientID%>").value;
        document.getElementById('<%=hdfReleaseDate.ClientID %>').value = releasedate;
        if (window.focus) {
            newwin.focus()
        }
        return false;
    }
</script>
<script type="text/javascript">
    function clrCtrl() {
        document.getElementById('<%=txtRelease_Date.ClientID %>').value = "";
    }
    function clrCtrl1() {
        document.getElementById('<%=txtPost_Available_Date.ClientID %>').value = "";
    }
</script>
<script type="text/javascript">
    function UpdateInfo(ctrl) {
        var itemcode = document.getElementById('<%=txtItem_Code.ClientID %>').value;
        var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        if (itemcode != "") {
            return true;
        }
        else {
            return false;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<asp:HiddenField ID="CustomHiddenField" runat="server" />
<asp:HiddenField ID="hdfPostDate" runat="server" />
<asp:HiddenField ID="hdfReleaseDate" runat="server"/>
<asp:HiddenField  ID="hdfCtrl_ID" runat="server"/>
<asp:HiddenField  ID="hdfCatID" runat="server"/>
<div id="CmnWrapper">
<div id="CmnContents">
<div id="ComBlock">
<div class="setListBox iconSet iconEdit">
<h1>商品情報編集</h1>
  <main>  
 <input id="tab1" type="radio" name="tabs" checked style="visibility:hidden;border-top: 1px solid #ddd;padding: 0px 0px;position:fixed;">
<label for="tab1" style="padding: 0px 0px;position:fixed;">RCM_Item</label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<input id="tab2" type="radio" name="tabs" style="visibility:hidden;border-top: 1px solid #ddd;padding: 0px 14px;position:fixed;">
<label for="tab2" style="padding: 0px 14px;position:fixed;">ものたろう</label>
<section id="content1">
<div class="itemCmnSet editPage">
<div id="block1" class="cmnEdit inlineSet">
		<dl class="itemNo">
			<dt>商品番号</dt>
            <asp:UpdatePanel runat="server" id="UpdatePanel8" updatemode="Conditional" ><ContentTemplate>
			<dd><asp:TextBox ID="txtItem_Code" runat="server" Width="238px" MaxLength="32" onchange="if(!UpdateInfo(this)) return false; setTimeout('__doPostBack(\'tb1\',\'\')', 0)"></asp:TextBox></dd>
            </ContentTemplate><Triggers>
                <asp:Asyncpostbacktrigger controlid="txtItem_Code"/>
           </Triggers>
      </asp:UpdatePanel>
		</dl>
		<dl class="itemName">
			<dt>商品名</dt>
			<dd><asp:TextBox ID="txtItem_Name" runat="server" TextMode="MultiLine" MaxLength="255"></asp:TextBox></dd>
		</dl>
		<dl>
			<dt>メーカー名へ</dt>
				<dd><asp:TextBox ID="txtmakername" runat="server" Width="290px" MaxLength="200"></asp:TextBox></dd>
		</dl>
    <div class="lblnewstyle" style="width:307px;height:28px;">
        <dt>定価（税抜）</dt>
            <dd style="margin-left:6px;"><asp:TextBox runat="server" ID="txtList_Price" Width="69px" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>		
        <dt style="margin-left: 3px;">原価（税抜）</dt>		
            <dd style="margin-left:6px;"><asp:TextBox runat="server" ID="txtcost" Width="69px" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>		
             </div>
        <div class="lblnewstyle" style="width:350px;height:28px;">		
        <dt>販売価格 （税抜）</dt>		
            <dd style="margin-left:6px;"><asp:TextBox runat="server" ID="txtSale_Price" Width="69px" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>		
        <dt style="margin-left: 3px;">原価率（税抜）</dt>		
             <dd style="margin-left:6px;"><asp:Label runat="server" ID="lblcostrate"/></dd>		
        	
        </div>
     <div class="lblstyle" style="width:307px;height:28px;">
         <dt>Jisha_Price </dt>
            <dd style="margin-left:6px;"><asp:TextBox runat="server" ID="txtJisha_Price" Width="69px" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
    </div>
        <div class="lblnewstyle" style="width:307px;height:28px;">
        <dt>利益率</dt>		
            <dd style="margin-left:6px;"><asp:Label runat="server" ID="lblprofitrate"/></dd>		
        <dt>割引率</dt>		
            <dd style="margin-left:6px;"><asp:Label runat="server" ID="lbldiscountrate"/></dd>	
        </div>
		<div id="imgEntryTechnology">
			<dl>
				<dt>商品画像  <input type="button" id ="lnkAddPhoto" onclick ="ShowDialog(this)" value="画像登録" runat="server" visible="true"/></dt>
               <dd>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage1"><asp:Image runat="server" ID="Image1" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage2"><asp:Image runat="server" ID="Image2" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage3"><asp:Image runat="server" ID="Image3" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage4"><asp:Image runat="server" ID="Image4" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink></p>
					<p><asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage5"><asp:Image runat="server" ID="Image5" ImageUrl="~/Item_Image/no_image.jpg"/></asp:HyperLink></p>
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
    <dl>		
           <%-- <dt>YahooエビデンスURL</dt>--%>		
            <dd><asp:TextBox runat="server" ID="txtyahoourl" Width="525px" Visible="false"></asp:TextBox></dd>		
            <%--<dt>闇市パスワード</dt>--%>		
            <dd><asp:TextBox runat="server" ID="txtBlackMarket_Password" onkeypress="return isNumberKeys(event)" Width="210px" MaxLength="50" Visible="false"/></dd>		
			<%--<dt>二重価格文書管理番号</dt>--%>		
            <dd><asp:TextBox runat="server" ID="txtDoublePrice_Ctrl_No" onkeypress="return isNumberKeys(event)" Width="70px" MaxLength="50" Visible="false"/></dd>		
           <%--   <dt>Inventory Flag</dt>--%>		
              <asp:UpdatePanel runat="server" id="UpdatePanel6" updatemode="Conditional"><ContentTemplate>		
              <dd><asp:CheckBox runat="server" ID="CheckBox1" oncheckedchanged="chkInventory_OnCheckChanged" AutoPostBack="true" Visible="false" /></dd>		
               </ContentTemplate> <Triggers>		
                <asp:Asyncpostbacktrigger controlid="chkInventory"/>		
           </Triggers>		
           </asp:UpdatePanel>		
        </dl>
</div><!--/#block1-->
<div id="scrollArea" class="cmnEdit inlineSet">
	<div id="hideBlock" class="skuBlock">
		<h2>SKUデータ </h2>
		<div id="hideBox" class="skubox">
		<div class="sku1">
         <asp:GridView runat="server" ID="gvSKU"></asp:GridView>
		</div>
        <div><input type="button" id = "btnAdd" onclick ="AddSKU(this)" value="AddSKU" runat="server"/>
              <dl style="padding-left:77px;margin-top:-1em;">
			<dt  style="padding-left:18px;margin-top:-1em;">SKU</dt>
            <dd  style="padding-left:53px;margin-top:-1.5em;">
                <asp:RadioButton runat="server" ID="rdb1" Text="あり" GroupName="groupsku" Enabled="false" CssClass="rdostyleclass"/>
                <asp:RadioButton runat="server" ID="rdb2" Text="なし" GroupName="groupsku" Enabled="false" CssClass="rdostyleclass"/></dd>
        </dl>
        </div>
		<h3>SKU サイズ&amp;カラー</h3>
		<div id="hideBox2" class="skubox">
			<div class="sku2">
             <asp:GridView runat="server" ID="gvSKUSize" Width="100%" Visible="false"></asp:GridView>
            </div>
		    <div class="sku2 sku3">
             <asp:GridView runat="server" ID="gvSKUColor" Width="100%" Visible="false"></asp:GridView> 
            </div>
            </div><!-- /#hideBox2 -->
	</div><!-- /#hideBox -->
	</div ><!-- /#hideBlock -->
<!-- /SKU -->
<div id="block2">
	<div class="dBlock itemInfo">
		<dl>
			<dt>年度</dt>
				<dd><asp:TextBox ID="txtYear" runat="server" Width="115px" MaxLength="20"></asp:TextBox></dd>
			<dt>シーズン</dt>
				<dd><asp:TextBox ID="txtSeason" runat="server" Width="70px" MaxLength="40"></asp:TextBox></dd>
			<dt>カタログ情報</dt>
				<dd><asp:TextBox ID="txtCatalog_Information" runat="server" Width="278px" MaxLength="3000"></asp:TextBox></dd>
		</dl>
	</div>
	<div class="dBlock">
		<dl class="relatedProduct">
			<dt>関連商品</dt>
			<dd><asp:TextBox runat="server" ID="txtRelated1" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtRelated2" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtRelated3" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtRelated4" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtRelated5" onkeypress="return isNumberKeys(event)"/>
			<table><tr><td style="width: 74px;text-align: center;">箱</td>
                         <td style="width: 144px;text-align: center;">420</td>
                         <td style="width: 118px;text-align: center;">ml</td>
                         <td style="width: 154px;text-align: center;">12</td>
                         <td></td>
                     </tr>
            </table>
            <dt>販売単位</dt><dd><asp:DropDownList runat="server" ID="ddlsalesunit" Width="70px"></asp:DropDownList></dd>
			<dt>内容量	</dt><dd><asp:TextBox runat="server" ID="txtcontentquantityunitno1" Width="50px" /></dd>
            <dt>内容量単位	</dt><dd><asp:DropDownList runat="server" ID="ddlcontentunit1" Width="70px"></asp:DropDownList></dd>
            <dt>まとめ販売数	</dt><dd><asp:TextBox runat="server" ID="txtcontentquantityunitno2" Width="50px" /></dd>
            <dt>まとめ販売単位	</dt><dd><asp:DropDownList runat="server" ID="ddlcontentunit2" Width="50px"></asp:DropDownList></dd>
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
			</dd>
			<dd class="piOpen2">
                <p><asp:TextBox runat="server" ID="txtTemplate3" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content3" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate4" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content4" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate5" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content5" TextMode="MultiLine"/></p>
				<p><asp:TextBox runat="server" ID="txtTemplate6" onkeypress="return isNumberKeys(event)"/><asp:TextBox runat="server" ID="txtTemplate_Content6" TextMode="MultiLine"/></p>
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
			<dt>PC用販売説明文</dt>
			<dd><asp:TextBox runat="server" ID="txtSale_Description_PC" TextMode="MultiLine"/></dd>
		</dl>
		<dl>
            <dt>PC用商品説明文</dt>
			<dd><asp:TextBox runat="server" ID="txtItem_Description_PC"  TextMode="MultiLine"/></dd>
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
        <dl>
            <dt>PC用キャッチコピー</dt>
            <dd><asp:TextBox runat="server" ID="txtCatchCopy" TextMode="MultiLine"></asp:TextBox></dd>
        </dl>
        <dl>
            <dt>モバイル用キャッチコピー</dt>
            <dd><asp:TextBox runat="server" ID="txtCatchCopyMobile" TextMode="MultiLine"></asp:TextBox></dd>
        </dl>
         <dl>
           <%--<dt>競技名</dt>--%>
				<dd><asp:TextBox ID="txtCompetition_Name" runat="server" Width="290px" MaxLength="200" Visible="false"></asp:TextBox></dd>
			<%--<dt>分類名</dt>--%>
				<dd><asp:TextBox ID="txtClass_Name" runat="server" Width="290px" MaxLength="200" Visible="false"></asp:TextBox></dd>
	    </dl>
	</div>
	<div class="dBlock inlineSet">
		<dl class="itemOption">
			<dt>オプション<br/><input type="button" id = "btnAddOption" onclick ="ShowOption(this)" value="選 ぶ" runat="server" visible="false"/></dt>
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
			<dt>ショップカテゴリ1 <input type="button" id = "btnAddCatagories" onclick ="ShowCatagoryList(this)" value="選 ぶ" runat="server" visible="false"/></dt>
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
<asp:Label runat="server" ID="lblcat2" Text="ショップカテゴリ 選 ぶ2" Visible="false"/> &nbsp;&nbsp;<asp:Button ID="btnCategoryAdd" runat="server" Text="選 ぶ" OnClick="btnCategoryAdd_OnClick" Width="57px" Visible="false" />
    <asp:GridView ID="gvCategory" runat="server" ShowHeader="true" AutoGenerateColumns="false" GridLines="None" Width="600px" CssClass="table" Visible="false">
            <Columns>
            <asp:TemplateField>
            <HeaderStyle/>
           <HeaderTemplate>
            <asp:Button ID="btnRemove" runat="server" Text="Remove" OnClick="btnRemove_OnClick" Visible="false"/>
            <asp:Label ID="lblalert" runat="server" Text="Please type like as  this format(eg:塗料（1）\リボス【LIVOS】\) " ForeColor="Red"></asp:Label>
           </HeaderTemplate>
           <ItemTemplate>
           <asp:TextBox ID="txtCategory" runat="server" Width="492px"></asp:TextBox>
           </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblSN" runat="server" Text="表示順"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
             <asp:TextBox ID="txtSN" runat="server" Width="50px" Text="0" onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            </Columns>
            </asp:GridView>
    <div class="dBlock itemSpace">
		<dl>
			<dt>Jisha Category</dt>
			<dd><asp:GridView ID="gvjishacategory" runat="server" ShowHeader="false" AutoGenerateColumns="false" GridLines="None" Width="600px" CssClass="table">
            <Columns>
            <asp:TemplateField>
            <ItemTemplate>
               <asp:Label ID="lblCatno" runat="server" Text="カテゴリコード" Width="100px"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
            <ItemTemplate>
             <asp:TextBox ID="txtCatno" runat="server" Width="100px" Text=""></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField>
            <ItemTemplate>
                <asp:Label ID="lblCatName" runat="server" Text="カテゴリ名" Width="80px"></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <ItemTemplate>
                <asp:TextBox ID="txtCategoryName" runat="server" Width="305px"></asp:TextBox>
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
			<p>楽天<asp:TextBox runat="server" ID="txtRakuten_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)" Width="100px" />
            <asp:TextBox runat="server" ID="txtRakuten_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)" Width="350px"/>
            <input type="button" id = "btnRakuten_CategoryID" onclick ="ShowMallCategory(1,this)" value="選 ぶ" runat="server" visible="false"/></p>
			<p>ヤフー<asp:TextBox runat="server" ID="txtYahoo_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)" Width="100px"/>
             <asp:TextBox runat="server" ID="txtYahoo_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)" Width="350px"/>
            <input type="button" id = "btnYahoo_CategoryID" onclick ="ShowMallCategory(2,this)" value="選 ぶ" runat="server" visible="false"/></p>
			<p style="visibility:hidden;">ポンパレ<asp:TextBox runat="server" ID="txtPonpare_CategoryID" ReadOnly="true" onkeypress="return isNumberKeys(event)" Width="100px"/>
            <asp:TextBox runat="server" ID="txtPonpare_CategoryPath" ReadOnly="true" onkeypress="return isNumberKeys(event)" Width="350px"/>
            <input type="button" id = "btnPonpare_CategoryID" onclick ="ShowMallCategory(3,this)" value="選 ぶ" runat="server" visible="false"/></p>
			<p>ヤフースペック値<asp:TextBox runat="server" ID="txtYahooValue1" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <asp:TextBox runat="server" ID="txtYahooValue2" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <asp:TextBox runat="server" ID="txtYahooValue3" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <asp:TextBox runat="server" ID="txtYahooValue4" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <asp:TextBox runat="server" ID="txtYahooValue5" ReadOnly="true" onkeypress="return isNumberKeys(event)"/>
            <input type="button" id = "imgbYahooSpecValue" onclick ="ShowYahooSpecValue(this)" value="選 ぶ" runat="server" visible="false"/></p>
			</dd>
		</dl>
 	</div>
	<div class="dBlock Exhibit">
	        <dl>
			<dt style ="height :85px">出品対象ショップ</dt>
            <asp:UpdatePanel runat="server" id="UpdatePanel" updatemode="Conditional" ><ContentTemplate>
			<dd runat="server" id="dd1">
				<b><asp:Label runat="server" ID="lblShopName" Text=""></asp:Label></b>
                <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                <asp:DataList ID="dlShop1" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <ItemTemplate>
            <p><asp:CheckBox runat="server" ID="ckbMall1Shop" EnableViewState="true" Text='<%# Bind("Mall_Name")%>' style="padding: 0px 0px;"/><asp:Label runat="server" ID="lblMall1ShopID" Text='<%# Bind("ID")%>' Visible="false" /></p>
                </ItemTemplate>
                </asp:DataList>
			</dd>
             </ContentTemplate><Triggers>
                <asp:Asyncpostbacktrigger controlid="dlShop1"/>
           </Triggers>
      </asp:UpdatePanel>
      <asp:UpdatePanel runat="server" id="UpdatePanel1" updatemode="Conditional"><ContentTemplate>
			<dd runat="server" id="dd2">
				<b><asp:Label runat="server" ID="lblShopName1" Text=""></asp:Label></b>
                  <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                <asp:DataList ID="dlShop2" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <ItemTemplate>
                <p><asp:CheckBox runat="server" ID="ckbMall2Shop" EnableViewState="true" Text='<%# Bind("Mall_Name")%>'/> <asp:Label runat="server" ID="lblMall2ShopID" Text='<%# Bind("ID")%>' Visible="false" /></p>
                </ItemTemplate>
                </asp:DataList>
			</dd>
            </ContentTemplate> <Triggers>
                <asp:Asyncpostbacktrigger controlid="dlShop2"/>
           </Triggers>
           </asp:UpdatePanel>
           <asp:UpdatePanel runat="server" id="UpdatePanel2" updatemode="Conditional"><ContentTemplate>
			<dd runat="server" id="dd3">
				<b><asp:Label runat="server" ID="lblShopName2" Text=""></asp:Label></b>
                  <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                 <asp:DataList ID="dlShop3" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <ItemTemplate>
                <p><asp:CheckBox runat="server" ID="ckbMall3Shop" EnableViewState="true" Enabled="false" Text='<%# Bind("Mall_Name")%>'/> <asp:Label runat="server" ID="lblMall3ShopID" Text='<%# Bind("ID")%>' Visible="false" /> </p>
                </ItemTemplate>
                </asp:DataList>
				</dd>
                </ContentTemplate> <Triggers>
                <asp:Asyncpostbacktrigger controlid="dlShop3"/>
           </Triggers>
           </asp:UpdatePanel>
           <asp:UpdatePanel runat="server" id="UpdatePanel3" updatemode="Conditional"><ContentTemplate>
			<dd runat="server" id="dd4">
				<b><asp:Label runat="server" ID="lblShopName3" Text=""></asp:Label></b>
                  <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                <asp:DataList ID="dlShop4" runat="server" RepeatDirection="Horizontal" 
                    RepeatLayout="Flow" CaptionAlign="Bottom">
                <ItemTemplate>
                <p><asp:CheckBox runat="server" ID="ckbMall4Shop" EnableViewState="true" Text='<%# Bind("Mall_Name")%>'/> <asp:Label runat="server" ID="lblMall4ShopID" Text='<%# Bind("ID")%>' Visible="false" /> </p>
                </ItemTemplate>
                </asp:DataList>
			</dd>
            </ContentTemplate> <Triggers>
                <asp:Asyncpostbacktrigger controlid="dlShop4"/>
           </Triggers>
           </asp:UpdatePanel>
           <asp:UpdatePanel runat="server" id="UpdatePanel4" updatemode="Conditional"><ContentTemplate>
			<dd runat="server" id="dd5">
				<b><asp:Label runat="server" ID="lblShopName4" Text=""></asp:Label></b>
                  <!-------------------We used  出品対象ショップ  binding method  for    DataList of Asp.net--------------------------------->
                <asp:DataList ID="dlShop5" runat="server"  RepeatDirection="Horizontal" RepeatColumns="5" RepeatLayout="Flow">
                <ItemTemplate >
                <p><asp:CheckBox runat="server" ID="ckbMall5Shop" EnableViewState="true" Text='<%# Bind("Mall_Name")%>'/> <asp:Label runat="server" ID="lblMall5ShopID" Text='<%# Bind("ID")%>' Visible="false" /> </p>
                </ItemTemplate>
                </asp:DataList>
			</dd>
            </ContentTemplate> <Triggers>
                <asp:Asyncpostbacktrigger controlid="dlShop5"/>
           </Triggers>
           </asp:UpdatePanel>
		</dl>
        </div>
    <div>
        <dl>
        <dt  style="height:56px; margin-bottom:10px;">Item_Code_URL</dt>
        <asp:UpdatePanel runat="server" id="UpdatePanel9" updatemode="Conditional" ><ContentTemplate>
        <dd style="width:295px;"> <asp:DataList ID="dlShop" runat="server" RepeatDirection="Vertical" RepeatColumns="3" >
                <ItemTemplate >
               <asp:CheckBox runat="server" ID="ckbShop" EnableViewState="true" Visible="false" Checked="true"/>
                <asp:Label runat="server" ID="lblShopID" Text='<%# Bind("ID")%>' Visible="false"/>
                <asp:Label runat="server" ID="lblShopName" Text='<%# Bind("Shop_Name")%>'/>
                <asp:TextBox ID="txtItem_CodeList" runat="server" Text='<%# Bind("Item_Code")%>' Width="175px"/>
                </ItemTemplate>
                </asp:DataList></dd>
                </ContentTemplate><Triggers>
                <asp:Asyncpostbacktrigger controlid="dlShop"/>
           </Triggers>
      </asp:UpdatePanel>
        </dl>
	</div>
	<div class="dBlock itemPriceFlag dlFloat">
		<dl>
			
			<dt>発売日</dt>
            <dd><asp:TextBox runat="server" ID="txtRelease_Date" Width="85px" ReadOnly="true" onclick="hdate()"></asp:TextBox>
                <asp:Image ID="ImageButton1" runat="server" Width="15px" Height="15px" ImageUrl="~/Styles/clear.png"  ImageAlign="AbsBottom"  Onclick="clrCtrl()"/> </dd>
			<dt>掲載可能日</dt><dd><asp:TextBox runat="server" ID="txtPost_Available_Date" Width="85px" ReadOnly="true"></asp:TextBox>
              <asp:Image ID="ImageButton2" runat="server" Width="15px" Height="15px" ImageUrl="~/Styles/clear.png" ImageAlign="AbsBottom" Onclick="clrCtrl1()" /> 
            </dd>
            <dt>製品コード</dt><dd><asp:TextBox runat="server" ID="txtProduct_Code" onkeypress="return isNumberKeys(event)" MaxLength="100" Width="204px"/></dd>
		</dl>
		<dl>
			<dt>ブランドコード</dt><dd><asp:TextBox runat="server" ID="txtBrand_Code" Width="77px" MaxLength="4" ></asp:TextBox></dd>
			<dt>送料フラグ</dt>
			<dd>
             <asp:DropDownList runat="server" ID="ddlShipping_Flag" Width="83px" >
             <asp:ListItem Value="0">送料別</asp:ListItem>
             <asp:ListItem Value="1">送料込</asp:ListItem>
             </asp:DropDownList>
			</dd>
            <dt>代引料フラグ</dt>
			<dd>
            <asp:DropDownList runat="server" ID="ddlDelivery_Charges" Width="83px">
            <asp:ListItem Value="0">代引料別</asp:ListItem>
            <asp:ListItem Value="1">代引料込</asp:ListItem>
            </asp:DropDownList>
			</dd>
			<dt>倉庫指定</dt>
			<dd>
            <asp:DropDownList runat="server" ID="ddlWarehouse_Specified" Width="83px">
            <asp:ListItem Value="0">販売中</asp:ListItem>
            <asp:ListItem Value="1">倉庫</asp:ListItem>
            </asp:DropDownList>
			</dd>
		</dl>
		<dl>
			<dt>個別送料</dt><dd><asp:TextBox runat="server" ID="txtExtra_Shipping" onkeypress="return isNumberKey(event)" MaxLength="8" Width="267px" /></dd>
			 <dt>即日出荷</dt>
               <asp:UpdatePanel runat="server" id="UpdatePanel5" updatemode="Conditional"><ContentTemplate>
            <dd>
            <asp:CheckBox runat="server" ID="delivery_flag" oncheckedchanged="delivery_flag_CheckedChanged" AutoPostBack="true" />
            対応する</dd>
                 </ContentTemplate> <Triggers>
                <asp:Asyncpostbacktrigger controlid="delivery_flag"/>
           </Triggers>
           </asp:UpdatePanel>
	  	
             <dt>掲載不要</dt>
            <dd><input type="checkbox" runat="server" id="chkActive" onclick="OnCheckedChanged(this)"/>
            </dd>
		</dl>
        <dl>
             <dt>Maker_Code</dt><dd><asp:TextBox runat="server" ID="txtmaker_code" MaxLength="8" Width="250px" /></dd>
        </dl>
<dl>
           <%-- <dt>SKU</dt>
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional"> <ContentTemplate>
            <dd><asp:RadioButton runat="server" ID="rdb1" Text="あり" GroupName="groupsku" Enabled="false" CssClass="rdostyleclass"/>
                    <asp:RadioButton runat="server" ID="rdb2" Text="なし" GroupName="groupsku" Enabled="false" CssClass="rdostyleclass"/></dd>
             </ContentTemplate>
		     </asp:UpdatePanel>--%>
              <asp:UpdatePanel runat="server" id="UpdatePanel20" updatemode="Conditional"><ContentTemplate>
              <dd><asp:CheckBox runat="server" ID="chkInventory" oncheckedchanged="chkInventory_OnCheckChanged" AutoPostBack="true" Visible="false" /></dd>
               </ContentTemplate> <Triggers>
                <asp:Asyncpostbacktrigger controlid="chkInventory"/>
           </Triggers>
           </asp:UpdatePanel>
        </dl>
	</div>
	<div class="dBlock itemSpace" style="visibility:hidden">
		<dl>
            <dt  id="chkact"> 掲載不要 の理由</dt>
			<dd><asp:TextBox runat="server" ID="txtInactive"  TextMode="MultiLine"/></dd>
			<dt>フリースペース２</dt>
			<dd><asp:TextBox runat="server" ID="txtAdditional_2"  TextMode="MultiLine"/></dd>
			<dt>フリースペース３</dt>
			<dd><asp:TextBox runat="server" ID="txtAdditional_3"  TextMode="MultiLine"/></dd>
			<dt style="display:none">ゼット用項目（PC商品説明文）</dt>
			<dd style="display:none"><asp:TextBox runat="server" ID="txtZett_Item_Description" TextMode="MultiLine"/></dd>
			<dt style="display:none">ゼット用項目（PC販売説明文）</dt>
			<dd style="display:none"><asp:TextBox runat="server" ID="txtZett_Sale_Description" TextMode="MultiLine"/></dd>
		</dl>
	</div>
</div><!-- /#block2 -->
</div><!-- /#scrollArea -->
</div><!--/.itemCmnSet-->
</section>
<section id="content2">
	<div class="itemCmnSet editPage">
        <div id="block1" class="cmnEdit inlineSet">
		<dl>
            <dt>(ブランド名)</dt>
            <dd><asp:TextBox ID="txtBrand_Name" runat="server" MaxLength="40" Width="290px"></asp:TextBox></dd>
            <dt>商品詳細登録コメント</dt>
            <dd><asp:TextBox ID="txtcomment" runat="server" Width="290px" MaxLength="40" TextMode="MultiLine"></asp:TextBox></dd>
            <dt>(市場売価)</dt>
            <dd><asp:TextBox ID="txtsellingprice" runat="server" Width="290px" MaxLength="40" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
            <dt>仕入価格</dt>
            <dd><asp:TextBox ID="txtpurchaseprice" runat="server" Width="290px" MaxLength="40" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
            <dt>賞味期限</dt>
			<dd><asp:TextBox ID="txtsellby" runat="server" Width="290px" MaxLength="40" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
			<dt>年間出荷数もしくは売れ筋A～Dランク</dt>
			<dd><asp:TextBox ID="txtsellingrank" runat="server" Width="50px" MaxLength="20"></asp:TextBox></dd>
        </dl>
        </div>
        <div id="scrollArea" class="cmnEdit inlineSet">
        <div id="block2">
	    <div class="dBlock itemInfo">
        <dl class="relatedProduct">
            <dt style="width:120px;">引渡方法</dt>
				<dd><asp:DropDownList runat="server" ID="ddldeliverymethod">
                        </asp:DropDownList></dd>
            <dt style="width:120px;">配送種別</dt>
				<dd style="width:80px;"><asp:DropDownList runat="server" ID="ddldeliverytype" Width="80px">
                        </asp:DropDownList></dd>
			<dt style="width:120px;">入荷日数</dt>
				<dd style="width:100px;"><asp:TextBox ID="txtdeliverydays" runat="server" MaxLength="40" Width="85px" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
		</dl>
        <dl class="relatedProduct">
            <dt style="width:120px;">代引可否</dt>
				<dd><asp:DropDownList runat="server" ID="ddldeliveryfees">
                        </asp:DropDownList></dd>
            <dt style="width:120px;">笠間納品可否</dt>
				<dd style="width:80px;"><asp:DropDownList runat="server" ID="ddlksmavaliable" Width="80px">
                        </asp:DropDownList></dd>
            <dt style="width:120px;">笠間納品入荷日数</dt>
				<dd style="width:100px;"><asp:TextBox ID="txtksmdeliverydays" runat="server"  MaxLength="5" Width="85px" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
       </dl>
        <dl class="relatedProduct">
             <dt style="width:120px;">返品可否</dt>
				<dd><asp:DropDownList runat="server" ID="ddlreturnableitem" Width="80px" >
                        </asp:DropDownList></dd>
             <dt style="width:120px;">該当法令</dt>
		     <dd style="width:100px;"><asp:DropDownList runat="server" ID="ddlnoapplicablelaw" Width="295px" >
                </asp:DropDownList></dd>
      </dl>
        <dl class="relatedProduct">
            <dt style="width:120px;">販売許可・認可・届出 </dt>
			 <dd><asp:DropDownList runat="server" ID="ddlsalespermission" Width="205px" >
                        </asp:DropDownList></dd>
            <dt style="width:120px;">法令・規格</dt>
			<dd style="width:150px;"><asp:DropDownList runat="server" ID="ddllaw" Width="170px" >
                        </asp:DropDownList></dd>
            </dl>
        <dl class="relatedProduct">
            <dt style="width:120px;">(全国)</dt>
            <dd><asp:TextBox ID="txtnationwide" runat="server" Width="205px" MaxLength="40" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
            <dt style="width:120px;">(北海道)</dt>
            <dd style="width:150px;"><asp:TextBox ID="txthokkaido" runat="server" Width="170px" MaxLength="40" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
        </dl>
        <dl class="relatedProduct">
            <dt style="width:120px;">(沖縄)</dt>
            <dd><asp:TextBox ID="txtokinawa" runat="server" Width="205px" MaxLength="40" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
            <dt style="width:120px;">(離島)</dt>
            <dd style="width:150px;"><asp:TextBox ID="txtremoteisland" runat="server" Width="170px" MaxLength="40" onkeypress="return isNumberKey(event)"></asp:TextBox></dd>
        </dl>
        <dl class="relatedProduct">
            <dt style="width:120px;">(直送時配送不可地域)</dt>
            <dd><asp:TextBox ID="txtundeliveredarea" runat="server" Width="205px" MaxLength="40"></asp:TextBox></dd>
            <dt style="width:120px;">危険物の含有量</dt>
            <dd style="width:100px;"><asp:TextBox ID="txtdangerousgoodscontents" runat="server" Width="170px" MaxLength="40"></asp:TextBox></dd>
       </dl>
        <dl class="relatedProduct">
            <dt style="width:120px;">危険物の種別</dt>
            <dd><asp:DropDownList runat="server" ID="ddldanggoodsclass" Width="205px" >
                 </asp:DropDownList></dd>
            <dt style="width:120px;">危険物の品名</dt>
            <dd style="width:120px;"><asp:DropDownList runat="server" ID="ddldanggoodsname" Width="170px" >
                 </asp:DropDownList></dd>
       </dl>
        <dl class="relatedProduct">
            <dt style="width:120px;">危険等級</dt>
            <dd><asp:DropDownList runat="server" ID="ddlriskrating" Width="205px" >
                 </asp:DropDownList></dd>
            <dt style="width:120px;">危険物の性質</dt>
            <dd style="width:100px;"><asp:DropDownList runat="server" ID="ddldanggoodsnature" Width="170px">
                 </asp:DropDownList></dd>
       </dl>
        <dl class="relatedProduct">
            <dt style="width:120px;">お客様組立て</dt>
			<dd style="width:100px;"><asp:DropDownList runat="server" ID="ddlcustomerassembly" Width="90px" >
                        </asp:DropDownList></dd>
            <dt style="width:170px;">消防法上、届出を必要とする物質</dt>
			<dd style="width:100px;"><asp:DropDownList runat="server" ID="ddlfirelaw" Width="225px" >
                        </asp:DropDownList></dd>
       </dl>
       </div>
     </div>
    </div>
   </div>
</section>
    <div class="itemCmnSet editPage">
<div class="btn">
   <p>
    <asp:Button runat="server" ID="btnSave" Text="登 録" OnClientClick="SaveClick()" onclick="btnSave_Click"/>
    <asp:Button runat="server" ID="btnPreview" Text="プレビュー" onclick="btnPreview_Click" />
   </p>
</div> 
</div>
</main>
</div>
</div><!--/.setListBox-->
</div>
</div><!--CmnWrapper-->
<script src="js/lightbox.min.js" type="text/javascript"></script>
<script type = "text/javascript">
    function Confirm() {
        var confirm_value = document.createElement("INPUT");
        confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("Are you sure you want to change？")) {
            confirm_value.value = "はい";
        } else {
            confirm_value.value = "いいえ";
        }
        var val = document.forms[0].appendChild(confirm_value);
        if (val.value.toString() == "はい") {
        }
    }
</script>
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
