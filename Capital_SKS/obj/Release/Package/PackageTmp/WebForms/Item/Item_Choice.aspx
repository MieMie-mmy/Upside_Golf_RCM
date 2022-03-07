<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_Choice.aspx.cs" Inherits="ORS_RCM.Item_Choice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <br />
    <div>
    <asp:Image ID="lblSearch" runat="server" 
    Height="30px" Width="30px" ImageUrl="~/images/index.jpg" 
    BorderStyle="Solid" BorderWidth="0px"  ImageAlign="AbsBottom"  />&nbsp;
    <%--<asp:Label runat="server" ID="Label1" Text="Search"  /> --%>
    <asp:TextBox runat="server" ID="txtSearch" Width="200px" />&nbsp;
    <asp:Button runat="server" ID="btnSearch" Text="Search" 
            onclick="btnSearch_Click1"  />
    </div>
    <br />
    <br />
    <div>
        <asp:GridView ID="gvItem" runat="server" Width="50%" AutoGenerateColumns="False"
           AllowPaging="True" PageSize="50" CellPadding="4" ForeColor="#333333" 
            GridLines="None" onpageindexchanging="gvItem_PageIndexChanging" >
         <AlternatingRowStyle BackColor="White" />
        <Columns>
        <asp:TemplateField HeaderText="ID" Visible="false">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblID" Text='<%# Eval("ID")%>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
            <asp:CheckBox ID="ckbItem" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Item Code">
             <ItemTemplate>
              <asp:Label ID="lblItemCode" runat="server" Text ='<%#Eval("Item_Code") %>'></asp:Label>
             </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="Item Name">
            <ItemTemplate>
                <asp:Label ID="Label3" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
            </ItemTemplate>
      </asp:TemplateField>
      </Columns>

            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />

        </asp:GridView>
        <br />
        <asp:Button runat="server" ID="btnOK" Text="決定" Width="200px" 
            onclick="btnOK_Click" />
        
    </div>
    </form>
</body>
</html>
