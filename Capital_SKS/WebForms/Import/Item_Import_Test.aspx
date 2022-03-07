<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item_Import_Test.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Item_Import_Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:FileUpload runat="server" ID="uplFile" /><br />
       <asp:Button runat="server" ID="btnImport" Text="ImportMster" 
            onclick="btnImport_Click" />
        <asp:Button runat="server" ID="btnImportSKU" Text="ImportSKU" />
    </div>
    </form>
</body>
</html>
