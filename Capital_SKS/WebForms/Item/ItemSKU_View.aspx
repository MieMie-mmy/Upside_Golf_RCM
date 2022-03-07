<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemSKU_View.aspx.cs" Inherits="ORS_RCM.WebForms.Item.ItemSKU_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

<link rel="stylesheet" href="../../Styles/base.css"/>
<link rel="stylesheet" href="../../Styles/common.css"/>
<link rel="stylesheet" href="../../Styles/manager-style.css"/>
<link rel="stylesheet" href="../../Styles/item.css"/>

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/javascript"></script>
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>

<title>商品管理システム＜SKU＞</title>

<script language="javascript" type="text/javascript">

        function GetRowValue(val) {

            // hardcoded value used to minimize the code.

            // ControlID can instead be passed as query string to the popup window

           window.opener.document.getElementById("hfRefresh").value = val;

            window.close();

        }
</script>
</head>
<body>
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox iconSet iconDb">

		<h1>SKU</h1>
	</div><!--setDetailBox-->
	</div><!--ComBlock-->
</div><!--CmnContents-->
<form runat="server">
<div class="itemSKU">
    <asp:Label ID="Label1" runat="server" Text="商品番号:"></asp:Label>
    <asp:Label ID="lblitemname" runat="server" Text=""></asp:Label>
<div runat="server" id="divTable" >
</div>
<%--<asp:GridView ID="gvItem" runat="server" class="listTable">
</asp:GridView>--%>
<%--<asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="false" ShowHeader="false" class="listTable">
<Columns>
<asp:TemplateField>
<ItemTemplate>
<table>
<tr>
	<th>&nbsp;</th>
	<th><asp:Label runat="server" ID="Label1" Text ='<%#Eval("Size_Name_Official") %>'/><span><asp:Label runat="server" ID="Label2" Text ='<%#Eval("Size_Code") %>'/></span></th>
</tr>
<tr>
	<td><asp:Label runat="server" ID="Label3" Text ='<%#Eval("Color_Name") %>' /><span><asp:Label runat="server" ID="Label4" Text ='<%#Eval("Color_Code") %>'/></span></td>
	<td colspan="2"><asp:Label runat="server" ID="Label5" Text ='<%#Eval("Quantity") %>'/></td>
</tr>
</table>
</ItemTemplate>
</asp:TemplateField>
</Columns>
</asp:GridView>--%>

</div>

    <%--<form id="form1" runat="server">
    <div>
    
    <asp:GridView ID="gvItem" runat="server" AutoGenerateColumns="False" 
        CellPadding="4" EmptyDataText="There is no data to display." PageSize="10" 
        EnableTheming="False" ForeColor="#333333" GridLines="None" 
        ShowHeaderWhenEmpty="True" AllowPaging="True" 
            onpageindexchanging="gvItem_PageIndexChanging">
        <AlternatingRowStyle BackColor="White" />
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#B0B0B0" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#B0B0B0" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#B0B0B0" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
        <PagerStyle HorizontalAlign = "Left" CssClass = "GridPager" ForeColor="Black" />
        <Columns>
               <asp:TemplateField HeaderText="">
                             <HeaderStyle Width="100px" />
                             <ItemStyle Width="100px" HorizontalAlign="Left"  />
                           
                     <FooterTemplate>
                    <asp:Label ID ="lbllink" Text ="Link" runat ="server"></asp:Label>
                    </FooterTemplate>
                         </asp:TemplateField>
           
                        <asp:TemplateField HeaderText="Size_Code" FooterStyle-Width="900px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbSizecode" runat="server" Text ='<%#Eval("Size_Code") %>'></asp:Label>
                    </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                     <asp:TemplateField HeaderText="Size_Name" FooterStyle-Width="900px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblsize" runat="server" Text ='<%#Eval("Size_Name") %>'></asp:Label>
                    </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Color_Code" FooterStyle-Width="900px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lblcolorCode" runat="server" Text ='<%#Eval("Color_Code") %>'></asp:Label>
                    </ItemTemplate>
                      <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Color_Name" FooterStyle-Width="900px" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Color_Name") %>'></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>

        </Columns>

    </asp:GridView>
    
    </div>

<%--<div class="content">
<div style="padding-left:150px; width:auto;">
<table align="center">
<tr>
<td>
<asp:Button ID="btnCancel" runat="server" Text="キャンセル" Width="150px" onclick="btnCancel_Click" />
</td>
<td style="text-align:center;">
<asp:LinkButton runat="server" ID="LinkButton" Text="▲TOP" Width="100px" ForeColor="Black" BackColor="White"/>
</td>
</tr>
</table>
</div>
</div>
<br />
<br />
<br />
<br />
<br />--%>
</form>
</body>
</html>
