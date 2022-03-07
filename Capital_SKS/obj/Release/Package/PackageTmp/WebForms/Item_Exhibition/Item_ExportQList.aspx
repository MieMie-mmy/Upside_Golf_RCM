<%@ Page Title="商品管理システム＜出品期日待ち商品一覧＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_ExportQList.aspx.cs" Inherits="ORS_RCM.Item_ExportQList" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
    <link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
    <link href="../../Scripts/jquery.page-scroller.js" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
     function CallTab(e) {
         var val = document.getElementById("<%=hfNewTab.ClientID%>").innerHTML;
         if (val != "1")
             document.forms[0].target = "_blank";
         else
             document.forms[0].target = "";
     }
</script>

    <script type = "text/javascript">
    function SetTarget() {
        document.forms[0].target = "_blank";
        location.reload();
    }
</script>

    <script type="text/javascript">
     function pageLoad(sender, args) {
         $(function () {
             $("[id$=txtpavadate]").datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 buttonImage: '../../images/calendar.gif',
                 dateFormat: 'dd/M/yy',
                 yearRange: "2013:2030"
             });
         });
         $(function () {
             $("[id$=txtpdateend]").datepicker({
                 showOn: 'button',
                 buttonImageOnly: true,
                 buttonImage: '../../images/calendar.gif',
                 dateFormat: 'dd/M/yy',
                 yearRange: "2013:2030"
             });
         });
     }
	</script>
    <style>
        .margin{
            margin-bottom:3px;
        }
        #ComBlock .ui-datepicker-trigger{
            margin-left : 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#CmnContents">▲TOP</a></p>
	<asp:HiddenField ID="hdfFromDate" runat="server" />
	<asp:HiddenField ID="hdfToDate" runat="server" />
    <body>
        <form action="#" method="get">
            <div id="CmnWrapper">
                <div id="CmnContents">
                    <div id="ComBlock">
                        <div class="setListBox inlineSet iconSet iconShop">
                            <h1>出品期日待ち商品一覧</h1>
                            <div class="exbCmnSet2 exbCmnSet resetValue searchBox">
                                <h2>出品期日待ち商品一覧検索</h2>
                                <asp:UpdatePanel ID="UPSearch" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dl>
                                            <dt>商品番号</dt>
                                            <dd>
                                                <asp:TextBox ID="txtitemno" runat="server" Width="125px"></asp:TextBox></dd>
                                            <dt>商品名</dt>
                                            <dd>
                                                <asp:TextBox ID="txtproductname" runat="server" Width="125px"></asp:TextBox></dd>
                                            <dt>ブランド</dt>
                                            <dd>
                                                <asp:TextBox ID="txtbrand" runat="server" Width="125px"></asp:TextBox></dd>
                                            <dt>仕入先</dt>
                                            <dd>
                                                <asp:TextBox ID="txtsupplier" runat="server" Width="125px"></asp:TextBox></dd>
                                            <dt>カタログ番号</dt>
                                            <dd>
                                                <asp:TextBox ID="txtcatno" runat="server" Width="125px"></asp:TextBox></dd>
                                            <dt>掲載可能日</dt>
                                            <dd>
                                                <asp:TextBox ID="txtpavadate" runat="server" Width="125px" ReadOnly="true"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Width="16px" Height="15px" ImageUrl="~/Styles/clear.png" OnClick="ImageButton1_Click" ImageAlign="AbsBottom"  CssClass="margin"/></dd>
                                            <dt style="width: 65px; text-align: center;">~ </dt>
                                            <dd>
                                                <asp:TextBox ID="txtpdateend" runat="server" Width="125px"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton2" runat="server" Width="16px" Height="15px" ImageUrl="~/Styles/clear.png" OnClick="ImageButton2_Click" ImageAlign="AbsBottom" CssClass="margin"/></dd>
                                        </dl>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                <p>
                                    <asp:Button ID="btnSearch" runat="server" Text="検 索" Width="114px" OnClick="btnSearch_Click" /></p>
                            </div>
                            <asp:UpdatePanel ID="UPanel" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="operationBtn">
                                        <p><asp:Button ID="btnselectall" runat="server" Text="全て選択" OnClick="btnselectall_Click" /></p>
                                        <p><asp:Button ID="btncancelall" runat="server" Text="全て解除" OnClick="btncancelall_Click" /></p>
                                        <p><asp:Button ID="btnexhibition" runat="server" Text="選択商品を出品する" OnClick="btnexhibition_Click" OnClientClick="CallTab(event);" /></p>
                                    </div>
                                    <!-- exbition list -->
                                    <div class="exbCmnSet2 resetValue editBox">
                                        <asp:HiddenField ID="hfdate" runat="server" />
                                        <asp:GridView ID="gvexpQ" runat="server" AllowPaging="True" CssClass="ExportQTable" AutoGenerateColumns="False" EmptyDataText="There is no data to display!" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" OnRowCommand="gvexpQ_RowCommand" OnRowDataBound="gvexpQ_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnedit" runat="server" Text="商品編集" OnClientClick="SetTarget();" CommandName="DataEdit" CommandArgument='<%#Eval("Item_Code") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText=" 対象 ">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="ckItem" runat="server" AutoPostBack="true" OnCheckedChanged="ckItem_Check" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate> SKS <br /> ST </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <p id="PWaitL" runat="server" class="waitL"></p>
                                                        <asp:Label ID="lblSKUStatus" runat="server" Text='<%#Eval("Export_Status") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate> Shop <br /> ST </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <p id="PWait" runat="server" class="wait"></p>
                                                        <p id="POk" runat="server" class="ok"></p>
                                                        <p id="PDel" runat="server" class="del"></p>
                                                        <p id="PInactive" runat="server" class="deactive"></p>
                                                        <asp:Label ID="lblshop" runat="server" Text='<%#Eval("Ctrl_ID") %>' Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="掲載可能日">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label8" runat="server" Text='<%#Eval("Post_Available_Date") %>' CssClass="spalign"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="商品番号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label9" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="商品名">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label10" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="商品画像">
                                                    <ItemTemplate>
                                                        <p><asp:Image runat="server" ID="Image1" /></p>
                                                        <p><asp:Image runat="server" ID="Image2" /></p>
                                                        <p><asp:Image runat="server" ID="Image3" /></p>
                                                        <p><asp:Image runat="server" ID="Image4" /></p>
                                                        <p><asp:Image runat="server" ID="Image5" /></p>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ID" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ID" HeaderText="ID" Visible="false" />

                                                <asp:TemplateField HeaderText="ブランド">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblb" runat="server" Text='<%#Eval("Brand_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="仕入先">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label11" runat="server" Text='<%#Eval("Company_Name") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="カタログ番号">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label7" runat="server" Text='<%#Eval("Catalog_Information") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerSettings Visible="False" />
                                        </asp:GridView>
                                        <asp:Label runat="server" ID="hfNewTab" Text="1" Style="display: none;" />
                                    </div>
                                    <!-- exbition list -->
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="btnselectall" />
                                    <asp:AsyncPostBackTrigger ControlID="btncancelall" />
                                    <asp:PostBackTrigger ControlID="btnexhibition" />
                                    <asp:AsyncPostBackTrigger ControlID="gvexpQ" />
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
        </form>
    </body>
	 <div class="btn">
		<uc1:UCGrid_Paging  runat="server" ID="gp" />
	 </div>
</asp:Content>

