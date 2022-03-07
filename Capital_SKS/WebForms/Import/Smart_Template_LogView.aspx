<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Smart_Template_LogView.aspx.cs" Inherits="ORS_RCM.Smart_Template_LogView" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />
	<style type="text/css">
	.dv
	{
	 overflow:scroll;   
	 }
	</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setDetailBox impirtAtt iconSet iconLog editBox">

		<h1>商品説明文データインポート</h1>
	   
		<div class="dbCmnSet editBox">
			<h2>商品説明文データ</h2>

		<div class="listTableOver onOver">
	<asp:GridView ID="gvlog" runat="server" AutoGenerateColumns="False" onpageindexchanging="gvlog_PageIndexChanging" AllowPaging="True" PageSize="30" CssClass="listTable itemCatIpt" >
		<Columns>
			<asp:BoundField DataField="チェック" HeaderText="チェック" />
			<asp:BoundField DataField="Item_Code" HeaderText="商品番号" />
			<asp:BoundField DataField="Template1" HeaderText="基本情報1" />
			<asp:BoundField DataField="Template_Content1" HeaderText="基本情報内容1" />
			<asp:BoundField DataField="Template2" HeaderText="基本情報2" />
			<asp:BoundField DataField="Template_Content2" HeaderText="基本情報内容2" />
			<asp:BoundField DataField="Template3" HeaderText="基本情報3" />
			<asp:BoundField DataField="Template_Content3" HeaderText="基本情報内容3" />
			<asp:BoundField DataField="Template4" HeaderText="基本情報4" />
			<asp:BoundField DataField="Template_Content4" HeaderText="基本情報内容4" />
			<asp:BoundField DataField="Template5" HeaderText="基本情報5" />
			<asp:BoundField DataField="Template_Content5" HeaderText="基本情報内容5" />
			<asp:BoundField DataField="Template6" HeaderText="基本情報6" />
			<asp:BoundField DataField="Template_Content6" HeaderText="基本情報内容6" />
			<asp:BoundField DataField="Template7" HeaderText="基本情報7" />
			<asp:BoundField DataField="Template_Content7" HeaderText="基本情報内容7" />
			<asp:BoundField DataField="Template8" HeaderText="基本情報8" />
			<asp:BoundField DataField="Template_Content8" HeaderText="基本情報内容8" />
			<asp:BoundField DataField="Template9" HeaderText="基本情報9" />
			<asp:BoundField DataField="Template_Content9" HeaderText="基本情報内容9" />
			<asp:BoundField DataField="Template10" HeaderText="基本情報10" />
			<asp:BoundField DataField="Template_Content10" HeaderText="基本情報内容10" />
			<asp:BoundField DataField="Template11" HeaderText="基本情報11" />
			<asp:BoundField DataField="Template_Content11" HeaderText="基本情報内容11" />
			<asp:BoundField DataField="Template12" HeaderText="基本情報12" />
			<asp:BoundField DataField="Template_Content12" HeaderText="基本情報内容12" />
			<asp:BoundField DataField="Template13" HeaderText="基本情報13" />
			<asp:BoundField DataField="Template_Content13" HeaderText="基本情報内容13" />
			<asp:BoundField DataField="Template14" HeaderText="基本情報14" />
			<asp:BoundField DataField="Template_Content14" HeaderText="基本情報内容14" />
			<asp:BoundField DataField="Template15" HeaderText="基本情報15" />
			<asp:BoundField DataField="Template_Content15" HeaderText="基本情報内容15" />
			<asp:BoundField DataField="Template16" HeaderText="基本情報16" />
			<asp:BoundField DataField="Template_Content16" HeaderText="基本情報内容16" />
			<asp:BoundField DataField="Template17" HeaderText="基本情報17" />
			<asp:BoundField DataField="Template_Content17" HeaderText="基本情報内容17" />
			<asp:BoundField DataField="Template18" HeaderText="基本情報18" />
			<asp:BoundField DataField="Template_Content18" HeaderText="基本情報内容18" />
			<asp:BoundField DataField="Template19" HeaderText="基本情報19" />
			<asp:BoundField DataField="Template_Content19" HeaderText="基本情報内容19" />
			<asp:BoundField DataField="Template20" HeaderText="基本情報20" />
			<asp:BoundField DataField="Template_Content20" HeaderText="基本情報内容20" />
			<asp:BoundField DataField="Detail_Template1" HeaderText="詳細情報1" />
			<asp:BoundField DataField="Detail_Template_Content1" HeaderText="詳細情報内容1" />
			<asp:BoundField DataField="Detail_Template2" HeaderText="詳細情報2" />
			<asp:BoundField DataField="Detail_Template_Content2" HeaderText="詳細情報内容2" />
			<asp:BoundField DataField="Detail_Template3" HeaderText="詳細情報3" />
			<asp:BoundField DataField="Detail_Template_Content3" HeaderText="詳細情報内容3" />
			<asp:BoundField DataField="Detail_Template4" HeaderText="詳細情報4" />
			<asp:BoundField DataField="Detail_Template_Content4" HeaderText="詳細情報内容4" />
	   <%--     <asp:BoundField DataField="Detail_Template5" HeaderText="詳細情報5" />
			<asp:BoundField DataField="Detail_Template_Content5" HeaderText="詳細情報内容5" />--%>
			<asp:BoundField DataField="Zett_Item_Description" HeaderText="PC商品説明文" />
			<asp:BoundField DataField="Zett_Sale_Description" HeaderText="PC販売説明文" />
			<asp:BoundField DataField="Related_ItemCode1" HeaderText="関連商品1" />
			<asp:BoundField DataField="Related_ItemCode2" HeaderText="関連商品2" />
			<asp:BoundField DataField="Related_ItemCode3" HeaderText="関連商品3" />
			<asp:BoundField DataField="Related_ItemCode4" HeaderText="関連商品4" />
			<asp:BoundField DataField="Related_ItemCode5" HeaderText="関連商品5" />
			<asp:BoundField DataField="Library_Image1" HeaderText="テクノロジー画像1" />
			<asp:BoundField DataField="Library_Image2" HeaderText="テクノロジー画像2" />
			<asp:BoundField DataField="Library_Image3" HeaderText="テクノロジー画像3" />
			<asp:BoundField DataField="Library_Image4" HeaderText="テクノロジー画像4" />
			<asp:BoundField DataField="Library_Image5" HeaderText="テクノロジー画像5" />
			<asp:BoundField DataField="Library_Image6" HeaderText="テクノロジー画像6" />
			<asp:BoundField DataField="Campaign_Image1" HeaderText="キャンペーン画像1" />
			<asp:BoundField DataField="Campaign_Image2" HeaderText="キャンペーン画像2" />
			<asp:BoundField DataField="Campaign_Image3" HeaderText="キャンペーン画像3" />
			<asp:BoundField DataField="Campaign_Image4" HeaderText="キャンペーン画像4" />
			<asp:BoundField DataField="Campaign_Image5" HeaderText="キャンペーン画像5" />
			<asp:BoundField DataField="Error_Message" HeaderText="エラー内容" />
		</Columns>

	</asp:GridView>

	</div>     
		</div>
		
		<!--setDetailBox-->



	</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
