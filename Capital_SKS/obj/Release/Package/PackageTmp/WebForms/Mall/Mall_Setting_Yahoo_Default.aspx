<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mall_Setting_Yahoo_Default.aspx.cs" Inherits="ORS_RCM.Mall_Setting_Yahoo_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">

<!-- Default Value -->
	<div class="setDetailBox defaultSet iconSet iconYahoo">
		<h1>ヤフーデフォルト値編集<span>全て必須項目。ただし、ブランク可</span></h1>

<table class="shopCmnSet editTable">
 
  <tr>


              <th>ショップ名</th>
 

 <td>
 
 <asp:Label ID="lblShopName" runat="server" Font-Size="Medium"></asp:Label>

 <asp:TextBox ID="txtshopID"  runat="server" Visible="False"></asp:TextBox>
 
 </td>


 <td>
 
 </td>
 </tr>


     <tr>
     
     <th>出品モール</th>
     <td>
       <asp:Label ID="lblMallName" runat="server" Font-Size="Medium"></asp:Label>
     </td>
     
     
     </tr>

 <tr>
 
  　
  <th>重量</th>
 

 <td>
 <asp:TextBox ID="txtweight"  runat="server"></asp:TextBox>
     <asp:Label ID="lblWeight" runat="server"   Visible="False"></asp:Label>
 </td>

  </tr>

  </table>

  
  
  <div class="btn">
   <input type="submit" id="btnpopup" onclick="" runat="server" style="width:200px"  value="確認画面へ"/>
  <asp:Button ID="btnConfirm_Save" runat="server" Text="確認画面へ" 
            onclick="btnConfirm_Click"/>

</div>
  
  
    
    </div>
 </div>
 
</asp:Content>
