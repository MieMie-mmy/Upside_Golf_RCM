<%@ Page Title="商品管理システム＜商品情報一覧（商品管理）＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_View2.aspx.cs" Inherits="ORS_RCM.Item_View2" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
<%--<link rel="stylesheet" href="http://code.jquery.com/ui/1.9.2/themes/base/jquery-ui.css" />
<script src="http://code.jquery.com/jquery-1.8.3.js"></script>
<script src="http://code.jquery.com/ui/1.9.2/jquery-ui.js"></script>--%>
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
             document.getElementById("<%=txtitemno.ClientID%>").value = null;
             document.getElementById("<%=txtbrandname.ClientID%>").value = null;
             document.getElementById("<%=txtcatinfo.ClientID%>").value = null;
             document.getElementById("<%=txtdate.ClientID%>").value = null;
             document.getElementById("<%=txtdateapproval.ClientID%>").value = null;
             document.getElementById("<%=txtproductname.ClientID%>").value = null;
document.getElementById("<%=txtmanproductcode.ClientID%>").value = null;
             document.getElementById("<%=txtconmpanyname.ClientID%>").value = null;
             document.getElementById("<%=txtcompetitionname.ClientID%>").value = null;
             document.getElementById("<%=txtclassname.ClientID%>").value = null;
             document.getElementById("<%=txtyear.ClientID%>").value = null;
             document.getElementById("<%=txtseason.ClientID%>").value = null;
             document.getElementById("<%=txtremark.ClientID%>").value = null;
             document.getElementById("<%=txtjancode.ClientID%>").value = null;
             document.getElementById("<%=txtsalemanagementcode.ClientID%>").value = null;
             document.getElementById("<%=txtinstrauctionno.ClientID%>").value = null;
             var drp1 = document.getElementById("<%=ddlpersonincharge.ClientID%>");
             var drp2 = document.getElementById("<%=ddlsksstatus.ClientID%>");
             var drp3 = document.getElementById("<%=ddlspecialflag.ClientID%>");
             var drp4 = document.getElementById("<%=ddlreservationflag.ClientID%>");
             var drp5 = document.getElementById("<%=ddlshopstatus.ClientID%>");
             var drp6 = document.getElementById("<%=chkno.ClientID%>");
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
    function CallTab(e) {

//        	    var val = document.getElementById("<%=hfNewTab.ClientID%>").innerHTML;
//        		if(val!="1")
        document.forms[0].target = "_blank";
//                else
//                    document.forms[0].target = ""; 
    }

    function ddlpage_change() {
        document.forms[0].target = "";
    }
</script>

<script type="text/javascript">
    function pageLoad(sender, args) {
        $(function () {

            $("[id$=txtdate]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '../../images/calendar.gif',
                dateFormat: 'dd/M/yy',
                yearRange: "2013:2030"
            });
        });
        $(function () {
            $("[id$=txtdateapproval]").datepicker({
                showOn: 'button',
                buttonImageOnly: true,
                buttonImage: '../../images/calendar.gif',
                dateFormat: 'dd/M/yy',
                yearRange: "2013:2030"
            });
        });
    }
	</script>
<script type="text/javascript">
    $(function () {
        $("#hideBlock p").on("click", function () {
            var isVisible = $("#hideBox").is(":visible");
            var showhide = document.getElementById("<%=hfShowHide.ClientID%>");
             showhide.value = !isVisible;

            $(this).next().slideToggle();
            $(this).toggleClass("active"); //追加部分
            document.cookie = "popupinfo=" +"lastinfo="+ showhide.value;

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

<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8)
            return true;
        else return false;
    } 
</script>
<script type="text/javascript">
    function Show(strOpen, ctrl) {
        var left = (screen.width / 2) - (800 / 2);
        var top = (screen.height / 2) - (500 / 2);
        var retval = "";
        //show modal dialog box and collect its return value
        retval = window.open('../Item/ItemSKU_View.aspx?Item_Code=' + strOpen, window,
		 'status=1,resizable=0,menubar=0,toolbar =0, location ,width=800,height=500,scrollbars=1,top=' + top + ',left=' + left);
        //check if user closed the dialog 
        //without selecting any value
        if (retval == undefined)
            retval = window.returnValue;
    }
</script>

<script type="text/javascript">
    function NewTabPreview(ctrl, e) {
        var index = ctrl.selectedIndex;
        if (index == 0)
            return;
        else {
            document.forms[0].target = "_blank";
            document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
            __doPostBack('', '');
        }
    }
</script>

<script type="text/javascript">
    function NewTab(ctrl, e) {
        var objvalue = ctrl.id;
        var temp = objvalue.split("_");
        var rowNo = temp[temp.length - 1];
        var itemcode = document.getElementById('MainContent_gvItem_lnkItemNo_' + rowNo).innerText;
        if (typeof itemcode === undefined) {
            itemcode = document.getElementById('MainContent_gvItem_lnkItemNo_' + rowNo).outerText;
            window.open('Item_Master.aspx?Item_Code=' + itemcode, '_blank');
        }
        else {
            window.open('Item_Master.aspx?Item_Code=' + itemcode, '_blank');
        }
        //document.forms[0].target = "_blank";
        //document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
        //__doPostBack('', '');
    }
</script>
<script type = "text/javascript">
    function Confirm() {
      
        
     var  confirm_value = document.createElement("INPUT");
         confirm_value.type = "hidden";
        confirm_value.name = "confirm_value";
        if (confirm("削除出品の確認画面に進みます。よろしいですか？")) {

            confirm_value.value = "はい";
        } else {

            confirm_value.value = "いいえ";
        }
     
        var val = document.forms[0].appendChild(confirm_value);
        if (val.value.toString() == "はい") {

            document.forms[0].target = "_blank";
            location.reload();
        }
        else {
            document.forms[0].target = "";
            document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
            __doPostBack('', '');
        }
       
    }
    </script>

        <script type = "text/javascript">
            function Confirm1() {


                            var confirm_value;
                            $("#dialog-confirm").dialog({
                                resizable: false,
                                height: 200,
                                modal: true,
                                buttons: {
                                    "はい": function () {
                                  // var name=     document.getElementById("<%=hfCtrl.ClientID%>").value = ctrl.id;
                                        //__doPostBack('', '');
                                   //   doClicks("#<%=btndelete.ClientID%>",event);
                                   //doClicks(name, event);
                                       // $(this).dialog("close");
                                    },
                                    "いいえ": function () {

                                      //  $(this).dialog("close");
                                    }
                                }

                            });
                //
                //            $(function () {

                //                $("#<%=btndelete.ClientID%>").on("click", function (event) {
                //                    event.preventDefault();
                //                    $("#dialog-confirm").dialog({
                //                        resizable: false,
                //                        height: 140,
                //                        modal: true,
                //                        buttons: {
                //                            Ok: function () {
                //                                $(this).dialog("close");
                //                                __doPostBack($('#<%= btndelete.ClientID %>').attr('name'), '');
                //                            },
                //                            Cancel: function () {
                //                                $(this).dialog("close");
                //                            }
                //                        }
                //                    });
                //                });
                //            });
            }
    </script>


  <%--  <div id="dialog-confirm" style="display: none;" title="Confirm Delete">
    <p><span class="ui-icon ui-icon-alert" style="float: left; margin: 0 7px 20px 0;"></span>削除出品の確認画面に進みます。よろしいですか？</p>
</div>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfShowHide" runat="server" />
    <asp:HiddenField ID="hdfFromDate" runat="server" />
    <asp:HiddenField ID="hdfToDate" runat="server" />

    <asp:HiddenField ID="hfid" runat="server" />
    <body>
          <asp:Panel ID="Panel2" runat="server" DefaultButton="btnsearch">
       <%-- <asp:UpdatePanel ID="UPanel1" runat="server">
            <ContentTemplate>--%>
                <div id="CmnContents">
                    <div id="ComBlock">

                        <div class="setListBox inlineSet iconSet iconList">
                            <h1>商品情報一覧（商品管理）</h1>
                            <div class="itemCmnSetKnr itemCmnSet resetValue searchBox">
                                <h2>商品情報一覧検索</h2>
                                <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
                                   <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <dl>
                                            <dt title="Item Number">商品番号<br>
                                                <asp:CheckBox ID="chkno" runat="server" Text=" 完全" /></dt>
                                            <dd>
                                                <asp:TextBox TextMode="MultiLine" ID="txtitemno" runat="server"></asp:TextBox>
                                            </dd>
                                            <dt title="Brand name">ブランド名</dt>
                                            <dd>
                                                <asp:TextBox ID="txtbrandname" runat="server"></asp:TextBox>
                                            </dd>
                                            <dt title="Catalog information">カタログ情報</dt>
                                            <dd>
                                                <asp:TextBox ID="txtcatinfo" runat="server"></asp:TextBox>
                                            </dd>
                                            <dt title="Person in charge" style="display:none;">担当者</dt>
                                            <dd style="display:none;">
                                                <asp:DropDownList ID="ddlpersonincharge" runat="server">
                                                </asp:DropDownList>
                                            </dd>
                                            <dt title="SKS status">SKSステータス</dt>
                                            <dd>
                                                <asp:DropDownList ID="ddlsksstatus" runat="server">
                                                    <asp:ListItem></asp:ListItem>
                                                    <asp:ListItem Value="1">ページ制作</asp:ListItem>
                                                    <asp:ListItem Value="3">出品待ち</asp:ListItem>
                                                    <asp:ListItem Value="2">期日出品待ち</asp:ListItem>
                                                    <asp:ListItem Value="4">出品済</asp:ListItem>
                                                    <asp:ListItem Value="6">価格なし</asp:ListItem>
                                                </asp:DropDownList>

                                            </dd>
                                            <dt title="Date of approval">承認日</dt>
                                            <dd><%--class="cal"--%>
                                                <asp:TextBox ID="txtdate" runat="server" ReadOnly="True"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
                                                    ImageUrl="~/Styles/clear.png" OnClick="ImageButton1_Click" ImageAlign="AbsBottom" />
                                            </dd>
                                            <dt style="width: 85px; text-align: center;">~ </dt>
                                            <dd>
                                                <asp:TextBox ID="txtdateapproval" runat="server" ReadOnly="True"></asp:TextBox>
                                                <asp:ImageButton ID="ImageButton2" runat="server" Width="15px" Height="15px"
                                                    ImageUrl="~/Styles/clear.png" OnClick="ImageButton2_Click" ImageAlign="AbsBottom" />
                                            </dd>
                                            <dt style="width: 85px;">
                                                <asp:Label runat="server" ID="lblPrice" Text="販売価格" ToolTip="Special Flag" /></dt>
                                            <dd>
                                                <asp:DropDownList runat="server" ID="ddlSellingPrice">
                                                    <asp:ListItem Value=""></asp:ListItem>
                                                    <asp:ListItem Value="1">なし</asp:ListItem>
                                                </asp:DropDownList></dd>
                                            <dt>
                                                <asp:Label runat="server" ID="lblExhibition" Text="M ステータス" /></dt>
                                            <dd>
                                                <asp:DropDownList runat="server" ID="ddlExhibiton">
                                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                                    <asp:ListItem Value="3">出品済</asp:ListItem>
                                                    <asp:ListItem Value="0">未出品</asp:ListItem>
                                                </asp:DropDownList></dd>
                                        </dl>
                                      </ContentTemplate>   
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="ImageButton1" />
                                        <asp:AsyncPostBackTrigger ControlID="ImageButton2" />
                                    </Triggers>
                                </asp:UpdatePanel>


                                    <%--<asp:UpdatePanel ID="UP" runat="server">
   <ContentTemplate>--%>

                                    <div id="hideBlock">

                                        <p>詳細検索</p>


                                        <%--<h4><asp:Button ID="btndivshowhide" runat="server" Text="+" Width="19px" Height="24px" 
		onclick="btndivshowhide_Click" />&nbsp;詳細検索</h4>--%>

                                        <%--<asp:Label ID="lbl" Text="-----------------------------------------------------------------------------------------------------------------------------------------------------------" runat="server" Width="100%"></asp:Label>--%>
                                        <div id="hideBox">
                                            <%--<div id="hidBox" runat="server" visible="false">--%>


                                            <dl>
                                                <dt>商品名</dt>

                                                <dd>
                                                    <asp:TextBox ID="txtproductname" runat="server"></asp:TextBox>
                                                </dd>


                                                <dt>メーカー商品コード</dt>


                                                <dd>
                                                    <asp:TextBox ID="txtmanproductcode" runat="server"></asp:TextBox>
                                                </dd>
                                                <dt>仕入先名</dt>

                                                <dd>
                                                    <asp:TextBox ID="txtconmpanyname" runat="server"></asp:TextBox>
                                                </dd>

                                                <dt>競技名</dt>

                                                <dd>
                                                    <asp:TextBox ID="txtcompetitionname" runat="server"></asp:TextBox>
                                                </dd>

                                                <dt>分類名</dt>

                                                <dd>
                                                    <asp:TextBox ID="txtclassname" runat="server"></asp:TextBox><br />
                                                </dd>


                                                <dt style="display:none;">特記フラグ</dt>

                                                <dd style="display:none;">
                                                    <asp:DropDownList ID="ddlspecialflag" runat="server">
                                                        <asp:ListItem Value=""></asp:ListItem>
                                                        <%--<asp:ListItem Value="0">なし</asp:ListItem>--%>
                                                        <asp:ListItem Value="1">【★】</asp:ListItem>
                                                        <asp:ListItem Value="3">【■】</asp:ListItem>
                                                        <asp:ListItem Value="5">【F】</asp:ListItem>
                                                        <asp:ListItem Value="6">【□】</asp:ListItem>
                                                        <asp:ListItem Value="8">送料別途</asp:ListItem>
                                                        <asp:ListItem Value="9">送料お見積もり</asp:ListItem>
                                                        <asp:ListItem Value="10">【◎】</asp:ListItem>
                                                        <asp:ListItem Value="11">【◆】</asp:ListItem>
                                                    </asp:DropDownList>
                                                </dd>

                                                <dt style="display:none;">予約フラグ"</dt>

                                                <dd style="display:none;">
                                                    <asp:DropDownList ID="ddlreservationflag" runat="server">
                                                        <asp:ListItem Value=""></asp:ListItem>
                                                        <asp:ListItem Value="1">通常商品</asp:ListItem>
                                                        <asp:ListItem Value="2">予約商品</asp:ListItem>
                                                        <asp:ListItem Value="3">☆即☆</asp:ListItem>
                                                    </asp:DropDownList>
                                                </dd>

                                                <dt style="display:none;">年度</dt>
                                                <dd style="display:none;">
                                                    <asp:TextBox ID="txtyear" runat="server"></asp:TextBox>
                                                </dd>
                                                <dt style="display:none;">シーズン</dt>

                                                <dd style="display:none;">
                                                    <asp:TextBox ID="txtseason" runat="server"></asp:TextBox></dd>


                                                <dt>ショップ<br />
                                                    ステータス</dt>
                                                <dd>
                                                    <asp:DropDownList ID="ddlshopstatus" runat="server">
                                                        <asp:ListItem></asp:ListItem>
                                                        <asp:ListItem Value="n">未掲載</asp:ListItem>
                                                        <asp:ListItem Value="u">掲載中</asp:ListItem>
                                                        <asp:ListItem Value="g">掲載無</asp:ListItem>
                                                        <asp:ListItem Value="w">倉庫</asp:ListItem>
                                                        <asp:ListItem Value="e">エラー</asp:ListItem>
                                                        <asp:ListItem Value="nu">削・無のぞく</asp:ListItem>
                                                        <%--<asp:ListItem Value="d">削除</asp:ListItem>--%>
                                                    </asp:DropDownList><br />

                                                </dd>
                                                <dt>備考</dt>
                                                <dd>
                                                    <asp:TextBox ID="txtremark" runat="server"></asp:TextBox>
                                                </dd>

                                                <dt style="display:none;">JANコード</dt>

                                                <dd style="display:none;">
                                                    <asp:TextBox ID="txtjancode" runat="server"></asp:TextBox>

                                                </dd>
                                                <%--	     <dd><asp:CheckBox ID="chksearch" runat="server" />完全</dd>--%>


                                                <dt>販売管理コード </dt>


                                                <dd>
                                                    <asp:TextBox ID="txtsalemanagementcode" runat="server"></asp:TextBox>
                                                </dd>
                                                <dt>指示書番号</dt>
                                                <dd>
                                                    <asp:TextBox ID="txtinstrauctionno" runat="server"></asp:TextBox></dd>
                                            </dl>

                                        </div>
                                    </div>
                                    <%--  </div>--%>

                                    <%--
	  </ContentTemplate>
   </asp:UpdatePanel>--%>

                                    <p>
                                        <asp:Button ID="btnsearch" runat="server" Text="検 索" Width="128px" OnClientClick="target=''" ToolTip="Search" OnClick="btnsearch_Click" />

                                    </p>
                                    <%--         </ContentTemplate>--%>
                                    <%--      </asp:UpdatePanel>--%>
                                </asp:Panel>

                            </div>

                        </div>
                    </div>
                </div>
          <%--  </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ImageButton1" />
                <asp:AsyncPostBackTrigger ControlID="ImageButton2" />
                <asp:AsyncPostBackTrigger ControlID="btnsearch" />
            </Triggers>
        </asp:UpdatePanel>--%>
    </asp:Panel>
        <div id="CmnContents2">
         <%--   <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>--%>
                    <div id="ComBlock2">
                        <asp:HiddenField ID="hfoption" runat="server" />
                        <asp:HiddenField ID="hfCtrl" runat="server" Value="" />
                        <div class="widthhMax iconEx operationBtn">
                            <div class="stSet sksST shopST" style="margin-left: 372px;">
                                <p class="page"></p>ページ制作 
                                <p class="wait1"></p>出品待ち 
                                <p class="waitL"></p>期日出品待ち 
                                <p class="exhibit"></p>出品中   
                                <p class="ok1"></p>出品済
                                <p class="notok1"></p>価格なし /    
                                <p class="wait"></p>未掲載    
                                <p class="ok"></p>掲載中
                                <p class="deactive"></p>掲載無
                                <p class="warehouse"></p>倉庫
                                <p class="warehouseerror"></p>エラー/
                                <p class="mono"></p>出品済
                                <p class="nomono"></p>未出品
                            </div>
                            <br />
                            <br />
                            <p>
                                <asp:Button ID="btnselectall" runat="server" Text="全て選択" OnClientClick="target=''"
                                    OnClick="btnselectall_Click" Width="55px" />
                            </p>
                            <p>
                                <asp:Button ID="btncancelall" runat="server" Text="全て解除" OnClientClick="target=''"
                                    OnClick="btncancelall_Click" Width="55px" />
                            </p>
                            <p>
                                <asp:Button ID="btnexhibition" runat="server" Text="出品"
                                    OnClick="btnexhibition_Click" ToolTip="Exhibition" OnClientClick="CallTab(event);" Width="35px" />
                            </p>
                            <p style="margin-right: -10px;">
                                <asp:Button ID="btnwarehouse" runat="server" Text="倉庫"
                                    OnClick="btnwarehouse_Click" ToolTip="Warehouse" OnClientClick="CallTab(event);" Width="35px" />
                            </p>
                            <p>
                                <asp:Button ID="btndelete" runat="server" Text="削除" ToolTip="Delete" OnClientClick="Confirm()"
                                    OnClick="btndelete_Click" Width="32px" Visible="false" />
                            </p>
                            <%--   	<p>	<asp:Button ID="btndelete" runat="server" Text="削除" ToolTip="Delete" OnClientClick ="Confirm()" 
				 Width="32px"  /></p>	--%>
                            <p>
                                <asp:Label ID="Label3" runat="server" Text="ダウンロード項目"></asp:Label></p>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <p style="margin-left: -5px;">
                                        <asp:DropDownList ID="ddlname" runat="server"
                                            OnSelectedIndexChanged="ddlname_SelectedIndexChanged" AutoPostBack="true" Width="80px" Enabled="false">
                                        </asp:DropDownList>
                                    </p>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <p style="margin-left: -3px; margin-right: 0px;">
                                <asp:Button runat="server" ID="btnGenerate" Text="エクスポート" OnClick="btnGenerate_Click" OnClientClick="target=''"
                                    ToolTip="Export" Width="80px" />
                            </p>



                            <p class="itemPage">
                                <asp:DropDownList ID="ddlpage" runat="server" onchange="ddlpage_change(this,event);"
                                    OnSelectedIndexChanged="ddlpage_SelectedIndexChanged" AutoPostBack="true">
                                    <asp:ListItem>100</asp:ListItem>
                                     <asp:ListItem>50</asp:ListItem>
                                      <asp:ListItem>30</asp:ListItem>
                                </asp:DropDownList>
                            </p>

                            <p>
                                <asp:LinkButton ID="lnkdownload" runat="server" OnClick="lnkdownload_Click"></asp:LinkButton></p>
                        </div>
                        <asp:HiddenField ID="hfview" runat="server" />
                        <div class="itemCmnSetKnr resetValue editBox">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <br />
                                    <br />
                                    <div id="gvscroll" runat="server">
                                        <asp:GridView ID="gvItem" runat="server" AllowPaging="True" EmptyDataText="There is no data to display."
                                            EnableTheming="False" ForeColor="#333333" GridLines="None" CssClass="managementList listTable"
                                            ShowHeaderWhenEmpty="True" DataKeyNames="ID" OnPageIndexChanging="gvItem_PageIndexChanging"
                                            OnRowCommand="gvItem_RowCommand" AutoGenerateColumns="False" OnRowDataBound="gvItem_RowDataBound">

                                            <PagerSettings Visible="False" />
                                            <PagerStyle CssClass="paging" HorizontalAlign="Center" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="#" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblNo" Text='<%#Bind("No") %>' Width="50px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Width="45px">
                                                    <HeaderTemplate><asp:CheckBox ID="ckall" runat="server" OnCheckedChanged="ckall_Check" AutoPostBack="true" Visible="false" />対象</HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox runat="server" ID="ckItem" AutoPostBack="true" OnCheckedChanged="ckItem_Check" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                    <HeaderTemplate> SKS <br /> ST </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <p id="Ppage" runat="server" class="page"></p>
                                                        <p id="PWaitSt" runat="server" class="wait1"></p>
                                                        <p id="PWaitL" runat="server" class="waitL"></p>
                                                        <p id="PExhibit" runat="server" class="exhibit"></p>
                                                        <p id="POkSt" runat="server" class="ok1"></p>
                                                        <p id="PNOK" runat="server" class="notok1"></p>
                                                        <asp:Label ID="Label4" runat="server" Text='<%#Bind("SKSST") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                    <HeaderTemplate> SHOP <br /> ST </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <p id="PWait" runat="server" class="wait"></p>
                                                        <p id="POk" runat="server" class="ok"></p>
                                                        <p id="PDel" runat="server" class="del"></p>
                                                        <p id="PInactive" runat="server" class="deactive"></p>
                                                        <p id="PWarehouse" runat="server" class="warehouse"></p>
                                                        <p id="Warehouseerror" runat="server" class="warehouseerror"></p>
                                                        <asp:Label ID="Label6" runat="server" Text='<%#Bind("SHOP") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
                                                    <HeaderTemplate> M <br /> ST </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <p id="Mpage" runat="server" class="mono"></p>
                                                        <p id="MWaitSt" runat="server" class="nomono"></p>
                                                        <asp:Label ID="lblMStatus" runat="server" Text='<%#Bind("MCtrl_ID") %>' Visible="false"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="商品画像">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label45" runat="server" Text='<%#Bind("ID") %>' Visible="false"></asp:Label>
                                                        <asp:Label ID="Label46" runat="server" Text='<%#Bind("商品画像") %>' Visible="false"></asp:Label>
                                                        <asp:Image ID="imgitemimage" runat="server" ImageUrl='<%# Eval("商品画像").ToString().Length<3 ? "http://59.106.219.138/RCM_ORS/Item_Images/item_image/no_image.jpg": Eval("商品画像", "http://59.106.219.138/RCM_ORS/Item_Images/item_image/{0}")%>'
                                                            onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='default'" Width="80px" Height="80px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="商品番号">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblItemName" Text='<%# Eval("商品番号") %>' Visible="false" />
                                                        <asp:Label ID="lnkItemNo" runat="server" CssClass="labelAsLink" Text='<%# Eval("商品番号") %>' onclick="NewTab(this,event);"></asp:Label>
                                                        <%-- <asp:LinkButton ID="btnEdit" runat="server" CommandArgument='<%# Eval("商品番号") %>' 
        Text='<%# Eval("商品番号") %>' CommandName="DataEdit" ></asp:LinkButton>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="商品名">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label33" runat="server" Text='<%#Eval("商品名") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="定価（税抜）">
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblPrice" Style="text-align: right" runat="server" Text='<%#Eval("定価", "{0:#,###}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="販売価格（税抜）">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSalePrice" Style="text-align: right" runat="server" Text='<%#Eval("販売価格", "{0:#,###}" )%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="原価（税抜）">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCost" Style="text-align: right" runat="server" Text='<%#Eval("原価", "{0:#,###}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="送料">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label36" runat="server" Text='<%#Eval("送料") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="SKU">
                                                    <ItemTemplate>
                                                        <asp:Button ID="btnSKU" runat="server" Text="別窓表示" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ショップページ">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlshoppage" onchange="NewTabPreview(this,event);" Width="150px" runat="server">
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ブランド名">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("ブランド名") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="仕入先名">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label38" runat="server" Text='<%#Eval("仕入先名") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="年度">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label39" runat="server" Text='<%#Eval("年度") %>'>></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="シーズン">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label40" runat="server" Text='<%#Eval("シーズン") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="割引率">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label41" runat="server" Text='<%#Eval("割引率","{0:0.##}") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="原価率">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label42" runat="server" Text='<%#Eval("原価率","{0:0.##}") %>'></asp:Label>

                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="カタログ情報">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtcatInfo" runat="server" TextMode="MultiLine" ReadOnly="true" Text='<%#Eval("カタログ情報") %>' Font-Size="Small" ForeColor="#505050"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="競技名">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label43" runat="server" Text='<%#Eval("競技名") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="分類名">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Label44" runat="server" Text='<%#Eval("分類名") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                          <%--      <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalCount" runat="server" Text='<%# Eval("TotalCount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                <asp:BoundField DataField="ID" HeaderText="ID" />
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                     </ContentTemplate>
                            </asp:UpdatePanel>
                                   <%-- <div>
                                        <asp:ObjectDataSource ID="ObjectDataSource" EnablePaging="true" SelectCountMethod="TotalRowCount" SelectMethod="SearchItem_View2_Data" TypeName="ORS_RCM_BL.Item_Information_BL"
                                            StartRowIndexParameterName="startIndex" runat="server" OnSelecting="ObjectDataSource_Selecting" MaximumRowsParameterName="PageSize"></asp:ObjectDataSource>
                                        <asp:HiddenField runat="server" ID="hdfsearch" />
                                    </div>--%>
                                    <asp:Label runat="server" Style="display: none;" ID="hfNewTab" Text="1" />                           

                        </div>

                    </div>
                    <div class="btn">
                        <uc1:UCGrid_Paging ID="gp" runat="server" />
                    </div>
               <%-- </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnselectall" />
                    <asp:AsyncPostBackTrigger ControlID="btncancelall" />
                    <asp:PostBackTrigger ControlID="btnexhibition" />
                    <asp:PostBackTrigger ControlID="btnwarehouse" />
                    <asp:PostBackTrigger ControlID="btnGenerate" />
                    <asp:AsyncPostBackTrigger ControlID="ddlpage" />
                    <asp:PostBackTrigger ControlID="lnkdownload" />
                    <asp:AsyncPostBackTrigger ControlID="gvItem" />
                </Triggers>
            </asp:UpdatePanel>--%>
        </div>
    </body>
</asp:Content>
