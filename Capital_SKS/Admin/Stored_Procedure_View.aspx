﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Stored_Procedure_View.aspx.cs" Inherits="Upside_Golf_RCM.Admin.Stored_Procedure_View" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
      <table>
    <tr><td><asp:Label ID="Label1" runat="server" Text="Stored Procedure Name"></asp:Label></td><td>
        <asp:TextBox ID="Txttablesearch" runat="server" Height="42px" Width="186px"></asp:TextBox></td><td>
            <asp:Button ID="Btnsearch" runat="server" Text="Search" 
                onclick="Btnsearch_Click" /></td>
    </tr>
    </table>
    </div>
    <asp:GridView ID="GdViewtable" runat="server">
        <Columns>
             <asp:TemplateField ShowHeader="False" >
                    <ItemTemplate>
                        <asp:CheckBox ID="Chktable" runat="server" ></asp:CheckBox>
                    </ItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
    
    </div>
    </form>
</body>
</html>
