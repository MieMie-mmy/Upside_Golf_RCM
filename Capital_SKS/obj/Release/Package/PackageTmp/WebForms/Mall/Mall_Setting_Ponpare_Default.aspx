<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mall_Setting_Ponpare_Default.aspx.cs" Inherits="ORS_RCM.Mall_Setting_Ponpare_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/shop.css" type="text/css"/>

   <link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css" //>
	<link href="../../Scripts/jquery-1.3.min.js" rel="stylesheet" type="text/css" />
	<link href="../../Scripts/jquery.droppy.js" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
   <div id="CmnContents">
	<div id="ComBlock">

<div class="setDetailBox defaultSet iconSet iconPon">
		<h1>ポンパレモールデフォルト値編集<span>全て必須項目。ただし、ブランク可</span></h1>


<table class="shopCmnSet editTable">
<tbody>
<tr>
    <th>ョップ名</th>
    
    <td>
        <asp:Label ID="Label2" runat="server" Text="ラケットプラザ" Visible="false"></asp:Label>
        <asp:Label ID="lblshopname" runat="server" Text="" ></asp:Label>
        </td>
    </tr>

    <tr>
    
    
       <th>出品モール</th> 
        <td>
            <asp:Label ID="Label4" runat="server" Text=" 	ポンパレモール" Visible ="false"></asp:Label>
            <asp:Label ID="lblmall" runat="server" Text="" ></asp:Label>
            </td>
        </tr>
       <tr>
       
       
           <th>販売ステータス</th>
           <td>
               <%--<asp:RadioButtonList ID="rdosalestatus" runat="server" 
                   RepeatDirection ="Horizontal" Width="200px">
               <asp:ListItem  Value="1">未販売 </asp:ListItem>
               <asp:ListItem Selected ="True" Value="2">販売中</asp:ListItem>
               </asp:RadioButtonList>--%>
               <asp:RadioButton runat="server" ID="rdosalestatus1" Text="未販売" GroupName="rdosalestatus" />
               <asp:RadioButton runat="server" ID="rdosalestatus2" Text="販売中" GroupName="rdosalestatus" />
               <br />
               <asp:Label ID="lblsstatue" runat="server" Text="" Visible="false"></asp:Label>
           </td>
           </tr>
           <tr>
          <th> 送料</th>
               <td>
                   <%--<asp:RadioButtonList ID="rdopost" runat="server" RepeatDirection ="Horizontal" 
                       Width="200px">
                   <asp:ListItem Selected="True" Value="0">送料別   </asp:ListItem>
                   <asp:ListItem Value="1">送料込</asp:ListItem>
                   </asp:RadioButtonList>--%>
                <asp:RadioButton runat="server" ID="rdopost1" Text="送料別" GroupName="rdopost" />
                <asp:RadioButton runat="server" ID="rdopost2" Text="送料込" GroupName="rdopost" />
                <br />
                   <asp:Label ID="lblpost" runat="server" Text="" Visible="false"></asp:Label>
               </td>
               </tr>
               <tr>
               <th>個別送料</th>
                   <td>
                       <asp:TextBox ID="txtexship" runat="server"></asp:TextBox>
                       <asp:Label ID="lblexship" runat="server" Text="" Visible="false"></asp:Label>
                       </td>
                   </tr>
                   <tr>
                 
                       <th>代引料</th>
                       <td>
                           <%--<asp:RadioButtonList ID="rdodelivery" runat="server" 
                               RepeatDirection="Horizontal" Width="200px">
                           <asp:ListItem Selected ="True" Value="0">代引料別 </asp:ListItem>
                           <asp:ListItem Value="1">代引料込</asp:ListItem>
                           </asp:RadioButtonList>--%>
                           <asp:RadioButton runat="server" ID="rdodelivery1" Text="代引料別" GroupName="rdodelivery" />
                            <asp:RadioButton runat="server" ID="rdodelivery2" Text="代引料込" GroupName="rdodelivery" />
                            <br />
                           <asp:Label ID="lbldelivery" runat="server" Text="" Visible="false"></asp:Label>
                       </td>
                       </tr>
                       <tr>
                      
                           <th>シークレットセールパスワード</th>
                           <td>
                               <asp:TextBox ID="txtpassword" runat="server"></asp:TextBox>
                               <asp:Label ID="lblpassword" runat="server" Text="" Visible="false"></asp:Label>
                               </td>
                           </tr>

                          
                           </tbody>
</table>
</div>
</div>
</div>

	<div class="btn">
		<div class="userRole">
			<input type="submit" id="btnpopup" onclick="" runat="server" style="width:200px"  value="確認画面へ"/>
			<asp:Button runat="server" Width="130px" ID="btnsave" onclick="btnsave_Click" Text="確認画面へ" Visible="false" />
		</div>     
	</div>


</asp:Content>
