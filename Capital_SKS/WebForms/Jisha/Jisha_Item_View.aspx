<%@ Page Title="" Language="C#" MasterPageFile="~/JishaMaster.Master" AutoEventWireup="true" CodeBehind="Jisha_Item_View.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Item_View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
.Button {
  border: 3px outset;
  padding: 2px;
  text-decoration: none;
}
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
<%--<div align="right">
<asp:Button ID="btnCart" runat="server" Text="Show Cart" Width="200px" 
        onclick="btnCart_Click" />
</div>--%>
<div align="center" >
       <asp:DataList ID="dlSiteMap" runat="server" CellSpacing="5" CellPadding="5"
        RepeatDirection="Horizontal" Width="95%"
        onitemcommand="dlSiteMap_ItemCommand">
        <ItemTemplate>
        <asp:LinkButton ID="LinkButton" runat="server" CommandArgument='<%# Eval("Category_ID") %>' Text='<%# Eval("Description") %>' CommandName="DataEdit" ></asp:LinkButton> >>
        </ItemTemplate>
        </asp:DataList>
       <br />
       <asp:DataList ID="DataList" runat="server" CellSpacing="5" CellPadding="5"
        RepeatDirection="Vertical" RepeatColumns="3" 
        onitemcommand="DataList_ItemCommand">
        <ItemTemplate>
        <asp:LinkButton ID="LinkButton" runat="server" CommandArgument='<%# Eval("Category_ID") %>' Text='<%# Eval("Description") %>' CommandName="DataEdit" ></asp:LinkButton>
        </ItemTemplate>
        </asp:DataList><br />
        <asp:LinkButton runat="server" ID="lbtnFirst" Text="First"  onclick="lbtnFirst_Click"/>&nbsp;&nbsp;
        <asp:LinkButton runat="server" ID="lbtnPrevious" Text="Previous"  onclick="lbtnPrevious_Click"  />&nbsp;&nbsp;
        <asp:DataList ID="dlPaging" runat="server" BackColor="White" 
        BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" 
        GridLines="Both" onitemdatabound="dlPaging_ItemDataBound" RepeatLayout="Flow"
        onitemcommand="dlPaging_ItemCommand" RepeatDirection="Horizontal" >
        <ItemTemplate>
         <asp:LinkButton runat="server" ID="lnkbtnPaging" Text='<%# Eval("PageText")%>' 
         CommandName="Paging" CommandArgument='<%# Eval("PageIndex")%>' />&nbsp;&nbsp;
         </ItemTemplate>
        <FooterStyle BackColor="White" ForeColor="#000066" />
        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
        <ItemStyle ForeColor="#000066" />
        <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
        </asp:DataList>&nbsp;&nbsp;
        <asp:LinkButton runat="server" ID="lbtnNext" Text="Next"   onclick="lbtnNext_Click" />&nbsp;&nbsp;
        <asp:LinkButton runat="server" ID="lbtnLast" Text="Last"  onclick="lbtnLast_Click" /><br />
        <asp:Label runat="server" ID="lblPageInfo"  />
</div>
<br />

<asp:DataList ID="dListItems" runat="server" BackColor="White" BorderColor="#CCCCCC"
Width="95%" GridLines="Both" Height="200px" RepeatDirection="Horizontal" RepeatColumns="2"
onitemcommand="dListItems_ItemCommand"  >
<ItemTemplate>
<asp:Label runat="server" ID="lblID" Text='<%# Bind("ID")%>' Visible="false" /> 

<asp:Image ID="imgItem" runat="server" Width="50px" ControlStyle-Width="100" ImageUrl='<%# Bind("Image_Name1", "~/Item_Image/{0}") %>'/>
<br /><br />
<asp:Label runat="server" ID="lblImageName" Text='<%# Bind("Item_Name")%>' ForeColor="Green" />
<br />
<asp:Label runat="server" ID="Label3" Text="Price: " />
<asp:Label runat="server" ID="Label1" Text='<%# Bind("List_Price")%>' ForeColor="Orange" />
<br />
<asp:Label runat="server" ID="Label4" Text="Sale Price: "/>
<asp:Label runat="server" ID="Label2" Text='<%# Bind("Sale_Price")%>' ForeColor="Red"/>
<br /><br />
<asp:LinkButton runat="server" ID="lnkDetail" Text="Detail & Order" CssClass="Button"
CommandName="Item Detail"  Font-Strikeout="false" />
<br /><br />
</ItemTemplate>
    <FooterStyle BackColor="White" ForeColor="#000066" />
    <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White"/>
    <ItemStyle HorizontalAlign="center" ForeColor="#000066" Width="50px" />
    <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White"/>
</asp:DataList>
</asp:Content>
