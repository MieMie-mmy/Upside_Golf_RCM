<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ItemSeparated_OrderList.aspx.cs" Inherits="ORS_RCM.ItemSeparated_OrderList" %>

<%@ Register src="../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
    <link rel="stylesheet" href="../Styles/base.css" />
    <link rel="stylesheet" href="../Styles/common.css" />
    <link rel="stylesheet" href="../Styles/common1.css" />
    <link rel="stylesheet" href="../Styles/manager-style.css" />
    <link href="../Styles/promotion.css" rel="stylesheet" />
    <link href="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/pagesno.css" rel="stylesheet" />
    <link href="../Styles/item.css" rel="stylesheet" />
    <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
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
                document.getElementById("<%=txtItemCode.ClientID%>").value = null;
              document.getElementById("<%=txtbrandname.ClientID%>").value = null;
              document.getElementById("<%=txtcompetitionname.ClientID%>").value = null;
              document.getElementById("<%=txtyear.ClientID%>").value = null;
              document.getElementById("<%=txtseason.ClientID%>").value = null;
              var drp6 = document.getElementById("<%=chkCode.ClientID%>");
              drp6.checked = false;
          }
      }
    </script>

    <script type="text/javascript">
        //call web method function(current row no of gridview) without postback
        function UpdateQuantity(rowNo) {
            //get current quantity
            var quantity = document.getElementById('MainContent_gvItem_SeparatedOrder_txtStockQuantity_' + rowNo).value;
            //get current id
            var ID = document.getElementById('MainContent_gvItem_SeparatedOrder_lblID_' + rowNo).value;
            rowNo = parseInt(rowNo) + parseInt("1"); //get next rowNo that focus to another textbox
            var txt = document.getElementById('MainContent_gvItem_SeparatedOrder_txtStockQuantity_' + rowNo);
            if (txt != null) {
                txt.focus(); //focus next textbox
            }
            $.ajax({
                type: "POST",
                url: "Item_Stock_View.aspx/updateQuantity", //call c# function
                contentType: "application/json;charset=utf-8",
                data: "{'id':'" + ID + "','quantity':'" + quantity + "'}", //passing id and quantity to update
                dataType: "json",
                success: function (data) {
                },
                error: function (result) {
                    alert("Error");
                }
            });
        }
    </script>

    <script type="text/javascript">
        function trapReturn(ctrl, e) {
            var objval = ctrl.value;
            var objvalue = ctrl.id;
            var temp = objvalue.split("_");
            var rowNo = temp[temp.length - 1];
            var iKeyCode = 0;
            document.getElementById("<%=hfrowNo.ClientID%>").value = rowNo;
        if (window.event) iKeyCode = window.event.keyCode
        else if (e) iKeyCode = e.which;
        if (iKeyCode == 13) {
            //press enter key to gridview's textbox to update quantity
            document.getElementById("<%=hfctrl.ClientID%>").value = objvalue;
            UpdateQuantity(rowNo);
            return false;
        }
        if (!(iKeyCode == 8 || iKeyCode == 13 || iKeyCode == 46) && (iKeyCode < 48 || iKeyCode > 57)) {
            return false;
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
        #ComBlock .ui-datepicker-trigger{
            margin-left : 2px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfctrl" runat="server" Value="" />
    <asp:HiddenField ID="hfrowNo" runat="server" Value="-1" />
    <asp:UpdatePanel ID="UPanel" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="CmnContents">
                <div id="ComBlock">
                    <div class="setListBox inlineSet iconSet iconList" style="margin-top: 50px">
                        <h1>商品別注文一覧画面</h1>
                        <div class="exbCmnSet2 exbCmnSet resetValue searchBox">
                            <h2>商品別注文ー検索</h2>
                            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
                                <dl>
                                    <dt>商品番号<br>
                                        <asp:CheckBox runat="server" ID="chkCode" Text="完全" /></dt>
                                    <dd>
                                        <asp:TextBox TextMode="MultiLine" runat="server" ID="txtItemCode"></asp:TextBox>
                                    </dd>
                                    <dt>ブランド名</dt>
                                    <dd>
                                        <asp:TextBox ID="txtbrandname" runat="server"></asp:TextBox></dd>
                                    <dt>商品名</dt>
                                    <dd>
                                        <asp:TextBox runat="server" ID="txtItem_Name"></asp:TextBox>
                                    </dd>
                                    <dt>対象ショップ</dt>
                                    <dd>
                                        <asp:ListBox ID="lstShop" runat="server" Height="50px"
                                            SelectionMode="Multiple">
                                            <asp:ListItem Value="paintandtool" Text="paintandtool"></asp:ListItem>
                                            <asp:ListItem Value="ペイントアンドツール" Text="ペイントアンドツール"></asp:ListItem>
                                            <%--      <asp:ListItem Value="ラケットプラザ" Text="ラケットプラザ"></asp:ListItem>--%>
                                        </asp:ListBox>
                                    </dd>

                                    <dt>注文日時</dt>
                                    <dd style="width: initial;">
                                        <asp:TextBox ID="txtFromDate" runat="server" ReadOnly="True" Width="130px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton1" runat="server" Width="16px" Height="15px"
                                            ImageUrl="~/Styles/clear.png" OnClick="ImageButton1_Click" />
                                    </dd>
                                    <dt style="width: 60px; text-align: center;">~ </dt>
                                    <dd style="width: initial;">
                                        <asp:TextBox ID="txtToDate" runat="server" ReadOnly="True" Width="130px"></asp:TextBox>
                                        <asp:ImageButton ID="ImageButton2" runat="server" Width="16px" Height="15px"
                                            ImageUrl="~/Styles/clear.png" OnClick="ImageButton2_Click" /></dd>
                                    <dt style="width: 60px;">競技名</dt>
                                    <dd>
                                        <asp:TextBox ID="txtcompetitionname" runat="server"></asp:TextBox>
                                    </dd>
                                    <dt>年度</dt>
                                    <dd>
                                        <asp:TextBox ID="txtyear" runat="server"></asp:TextBox>
                                    </dd>
                                    <dt>シーズン</dt>
                                    <dd>
                                        <asp:TextBox ID="txtseason" runat="server"></asp:TextBox>
                                    </dd>
                                    <dt>担当者</dt>
                                    <dd>
                                        <asp:DropDownList runat="server" ID="ddlPerson"></asp:DropDownList></dd>
                                </dl>
                                <p>
                                    <asp:Button runat="server" ID="btnSearch" Text="検 索" OnClick="btnSearch_Click" />
                                </p>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>

            <div id="CmnContents2">
                <div id="ComBlock2">
                    <p class="itemPage">
                        <asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>150</asp:ListItem>
                            <asp:ListItem>200</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                </div>

                <div class="stcCmnSet ordCmnSet editBox iconSet2">
                    <div class="listTableOver">
                        <input type="button" id="btnEdit" style="display: none;" />
                        <asp:GridView runat="server" AutoGenerateColumns="False" ID="gvItem_SeparatedOrder"
                            DataKeyNames="ID" AllowPaging="True" CssClass="managementList listTable"
                            EmptyDataText="There is no data to display!" PageSize="50"
                            ShowHeaderWhenEmpty="True" OnRowCommand="gvItem_SeparatedOrder_RowCommand"
                            OnPageIndexChanging="gvItem_SeparatedOrder_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderStyle Width="50px" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("Item_Code") %>' Text='<%# Eval("Item_Code") %>' CommandName="DataEdit" Visible="false"></asp:LinkButton>
                                        <%--<asp:Button ID="btnmerchInfo" runat="server"  Text='<%# Eval("Item_Code") %>'  CommandArgument='<%#Eval("Item_Code") %>'  CommandName="DataEdit" Visible="false"/>--%>
                                        <asp:Button runat="server" ID="Button1" Text="編集" Width="80px" CommandName="DataEdit" CommandArgument='<%# Eval("Item_Code") %>' /><br />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="商品画像">
                                    <ItemTemplate>
                                        <%--<p><asp:Image runat="server" ID="Image"/></p>--%>
                                        <asp:Label ID="Label45" runat="server" Text='<%#Bind("ID") %>' Visible="false"></asp:Label>
                                        <asp:Label ID="Label46" runat="server" Text='<%#Bind("商品画像") %>' Visible="false"></asp:Label>
                                        <asp:Image ID="imgitemimage" runat="server" ImageUrl='<%# Eval("商品画像").ToString().Length<3 ? "~/Item_Image/no_image.jpg": Eval("商品画像", "~/Item_Image/{0}")%>'
                                            onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='default'" Width="100px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="商品番号">
                                    <HeaderStyle Width="70px" />
                                    <ItemTemplate>
                                        <asp:Label runat="server" ID="lblItemNumber" Text='<%#Eval("Item_Code") %>'></asp:Label>
                                        <asp:HiddenField runat="server" ID="lblID" Value='<%#Eval("ID") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="商品名">
                                    <HeaderStyle Width="250px" />
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="年度">
                                    <HeaderStyle Width="15px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblyear" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="シーズン">
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblSeason" runat="server" Text='<%#Eval("Season") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="競技">
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblCompetition" runat="server" Text='<%#Eval("競技") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="分類">
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblClassfication" runat="server" Text='<%#Eval("分類") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="ブランド">
                                    <HeaderStyle />
                                    <ItemTemplate>
                                        <asp:Label ID="lblBrand" runat="server" Text='<%#Eval("Brand_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="定価">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" Style="text-align: right" runat="server" Text='<%#Eval("定価", "{0:#,###}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="割引率">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDiscount_Rate" runat="server" Text='<%#Eval("割引率","{0:0.##}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="原価">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCost" Style="text-align: right" runat="server" Text='<%#Eval("原価", "{0:#,###}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="粗利率">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrossProfit" Style="text-align: right" runat="server" Text='<%#Eval("粗利率", "{0:0.##}") %>'></asp:Label><span>%</span>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="単価">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalePrice" Style="text-align: right" runat="server" Text='<%#Eval("単価", "{0:#,###}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="数量">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantity" Style="text-align: right" runat="server" Text='<%#Eval("数量") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="金額">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmount" Style="text-align: right" runat="server" Text='<%#Eval("金額") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="特記">
                                    <HeaderStyle Width="150px" />
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemark" Style="text-align: right" runat="server" Text='<%#Eval("特記") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="担当者">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPerson_Incharge" Style="text-align: right" runat="server" Text='<%#Eval("User_Name") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings Visible="False" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ImageButton1" />
            <asp:AsyncPostBackTrigger ControlID="ImageButton2" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:AsyncPostBackTrigger ControlID="ddlpage" />
            <asp:AsyncPostBackTrigger ControlID="gvItem_SeparatedOrder" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:HiddenField runat="server" ID="hdfFromDate" />
    <asp:HiddenField runat="server" ID="hdfToDate" />
    <asp:HiddenField runat="server" ID="hdfSearch" />

    <div class="btn">
        <uc1:UCGrid_Paging ID="gp3" runat="server" />
    </div>



</asp:Content>
