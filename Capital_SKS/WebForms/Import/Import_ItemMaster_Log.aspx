<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_ItemMaster_Log.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Import_ItemMaster_Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/database.css" rel="stylesheet" type="text/css" />
    <link href="../../Scripts/jquery.page-scroller.js" rel="stylesheet" type="text/css" />

	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />
    <script type="text/javascript">
        $(document).ready(function () {
            $(window).scrollTop();
        });

        
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox iconSet iconLog editBox">

	<h1>商品マスタデータインポートログ</h1>

	<div class="dbCmnSet editBox logDb">
    <h2 id="headermaster" runat="server">商品マスタ</h2>
    <div id="divmaster" runat="server" class="listTableOver onOver">
        <asp:GridView ID="gvmaster" runat="server" AutoGenerateColumns="False" 
            CssClass="listTable itemDb" onrowdatabound="gvmaster_RowDataBound" 
            onpageindexchanging="gvmaster_PageIndexChanging" AllowPaging="True"  PageSize="30">
        <Columns>
        <asp:TemplateField HeaderText="ID" Visible="false">
        <ItemTemplate>
            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="チェック">
        <ItemTemplate>
            <asp:Label ID="Label2" runat="server" Text='<%#Eval("チェック") %>' ForeColor="Red"></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField>
             <HeaderTemplate>販売商品<br />管理番号</HeaderTemplate>
        <ItemTemplate>
            <asp:Label ID="Label3" runat="server" Text='<%#Eval("Item_AdminCode") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <%--<asp:TemplateField HeaderText="販売商品 管理番号">
        <ItemTemplate>
            <asp:Label ID="Label4" runat="server" Text=""></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>--%>
             <asp:TemplateField HeaderText="商品番号">
        <ItemTemplate>
            <asp:Label ID="Label5" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="商品名">
        <ItemTemplate>
            <asp:Label ID="Label6" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="定価">
        <ItemTemplate>
<%--            <asp:Label ID="Label7" runat="server" Text='<%#Eval("List_Price") %>'></asp:Label>--%>
            <asp:Label ID="Label7" runat="server" Text='<%#Eval("List_Price") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="販売価格" >
        <ItemTemplate>
            <asp:Label ID="Label8" runat="server" Text='<%#Eval("Sale_Price") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="原価">
        <ItemTemplate>
            <asp:Label ID="Label9" runat="server" Text='<%#Eval("Cost") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="発売日">
        <ItemTemplate>
            <asp:Label ID="Label10" runat="server" Text='<%#Eval("Release_Date") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="掲載可能日">
        <ItemTemplate>
            <asp:Label ID="Label11" runat="server" Text='<%#Eval("Post_Available_Date") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="承認日">
        <ItemTemplate>
            <asp:Label ID="Label11" runat="server" Text='<%#Eval("Approve_Date") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="年度">
        <ItemTemplate>
            <asp:Label ID="Label12" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="シーズン">
        <ItemTemplate>
            <asp:Label ID="Label13" runat="server" Text='<%#Eval("Season") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="ブランド">
        <ItemTemplate>
            <asp:Label ID="Label14" runat="server" Text='<%#Eval("Brand_Name") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        
             <asp:TemplateField >
             <HeaderTemplate>ヤフー<br />ブランドコード</HeaderTemplate>
        <ItemTemplate>
            <asp:Label ID="Label15" runat="server" Text='<%#Eval("Brand_Code_Yahoo") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="競技">
        <ItemTemplate>
            <asp:Label ID="Label16" runat="server" Text='<%#Eval("Competition_Name") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="分類">
        <ItemTemplate>
            <asp:Label ID="Label17" runat="server" Text='<%#Eval("Class_Name") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
             <asp:TemplateField HeaderText="カタログ情報">
        <ItemTemplate>
            <asp:Label ID="Label18" runat="server" Text='<%#Eval("Catalog_Info") %>'></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="エラー内容">
        <ItemTemplate>
            <asp:Label ID="Label19" runat="server" Text='<%#Eval("Error_Message") %>' ForeColor="Red"></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
        </Columns>
        </asp:GridView>

    
      

    </div>

    <h2 id="headersku" runat="server">SKUマスタ</h2>
			<div id="divsku" runat="server" class="listTableOver noWide">
              <asp:GridView ID="gvsku" runat="server" AutoGenerateColumns="False" 
                    CssClass="listTable" onrowdatabound="gvsku_RowDataBound" 
                    onpageindexchanging="gvsku_PageIndexChanging" AllowPaging="True" PageSize="30">
                  <Columns>
                      

                      <asp:TemplateField HeaderText="チェック">
                      <ItemTemplate>
                          <asp:Label ID="Label20" runat="server" Text='<%#Eval("チェック") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>

                      <asp:TemplateField HeaderText="販売商品管理番号">
                      <ItemTemplate>
                          <asp:Label ID="Label21" runat="server" Text='<%#Eval("Item_AdminCode") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="商品番号">
                      <ItemTemplate>
                          <asp:Label ID="Label22" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="サイズ名">
                      <ItemTemplate>
                          <asp:Label ID="Label23" runat="server" Text='<%#Eval("Size_Name") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="カラー名">
                      <ItemTemplate>
                          <asp:Label ID="Label24" runat="server" Text='<%#Eval("Color_Name") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="サイズコード">
                      <ItemTemplate>
                          <asp:Label ID="Label25" runat="server" Text='<%#Eval("Size_Code") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="カラーコード">
                      <ItemTemplate>
                          <asp:Label ID="Label26" runat="server" Text='<%#Eval("Color_Code") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="JANコード">
                      <ItemTemplate>
                          <asp:Label ID="Label27" runat="server" Text='<%#Eval("JAN_Code") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                      
                      <asp:TemplateField HeaderText="エラー内容">
                      <ItemTemplate>
                          <asp:Label ID="Label28" runat="server" Text='<%#Eval("Error_Message") %>'></asp:Label>
                      </ItemTemplate>
                      </asp:TemplateField>
                  </Columns>

        </asp:GridView>
        </div>

        <h2 id="headerinventory" runat="server">在庫データ</h2>
			<div id="divinventory" runat="server" class="listTableOver noWide">
                <asp:GridView ID="gvInv" runat="server" AutoGenerateColumns="False" 
                    CssClass="listTable" onrowdatabound="gvInv_RowDataBound" 
                    onpageindexchanging="gvInv_PageIndexChanging" AllowPaging="True" PageSize="30">
                <Columns>
                
                    <asp:TemplateField HeaderText="チェック">
                    <ItemTemplate>
                        <asp:Label ID="Label29" runat="server" Text='<%#Eval("チェック") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                      
                    <asp:TemplateField HeaderText="販売商品管理番号">
                    <ItemTemplate>
                        <asp:Label ID="Label30" runat="server" Text='<%#Eval("Item_AdminCode") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
               
                    <asp:TemplateField HeaderText="商品番号">
                    <ItemTemplate>
                        <asp:Label ID="Label31" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                
                    <asp:TemplateField HeaderText="サイズ名">
                    <ItemTemplate>
                        <asp:Label ID="Label32" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                
                    <asp:TemplateField HeaderText="カラー名">
                    <ItemTemplate>
                        <asp:Label ID="Label33" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                
                    <asp:TemplateField HeaderText="サイズコード">
                    <ItemTemplate>
                        <asp:Label ID="Label34" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                
                    <asp:TemplateField HeaderText="カラーコード">
                    <ItemTemplate>
                        <asp:Label ID="Label35" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="在庫数">
                    <ItemTemplate>
                        <asp:Label ID="Label36" runat="server" Text='<%#Eval("Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="自社在庫数">
                    <ItemTemplate>
                        <asp:Label ID="Label43" runat="server" Text='<%#Eval("Jisha_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="モーカー在庫数">
                    <ItemTemplate>
                        <asp:Label ID="Label38" runat="server" Text='<%#Eval("Maker_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="豊中在庫数">
                    <ItemTemplate>
                        <asp:Label ID="Label39" runat="server" Text='<%#Eval("Toyonaka_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="石橋在庫数">
                    <ItemTemplate>
                        <asp:Label ID="Label40" runat="server" Text='<%#Eval("Ishibashi_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="江坂在庫数">
                    <ItemTemplate>
                        <asp:Label ID="Label41" runat="server" Text='<%#Eval("Esaka_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="三宮在庫数">
                    <ItemTemplate>
                        <asp:Label ID="Label42" runat="server" Text='<%#Eval("Sannomiya_Quantity") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="エラー内容">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Error_Message") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>

                </Columns>
                </asp:GridView>
            </div>

            <div id="divtagID" class="dbitem_confirm" runat="server" >
             <h2 id="headertagID" runat="server">楽天タグID</h2>
                <asp:GridView ID="gdvTagID" runat="server"  CssClass="listTable" onpageindexchanging="gdvTagID_PageIndexChanging" 
                    AllowPaging="True" PageSize="2000" AutoGenerateColumns="False" Width="1700px" style="text-align:center;" visible="false">
                <Columns>
                    <asp:BoundField DataField="Item_Code" HeaderText=" 商品番号 " ItemStyle-Width="160px" />
                    <asp:BoundField DataField="Item_Name" HeaderText=" 商品名 " HeaderStyle-Width="230px"/>
			        <asp:BoundField DataField="Size_Name" HeaderText=" サイズ名 " HeaderStyle-Width="60px"/>
			        <asp:BoundField DataField="Color_Name" HeaderText=" カラー名 " HeaderStyle-Width="170px"/> 
                    <asp:BoundField DataField="Rakuten_CategoryID" HeaderText="ディレクトリID" HeaderStyle-Width="40px"/>
                    <asp:BoundField DataField="Tag_Name1" HeaderText=" タグ名1 " HeaderStyle-Width="108px"/>
			        <asp:BoundField DataField="Tag_Name2" HeaderText=" タグ名2 " HeaderStyle-Width="108px"/>
			        <asp:BoundField DataField="Tag_Name3" HeaderText=" タグ名3 " HeaderStyle-Width="108px"/>
                    <asp:BoundField DataField="Tag_Name4" HeaderText=" タグ名4 " HeaderStyle-Width="108px" />
                    <asp:BoundField DataField="Tag_Name5" HeaderText=" タグ名5 " HeaderStyle-Width="108px"/>
                    <asp:BoundField DataField="Tag_Name6" HeaderText=" タグ名6 " HeaderStyle-Width="108px"/>
                    <asp:BoundField DataField="Tag_Name7" HeaderText=" タグ名7 " HeaderStyle-Width="108px"/>
                    <asp:BoundField DataField="Tag_Name8" HeaderText=" タグ名8 "  ItemStyle-Width="100px"/>
                    <asp:BoundField DataField="Error_Message" HeaderText=" エラー内容 "  ItemStyle-Width="100px"/>

                </Columns>
                </asp:GridView>
            </div>
        <h2 id="headermonotaro" runat="server">ものたろ</h2>
        <div id="divmonotaro" runat="server" class="listTableOver noWide">
			<asp:GridView ID="gvmonotaro" runat="server" AllowPaging="true" onrowdatabound="gvMon_RowDataBound" 
                    onpageindexchanging="gvMon_PageIndexChanging" AutoGenerateColumns= "false"  CssClass="listTable" PageSize="30">
		        <Columns>
                  <asp:TemplateField HeaderText="チェック">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("チェック") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品番号">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品名">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="エラー内容">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Error_Message") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="JANコード">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("JAN_Code") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(メーカー名)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Brand_Name") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(年間出荷数もしくは売れ筋A～Dランク)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Selling_Rank") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="配送種別">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Delivery_Type") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(笠間納品可否)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Customer_Delivery_Type") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(笠間納品入荷日数)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Customer_Delivery_Day") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="入荷日数">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Delivery_Day") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                      <asp:TemplateField HeaderText="出荷日数">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Day_Ship") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="返品承認要否">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Return_Necessary") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="倉庫コード">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Warehouses_Code") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="お客様組立て">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Customer_Assembly") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="引渡方法">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Delivery_Method") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="代引可否">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Cash_On_Delivery_Fee") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="返品可否">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Return_Type") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(全国)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Nation_Wide") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(北海道)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Hokkaido") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(沖縄)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Okinawa") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(離島)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Remote_Island") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(市場売価)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Market_Selling_Price") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(メーカー名)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Maker_Name") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="仕入価格">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Purchase_Price") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="(直送時配送不可地域)">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Undelivered_Area") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="商品詳細登録コメント">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Item_Details_Registration_Comment") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="該当法令">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Applicable_Law") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="販売許可・認可・届出">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Sales_Permission") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="賞味期限">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Sell_By") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="法令・規格">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Laws_And_Regulation") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="消防法上、届出を必要とする物質">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Fire_Service_Law") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="危険物の種別">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Dangarous_Goods") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="危険物の品名">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Dangarous_Goods_Name") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="危険等級">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Risk_Rating") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="危険物の性質">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Dangerous_Goods_Nature") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="危険物の含有量">
                    <ItemTemplate>
                        <asp:Label ID="Label37" runat="server" Text='<%#Eval("Dangerous_Goods_Contents") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
	        </asp:GridView>
		</div>
</div>
</div><!--setDetailBox-->
</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
