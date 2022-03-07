<%@ Page Title="" Language="C#" MasterPageFile="~/JishaMaster.Master" AutoEventWireup="true" CodeBehind="Jisha_Item_Detail.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Item_Detail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
<asp:GridView ID="gvCategory" runat="server"   AutoGenerateColumns="False" ShowHeader="False"  border="0"
		GridLines="None" style="text-align:justify;width:600px" 
        onrowdatabound="gvCategory_RowDataBound"  >
		<Columns>
		<asp:TemplateField>
		<ItemTemplate>
        <asp:Label runat="server" ID="lblCategory_No" Text='<%# Eval("Category_No") %>' Visible="false" />
        <asp:DataList ID="dlSiteMap" runat="server" RepeatDirection="Horizontal" Width="95%"
        onitemcommand="dlSiteMap_ItemCommand">
        <ItemTemplate>
        <asp:LinkButton ID="LinkButton" runat="server" CommandArgument='<%# Eval("Category_ID") %>' Text='<%# Eval("Description") %>' CommandName="DataEdit" ></asp:LinkButton> >>
        </ItemTemplate>
        </asp:DataList>
		</ItemTemplate>
		</asp:TemplateField>
        </Columns>
</asp:GridView>
        
       <br />
<div>
<asp:Label ID="lblItem_Name" runat="server"></asp:Label>
</div>
<div align="center">
<asp:Image ID="imgItem" runat="server" Width="500px" Height="500px" />
    <br />
</div>
<div align="center">
<asp:Literal ID="lt_Sale_Description" runat="server"></asp:Literal>
    <br />
</div>
<div align="center">
  <asp:Literal ID="lt_Item_Description" runat="server"></asp:Literal>
    <br />
</div>
<div align="center">
<table>
<tr>
<td valign="top">
<asp:Image ID="Image1" runat="server" Width="200px" Height="200px" />
<br />
<div id="divItem_Image" runat="server">

</div>
</td>
<td>
<table>
<tr>
<td>
<asp:Label ID="lblItem_Name1" runat="server"></asp:Label>
</td>
</tr>
<tr>
<td>商品番号: <asp:Label ID="lblItem_Code2" runat="server"></asp:Label> 
<br />--------------------------------------------
</td>
</tr>
<tr>
<td>メーカー希望小売価格（税込）: <asp:Label ID="lblList_Price" runat="server"></asp:Label> 円
<br />--------------------------------------------
</td>
</tr>
<tr>
<td style="color:Red">
ラケプラ特別価格（税込）: <asp:Label ID="lblPrice"   runat="server" Font-Size="Medium"> </asp:Label>円
</td>
</tr>
<tr><%-- Item_Option--%>
<td>
--------------------------------------------
<asp:Panel runat="server" ID="PanelOption" />
</td>
</tr>
<tr>
<td>
--------------------------------------------<br />
<asp:Label ID="Label1" runat="server" Text="下記商品リストよりご希望の色／サイズをお選び下さい。"></asp:Label>
<br />
サイズ x カラー
</td>
</tr>
<tr>
<td>
<div runat="server" id="divSKUTable" >
</div>
<%--<asp:GridView ID="gvSKU" runat="server" EmptyDataText="There is no data to display.">
</asp:GridView>--%>
</td>
</tr>
<tr>
<td>
<asp:Label runat="server" ID="Label3" Text="個数:" Font-Bold="true" />
&nbsp;<asp:TextBox runat="server" ID="txtQuantity" Width="50px" onkeypress="return isNumberKey(event)" />
&nbsp;<asp:Label runat="server" ID="lblWarning"></asp:Label>
<br />
<asp:Button runat="server" ID="btnAdd" Text="買い物かごにいれる" Width="200px" onclick="btnAdd_Click" />
<br />
</td>
</tr>
</table>
</td>
</tr>
</table>

</div>
</div>
</asp:Content>