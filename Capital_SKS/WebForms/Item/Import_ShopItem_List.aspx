<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_ShopItem_List.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Import_ShopItem_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  
    
   <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/shop.css" type="text/css"/>

   <link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css"/>
	<link href="../../Scripts/jquery-1.3.min.js" rel="stylesheet" type="text/css" />
	<link href="../../Scripts/jquery.droppy.js" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
	<div id="ComBlock">
    <div class="setListBox inlineSet iconSet iconList">
	
<div class="setDetailBox iconSet iconEdit">

		<h1>インポートショップCSV_覧</h1>

<div class="shopCmnSet resetValue searchBox">
     
     	<h2>出品ログ検索</h2>

		          <asp:Panel ID="Panel1" runat="server" DefaultButton="btnsearch">
                  
               <dl>
			   
                         <dt>商品番号</dt>
			   <dd><asp:TextBox ID="txtItemCode" runat="server"></asp:TextBox> </dd>
             
                  

		   </dl>      

           
				
		<p><asp:Button ID="btnsearch" runat="server" Text="検索する" 
			 onclick="btnsearch_Click" /></p>
             </asp:Panel>
			 </div>
		
        </div>
           
           
 <div  class="userCmnSet resetValue editBox" style="overflow-x:auto;width:900px">


         <asp:UpdatePanel ID="upl1" runat="server">
           
        
              
              
                <ContentTemplate>   
           
           
   

	<asp:GridView ID="gvImportshop" runat="server" ForeColor="#333333"   AllowSorting="true"
		GridLines="None" AutoGenerateColumns="False"  CssClass="listTable"
		EmptyDataText="There is no data to display"   PagerSettings-PageButtonCount="10"
		ShowHeaderWhenEmpty="True"  AllowPaging="False" 
		
	 Width="900px">
 	   
	    <AlternatingRowStyle BackColor="#eeeeee" />
     	<Columns>
			
            						
					<asp:TemplateField  HeaderText="ショップID"   FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					   
					<ItemTemplate>
						<asp:Label ID="lbshopID" runat="server" Text ='<%#Eval("Shop_ID") %>'></asp:Label>
					</ItemTemplate>
                                      
				  
                                        <FooterStyle   CssClass="scrolled1 col2"  Width="100px"></FooterStyle>

                                 <HeaderStyle    CssClass="scrolled1 col2" HorizontalAlign="Left"></HeaderStyle>
					
				  
					</asp:TemplateField>


                    <asp:TemplateField  HeaderText="ショップ"   FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					   
					<ItemTemplate>
						<asp:Label ID="lbshopID" runat="server" Text ='<%#Eval("Shop") %>'></asp:Label>
					</ItemTemplate>
                                      
				  
                                        <FooterStyle   CssClass="scrolled1 col2"  Width="100px"></FooterStyle>

                                 <HeaderStyle    CssClass="scrolled1 col2" HorizontalAlign="Left"></HeaderStyle>
					
				  
					</asp:TemplateField>






                    <asp:TemplateField  HeaderText="ショップ名"   FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					   
					<ItemTemplate>
						<asp:Label ID="lbshopID" runat="server" Text ='<%#Eval("Shop_Name") %>'></asp:Label>
					</ItemTemplate>
                                      
				  
                                        <FooterStyle   CssClass="scrolled1 col2"  Width="100px"></FooterStyle>

                                 <HeaderStyle    CssClass="scrolled1 col2" HorizontalAlign="Left"></HeaderStyle>
					
				  
					</asp:TemplateField>


            <%--        <asp:TemplateField  HeaderText="MallID"   FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					   
					<ItemTemplate>
						<asp:Label ID="lbshopID" runat="server" Text ='<%#Eval("MallID") %>'></asp:Label>
					</ItemTemplate>
                                      
				  
                                        <FooterStyle   CssClass="scrolled1 col2"  Width="100px"></FooterStyle>

                                 <HeaderStyle    CssClass="scrolled1 col2" HorizontalAlign="Left"></HeaderStyle>
					
				  
					</asp:TemplateField>--%>




<%--
				   <asp:TemplateField HeaderText="Ctrl_ID" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblctrl" runat="server" Text ='<%#Eval("Ctrl_ID") %>'  ></asp:Label>
					</ItemTemplate>
					  <HeaderStyle CssClass="scrolled1 col2" HorizontalAlign="Center"></HeaderStyle>
					   <FooterStyle  CssClass="scrolled1 col2" Width="900px"></FooterStyle>
					</asp:TemplateField>--%>


					
				   <asp:TemplateField HeaderText="Item_AdminCode" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblftpl" runat="server" Text ='<%#Eval("Item_AdminCode") %>'></asp:Label>
					</ItemTemplate>
					
							
							<HeaderStyle CssClass="scrolled1 col2"  HorizontalAlign="Center"></HeaderStyle>
							 <FooterStyle  CssClass="scrolled1 col2"  Width="900px"></FooterStyle>

					</asp:TemplateField>



                       <asp:TemplateField HeaderText="Item_Code" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblitemcode" runat="server" Text ='<%#Eval("Item_Code") %>'></asp:Label>
					</ItemTemplate>
					
							
							<HeaderStyle  CssClass="scrolled" HorizontalAlign="Center"></HeaderStyle>
							 <FooterStyle  CssClass="scrolled" Width="900px"></FooterStyle>

					</asp:TemplateField>


                      <asp:TemplateField HeaderText="Item_Name" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lbllitemname" runat="server" Text ='<%#Eval("Item_Name") %>'></asp:Label>
					</ItemTemplate>
					
							
							<HeaderStyle  CssClass="scrolled" HorizontalAlign="Center"></HeaderStyle>
							 <FooterStyle  CssClass="scrolled" Width="900px"></FooterStyle>

					</asp:TemplateField>


                    
              <%--        <asp:TemplateField HeaderText="Point_Code" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblpontcode" runat="server" Text ='<%#Eval("Point_Code") %>'></asp:Label>
					</ItemTemplate>
					
							
							<HeaderStyle   CssClass="scrolled"   HorizontalAlign="Center"></HeaderStyle>
							 <FooterStyle  CssClass="scrolled"  Width="900px"></FooterStyle>

					</asp:TemplateField>


                            <asp:TemplateField HeaderText="Point_Term" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblpointterm" runat="server" Text ='<%#Eval("Point_Term") %>'></asp:Label>
					</ItemTemplate>
					
							
							<HeaderStyle  CssClass="scrolled" HorizontalAlign="Center"></HeaderStyle>
							 <FooterStyle CssClass="scrolled"  Width="900px"></FooterStyle>

					</asp:TemplateField>


                    
                            <asp:TemplateField HeaderText="Created_Date" FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left">
					<ItemTemplate>
						<asp:Label ID="lblcreateddate" runat="server" Text ='<%# Bind("Created_Date","{0:dd-MM-yyyy}") %>'></asp:Label>
					</ItemTemplate>
					
							
							<HeaderStyle  CssClass="scrolled"  HorizontalAlign="Center"></HeaderStyle>
							 <FooterStyle  CssClass="scrolled"  Width="900px"></FooterStyle>

					</asp:TemplateField>
--%>


	

				 
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

       

 
       </ContentTemplate>
        </asp:UpdatePanel>

       

       </div>

       </div>
       </div>
       </div>
</asp:Content>
