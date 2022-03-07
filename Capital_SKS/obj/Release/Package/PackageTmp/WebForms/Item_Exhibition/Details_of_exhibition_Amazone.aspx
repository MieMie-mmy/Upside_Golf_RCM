<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Details_of_exhibition_Amazone.aspx.cs" Inherits="ORS_RCM.WebForms.Item_Exhibition.Details_of_exhibition_Amazone_" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<script src="../js/jquery.page-scroller.js"></script>
   <style type ="text/css">
    
 
   </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<body>
<div id="CmnWrapper">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
<div id="CmnContents">
	<div id="ComBlock">
    <div class="setDetailBox shopCmnSet defaultSet iconSet iconRakuten exbCmnSet">
 <h1>amazon出品詳細</h1>
    	<ul class="pageLink">
			<li><a href="#error">エラーメッセージ</a></li>
			<li><a href="#item-txt">item.txt</a></li>
		</ul>

		<h2 id="error">エラーメッセージ</h2>
 

    <table class="errorBox">
    <tbody>
    <tr>
    <td>
        <asp:Label ID="Label1" runat="server" Text="item.txt"></asp:Label>
    </td>
 
    <td>
        <asp:Label ID="lblitemerror" runat="server" Text=""></asp:Label>
    </td>
    </tr>
    </tbody>
    </table>
  
    <h2 id="item-txt">item.txt</h2>
   
    <div>
    <table class="editTable">
    <tbody>
    <tr>
    <th>SKU</th>
 
    <td>
        <asp:Label ID="lblsku" runat="server" Text="lblsku"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>price</th>
  
        <td>
            <asp:Label ID="lblprice" runat="server" Text="Label"></asp:Label></td>
    </tr>
    <tr>
    <th>quantity</th>

    <td>
        <asp:Label ID="lblquantity" runat="server" Text="lblquantity"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>product-id</th>
   
    <td>
        <asp:Label ID="lblproductid" runat="server" Text="Label"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>product-id-type</th>

        <td>
            <asp:Label ID="lblproductidtype" runat="server" Text="lblproductidtype"></asp:Label>
        </td>

    </tr>
    <tr>
    <th>codition-type</th>
   
    <td>
        <asp:Label ID="lblcondition" runat="server" Text="Label"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>codition-note</th>
 
    <td>
        <asp:Label ID="lblconditionnote" runat="server" Text="lblconditionnote"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>ASIN-hint</th>
  
    <td>
        <asp:Label ID="lblasin" runat="server" Text="lblasin"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>title</th>
   
    <td>
        <asp:Label ID="lbltitle" runat="server" Text="lbltitle"></asp:Label></td>
    </tr>
    <tr>
    <th>poaration-type</th>

        <td>
            <asp:Label ID="lblpoartype" runat="server" Text=""></asp:Label>
        </td>
        </tr>
    <tr>
    <th>sale-price</th>

        <td>
            <asp:Label ID="lblsaleprice" runat="server" Text="lblsaleprice"></asp:Label>
        </td>
        </tr>
    <tr>
    <th>sale-start-date</th>
 
    <td>
        <asp:Label ID="lblsalesdate" runat="server" Text="lblsalesdate"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>sale-end-date</th>

    <td>
        <asp:Label ID="lblsaleenddate" runat="server" Text="lblsaleenddate"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>leadtime-to-ship</th>
    
    <td>
        <asp:Label ID="lblleadship" runat="server" Text="lblleadship"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>lunch-date</th>
   
    <td>
        <asp:Label ID="lbllunchdate" runat="server" Text="lbllunchdate"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>ls-giftwrap-available</th>
   
    <td>
        <asp:Label ID="lblgiftava" runat="server" Text="lblgiftava"></asp:Label>
    </td>
    </tr>
    <tr>
    <th>ls-gift-message-available</th>
    
    <td>
        <asp:Label ID="lblgiftmsg" runat="server" Text="lblgiftmsg"></asp:Label>
    </td>
    </tr>
    </tbody>
    </table>
    </div>
    </div>
    </div>
    </div>
 </div>
 </body>
</asp:Content>
