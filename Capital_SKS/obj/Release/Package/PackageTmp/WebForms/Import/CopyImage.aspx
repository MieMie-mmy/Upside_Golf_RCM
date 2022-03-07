<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CopyImage.aspx.cs" Inherits="ORS_RCM.WebForms.Import.CopyImage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
<br />
<br />
<br />
    <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Height="100px" Width="800px" PlaceHolder="eg: image.jpg,picture.jpg"></asp:TextBox> 
    <asp:Button ID="Button1" runat="server" Text="Copy" onclick="Button1_Click" />
</asp:Content>
