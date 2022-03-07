<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Quick_Exhibition.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Quick_Exhibition" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
<br />
<br />
<br />
<asp:TextBox runat="server" ID="txtItem_Codes" Height="100px" Width="800px" 
TextMode="MultiLine" PlaceHolder="cps-test-20141211-10,cps-test-20141211-27"/>
<asp:Button runat="server" ID="btnExhibition" Text="Quick Exhibition" onclick="btnExhibition_Click" />
<br />
<br />

<asp:Button runat="server" ID="btnCheck" Text="Check Exhibition" onclick="btnCheck_Click" />
<asp:GridView ID="gvquickexhibition" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" onpageindexchanging="gvquickexhibition_PageIndexChanging"
EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" AllowPaging="True"  CssClass="listTable" PageSize="100">
</asp:GridView>
</asp:Content>
