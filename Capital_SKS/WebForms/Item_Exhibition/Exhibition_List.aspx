<%@ Page Title="商品管理システム＜出品一覧＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exhibition_List.aspx.cs" Inherits="ORS_RCM.WebForms.Item_Exhibition.Exhibition_List" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  

<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
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
            var drp5 = document.getElementById("<%=ddlbatchcheck.ClientID%>");
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


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<asp:HiddenField ID="hdfFromDate" runat="server" />
	<asp:HiddenField ID="hdfToDate" runat="server" />



<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox inlineSet iconSet iconShop">
		<h1>出品一覧</h1>

<!-- Exhibition log search -->
		<div class="exbCmnSet resetValue searchBox">

			<h2>出品検索</h2>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
<dl>
<dt>商品番号<br><asp:CheckBox ID="chkitemcode" runat="server" />完全</dt>
<%--	<asp:Label ID="Label10" runat="server" Text="商品番号" Width="50px" 
		ToolTip="Item_Number"></asp:Label>--%>

<dd>	<asp:TextBox TextMode ="MultiLine" ID="txtcode" runat="server" ></asp:TextBox>
</dd>

	<dt><asp:Label ID="Label11" runat="server" Text="商品名" ToolTip="Product Name "></asp:Label></dt>

<dd><asp:TextBox ID="txtproname" runat="server" ></asp:TextBox></dd>	
<dt title="Catalog information">カタログ情報</dt>
<dd>  <asp:TextBox ID="txtcatinfo" runat="server" ></asp:TextBox></dd>
	<dt title="Brand name">ブランド名</dt>
	<dd>  <asp:TextBox ID="txtbrandname" runat="server" ></asp:TextBox></dd>
	<dt title="CompanyName">仕入先名</dt>
	<dd><asp:TextBox ID="txtcompname" runat="server" ></asp:TextBox></dd>
	<dt title="Competition name">競技名</dt>
	<dd><asp:TextBox ID="txtcompetitionname" runat="server" ></asp:TextBox></dd>	
	<dt title="Class name">分類名</dt>
	<dd><asp:TextBox ID="txtclassname" runat="server" ></asp:TextBox></dd>
	<dt title="Year">年度</dt>
	<dd><asp:TextBox ID="txtyear" runat="server" ></asp:TextBox></dd>	
	<dt title="Season">シーズン</dt>
	<dd><asp:TextBox ID="txtseason" runat="server"></asp:TextBox></dd>
	<dt title="Exhibition date and time">出品日時</dt>

<dd class="cal">
<asp:UpdatePanel runat="server" ID="uppnl1">
<ContentTemplate>
<asp:TextBox ID="txtexdatetime1" runat="server" ReadOnly="True"></asp:TextBox>
<asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom" /> &nbsp; ~
				<asp:TextBox ID="txtdatetime2" runat="server" ReadOnly="True"></asp:TextBox>
			<asp:ImageButton ID="ImageButton2" runat="server" Width="15px" Height="15px"
			ImageUrl="~/Styles/clear.png" onclick="ImageButton2_Click" ImageAlign="AbsBottom" />
</ContentTemplate>
</asp:UpdatePanel>
</dd>
	<%--
<asp:TextBox ID="txtexdatetime1" runat="server" Width="118px"></asp:TextBox>

	<asp:TextBox ID="txtdatetime2" runat="server" Width="118px"></asp:TextBox>--%>
	<dt><asp:Label ID="Label20" runat="server" Text="出品者 " ToolTip="Exhibitor "></asp:Label></dt>

	<dd><asp:DropDownList ID="ddlexhibitor" runat="server" >
	  
		</asp:DropDownList></dd>	

		<dt><asp:Label ID="Label21" runat="server" Text="出品モール" ToolTip="Exhibition Mall "></asp:Label></dt>

				<dd>	<asp:DropDownList ID="ddlmall" runat="server" >
                   
					</asp:DropDownList></dd>

		<dt><asp:Label ID="Label22" runat="server" Text="備考" ToolTip="Remarks "></asp:Label></dt>

		<dd><asp:TextBox ID="txtremark" runat="server" ></asp:TextBox></dd>

		<dt><asp:Label ID="Label23" runat="server" Text="出品結果エラー" 
			ToolTip="Exhibition result error "></asp:Label></dt>

		<dd><asp:DropDownList ID="ddlexbresulterror" runat="server" >
		<asp:ListItem></asp:ListItem>
		<asp:ListItem Value="2">○</asp:ListItem>
		<asp:ListItem Value="1">×</asp:ListItem>
		<asp:ListItem Value="0">未</asp:ListItem>
		<asp:ListItem Value="3">対象外</asp:ListItem>
		</asp:DropDownList></dd>

	<dt><asp:Label ID="Label9" runat="server" Text="APIチェック" ToolTip="API Check "></asp:Label></dt>

<dd><asp:DropDownList ID="ddlAPIcheck" runat="server" >
	<asp:ListItem></asp:ListItem>
	 <asp:ListItem Value="2">○</asp:ListItem>
		<asp:ListItem Value="1">×</asp:ListItem>
		<asp:ListItem Value="0">未</asp:ListItem>
		<asp:ListItem Value="3">対象外</asp:ListItem>
	</asp:DropDownList></dd>	

<dt>	<asp:Label ID="Label24" runat="server" Text="バッチチェック" ToolTip="Batch check"></asp:Label></dt>

	<dd><asp:DropDownList ID="ddlbatchcheck" runat="server" >
	<asp:ListItem></asp:ListItem>
	 <asp:ListItem Value="2">○</asp:ListItem>
		<asp:ListItem Value="1">×</asp:ListItem>
		<asp:ListItem Value="0">未</asp:ListItem>
		<asp:ListItem Value="3">対象外</asp:ListItem>
	</asp:DropDownList></dd>
			<%--<asp:Label ID="Label7" runat="server" Text="出品ショップ"></asp:Label>
			
				<asp:DropDownList ID="ddlshop" runat="server">
				</asp:DropDownList>--%>

	<p>	<asp:Button ID="btnsearch" runat="server" Text="検索" OnClientClick="target=''" 
			onclick="btnsearch_Click" Width="166px" ToolTip="Search"  /></p>
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
			<asp:DropDownList ID="ddlpage" runat="server"  onselectedindexchanged="ddlpage_SelectedIndexChanged" AutoPostBack="true">
			<asp:ListItem>30</asp:ListItem>
			<asp:ListItem>50</asp:ListItem>
			<asp:ListItem>100</asp:ListItem>
			</asp:DropDownList>
             
            <asp:Button ID="btnDownload" runat="server" Text="ダウンロード" onclick="btnDownload_Click" ToolTip="Download"  />
            <asp:LinkButton ID="lnkdownload" runat="server" onclick="lnkdownload_Click" ></asp:LinkButton>
			</p>
  </div>
  
    <div class="exbCmnSet listSet resetValue iconSet2"  >
	<asp:GridView ID="gvexhibition" runat="server" CellPadding="4" 
		ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
		onrowcommand="gvexhibition_RowCommand" 
		EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" 
		AllowPaging="True" onpageindexchanging="gvexhibition_PageIndexChanging" 
		DataKeyNames="ID" onrowdatabound="gvexhibition_RowDataBound"   CssClass="listTable" PageSize="30">
		<Columns>
		<asp:TemplateField HeaderText="ID" Visible="false" >
		<ItemTemplate>
			<asp:Label ID="lblID" runat="server" Text='<%#Eval("Item_ID")%>' CommandName ="Name"></asp:Label>
            <asp:Label ID="lblEID" runat="server" Text='<%#Eval("ID")%>' CommandName ="Name"></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
		<ItemTemplate>
			<asp:Button ID="btndetail" runat="server" Text="詳 細 " OnClientClick="target='_blank'" CommandName="Detail" CommandArgument='<%#Eval("Mall_ID") %>'/>
            <asp:Button ID="btnEdit" runat="server" Text="商品編集" OnClientClick="target='_blank'" CommandName="Edit" CommandArgument='<%#Eval("Item_Code") %>' />
            <asp:Button ID="btnExport" runat="server" Text="CSV" OnClientClick="target=' '" CommandName="Export" CommandArgument='<%#Eval("Mall_ID") %>' />
          <asp:Button ID="btncancel" runat="server" Text="出品取消"  Visible="false"/>
		</ItemTemplate>
		</asp:TemplateField>
			  <asp:TemplateField HeaderText="出品日時" >
		<ItemTemplate>
			<asp:Label ID="lblexhibidateandtime" runat="server" Text='<%#Eval("Exbdate","{0:yyyy/MM/dd  HH:mm}") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="処理開始時刻">
		<ItemTemplate>
			<asp:Label ID="lblprocessstarttime" runat="server" Text='<%#Eval("Exhibition_Date","{0:yyyy/MM/dd  HH:mm}") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="商品番号" >
		<ItemTemplate>
			<asp:Label ID="Label2" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField HeaderText="商品名" >
		<ItemTemplate>
			<asp:Label ID="Label1" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField >
		  <asp:TemplateField ItemStyle-CssClass ="stSet mallSet"  HeaderText="出品ショップ ">
		<ItemTemplate>
		<p id="r" class="iconR"   runat="server" visible="false">	</p><asp:Label ID="Label6" runat="server" Text='<%#Eval("Shop_Name") %>'    Visible="false" ></asp:Label>
        <p id="y" class="iconY"  runat="server" visible="false"></p><asp:Label ID="Label3" runat="server" Text='<%#Eval("Shop_Name") %>'  Visible="false" ></asp:Label>
	     <p id="p" class="iconP"  runat="server" visible="false"></p><asp:Label ID="Label4" runat="server" Text='<%#Eval("Shop_Name") %>'      Visible="false" ></asp:Label>
	       <p  id="j"  class="iconJ"  runat="server" visible="false"></p><asp:Label ID="Label5" runat="server" Text='<%#Eval("Shop_Name") %>'    Visible="false" ></asp:Label>
	      <p id="a" class="iconA"  runat="server" visible="false"></p><asp:Label ID="Label7" runat="server" Text='<%#Eval("Shop_Name") %>'    Visible="false" ></asp:Label>

		<%--   <asp:Image ID="imgRakuten" runat="server" ImageUrl="../../images/icon-rakuten.png"  CssClass="stSet mallSet" BackColor="#b3321e" Width="20px" Height="20px" Visible="false" ForeColor ="#b3321e"/>
			<asp:Image ID="imgY" runat="server" ImageUrl="../../images/icon-yahoo.png"  CssClass="Icon" BackColor="#f39700" Width="20px" Height="20px" Visible="false" ForeColor="#f39700"/>
			 <asp:Image ID="imgp" runat="server" ImageUrl="../../images/icon-pon.png"  CssClass="iconP" BackColor="#e6002d" Width="20px" Height="22px" Visible="false"/>
					<asp:Image ID="imga" runat="server" ImageUrl="../../images/icon-pon.png"  CssClass="Icon" BackColor="#e6002d" Width="20px" Height="22px" Visible="false"/>--%>
<%--  <asp:Image ID="p" runat="server" ImageUrl ="../../images/p.gif"  Visible="false" Width="20px" />
<asp:Image ID="r" runat="server" ImageUrl ="../../images/R.gif"  Visible="false" Width="20px"/>
<asp:Image ID="y" runat="server" ImageUrl ="../../images/y.gif"  Visible="false" Width="20px" />
<asp:Image ID="j" runat="server" ImageUrl ="../../images/jisha.gif"  Visible="false"  Width="20px"/>
<asp:Image ID="a" runat="server" ImageUrl ="../../images/a.gif"  Visible="false"  Width="20px"/>--%>
		</ItemTemplate>
		</asp:TemplateField>
	
		<asp:TemplateField HeaderText="出品者">
		<ItemTemplate>
			<asp:Label ID="lblexhibitor" runat="server" Text='<%#Eval(" User_Name") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField >
        <HeaderTemplate>出品結果<br>エラー</HeaderTemplate>
        
		<ItemTemplate>
			<asp:Label ID="lblexhiresulterror" runat="server" Text='<%#Eval("ExportError_Check") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField >
        <HeaderTemplate>API<br />チェック</HeaderTemplate>
		<ItemTemplate>
			<asp:Label ID="lblAPIcheck" runat="server" Text='<%#Eval("API_Check") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField >
        <HeaderTemplate>バッチ<br />チェック</HeaderTemplate>
		<ItemTemplate>
			<asp:Label ID="lblbatchcheck" runat="server" Text='<%#Eval("Batch_Check") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField >
        <HeaderTemplate>ショップ<br />確認</HeaderTemplate>
		<ItemTemplate>
		<asp:Button ID="btnshopconfirmation" runat="server" Text="表　示" OnClientClick="target='_blank'" CommandName="ShopInfo" CommandArgument='<%#Eval("Mall_ID") %>'  />
		</ItemTemplate>
		</asp:TemplateField>
        <asp:TemplateField HeaderText="Shop_SiteName" Visible="False">
        <ItemTemplate>
            <asp:Label ID="lblsitename" runat="server" Text='<%#Eval("Shop_SiteName") %>'  Visible="false"></asp:Label>
            <%--<asp:Label ID="lblIsCSV_Generate" runat="server" Text='<%#Eval("IsCSV_Generate") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsRakutenCSV_Generate" runat="server" Text='<%#Eval("IsRakutenCSV_Generate") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsYahooCSV_Generate" runat="server" Text='<%#Eval("IsYahooCSV_Generate") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsPonpareCSV_Generate" runat="server" Text='<%#Eval("IsPonpareCSV_Generate") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsAmazonCSV_Generate" runat="server" Text='<%#Eval("IsAmazonCSV_Generate") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsJishaCSV_Generate" runat="server" Text='<%#Eval("IsJishaCSV_Generate") %>'  Visible="false"></asp:Label>--%>
            <asp:Label ID="lblShop_ID" runat="server" Text='<%#Eval("Shop_ID") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsR1_Collect" runat="server" Text='<%#Eval("IsR1_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsR2_Collect" runat="server" Text='<%#Eval("IsR5_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsR3_Collect" runat="server" Text='<%#Eval("IsR8_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsR4_Collect" runat="server" Text='<%#Eval("IsR12_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsY2_Collect" runat="server" Text='<%#Eval("IsY2_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsY6_Collect" runat="server" Text='<%#Eval("IsY6_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsY9_Collect" runat="server" Text='<%#Eval("IsY9_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsY13_Collect" runat="server" Text='<%#Eval("IsY13_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsY17_Collect" runat="server" Text='<%#Eval("IsY17_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsP3_Collect" runat="server" Text='<%#Eval("IsP3_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsJ21_Collect" runat="server" Text='<%#Eval("IsJ21_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblIsA4_Collect" runat="server" Text='<%#Eval("IsA4_Collect") %>'  Visible="false"></asp:Label>
            <asp:Label ID="lblCSV_FileName" runat="server" Text='<%#Eval("CSV_FileName") %>'  Visible="false"></asp:Label>
        </ItemTemplate>
            </asp:TemplateField>

<%--        <asp:TemplateField HeaderText="IsCSV_Generate" Visible="False">
        <ItemTemplate>
            
        </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField HeaderText="CSV_FileName" Visible="False">
        <ItemTemplate>
            
        </ItemTemplate>
         </asp:TemplateField>--%>
	   <%--  <asp:TemplateField HeaderText="掲載チェック">
		<ItemTemplate>
			<asp:Label ID="Label35" runat="server" Text=""></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
  
		<asp:TemplateField HeaderText="アップロード日時">
		<ItemTemplate>
			<asp:Label ID="Label37" runat="server" Text=""></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
			
			<asp:TemplateField HeaderText="商品管理番号（商品URL)" Visible ="false">
		<ItemTemplate>
			<asp:Label ID="Label3" runat="server" Text='<%#Eval("Item_AdminCode") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
			<asp:TemplateField HeaderText="Sale_Code" Visible ="false">
		<ItemTemplate>
			<asp:Label ID="Label4" runat="server" Text='<%#Eval("Sale_Code") %>'></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
			<asp:TemplateField HeaderText="Product_Code" Visible ="false">
		<ItemTemplate>
			<asp:Label ID="Label5" runat="server" Text='<%#Eval("Product_Code") %>'></asp:Label>
		</ItemTemplate>
		
		</asp:TemplateField>
	   <asp:TemplateField HeaderText="担当者">
	   <ItemTemplate>
		   <asp:Label ID="Label38" runat="server" Text="Label"></asp:Label>
	   </ItemTemplate>
	   </asp:TemplateField>
		
			   <asp:TemplateField HeaderText="出品モール">
		<ItemTemplate>
			<asp:Label ID="Label7" runat="server" Text='<%#Eval("Mall_Name") %>' ></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>
			  <asp:TemplateField HeaderText="Exhibition Mall ID" Visible="false">
		<ItemTemplate>
			<asp:Label ID="Label8" runat="server" Text='<%#Eval("Mall_ID") %>' ></asp:Label>
		</ItemTemplate>
		</asp:TemplateField>--%>
		    
		</Columns>
		<PagerSettings Visible="False" />
	</asp:GridView>
	</div>
	</div>
	<div class="btn"> 
		<uc1:UCGrid_Paging  runat="server" ID="gp" /> 
	</div>
	</div>
      </asp:Panel>
</asp:Content>