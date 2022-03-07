<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="New_ItemData_Create.aspx.cs" Inherits="Capital_SKS.WebForms.Item.New_ItemData_Create" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../../Styles/tab-style.css" />
    <link rel="stylesheet" href="../../Styles/base.css" />
    <link rel="stylesheet" href="../../Styles/common.css" />
    <link rel="stylesheet" href="../../Styles/item.css" />
    <link href="css/lightbox.css" rel="stylesheet" />
    <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
    <script src="../../Scripts/jquery.droppy.js" type="text/javascript"></script>
    <style type="text/css">
        div.searchBox dl dd {
            width:auto;
        }
        input[type="submit"] {
            padding:4px 15px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="UPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="CmnWrapper">
                <div id="CmnContents">
                    <div id="ComBlock">
                        <div class="setListBox inlineSet iconSet iconList">
                            <h1>商品登録</h1>
                            <asp:Panel ID="Panel1" runat="server" class="itemCmnSetKnr itemCmnSet resetValue searchBox">
                                <dl>
                                    <dt>商品番号</dt>
                                    <dd>
                                        <asp:TextBox ID="txtItem_Code" runat="server" Width="200px" required="true"></asp:TextBox></dd>
                                    <dt>商品名</dt>
                                    <dd>
                                        <asp:TextBox ID="txtItem_Name" runat="server" Width="200px" TextMode="MultiLine" required="true"></asp:TextBox></dd>
                                    <dt></dt>
                                    <dd>
                                        <asp:Button ID="btnSave" runat="server" Text="追加" OnClick="btnSave_Click" /></dd>
                                </dl>
                            </asp:Panel>
                        </div>
                    </div>
                    <asp:GridView ID="gvItem_List" runat="server" AutoGenerateColumns="false" ShowHeaderWhenEmpty="true" OnRowEditing="gvItem_List_RowEditing"
                        OnRowDeleting="gvItem_List_RowDeleting" OnRowUpdating="gvItem_List_RowUpdating" OnRowCancelingEdit="gvItem_List_RowCancelingEdit" PageSize="30"
                        AllowPaging="true" EmptyDataText="There is no data to display" EnableTheming="False" ForeColor="#333333" GridLines="None" CssClass="managementList listTable">
                        <PagerSettings Visible="False" />
                        <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                        <Columns>
                            <asp:TemplateField HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Eval("ID") %>'></asp:Label>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="商品番号">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem_Code" runat="server" Text='<%# Eval("Item_Code") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtItem_Code" runat="server" Text='<%# Eval("Item_Code") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="商品名">
                                <ItemTemplate>
                                    <asp:Label ID="lblItem_Name" runat="server" Text='<%# Eval("Item_Name") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtItem_Name" runat="server" Text='<%# Eval("Item_Name") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnEdit" runat="server" Text="編集" formnovalidate="formnovalidate" CommandName="Edit" />
                                    <asp:Button ID="btnDelete" runat="server" Text="削除" formnovalidate="formnovalidate" CommandName="Delete" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="btnUpdate" runat="server" Text="更新" formnovalidate="formnovalidate" CommandName="Update" />
                                    <asp:Button ID="btnCancel" runat="server" Text="キャンセル" formnovalidate="formnovalidate" CommandName="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                <div class="btn">
                    <uc1:UCGrid_Paging ID="gp" runat="server" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" />
            <asp:AsyncPostBackTrigger ControlID="gvItem_List" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
