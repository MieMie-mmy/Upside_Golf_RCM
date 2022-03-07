<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="System_ErrorLog_View.aspx.cs" Inherits="ORS_RCM.WebForms.Item.System_ErrorLog_View" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/database.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
<script type="text/javascript">
    function ddlStatus_Change(ctrl, e) {
        var objvalue = ctrl.id;
        var temp = objvalue.split("_");
        var rowNo = temp[temp.length - 1];
        var status = document.getElementById('MainContent_gvlog_ddlStatus_' + rowNo).value;
        var id = document.getElementById('MainContent_gvlog_lblID_' + rowNo).value;
        $.ajax({
            type: "POST",
            url: "System_ErrorLog_View.aspx/UpdateStatus", //call c# function
            contentType: "application/json;charset=utf-8",
            data: "{'id':'" + id + "','status':'" + status + "'}", //passing id and quantity to update
            dataType: "json",
            success: function (data) {
            },
            error: function (result) {
                alert(result);
            }
        });
     }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconList">

    <div class="itemInfo resetValue searchBox iconEx" >
			<h2>エラー一覧検索</h2>
            <table>
                <tr>
                    <td><asp:Label runat="server" ID="Label1" Text="担当者" /></td>
                    <td><asp:DropDownList runat="server" ID="ddlUserList"></asp:DropDownList></td>
                    <td><asp:Label runat="server" ID="Label3" Text="ステータス"/></td>
                    <td><asp:DropDownList runat="server" ID="ddlStatus">
                                <asp:ListItem Text="Error" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>                                
                                <asp:ListItem Text="All" Value="-1"></asp:ListItem>
                            </asp:DropDownList>
                    </td>
                    <td><asp:Label runat="server" ID="Label2" Text="説明"/></td>
                    <td><asp:TextBox runat="server" ID="txtDetail" TextMode="MultiLine"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="7" align="center">
                        <asp:Button Text="検索" runat="server" ID="btnSearch" Width="150px" 
                            onclick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
        </div>
    <div class="dbCmnSet editBox">
<asp:GridView ID="gvlog" runat="server" AutoGenerateColumns="False" 
	 CellPadding="4"    ForeColor="#333333" GridLines="None" 
	

		EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True" 
		CssClass="listTable itemDb" Width="97%" PageSize="30" 
            onrowdatabound="gvlog_RowDataBound">
  

		<Columns>
			<asp:TemplateField HeaderText="日時 " FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					<ItemTemplate>
						<asp:Label ID="lbdate" runat="server" Text ='<%#Eval("Date","{0:yyyy/MM/dd HH:mm:ss}") %>'></asp:Label>
					</ItemTemplate>
			 </asp:TemplateField>

			 <asp:TemplateField HeaderText="担当者 " ItemStyle-Width="100px">
			 <ItemTemplate>
				 <asp:Label ID="Label1" runat="server" Text='<%#Eval("User_Name") %>'></asp:Label>
			 </ItemTemplate>
			 </asp:TemplateField>
		  <asp:TemplateField HeaderText="説明 " FooterStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" >
					<ItemTemplate>
						<asp:Label ID="lbltype" runat="server" Text ='<%#Eval("ErrorDetail") %>'></asp:Label>
					</ItemTemplate>		  
                    <FooterStyle Width="100px"></FooterStyle>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
			</asp:TemplateField>	
            <asp:TemplateField HeaderText="ステータス">
                <ItemTemplate>
                    <asp:DropDownList ID="ddlStatus" runat="server" onchange="ddlStatus_Change(this, event)">
                        <asp:ListItem Text="Error" Value="0"></asp:ListItem>
                        <asp:ListItem Text="Fixed" Value="1"></asp:ListItem>
                    </asp:DropDownList>
                    <asp:HiddenField runat="server" ID="lblID" Value='<%#Eval("ID") %>' />
                    <asp:Label runat="server" Visible="false" ID="lblStatus" Text='<%#Eval("Status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
		</Columns>
	</asp:GridView>
    </div>
    </div>
    </div>
    </div>
    <div class="btn">
   <uc1:UCGrid_Paging ID="gp" runat="server" /> 
</div>
    </div>
</asp:Content>
