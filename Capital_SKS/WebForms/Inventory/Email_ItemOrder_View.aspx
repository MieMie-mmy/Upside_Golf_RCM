<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Email_ItemOrder_View.aspx.cs" Inherits="ORS_RCM.Email_ItemOrder_View" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/order.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
<link href ="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
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
              document.getElementById("<%=txtFromDate.ClientID%>").value = null;
              document.getElementById("<%=txtToDate.ClientID%>").value = null;
              var drp1 = document.getElementById("<%=ddlShopName.ClientID%>");
              var drp6 = document.getElementById("<%=chkCode.ClientID%>");
              drp1.selectedIndex = 0;
             
              drp6.checked = false;
          }
      }
</script>
	<script type="text/javascript">
		function pageLoad(sender, args) {
			$(function () {
				$("[id$=txtFromDate]").datepicker({
					showOn: 'button',
					buttonImageOnly: true,
					buttonImage: '../../images/calendar.gif',
					dateFormat: 'yy/mm/dd',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:2030" ,
				});
			});
			$(function () {
				$("[id$=txtToDate]").datepicker({
					showOn: 'button',
					buttonImageOnly: true,
					buttonImage: '../../images/calendar.gif',
					dateFormat: 'yy/mm/dd',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:2030" ,
				});
			});
		}
</script>
    <style>
        .margin{
            margin-bottom:3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox inlineSet iconSet iconList">
		<h1>注文商品一覧</h1>
		<div class="ordCmnSet resetValue searchBox">
			<h2>注文検索</h2>
<asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
<dl>
					<dt >ショップ名</dt>
					<dd>
					<asp:DropDownList runat="server" ID="ddlShopName">
</asp:DropDownList>
					</dd>
					<dt>商品番号<br><asp:CheckBox runat="server" ID="chkCode" Text="完全" /></dt>
					<dd><asp:TextBox TextMode ="MultiLine" runat="server" ID="txtItemNumber" ></asp:TextBox>
</dd>
					<dt>注文日時</dt>
					<dd class="cal"><asp:TextBox ID="txtFromDate" runat="server" ReadOnly="True" ></asp:TextBox>
<asp:ImageButton ID="ImageButton1" runat="server" Width="16px" Height="15px" CssClass="margin"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom"/> &nbsp; ~
<asp:TextBox ID="txtToDate" runat="server" ReadOnly="True" ></asp:TextBox>
<asp:ImageButton ID="ImageButton2" runat="server" Width="16px" Height="15px" CssClass="margin"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton2_Click" ImageAlign="AbsBottom" />
</dd>
				</dl>
				<p><asp:Button runat="server" ID="btnSearch" Text="検 索" onclick="btnSearch_Click" 
		Width="113px"/>
</p>
</asp:Panel>
</div>
<div class="ordCmnSet resetValue editBox iconSet2">
<div class="listTableOver">
<table  width="100%">
<tr>
<td>
 
<asp:GridView runat="server" AutoGenerateColumns="False" ID="gvEmailItem" ShowHeaderWhenEmpty="true" DataKeyNames="ID" Width="100%" AllowPaging="True" EmptyDataText="There is no data to display." OnPageIndexChanging="gvEmailItem_PageIndexChanging" CssClass="listTable" onrowcommand="gvEmailItem_RowCommand">
<Columns>
<asp:TemplateField>
<HeaderStyle Width="50px" />
<ItemTemplate>
	<asp:Button ID="btnEdit" runat="server" Text="商品情報" CommandName="DataEdit" CommandArgument='<%#Eval("Item_Code") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="注文日時">
<HeaderStyle Width="50px" />
<ItemTemplate>
<asp:Label runat="server" ID="lblEmailDate" Text ='<%#Eval("Email_Date") %>'></asp:Label>
</ItemTemplate>    
</asp:TemplateField>
<asp:TemplateField HeaderText="商品番号">
<HeaderStyle Width="70px" />
<ItemTemplate>
<asp:Label runat="server" ID="lblItemCode" Text ='<%#Eval("Item_Code") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="サイズ名">
<HeaderStyle Width="70px" />
<ItemTemplate>
<asp:Label runat="server" ID="lblSizeName" Text ='<%#Eval("Size_Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="カラー名">
<HeaderStyle Width="30px" />
<ItemTemplate>
<asp:Label runat="server" ID="txtColorName" Text ='<%#Eval("Color_Name") %>' Width="98%"></asp:Label>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ショップ名">
<HeaderStyle Width="50px" />
<ItemTemplate>
<asp:Label runat="server" ID="lblShopName" Text ='<%#Eval("Store_Name") %>'></asp:Label>
</ItemTemplate>
</asp:TemplateField>
</Columns>
	<PagerSettings Visible="False" />
</asp:GridView>
</td>
</tr>
</table>
</div>
</div>
</div>
</div>
</div>
<asp:HiddenField runat="server" ID="hdfFromDate"/>
<asp:HiddenField runat="server" ID="hdfToDate"/>
<asp:HiddenField runat="server" ID="hdfSearch"/>
<div class="btn">
	<uc1:UCGrid_Paging  runat="server"  ID="gp"/>
</div>
</asp:Content>
