<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Log_Data_Delete.aspx.cs" Inherits="ORS_RCM.Admin.Log_Data_Delete" %>
<asp:content id="Content1" contentplaceholderid="HeadContent" runat="server">
<link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
     function pageLoad(sender, args) {
         $(function () {
             $("[id$=txtfromdate]").datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 showsTime: true,
                 buttonImage: '../../images/calendar.gif',
                 dateFormat: 'dd/M/yy',
                 yearRange: "2013:2030",
                 button: ".next()",
                 align: "Middle"
             });
         });
         $(function () {
             $("[id$=txttodate]").datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 showsTime: true,
                 buttonImage: '../../images/calendar.gif',
                 dateFormat: 'dd/M/yy',
                 yearRange: "2013:2030",
                 button: ".next()"
             });
         });
     }
    </script>

    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("ARE YOU SURE TO DELETE DATA FROM LOG TABLES")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    
    <script type="text/javascript">
        function Value(ctrl) {
            if (ctrl.id == "MainContent_txtfromdate")
                document.getElementById('<%=hdfFromDate.ClientID%>').value = ctrl.value;
            else if (ctrl.id == "MainContent_txttodate")
                document.getElementById('<%=hdfToDate.ClientID%>').value = ctrl.value;
            else if (ctrl.id == "MainContent_ddlLog")
                document.getElementById('<%=hdfTable.ClientID%>').value = ctrl.value;
            var fromdate = document.getElementById('<%=hdfFromDate.ClientID%>').value;
            var todate = document.getElementById('<%=hdfToDate.ClientID%>').value;
            var table = document.getElementById('<%=hdfTable.ClientID%>').value;
            if ((fromdate != '' && fromdate != null) && (todate != '' && todate != null) && table != 0)
            {
                document.getElementById('<%=btnDelete.ClientID%>').disabled = false;
                __doPostBack();
            }
            else
                document.getElementById('<%=btnDelete.ClientID%>').disabled = true;
        }
        function BTNEnable() {
            document.getElementById('<%=btnDelete.ClientID%>').disabled = true;
        }
    </script>
</asp:content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdfFromDate" runat="server" />
	<asp:HiddenField ID="hdfToDate" runat="server" />
    <asp:HiddenField ID="hdfTable" runat="server" />
    <body>
        <div id="CmnContents">
            <div id="ComBlock">
                <div class="setListBox inlineSet iconSet iconList">
                    <h1>Log_Data_Delete</h1>
                    <div class="itemCmnSetKnr itemCmnSet resetValue searchBox">
                        <h3 style="color:Red;padding-left:20px;">*****Delete Data From LOG Tables*****</h3>
                        <dl style="padding-top:30px;padding-bottom:20px;">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <dt>From Date</dt>
                                    <dd class="cal" style="width: 200px;"><asp:TextBox ID="txtfromdate" runat="server" ReadOnly="True" onchange="Value(this)"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px" ImageUrl="~/Styles/clear.png" OnClientClick="BTNEnable()" OnClick="fromdate_Click" ImageAlign="Baseline" />
                                        &nbsp; ~</dd>
                                    <dt>To Date</dt>
                                    <dd class="cal" style="width: 200px;"><asp:TextBox ID="txttodate" runat="server" ReadOnly="True" onchange="Value(this)"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton2" runat="server" Width="15px" Height="15px" ImageUrl="~/Styles/clear.png" OnClientClick="BTNEnable()" OnClick="todate_Click" ImageAlign="Baseline" /></dd>
                                    <dt>Log List</dt>
                                    <dd><asp:DropDownList ID="ddlLog" runat="server" onchange="Value(this)">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="1">Exhibtion_Log</asp:ListItem>
                                            <%-- <asp:ListItem Value="2">Promotion_Log</asp:ListItem>--%>
                                            <asp:ListItem Value="3">Error_Log</asp:ListItem>
                                            <asp:ListItem Value="4">Import_Log</asp:ListItem>
                                            <asp:ListItem Value="5">Item_ExportQ_Log</asp:ListItem>
                                        </asp:DropDownList></dd>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ImageButton1" />
                                    <asp:AsyncPostBackTrigger ControlID="ImageButton2" />
                                    <asp:AsyncPostBackTrigger ControlID="ddlLog" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <p><asp:Button ID="btnDelete" runat="server" Enabled="false" OnClientClick="Confirm()" OnClick="btnDelete_OnClick" Text="Delete" /></p>
                        </dl>
                    </div>
                </div>
            </div>
        </div>
    </body>
</asp:Content>
