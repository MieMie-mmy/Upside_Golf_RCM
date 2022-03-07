<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuerySearch.aspx.cs" Inherits="ORS_RCM.WebForms.QuerySearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table width="100%">
<tr>
<td>
<asp:TextBox runat="server" ID="txtQuery" TextMode="MultiLine" Height="90px" 
        Width="356px"></asp:TextBox>
        <asp:Button runat="server" ID="btnRun" Text="Run Query" onclick="btnRun_Click"/>
</td>
</tr>
<tr>
<td colspan="2">
<asp:GridView runat="server" ID="gvResult" AutoGenerateColumns="true" EmptyDataText="There is no record to display.">
</asp:GridView>
</td>
</tr>
</table>
    </div>
    </form>
</body>
</html>
