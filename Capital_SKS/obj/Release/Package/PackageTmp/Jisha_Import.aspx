<%@ Page Title="" Language="C#" MasterPageFile="~/JishaMaster.Master" AutoEventWireup="true" CodeBehind="Jisha_Import.aspx.cs" Inherits="ORS_RCM.Jisha_Import" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br /><br />
<br /><br />
<br /><br />
<div>
<table>
<tr>
<td>
<asp:Label ID="Label1" runat="server" Text="Import Jisha Item Master : " />
<br /><br />
<asp:Label ID="Label2" runat="server" Text="Import Jisha Item : " />
<br /><br />
<asp:Label ID="Label3" runat="server" Text="Import Jisha Item Category : " />
<br /><br />
</td>
<td>
<asp:FileUpload ID="fuItem_Master" runat="server" />
<br /><br />
<asp:FileUpload ID="fuItem" runat="server" />
<br /><br />
<asp:FileUpload ID="fuItem_Category" runat="server" />
<br /><br />
</td>
<td>
<asp:Button ID="btnRead" runat="server" Text="Read" Width="150px" onclick="btnRead_Click"  />
&nbsp;&nbsp;
<asp:Button ID="btnImport" runat="server" Text="Import" Width="150px" onclick="btnImport_Click" />
</td>
</tr>
</table>
</div>

<br />

<div>
<asp:Label ID="Label4" runat="server" Text="Jisha Item Master : " /><br />
<asp:GridView ID="gv_Jisha_Item_Master" runat="server" Width="95%" ></asp:GridView>
<br /><br />
<asp:Label ID="Label5" runat="server" Text="Jisha Item  : " /><br />
<asp:GridView ID="gv_Jisha_Item" runat="server" Width="95%"></asp:GridView>
<br /><br />
<asp:Label ID="Label6" runat="server" Text="Jisha Item Category : " /><br />
<asp:GridView ID="gv_Jisha_Item_Category" runat="server" Width="95%"></asp:GridView>
<br /><br />
</div>
</asp:Content>
