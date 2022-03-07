<%@ Page Title="ラケットプラザ＜管理画面／注文データダウンロード＞" Language="C#" MasterPageFile="~/Jisha_Admin_Master.Master" AutoEventWireup="true" CodeBehind="Jisha_Order_Download.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Order_Download" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
  <link href="../../Styles/Jisha_base.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/Jisha_common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/Jisha_style.css" rel="stylesheet" type="text/css" />
  <link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
   <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
 <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<%--	<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />--%>
    
      
  <script type="text/javascript">
           function pageLoad(sender, args) {
               $(function () {
                   $("[id$=txtexdatetime1]").datepicker({
                       showOn: 'button',
                       buttonImageOnly: true,
                       buttonImage: '../Styles/calendar.gif',
                       dateFormat: 'dd/M/yy',
                       yearRange: "2013:2030"
                   });
               });
               $(function () {
                   $("[id$=txtdatetime2]").datepicker({
                       showOn: 'button',
                       buttonImageOnly: true,
                       buttonImage: '../Styles/calendar.gif',
                       dateFormat: 'dd/M/yy',
                       yearRange: "2013:2030"
                   });
               });
           }
	</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="CmnWrapper">
<div id="CmnContents">
	<div id="ComBlock">
        <asp:HiddenField ID="hdfFromDate" runat="server" />
        <asp:HiddenField ID="hdfToDate" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
	<div class="orderSet">
	<h1>注文データダウンロード</h1>
	
	<div class="orderDL">
    	<form>
	<p>
        <asp:RadioButton ID="rdodownload" runat="server"  Text="  前回のダウンロード以降の注文データ"/>
  
        <asp:Button ID="btndownload" runat="server" Text="ダウンロード" Height="26px" Width="74px" />


    <asp:UpdatePanel runat="server" ID="uppnl1">
<ContentTemplate>
        <asp:RadioButton ID="rdodownload2" runat="server"  />
        
<asp:TextBox ID="txtexdatetime1" runat="server" ReadOnly="True" Width="118px"   Height="25px"></asp:TextBox>
<asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom" /> &nbsp; ～
				<asp:TextBox ID="txtdatetime2" runat="server" ReadOnly="True" Width="118px"   Height="25px"></asp:TextBox>
				<asp:ImageButton ID="ImageButton2" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton2_Click" ImageAlign="AbsBottom" />

    
    の注文データ
        <asp:Button ID="btndownload2" runat="server" Text="ダウンロード"  Height="26px"  Width="74px"/>
 </ContentTemplate>
</asp:UpdatePanel>
       </p>
      <table class="tableSet">
		<tbody>
        <tr>
        <asp:GridView ID="gvorder" runat="server" CssClass="tableSet" 
            AutoGenerateColumns="False" EmptyDataText="There is no data to display!" 
                ShowHeaderWhenEmpty="True">
            
            <Columns>
        
        <asp:TemplateField  HeaderText="注文日時">
            <ItemTemplate>
                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Order_Date" ,"{0:yyyy/MM/dd  hh:mm}")%>'></asp:Label>

            </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="注文件数">
            <ItemTemplate>
                <asp:Label ID="lblOrderCount" runat="server" Text='<%#Eval("ordercount") %>'></asp:Label>
            </ItemTemplate>
            </asp:TemplateField>

              <asp:TemplateField HeaderText="ダウンロード">
            <ItemTemplate>
                <asp:Button ID="btnOrderDownload" OnClick="btnOrderDownload_Click" runat="server" Text="ダウンロード"  Height="26px" Width="74px"/>
                <asp:LinkButton runat="server" ID="lnkDownload" Visible="false" onclick="lnkDownload_Click"></asp:LinkButton>
            </ItemTemplate>
            </asp:TemplateField>
   
            </Columns>
        </asp:GridView>
        </tr>
</tbody>
</table>
</div><!--inlineSet-->

</div><!--ComBlock-->
</div><!--CmnContents-->


</div><!--CmnWrapper-->
</asp:Content>
