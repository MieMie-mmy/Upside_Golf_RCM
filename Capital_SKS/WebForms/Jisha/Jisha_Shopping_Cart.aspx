<%@ Page Title="" Language="C#" MasterPageFile="~/JishaMaster.Master" AutoEventWireup="true" CodeBehind="Jisha_Shopping_Cart.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Item_OrderDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div>
<asp:GridView runat="server" ID="gvCart" AutoGenerateColumns="False" Width="80%" 
        onrowdeleting="gvCart_RowDeleting" ShowFooter="True"
        
        EmptyDataText="Currently, You have no items in your shopping basket.<br/>Please enjoy shopping by all means! We are looking forward to use." 
        onrowdatabound="gvCart_RowDataBound"  >
        <Columns>
         <asp:BoundField DataField="Item_ID" HeaderText="Item ID" Visible="false" />
         
         <asp:TemplateField HeaderText="画像" >
            <ItemTemplate>
             <asp:Image ID="img"  ImageUrl ='<%# Eval("Image_Name") %>'   runat="server" 
             ControlStyle-Width="80" ControlStyle-Height = "90"   />
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="商　品　情　報">
            <ItemTemplate>
            <asp:Label runat="server" ID="Label3" Text='<%# Eval("Item_Name") +"<br/>￭"+ "サイズ:"+Eval("Size_Name") + "カラー:"+Eval("Color_Name") %>'  />
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="単    価">
            <ItemTemplate>
            <asp:Label runat="server" ID="Label4" Text='<%# "￥"+ Eval("Price")%>'  />
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="数    量">
            <ItemTemplate>
            <%--<asp:Label runat="server" ID="lblQuantity1" Text='<%# Eval("Quantity") +"個" %>'  />--%>
             <asp:Label runat="server" ID="lblQuantity" Text='<%# Eval("Quantity") %>' />
             <asp:Label runat="server" ID="Label1" Text="個" />
            </ItemTemplate>
            <FooterTemplate>
            <asp:Label runat="server" ID="lblTotalQuantity" ForeColor="Red"/>
            <asp:Label runat="server" ID="Label2" Text="個" ForeColor="Red"/>
            
            </FooterTemplate>
            </asp:TemplateField>
       
         <asp:CommandField ShowDeleteButton="true" DeleteText="削除"/>

         <asp:TemplateField HeaderText="小   計">
            <ItemTemplate>
            <%--<asp:Label runat="server" ID="lblAmount1" Text='<%# Eval("Amount") +"円" %>'  />--%>
            <asp:Label runat="server" ID="lblAmount" Text='<%# Eval("Amount") %>' />
            <asp:Label runat="server" ID="Label5" Text="円" />
            </ItemTemplate>
            <FooterTemplate>
            <asp:Label runat="server" ID="Label7" Text=" 合計金額： " ForeColor="Red"/>
            <asp:Label runat="server" ID="lblTotalAmount" ForeColor="Red"/>
            <asp:Label runat="server" ID="Label6" Text="円" ForeColor="Red"/>
            </FooterTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle HorizontalAlign="Center" />
    <FooterStyle HorizontalAlign="Center" />
    <RowStyle HorizontalAlign="Center" />
</asp:GridView>

    <br />
    <asp:Button runat="server" ID="btnContinue" Text="Continue Buy" Width="200px" 
        onclick="btnContinue_Click" /> &nbsp;&nbsp;&nbsp;
        <asp:Button runat="server" ID="btnOrder" Text="Order Buy" Width="200px" onclick="btnOrder_Click" 
         />

</div>
</asp:Content>
