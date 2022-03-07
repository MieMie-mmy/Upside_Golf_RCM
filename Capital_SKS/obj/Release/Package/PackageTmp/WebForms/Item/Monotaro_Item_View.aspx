<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Monotaro_Item_View.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Monotaro_Item_View" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8 || charCode == 46)
            return true;
        else return false;
    }
</script>
<script language="javascript" type="text/javascript">
    function doClick(buttonName, e) {
        //the purpose of this function is to allow the enter key to 
        //point to the correct button to click.
        var key;
        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 13) {
            //Get the button the user wants to have clicked
            var btn = document.getElementById(buttonName);
            if (btn != null) { //If we find the button click it
                btn.click();
                event.keyCode = 0
            }
        }
    }
</script>

<script type="text/javascript">
    window.document.onkeydown = function (e) {
        if (!e) e = event;
        if (e.keyCode == 27) {
            document.getElementById("<%=txtItem_Name.ClientID%>").value = null;
            document.getElementById("<%=txtItem_Code.ClientID%>").value = null;
            document.getElementById("<%=txtImage_Name.ClientID%>").value = null;
            document.getElementById("<%=txtCatalog_Infromation.ClientID%>").value = null;
            document.getElementById("<%=txtBrand_Name.ClientID%>").value = null;
            document.getElementById("<%=txtCategory_Name.ClientID%>").value = null;
            document.getElementById("<%=txtCompetition_Name.ClientID%>").value = null;
            document.getElementById("<%=txtColor_Name.ClientID%>").value = null;
            document.getElementById("<%=txtYear.ClientID%>").value = null;
            document.getElementById("<%=txtSeason.ClientID%>").value = null;
            document.getElementById("<%=txtKeyword.ClientID%>").value = null;

            var drp4 = document.getElementById("<%=ddlReservation_Flag.ClientID%>");
            var drp5 = document.getElementById("<%=ddlPerson.ClientID%>");
            var drp6 = document.getElementById("<%=chkCode.ClientID%>");
            drp1.selectedIndex = 0;
            drp2.selectedIndex = 0;
            drp4.selectedIndex = 0;
            drp5.selectedIndex = 0;
            drp6.checked = false;
        }
    }
</script>
<script type="text/javascript">
    function CallTab(e) {
        var val = document.getElementById("<%=hfNewTab.ClientID%>").innerHTML;
        if (val != "1")
            document.forms[0].target = "_blank";
        else
            document.forms[0].target = "";
    }
    function ddlpage_change() {
        document.forms[0].target = "";
    }
    function NotNewTab() {
        document.forms[0].target = "_blank";
    }
    function NewTab(ctrl, e) {
        document.forms[0].target = "_blank";
        document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
        __doPostBack('', '');
    }
</script>
<script type="text/javascript">
    function Show(strOpen, ctrl) {
        var left = (screen.width / 2) - (600 / 2);
        var top = (screen.height / 2) - (500 / 2);
        var retval = "";
        //show modal dialog box and collect its return value
        retval = window.open('../Item/ItemSKU_View.aspx?Item_Code=' + strOpen, '_blank', 'width=1000,height=500; left=150, right=50, top=50; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
        if (retval == undefined)
            retval = window.returnValue;
    }
</script>
<script type="text/javascript">
    function NewTabPreview(ctrl, e) {
        var index = ctrl.selectedIndex;
        if (index == 0)
            return;
        else {
            document.forms[0].target = "_blank";
            document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
            __doPostBack('', '');
        }
    }
</script>
<script type="text/javascript">
    function ShowPreview(itemcode, strOpen, ctrl) {
        var left = (screen.width / 2) - (600 / 2);
        var top = (screen.height / 2) - (500 / 2);
        var retval = "";
        //show modal dialog box and collect its return value
        retval = window.open('../Item/Item_Preview_Edit.aspx?ID=' + strOpen + '&Item_Code=' + itemcode, '_blank', 'width=1000,height=500; left=150, right=50, top=50; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
        if (retval == undefined)
            retval = window.returnValue;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
<div id="CmnContents">
<div id="ComBlock">
<div class="setListBox inlineSet iconSet iconList">
		<h1>ものたろう商品情報一覧</h1>
<!-- Exhibition log search -->
		<div class="itemInfo resetValue searchBox iconEx" >
			<h2>ものたろう商品情報一覧検索</h2>
	<asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
				<dl>
					<dt><asp:Label runat="server" ID="Label1" Text="商品名" ToolTip="Item Name" /></dt>
					<dd><asp:TextBox runat="server" ID="txtItem_Name" /></dd>
					<dt><asp:Label runat="server" ID="Label2" Text="商品番号" ToolTip="Item Code" /><br><asp:CheckBox runat="server" ID="chkCode" Text="完全" /></dt>
					<dd><asp:TextBox onkeydown="return checkKeycode(this)" TextMode ="MultiLine" runat="server" ID="txtItem_Code" /></dd>
					<dt><asp:Label runat="server" ID="Label3" Text="画像ファイル名" ToolTip="Image Name" /></dt>
					<dd><asp:TextBox runat="server" ID="txtImage_Name"  /></dd>
					<dt><asp:Label runat="server" ID="Label5" Text="カタログ情報" ToolTip="Catalog Information" /></dt>
					<dd><asp:TextBox runat="server" ID="txtCatalog_Infromation" /></dd>
					<dt><asp:Label runat="server" ID="Label7" Text="ブランド名" ToolTip="Brand Name" /></dt>
					<dd><asp:TextBox runat="server" ID="txtBrand_Name"  /></dd>
					<dt><asp:Label runat="server" ID="Label9" Text="カテゴリ名" ToolTip="Category Name" /></dt>
					<dd><asp:TextBox runat="server" ID="txtCategory_Name"  /></dd>
					<dt><asp:Label runat="server" ID="Label10" Text="競技名" ToolTip="Competitation Name" /></dt>
					<dd><asp:TextBox runat="server" ID="txtCompetition_Name"  /></dd>
					<dt><asp:Label runat="server" ID="Label11" Text="カラー名" ToolTip="Color Name" /></dt>
					<dd><asp:TextBox runat="server" ID="txtColor_Name"  /></dd>
					<dt><asp:Label runat="server" ID="Label12" Text="年度" ToolTip="Year" /></dt>
					<dd><asp:TextBox runat="server" ID="txtYear" /></dd>
					<dt><asp:Label runat="server" ID="Label13" Text="シーズン" ToolTip="Season" /></dt>
					<dd><asp:TextBox runat="server" ID="txtSeason" /></dd>
                    <dt><asp:Label runat="server" ID="lblExhibition" Text="M ステータス"/></dt>
					<dd><asp:DropDownList runat="server" ID="ddlExhibiton" >
                        <asp:ListItem Value="-1">All</asp:ListItem>
                            <asp:ListItem Value="3">出品済</asp:ListItem>
                        	<asp:ListItem Value="0">未出品</asp:ListItem>
                            </asp:DropDownList></dd>
                    <dt><asp:Label runat="server" ID="Label4" Text="Ready ステータス"/></dt>
					<dd><asp:DropDownList runat="server" ID="ddlReaddy" >
                        <asp:ListItem Value="-1">All</asp:ListItem>
                            <asp:ListItem Value="1">はい</asp:ListItem>
                        	<asp:ListItem Value="2">いいえ</asp:ListItem>
                            </asp:DropDownList></dd>
					<%--<dt><asp:Label runat="server" ID="Label16" Text="特記フラグ" ToolTip="Special Flag" /></dt>
					<dd><asp:DropDownList runat="server" ID="ddlSpecial_Flag" >
							<asp:ListItem Value=""></asp:ListItem>
							<asp:ListItem Value="1">【★】</asp:ListItem>
							<asp:ListItem Value="3">【■】</asp:ListItem>
							<asp:ListItem Value="5">【F】</asp:ListItem>
							<asp:ListItem Value="6">【□】</asp:ListItem>
							<asp:ListItem Value="8">送料別途</asp:ListItem>
							<asp:ListItem Value="9">送料お見積もり</asp:ListItem>
                            <asp:ListItem Value="10">【◎】</asp:ListItem>
                            <asp:ListItem Value="11">【◆】</asp:ListItem>
							</asp:DropDownList>
					</dd>--%>
                    <dt><asp:Label runat="server" ID="lblPrice" Text="販売価格" ToolTip="Special Flag" /></dt>
					<dd><asp:DropDownList runat="server" ID="ddlSellingPrice" >
							<asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="1">なし</asp:ListItem>
                            </asp:DropDownList></dd>
                    
					<dt><asp:Label runat="server" ID="Label17" Text="予約フラグ" ToolTip="Reservation Flag" /></dt>
					<dd><asp:DropDownList runat="server" ID="ddlReservation_Flag" >
							<asp:ListItem Value=""></asp:ListItem>
                            <asp:ListItem Value="0">なし</asp:ListItem>
							<asp:ListItem Value="1">通常商品</asp:ListItem>
                            <asp:ListItem Value="2">予約商品</asp:ListItem>
                            <asp:ListItem Value="3">☆即☆</asp:ListItem>
							</asp:DropDownList>
					</dd>
					<dt><asp:Label runat="server" ID="Label18" Text="担当者" ToolTip="Person in charge" /></dt>
					<dd><asp:DropDownList runat="server" ID="ddlPerson" ></asp:DropDownList></dd>
					<dt><asp:Label runat="server" ID="Label19" Text="キーワード検 索" ToolTip="Keyword" /></dt>
					<dd><asp:TextBox runat="server" ID="txtKeyword"/></dd>
				</dl>
				<p><asp:Button ID="btnSearch" runat="server" Text="検索" OnClientClick="target=''" onclick="btnSearch_Click" Width="150px" /></p>
                       </asp:Panel>               
		</div>
                    <div class="stSet sksST shopST">
					<p class="mono"></p>･･･出品済
					<p class="nomono"></p>･･･未出品/
                    <p class="ready"></p>･･･Ready
					<p class="noready"></p>･･･Not Ready
		            </div>
<!-- /Exhibition log search -->
	</div><!--setListBox-->
<!-- Checkbox allSET -->
<div class="operationBtn">
				<p><asp:Button ID="btnCheckAll" runat="server" Text="全てを選択" OnClientClick="target=''"    onclick="btnCheckAll_Click" />&nbsp;
				<asp:Button ID="btnCheckCancel" runat="server" Text="全てを解除" OnClientClick="target=''"    onclick="btnCheckCancel_Click" />&nbsp;
				<asp:Button ID="btnexhibition" runat="server" Text="選択商品を出品する" OnClientClick="target=''" onclick="btnexhibition_Click" />
        </p>
				<p class="itemPage">
			    <asp:DropDownList ID="ddlpage" runat="server" onchange="ddlpage_change(this,event);"  AutoPostBack="true" onselectedindexchanged="ddlpage_SelectedIndexChanged" >
                <asp:ListItem>30</asp:ListItem>
		        <asp:ListItem>50</asp:ListItem>
		        <asp:ListItem>100</asp:ListItem>
		        </asp:DropDownList>
                </p>
		</div>
<!-- /heckbox allSET -->
<!-- exbition list -->	
<div class="itemCmnSet itemInfo">
	<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>
<asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" EmptyDataText="There is no data to display." 
EnableTheming="False" ForeColor="#333333" GridLines="None"   CssClass="listTable2 listTable"
ShowHeaderWhenEmpty="True" AllowPaging="true" onrowcommand="gvItem_RowCommand" 
onrowdatabound="gvItem_RowDataBound" >
<Columns>
<asp:TemplateField HeaderText="行">
<ItemTemplate>
<asp:Label runat="server" ID="lblID" Text='<%#Bind("ID") %>'  Visible="false" />
<asp:Label runat="server" ID="lblNo" Text='<%#Bind("No") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<HeaderTemplate>
<asp:Label runat="server" ID="Label" Text="対象" />
<asp:CheckBox ID="chkall"  runat="server" OnCheckedChanged="chkall_CheckedChanged" AutoPostBack="true" Visible="false"/>
</HeaderTemplate>
<ItemTemplate>
<asp:CheckBox runat="server" ID="chkItem" OnCheckedChanged="chkItem_CheckedChanged" AutoPostBack="true"  />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
<HeaderTemplate>M<br /> ST</HeaderTemplate>
<ItemTemplate>
                    <p id="Ppage" runat="server" class="mono"></p>
					<p id="PWaitSt" runat="server" class="nomono"></p>
	<asp:Label ID="lblSKUStatus" runat="server" Text ='<%#Eval("MCtrl_ID") %>' Visible="false"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
<HeaderTemplate>Ready<br /> ST</HeaderTemplate>
<ItemTemplate>
                    <p id="PReady" runat="server" class="ready"></p>
					<p id="PNReady" runat="server" class="noready"></p>
	<asp:Label ID="lblReadyStatus" runat="server" Text ='<%#Eval("RStatus") %>' Visible="false"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<HeaderTemplate>売れ筋<br />ランク</HeaderTemplate>
<ItemTemplate>
<center><asp:Label ID="lblSellingRank" runat="server" Text ='<%#Eval("Selling_Rank") %>'/></center>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="商品番号">
<ItemTemplate>
<asp:Label runat="server" ID="lnkItemNo" Text='<%#Eval("Item_Code") %>' CssClass="labelAsLink" onclick="NewTabPreview(this,event);"></asp:Label>
<asp:Label runat="server" ID="LabelItem_Code" Text='<%# Eval("Item_Code") %>' Visible="false" />
<asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Item_Code") %>' Text='<%# Eval("Item_Code") %>' CommandName="DataEdit" Visible="false" ></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="商品名">
<ItemTemplate>
<asp:Label ID="lblItemName" runat="server" Text ='<%#Eval("Item_Name") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="商品画像">
<ItemTemplate>
<p><asp:Image runat="server" ID="Image1" ImageUrl="~/Item_Image/no_image.jpg"/></p>
<p><asp:Image runat="server" ID="Image2" ImageUrl="~/Item_Image/no_image.jpg"/></p>
<p><asp:Image runat="server" ID="Image3" ImageUrl="~/Item_Image/no_image.jpg"/></p>
<p><asp:Image runat="server" ID="Image4" ImageUrl="~/Item_Image/no_image.jpg"/></p>
<p><asp:Image runat="server" ID="Image5" ImageUrl="~/Item_Image/no_image.jpg"/></p>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="配送">
<ItemTemplate>
    <asp:Label ID="lbldeliverytype" runat="server" Text ='<%#Eval("Customer_Delivery_Type") %>' Visible="false" />
    <asp:DropDownList runat="server" ID="ddldeliverytype" Width="88px" Enabled="false"></asp:DropDownList>
    <asp:Label ID="lbldeliverymethod" runat="server" Text ='<%#Eval("Delivery_Method") %>' Visible="false"/>
    <asp:DropDownList runat="server" ID="ddldeliverymethod" Enabled="false"></asp:DropDownList><br />
    <asp:Label ID="lblCOD" runat="server" Text ='<%#Eval("Cash_On_Delivery_Fee") %>' Visible="false"/>
    <asp:DropDownList runat="server" ID="ddlCOD" Enabled="false" Width="88px"></asp:DropDownList>
    <asp:Label ID="lblKasama" runat="server" Text ='<%#Eval("Customer_Delivery_Type") %>' Visible="false"/>
    <asp:DropDownList runat="server" ID="ddlKasama" Enabled="false"></asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
</Columns>
<PagerSettings Visible="False" />
</asp:GridView>
<asp:Label runat="server"  ID="hfNewTab" Text="1" style="display:none;"/>
    </ContentTemplate>
    </asp:UpdatePanel>
		</div>
	<uc1:UCGrid_Paging  runat="server"  ID="gp"/>
<!-- /List paging -->
</div><!--ComBlock-->
</div><!--CmnContents-->
<asp:HiddenField ID="hfRefresh" runat="server" />
<asp:HiddenField ID="hfCtrl" runat="server" Value="" />
</div>
</asp:Content>

