<%@ Page Title="商品管理システム＜ショップカテゴリ一覧画面＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category_View.aspx.cs" Inherits="ORS_RCM.Category_View" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
<link rel="stylesheet" href="../../Styles/shop_category.css"  type ="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
       
       <script type="text/javascript">
           window.document.onkeydown = function (e) {
               if (!e) e = event;
               if (e.keyCode == 27) {
                   document.getElementById("<%=txtdesc.ClientID%>").value = null;
                   
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

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
	<div id="ComBlock">
		<div class="setListBox inlineSet iconSet iconList">
			<h1>ショップカテゴリ一覧画面</h1>
		<!-- CategoryID search -->
				<div class="CateIdSar resetValue searchBox">
					<h2>カテゴリ検索<span>※注意：スペース区切りで一致検 索</span></h2>
					<dl>
						<dt>カテゴリ名</dt>
						<dd><asp:TextBox ID="txtdesc" runat="server"  placeholder="例）テニス シューズ プリンス"></asp:TextBox></dd> 
						<dd><asp:TextBox ID="txtcatid" runat="server"  Visible="false"></asp:TextBox></dd>
					</dl>
				   <p><asp:Button ID="btnSearch" runat="server" Text="検 索" onclick="btnSearch_Click"/></p>
				</div>
		
				<div class="shopCate operationBtn">
					<asp:FileUpload runat="server" ID="upl1" />
			
						<asp:Button ID="btnImport" runat="server" Text="インポート" onclick="btnImport_Click" />
						<asp:Button ID="btnExport" runat="server" Text="ー覧データをエクスポート" onclick="btnExport_Click" />
					<p><asp:LinkButton ID="lnkdownload" runat="server" onclick="lnkdownload_Click1"></asp:LinkButton></p>
				</div>
		
		
		<!-- /operationBtn -->
		
				<div class="shopCate resetValue">
      
					<%--<asp:HiddenField ID="hfhidid" runat="server" />
					<asp:HiddenField ID="hfhidtext" runat="server" />--%>
   
					<asp:Button ID="btnNew" runat="server" Text="New" 
						onclick="btnNew_Click" Visible ="false" />
       
					&nbsp;&nbsp;<p class="cateTop">カテゴリTOP</p>
						
					<table>
						<tr>
							<td>
								<ul id="menuTree">
									<li>
										<div class="shopCateEdit">
											<p><asp:Button ID="Button1" runat="server" Text="カテゴリの編集" onclick="btnupdatenode_Click"  /></p>
											<p><asp:Button ID="btnAdd" runat="server" Text="孫カテゴリの追加" onclick="btnAdd_Click" /></p>
										</div>
									</li>
								</ul>
							</td>
						</tr>
					</table>      
                              
     
   
					<div class="shopCateList">

                        <%--<asp:TreeView ID="tvCategory" runat="server" ImageSet="Simple" NodeIndent="10" 
                          ShowLines="True" onselectednodechanged="tvCategory_SelectedNodeChanged"  
                               style="table-layout:left;" ShowCheckBoxes="Root">

                            <ParentNodeStyle BackColor="#D2ECFF" Width="800px" Height="0px" />
                            <SelectedNodeStyle BackColor="#66CCFF" ForeColor="#666666" />
                        </asp:TreeView>--%>
   

<%--                           <asp:TreeView ID="tvCategory" runat="server"  ShowLines="True"  
                               NodeIndent="10"  Width="80px" 
                               onselectednodechanged="tvCategory_SelectedNodeChanged" 
                               ontreenodeexpanded="tvCategory_TreeNodeExpanded">
                             <ParentNodeStyle BackColor="#D2ECFF" />
                            <SelectedNodeStyle BackColor="#66CCFF" ForeColor="#666666" />
                            
                           </asp:TreeView>--%>
                           <asp:UpdatePanel ID ="UP1" runat ="server" UpdateMode ="Conditional">
                           <ContentTemplate>

                                	<asp:HiddenField ID="hfhidid" runat="server" />
					            <asp:HiddenField ID="hfhidtext" runat="server" />
                                 <asp:TreeView ID="tvCategory" runat="server"  ShowLines="True"  
                               NodeIndent="10"  Width="80px" 
                               onselectednodechanged="tvCategory_SelectedNodeChanged" 
                               ontreenodeexpanded="tvCategory_TreeNodeExpanded">
                             <ParentNodeStyle BackColor="#D2ECFF" />
                            <SelectedNodeStyle BackColor="#66CCFF" ForeColor="#666666" />
                            
                           </asp:TreeView>
                           </ContentTemplate>
                           <Triggers>
                                  <asp:AsyncPostBackTrigger ControlID="tvCategory" EventName="TreeNodeExpanded" />
                           </Triggers>
                       </asp:UpdatePanel>
   
					</div>
  
					<ul class="shopCateDate">
						<li>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                      <ContentTemplate>
					  <asp:GridView ID="gvtreeview" runat="server" AutoGenerateColumns="False" 
						  CellPadding="4" EmptyDataText="There is no data to display!" 
						  ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" 
						  DataKeyNames="ID" Width="320px" style="margin-left:400px">
						  <AlternatingRowStyle BackColor="White" />
				
						  <EditRowStyle BackColor="#2461BF" />
						  <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
						  <HeaderStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="White" />
						  <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
						  <RowStyle BackColor="#EFF3FB" />
				
						  <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						  <SortedAscendingCellStyle BackColor="#F5F7FB" />
						  <SortedAscendingHeaderStyle BackColor="#6D95E1" />
						  <SortedDescendingCellStyle BackColor="#E9EBEF" />
						  <SortedDescendingHeaderStyle BackColor="#4870BE" />
						   <Columns>
									   <asp:TemplateField HeaderText="ID" Visible="false"  >
								 <ItemTemplate>
								 <asp:Label ID ="lblID" runat="server" Text='<%# Eval("ID")%>' ></asp:Label>
							  
								 </ItemTemplate>
								 
							 </asp:TemplateField>
								 <asp:TemplateField HeaderText="Description">
								 <ItemTemplate>
								 <asp:Label ID ="lbldesc" runat="server" Text='<%# Eval("Description")%>'  Width="200px"></asp:Label>
								 </ItemTemplate>
								
								 </asp:TemplateField>
								 <asp:TemplateField HeaderText="Serial Number" ItemStyle-Width="120px">
								 <ItemTemplate>
								 
									 <asp:TextBox ID="txtserial" runat="server" Text='<%#Eval("Category_SN")%>' onkeypress="return isNumberKey(event)" Width="50px" ></asp:TextBox>
								 </ItemTemplate>
							
							   </asp:TemplateField>
									
									</Columns>
				
					  </asp:GridView>
						 </ContentTemplate>
                          <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="tvCategory" EventName="SelectedNodeChanged" />
                             <%--  <asp:AsyncPostBackTrigger ControlID="tvCategory" EventName="TreeNodeExpanded" />--%>
                        </Triggers>
                    </asp:UpdatePanel>
                          </li>
					</ul>

					<div class="shopCateBtn"><asp:Button ID="btnupdate" runat="server" Text="編集" onclick="btnupdate_Click"/></div>
    </div><!-- /.shopCate.resetValue -->

	</div><!--setListBox-->
	</div><!--ComBlock-->
</div><!--/#CmnContents-->
   
   



</asp:Content>
  