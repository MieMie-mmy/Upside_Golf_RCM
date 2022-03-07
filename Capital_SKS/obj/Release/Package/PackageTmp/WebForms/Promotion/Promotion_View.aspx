<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Promotion_View.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Promotion_View" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css" />
<link href="../../Scripts/jquery-1.3.min.js" rel="stylesheet" type="text/css" />
<link href="../../Scripts/jquery.droppy.js" rel="stylesheet" type="text/css" />

    


	<script type="text/javascript">

		function checkAll(objRef) {
			var GridView = objRef.parentNode.parentNode.parentNode;
			var inputList = GridView.getElementsByTagName("input");
			for (var i = 0; i < inputList.length; i++) {
				//Get the Cell To find out ColumnIndex
				var row = inputList[i].parentNode.parentNode;
				if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
					if (objRef.checked) {
						inputList[i].checked = true;
					}
					else {
						inputList[i].checked = false;
					}
				}
			}
		}

		$(document).ready(function () {
			$(".scroll").click(function (event) {
				$('html,body').animate({ scrollTop: 0 }, "slow");
			});
		});
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<p id="toTop"><a href="#divtop">▲TOP</a></p>
<div id="CmnWrapper">
<div id="CmnContents">
<div id="ComBlock">
<div class="setListBox inlineSet iconSet iconList">
	<h1>プロモーション一覧</h1>

    

<div class="prmCmnSet resetValue searchBox">
			<h2>プロモーション一覧検索</h2>
							<dl>
					<dt>プロモーション</dt>
					<dd><asp:TextBox runat="server" ID="txtPromotion_Name" Width="120px"/>
</dd>
					<dt>ショップ</dt>
					<dd>
						<asp:DropDownList runat="server" ID="ddlShop_Name" Width="180px" AppendDataBoundItems="True">
<asp:ListItem Value="-1" Text="ショップ選択" />
</asp:DropDownList>

					</dd>
					<dt>種別</dt>
					<dd>
<asp:DropDownList runat="server" ID="ddlType"  Width="120px">
<asp:ListItem Value=""></asp:ListItem>
<asp:ListItem Value="0">商品別ポイント</asp:ListItem>
<asp:ListItem Value="1">店舗別ポイント</asp:ListItem>
<asp:ListItem Value="2">商品別クーポン</asp:ListItem>
<asp:ListItem Value="3">送料</asp:ListItem>
<asp:ListItem Value="4">即日出荷</asp:ListItem>
<asp:ListItem Value="5">予約</asp:ListItem>
<asp:ListItem Value="6">事前告知</asp:ListItem>
<asp:ListItem Value="7">シークレット</asp:ListItem>
<asp:ListItem Value="8">プレゼントキャンペーン</asp:ListItem>
</asp:DropDownList>
					</dd>

					<dt>ブランド</dt>
					<dd><asp:TextBox runat="server" ID="txtBrand_Name" Width="140px"/>
</dd>
					<dt>開催ステータス</dt>
					<dd>
<asp:CheckBoxList runat="server" ID="chklStatus"
		RepeatDirection="Horizontal" RepeatLayout="Flow">
		<asp:ListItem Value="0">開催前</asp:ListItem>
		<asp:ListItem Value="1">開催中</asp:ListItem>
		<asp:ListItem Value="2">終了</asp:ListItem>
		<asp:ListItem Value="3">中止</asp:ListItem>
		</asp:CheckBoxList>
</dd>
<dt>開催期間指定</dt>
					<dd>
<asp:RadioButtonList runat="server" ID="rdolPeriod" 
		RepeatDirection="Horizontal" RepeatLayout="Flow">
		<asp:ListItem Value="0" Selected="true">期間指定なし</asp:ListItem>
		<asp:ListItem Value="1">1ヶ月以内</asp:ListItem>
		<asp:ListItem Value="2">3ヶ月以内</asp:ListItem>
		</asp:RadioButtonList>
</dd>
				</dl>
				<p><asp:Button runat="server" ID="btnNew" Text="New" Width="200px" 
		onclick="btnNew_Click" />
&nbsp;&nbsp;<asp:Button runat="server" ID="btnSearch" Text="検 索" Width="200px" 
		onclick="btnSearch_Click" />
</p>
			
		</div>
        </div>
<br />
<div align="center">
<asp:GridView runat="server" ID="gvPromotion" AutoGenerateColumns="False" 
		Width="100%" OnRowCommand="gvPromotion_RowCommand" AllowPaging="true" 
		onpageindexchanging="gvPromotion_PageIndexChanging" DataKeyNames="ID" 
		onrowdatabound="gvPromotion_RowDataBound" CssClass="listTable" >
<Columns>
<%--<asp:TemplateField>
<HeaderTemplate>
		<asp:CheckBox ID="chkAll" runat="server"
			onclick = "checkAll(this);" />
	</HeaderTemplate> 
<ItemTemplate>
<asp:CheckBox runat="server" ID="chkExport" onclick="Check_Click(this)"/>
</ItemTemplate>
</asp:TemplateField>--%>
<asp:TemplateField HeaderText="操作">
<ItemTemplate>
<asp:Button runat="server" ID="btnEdit" Text="編集" Width="80px"
CommandName="DataEdit" CommandArgument='<%# Eval("ID") %>' /><br />
<asp:Button runat="server" ID="btnCopy" Text="コピー" Width="80px"
CommandName="DataCopy" CommandArgument='<%# Eval("ID") %>' />
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="No">
<ItemTemplate>
<asp:Label runat="server" ID="Label1" Text='<% #Eval("No")%>'/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="プロモーション">
<ItemTemplate>
<asp:Label runat="server" ID="Label2" Text='<% #Eval("Promotion_Name")%>'/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="開催期間">
<ItemTemplate>
<asp:Label runat="server" ID="Label3" Text='<% #Eval("Period_From","{0:yyyy/MM/dd  hh:mm}")%>'/>
<asp:Label runat="server" ID="Label4" Text="~"/>
<asp:Label runat="server" ID="Label5" Text='<% #Eval("Period_To","{0:yyyy/MM/dd  hh:mm}")%>'/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField >
<headertemplate>開催ステータス</headertemplate>
<ItemTemplate>
<asp:Label runat="server" ID="lblStatus"/>
</ItemTemplate>
</asp:TemplateField>
<asp:TemplateField HeaderText="ショップ">
<ItemTemplate>
    <asp:Image ID="rbseball" runat="server" ImageUrl ="../../images/Bp.gif"  Visible="false" />
    <asp:Image ID="by" runat="server" ImageUrl ="../../images/Bp_orange.gif"  Visible="false" />
    <asp:Image ID="hp" runat="server" ImageUrl ="../../images/HP_orange.gif"  Visible="false" />
    <asp:Image ID="rlp" runat="server" ImageUrl ="../../images/Lp.gif"  Visible="false" />
    <asp:Image ID="ylp" runat="server" ImageUrl ="../../images/Lp_orange.gif"  Visible="false" />
    <asp:Image ID="rrp" runat="server" ImageUrl ="../../images/Rp.gif"  Visible="false" />
    <asp:Image ID="ablack" runat="server" ImageUrl ="../../images/Rp_black.gif"  Visible="false" />
    <asp:Image ID="yrp" runat="server" ImageUrl ="../../images/Rp_orange.gif"  Visible="false" />
<asp:Image ID="prp" runat="server" ImageUrl ="../../images/Rp_red.gif"  Visible="false" />
<asp:Image ID="rsp" runat="server" ImageUrl ="../../images/Sp.gif"  Visible="false" />
 <asp:Image ID="ysp" runat="server" ImageUrl ="../../images/Sp_orange.gif"  Visible="false" />
 <asp:Image ID="pbp" runat="server" ImageUrl ="../../images/BP_red.gif"  Visible="false" />
<asp:Image ID="abp" runat="server" ImageUrl ="../../images/BP_black.gif"  Visible="false" />
<asp:Image ID="rhp" runat="server" ImageUrl ="../../images/HP_red.gif"  Visible="false" />
<asp:Image ID="ajp" runat="server" ImageUrl ="../../images/jisha.gif"  Visible="false" />
<asp:DropDownList runat="server" ID="ddlShop" Visible ="false"></asp:DropDownList>
</ItemTemplate>
</asp:TemplateField>
</Columns>
    <PagerSettings Visible="False" />
<RowStyle HorizontalAlign="Center" />

</asp:GridView>
</div>
<div class="btn">
	<uc1:UCGrid_Paging ID="UCGrid_Paging1" runat="server" />
</div>

</div><!--ComBlock-->
</div>
</div>
</asp:Content>
