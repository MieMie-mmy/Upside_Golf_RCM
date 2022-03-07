<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shop_View.aspx.cs" Inherits="ORS_RCM.Shop_View" %>
<%@ Register Src="~/UCGrid_Paging.ascx" TagPrefix="uc" TagName="UCGrid_Paging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   
   <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/shop.css" type="text/css"/>

    <link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css"/>

	<style type="text/css">
		.content
		{   
			height:65px;
			background-color:#7c7c7c;
			position:fixed;
			left:0px;
			right:0px;
			bottom:5px;
			opacity:0.7;
		}​
	</style>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>


<div id="CmnContents">
	<div id="ComBlock">

    	<%--<div class="setListBox inlineSet iconSet iconList">--%>
	 <div class="setListBox inlineSet iconSet iconList">
		 <h1>ショップ一覧</h1>

<div class="shopCmnSet resetValue searchBox">	 
		<h2>ショップ検索</h2>     
				  
			<%--	<form action="#" method="get">  --%>
			   <dl>
			   
						 <dt>ショップ名</dt>
			     <dd><asp:TextBox ID="txtShopname" runat="server"></asp:TextBox> </dd>
			 
						 <dt>出店モール</dt>
				  <dd>
					<asp:DropDownList ID="ddlmall" runat="server">
					</asp:DropDownList>
				 </dd>

		   </dl>      

		   
				
		<p><asp:Button ID="btnsearch" runat="server" Text="検索" 
			 onclick="btnsearch_Click" BackColor="#F2F2F2" Height="25px" Width="150px" /></p>

            <%-- </form>--%>
			 </div>
		
		</div>
		   
		   
  
 
<asp:UpdatePanel ID="upl1" runat="server">
	<ContentTemplate>
<div class="operationBtn">
      <p> <asp:Button ID="btnnew" runat="server" Text="ショップを追加する" 
		onclick="btnnew_Click" /></p>
		</div>
		
		   
		   
   <div class="shopCmnSet resetValue editBox">

	<asp:GridView ID="gvshop" runat="server" ForeColor="#333333"  PageSize="10" AllowSorting="true"
		GridLines="None" AutoGenerateColumns="False"  CssClass="listTable"
		EmptyDataText="There is no data to display"   PagerSettings-PageButtonCount="10"
		ShowHeaderWhenEmpty="True"  AllowPaging="True"  Width="1000px"
		onpageindexchanging="gvshop_PageIndexChanging" 
		onrowcommand="gvshop_RowCommand"  
           onrowdatabound="gvshop_RowDataBound">
	   
		<AlternatingRowStyle BackColor="#eeeeee" />
		<Columns>
			<asp:TemplateField>
							 <HeaderStyle Width="100px" Font-Bold="false"  Font-Size="12px"/>
							 <ItemStyle Width="100px" HorizontalAlign="Center"  />
							 <ItemStyle HorizontalAlign="Center" Width="100px" />
							 <ItemTemplate>

						   
						
							  <asp:Button ID="btnEdit"  runat="server" CommandArgument='<%# Eval("ID") %>' Text="編集"
								  CommandName="DataEdit"></asp:Button>  </ItemTemplate></asp:TemplateField>
							 
							 
							  <asp:TemplateField>  
							  <HeaderStyle Width="100px" /> 
							  <ItemStyle Width="100px" HorizontalAlign="Center"  />
							 <ItemStyle HorizontalAlign="Center" Width="100px" />
							   <ItemTemplate>
								  <asp:Button ID="btndefaultsetting" runat="server" CommandArgument='<%#Eval("ID") %>' Text="デフォルト値設定" CommandName="DefaultSetting" ></asp:Button>
						   </ItemTemplate></asp:TemplateField>
						
						 <asp:TemplateField>   
						   <HeaderStyle Width="100px" />
						  <ItemStyle Width="100px" HorizontalAlign="Center"  />
						   <ItemStyle HorizontalAlign="Center" Width="100px" />   
						   <ItemTemplate>
						   <asp:Button ID="btndefaultfix" runat="server"  CommandArgument='<%#Eval("ID") %>' Text="固定値設定" CommandName="Fixedvalue" ></asp:Button>
							 
								
							   
							 </ItemTemplate></asp:TemplateField>
							<asp:TemplateField  HeaderText="ショップ名"   FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					   
					<ItemTemplate>
						<asp:Label ID="lbname" runat="server" Text ='<%#Eval("Shop_Name") %>'></asp:Label>
					</ItemTemplate>
					
				  
<FooterStyle Width="100px"></FooterStyle>

<HeaderStyle HorizontalAlign="Left" Font-Bold="false"></HeaderStyle>
					
				  
					</asp:TemplateField>

				   <asp:TemplateField HeaderText="出店モール" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblmall" runat="server" Text ='<%#Eval("Mall_ID") %>'  ></asp:Label>
					</ItemTemplate>
					  <HeaderStyle HorizontalAlign="Center" Font-Bold="false"></HeaderStyle>
					   <FooterStyle Width="900px"></FooterStyle>
					</asp:TemplateField>


			
				   <asp:TemplateField HeaderText="ステータス" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblftpl" runat="server" Text ='<%#Eval("Status") %>'></asp:Label>
					</ItemTemplate>
					
							
							<HeaderStyle HorizontalAlign="Center" Font-Bold="false"></HeaderStyle>
							 <FooterStyle Width="900px"></FooterStyle>

					</asp:TemplateField>

						 
		</Columns>
		<HeaderStyle Height="30px" Width="100" Font-Bold="false" Font-Size="12px"
						BorderStyle="Solid" BorderWidth="1px" BorderColor="#CCCCCC" />
					<PagerSettings Mode="NumericFirstLast" PreviousPageText="Previous" NextPageText="Prev"
						FirstPageText="First" LastPageText="Next" Position="bottom" 
			Visible="False" />
					<PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" BackColor="White"
						Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
					<RowStyle Height="20px" Font-Size="13px" BorderColor="#CCCCCC" 
						BorderWidth="1px" />

	   </asp:GridView>
   
	   <%--<uc:UCGrid_Paging runat="server" ID="gp" />
	   </div>
	   </div>
		<div style="padding-left:50px;" >            

		</div>--%>
		
		</div>
	   

	
   
		</ContentTemplate>

			

	 </asp:UpdatePanel>

	<div class="btn">
		<uc:UCGrid_Paging runat="server" ID="gp" />
	</div>		
		
	</div>
	
	
	 </div>
	 
	
	

	
</asp:Content>
