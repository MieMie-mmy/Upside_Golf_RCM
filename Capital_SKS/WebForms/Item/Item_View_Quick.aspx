<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_View_Quick.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_View_Quick" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
<link href="../../Scripts/jquery.droppy.js" rel="stylesheet" type="text/css" />

<script type="text/javascript">
    function Show(strOpen, ctrl) {
        var left = (screen.width / 2) - (600 / 2);
        var top = (screen.height / 2) - (500 / 2);
        var retval = "";
        //show modal dialog box and collect its return value
        retval = window.open('../Item/ItemSKU_View.aspx?Item_Code=' + strOpen, window,
		 'status=1,resizable=0,menubar=0,toolbar =0, location ,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
        //check if user closed the dialog 
        //without selecting any value
        if (retval == undefined)
            retval = window.returnValue;
    }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
<asp:HiddenField runat="server" ID="hfRefresh" />
<div id="CmnWrapper">
<div id="CmnContents">
<div id="ComBlock">
<div class="setListBox inlineSet iconSet iconList">
		<h1>商品情報一覧</h1>
		<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
<!-- Exhibition log search -->
		<div class="itemInfo resetValue searchBox iconEx">
			<h2>商品情報一覧検索</h2>
	
				<dl>
					<dt><asp:Label runat="server" ID="Label1" Text="商品名" ToolTip="Item Name" /></dt>
					<dd><asp:TextBox runat="server" ID="txtItem_Name" /></dd>
					<dt><asp:Label runat="server" ID="Label2" Text="商品番号" ToolTip="Item Code" /></dt>
					<dd><asp:TextBox runat="server" ID="txtItem_Code"  /></dd>
				</dl>
				<p><asp:Button ID="btnSearch" runat="server" Text="検索" onclick="btnSearch_Click" Width="150px" /></p>
                                      
		</div>
		<div class="stSet sksST shopST">
<asp:Label ID="Label35" runat="server" Width="20px" ForeColor="White" Text="制" style=" text-align:center;  background-color: #F39;"></asp:Label>
<asp:Label ID="Label34" runat="server" Text="･･･ページ制作"></asp:Label>
<asp:Label ID="Label21" runat="server" Width="20px" ForeColor="White" Text="未" style="text-align:center; background-color: #ff0000;"></asp:Label>
<asp:Label ID="Label22" runat="server" Text="･･･出品待ち"></asp:Label>
<asp:Label ID="Label23" runat="server" Width="20px" ForeColor="White" Text="期" style="text-align:center; background-color: yellow;"></asp:Label>
<asp:Label ID="Label24" runat="server" Text="･･･期日出品待ち"></asp:Label>
<asp:Label ID="Label25" runat="server" Width="20px" ForeColor="White" Text="掲" style="text-align:center; background-color: #00F;"></asp:Label>
<asp:Label ID="Label26" runat="server" Text="･･･出品済 / "></asp:Label>
<asp:Label ID="Label27" runat="server" Width="20px" ForeColor="White" Text="未" style="text-align:center; background-color: #ff0000;"></asp:Label>
<asp:Label ID="Label28" runat="server" Text="･･･未掲載"></asp:Label>
<asp:Label ID="Label29" runat="server" Width="20px" ForeColor="White" Text="掲" style="text-align:center; background-color: #00F;"></asp:Label>
<asp:Label ID="Label30" runat="server" Text="･･･掲載中"></asp:Label>
<asp:Label ID="Label31" runat="server" Width="20px" ForeColor="White" Text="削" style="text-align:center; background-color: #000;"></asp:Label>
<asp:Label ID="Label32" runat="server" Text="･･･削除"></asp:Label>
		 </div>
<!-- /Exhibition log search -->
	</div><!--setListBox-->

<!-- Checkbox allSET -->
<div class="operationBtn">
				<p><asp:Button ID="btnCheckAll" runat="server" Text="全てを選択"  onclick="btnCheckAll_Click" />&nbsp;
				<asp:Button ID="btnCheckCancel" runat="server" Text="全てを解除"  onclick="btnCheckCancel_Click" />&nbsp;
				<asp:Button ID="btnexhibition" runat="server" Text="選択商品を出品する" OnClientClick="CallTab(event);" onclick="btnexhibition_Click"   />&nbsp;
				<asp:Button ID="btnQuickEdit" runat="server" Text="選択商品をクイック編集で表示" onclick="btnQuickEdit_Click"  /></p>
				<p class="itemPage">
			    <asp:DropDownList ID="ddlpage" runat="server" 
			        onselectedindexchanged="ddlpage_SelectedIndexChanged" AutoPostBack="true">
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
onrowdatabound="gvItem_RowDataBound" PageSize="30" >
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
<asp:TemplateField>
<HeaderTemplate>SKS <br /> ST</HeaderTemplate>
<ItemTemplate>
	<asp:Label ID="lblSKUStatus" runat="server" Text ='<%#Eval("Export_Status") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField>
<HeaderTemplate>Shop <br /> ST</HeaderTemplate>
<ItemTemplate>
<asp:Label ID="lblshop" runat="server" Text ='<%#Eval("Ctrl_ID") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="商品番号">
<ItemTemplate>
<asp:Label runat="server" ID="LabelItem_Code" Text='<%# Eval("Item_Code") %>' Visible="false" />
<asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Item_Code") %>' 
Text='<%# Eval("Item_Code") %>' CommandName="DataEdit" ></asp:LinkButton>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="商品名">
<ItemTemplate>
<asp:Label ID="lblItemName" runat="server" Text ='<%#Eval("Item_Name") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="商品画像">
<ItemTemplate>
<asp:Image ID="imgItem" runat="server" Width="100px" Height="100px"
 ImageUrl='<%# Eval("Image_Name", "~/Item_Image/{0}") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="SKU">
<ItemTemplate>

</ItemTemplate>
</asp:TemplateField>
</Columns>
<PagerSettings Visible="False" />
</asp:GridView>
<%--<asp:Label runat="server"  ID="hfNewTab" Text="1" style="display:none;"/>--%>
    </ContentTemplate>
    </asp:UpdatePanel>
		</div>
<!-- /exbition list -->

<!-- List paging -->
<div class="btn">
	<uc1:UCGrid_Paging  runat="server"  ID="gp"/>
</div>
<!-- /List paging -->
</div><!--ComBlock-->
</div><!--CmnContents-->
</div><!--CmnWrapper-->
</div>
</asp:Content>
