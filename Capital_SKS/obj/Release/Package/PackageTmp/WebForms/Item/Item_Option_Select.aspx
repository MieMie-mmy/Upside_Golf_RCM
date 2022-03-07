<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_Option_Select.aspx.cs" Inherits="ORS_RCM.Item_Option_Select" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript"　language="javascript">
        function SendValue() {
            var label = document.getElementById('<%=txtLabel.ClientID%>').value;
            window.opener.document.getElementById("hdf").value = label;
            window.opener.__doPostBack();
            window.close();
        }
        function ClearValue() {
            opener.document.getElementById("hdfValue").value = "";
            opener.document.getElementById("hdfChoice").value = "";
            opener.document.getElementById("hdfLabel").value = "";
            window.opener.__doPostBack();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <asp:Label  runat="server" ID="Label1" Text="Display Label : " /> 
    <asp:TextBox  runat="server" ID="txtLabel" Width="250px" Placeholder=" Label -> Color "/> <br />
    
    <asp:Label  runat="server" ID="Label2" Text="Display Value : " /> 
    <asp:TextBox  runat="server" ID="txtOption" Width="250px" Placeholder=" Value -> Green,White,Black "/> <br />
    
    <asp:Label  runat="server" ID="Label3" Text="Display Theme : " /> 
    <asp:DropDownList runat="server" ID="ddlTheme" Width="250px" > 
    <asp:ListItem Value="3">--Select--</asp:ListItem>
   <%-- <asp:ListItem Value="2">CheckBox</asp:ListItem>--%>
    <asp:ListItem Value="1">SelectBox</asp:ListItem>
    </asp:DropDownList>
    <br />
  
    <asp:Button runat="server" ID="btnOK" Text="OK" Width="200px" 
            onclick="btnOK_Click" />
  
    </div>
    </form>
</body>
</html>
