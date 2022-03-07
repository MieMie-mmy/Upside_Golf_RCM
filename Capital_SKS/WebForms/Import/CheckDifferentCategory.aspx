<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CheckDifferentCategory.aspx.cs" Inherits="ORS_RCM.WebForms.Import.CheckDifferentCategory" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<br />
<br />
<br />
<br />
<center>
<h2>Rakuten_SportsPlaza_Shop_Item_Category</h2>
<br />
<div style="width:30%">
<asp:RadioButtonList ID="rdoList" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" 
  onselectedindexchanged="rdoList_SelectedIndexChanged">
<asp:ListItem Value="1" Selected="True">BatchCheck</asp:ListItem>
<asp:ListItem Value="2">Import</asp:ListItem>
</asp:RadioButtonList>
</div>
<br />
<asp:Button runat="server" Text="削除 Import" ToolTip="Truncate" ID="btnTruncate" onclick="btnTruncate_Click" Visible="false"/>
&nbsp;&nbsp;<asp:FileUpload runat="server" ID="fuCategory" Enabled="false"/>
&nbsp;&nbsp;<asp:Button runat="server" Text="インポート開始" ToolTip="Import" ID="btnImport" onclick="btnImport_Click" Enabled="false"/>
&nbsp;&nbsp;<asp:Button runat="server" Text="チェック" ToolTip="Check" ID="btnCheck" onclick="btnCheck_Click"/>
<asp:GridView ID="gvCategory" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" Width="50%"
EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True"  CssClass="listTable">
</asp:GridView>
</center>
</asp:Content>
