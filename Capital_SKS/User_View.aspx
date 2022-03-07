<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
	CodeBehind="User_View.aspx.cs" Inherits="ORS_RCM.User_View" %>
	<%@ Register Src="~/UCGrid_Paging.ascx" TagPrefix="uc" TagName="UCGrid_Paging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

		<link href="Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="Styles/manager-style.css" rel="stylesheet" type="text/css" />

     <link href="Styles/user.css" rel="stylesheet" type="text/css" />

     
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<script src="../js/jquery.page-scroller.js"></script>

 <script type="text/javascript">
     window.document.onkeydown = function (e) {
         if (!e) e = event;
         if (e.keyCode == 27) {
             document.getElementById("<%=txtuser.ClientID%>").value = null;
             var drp5 = document.getElementById("<%=RadioButton3.ClientID%>");
             drp5.checked = true;
         }
     }
</script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
		<div id="CmnContents">
				<div id="ComBlock">
					<div class="setListBox inlineSet iconSet iconList">
							 <h1>ユーザ一覧</h1>
                             <!-- User search -->
			                      <div class="userCmnSet resetValue searchBox">
								<h2>ユーザ検索</h2>
                                					  
										<%--	<form action="#" method="get">	 --%> 
												  <dl>
												  											  
												  
											     <dt>ユーザー名</dt> 


						
												<dd> <asp:TextBox ID="txtuser" runat="server"></asp:TextBox></dd>
						  
						


													 <dt>ステータス</dt>
						<dd>
							<asp:RadioButton ID="RadioButton1" runat="server" Text="有効" Value="1" GroupName="rbd" />
					 
							<asp:RadioButton ID="RadioButton2" runat="server" Text="無効" Value="0" GroupName="rbd" />
					  
							<asp:RadioButton ID="RadioButton3" runat="server" Text="全て" GroupName="rbd" Checked="true" />
						
						</dd>

						</dl>


						  <p> <asp:Button ID="btnSearch" runat="server" Text="検索" OnClick="btnSearch_Click" 
                                  BackColor="#F2F2F2" Width="150px" Height="25px"/></p>
					
				  
					  
							<asp:Label ID="Login_ID" runat="server" Text="Login_ID" Font-Size="Medium" Visible="false"></asp:Label>
                            		<asp:TextBox ID="txtlogin" runat="server" visible="false"></asp:TextBox>



				<%--</form>--%>
				

				 </div>
                  
							  <div class="operationBtn">
							   <p> <asp:Button ID="btnNew" runat="server" Text="ユーザーを追加する" OnClick="btnNew_Click" 
                                       BackColor="#F2F2F2"/></p>
							</div>

		
				<div  class="userCmnSet resetValue editBox">

		   <asp:UpdatePanel ID="upl1" runat="server">
		   <ContentTemplate>


				<asp:GridView ID="gvuser" runat="server"  ShowHeaderWhenEmpty="True" CssClass="listTable"
					AllowPaging="True" GridLines="None" boder-color="#CCC" PageSize="10" 
					EmptyDataText="There is no data to display." AutoGenerateColumns="False" OnPageIndexChanging="gvuser_PageIndexChanging"
					OnRowCommand="gvuser_RowCommand" Width="1000px" 
					OnRowDataBound="gvuser_RowDataBound" AllowSorting="True">
						
					<AlternatingRowStyle BackColor="#eeeeee" />
					<Columns>
						<asp:TemplateField>
							<HeaderStyle Width="134px" font-size="XX-Large"/>
							<ItemStyle HorizontalAlign="Left" Width="128px"/>

							<ItemTemplate>
								<asp:Button runat="server" ID="lnkedit" Text="編集" CommandName="DataEdit"
									CommandArgument='<%# Eval("ID") %>'></asp:Button>
									<asp:Button runat="server" ID="lnkuser" Text="UserEdit" CommandName="UserEdit"
									CommandArgument='<%# Eval("ID") %>' Visible="false"></asp:Button>
							</ItemTemplate>
							   
								
							
						</asp:TemplateField>
 
						<asp:TemplateField HeaderText="ユーザ名" meta:resourcekey="User_Name">
							<HeaderStyle  HorizontalAlign="Right" Width="350px"/>
							<ItemStyle  HorizontalAlign="Right" Width="350px"/>
							<EditItemTemplate>
								<asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("User_Name") %>'></asp:TextBox>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="Label20" runat="server" Text='<%# Bind("User_Name") %>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>

						<asp:TemplateField HeaderText="Login_ID" meta:resourcekey="Login_ID">
						<HeaderStyle/>
							<ItemStyle/>
							<EditItemTemplate>
								<asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("Login_ID") %>'></asp:TextBox>
							</EditItemTemplate>
							<ItemTemplate>
								<asp:Label ID="Label21" runat="server" Text='<%# Bind("Login_ID") %>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateField>

						<asp:TemplateField HeaderText="ステータス" FooterStyle-Width="150px" HeaderStyle-HorizontalAlign="Left">
							<FooterStyle Width="150px" />
						<HeaderStyle  HorizontalAlign="Left"/>
							<ItemStyle  HorizontalAlign="Left" />
							<ItemTemplate>
								<asp:Label ID="lblitem" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
							</ItemTemplate>

							
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
						</asp:TemplateField>
						
						
						   <asp:TemplateField HeaderText="登録日" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
							   <FooterStyle Width="150px" />
						   <HeaderStyle/>
							<ItemStyle  HorizontalAlign="Left" />

							<ItemTemplate>
								
								<asp:Label ID="Label4" runat="server" Text='<%# Bind("Updated_Date","{0:yyyy/MM/dd}") %>'></asp:Label>
							</ItemTemplate>

						   
							<HeaderStyle HorizontalAlign="Left"></HeaderStyle>

						</asp:TemplateField>

					</Columns>

				   
			
					<PagerSettings Visible="False" />
                    			   
			
					<PagerStyle BorderColor="#CCCCCC"   BackColor="White"
						Height="30px" VerticalAlign="Bottom" HorizontalAlign="Left" />
					<RowStyle Height="20px" Font-Size="13px" BorderColor="#CCCCCC"/>


				</asp:GridView>
				
				   
				</ContentTemplate>
				</asp:UpdatePanel>

			  
			  </div>
			  </div>

              
			</div>
            
            
			
	<div class="btn pageNav">
		<uc:ucgrid_paging runat="server" ID="gp" />
	</div>
    
    </div>
</asp:Content>
