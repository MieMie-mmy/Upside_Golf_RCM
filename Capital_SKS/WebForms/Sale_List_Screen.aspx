<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Sale_List_Screen.aspx.cs" Inherits="ORS_RCM.WebForms.Sale_List_Screen" %>

<%@ Register src="../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link rel="stylesheet" href="../../Styles/base.css" />
    <link rel="stylesheet" href="../../Styles/common.css" />
    <link rel="stylesheet" href="../../Styles/manager-style.css" />
    <link rel="stylesheet" href="../../Styles/promotion.css " />

    <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
    <link href="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
    <link href="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" />

    <script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>

    <title>ショップ別売上一覧画面</title>

    <%--<link href="../../Styles/order.css" rel="stylesheet" type="text/css" />--%>
    <script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>

    <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
    <link href="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function doClick(buttonName, e) {
            //the purpose of this function is to allow the enter key to 
            //point to the correct button to click.
            var key;

            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 13) {
                //Get the button the user wants to have clicked
                var btn = document.getElementById(buttonName);
                if (btn != null) { //If we find the button click it
                    btn.click();
                    event.keyCode = 0
                }
            }
        }
    </script>
    <script type="text/javascript">
        window.document.onkeydown = function (e) {
            if (!e) e = event;
            if (e.keyCode == 27) {
                document.getElementById("<%=txtFromDate.ClientID%>").value = null;
              document.getElementById("<%=txtToDate.ClientID%>").value = null;
          }
      }
    </script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtFromDate]").datepicker({
                    showOn: 'button',
                    buttonImageOnly: true,
                    buttonImage: '../images/calendar.gif',
                    dateFormat: 'dd/M/yy',
                    yearRange: "2013:2030",
                });
            });
            $(function () {
                $("[id$=txtToDate]").datepicker({
                    showOn: 'button',
                    buttonImageOnly: true,
                    buttonImage: '../images/calendar.gif',
                    dateFormat: 'dd/M/yy',
                    yearRange: "2013:2030",
                });
            });
        }

    </script>
    <style>
        .margin{
            margin-bottom:3px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CmnContents">
        <div id="ComBlock">
            <div class="setListBox inlineSet iconSet iconList" style="margin-top: 80px">
                <h1>ショップ別売上一覧画面</h1>
                <div class="ordCmnSet resetValue searchBox">
                    <h2>ショップ別売上の検索</h2>
                    <asp:UpdatePanel ID="UPanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
                                <dl style="width: initial; float: left; padding-left: 20px;">
                                    <dt>注文日時</dt>
                                    <dd class="cal" style="width: initial;">
                                        <asp:TextBox ID="txtFromDate" runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Width="16px" Height="15px" CssClass="margin"
                                            ImageUrl="~/Styles/clear.png" ImageAlign="AbsBottom"
                                            OnClick="ImageButton1_Click" />
                                        &nbsp; ~
                                        <asp:TextBox ID="txtToDate" runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton2" runat="server" Width="16px" Height="15px" CssClass="margin"
                                            ImageUrl="~/Styles/clear.png" ImageAlign="AbsBottom"
                                            OnClick="ImageButton2_Click" />
                                    </dd>
                                    <dt>対象ショップ</dt>
                                    <dd>
                                        <asp:ListBox ID="lstShop" runat="server" Height="50px" SelectionMode="Multiple">
                                            <asp:ListItem Value="paintandtool" Text="paintandtool"></asp:ListItem>
                                            <asp:ListItem Value="ペイントアンドツール" Text="ペイントアンドツール"></asp:ListItem>
                                        </asp:ListBox>
                                    </dd>
                                </dl>
                                <p>
                                    <asp:Button runat="server" ID="btnSearch" Text="検 索" OnClick="btnSearch_Click" Width="113px" />
                                </p>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ImageButton1" />
                            <asp:AsyncPostBackTrigger ControlID="ImageButton2" />
                            <asp:PostBackTrigger ControlID="btnSearch" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <asp:UpdatePanel ID="UPGrid" runat="server">
                    <ContentTemplate>
                        <div class="ordCmnSet resetValue editBox iconSet2" style="background: transparent;">
                            <div class="listTableOver">
                                <p class="itemPage">
                                    <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                                        <asp:ListItem>5</asp:ListItem>
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                    </asp:DropDownList>
                                </p>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:GridView runat="server" AutoGenerateColumns="False" ID="gvSale_list" ShowHeaderWhenEmpty="true"
                                                Width="100%" AllowPaging="True" EmptyDataText="There is no data to display."
                                                OnPageIndexChanging="gvSale_list_PageIndexChanging" CssClass="listTable"
                                                OnRowCommand="gvSale_list_RowCommand">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="注文日時">
                                                        <HeaderStyle Width="50px" />
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblEmailDate" Text='<%#Eval("Email_Date") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="在硬数">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblOrder_Count" Text='<%#Eval("OrderQty") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="売上金額">
                                                        <HeaderStyle Width="70px" />
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblOrder_Amount" Text='<%#Eval("Amount") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <HeaderStyle Width="50px" />
                                                        <ItemTemplate>
                                                            <%--CommandArgument='<%#Eval("shop") %>'--%>
                                                            <asp:Button ID="btnEdit" runat="server" Text="商品別注文一覧" CommandName="DataEdit" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerSettings Visible="False" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlpage" />
                        <asp:AsyncPostBackTrigger ControlID="gvSale_list" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
        <asp:HiddenField runat="server" ID="hdfFromDate" />
        <asp:HiddenField runat="server" ID="hdfToDate" />
        <asp:HiddenField runat="server" ID="hdfSearch" />
        <div class="btn">
            <uc1:UCGrid_Paging ID="gp2" runat="server" />
        </div>
</asp:Content>
