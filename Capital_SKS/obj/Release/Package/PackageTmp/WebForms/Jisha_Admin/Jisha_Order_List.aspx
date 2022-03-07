<%@ Page Title="" Language="C#" MasterPageFile="~/Jisha_Admin_Master.Master" AutoEventWireup="true" CodeBehind="Jisha_Order_List.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Order_List" %>
<%@ Register Src="~/UCGrid_Paging.ascx" TagPrefix="uc" TagName="UCGrid_Paging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
	<link href="../../Styles/Jisha_base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Jisha_common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Jisha_style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="CmnContents">
	<div id="ComBlock">
	
	<div class="orderSet">
	<h1>注文一覧</h1>

	<asp:GridView runat="server" ID="gvOrderList" AutoGenerateColumns="false" CssClass="tableSet" PageSize="10" AllowPaging="true">
		<Columns>
			<asp:TemplateField>
				<HeaderTemplate>
					<asp:Label runat="server" ID="lblDetailHeader"></asp:Label>
				</HeaderTemplate>
				<ItemTemplate>
					<asp:Button runat="server" ID="btnDetail" Text="詳細" OnClick="btnDetailClick" />
					<asp:Label runat="server" ID="lblOrderID" Text='<%#Eval("Order_ID") %>' Visible="false"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField>
				<HeaderTemplate>
					<asp:Label runat="server" ID="lblOrderDateHeader" Text="注文日時"></asp:Label>
				</HeaderTemplate>
				<ItemTemplate>
					<asp:Label runat="server" ID="lblOrderDate" Text='<%#Eval("Order_Date") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField>
				<HeaderTemplate>
					<asp:Label runat="server" ID="lblOrderNameHeader" Text="注文者氏名"></asp:Label>
				</HeaderTemplate>
				<ItemTemplate>
					<asp:Label runat="server" ID="lblOrderName"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField>
				<HeaderTemplate>
					<asp:Label runat="server" ID="lblPayMethodHeader" Text="決済方法"></asp:Label>
				</HeaderTemplate>
				<ItemTemplate>
					<asp:Label runat="server" ID="lblPayMethod" Text='<%#Eval("Payment_Method") %>' ></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField>
				<HeaderTemplate>
					<asp:Label runat="server" ID="lblOrderAmountHeader" Text="注文金額"></asp:Label>
				</HeaderTemplate>
				<ItemTemplate>
					<asp:Label runat="server" ID="lblOrderAmount" Text='<%#Eval("Total") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>
	    <PagerSettings Visible="False" />
	</asp:GridView>


	</div><!--inlineSet-->

</div><!--ComBlock-->
</div><!--CmnContents-->

<%--<div class="btn pageNav" >

<uc:UCGrid_Paging runat="server" ID="gp" />

</div>--%>

</asp:Content>
