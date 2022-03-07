<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Item_ImportLog_View.aspx.cs" Inherits="ORS_RCM.Item_ImportLog_View" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">


<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconList">

	<h1>データベース一覧</h1>

<!-- Link List -->	
	<h2>データのインポート</h2>
		<div class="dbCmnSet linkList">
		<ul>
		
 <li> <asp:LinkButton ID="lnkmaster" runat="server" onclick="lnkmaster_Click">商品マスタデータインポート</asp:LinkButton></li>
	<li> <asp:LinkButton ID="lnkdata" runat="server" onclick="lnkdata_Click">オプションデータインポート</asp:LinkButton></li>
  <li>   <asp:LinkButton ID="lnkcategory" runat="server" onclick="lnkcategory_Click">商品カテゴリデータインポート</asp:LinkButton></li>
	<li>   <asp:LinkButton ID="lnkInfodata" runat="server" onclick="lnkInfodata_Click" >商品情報データインポート</asp:LinkButton></li>
	<li>   <asp:LinkButton ID="lnkSmartTemplateImport" runat="server" onclick="lnkSmartTemplateImport_Click" >商品説明文データインポート</asp:LinkButton></li>
            <li><asp:LinkButton ID="lnkProductDirectory" runat="server" onclick="lnkProductDirectory_Click">商品ディレクトリデータインポート</asp:LinkButton></li>
		</ul>
	  
		</div>
<h2>インポートログ</h2>
		

<div class="dbCmnSet editBox">

	<asp:GridView ID="gvimportlog" runat="server" AutoGenerateColumns="False" 
	 CellPadding="4"    ForeColor="#333333" GridLines="None" AllowPaging="True" 
		onpageindexchanging="gvimportlog_PageIndexChanging" 
		onrowdatabound="gvimportlog_RowDataBound" 
		EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" 
		onrowcommand="gvimportlog_RowCommand" CssClass="listTable itemDb" Width="97%">
  

		<Columns>
			<asp:TemplateField>
							 <HeaderStyle Width="150px" />
							 <ItemStyle Width="150px" HorizontalAlign="Center"  />
							 <ItemStyle HorizontalAlign="Center" Width="150px" />
							 <ItemTemplate>

							  <asp:Button ID="btnEdit"  runat="server" CommandArgument='<%# Eval("ID") %>' Text="詳　細"
								  CommandName="DataEdit"></asp:Button>  </ItemTemplate></asp:TemplateField>
				 <asp:TemplateField HeaderText="ID" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left"  Visible ="false">
					<ItemTemplate>
						<asp:Label ID="lblID" runat="server" Text ='<%#Eval("Import_Type") %>' ></asp:Label>
					</ItemTemplate>

<FooterStyle Width="150px"></FooterStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			 </asp:TemplateField>
			<asp:TemplateField HeaderText="日時 " FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
				<ItemStyle HorizontalAlign="Center" Width="300px" />
					<ItemTemplate>
						<asp:Label ID="lbdate" runat="server" Text ='<%#Eval("Imported_Date","{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
					</ItemTemplate>

<FooterStyle Width="100px"></FooterStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			 </asp:TemplateField>
			 <asp:TemplateField HeaderText="担当者 ">
			 <ItemTemplate>
				 <asp:Label ID="Label1" runat="server" Text='<%#Eval("User_Name") %>'></asp:Label>
			 </ItemTemplate>
			 </asp:TemplateField>
		  <asp:TemplateField HeaderText="データ種別 " FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					<ItemTemplate>
						<asp:Label ID="lbltype" runat="server" Text ='<%#Eval("Import_Type") %>'></asp:Label>
					</ItemTemplate>
					
				  
<FooterStyle Width="100px"></FooterStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
					
				  
					</asp:TemplateField>
			 <asp:TemplateField HeaderText="データ件数 " FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					<ItemTemplate>
						<asp:Label ID="lbcount" runat="server" Text ='<%#Eval("Record_Count") %>'></asp:Label>
					</ItemTemplate>

<FooterStyle Width="100px"></FooterStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			 </asp:TemplateField>
			
				<%--<asp:TemplateField HeaderText="Imported_By" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					<ItemTemplate>
						<asp:Label ID="lbimpby" runat="server" Text ='<%#Eval("Imported_By") %>'></asp:Label>
					</ItemTemplate>
			 </asp:TemplateField>--%>
				<asp:TemplateField HeaderText="エラー件数" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					<ItemTemplate>
                    <asp:LinkButton ID="lberrcount" runat="server" CommandArgument='<%# Eval("ID") %>' Text='<%# Eval("Error_Count") %>' CommandName="Datashow"  ></asp:LinkButton>
                 <%--   	<asp:Label ID="lberrcount" runat="server" Text ='<%#Eval("Error_Count") %>' CssClass="labelAsLink" onclick="NewTabPreview(this,event);"></asp:Label>--%>
					<%--	<asp:Label ID="lberrcount" runat="server" Text ='<%#Eval("Error_Count") %>'></asp:Label>--%>
					</ItemTemplate>

<FooterStyle Width="100px"></FooterStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			 </asp:TemplateField>
		</Columns>
		<PagerSettings Visible="False" />
	</asp:GridView>


	</div>

</div><!--setListBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->

<div class="btn">
   <uc1:UCGrid_Paging ID="gp" runat="server" /> 
</div>
</asp:Content>
