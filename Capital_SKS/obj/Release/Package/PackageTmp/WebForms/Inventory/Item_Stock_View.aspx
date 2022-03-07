<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_Stock_View.aspx.cs" Inherits="ORS_RCM.Item_Search" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/stock.css" rel="stylesheet" type="text/css" />
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
              document.getElementById("<%=txtItemNumber.ClientID%>").value = null;
              document.getElementById("<%=txtprocode.ClientID%>").value = null;
              document.getElementById("<%=txtProductName.ClientID%>").value = null;
              document.getElementById("<%=txtcatinfo.ClientID%>").value = null;
              document.getElementById("<%=txtbrandname.ClientID%>").value = null;
              document.getElementById("<%=txtcompetitionname.ClientID%>").value = null;
              document.getElementById("<%=txtyear.ClientID%>").value = null;
              document.getElementById("<%=txtseason.ClientID%>").value = null;
              document.getElementById("<%=txtjancode.ClientID%>").value = null;
              var drp6 = document.getElementById("<%=chkCode.ClientID%>");
          
              drp6.checked = false;
          }
      }
</script>

<%--<script type="text/javascript">
    //call web method function(current row no of gridview) without postback
    function UpdateQuantity(rowNo) {  
    //get current quantity
        var quantity = document.getElementById('MainContent_gvItem_txtStockQuantity_' + rowNo).value;
    //get current id
    var ID = document.getElementById('MainContent_gvItem_lblID_' + rowNo).value;
    rowNo = parseInt(rowNo) + parseInt("1");//get next rowNo that focus to another textbox
    var txt = document.getElementById('MainContent_gvItem_txtStockQuantity_' + rowNo);
    if (txt != null) {
        txt.focus();//focus next textbox
    }
    $.ajax({adasdasd
        type: "POST",
        url: "Item_Stock_View.aspx/updateQuantity",//call c# function
        contentType: "application/json;charset=utf-8",
        data: "{'id':'" + ID + "','quantity':'" + quantity + "'}",//passing id and quantity to update
        dataType: "json",
        success: function (data) {
        },
        error: function (result) {
            alert("Error");
        }
    });
}
</script>--%>

<script type="text/javascript">
    //call web method function(current row no of gridview) without postback
    function UpdateQuantity(rowNo) {
        //get current quantity
        var quantity = document.getElementById('MainContent_gvItem_txtStockQuantity_' + rowNo).value;

        var jisha_quantity = document.getElementById('MainContent_gvItem_txtJishaQuantity_' + rowNo).value;

        //get current id
        var ID = document.getElementById('MainContent_gvItem_lblID_' + rowNo).value;
        rowNo = parseInt(rowNo) + parseInt("1"); //get next rowNo that focus to another textbox
        var txt = document.getElementById('MainContent_gvItem_txtStockQuantity_' + rowNo);
        if (txt != null) {
            txt.focus(); //focus next textbox
        }
        $.ajax({
            type: "POST",
            url: "Item_Stock_View.aspx/updateQuantity", //call c# function
            contentType: "application/json;charset=utf-8",
            data: "{'id':'" + ID + "','quantity':'" + quantity + "', 'jisha_quantity': '" + jisha_quantity + "'}", //passing id and quantity to update


            dataType: "json",

            success: function (data) {
            },
            error: function (result) {
                alert("Error");
            }
        });
    }
</script>

<script type="text/javascript">
    function trapReturn(ctrl, e) {
        var objval = ctrl.value;
        var objvalue = ctrl.id;
        var temp = objvalue.split("_");
        var rowNo = temp[temp.length - 1];
        var iKeyCode = 0;
        document.getElementById("<%=hfrowNo.ClientID%>").value = rowNo;
        if (window.event) iKeyCode = window.event.keyCode
        else if (e) iKeyCode = e.which;
        if (iKeyCode == 13) {
            //press enter key to gridview's textbox to update quantity
            document.getElementById("<%=hfctrl.ClientID%>").value = objvalue;
            UpdateQuantity(rowNo);
            return false;
        }
        if (!(iKeyCode == 8 || iKeyCode == 13 || iKeyCode == 46) && (iKeyCode < 48 || iKeyCode > 57)) {
            return false;
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfctrl" runat="server" Value="" />
    <asp:HiddenField ID="hfrowNo" runat="server" Value="-1" />
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox inlineSet iconSet iconList">
		<h1>在庫一覧</h1>
		<div class="stcCmnSet resetValue searchBox">
			<h2>在庫検索</h2>

<%--<asp:Panel runat="server" ID="pnl1" >--%>
<asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">

<dl>
					<dt>商品番号<br><asp:CheckBox runat="server" ID="chkCode" Text="完全" /></dt>
					<dd><asp:TextBox TextMode ="MultiLine" runat="server" ID="txtItemNumber"></asp:TextBox>
</dd>
					<dt>メーカー<br>商品コード</dt>
					<dd><asp:TextBox ID="txtprocode" runat="server"></asp:TextBox>
</dd>
					<dt>商品名</dt>
					<dd><asp:TextBox runat="server" ID="txtProductName"></asp:TextBox>
</dd>
					<dt>カタログ情報</dt>
					<dd><asp:TextBox ID="txtcatinfo" runat="server"></asp:TextBox>
</dd>
					<dt>ブランド名</dt>
					<dd><asp:TextBox ID="txtbrandname" runat="server"></asp:TextBox>
</dd>
					<dt>競技名</dt>
					<dd><asp:TextBox ID="txtcompetitionname" runat="server"></asp:TextBox>
</dd>
					<dt>年度</dt>
					<dd><asp:TextBox ID="txtyear" runat="server"></asp:TextBox>
</dd>
					<dt>シーズン</dt>
					<dd><asp:TextBox ID="txtseason" runat="server"></asp:TextBox>
</dd>
					<dt>JANコード</dt>
					<dd><asp:TextBox ID="txtjancode" runat="server"></asp:TextBox>
</dd>
				</dl>
				<p><asp:Button runat="server" ID="btnSearch" Text="検 索" onclick="btnSearch_Click"/>
</p>
  </asp:Panel>
</div>
</div>
</div>
</div>
<%--</asp:Panel>--%>
<div id="CmnContents2">
<div id="ComBlock2">
    <p class="itemPage">
			    <asp:DropDownList ID="ddlpage" runat="server"  AutoPostBack="true" onselectedindexchanged="ddlpage_SelectedIndexChanged" >
		        <asp:ListItem>30</asp:ListItem>
		        <asp:ListItem>50</asp:ListItem>
		        <asp:ListItem>100</asp:ListItem>
		        </asp:DropDownList>
                </p>
</div>

<div class="stcCmnSet ordCmnSet editBox iconSet2">
<div class="listTableOver" >




<%--<asp:Button runat="server" ID="btnEdit" Text="編集" onclick="btnEdit_Click" style="display: none;"/>--%>
<input type="button" id="btnEdit" style="display: none;" />
<asp:GridView runat="server" AutoGenerateColumns="False" ID="gvItem" 
		DataKeyNames="ID"  AllowPaging="True" 
		onpageindexchanging="gvItem_PageIndexChanging" CssClass="listTable" 
		EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" 
		onrowdatabound="gvItem_RowDataBound" onrowcommand="gvItem_RowCommand">

<Columns>
<asp:TemplateField>
<ItemTemplate>
	<asp:Button ID="btnmerchInfo" runat="server" Text="商品情報"  CommandName="Information" CommandArgument='<%#Eval("Item_Code") %>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="注文日時" Visible="false">
<ItemTemplate>
	<asp:Label ID="Label8" runat="server" Text=""></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="商品番号">
<ItemTemplate>
<asp:Label runat="server" ID="lblItemNumber" Text ='<%#Eval("Item_Code") %>'></asp:Label>
<asp:Label runat="server" ID="lblID" Text ='<%#Eval("ID") %>' Visible="false"></asp:Label>
</ItemTemplate>    
</asp:TemplateField>
<asp:TemplateField HeaderText="サイズコード">
<ItemTemplate>
<asp:Label runat="server" ID="lblSizeCode" Text ='<%#Eval("Size_Code") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="カラーコード">
<ItemTemplate>
<asp:Label runat="server" ID="lblColorCode" Text ='<%#Eval("Color_Code") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>  
<asp:TemplateField HeaderText="商品名">
<ItemTemplate>
	<asp:Label ID="Label9" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="サイズ">
<ItemTemplate>
<asp:Label runat="server" ID="lblSizeName" Text ='<%#Eval("Size_Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="カラー">
<ItemTemplate>
<asp:Label runat="server" ID="lblColorName" Text ='<%#Eval("Color_Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ショップ" Visible="false">
<ItemTemplate>
	<asp:Label ID="Label10" runat="server" Text=""></asp:Label>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="在庫数" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
<asp:TextBox style="text-align:right" runat="server" ID="txtStockQuantity" onkeypress="return trapReturn(this, event);" Text ='<%#Eval("Quantity") %>'></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>

<%--Added New Column by  EEP at 2015.12.21--%>

<asp:TemplateField HeaderText="自社在庫数" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
<asp:TextBox style="text-align:right" runat="server" ID="txtJishaQuantity" onkeypress="return trapReturn(this, event);"  Text ='<%#Eval("Jisha_Quantity") %>'></asp:TextBox>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="更新日時" >
<ItemTemplate>
	<asp:Label ID="Label11" runat="server" Text='<%#Eval("Updated_Date") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="定価" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
	<asp:Label ID="lblListPrice" style="text-align:right" runat="server" Text='<%#Eval("List_Price") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="販売価格" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
	<asp:Label ID="lblSalePrice" style="text-align:right" runat="server" Text='<%#Eval("Sale_Price") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="原価" ItemStyle-HorizontalAlign="Right">
<ItemTemplate>
	<asp:Label ID="lblCost" style="text-align:right" runat="server" Text='<%#Eval("Cost") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="競技">
<ItemTemplate>
	<asp:Label ID="Label15" runat="server" Text='<%#Eval("Competition_Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="年度">
<ItemTemplate>
<asp:Label ID="Label1" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="シーズン">
<ItemTemplate>
<asp:Label ID="Label2" runat="server" Text='<%#Eval("Season") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="JANコード">
<ItemTemplate>
<asp:Label ID="Label3" runat="server" Text='<%#Eval("JAN_Code") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="Stock Flag">
<ItemTemplate>
     <asp:CheckBox ID="chkStockflg" runat="server" OnCheckedChanged="chkStockflg_CheckedChanged" AutoPostBack="true" />
     <asp:Label ID="LblStockflg" runat="server" Text='<%# Eval("Flag") %>' Visible="false"></asp:Label>
</ItemTemplate>
</asp:TemplateField>
</Columns>
	<PagerSettings Visible="False" />
</asp:GridView>  

</div>
</div>
</div>

<div class="btn">
	<uc1:UCGrid_Paging  runat="server"  ID="gp"/>
</div>

<asp:HiddenField runat="server" ID="hdfSearch"/>
</asp:Content>
