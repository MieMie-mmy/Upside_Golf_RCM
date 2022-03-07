<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SKS_DB_Backup.aspx.cs" Inherits="ORS_RCM.WebForms.SKS_DB_Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

  <script type="text/javascript">
      $(function () {
          $('#btnBackup').click(function () {
                  $.ajax({
                      type: "POST",
                      contentType: "application/json; charset=utf-8",
                      url: "SKS_DB_Backup.aspx/BackupSKS",
                      dataType: "json",
                      success: function (data) {
                          var obj = data.d;
                          if (obj == 'true') {
                              alert("Backuped Successfully");
                          }
                      },
                      error: function (result) {
                          alert("Error");
                      }
                  });
              }
          )
      });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br />
      <br />
        <br />
    <b>Database Backup</b>
<table>
<tr>
<td>
<%--<asp:Button runat="server" ID="btnBackup" Text="Backup" onclick="btnBackup_Click" />--%>
<input type = "button" id="btnBackup" value="Backup" />
</td>
</tr>
<tr>
<td>
<asp:Label runat="server" ID="lblMessage"></asp:Label>
</td>
<td>
    &nbsp;</td>
</tr>
</table>
</asp:Content>
