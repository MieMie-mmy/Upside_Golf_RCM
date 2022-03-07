<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Smart_Template_Edit_Confirmatiion.aspx.cs" Inherits="ORS_RCM.WebForms.Mall.Smart_Template_Edit_Confirmatiion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

  <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css" />


     


<title>商品管理システム＜スマートテンプレート編集確認＞</title>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<div id="CmnWrapper">


<div id="HeaderWrapper" class="inlineSet">

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox inlineSet iconSet iconUser">
	
    <h1>スマートテンプレート編集確認</h1>

		<div class="smartTmp confBox entryBox">
					
			<div>
            <table>
          <tbody>
            <asp:DataList ID="DataList1" runat="server"  
            BorderStyle="None" CellPadding="4" CellSpacing="4"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="1" RepeatDirection="Vertical"
            headerstyle-font-size="12pt" headerstyle-horizontalalign="center" headerstyle-font-bold="True"
            itemstyle-backcolor="#778899" itemstyle-forecolor="#ffffff" 
            footerstyle-font-size="9pt" footerstyle-font-italic="True" Width="600px" ShowFooter="False" ShowHeader="False">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />

            <HeaderStyle BackColor="Aqua" Font-Bold="True" Font-Size="Large" ForeColor="White"
                HorizontalAlign="Center" VerticalAlign="Middle" />
            
            <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="2px" />

            <HeaderTemplate>Option Detail View</HeaderTemplate>

 
             <%-- <SelectedItemTemplate>
         
        
               <tr>
              
              <td>   
              <b> <%# DataBinder.Eval(Container.DataItem, "Status") %> </td>
               </tr>

               <tr>
              <td>
                   
                     <%# DataBinder.Eval(Container.DataItem, "Template_ID") %>

                </td>

                </tr> 

                <tr>
                <td>
                   
                     <%# DataBinder.Eval(Container.DataItem, "Shop_ID") %>
                  
                </td>

                 </tr>


                 <tr>
                 <td>

                     <%# DataBinder.Eval(Container.DataItem, "Shop_Name") %>
            
               </td>
                  </tr>


                  <tr>
                  
                  <td>

                     <%# DataBinder.Eval(Container.DataItem, "Template_Description") %>
                    

                     </td>

                     </tr>

                 </b>
             </SelectedItemTemplate>--%>


            <ItemTemplate>
          
            <table>
             <tbody>
         <tr>    

       <th>ステータス</th>

         <td>
                      <asp:Label    ID="lblStatus"    runat="server"  Text='<%# DataBinder.Eval(Container.DataItem,"Status") %> '></asp:Label>
         </td>


         </tr>
                   

                   <tr>
               
                  <th>スマートテンプレート</th >
                    
                
                   <td>  <asp:Label ID="lblTemplateID" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Template_ID") %>' ></asp:Label></td>

                     </tr>

                     <tr>
             
                           <td><asp:Label ID="lblShopID" runat="server"   Text='<%# DataBinder.Eval(Container.DataItem, "Shop_ID") %>'  Visible="false"></asp:Label></td>
                
                     </tr>

             <tr>  
             
               <th>ショップ</th>
                                          <td><asp:Label ID="lblShopName"  runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Shop_Name") %>'></asp:Label>
                     
                     </td>
             </tr>

                <tr>
                
              <th>テンプレート</th> <td><asp:Label ID="lblTemDescription" runat="server"   Text='<%# DataBinder.Eval(Container.DataItem, "Template_Description") %> '></asp:Label></td>
                  


             </tr>

       


                 </table>


          </ItemTemplate>

           
            </asp:DataList>
        

        <//tbody>
        </table>
        </div>

			
		</div>







            </div>
            </div>
            </div>




            </div>

            </div>
            
            <div class="btn">
            
            <asp:Button ID="BtnSave" runat="server" Text="登録する"  Width="300px" 
                    onclick="BtnSave_Click"></asp:Button>
            
          
            
            </div>
</asp:Content>
