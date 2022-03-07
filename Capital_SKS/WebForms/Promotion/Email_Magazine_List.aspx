<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Email_Magazine_List.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Email_Magazine_List" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../Styles/promotion_base.css" rel="stylesheet" />
<link href="../../Styles/promotion_common.css" rel="stylesheet" />
<link href="../../Styles/promotion_manager_style.css" rel="stylesheet" />
<link href="../../Styles/promotion_promotion.css" rel="stylesheet" />
 <link href="../../Styles/promotion_pagesno.css" rel="stylesheet" />
 <link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
 <script src="../../Scripts/calendar1.js" type="text/javascript"></script>

 <script type="text/javascript">
     window.document.onkeydown = function (e) {
         if (!e) e = event;
         if (e.keyCode == 27) {
             document.getElementById("<%=txtMagazineID.ClientID%>").value = null;
             document.getElementById("<%=txtMagazineName.ClientID%>").value = null;
             document.getElementById("<%=txtDeliveryDate.ClientID%>").value = null;
             document.getElementById("<%=txtCampaignID.ClientID%>").value = null;

             var drp1 = document.getElementById("<%=lstTarget_Shop.ClientID%>");
           
             drp1.selectedIndex = -1;
            
         }
     }
</script>

<script type="text/javascript" language="javascript">
function pageLoad(sender, args) {
$(document).ready(function () {
$("#<%=txtDeliveryDate.ClientID %>").datepicker(
{ 
showOn: 'button',
dateFormat: 'yy/mm/dd ',
buttonImageOnly: true,
buttonImage:'../../images/calendar.gif',
changeMonth: true,
changeYear: true,
yearRange: "1900:2030" ,
}
);
$(".ui-datepicker-trigger").mouseover(function () {
$(this).css('cursor', 'pointer');
});

});

}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdfSearch" runat="server" />
    <asp:HiddenField ID="hdfDeliveryDate" runat="server" />
 <%--   <p id="toTop"><a href="#divtop">▲TOP</a></p>--%>
<div id="CmnContents">
	<div id="ComBlock" style="margin-top: 60px;">
		<div class="setListBox inlineSet iconSet iconList">
		<h1>メールマガジン一覧</h1>
		<div class="prmCmnSet prmMail resetValue searchBox">
			<h2>メールマガジン検索</h2>
               <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
			<div class="block1">
         
				<dl>
					<dt>メールマガジンID</dt>
					<dd>  <asp:TextBox runat="server" ID="txtMagazineID" Width="120px"/></dd>
					
					<dt>メールマガジン名</dt>
					<dd>   <asp:TextBox runat="server" ID="txtMagazineName" Width="274px"/></dd>
					
					<dt>配信予定</dt>
					<dd>      <asp:TextBox ID="txtDeliveryDate" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                                  <asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom" />
                    </dd>
					
					<dt>キャンペーンID</dt>
					<dd><asp:TextBox runat="server" ID="txtCampaignID" Width="274px"/></dd>
				</dl>
			</div>

            <div class="block2">
				<dl>		
					<dt>対象ショップ</dt>
					<dd>
					   
					    <asp:ListBox ID="lstTarget_Shop" runat="server" SelectionMode="Multiple"></asp:ListBox>
					   
					</dd>
				</dl>
			</div>
            <p><asp:Button runat="server" ID="btnSearch" Text="検 索" Width="200px" onclick="btnSearch_Click" /></p>
			  </asp:Panel>
		</div><!-- /.searchBox -->
          
	</div><!--setListBox-->
  
    <div class="operationBtn">
			<p>
			<asp:Button ID="btnNew" runat="server" Text="メールマガジンの追加" OnClick="btnNew_Click" 
                                       BackColor="#F2F2F2"/>
			</p>

            <p class="itemPage">
				<asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" onselectedindexchanged="ddlpage_SelectedIndexChanged">
			<asp:ListItem>30</asp:ListItem>
			<asp:ListItem>50</asp:ListItem>
			<asp:ListItem>100</asp:ListItem>
			</asp:DropDownList>
			</p>
		</div><!-- /.operationBtn -->
       
       <div class="prmCmnSet resetValue listBox">
         <asp:GridView ID="gvEmailMagazine" runat="server" CellPadding="4" 
               ForeColor="#333333" GridLines="None" CssClass="listTable" 
        EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True"  
               AutoGenerateColumns="false"  Width="100%"  
               OnRowCommand="gvEmailMagazine_RowCommand" 
               OnRowdatabound="gvEmailMagazine_RowDataBound" 
               onpageindexchanging="gvEmailMagazine_PageIndexChanging" PageSize="30" 
               AllowPaging="True">
    <Columns>
<asp:TemplateField HeaderText="">
<ItemTemplate>
<asp:Button runat="server" ID="btnEdit" Text="編集" Width="80px"
CommandName="DataEdit" CommandArgument='<%# Eval("ID") %>' /><br />
<asp:Button runat="server" ID="btnDisplay" Text="制作" Width="80px"
CommandName="DataDisplay" CommandArgument='<%# Eval("ID") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="メールマガジンID">
<ItemTemplate>
<asp:Label runat="server" ID="Label1" Text='<% #Eval("Mail_magazine_ID")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="メールマガジン名">
<ItemTemplate>
<asp:Label runat="server" ID="Label3" Text='<% #Eval("Mail_magazine_Name")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="配信予定日">
<ItemTemplate>
<asp:Label runat="server" ID="Label2" Text='<% #Eval("Delivery_date","{0:yyyy/MM/dd  hh:mm}")%>'/>
</ItemTemplate>
</asp:TemplateField>

<asp:TemplateField HeaderText="対象ショップ" >
<ItemTemplate>
<asp:Label ID="lblShopName" runat="server" Text='<%#Eval("Shop_ID") %>'></asp:Label>
 </ItemTemplate>
</asp:TemplateField>
<%--  <asp:TemplateField HeaderText="ID" Visible = "false">
            <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>--%>
   <asp:TemplateField HeaderText="キャンペーンID" >
            <ItemTemplate>
                    <asp:Label ID="lblCampaign" runat="server" Text='<%#Eval("CampaignID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
   </Columns>
             <PagerSettings Visible="False" />
    </asp:GridView>

   </div>
        </div><!--ComBlock-->
</div><!--CmnContents-->
	<uc1:ucgrid_paging ID="gp" runat="server" visible="true" />
</asp:Content>



