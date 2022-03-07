<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product_Directory_Log.aspx.cs" Inherits="Capital_SKS.WebForms.Import.Product_Directory_Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#CmnContents">▲TOP</a></p>
    <div id="CmnContents">
        <div id="ComBlock">
            <div class="setDetailBox iconSet iconLog editBox">
                <div class="dbCmnSet editBox logDb">
               <h2>全商品ディレクトリID</h2>
                    <asp:GridView ID="gdproduct" runat="server" AllowPaging="true" OnPageIndexChanging="gdproduct_PageIndexChanging" PageSize="30" AutoGenerateColumns="false" CssClass="listTable itemCatIpt itemCatLog">
                        <Columns>
                              <asp:BoundField DataField="チェック" HeaderText="チェック">
        <ControlStyle Width="100px" BackColor="#FF6600" ForeColor="#FF3300" />
        <HeaderStyle Width="100px" />
        <ItemStyle Width="100px" ForeColor="#FF3300" />
        </asp:BoundField>
        <asp:TemplateField>
            <ControlStyle Width="150px" />
            <HeaderStyle Width="150px" />
            <ItemStyle Width="150px" />
            <HeaderTemplate>商品番号</HeaderTemplate>
            <ItemTemplate>
                <asp:Label runat="server" ID="lblCtrlID" Text='<%# Eval("Item_Code") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>	
	   <%--<asp:TemplateField>
            <HeaderTemplate>全商品ディレクトリID</HeaderTemplate>
            <ItemTemplate>
                <p><asp:Label  runat="server" ID="lblCategoryName" Text='<%# Eval("Rakuten_CategoryID") %>' ></asp:Label></p>
            </ItemTemplate>
        </asp:TemplateField>	
        <asp:TemplateField>
            <HeaderTemplate>プロダクトカテゴリ</HeaderTemplate>
            <ItemTemplate>
                <p><asp:Label  runat="server" ID="lblCategoryName" Text='<%# Eval("Yahoo_CategoryID") %>' ></asp:Label></p>
            </ItemTemplate>
        </asp:TemplateField>--%>
	<asp:BoundField DataField="ErrMsg" HeaderText="エラー内容" />
		<asp:BoundField DataField="Type" Visible="false" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
