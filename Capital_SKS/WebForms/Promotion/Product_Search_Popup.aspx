<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Product_Search_Popup.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Product_Search_Popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <fieldset>
    <table>
    <tr>
    <td>
    商品番号
        <asp:TextBox ID="txtitemcode" runat="server"  Width="140px"></asp:TextBox>
        <asp:CheckBox ID="chktype" runat="server" />完全&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        商品名
        <asp:TextBox ID="txtitemname" runat="server"  Width="140px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        ショップステータス
        <asp:DropDownList ID="ddlshopstatus" runat="server"  Width="140px">
        <asp:ListItem></asp:ListItem>
        <asp:ListItem>未掲載</asp:ListItem>
        <asp:ListItem>掲載中</asp:ListItem>
        <asp:ListItem>削除</asp:ListItem>
        </asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        ブランド名
        <asp:TextBox ID="txtbrandname" runat="server"  Width="140px"></asp:TextBox>
 </td>
        </tr>
        <tr>
        <td>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        指示書番号
            <asp:TextBox ID="txtinstructionno" runat="server"  Width="140px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            競技
            <asp:TextBox ID="txtcompetition" runat="server"  Width="140px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           
            分類
            <asp:TextBox ID="txtclassification" runat="server"  Width="140px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

        </td>
        </tr>
        <tr>
        <td>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;
        
         
        年度
            <asp:TextBox ID="txtyear" runat="server"  Width="140px"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            シーズン
            <asp:TextBox ID="txtseason" runat="server"  Width="140px"></asp:TextBox>
        </td>
        </tr>
        </table>
        <div align="center">
            <asp:Button ID="btnsearch" runat="server" Text="検索" style="margin-left: 0px" 
                Width="92px" onclick="btnsearch_Click" /></div>
    </fieldset>
        <asp:Button ID="btnselectall" runat="server" Text="全て選択" 
            onclick="btnselectall_Click" />&nbsp;&nbsp;&nbsp;<asp:Button 
            ID="btncancelall" runat="server" Text="全て解除" onclick="btncancelall_Click" />
        <br />
        <asp:GridView ID="gvitem" runat="server" AutoGenerateColumns="False" 
            EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" 
            onrowdatabound="gvitem_RowDataBound" AllowPaging="True" PageSize="30" 
            onpageindexchanging="gvitem_PageIndexChanging" >
            <Columns>
            <asp:TemplateField  >
                    <ItemTemplate >
                        <asp:CheckBox ID="chkitem" OnCheckedChanged="ckItems_Check"  runat="server" />
                        
                    </ItemTemplate>
                   
                </asp:TemplateField>
             
                <asp:TemplateField HeaderText ="商品番号">
                    <ItemTemplate >
                        <asp:Label ID="lblitemcode" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                   <asp:TemplateField HeaderText ="商品名">
                    <ItemTemplate >
                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                   <asp:TemplateField HeaderText ="対象ショップ">
                    <ItemTemplate >
                        <asp:Label ID="lblshop" runat="server" Text='<%#Eval("Shop_Name") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                   <asp:TemplateField HeaderText ="SHOP ST" ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                    <ItemTemplate >
                       <p id="PWait" runat="server" class="wait"></p>
					<p id="POk" runat="server" class="ok"></p>
					<p id="PDel" runat="server" class="del"></p>
                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Ctrl_ID") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                   <asp:TemplateField HeaderText ="ブランド名">
                    <ItemTemplate >
                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Brand_Name") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                   <asp:TemplateField HeaderText ="年度">
                    <ItemTemplate >
                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                   <asp:TemplateField HeaderText ="シーズン">
                    <ItemTemplate >
                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("Season") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                 <asp:TemplateField HeaderText ="指示書番号">
                    <ItemTemplate >
                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("Instruction_No") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
                       <asp:TemplateField Visible="false">
                    <ItemTemplate >
                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                    </ItemTemplate>
                   
                </asp:TemplateField>
            </Columns>
        </asp:GridView><br />
     <div align="center">   <asp:Button ID="btnOK" runat="server" Text="決定" Width="67px" 
             onclick="btnOK_Click" /></div>
    </div>
    </form>
</body>
</html>
