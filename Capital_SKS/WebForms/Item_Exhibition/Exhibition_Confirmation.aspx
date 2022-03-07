<%@ Page Title="商品管理システム＜出品確認＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exhibition_Confirmation.aspx.cs" Inherits="ORS_RCM.WebForms.Item_Exhibition.Exhibition_Confirmation" %>
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
    <p class="attText" runat="server" id ="exb" visible="false">下記内容をショップへ出品します。間違いがなければ「確定」ボタンを押してください</p>
	<!-- Delete -->
	<p class="attText" runat="server" id="deleteexb" visible="false">下記内容をショップから削除します。間違いがなければ「確定」ボタンを押してください</p>

       
   <div class="itemExb">
	<form action="#" method="get">


    <asp:GridView ID="gvlist" runat="server" AutoGenerateColumns="False" CssClass="listTable">
    <Columns>
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
      <asp:TemplateField HeaderText="指示書番号">
    <ItemTemplate>
        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Instruction_No")%>'  ></asp:Label>
    </ItemTemplate>
    </asp:TemplateField>
        <asp:TemplateField HeaderText="Permission">
            <ItemTemplate>
                <asp:Label ID="Label6" runat="server" Text='<%#Eval("Permission") %>' style="color:red;"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    </asp:GridView>
 
    <div class="btn">
    <input type="submit" id="btnpopup" onclick="this.form.target" runat="server" style="width:200px" visible="false"  value="確定"/>
    <asp:Button ID="btnSave" runat="server" Text="確定" onclick="btnSave_Click"  />
    </div>
    </form>
	</div>
   
<!-- /CategoryID list -->
	</div><!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
