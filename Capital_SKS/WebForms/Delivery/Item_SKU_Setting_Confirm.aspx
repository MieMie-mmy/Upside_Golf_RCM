<%@ Page Language="C#" MasterPageFile="~/Site.Master"  AutoEventWireup="true" CodeBehind="Item_SKU_Setting_Confirm.aspx.cs" Inherits="ORS_RCM.WebForms.Delivery.Item_SKU_Setting_Confirm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/javascript"></script>
	<%--<link href="../../Scripts/jquery.page-scroller.js" />--%>

	<title>商品管理システム＜商品マスタデータインポート確認＞</title>

    <style type="text/css"> 
    .gvPagerCss span
    {
        background-color:#DEE1E7;
       /* font-size:15px;*/
    }  
    .gvPagerCss td
    {
        padding-left: 5px;   

        padding-right: 5px;  
    }
</style>
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).scrollTop();
        });

        //    function ToTopOfPage(sender, args) {
        //                    setTimeout("window.scrollTo(0, 0)", 0);
        //}
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfdmsg" runat="server" />
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox iconSet iconCheck editBox">

	<h1>商品マスタデータインポート 確認</h1>
	<p class="attText">下記内容で間違いなければ「更新」ボタンを押してください</p>
	<div class="dbCmnSet editBox">
	<form action="#" method="get">
		

		<!-- SKU Master -->
		<h2 id="headerdelivery" runat="server">Delivery_Setting_Comfrim</h2>
		<div id="divdelivery" runat="server">
			<asp:GridView ID="gvdelivery" runat="server" CellPadding="6" ForeColor="#333333" AllowPaging="true"
		GridLines="None" AutoGenerateColumns="False" CssClass="listTable" 
                onpageindexchanging="gvdelivery_PageIndexChanging" >
		  <Columns>
                <asp:BoundField DataField="チェック" HeaderText=" チェック "/>
              <asp:TemplateField>
                <HeaderTemplate>商品番号</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:150px" ID="lblColorName" Text='<%# Eval("Item_Code") %>' ></asp:Label> </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate>サイズ名</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:150px" ID="lblColorName" Text='<%# Eval("Size_Name") %>' ></asp:Label> </ItemTemplate>
            </asp:TemplateField>
              <asp:TemplateField>
                <HeaderTemplate>カラー名</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:150px" ID="lblColorName" Text='<%# Eval("Color_Name") %>' ></asp:Label> </ItemTemplate>
            </asp:TemplateField>
		<asp:TemplateField>
                <HeaderTemplate>サイズコード</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:150px" ID="lblColorName" Text='<%# Eval("Color_Code") %>' ></asp:Label> </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
                <HeaderTemplate>カラーコード</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:150px" ID="lblColorName" Text='<%# Eval("Size_Code") %>' ></asp:Label> </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField>
                <HeaderTemplate>楽天発送番号</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:150px" ID="lblColorName" Text='<%# Eval("Rakuten_ShippingNo") %>' ></asp:Label> </ItemTemplate>
            </asp:TemplateField>
               <asp:TemplateField>
                <HeaderTemplate>ヤフー発送番号</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:150px" ID="lblColorName" Text='<%# Eval("Yahoo_ShippingNo") %>' ></asp:Label> </ItemTemplate>
            </asp:TemplateField>
               <asp:BoundField DataField="エラー内容" HeaderText="エラー内容" />
		</Columns>
	</asp:GridView>
		</div>
        	<div class="btn"><asp:Button runat="server" ID="btnUpdate" Text="更 新" OnClick="btnUpdate_Click" /></div>
	</form>
	</div>
	</div><!--setDetailBox-->
	</div><!--ComBlock-->
	</div><!--CmnContents-->
</asp:Content>