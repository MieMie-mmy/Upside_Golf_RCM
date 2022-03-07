<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MallData_Import.aspx.cs" Inherits="ORS_RCM.WebForms.MallData_Import" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<script type="text/javascript">
    $(function () {
        $('#btnDownload').click(function () {
            var txt = document.getElementById("<%=txtItemCode.ClientID%>").value;
            var chkracket = document.getElementById("<%=chkRacket.ClientID%>").checked;
            var chkluckpiece = document.getElementById("<%=chkLuckpiece.ClientID%>").checked;
            var chksport = document.getElementById("<%=chkSport.ClientID%>").checked;
            var chkbaseball = document.getElementById("<%=chkBaseball.ClientID%>").checked;
            divdownloading.style.visibility = "visible";
            divbody.style.display = "none";
           
            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "MallData_Import.aspx/Download",
                dataType: "json",
                data: "{'ItemCode':'" + txt + "','Racket':'" + chkracket + "','LuckPiece':'" + chkluckpiece + "','SportPlaza':'" + chksport + "','BaseballPlaza':'" + chkbaseball + "'}",
                success: function (data) {
                    var obj = data.d;
                    if (obj == 'true') {
                        alert("download Successfully!");
                        divbody.style.display = "block";
                        divdownloading.style.visibility = "hidden";
                    }
                    else {
                        $('#lblmsg').html('Download Failed! Check system error log!');
                    }
                },
                error: function (result) {
                    alert(result);
                }
            });
        });
    });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br /><br /><br /><br /><br /><br />
<div id="divbody">
<asp:Label runat="server" ID="lbl" Text="*****Download Mall Data by Item Code From RakutenMall Only*****" style="color:Red;"></asp:Label>
<br /><br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label runat="server" ID="lblItemCode" Text="Item Code" style="vertical-align:top;"></asp:Label>
<asp:TextBox runat="server" ID="txtItemCode"  Width="200px" Height="100px" TextMode="MultiLine"></asp:TextBox>
<br /><br />
<asp:CheckBox runat="server" ID="chkRacket" />Racket
<asp:CheckBox runat="server" ID="chkLuckpiece" />LuckPiece
<br />
<asp:CheckBox runat="server" ID="chkSport" />Sport Plaza
<asp:CheckBox runat="server" ID="chkBaseball" />Baseball Plaza
<br /><br />
<input type="button" value="Download" id="btnDownload" style="margin-left:200px;" />
<br />
</div>
<div id="divdownloading" style="margin-left:350px; visibility:hidden">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
Downloading Please Wait....
<br />
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Image runat="server" ID="imgDownload" Height="101px"
        ImageUrl="~/images/downloading1.GIF" Width="229px" />
 </div>
</asp:Content>
