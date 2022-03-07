<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Status_Change_Confirmation.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Status_Change_Confirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox iconSet iconCheck">
	<h1>出品確認</h1>
    <p class="attText" runat="server" id ="exb" visible="false">If Confirm button click,SKS Status will Change  出品待ち To ページ制作</p>
	<!-- Delete -->
	<p class="attText" runat="server" id="deleteexb" visible="false">If Confirm button click,SKS Status will Change  ページ制作 and Shop Status will change 未掲載 </p>
   <div class="itemExb">
	<form action="#" method="get">
    <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" CssClass="listTable" OnRowDataBound="gvlist_OnRowDataBound">
    <Columns>
    <asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
<HeaderTemplate>SKS <br /> ST</HeaderTemplate>
<ItemTemplate>
                    <p id="Ppage" runat="server" class="page"></p>
					<p id="PWaitSt" runat="server" class="wait1"></p>
					<p id="PWaitL" runat="server" class="waitL"></p>
                    <p id="PExhibit" runat="server" class="exhibit"></p>
					<p id="POkSt" runat="server" class="ok1"></p>
<asp:Label ID="lblSKUStatus" runat="server" Text ='<%#Eval("Export_Status") %>' Visible="false"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Top">
<HeaderTemplate>Shop <br /> ST</HeaderTemplate>
<ItemTemplate>
                    <p id="PWait" runat="server" class="wait"></p>
					<p id="POk" runat="server" class="ok"></p>
					<p id="PDel" runat="server" class="del"></p>
                    <p id="PInactive" runat="server" class="deactive"></p>
<asp:Label ID="lblshop" runat="server" Text ='<%#Eval("Ctrl_ID") %>' Visible="false" />
</ItemTemplate>
</asp:TemplateField>
    <asp:TemplateField HeaderText="商品番号">
    <ItemTemplate>
        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Item_Code")%>' ></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="商品名">
    <ItemTemplate>
        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Item_Name")%>' ></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="カタログ情報">
    <ItemTemplate>
        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Catalog_Information")%>' ></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
      <asp:TemplateField HeaderText="ブランド名">
    <ItemTemplate>
        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Brand_Name")%>' ></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
    </Columns>
    </asp:GridView>
    <div class="btn">
    <input type="submit" id="btnpopup" onclick="this.form.target='" runat="server" style="width:200px" visible="false"  value="確定"/>
    <asp:Button ID="btnSave" runat="server" Text="確定" onclick="btnSave_Click"  />
    </div>
    </form>
	</div>
</div><!--setDetailBox-->
</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
