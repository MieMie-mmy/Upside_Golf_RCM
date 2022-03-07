<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Product_Directory_Confirm.aspx.cs" Inherits="Capital_SKS.WebForms.Import.Product_Directory_Confirm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />
    <title>商品ディレクトリデータインポート</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#CmnContents">▲TOP</a></p>
    <div id="CmnContents">
        <div id="ComBlock">
            <div class="setDetailBox iconSet iconCheck editBox">
                <h1>商品ディレクトリデータインポート</h1>
                <div class="dbCmnSet editBox">
                    <form action="#" method="get">
                        <asp:GridView ID="gdProductDirectory" runat="server" AutoGenerateColumns="false" CssClass="listTable itemCatIpt" 
                            EmptyDataText="No data to display" OnPageIndexChanging="gdProductDirectory_PageIndexChanging" ShowHeaderWhenEmpty="true" AllowPaging="true" PageSize="30" OnRowDataBound="gdProductDirectory_RowDataBound">
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
                <asp:Label runat="server" ID="lblCtrlID" Text='<%# Eval("商品番号") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>	
	   <asp:TemplateField>
            <HeaderTemplate>全商品ディレクトリID</HeaderTemplate>
            <ItemTemplate>
                <p><asp:Label  runat="server" ID="lblCategoryName" Text='<%# Eval("全商品ディレクトリID") %>' ></asp:Label></p>
            </ItemTemplate>
        </asp:TemplateField>	
        <asp:TemplateField>
            <HeaderTemplate>プロダクトカテゴリ</HeaderTemplate>
            <ItemTemplate>
                <p><asp:Label  runat="server" ID="lblCategoryName" Text='<%# Eval("プロダクトカテゴリ") %>' ></asp:Label></p>
            </ItemTemplate>
        </asp:TemplateField>
                                <asp:TemplateField>
            <HeaderTemplate>Wowma Category</HeaderTemplate>
            <ItemTemplate>
                <p><asp:Label  runat="server" ID="lblCategoryName" Text='<%# Eval("Wowma_CategoryID") %>' ></asp:Label></p>
            </ItemTemplate>
        </asp:TemplateField>
                                 <asp:TemplateField>
            <HeaderTemplate>ORS自社 Category</HeaderTemplate>
            <ItemTemplate>
                <p><asp:Label  runat="server" ID="lblCategoryName" Text='<%# Eval("ORS自社_CategoryID") %>' ></asp:Label></p>
            </ItemTemplate>
        </asp:TemplateField>

		<asp:BoundField DataField="Type" Visible="false" />  
                            </Columns>
                        </asp:GridView>
                        <div class="btn">
                            <asp:Button ID="btnUpdate" runat ="server" Text="更 新" OnClick="btnUpdate_Click" />
                        </div>
                    </form>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
