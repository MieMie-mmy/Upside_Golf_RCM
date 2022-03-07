<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Popup_Option.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Popup_Option" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div>

    <table>
    <tr>
        <asp:DataList ID="DataList1" runat="server" BackColor="Gray" BorderColor="#666666"
            BorderStyle="None" BorderWidth="2px" CellPadding="3" CellSpacing="2"
            Font-Names="Verdana" Font-Size="Small" GridLines="Both" RepeatColumns="1" RepeatDirection="Vertical"
            Width="600px" ShowFooter="False" ShowHeader="False">
            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
            <HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
                HorizontalAlign="Center" VerticalAlign="Middle" />
            <HeaderTemplate>
                Option Registration</HeaderTemplate>
            <ItemStyle BackColor="White" ForeColor="Black" BorderWidth="2px" />

    


      
                      
            <ItemTemplate>

          
         &nbsp;  &nbsp; &nbsp;   <b>グループ名 Group Name:</b>


         &nbsp; &nbsp;  &nbsp;    <asp:Label ID="lblGroup" runat="server" Text='<%# Eval("Option_GroupName") %>'></asp:Label>
                <br />

           
            

         &nbsp; &nbsp;  &nbsp;   <b>項目名Item  Name:</b>&nbsp;&nbsp;

                <asp:Label ID="lblName" runat="server" Text='<%# Eval("name1") %>'></asp:Label>
                <br />
         &nbsp; &nbsp;  &nbsp;         <b> Option_Value</b>&nbsp;&nbsp;
                <asp:Label ID="lblValue" runat="server" Text=' <%# Eval("value1") %>'></asp:Label>
                <br />

           

          
            <ItemTemplate>
 
            <asp:RadioButton  ID="rdoOption"   runat="server"  /> 
            </ItemTemplate>
             
         <ItemTemplate>
           <b>項目名Item  Name:</b>

              &nbsp; &nbsp;  <asp:Label ID="Label1" runat="server" Text='<%# Eval("name2") %>'></asp:Label>
                <br />
       &nbsp; &nbsp;  &nbsp;  <b> Option_Value</b>&nbsp; &nbsp;


                <asp:Label ID="Label2" runat="server" Text=' <%# Eval("value2") %>'></asp:Label>
                <br />

        </ItemTemplate>

              <ItemTemplate>
               </ItemTemplate>


          &nbsp; &nbsp;  &nbsp;          <ItemTemplate>
                 <br />
              &nbsp; &nbsp;  &nbsp;     <b>項目名Item  Name:</b>&nbsp; &nbsp;
                <asp:Label ID="Label3" runat="server" Text='<%# Eval("name3") %>'></asp:Label>
                <br />
            &nbsp; &nbsp;  &nbsp;      <b> Option_Value</b>&nbsp; &nbsp;&nbsp;
                <asp:Label ID="Label4" runat="server" Text=' <%# Eval("value3") %>'></asp:Label>
                <br />

                 </ItemTemplate>

                   </ItemTemplate>
              
      

        </asp:DataList>
        </tr>
    </table>
    </div>

</asp:Content>
