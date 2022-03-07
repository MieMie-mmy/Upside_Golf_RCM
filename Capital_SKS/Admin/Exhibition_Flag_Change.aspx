<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exhibition_Flag_Change.aspx.cs" Inherits="ORS_RCM.Admin.Exhibition_Flag_Change" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
    
 <script type="text/javascript">
     function pageLoad(sender, args) {
         $(function () {
             $("[id$=txtStartDate]").datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 buttonImage: '../../images/calendar.gif',
                 dateFormat: 'dd/M/yy',
                 yearRange: "2013:2030"
             });
         });
         $(function () {
             $("[id$=txtEndDate]").datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 buttonImage: '../../images/calendar.gif',
                 dateFormat: 'dd/M/yy',
                 yearRange: "2013:2030"
             });
         });
     }
	</script>
    <script type="text/javascript">
        function Value(ctrl) {
            if (ctrl.id == "MainContent_txtItemCode") {
                document.getElementById('<%=itemcode.ClientID%>').value = ctrl.value;
            }           
           <%-- else if (ctrl.id == "MainContent_ddlExhibitor") {
                document.getElementById('<%=exhibitor.ClientID%>').value = ctrl.value;
            }--%>
            if (ctrl.id == "MainContent_ddlMall") {
                document.getElementById('<%=mall.ClientID%>').value = ctrl.value;
            }
            TextChange();
        }
        function TextChange() {
            var itemcode = document.getElementById("<%=itemcode.ClientID%>").value;           
            <%--var exhibitor = document.getElementById("<%=exhibitor.ClientID%>").value;--%>
            var mall = document.getElementById("<%=mall.ClientID%>").value;
            if ((itemcode != '' && itemcode != null) && (mall != '' && mall != null))
            {
                document.getElementById('<%=btnChangeFlag.ClientID%>').disabled = false;
                __doPostBack();
            }
            else
                document.getElementById('<%=btnChangeFlag.ClientID%>').disabled = true;
        }
        function BTNEnable() {
            document.getElementById('<%=btnChangeFlag.ClientID%>').disabled = true;
        }
    </script>
    <script type="text/javascript">
        function ddlpage_change() {
            document.forms[0].target = "";
        }
    </script>
    <script type="text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("ARE YOU SURE TO CHANGE THE EXHIBITION FLAGS")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="itemcode" runat="server" />
    <asp:HiddenField ID="startdate" runat="server" />
    <asp:HiddenField ID="enddate" runat="server" />
    <asp:HiddenField ID="exhibitor" runat="server" />
    <asp:HiddenField ID="mall" runat="server" />
    <body>
        <div id="CmnContents">
            <div id="ComBlock">
                <div class="setListBox inlineSet iconSet iconList">
                    <h1>Update Exhibition Flag</h1>
                    <div class="itemCmnSetKnr itemCmnSet resetValue searchBox">
                        <dl style="padding-top:30px;padding-bottom:20px;">
                            <dt title="Item Number">商品番号</dt>
                            <dd><asp:TextBox ID="txtItemCode" runat="server" TextMode="MultiLine" onchange="Value(this)"></asp:TextBox></dd>                        
                            <dt>出品者</dt>
                            <dd><asp:DropDownList ID="ddlExhibitor" runat="server" onchange="Value(this)"></asp:DropDownList></dd>
                            <dt>出品モール</dt>
                            <dd><asp:DropDownList ID="ddlMall" runat="server" onchange="Value(this)"></asp:DropDownList></dd>
                            <p><asp:Button ID="btnChangeFlag" runat="server" Text="Update_Flag" OnClientClick="Confirm()" OnClick="btnChangeFlag_Click" Width="150px"
                                ToolTip="Update Exhibition Flags" Enabled="false" /></p>
                        </dl>
                    </div>
                </div>
            </div>
        </div>
    </body>
</asp:Content>
