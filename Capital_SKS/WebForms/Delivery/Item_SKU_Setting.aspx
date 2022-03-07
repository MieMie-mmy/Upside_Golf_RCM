<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_SKU_Setting.aspx.cs" Inherits="ORS_RCM.WebForms.Delivery.Item_SKU_Setting" %>

<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link rel="stylesheet" href="../../Styles/Item.css"  type="text/css"/>
<link rel="stylesheet" href="../../Styles/manager-style.css"  type="text/css"/>
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
    <script type="text/javascript">

        function ddlpage_change() {
            document.forms[0].target = "";
        }


</script>
    <script type="text/javascript">
        $(function () {
            $(".txtDateCss").datepicker({
                dateFormat: 'yy/mm/dd',
                autoclose: false
            });
        });
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
<div id="ComBlock">
<div class="setListBox inlineSet iconSet iconList">
		<h1>商品個別発送設定</h1>
<!-- Exhibition log search -->
		<div class="itemInfo resetValue searchBox iconEx" style="height:auto;" >
			<h2>商品個別発送設定</h2>
            <br />
            <dl>
               <%-- <dt style="width:150px;"><asp:Label runat="server" ID="lblBrandName" Text="ブランド名"></asp:Label></dt>
                <dd style="width:200px;"><asp:TextBox runat="server" ID="txtBrandName" TextMode="MultiLine"></asp:TextBox></dd>--%>                 
                <dt style="width:100px;"><asp:Label runat="server" ID="lblItemCode" Text="商品番号"></asp:Label></dt>
                <asp:CheckBox runat="server" ID="chkItemCode" AutoPostBack="true"/>
                <dd style="width:150px;"><asp:TextBox runat="server" ID="txtItemCode" TextMode="MultiLine"></asp:TextBox></dd>
                 <dt style="width:100px;"><asp:Label runat="server" ID="lblItemName" Text="商品名"></asp:Label></dt>
                <dd style="width:150px;"><asp:TextBox runat="server" ID="txtItemName" TextMode="MultiLine"></asp:TextBox></dd>
                 <dt style="width:100px;"><asp:Label runat="server" ID="lblBrandName" Text="ブランド名"></asp:Label></dt>
                <dd style="width:150px;"><asp:TextBox runat="server" ID="txtBrandName" TextMode="MultiLine"></asp:TextBox></dd>
                 <dt style="width:150px;"><asp:Button runat="server" ID="btnSearch" OnClick="btnSearch_Click" Text="検索" Width="78px"/></dt>
            </dl>
		</div>
</div><!--setListBox-->
    <div class="operationBtn">
           <p><asp:Button runat="server" ID="btnAllCheck" Text="全て選択" Width="78px" Onclick="btnAllCheck_Click"/>&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnAllCancel" Text="全て解除" Width="78px" OnClick="btnAllCancel_Click"/>&nbsp;&nbsp;&nbsp;
                <asp:Button runat="server" ID="btnUpdate" Text="更新" Width="78px" OnClick="btnUpdate_Click"/>
                <asp:Button runat="server" ID="btnGenerate" Text="エクスポート"  onclick="btnGenerate_Click"  OnClientClick="target=''"  
				ToolTip="Export"  Width="80px"/>
                 
           </p>
      
           <p class="itemPage">
               <asp:LinkButton ID="lnkdownload" runat="server" onclick="lnkdownload_Click" ></asp:LinkButton>
               <asp:FileUpload runat="server" ID="uplDelivery_Import"/>
               <asp:Button runat="server" Text="インポート開始" ID="btnImport" 
                        onclick="btnImport_Click"/>
			   <asp:DropDownList ID="ddlpage" runat="server" onchange="ddlpage_change(this,event);" AutoPostBack="true" OnSelectedIndexChanged="ddlpage_SelectedIndexChanged" CssClass="ddl" >
                <asp:ListItem>30</asp:ListItem>
		        <asp:ListItem>50</asp:ListItem>
		        <asp:ListItem>100</asp:ListItem>
		        </asp:DropDownList>
           </p>
         <p>
    </div>
   <div class="itemCmnSet itemInfo">
	
    <asp:GridView runat="server" ID="gdvItemSetting" AllowPaging="TRUE" ShowHeaderWhenEmpty="True" style="width:1180px;margin-left:-81px;"
                    OnRowDataBound="gdvItemSetting_RowDataBound" AutoGenerateColumns="false" CssClass="gdvInnerSetting" EmptyDataText="There is no data to display.">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>
                                <asp:CheckBox runat="server" ID="chk" AutoPostBack="true" OnCheckedChanged="chk_SKU"/>
         </ContentTemplate>
       </asp:UpdatePanel>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="商品番号">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblID" Text='<%#Eval("ID") %>' Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="lblItemCode" Text='<%#Eval("Item_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="商品名">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblItemName" Text='<%#Eval("Item_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="サイズコード">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSizeCode" Text='<%#Eval("Size_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="カラーコード">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblColorCode" Text='<%#Eval("Color_Code") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="サイズ">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblSizeName" Text='<%#Eval("Size_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="カラー">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="lblColorName" Text='<%#Eval("Color_Name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="楽天発送番号">
                            <ItemTemplate>
                               <asp:DropDownList runat="server" ID="ddlRShipping"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ヤフー発送番号">
                            <ItemTemplate>
                                <asp:DropDownList runat="server" ID="ddlYShipping"></asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <%--  <asp:TemplateField HeaderText="元の設定に戻す日">
                            <ItemTemplate>
                               <asp:TextBox ID="txteddate" runat="server" Text='<%#Eval("Delivery_SettingDate") %>' Width="100" class="txtDateCss" AutoPostBack="false"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                    </Columns>
            <PagerSettings Visible="false" />
                </asp:GridView>
      
     </div>
     <!-- /exbition list -->
   <uc1:UCGrid_Paging  runat="server"  ID="gp"/><!-- /List paging -->
</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>