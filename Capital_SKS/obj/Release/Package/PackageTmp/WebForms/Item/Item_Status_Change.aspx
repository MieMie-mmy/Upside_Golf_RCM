<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Status_Change.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Status_Change" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
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
            document.getElementById("<%=txtItem_Code.ClientID%>").value = null;
            var drp1 = document.getElementById("<%=ddlExport_Status.ClientID%>");
            var drp2 = document.getElementById("<%=ddlCtrl_ID.ClientID%>");
            var drp6 = document.getElementById("<%=chkCode.ClientID%>");
            drp1.selectedIndex = 0;
            drp2.selectedIndex = 0;
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
        //		 'status=1,resizable=0,menubar=0,toolbar =0, location ,width=1000,height=500,scrollbars=1,top=' + top + ',left=' + left);
        //check if user closed the dialog 
        //without selecting any value
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
        // 'dialogHeight:500px; dialogWidth:1000px; dialogLeft:150px; dialogRight :50px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
        //check if user closed the dialog 
        //without selecting any value
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
<h1>ステータス変更一覧</h1>
<!-- Exhibition log search -->
<div class="itemInfo resetValue searchBox iconEx" >
	<h2>商品情報一覧検索</h2>
	<asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
				<dl>
                    
					<dt><asp:Label runat="server" ID="Label2" Text="商品番号" ToolTip="Item Code" /><br><asp:CheckBox runat="server" ID="chkCode" Text="完全" /></dt>
					<dd><asp:TextBox onkeydown="return checkKeycode(this)" TextMode ="MultiLine" runat="server" ID="txtItem_Code" /></dd>
                    <dt><asp:Label runat="server" ID="Label1" Text="ステータス" Width="60px"/></dt>
                    <asp:UpdatePanel runat="server" id="UpdatePanel2" updatemode="Conditional" ><ContentTemplate>
                    <asp:RadioButton ID="rdoSKSStauts" runat="server" GroupName="rdoStatus"  Text="SKSステータス" AutoPostBack="true"/>
                    <dd><asp:DropDownList runat="server" ID="ddlExport_Status" Visible="false">
							 <asp:ListItem Value="3">出品待ち</asp:ListItem>
							</asp:DropDownList>
					</dd>
                    <asp:RadioButton ID="rdoShopStatus" runat="server" GroupName="rdoStatus"  Text="ショップステータス" AutoPostBack="true"/>
                    <dd><asp:DropDownList runat="server" ID="ddlCtrl_ID" Visible="false">
							<asp:ListItem Value="dg">削除/掲載無</asp:ListItem>
							</asp:DropDownList>
					</dd>
                    </ContentTemplate><Triggers>
                    <asp:Asyncpostbacktrigger controlid="rdoSKSStauts"/>
                    <asp:Asyncpostbacktrigger controlid="rdoShopStatus"/>
                    </Triggers>
                    </asp:UpdatePanel>
                 <br />
                 <br />
				</dl>
                   <p><asp:Button ID="btnSearch" runat="server" Text="検索" OnClientClick="target=''" onclick="btnSearch_Click" Width="150px" /></p> 
                       </asp:Panel>               
		</div>
         <div class="ptSet sksST shopST">
					※ <p class="wait1"></p> 出品待ち  → <p class="page"></p> ページ制作    
                    <p style="margin-right:80px;"> <p></p>※ <p class="del"></p> 削除<p class="deactive"></p> 掲載無   → <p class="wait"></p> 未掲載 / SKSステータス:<p class="page"></p> ページ制作
                    </p>                    </div>
<!-- /Exhibition log search -->
</div><!--setListBox-->
<!-- Checkbox allSET -->
<div class="operationBtn">
		<p><asp:Button ID="btnCheckAll" runat="server" Text="全てを選択" OnClientClick="target=''"    onclick="btnCheckAll_Click" />&nbsp;
				<asp:Button ID="btnCheckCancel" runat="server" Text="全てを解除" OnClientClick="target=''"    onclick="btnCheckCancel_Click" />&nbsp;
				<asp:Button ID="btnStatus" runat="server" Text="ステータス変更" OnClientClick="NotNewTab();" onclick="btn_ChangeStatus_Click"   />&nbsp;
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
EnableTheming="False" ForeColor="#333333" GridLines="None"   CssClass="listTable3 listTable"
ShowHeaderWhenEmpty="True" AllowPaging="true" onrowdatabound="gvItem_RowDataBound" >
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
<HeaderTemplate>SKS <br /> ST</HeaderTemplate>
<ItemTemplate>
                    <p id="Ppage" runat="server" class="page"></p>
					<p id="PWaitSt" runat="server" class="wait1"></p>
					<p id="PWaitL" runat="server" class="waitL"></p>
                    <p id="PExhibit" runat="server" class="exhibit"></p>
					<p id="POkSt" runat="server" class="ok1"></p>
<asp:Label ID="lblSKUStatus" runat="server" Text ='<%#Eval("Export_Status") %>' Visible="false"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
<HeaderTemplate>Shop <br /> ST</HeaderTemplate>
<ItemTemplate>
					<p id="PWait" runat="server" class="wait"></p>
					<p id="POk" runat="server" class="ok"></p>
					<p id="PDel" runat="server" class="del"></p>
                    <p id="PInactive" runat="server" class="deactive"></p>
<asp:Label ID="lblshop" runat="server" Text ='<%#Eval("Ctrl_ID") %>' Visible="false" />
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
<p><asp:Image runat="server" ID="Image1"/></p>
<p><asp:Image runat="server" ID="Image2"/></p>
<p><asp:Image runat="server" ID="Image3"/></p>
<p><asp:Image runat="server" ID="Image4"/></p>
<p><asp:Image runat="server" ID="Image5"/></p>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="SKU/ショップ/プレビュー">
<ItemTemplate>
<asp:Button ID="btnSKU"  runat="server"  Text="SKU別窓表示" />
<asp:Label runat="server" ID="Label4" Text="-----------------------" />
<asp:DropDownList ID="ddlShop" onchange="NewTabPreview(this,event);" Width="150px" runat="server"></asp:DropDownList>
<asp:Label runat="server" ID="Label6" Text="-----------------------" />
<asp:Button ID="btnPreview"  runat="server"  Text="プレビュー" />
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
</div><!--ComBlock-->
</div><!--CmnContents-->
<asp:HiddenField ID="hfRefresh" runat="server" />
<asp:HiddenField ID="hfCtrl" runat="server" Value="" />
</div>
    </div>
</asp:Content>
