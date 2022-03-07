<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exhibition_List_Log.aspx.cs" Inherits="ORS_RCM.WebForms.Item_Exhibition.Exhibition_List_Log" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
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
                document.getElementById("<%=txtcode.ClientID%>").value = null;
            document.getElementById("<%=txtproname.ClientID%>").value = null;
            document.getElementById("<%=txtcatinfo.ClientID%>").value = null;
            document.getElementById("<%=txtbrandname.ClientID%>").value = null;
            document.getElementById("<%=txtcompname.ClientID%>").value = null;
            document.getElementById("<%=txtcompetitionname.ClientID%>").value = null;
            document.getElementById("<%=txtclassname.ClientID%>").value = null;
            document.getElementById("<%=txtyear.ClientID%>").value = null;
            document.getElementById("<%=txtseason.ClientID%>").value = null;
            document.getElementById("<%=txtexdatetime1.ClientID%>").value = null;
            document.getElementById("<%=txtdatetime2.ClientID%>").value = null;
            document.getElementById("<%=txtremark.ClientID%>").value = null;
            var drp1 = document.getElementById("<%=ddlexhibitor.ClientID%>");
            var drp2 = document.getElementById("<%=ddlmall.ClientID%>");
            var drp3 = document.getElementById("<%=ddlexbresulterror.ClientID%>");
            var drp4 = document.getElementById("<%=ddlAPIcheck.ClientID%>");
            var drp5 = document.getElementById("<%=ddlexhibitioncheck.ClientID%>");
            var drp6 = document.getElementById("<%=chkitemcode.ClientID%>");
            drp1.selectedIndex = 0;
            drp2.selectedIndex = 0;
            drp3.selectedIndex = 0;
            drp4.selectedIndex = 0;
            drp5.selectedIndex = 0;
            drp6.checked = false;
        }
    }
    </script>

    <script type="text/javascript">
        function openWindow(url) {
            window.open(url, '_blank');
            window.focus();
        }
    </script>
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(function () {
                $("[id$=txtexdatetime1]").datepicker({
                    showOn: 'button',
                    buttonImageOnly: true,
                    buttonImage: '../../images/calendar.gif',
                    dateFormat: 'dd/M/yy',
                    yearRange: "2013:2030"
                });
            });
            $(function () {
                $("[id$=txtdatetime2]").datepicker({
                    showOn: 'button',
                    buttonImageOnly: true,
                    buttonImage: '../../images/calendar.gif',
                    dateFormat: 'dd/M/yy',
                    yearRange: "2013:2030"
                });
            });
        }
    </script>
    <style type="text/css">  /*addbyct*/
        /*Tennis*/
    .mallSet p.iconT { background: #120fbb; }
    .mallSet p.iconT:before {content: "自";}

    .margin{
       margin-bottom:3px;
       margin-left: -3px;
    }
    #ComBlock .ui-datepicker-trigger{
        margin-left : 2px;
    }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdfFromDate" runat="server" />
    <asp:HiddenField ID="hdfToDate" runat="server" />
    <asp:UpdatePanel ID="UPanel" runat="server">
        <ContentTemplate>
            <div id="CmnContents">
                <div id="ComBlock">
                    <div class="setListBox inlineSet iconSet iconShop">
                        <h1>出品一覧</h1>
                        <div class="exbCmnSet resetValue searchBox">
                            <h2>出品検索</h2>
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
                                <dl>
                                    <dt>商品番号<br>
                                        <asp:CheckBox ID="chkitemcode" runat="server" />完全</dt>
                                    <dd>
                                        <asp:TextBox TextMode="MultiLine" ID="txtcode" runat="server"></asp:TextBox>
                                    </dd>
                                    <dt>
                                        <asp:Label ID="Label11" runat="server" Text="商品名" ToolTip="Product Name "></asp:Label></dt>
                                    <dd>
                                        <asp:TextBox ID="txtproname" runat="server"></asp:TextBox></dd>
                                    <dt title="Catalog information">カタログ情報</dt>
                                    <dd>
                                        <asp:TextBox ID="txtcatinfo" runat="server"></asp:TextBox></dd>
                                    <dt title="Brand name">ブランド名</dt>
                                    <dd>
                                        <asp:TextBox ID="txtbrandname" runat="server"></asp:TextBox></dd>
                                    <dt title="CompanyName">仕入先名</dt>
                                    <dd>
                                        <asp:TextBox ID="txtcompname" runat="server"></asp:TextBox></dd>
                                    <dt title="Competition name">競技名</dt>
                                    <dd>
                                        <asp:TextBox ID="txtcompetitionname" runat="server"></asp:TextBox></dd>
                                    <dt title="Class name">分類名</dt>
                                    <dd>
                                        <asp:TextBox ID="txtclassname" runat="server"></asp:TextBox></dd>
                                    <dt title="Year">年度</dt>
                                    <dd>
                                        <asp:TextBox ID="txtyear" runat="server"></asp:TextBox></dd>
                                    <dt title="Season">シーズン</dt>
                                    <dd>
                                        <asp:TextBox ID="txtseason" runat="server"></asp:TextBox></dd>
                                    <dt title="Exhibition date and time">出品日時</dt>
                                    <dd>
                                        <asp:TextBox ID="txtexdatetime1" runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Width="16px" Height="15px" CssClass="margin"
                                            ImageUrl="~/Styles/clear.png" OnClick="ImageButton1_Click" ImageAlign="AbsBottom" />
                                    </dd>
                                    <dt style="width: 65px; text-align: center;">~</dt>
                                    <dd>
                                        <asp:TextBox ID="txtdatetime2" runat="server" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton2" runat="server" Width="16px" Height="15px" CssClass="margin"
                                            ImageUrl="~/Styles/clear.png" OnClick="ImageButton2_Click" ImageAlign="AbsBottom" /></dd>
                                    <dd></dd>
                                    <dt>
                                        <asp:Label ID="Label20" runat="server" Text="出品者 " ToolTip="Exhibitor "></asp:Label></dt>
                                    <dd>
                                        <asp:DropDownList ID="ddlexhibitor" runat="server">
                                        </asp:DropDownList></dd>
                                    <dt>
                                        <asp:Label ID="Label21" runat="server" Text="出品モール" ToolTip="Exhibition Mall "></asp:Label></dt>
                                    <dd>
                                        <asp:DropDownList ID="ddlmall" runat="server">
                                        </asp:DropDownList></dd>
                                    <dt>
                                        <asp:Label ID="Label22" runat="server" Text="備考" ToolTip="Remarks "></asp:Label></dt>
                                    <dd>
                                        <asp:TextBox ID="txtremark" runat="server"></asp:TextBox></dd>
                                    <dt>
                                        <asp:Label ID="Label23" runat="server" Text="出品結果エラー" ToolTip="Exhibition result error "></asp:Label></dt>
                                    <dd>
                                        <asp:DropDownList ID="ddlexbresulterror" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="2">○</asp:ListItem>
                                            <asp:ListItem Value="1">×</asp:ListItem>
                                            <asp:ListItem Value="0">未</asp:ListItem>
                                            <asp:ListItem Value="3">対象外</asp:ListItem>
                                        </asp:DropDownList></dd>
                                    <dt>
                                        <asp:Label ID="Label9" runat="server" Text="APIチェック" ToolTip="API Check "></asp:Label></dt>
                                    <dd>
                                        <asp:DropDownList ID="ddlAPIcheck" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="2">○</asp:ListItem>
                                            <asp:ListItem Value="1">×</asp:ListItem>
                                            <asp:ListItem Value="0">未</asp:ListItem>
                                            <asp:ListItem Value="3">対象外</asp:ListItem>
                                        </asp:DropDownList></dd>
                                    <dt>
                                        <asp:Label ID="Label24" runat="server" Text="エラーチェック" ToolTip="Batch check"></asp:Label></dt>
                                    <dd>
                                        <asp:DropDownList ID="ddlexhibitioncheck" runat="server">
                                            <asp:ListItem></asp:ListItem>
                                            <asp:ListItem Value="1" Selected="True">エラー未処理のみ</asp:ListItem>
                                            <asp:ListItem Value="2">エラーのある商品</asp:ListItem>
                                            <asp:ListItem Value="3">未チェックの商品</asp:ListItem>
                                            <%--<asp:ListItem Value="3">対象外</asp:ListItem>--%>
                                        </asp:DropDownList></dd>
                                    <dt>
                                        <asp:Label ID="lblinstructionno" runat="server" Text="指示書番号"></asp:Label>
                                    </dt>
                                    <dd>
                                        <asp:TextBox ID="txtinstructionno" runat="server"></asp:TextBox>
                                    </dd>
                                    <p>
                                        <asp:Button ID="btnsearch" runat="server" Text="検索" OnClientClick="target=''" OnClick="btnsearch_Click" Width="166px" ToolTip="Search" />
                                    </p>
                                </dl>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div id="CmnContents2">
                <div id="ComBlock2">
                    <div class="operationBtn editBtn">
                        <p>
                            <asp:DropDownList ID="ddlpage" runat="server" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged" AutoPostBack="true">
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                            </asp:DropDownList>
                            <asp:Button ID="btnDownload" runat="server" Text="ダウンロード" OnClick="btnDownload_Click" ToolTip="Download" />
                            <asp:LinkButton ID="lnkdownload" runat="server" OnClick="lnkdownload_Click"></asp:LinkButton>
                        </p>
                    </div>

                    <div class="exbCmnSet listSet resetValue iconSet2">
                        <asp:GridView ID="gvexhibition" runat="server" CellPadding="4"
                            ForeColor="#333333" GridLines="None" AutoGenerateColumns="False"
                            OnRowCommand="gvexhibition_RowCommand"
                            EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True"
                            AllowPaging="True" OnPageIndexChanging="gvexhibition_PageIndexChanging"
                            DataKeyNames="ID" OnRowDataBound="gvexhibition_RowDataBound" CssClass="listTable" PageSize="30">
                            <Columns>
                                <asp:TemplateField HeaderText="ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("Item_ID")%>' CommandName="Name"></asp:Label>
                                        <asp:Label ID="lblEID" runat="server" Text='<%#Eval("ID")%>' CommandName="Name"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button ID="btndetail" runat="server" Text="詳 細 " OnClientClick="target='_blank'" CommandName="Detail" CommandArgument='<%#Eval("Mall_ID") %>' />
                                        <asp:Button ID="btnEdit" runat="server" Text="商品編集" OnClientClick="target='_blank'" CommandName="Edit" CommandArgument='<%#Eval("Item_Code") %>' />
                                        <asp:Button ID="btnExport" runat="server" Text="CSV" OnClientClick="target=' '" CommandName="Export" CommandArgument='<%#Eval("Mall_ID") %>' />
                                        <asp:Button ID="btncancel" runat="server" Text="出品取消" Visible="false" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="フラグ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCtrl_ID" runat="server" Text='<%#Eval("Ctrl_ID")%>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="出品日時">
                                    <ItemTemplate>
                                        <asp:Label ID="lblexhibidateandtime" runat="server" Text='<%#Eval("Exbdate","{0:yyyy/MM/dd  HH:mm}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="処理開始時刻">
                                    <ItemTemplate>
                                        <asp:Label ID="lblprocessstarttime" runat="server" Text='<%#Eval("Exhibition_Date","{0:yyyy/MM/dd  HH:mm}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="商品番号">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="商品名">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ItemStyle-CssClass="stSet mallSet" HeaderText="出品ショップ ">
                                    <ItemTemplate>
                                        <p id="r" class="iconR" runat="server" visible="false"></p>
                                        <asp:Label ID="Label6" runat="server" Text='<%#Eval("Shop_Name") %>' Visible="false"></asp:Label>
                                        <p id="y" class="iconY" runat="server" visible="false"></p>
                                        <asp:Label ID="Label3" runat="server" Text='<%#Eval("Shop_Name") %>' Visible="false"></asp:Label>
                                        <p id="w" class="iconW" runat="server" visible="false"></p>
                                        <asp:Label ID="Label4" runat="server" Text='<%#Eval("Shop_Name") %>' Visible="false"></asp:Label>
                                        <p id="j" class="iconJ" style="float: left" runat="server" visible="false"></p>
                                        <asp:Label ID="Label5" runat="server" Text='<%#Eval("Shop_Name") %>' Visible="false"></asp:Label>
                                        <p id="a" class="iconA" runat="server" visible="false"></p>
                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("Shop_Name") %>' Visible="false"></asp:Label>
                                        <p id = "t" class="iconT" runat="server" visible="false"></p>
                                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("Shop_Name") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="出品者">
                                    <ItemTemplate>
                                        <asp:Label ID="lblexhibitor" runat="server" Text='<%#Eval(" User_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>出品結果<br>エラー</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblexhiresulterror" runat="server" Text='<%#Eval("ExportError_Check") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>API<br />チェック</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAPIcheck" runat="server" Text='<%#Eval("API_Check") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>ショップ<br />確認</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Button ID="btnshopconfirmation" runat="server" Text="表　示" OnClientClick="target='_blank'" CommandName="ShopInfo" CommandArgument='<%#Eval("Mall_ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shop_SiteName" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblsitename" runat="server" Text='<%#Eval("Shop_SiteName") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblShop_ID" runat="server" Text='<%#Eval("Shop_ID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsR1_Collect" runat="server" Text='<%#Eval("IsR1_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsR2_Collect" runat="server" Text='<%#Eval("IsR5_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsR3_Collect" runat="server" Text='<%#Eval("IsR8_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsR4_Collect" runat="server" Text='<%#Eval("IsR12_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsY2_Collect" runat="server" Text='<%#Eval("IsY2_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsY6_Collect" runat="server" Text='<%#Eval("IsY6_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsY9_Collect" runat="server" Text='<%#Eval("IsY9_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsY13_Collect" runat="server" Text='<%#Eval("IsY13_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsY17_Collect" runat="server" Text='<%#Eval("IsY17_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsP3_Collect" runat="server" Text='<%#Eval("IsP3_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsW4_Collect" runat="server" Text='<%#Eval("IsW4_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsJ21_Collect" runat="server" Text='<%#Eval("IsJ21_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsA4_Collect" runat="server" Text='<%#Eval("IsA4_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblIsT6_Collect" runat="server" Text='<%#Eval("IsT6_Collect") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblCSV_FileName" runat="server" Text='<%#Eval("CSV_FileName") %>' Visible="false"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>再処理<br />済表示</HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkbox" Enabled="false" Checked='<%#Convert.ToBoolean(Eval("Exhibition_RecoveryFlag"))%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Visible="False" />
                        </asp:GridView>
                    </div>
                </div>
                <div class="btn">
                    <uc1:UCGrid_Paging runat="server" ID="gp" />
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnsearch" />
            <asp:AsyncPostBackTrigger ControlID="ImageButton1" />
            <asp:AsyncPostBackTrigger ControlID="ImageButton2" />
            <asp:AsyncPostBackTrigger ControlID="ddlpage" />
            <asp:PostBackTrigger ControlID="btnDownload" />
            <asp:PostBackTrigger ControlID="lnkdownload" />
            <asp:PostBackTrigger ControlID="gvexhibition" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
