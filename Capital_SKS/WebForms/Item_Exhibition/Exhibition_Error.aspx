<%@ Page Title="商品管理システム＜ユーザ一覧＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exhibition_Error.aspx.cs" Inherits="ORS_RCM.WebForms.Item_Exhibition.Exhibition_Error" %>
<%@ Register Src="~/UCGrid_Paging.ascx" TagPrefix="uc" TagName="UCGrid_Paging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <body>
        <div id="CmnWrapper">
            <div id="CmnContents">
                <div id="ComBlock">
                    <div class="setListBox inlineSet iconSet iconUser">
                        <h1>お知らせ</h1>
                        <div id="messageBox">
                            <asp:GridView runat="server" ID="gvExhibitionError" AllowPaging="True" AutoGenerateColumns="False"
                                OnRowDataBound="gvExhibitionError_RowDataBound" EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True"
                                OnPageIndexChanging="gvExhibitionError_PageIndexChanging" PageSize="30">
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="Label2" runat="server" Text="日時"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblcdate" Text='<%#Eval("Created_Date","{0:yyyy/MM/dd  hh:mm}")%>'></asp:Label>
                                            <itemstyle width="200px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Item_Code" HeaderText="商品番号" Visible="false" />
                                    <asp:TemplateField Visible="true">
                                        <HeaderTemplate>
                                            <asp:Label runat="server" Text="メッセージ"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblCheckType" Text='<%#Eval("Check_Type")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <HeaderTemplate>
                                            <asp:Label ID="Label1" runat="server" Text="Check Type Count"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblckcount" Text='<%#Eval("ck")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <h1>出品結果情報</h1>
                        <div class="exhibitInfo">
                            <asp:GridView runat="server" ID="gdvExhibitInfo" AllowPaging="true" AutoGenerateColumns="false" OnRowCreated="gdvExhibitInfo_RowCreated"
                                OnPageIndexChanging="gdvExhibitInfo_PageIndexChanging" PageSize="30">
                                <Columns>
                                    <asp:TemplateField HeaderText="日付">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDate" Text='<%#Eval("CSV_ExportedDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="R_PaintTool">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl1" Text='<%#Eval("R_racket") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Y_PaintTool">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl6" Text='<%#Eval("Y_racket") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="W_PaintTool">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl4" Text='<%#Eval("W_racket") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ORS自社">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbl5" Text='<%#Eval("TennisClassic") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="新規">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblnew" Text='<%#Eval("Export_statusNew") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="更新">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblupdate" Text='<%#Eval("Export_statusUpdate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="削除">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lbldelete" Text='<%#Eval("Export_statusDelete") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="出品結果エラー">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblErrcheck" Text='<%#Eval("ErrorCheckCount") %>' Width="45px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="APIチェック">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblAPIcheck" Text='<%#Eval("APICheckCount") %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>

                        <h1>注文状況</h1>
                        <asp:UpdatePanel ID="UPanel" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Button runat="server" ID="btnOrderCount" OnClick="btnOrderCount_Click" Text="日別注文数" Width="70px" /></td>
                                            <td>
                                                <asp:Button runat="server" ID="btnTotalAmount" OnClick="btnTotalAmount_Click" Text="日別受注金額" Width="80px" /></td>
                                            <td>
                                                <asp:Button runat="server" ID="btnWaitingStatus" OnClick="btnWaitingStatus_Click" Text="期日出品待ち" Width="90px" /></td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="exhibitInfo">
                                    <asp:GridView ID="gdvSalePrice" runat="server" ShowFooter="true" ShowHeaderWhenEmpty="true" EmptyDataText="There is no data to display!">
                                    </asp:GridView>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnOrderCount" />
                                <asp:AsyncPostBackTrigger ControlID="btnTotalAmount" />
                                <asp:AsyncPostBackTrigger ControlID="btnWaitingStatus" />
                                <asp:AsyncPostBackTrigger ControlID="gdvSalePrice" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <!--setListBox-->
                </div>
                <!--ComBlock-->
            </div>
            <!--CmnContents-->
        </div>
        <!--CmnWrapper-->
    </body>
    <div class="btn">
        <uc:UCGrid_Paging runat="server" ID="gp" Visible="false" />
    </div>
</asp:Content>
