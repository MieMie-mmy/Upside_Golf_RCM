
<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Campaign_promotion_View.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Campaign_promotion_View" %>

<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/jscript"></script>
    <link href="../../Styles/base.css" rel="stylesheet" />
    <link href="../../Styles/common.css" rel="stylesheet" />
    <link href="../../Styles/manager-style.css" rel="stylesheet" />
    <link href="../../Styles/promotion.css" rel="stylesheet" />
    <link href="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
    <link href="../../Styles/pagesno.css" rel="stylesheet" />
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
                document.getElementById("<%=txtCampaign_ID.ClientID%>").value = null;
		    document.getElementById("<%=txtCampaign_Name.ClientID%>").value = null;
		    document.getElementById("<%=txtPeriod_From.ClientID%>").value = null;
		    document.getElementById("<%=txtPeriod_To.ClientID%>").value = null;
		    document.getElementById("<%=txtCampaign_ID.ClientID%>").value = null;
		    document.getElementById("<%=txtCampaign_Name.ClientID%>").value = null;
		    document.getElementById("<%=txtSubject.ClientID%>").value = null;
		    document.getElementById("<%=txtTarget_Brand.ClientID%>").value = null;
		    document.getElementById("<%=txtInstruction_No.ClientID%>").value = null;
		    document.getElementById("<%=txtRemark.ClientID%>").value = null;
		    document.getElementById("<%=txtshippingno.ClientID%>").value = null;

		    var drp1 = document.getElementById("<%=lstCampaignType.ClientID%>");
		    var drp2 = document.getElementById("<%=ddlPrioritites.ClientID%>");
		    var drp3 = document.getElementById("<%=lstTargetShop.ClientID%>");
		    var drp4 = document.getElementById("<%=chkPresent.ClientID%>");
		    var drp5 = document.getElementById("<%=chkPublic.ClientID%>");
		    var drp6 = document.getElementById("<%=chkFull.ClientID%>");
		    drp1.selectedIndex = -1;
		    drp2.selectedIndex = -1;
		    drp3.selectedIndex = -1;
		    drp4.checked = false;
		    drp5.checked = false;
		    drp6.checked = false;
		}
	}
    </script>

    <script type="text/javascript">
        $(window).on('load resize', function () {
            var w = $(window).width() - 2;
            $('.listTable').css('width', w + 'px');
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $("#hideBlock p").on("click", function () {
                var isVisible = $("#hideBox").is(":visible");
                var showhide = document.getElementById("<%=hfShowHide.ClientID%>");
			showhide.value = !isVisible;

			$(this).next().slideToggle();
			$(this).toggleClass("active"); //追加部分
		});
	});
    </script>

    <script type="text/javascript">
        function SearchClick() {
            var isVisible = document.getElementById("<%=hfShowHide.ClientID%>").value;
	    if (isVisible == "true") {
	        $("#hideBlock p").next().slideToggle();
	        $("#hideBlock p").toggleClass("active");
	    }
	    else {
	        $("#hideBlock p").siblings().removeClass("active");
	    }
	}
    </script>

    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $("#<%=txtPeriod_From.ClientID %>").datepicker({
                    showOn: 'button',
                    dateFormat: 'yy/mm/dd',
                    buttonImageOnly: true,
                    buttonImage: '../../images/calendar.gif',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:2030",
                });
                $(".ui-datepicker-trigger").mouseover(function () {
                    $(this).css('cursor', 'pointer');
                });
            });

            $(document).ready(function () {
                $("#<%=txtPeriod_To.ClientID %>").datepicker({
                    showOn: 'button',
                    dateFormat: 'yy/mm/dd',
                    buttonImageOnly: true,
                    buttonImage: '../../images/calendar.gif',
                    changeMonth: true,
                    changeYear: true,
                    yearRange: "1900:2030",
                });
                $(".ui-datepicker-trigger").mouseover(function () {
                    $(this).css('cursor', 'pointer');
                });
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hdfFromDate" runat="server" />
    <asp:HiddenField ID="hdfToDate" runat="server" />
    <asp:HiddenField ID="hfShowHide" runat="server" />
    <asp:HiddenField ID="hfid" runat="server" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:HiddenField ID="HiddenField2" runat="server" />
    <asp:HiddenField ID="hfperiod" runat="server" />
    <asp:HiddenField ID="hfperioto" runat="server" />

    <p id="toTop"><a href="#divtop">▲TOP</a></p>
    <div id="CmnContents">
        <div id="ComBlock" style="margin-top: 60px;">
            <div class="setListBox inlineSet iconSet iconList">
                <h1>キャンペーン一覧</h1>
                <div class="prmCmnSet prmCamList resetValue searchBox">
                    <h2>キャンペーン検索</h2>
                    <div class="block1">
                        <dl>
                            <dt>キャンペーンID</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtCampaign_ID" /></dd>
                            <dt>キャンペーン名</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtCampaign_Name" /></dd>
                            <dt>開催期間</dt>
                            <dd class="promotionWidth">
                                <asp:TextBox ID="txtPeriod_From" runat="server" ReadOnly="true" />
                                <asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom" />
                                ～
                                <asp:TextBox ID="txtPeriod_To" runat="server" ReadOnly="true" />
                                <asp:ImageButton ID="ImageButton2" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton2_Click" ImageAlign="AbsBottom" />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="* Invalid Date"
                                    ControlToCompare="txtPeriod_From" ControlToValidate="txtPeriod_To" ForeColor="Red" Operator="GreaterThan" />
                            </dd>

                            <dt title="Campaign_Type">キャンペーン種別</dt>
                            <dd>
                                <asp:ListBox ID="lstCampaignType" runat="server" Height="100px"
                                    SelectionMode="Multiple">
                                    <asp:ListItem Value="0" Text="商品別ポイント"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="店舗別ポイント"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="商品別クーポン"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="送料"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="即日出荷"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="予約"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="事前告知"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="シークレットセール"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="プレゼントキャンペーン"></asp:ListItem>
                                </asp:ListBox>
                            </dd>
                            <dt title="Target">対象ショップ</dt>
                            <dd>
                                <asp:ListBox ID="lstTargetShop" runat="server" SelectionMode="Multiple" size="5"></asp:ListBox>
                            </dd>
                        </dl>
                    </div>
                    <div id="hideBlock">
                        <p>詳細検索</p>
                        <div id="hideBox">
                            <dl>
                                <dt>対象商品番号<br />
                                    完全<asp:CheckBox ID="chkFull" runat="server" /></dt>
                                <dd>
                                    <asp:TextBox ID="txtshippingno" TextMode="MultiLine" runat="server"></asp:TextBox>
                                </dd>
                                <dt>対象者</dt>
                                <dd>
                                    <asp:TextBox runat="server" ID="txtSubject" /></dd>
                                <dt>対象ブランド</dt>
                                <dd>
                                    <asp:TextBox runat="server" ID="txtTarget_Brand" /></dd>
                                <dt>指示書番号</dt>
                                <dd>
                                    <asp:TextBox runat="server" ID="txtInstruction_No" /></dd>
                                <dt>優先順位</dt>
                                <dd>
                                    <asp:DropDownList ID="ddlPrioritites" runat="server">
                                        <asp:ListItem Text="" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="特" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="高" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="中" Value="3"></asp:ListItem>
                                        <asp:ListItem Text="低" Value="4"></asp:ListItem>
                                    </asp:DropDownList>
                                </dd>
                                <dt>備考</dt>
                                <dd>
                                    <asp:TextBox runat="server" TextMode="MultiLine" ID="txtRemark" /></dd>
                                <dt></dt>
                                <dd>
                                    <asp:CheckBox ID="chkPublic" Text="公開" runat="server" />
                                    <asp:CheckBox ID="chkPresent" Text="プレゼント" runat="server" /></dd>
                            </dl>
                        </div>
                    </div>
                    <p>
                        <asp:Button runat="server" ID="btnSearch" Text="検 索" OnClick="btnSearch_Click" />
                    </p>

                </div>
                <!-- /.searchBox -->

            </div>
            <!--setListBox-->
        </div>
        <!--ComBlock-->

    </div>
    <!--CmnContents-->


    <div id="CmnContents2">
        <div id="ComBlock2">
            <div class="widthhMax iconEx operationBtn">
                <div class="operationBtn">
                    <p>
                        <asp:Button runat="server" ID="btnNew" Text="キャンペーンの追加" Width="200px" OnClick="btnNew_Click" />
                        <asp:Button runat="server" ID="btnExport" Text="データエクスポート" Width="200px" OnClick="btnExport_Click" />
                        <asp:Button runat="server" ID="btnCampaign_Schedule" Text="キャンペーンスケジュール" Width="200px" OnClick="btnCampaign_Schedule_Click" />

                    </p>
                    <p class="itemPage">
                        <asp:DropDownList ID="ddlpage" runat="server"
                            AutoPostBack="true" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged">
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                            <asp:ListItem>150</asp:ListItem>
                        </asp:DropDownList>
                    </p>
                </div>
            </div>
        </div>

        <div class="prmCmnSet resetValue listBox">

            <asp:GridView runat="server" ID="gvPromotion" AutoGenerateColumns="False"
                Width="100%" OnRowCommand="gvPromotion_RowCommand" AllowPaging="true" DataKeyNames="ID"
                OnRowDataBound="gvPromotion_RowDataBound" CssClass="prmCamList managementList listTable"
                ShowHeaderWhenEmpty="True">
                <Columns>

                    <asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:Button runat="server" ID="btnEdit" Text="編集" Width="80px"
                                CommandName="DataEdit" CommandArgument='<%# Eval("ID") %>' CssClass="operationBtn" /><br />
                            <asp:Button runat="server" ID="btnCopy" Text="表示" Width="80px"
                                CommandName="DataCopy" CommandArgument='<%# Eval("ID") %>' CssClass="operationBtn" />

                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="キャンペーンID">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCampaign_ID" Text='<% #Eval("Campaign_ID")%>' />
                        </ItemTemplate>

                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="キャンペーン名">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCam_name" Text='<% #Eval("Promotion_Name")%>' />
                        </ItemTemplate>

                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="キャンペーン種類">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblCampaignType" Text='<% #Eval("Campaign_TypeID")%>' />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="開催期間">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblPeriodFrom" Text='<% #Eval("Period_From", "{0:yyyy/MM/dd}")%>' />
                            <asp:Label runat="server" ID="Label4" Text="~" />
                            <asp:Label runat="server" ID="lblPeriodTo" Text='<% #Eval("Period_To", "{0:yyyy/MM/dd}")%>' />
                            <asp:Label runat="server" ID="lblDateDiff" Visible="false" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="ショップ" ItemStyle-Width="200px">
                        <ItemTemplate>
                            <asp:Image ID="rbseball" runat="server" ImageUrl="../../images/Bp.gif" Visible="false" />
                            <asp:Image ID="by" runat="server" ImageUrl="../../images/Bp_orange.gif" Visible="false" />
                            <asp:Image ID="hp" runat="server" ImageUrl="../../images/HP_orange.gif" Visible="false" />
                            <asp:Image ID="rlp" runat="server" ImageUrl="../../images/Lp.gif" Visible="false" />
                            <asp:Image ID="ylp" runat="server" ImageUrl="../../images/Lp_orange.gif" Visible="false" />
                            <asp:Image ID="rrp" runat="server" ImageUrl="../../images/Rp.gif" Visible="false" />
                            <asp:Image ID="ablack" runat="server" ImageUrl="../../images/Rp_black.gif" Visible="false" />
                            <asp:Image ID="yrp" runat="server" ImageUrl="../../images/Rp_orange.gif" Visible="false" />
                            <asp:Image ID="prp" runat="server" ImageUrl="../../images/Rp_red.gif" Visible="false" />
                            <asp:Image ID="rsp" runat="server" ImageUrl="../../images/Sp.gif" Visible="false" />
                            <asp:Image ID="ysp" runat="server" ImageUrl="../../images/Sp_orange.gif" Visible="false" />
                            <asp:Image ID="pbp" runat="server" ImageUrl="../../images/BP_red.gif" Visible="false" />
                            <asp:Image ID="abp" runat="server" ImageUrl="../../images/BP_black.gif" Visible="false" />
                            <asp:Image ID="rhp" runat="server" ImageUrl="../../images/HP_red.gif" Visible="false" />
                            <asp:Image ID="ajp" runat="server" ImageUrl="../../images/jisha.gif" Visible="false" />
                            <asp:DropDownList runat="server" ID="ddlShop" Visible="false"></asp:DropDownList>
                        </ItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="対象ブランド">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblTarget_Brand" Text='<% #Eval("Target_Brand")%>' />
                        </ItemTemplate>

                    </asp:TemplateField>



                    <asp:TemplateField HeaderText="優先順位">
                        <ItemTemplate>
                            <asp:Label runat="server" ID="Label2" Text='<% #Eval("Priority")%>' />
                        </ItemTemplate>

                    </asp:TemplateField>

                </Columns>
                <PagerSettings Visible="False" />
                <RowStyle HorizontalAlign="Center" />
                <EmptyDataTemplate>No records Found</EmptyDataTemplate>
            </asp:GridView>
        </div>
    </div>

    <div class="btn">
        <uc1:UCGrid_Paging ID="gp1" runat="server" />
    </div>

</asp:Content>
