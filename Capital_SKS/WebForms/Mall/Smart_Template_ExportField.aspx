<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Smart_Template_ExportField.aspx.cs" Inherits="ORS_RCM.WebForms.Mall.Smart_Template_ExportField" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	 <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	 <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
		<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />

	<link href="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" />
	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:HiddenField ID="hfID" runat="server" />
<%--<fieldset>--%>
<div id="CmnWrapper">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox inlineSet iconSet iconEdit">
	<h1 id="head" runat="server"></h1>
	<div class="exportBox entryBox">
	<form action="#" method="get">	
<table width="75%"><tr>

	<th>定義名</th>
   <td> <asp:TextBox ID="txtname" runat="server" Width="300px"></asp:TextBox>
	   <asp:Label ID="lbname" runat="server" Text="" Visible="false"></asp:Label>
   </td>
</tr>
<tr>
<th>ダウンロード項目</th>

<td>
	<asp:TextBox ID="txtfield" runat="server" TextMode ="MultiLine" Width="300px"></asp:TextBox>
	<asp:Label ID="lblfield" runat="server" Text="" Visible="false"></asp:Label>  
		</td>
</tr>
<tr>
<th>ステータス</th>

	<td>
		<asp:CheckBox ID="chkstatus" runat="server" />
		<asp:Label ID="lblstatus" runat="server" Text="" Visible="false"></asp:Label>
		</td>
	</tr>
  
</table>


<div>
</form>
</div>

	
	<asp:DataList ID="dtlist" runat="server" 
		CellPadding="7"  
		 onitemdatabound="dtlist_ItemDataBound" Width="100%"      BorderColor="#FF0000" BorderStyle="None" BorderWidth="0.1px" >
	  

			<ItemTemplate>
		   <%--     <asp:TextBox ID="txtID" runat="server" Text='<%#Eval("ID") %>'></asp:TextBox>--%>
		   <table  width="75%"><tr><td >
				<asp:Label ID="Label6" runat="server" Text='<%#Eval("ID") %>' Visible ="false"></asp:Label>
		   </td></tr>
		 <tr>
			<th>定義名</th>
	   
		 <td> <asp:TextBox ID="txtexpname" runat="server" Text='<%#Eval("Export_Name") %>'   Width="300px"></asp:TextBox>
                 
			 <asp:Label ID="lblexname" runat="server" Text='<%#Eval("Export_Name") %>' Visible="false"></asp:Label>

             	
		 </td>  
			   </tr>
			   <tr>
			   <th>ダウンロード項目</th>
			 
			<td>  <asp:TextBox ID="txtexpfield" runat="server" TextMode="MultiLine" Text='<%#Eval("Export_Fields") %>' Enabled="true" Width="300px"></asp:TextBox>
				<asp:Label ID="lbexfi" runat="server" Text='<%#Eval("Export_Fields") %>' Visible="false"></asp:Label>
			</td> 
		</tr>
		<tr>
		<th>ステータス</th>
 
				<td><asp:CheckBox ID="chk" runat="server"   Enabled="true" /></td>
				<td>
					<asp:Label ID="Label7" runat="server" Text='<%#Eval("Status") %>' Visible="false"></asp:Label>
				  </td>
				 </tr>
			  </table>
			</ItemTemplate>
	 
	 
	</asp:DataList>
</div>





	</div><!--setListBox-->

</div><!--ComBlock-->


</div><!--CmnContents-->
	<div class="btn">
		<div class="userRole">
		
			<asp:Button runat="server" Width="130px" ID="btnConfirm_Save" OnClick="btnupdate_Click" Text="登録"  />
		</div>     
	</div>
</asp:Content>
