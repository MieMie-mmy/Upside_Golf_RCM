<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mall_Category_Choice.aspx.cs" Inherits="ORS_RCM.Mall_Category_Choice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>商品管理システム</title> <%--＜ポンパレモールカテゴリ＞<asp:Label runat="server" ID="lblTitle" />--%>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

<link rel="stylesheet" href="../../Styles/base.css" />
<link rel="stylesheet" href="../../Styles/common.css" />
<link rel="stylesheet" href="../../Styles/manager-style.css" />
<link rel="stylesheet" href="../../Styles/item.css" />

<script type="text/javascript">
    function RadioCheck(rb) {
        var dl = document.getElementById("<%=gvMallCategory.ClientID%>");
        var rbs = dl.getElementsByTagName("input");
        var row = rb.parentNode.parentNode;
        for (var i = 0; i < rbs.length; i++) {
            if (rbs[i].type == "radio") {
                if (rbs[i].checked && rbs[i] != rb) {
                    rbs[i].checked = false;
                    break;
                }
            }
        }
    }
</script>

</head>
<body class="clNon">
<div id="PopWrapper">

<section runat="server">
	<h1><asp:Label runat="server" ID="lblheader" /></h1>
	
	<div id="PopContents" class="pop4_Mcate">
	<form runat="server">
	
	<dl class="popSearch">
		<dt>カテゴリ名検 索<span>※注意：スペース区切りで一致検 索</span></dt>
		<dd><asp:TextBox runat="server" ID="txtSearch" placeholder="例）テニス シューズ プリンス" /><asp:Button runat="server" ID="btnSearch" Text="検 索" onclick="btnSearch_Click" /></dd>
	</dl>
	
	<div>
	<p>選択中のショップカテゴリ</p>
		<table>
			<asp:GridView ID="gvMallCategory" runat="server" AutoGenerateColumns="False"
           AllowPaging="True" PageSize="50" CellPadding="4" ForeColor="#333333" GridLines="None" 
             onpageindexchanging="gvMallCategory_PageIndexChanging">
         <AlternatingRowStyle BackColor="White" />
        <Columns>
        <asp:TemplateField HeaderText="ID" Visible="false">
            <ItemTemplate>
                <asp:Label runat="server" ID="lblID" Text='<%# Eval("ID")%>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
        <ItemTemplate>
        <asp:RadioButton runat="server" ID="rdo" onclick ="RadioCheck(this);"/>
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="ショップカテゴリID">
             <ItemTemplate>
             <asp:Label runat="server" ID="lblCategory_ID" Text='<%#Eval("Category_ID") %>' Width="80px" />
             </ItemTemplate>
        </asp:TemplateField>
         <asp:TemplateField HeaderText="ショップカテゴリ">
            <ItemTemplate>
                <asp:Label ID="lblCategoryPath" runat="server" Text='<%#Eval("Category_Path") %>'  Width="525px"/>
            </ItemTemplate> 
      </asp:TemplateField>
      </Columns>
        </asp:GridView>
		</table>
	</div>
	<div class="btn"><p><asp:Button runat="server" ID="btnClose" Text="決定" onclick="btnClose_Click" Width="150px" /></p></div>
	</form>
	</div>
</section>
</div><!--PpoWrapper-->

</body>

</html>
