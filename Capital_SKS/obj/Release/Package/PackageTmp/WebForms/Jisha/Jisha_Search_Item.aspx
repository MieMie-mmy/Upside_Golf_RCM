<%@ Page Title="" Language="C#" MasterPageFile="~/JishaMaster.Master" AutoEventWireup="true" CodeBehind="Jisha_Search_Item.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Search_Item" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<style type="text/css">
#btnDetail
{
    color: #FFF;
	font-size: 12px;
	font-weight: bold;
	height: 25px;
	line-height: 25px;
	margin: 10px 0 0 0 ;
	padding: 0;
	overflow: hidden;
	text-align: center;
	width: 134px;
	display: block;
	text-decoration: none;
    
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:DataList ID="datalistshow" runat="server" BackColor="White"  BorderWidth="0"
			BorderStyle="None"   CellPadding="5" 
			Font-Names="Verdana" Font-Size="Small" GridLines="None" 
        RepeatDirection="Horizontal" RepeatColumns="4" 
        onitemcommand="datalistshow_ItemCommand">
   
 <ItemTemplate>
  <table border="0">
				 <tr>
			<td>
        <asp:Image ID="imgsearch" runat="server" Width="100px"　height="100px"  ImageUrl='<%# Bind("Image_Name1", "~/Item_Image/{0}") %>'  />
        </td>
</tr>
<tr>
       <td>
			   <asp:Label ID="lblItem_Name" runat="server"  Text=' <%# Bind("Item_name") %>' BackColor="#f9f9f9" ForeColor="Green" Width="150px" ></asp:Label>
				<br />
			 
		   

			<asp:Label ID="lblSalePrice" runat="server" Text=' <%# Bind("Sale_Price") %>' BackColor="#f9f9f9" ForeColor="Orange"></asp:Label> <font style="color:Orange;  font-size:14px">円</font><br />
         <asp:Button ID="btnDetail" runat="server" Text="商品詳細とご注文"  CommandArgument='<%#Bind("ID") %>' Width="120px" ForeColor="#454545" BackColor="" />   
</td>
<td>
 
</td>
</tr>
        
        </table>
</ItemTemplate>	 
				 
	
  
    </asp:DataList>
</asp:Content>
