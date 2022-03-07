<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_Code_URL.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Code_URL" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
    .style{
    background: #d6efff;
       border-left: 3px solid #73c5ff;
    }
    .text
    {
            border: 1px solid #b5b5b5;
    background: #fff;
    padding: 1px 4px;
    border-radius: 4px;
    font-size: 12px;
    box-sizing: border-box;
        height: 24px;

        }
        .button
        {
            width :100%;
    margin: 20px auto 0px auto;
    position: fixed;
    bottom: 0px;
    background: rgba(61,58,53,1);
    left: 0px;
    padding: 7px 0px;
    text-align: center;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <div style="text-align:left;margin:auto;font-size:medium;">
     Item_Code_URL Adding Form
     <hr />
    </div>
    <div>
                <asp:DataList ID="dlShop" runat="server"  RepeatDirection="Vertical" RepeatColumns="2" RepeatLayout="Flow">
                <HeaderTemplate>
                <table>
                </HeaderTemplate>
                <ItemTemplate >
                <tr>
                <td><asp:CheckBox runat="server" ID="ckbShop" EnableViewState="true" Visible="false" Checked="true"/></td>
                <td><asp:Label runat="server" ID="lblShopID" Text='<%# Bind("ID")%>' Visible="false"/></td>
                <td><asp:Label runat="server" ID="lblShopName" Text='<%# Bind("Shop_Name")%>' CssClass="style" /></td>
                <td><asp:TextBox ID="txtItem_Code" runat="server" Text='<%# Bind("Item_Code")%>' CssClass="text"/></td>
                </tr>
                </ItemTemplate>
                <FooterTemplate>
    </table>
</FooterTemplate>
                </asp:DataList>
    </div>
    <div class="button">
        <asp:Button ID="btnSave" runat="server" Text="決定" OnClick="btnSave_OnClick" Width="100px" Height="20px" /></div>
    </form>
</body>
</html>
