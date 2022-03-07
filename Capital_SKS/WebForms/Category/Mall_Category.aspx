<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mall_Category.aspx.cs" Inherits="ORS_RCM.Mall_Category" %>
<%@ Register Src="~/UCGrid_Paging.ascx" TagPrefix="uc" TagName="UCGrid_Paging" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../Styles/mall_category.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
 <script type="text/javascript">
     window.document.onkeydown = function (e) {
         if (!e) e = event;
         if (e.keyCode == 27) {
             document.getElementById("<%=txtcname.ClientID%>").value = null;

         }
     }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
   
     
	<div id="CmnContents">
		<div id="ComBlock">
			<div class="setListBox inlineSet iconSet iconList">
				<asp:Label ID="Label1"   runat="server"  visible="false" Text='<h1>楽天ディレクトリID一覧</h1>'></asp:Label>    
				<asp:Label ID="Label2"   runat="server" visible="false" Text='<h1>ヤフーディレクトリID一覧</h1>'></asp:Label> 
<%--				<asp:Label  ID="Label3"   runat="server" visible="false" Text='<h1>ポンパレモールディレクトリID一覧</h1>'></asp:Label>--%>
				<asp:Label  ID="Label4"   runat="server" visible="false" Text='<h1>WowmaディレクトリID一覧</h1>'></asp:Label>
                 <asp:Label ID="Label5" runat="server" Visible="false" Text='<h1>ORSディレクトリID一覧</h1>'></asp:Label>
		<!-- CategoryID search -->
			<div class="CateIdSar resetValue searchBox">
				<asp:Label ID="lblRk"   runat="server"  visible="false" Text='<h2>楽天ディレクトリID検索</h2>'></asp:Label>    
				<asp:Label ID="lblYh"   runat="server" visible="false" Text='<h2>ヤフーディレクトリID検索</h2>'></asp:Label> 
<%--				<asp:Label  ID="lblPom"   runat="server" visible="false" Text='<h2>ポンパレモールディレクトリID検索</h2>'></asp:Label> --%>--%>
                <asp:Label  ID="lblWowma" runat="server" visible="false" Text='<h2>WowmaディレクトリID検索</h2>'></asp:Label> 
                 <asp:Label ID="lblTennis" runat="server" Visible="false" Text='<h2>ORSディレクトリID検索</h2>'></asp:Label>
				<dl>
					<dt>カテゴリ名 </dt> 
					<dd>  
						<asp:TextBox ID="txtcname" runat="server"  placeholder="例）テニス シューズ プリンス"></asp:TextBox >
					</dd>
				</dl>
				<p><asp:Button ID="btnsearch" runat="server" Text="検索" onclick="btnsearch_Click"  width="130px"/></p>
			</div>
  <!-- /CategoryID search -->

<!-- Import&Export -->     

			<div class="mallCate resetValue entryBox">                        
				<asp:FileUpload ID="upl1" runat="server" />
				<p><asp:Button ID="btnImport" runat="server" Text="インポートする" onclick="btnImport_Click" /></p>
				<p><asp:Button ID="btnExport" runat="server" Text="ー覧データをエクスポート" onclick="btnExport_Click" /></p>
				<p> <asp:LinkButton ID="lnkdownload" runat="server" onclick="lnkdownload_Click"></asp:LinkButton></p>
			</div>
			<div class="mallCate resetValue editBox">
				<asp:UpdatePanel ID="UpdatePanel1" runat="server">
		   
	<ContentTemplate>


	   <asp:GridView ID="gvmall" runat="server" AutoGenerateColumns="False"  
		EmptyDataText="There is no data to display." PageSize="50"   HorizontalAlign="Center"
		 GridLines="None" ShowHeaderWhenEmpty="True"  CssClass="listTable"
		   AllowPaging="True" onpageindexchanging="gvmall_PageIndexChanging" 
			onrowdatabound="gvmall_RowDataBound">

		   
			  

		   <PagerStyle HorizontalAlign = "Center"/>
		  
			<PagerSettings FirstPageText="First" LastPageText="Last" 
			   Mode="NumericFirstLast" Position="Bottom" Visible="False" />
			
							 
			<%--<AlternatingRowStyle BackColor="#eeeeee" /> --%>
	
	<Columns>
					<asp:TemplateField HeaderText="ディレクトリID"  HeaderStyle-HorizontalAlign="Center" >
					 <HeaderStyle  HorizontalAlign="Center"/>
					   <ItemStyle HorizontalAlign="Left" />
							<ItemStyle HorizontalAlign="Center" />
						   
					<ItemTemplate >
						<asp:Label ID="lbID" runat="server" Text ='<%#Eval("Category_ID") %>'></asp:Label>
					</ItemTemplate>
					  
					  
					</asp:TemplateField>

				   <asp:TemplateField HeaderText="カテゴリ名" HeaderStyle-HorizontalAlign="Center" >
							<ItemStyle HorizontalAlign="Left" />
					  <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
				
					<ItemTemplate>
					   
						<asp:Label ID="lbname" runat="server" Text ='<%#Eval("Category_Path") %>'  ></asp:Label>
					</ItemTemplate>

<FooterStyle Width="100px"></FooterStyle>

					<HeaderStyle HorizontalAlign="Center"/>
							<ItemStyle HorizontalAlign="Left" />
				
					</asp:TemplateField>
				  
				    
		</Columns>
			<HeaderStyle BorderStyle="Solid" BorderWidth="1px" BorderColor="#CCCCCC" />
                      
		
					<RowStyle Height="20px" Font-Size="13px" BorderColor="#CCCCCC"/>
                      
				</asp:GridView>
         
     </ContentTemplate>
    
				</asp:UpdatePanel>
                
                            <div class="btn">
          
	  <uc:ucgrid_paging runat="server" ID="gp" />
      
     
     </div>
   
      </div>

	</div>
     </div>
   
     
      
    </div>

</asp:Content>
