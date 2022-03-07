<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddSKU.aspx.cs" Inherits="ORS_RCM.WebForms.Item.AddSKU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" href="../../Styles/item.css"  type="text/css"/>
    <script type="text/javascript">
        function CheckInt(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align:center;min-width:1000px;margin:auto;">
    <h1 id="H1" class="tag_listHD" runat="server">SKU Adding Form</h1>
    </div>
    <div class="gdvsku">
         <asp:gridview ID="gdvAddSku" runat="server" ShowFooter="true" Cssclass="gdvSKU"
             AutoGenerateColumns="false" OnRowDataBound="gdvAddSku_RowDataBound" >
        <Columns>
            <asp:TemplateField>
            <ItemTemplate>
                  <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"/>
            </ItemTemplate>
             </asp:TemplateField>
          <%--  <asp:TemplateField HeaderText="販売管理番号">
                <ItemTemplate>
                    <asp:TextBox ID="txtItemAdminCode" runat="server" Width="110px" Text='<%# Eval("Item_AdminCode") %>'></asp:TextBox>
                    
                </ItemTemplate>
            </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="Item_Code" Visible="false">
            <ItemTemplate>
                 <asp:TextBox ID="txtItemCode" runat="server" Width="110px"  Text='<%# Eval("Item_Code") %>'></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="サイズ名<br>(項目選択肢別在庫用横軸選択肢)">
            <ItemTemplate>
                 <asp:TextBox ID="txtSizeName" runat="server" Width="180px" Text='<%# Eval("Size_Name") %>'></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField> 
            <asp:TemplateField HeaderText="カラー名<br>(項目選択肢別在庫用縦軸選択肢)">
            <ItemTemplate>
                 <asp:TextBox ID="txtColorName" runat="server" Width="180px" Text='<%# Eval("Color_Name") %>'></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="サイズコード">
            <ItemTemplate>
                 <asp:TextBox ID="txtSizeCode" runat="server" Width="100px" Text='<%# Eval("Size_Code") %>'></asp:TextBox> 
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="カラーコード">
            <ItemTemplate>
                 <asp:TextBox ID="txtColorCode" runat="server" Width="100px" Text='<%# Eval("Color_Code") %>'></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="サイズ正式名称">
            <ItemTemplate>
                 <asp:TextBox ID="txtSizeOfficialName" runat="server" Width="110px" Text='<%# Eval("Size_Name_Official") %>'></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="カラー正式名称">
            <ItemTemplate>
                 <asp:TextBox ID="txtColorOfficialName" runat="server" Width="110px" Text='<%# Eval("Color_Name_Official") %>'></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="JANコード">
            <ItemTemplate>
                 <asp:TextBox ID="txtJanCode" runat="server" Text='<%# Eval("JAN_Code") %>' Width="130px"></asp:TextBox>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty-flag">
            <ItemTemplate>
                <%-- <asp:TextBox ID="txtJanCode" runat="server" Text='<%# Eval("JAN_Code") %>' Width="130px"></asp:TextBox>--%>
                <asp:DropDownList runat="server" ID="ddlQtyFlag" OnSelectedIndexChanged="ddlQtyFlag_SelectedIndexChanged" AutoPostBack="true" Width="70px">
                    <%--<asp:ListItem Selected="True">--Selected--</asp:ListItem>--%>
                    <asp:ListItem Value="1">無限</asp:ListItem>
                    <asp:ListItem Value="2">手入力</asp:ListItem>
                    <asp:ListItem Value="3">完売（0)</asp:ListItem>
                </asp:DropDownList>
            </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="在庫数">
            <ItemTemplate>
                 <asp:TextBox ID="txtQty" runat="server" Text='<%# Eval("Quantity") %>' Width="100px" onkeypress="return CheckInt(event);"></asp:TextBox>
                 <%--<asp:RegularExpressionValidator ID="RegularExpressionValidator1" style="color:Red" ControlToValidate="txtQty" runat="server" ErrorMessage="Only Numbers" ValidationExpression="\d+"></asp:RegularExpressionValidator>--%>
            </ItemTemplate>
            <FooterStyle HorizontalAlign="Right" />
                <FooterTemplate>
                    <asp:Button ID="btnAddNewRow" runat="server" Text="Add New Row" OnClick="btnAddNewRow_Click"/>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"  CausesValidation="true"/>
                    <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click"/>
                </FooterTemplate>

             </asp:TemplateField>
        </Columns>

</asp:gridview>
    </div>
    </form>
</body>
</html>
