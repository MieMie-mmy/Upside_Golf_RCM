<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Library_Image_Log.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Library_Image_Log" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox impirtAtt iconSet iconLog editBox">

		<h1>画像アップロード データ</h1>
       
        <div class="dbCmnSet editBox">
        	<h2>画像名データ</h2>

         <div class="dv">
            <asp:GridView ID="gvimg" runat="server" AllowPaging="True" 
             AutoGenerateColumns="False" 
             PageSize="30" 
                 CssClass="itemIfoIpt listTable" EmptyDataText="There is no data to display!" 
                 ShowHeaderWhenEmpty="True" 
                 onpageindexchanging="gvimg_PageIndexChanging"    >
                <Columns>
                   
                    
                    <asp:BoundField DataField="Image_Name" HeaderText="画像名" />
                    <asp:TemplateField HeaderText="エラー内容">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("Error_Msg") %>' ForeColor="Red"></asp:Label>
                        </ItemTemplate>
                        
                    </asp:TemplateField>
                   
                </Columns>

        </asp:GridView>
       </div>
        </div>
     
        </div>
        
        <!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
