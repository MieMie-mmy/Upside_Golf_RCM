<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Smart_Template_View.aspx.cs" Inherits="ORS_RCM.Smart_Template_View" %>
<%@ Register Src="~/UCGrid_Paging.ascx" TagPrefix="uc" TagName="UCGrid_Paging" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
  
   <link href="../../Styles/shop.css" type="text/css"/>

   <link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css"/>
	<%--<link href="../../Scripts/jquery-1.3.min.js" rel="stylesheet" type="text/css" />
	<link href="../../Scripts/jquery.droppy.js" rel="stylesheet" type="text/css" />--%>

<%--
	<script type="text/javascript">
		$(function () {
			$("#nav").droppy();
		});  
			</script>  
		<script type="text/javascript">
		$(document).ready(function () {
			$(".scroll").click(function (event) {
				$('html,body').animate({ scrollTop: 0 }, "slow");
			});
		});
  
	</script>--%>

   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
	<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconList">
	<h1>スマートテンプレート一覧</h1>





		<dl>
			<dd>
		   </dd>

		   </dl>
		<p>&nbsp;</p>
			 </div>
			  
  
   
<div class="operationBtn">
			
		<asp:Button ID="btnnew" runat="server" Text="スマートテンプレート一を追加する" 
		onclick="btnnew_Click" />
		</div>


		
<asp:UpdatePanel ID="upl1" runat="server">
	<ContentTemplate>


   
   

		 
	
   <div class="smartTmp">
   
   

   


	<asp:GridView ID="gvSmt" runat="server" ForeColor="#333333"  PageSize="10" AllowSorting="true"
		GridLines="None" AutoGenerateColumns="False"  CssClass="listTable"
		EmptyDataText="There is no data to display"   PagerSettings-PageButtonCount="10" 
		ShowHeaderWhenEmpty="True"  AllowPaging="True" 
		
			
		   onrowcommand="gvSmt_RowCommand"  Width="900px"  
		
		   onrowdatabound="gvSmt_RowDataBound" 
		   onpageindexchanging="gvSmt_PageIndexChanging">
 
		   
		   <AlternatingRowStyle BackColor="#eeeeee" />

	  

		<Columns>
			<asp:TemplateField HeaderText="操作">
							 <HeaderStyle Width="100px"  />
							 <ItemStyle Width="100px" HorizontalAlign="Center"  />
							 <ItemStyle HorizontalAlign="Center" Width="100px" />
							 <ItemTemplate>

						   
						
							  <asp:Button ID="btnEdit"  runat="server" CommandArgument='<%#Eval("ID") %>' Text="編集"
								  CommandName="DataEdit"></asp:Button>  </ItemTemplate></asp:TemplateField>
							 
							 
							  <asp:TemplateField  Visible="false">  
							  <HeaderStyle Width="100px" /> 
							  <ItemStyle Width="100px" HorizontalAlign="Center"  />
							 <ItemStyle HorizontalAlign="Center" Width="100px" />
							   <ItemTemplate>
								  <asp:Button ID="btndefaultsetting" runat="server" CommandArgument='XX' Text="デフォルト値設定" CommandName="DefaultSetting" ></asp:Button>
						   </ItemTemplate></asp:TemplateField>
						
						 <asp:TemplateField Visible="false">   
						   <HeaderStyle Width="100px" />
						  <ItemStyle Width="100px" HorizontalAlign="Center"  />
						   <ItemStyle HorizontalAlign="Center" Width="100px" />   
						   <ItemTemplate>
						   <asp:Button ID="btndefaultfix" runat="server"  CommandArgument='"ID' Text="固定値設定" CommandName="Fixedvalue" ></asp:Button>
							 
								
							   
							 </ItemTemplate></asp:TemplateField>
							 <asp:TemplateField Visible="false">
							 <ItemTemplate>
							 <asp:Label ID="Label1" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
							 </ItemTemplate>
							 </asp:TemplateField>
							<asp:TemplateField  HeaderText="テンプレート名"   FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					        
					<ItemTemplate>
						<asp:Label ID="lbname" runat="server" Text ='<%#Eval("Template_name") %>'>'></asp:Label>
					</ItemTemplate>
					
				  
<FooterStyle Width="100px"></FooterStyle>

<HeaderStyle HorizontalAlign="Left"></HeaderStyle>
					
				  
					</asp:TemplateField>

				   <asp:TemplateField HeaderText="テンプレートID" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblmall" runat="server" Text ='<%#Eval("Template_ID") %>'  ></asp:Label>
					</ItemTemplate>
					  <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
					   <FooterStyle Width="900px"></FooterStyle>
					</asp:TemplateField>


					
				   <asp:TemplateField HeaderText="ステータス" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblftpl" runat="server" Text ='<%#Eval("Status") %>'></asp:Label>
					</ItemTemplate>
					
							
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							 <FooterStyle Width="900px"></FooterStyle>

					</asp:TemplateField>
	

				 
		</Columns>
		<HeaderStyle Height="30px" Width="100" Font-Size="15px" 
						BorderStyle="Solid" BorderWidth="1px" BorderColor="#CCCCCC" />
					<PagerSettings Mode="NumericFirstLast" PreviousPageText="Previous" NextPageText="Prev"
						FirstPageText="First" LastPageText="Next" Position="bottom" 
			Visible="False" />
					<PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" BackColor="White"
						Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
					<RowStyle Height="20px" Font-Size="13px" BorderColor="#CCCCCC" 
						BorderWidth="1px" />

	   </asp:GridView>
	   
		
	   </div>

	   
	

	   </ContentTemplate>
		
	 </asp:UpdatePanel>
   
		<div class="btn">
			<uc:ucgrid_paging runat="server" ID="gp" />
		</div>
		</div>

	</div>
</asp:Content>
