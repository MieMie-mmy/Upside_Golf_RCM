<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_Item_Confirm.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Import_Item_Confirm" MaintainScrollPositionOnPostback="false"%>

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
		<!-- Item Master -->
		<h2 id="headermaster" runat="server">商品マスタ</h2>
			<div id="divmaster" runat="server" class="listTableOver onOver">
			<asp:GridView ID="gvitemmaster" runat="server" AutoGenerateColumns="False" CellPadding="6" AllowPaging="true" OnPageIndexChanging="gvitemmaster_PageIndexChanging" 
            EmptyDataText="There is no data to display!" GridLines="None" ShowHeaderWhenEmpty="True" CssClass="listTable itemDb">
			<Columns>
			<%--<asp:TemplateField HeaderStyle-Width="30px" ControlStyle-Width="30px">
				<HeaderTemplate>
					チェック
				</HeaderTemplate>
				<ItemTemplate >
					<asp:Label ID="lblErr" runat="server" Text='<%# Eval("チェック") %>' style="float:left"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>--%>
			<%--<asp:BoundField DataField="Ctrl_ID" HeaderText="販売商品管理番号" />--%>
            <asp:BoundField DataField="チェック" HeaderText="チェック"/>
			<%--<asp:BoundField DataField="Item_AdminCode" HeaderText="販売商品管理番号"/>--%>
            <asp:TemplateField>
                <HeaderTemplate>販売商品<br />管理番号</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" Text='<%# Eval("Item_AdminCode") %>'></asp:Label> </ItemTemplate>
            </asp:TemplateField>
			<asp:BoundField DataField="Item_Code" HeaderText="商品番号" />
			<%--<asp:BoundField DataField="Item_Name" HeaderText="商品名" >--%>
            <%--</asp:BoundField>--%>
            <asp:TemplateField>
                <HeaderTemplate>商品名</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:300px" ID="lblItemName" Text='<%# Eval("Item_Name") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderStyle Width="50px" />
            <ItemStyle Width="50px" HorizontalAlign="Center" />
            <FooterStyle Width="50px" />
            <HeaderTemplate>定価</HeaderTemplate>
            <ItemTemplate><asp:Label runat="server" style="width:50px; text-align:center" ID="lblListPrice" Text='<%# Eval("List_Price") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
			<asp:BoundField DataField="Sale_Price" HeaderText="販売価格" />
			<asp:BoundField DataField="Cost" HeaderText="原価" />
			<asp:BoundField DataField="Release_Date" HeaderText="    発売日   " />
			<asp:BoundField DataField="Post_Available_Date" HeaderText="掲載可能日" />
			<asp:BoundField DataField="YEAR" HeaderText="年度" />
			<asp:BoundField DataField="Season" HeaderText="シーズン" />
			<%--<asp:BoundField DataField="Brand_Code" HeaderText="ブランドコード" />--%>
			<asp:BoundField DataField="Brand_Name" HeaderText="   ブランド   " />
			<%--<asp:BoundField DataField="Brand_Code_Yahoo" HeaderText="ヤフーブランドコード" />--%>
            <asp:TemplateField>
                <HeaderTemplate>   ヤフー   <br />ブランドコード</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" ID="lblyahooBrandCode" Text='<%# Eval("Brand_Code_Yahoo") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
			<%--<asp:BoundField DataField="Competition_Code" HeaderText="競技コード" />--%>
			<%--<asp:BoundField DataField="Competition_Name" HeaderText="競技名" /> --%>    
            <asp:TemplateField>
                <HeaderTemplate>競技</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" ID="lblCompetitionName" style="width:150px" Text='<%# Eval("Competition_Name") %>'></asp:Label></ItemTemplate>
            </asp:TemplateField>
                   
			<%--<asp:BoundField DataField="Class_Code" HeaderText="分類コード" />--%>
			<asp:BoundField DataField="Class_Name" HeaderText="分類" />
			<%--<asp:BoundField DataField="Company_name" HeaderText="仕入先名" />--%>
			<asp:BoundField DataField="Catalog_Information" HeaderText="カタログ情報" />
			<%--<asp:BoundField DataField="Special_Flag" HeaderText="特記フラグ" />
			<asp:BoundField DataField="Reservation_Flag" HeaderText="予約フラグ" />
			<asp:BoundField DataField="Instruction_No" HeaderText="指示書番号" />
			<asp:BoundField DataField="Approve_Date" HeaderText="承認日" />
			<asp:BoundField DataField="Remarks" HeaderText="備考" />
			<asp:BoundField DataField="Product_Code" HeaderText="メーカー商品コード" />--%>
            <asp:BoundField DataField="Sales_unit" HeaderText="販売単位" />
            <asp:BoundField DataField="Content_quantity_number_1" HeaderText="内容量数1" />
            <asp:BoundField DataField="Contents_unit_1" HeaderText="内容量単位1" />
            <asp:BoundField DataField="Content_quantity_number_2" HeaderText="内容量数2" />
            <asp:BoundField DataField="Contents_unit_2" HeaderText="内容量単位2" />
			<asp:BoundField DataField="エラー内容" HeaderText="エラー内容" />
		</Columns>
		        <PagerStyle HorizontalAlign="Left" />
		</asp:GridView>
		</div>

		<!-- SKU Master -->
		<h2 id="headersku" runat="server">SKUマスタ</h2>
		<div id="divsku" runat="server">
			<asp:GridView ID="gvsku" runat="server" CellPadding="6" ForeColor="#333333" AllowPaging="true"
		GridLines="None" AutoGenerateColumns="False" CssClass="listTable" 
                onpageindexchanging="gvsku_PageIndexChanging" >
		  <Columns>
			<%--<asp:TemplateField  >
				<HeaderTemplate>
					<asp:Label ID="Label2" runat="server" Text="チェック"></asp:Label>
				</HeaderTemplate>
				<ItemTemplate >
					<asp:Label ID="lblErrMsg" runat="server" Text='<%# Eval("チェック") %>' Width="100px"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>--%>
            <asp:BoundField DataField="チェック" HeaderText=" チェック "/>
			<asp:BoundField DataField="Item_AdminCode" HeaderText="     販売商品管理番号     "/>
			<asp:BoundField DataField="Item_Code" HeaderText="   商品番号   "/>
            <asp:BoundField DataField="Size_Name" HeaderText="   サイズ名   " />
			<%--<asp:BoundField DataField="Color_Name" HeaderText="       カラー名       "/>--%>
            <asp:TemplateField>
                <HeaderTemplate>カラー名</HeaderTemplate>
                <ItemTemplate><asp:Label runat="server" style="width:150px" ID="lblColorName" Text='<%# Eval("Color_Name") %>' ></asp:Label> </ItemTemplate>
            </asp:TemplateField>			
			<asp:BoundField DataField="Size_Code" HeaderText="   サイズコード   " />
            <asp:BoundField DataField="Color_Code" HeaderText="   カラーコード   " />
			<asp:BoundField DataField="JAN_Code" HeaderText="JANコード" />		
            <asp:BoundField DataField="エラー内容" HeaderText="エラー内容" />
			<%--<asp:TemplateField Visible="false"  >
				<HeaderTemplate>
					<asp:Label ID="Label1" runat="server" Text="エラー内容"></asp:Label>
				</HeaderTemplate>
				<ItemTemplate >
					<asp:Label ID="lblErrMsg" runat="server" Text='<%# Eval("エラー内容") %>' Width="100px"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>   --%>       
		</Columns>
	</asp:GridView>
		</div>

		<!-- Zaiko Master -->
		<h2 id="headerinventory" runat="server">在庫データ</h2>
		<div id="divinventory" runat="server" class="listTableOver noWide">
			<asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="False" CssClass="listTable" 
		EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" AllowPaging="true" OnPageIndexChanging="gvInventory_PageIndexChanging"
		CellPadding="4" ForeColor="#333333" GridLines="None"  Width="700px" >
		 <Columns>
		 <%--<asp:TemplateField>
			<HeaderTemplate>
				<asp:Label ID="Label3" runat="server" Text="チェック"></asp:Label>
			</HeaderTemplate>
			<ItemTemplate >
				<asp:Label ID="lblMsg" runat="server" Text='<%# Eval("チェック") %>' Width="100px"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>--%>
        <asp:BoundField DataField="チェック" HeaderText=" チェック "/>
		<asp:BoundField DataField="Item_AdminCode" HeaderText="販売管理番号" HeaderStyle-HorizontalAlign="Left" />
		<asp:BoundField DataField="Item_Code" HeaderText="商品番号" HeaderStyle-HorizontalAlign="Left" />
		<asp:BoundField DataField="Quantity" HeaderText="在庫数" HeaderStyle-HorizontalAlign="Left"  />    
        <asp:BoundField DataField="エラー内容" HeaderText="エラー内容" />    
<%--		<asp:TemplateField>
			<HeaderTemplate>
				<asp:Label ID="Label4" runat="server" Text="エラー内容"></asp:Label>
			</HeaderTemplate>
			<ItemTemplate >
				<asp:Label ID="lblMsg" runat="server" Text='<%# Eval("エラー内容") %>' Width="100px"></asp:Label>
			</ItemTemplate>
		</asp:TemplateField> --%>   
		</Columns>
	</asp:GridView>
		</div>

        <!-- added by ETZ for sks-390 TagID -->
		
		<div id="divtagID" class="dbitem_confirm" runat="server" >

            <h2 id="headertagID" runat="server">楽天タグID</h2>
			<asp:GridView ID="gdvTagID" runat="server" CellPadding="6" ForeColor="#333333" AllowPaging="true" 
		GridLines="None" AutoGenerateColumns="False" CssClass="listTable" width="1700px"  onpageindexchanging="gdvTagID_PageIndexChanging"
                PageSize="2000">
		  <Columns>
            <asp:BoundField DataField="Item_Code" HeaderText=" 商品番号 " 
                  HeaderStyle-Width="160px">
<HeaderStyle Width="160px"></HeaderStyle>
              </asp:BoundField>
            <asp:BoundField DataField="Item_Name" HeaderText=" 商品名 " 
                  HeaderStyle-Width="230px">
<HeaderStyle Width="230px"></HeaderStyle>
              </asp:BoundField>
			<asp:BoundField DataField="Size_Name" HeaderText=" サイズ名 " 
                  HeaderStyle-Width="60px">
<HeaderStyle Width="60px"></HeaderStyle>
              </asp:BoundField>
			<asp:BoundField DataField="Color_Name" HeaderText=" カラー名 " 
                  HeaderStyle-Width="170px"> 
<HeaderStyle Width="170px"></HeaderStyle>
              </asp:BoundField>
            <asp:BoundField DataField="Rakuten_CategoryID" HeaderText="ディレクトリID" 
                  HeaderStyle-Width="40px">
<HeaderStyle Width="40px"></HeaderStyle>
              </asp:BoundField>
            <asp:BoundField DataField="Tag_Name1" HeaderText=" タグ名1 " 
                  HeaderStyle-Width="108px">
<HeaderStyle Width="108px"></HeaderStyle>
              </asp:BoundField>
			<asp:BoundField DataField="Tag_Name2" HeaderText=" タグ名2 " 
                  HeaderStyle-Width="108px">
<HeaderStyle Width="108px"></HeaderStyle>
              </asp:BoundField>
			<asp:BoundField DataField="Tag_Name3" HeaderText=" タグ名3 " 
                  HeaderStyle-Width="108px">
<HeaderStyle Width="108px"></HeaderStyle>
              </asp:BoundField>
            <asp:BoundField DataField="Tag_Name4" HeaderText=" タグ名4 " 
                  HeaderStyle-Width="108px" >
<HeaderStyle Width="108px"></HeaderStyle>
              </asp:BoundField>
            <asp:BoundField DataField="Tag_Name5" HeaderText=" タグ名5 " 
                  HeaderStyle-Width="108px">
<HeaderStyle Width="108px"></HeaderStyle>
              </asp:BoundField>
            <asp:BoundField DataField="Tag_Name6" HeaderText=" タグ名6 " 
                  HeaderStyle-Width="108px">
<HeaderStyle Width="108px"></HeaderStyle>
              </asp:BoundField>
            <asp:BoundField DataField="Tag_Name7" HeaderText=" タグ名7 " 
                  HeaderStyle-Width="108px">
<HeaderStyle Width="108px"></HeaderStyle>
              </asp:BoundField>
            <asp:BoundField DataField="Tag_Name8" HeaderText=" タグ名8 "  
                  HeaderStyle-Width="108px">
	  
<HeaderStyle Width="108px"></HeaderStyle>
              </asp:BoundField>
	  
		</Columns>
	            <PagerStyle HorizontalAlign="Left" CssClass="gvPagerCss" />
	</asp:GridView>
		</div>

        	<div id="divmonotaro" class="dbitem_confirm" runat="server" style ="width :1300px;height :400px;overflow :scroll " >

            <h2 id="h1" runat="server">MonotaroImport</h2>
			<asp:GridView ID="gvmonotaro" runat="server" CellPadding="6" ForeColor="#333333" AllowPaging="true" 
		GridLines="None" AutoGenerateColumns= "true"  CssClass="listTable" width="1700px"  onpageindexchanging="gvmonotaro_PageIndexChanging" EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True"
                
                PageSize="2000">
		  <Columns>
           
		</Columns>
	            <PagerStyle HorizontalAlign="Left" CssClass="gvPagerCss" />
	</asp:GridView>
		</div>
		<div class="btn"><asp:Button runat="server" ID="btnUpdate" Text="更 新" OnClick="btnUpdate_Click" /></div>

      <%-- <div class="btn"> <asp:Button runat="server" ID="Button1" Text="Test" onClientclick="ScrollToTop()" /></div>--%>
	</form>
	</div>
	</div><!--setDetailBox-->
	</div><!--ComBlock-->
	</div><!--CmnContents-->
</asp:Content>
