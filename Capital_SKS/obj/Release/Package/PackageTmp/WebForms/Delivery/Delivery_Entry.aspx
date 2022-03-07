<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Delivery_Entry.aspx.cs" Inherits="ORS_RCM.WebForms.Delivery.Delivery_Entry" %>

<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8 || charCode == 46)
            return true;
        else return false;
    }
</script>
<script type="text/javascript">
    function ddlpage_change() {
        document.forms[0].target = "";
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
<div id="ComBlock">
<div class="setListBox inlineSet iconSet iconList">
		<h1>発送番号登録</h1>
		<div class="itemInfo resetValue searchBox iconEx" style="height:100px;" >
			<h2>発送番号登録</h2>
            <br />
            <dl>
                <dt><asp:Label ID="Label1" runat="server" Text="ヤフー発送番号"></asp:Label></dt>
                <dd style="width:110px;"><asp:TextBox ID="txtyshippingno" runat="server" Width="70px" onkeypress="return isNumberKey(event)" MaxLength="4"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="***" ForeColor="Red" ControlToValidate="txtyshippingno" ValidationGroup="Group"></asp:RequiredFieldValidator>
                </dd>
                <dt><asp:Label ID="Label2" runat="server" Text="楽天発送番号"></asp:Label></dt>
                <dd style="width:110px;"><asp:TextBox ID="txtrshippingno" runat="server" Width="70px" onkeypress="return isNumberKey(event)" MaxLength="2"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="***" ForeColor="Red" ControlToValidate="txtrshippingno" ValidationGroup="Group"></asp:RequiredFieldValidator>
                </dd>
                <dt><asp:Label ID="Label3" runat="server" Text="お届けの目安"></asp:Label></dt>
                <dd style="width:320px;"><asp:TextBox ID="txtestdelivery" runat="server" Width="280px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="***" ForeColor="Red" ControlToValidate="txtestdelivery" ValidationGroup="Group"></asp:RequiredFieldValidator>
                </dd>
                <dt><asp:Button ID="btnadd" runat="server" Text="追加" OnClick="btnadd_OnClick" Width="70px" ValidationGroup="Group" /></dt>
            </dl>
		</div>
</div><!--setListBox-->
    <div class="operationBtn">
           <p class="itemPage">
			    <asp:DropDownList ID="ddlpage" runat="server" onchange="ddlpage_change(this,event);"  AutoPostBack="true" onselectedindexchanged="ddlpage_SelectedIndexChanged" >
                <asp:ListItem>30</asp:ListItem>
		        <asp:ListItem>50</asp:ListItem>
		        <asp:ListItem>100</asp:ListItem>
		        </asp:DropDownList>
           </p>
    </div>
   <div class="itemCmnSet itemInfo">
	<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>--%>
    <asp:GridView ID="gvdelivery" runat="server" AutoGenerateColumns="False" EmptyDataText="There is no data to display." 
    EnableTheming="False" ForeColor="#333333" GridLines="None" CssClass="deliverysetting" ShowHeaderWhenEmpty="True" AllowPaging="true"
    OnRowCancelingEdit="gvdelivery_RowCancelingEdit" OnRowEditing="gvdelivery_RowEditing" OnRowUpdating="gvdelivery_RowUpdating" OnRowDeleting="gvdelivery_RowDeleting">
       <Columns>
        <asp:TemplateField HeaderText="行" Visible="false">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblID" Text='<%#Bind("ID") %>' Visible="false" />
            </ItemTemplate>
        </asp:TemplateField>
       <asp:TemplateField HeaderText="お届けの目安">
            <ItemTemplate>
                <asp:Label ID="lblestimated" runat="server" Text ='<%#Eval("Estimated") %>'/>
            </ItemTemplate>
           <EditItemTemplate>  
                <asp:TextBox ID="txtestimated" runat="server" Text='<%#Eval("Estimated") %>' onkeypress="return isNumberKey(event)" Width="300px"></asp:TextBox>  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="***" ForeColor="Red" ControlToValidate="txtestimated" ValidationGroup="Group2"></asp:RequiredFieldValidator>
           </EditItemTemplate>
       </asp:TemplateField>
       <asp:TemplateField HeaderText="楽天発送番号">
            <ItemTemplate>
                <asp:Label ID="lblrsetting" runat="server" Text ='<%#Eval("RSetting_Name") %>'/>
            </ItemTemplate>
           <EditItemTemplate>  
                <asp:TextBox ID="txtrsetting" runat="server" Text='<%#Eval("RSetting_Name") %>' onkeypress="return isNumberKey(event)" MaxLength="2"  Width="70px"></asp:TextBox>  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="***" ForeColor="Red" ControlToValidate="txtrsetting" ValidationGroup="Group2"></asp:RequiredFieldValidator>
           </EditItemTemplate>
       </asp:TemplateField>
       <asp:TemplateField HeaderText="ヤフー発送番号">
            <ItemTemplate>
                <asp:Label ID="lblysetting" runat="server" Text ='<%#Eval("YSetting_Name") %>' />
            </ItemTemplate>
           <EditItemTemplate>  
                <asp:TextBox ID="txtysetting" runat="server" Text='<%#Eval("YSetting_Name") %>' onkeypress="return isNumberKey(event)" MaxLength="4"  Width="70px"></asp:TextBox>  
                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="***" ForeColor="Red" ControlToValidate="txtysetting" ValidationGroup="Group2"></asp:RequiredFieldValidator>
           </EditItemTemplate>
       </asp:TemplateField>
       <asp:TemplateField>  
            <ItemTemplate>  
                <asp:Button ID="btn_Edit" runat="server" Text="変更" CommandName="Edit" />
                <asp:Button ID="btn_Delete" runat="server" Text="削除" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this record');" />
            </ItemTemplate>  
            <EditItemTemplate>  
                <asp:Button ID="btn_Update" runat="server" Text="更新" CommandName="Update" ValidationGroup="Group2"/>  
                <asp:Button ID="btn_Cancel" runat="server" Text="キャンセル" CommandName="Cancel"/>  
            </EditItemTemplate>
       </asp:TemplateField>   
       </Columns>
        <HeaderStyle Height="30px" Width="100" Font-Bold="false" Font-Size="12px" BorderStyle="Solid" BorderWidth="1px" BorderColor="#CCCCCC" />
		<PagerSettings Mode="NumericFirstLast" PreviousPageText="Previous" NextPageText="Prev" FirstPageText="First" LastPageText="Next" Position="bottom" Visible="False" />
		<PagerStyle BorderColor="#CCCCCC" BorderStyle="Solid" BorderWidth="1px" BackColor="White" Height="30px" VerticalAlign="Bottom" HorizontalAlign="Center" />
		<RowStyle Height="20px" Font-Size="13px" BorderColor="#CCCCCC" BorderWidth="1px"/>
       </asp:GridView>
     <%--  </ContentTemplate>
       </asp:UpdatePanel>--%>
     </div>
     <!-- /exbition list -->
   <uc1:UCGrid_Paging  runat="server"  ID="gp"/><!-- /List paging -->
</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
