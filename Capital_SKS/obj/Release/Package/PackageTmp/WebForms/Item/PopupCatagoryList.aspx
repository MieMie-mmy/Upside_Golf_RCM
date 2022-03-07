<%@ Page Title="商品管理システム＜ショップカテゴリ＞" Language="C#" AutoEventWireup="true" CodeBehind="PopupCatagoryList.aspx.cs" Inherits="ORS_RCM.PopupCatagoryList"  MaintainScrollPositionOnPostback="true"%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title>商品管理システム＜ショップカテゴリ＞</title>
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="../../Styles/item.css"  type="text/css"/>
<script type="text/javascript">
    window.onload = function () {
        var div = document.getElementById("cateArea");
        var div_position = document.getElementById("div_position");
        var position = parseInt('<%=Request.Form["div_position"] %>');
        if (isNaN(position)) {
            position = 0;
        }
        div.scrollTop = position;
        div.onscroll = function () {
            div_position.value = div.scrollTop;
        };
    };
</script>
</head>
<body class="clNon" >
<div id="PopWrapper">
<section>
	<h1>ショップカテゴリ</h1>
	<div id="PopContents" class="pop3_Scate inlineSet">
	 <form id="form1" runat="server">
	<dl class="popSearch">
		<dt>カテゴリ名検索<span>※注意：スペース区切りで一致検 索</span></dt>
		<dd>
       <asp:TextBox ID="txtdesc" runat="server"  placeholder="例）テニス シューズ プリンス"></asp:TextBox>  
       <asp:TextBox ID="txtcatid" runat="server"  Visible="false"></asp:TextBox>   
       <asp:Button ID="btnSearch" runat="server" Text="検 索" onclick="btnSearch_Click"/>
        </dd>
	   </dl>
    </div>
    <div id="cateArea" style="height: 300px">
        <asp:Button ID="btntreecontrol" runat="server" Text="Expand"  onclick="btntreecontrol_Click" Visible="false"/>
        <asp:TreeView ID="tvCategory" runat="server" Width="80px" ImageSet="Simple" NodeIndent="10" ShowLines="True"  style="float:left;"
            onselectednodechanged="tvCategory_SelectedNodeChanged" 
            ontreenodeexpanded="tvCategory_TreeNodeExpanded" >
        </asp:TreeView>
        
         <input type="hidden" id="div_position" name="div_position" />
   
            
      
           <asp:GridView runat="server" ID="gvSelectedCatagory" CellPadding="4" 
            ForeColor="#333333" GridLines="None" ShowHeader="true"  style="float:right;"
            AutoGenerateColumns="false"  onrowdeleting="gvSelectedCatagory_RowDeleting" Width="300px" >
        <Columns>
        <asp:TemplateField HeaderText="ID" Visible="false">
        <ItemTemplate>
        <asp:Label runat="server" ID="lblID" />
        </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
        <ItemTemplate>
          <asp:Label ID ="lbldesc" runat="server" Text='<%#Eval("CName")%>'></asp:Label>
       
        <asp:TextBox ID="txtserial" runat="server" Text='<%#Eval("Category_SN")%>' 
        onkeypress="return isNumberKey(event)" Width="20px"></asp:TextBox>

        </ItemTemplate>
        </asp:TemplateField>
       <asp:CommandField ShowDeleteButton="true" DeleteText="削除"/>
        </Columns>
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#E9E7E2" />
            <SortedAscendingHeaderStyle BackColor="#506C8C" />
            <SortedDescendingCellStyle BackColor="#FFFDF8" />
            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>
       
     

  

	 

   
   

	<div class="btn">
    <asp:Button runat="server" ID="btnClose" Text="決定" Width="80px" onclick="btnClose_Click" /> &nbsp;&nbsp;
    <asp:Button ID="btnCancel" runat="server" Text="キャンセル" onclick="btnCancel_Click" />
    </div>
      </form>
    
    </section>
    </div>
	
  
</body>
</html>