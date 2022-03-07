<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="ORS_RCM.WebForms.Item_Exhibition.OrderDetail" %>
<%--<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderDetail.aspx.cs" Inherits="ORS_RCM.WebForms.Item_Exhibition.OrderDetail" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../Styles/order.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
<link href ="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<%--<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />--%>
	<script type="text/javascript">
		function pageLoad(sender, args) {
			$(function () {
				$("[id$=txtdate]").datepicker({
					showOn: 'button',
					buttonImageOnly: true,
					buttonImage: '../../images/calendar.gif',
					dateFormat: 'yy/mm/dd',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:2030" ,
				});
			});
			$(function () {
				$("[id$=txtdateapproval]").datepicker({
					showOn: 'button',
					buttonImageOnly: true,
					buttonImage: '../../images/calendar.gif',
					dateFormat: 'yy/mm/dd',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:2030" ,
				});
			});
		}
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>
        <h1>Order Details</h1>
        <asp:HiddenField ID="hdfFromDate" runat="server" />
	    <asp:HiddenField ID="hdfToDate" runat="server" />
         <%--<script language="javascript">--%>

         
        	</dd>
				<dt title="Date of approval">承認日</dt>
				<dd class="cal"><asp:TextBox ID="txtdate" runat="server" ReadOnly="True"></asp:TextBox>
                    <asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
				    ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom" /> &nbsp; ~
				<asp:TextBox ID="txtdateapproval" runat="server" ReadOnly="True"></asp:TextBox>
				<asp:ImageButton ID="ImageButton2" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton2_Click" ImageAlign="AbsBottom" />
                </dd>
		    </dl>
    </div>

</asp:Content>