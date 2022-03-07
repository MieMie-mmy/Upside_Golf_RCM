<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_Item_Data.aspx.cs" Inherits="ORS_RCM.WebForms.Import.Import_Item_Data" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/database.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/item.css" rel="stylesheet" type="text/css" />

    <link href="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" />
	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	
	<%--<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
	<link href="../../Scripts/jquery.page-scroller.js" />--%>

	<title>商品管理システム＜商品マスタデータインポート＞</title>
    <script type="text/javascript">
    url =
        function openWindow(url) {
            window.open(url, '_blank');
            window.focus();
        }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
        <asp:HiddenField ID="hfcount" runat="server" />
        <asp:HiddenField ID="hfitemcode" runat="server" />
<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconImport">

	<h1 id ="head" runat="server"></h1>

<!-- Link List -->	
		
<!-- /Link List -->	
 
<!--setListBox-->

	<%--</div><!--ComBlock-->--%>
    <div class="dbCmnSet iptList inlineSet">
			<form action="#" method="get">
				<dl>
                     <dt style="width:225px;"><asp:Label ID="Label1" Text="商品情報データ" runat="server"></asp:Label></dt>

                        <dd> <asp:FileUpload runat="server" ID="uplItemData" /></dd>
                   
                </dl>
                <p style="margin-top: 8px;">  <asp:Button ID="btnImport" Height="32px" Width="200px" runat="server" Text="インポート開始" onclick="btnImport_Click" OnClientClick=""  />
                    <input type="submit" id="btnpopup" onclick="this.form.target='_blank';return true;" runat="server" style="width:200px"  Visible ="false"  />
				    </p>
		<%--	</form>--%>
         
	</div>
  
	<div class="setDetailBox iconSet iconCheck editBox">

	<h1 id="headcon" runat="server">オプションデータインポート 確認</h1>
	<p class="attText" id="par" runat="server">下記内容で間違いなければ「更新」ボタンを押してください</p>


    <div class="dbCmnSet editBox" >
    <%--<form action="#" method="get">--%>
    	<h2 id ="hcon" runat="server">オプションデータ</h2>
        <asp:HiddenField ID="hfid" runat="server" />
                <asp:GridView ID="gvdata" runat="server" AutoGenerateColumns="True" 
                    EmptyDataText="There is no data todisplay!" 
            ShowHeaderWhenEmpty="True" CssClass="itemIfoIpt listTable" Width="90%" 
            AllowPaging="True" onpageindexchanging="gvdata_PageIndexChanging" pagesize ="50">
                </asp:GridView>
              
  <div class="btn">
  <%--<input type="button" value="更 新"  onclick="button_Click"  runat="server">--%>
   
     <asp:Button runat="server" Width="130px" ID="btnConfirm_Save" OnClick="buttonupdate_Click" Text="更 新" Visible="false" />
     <%-- <asp:Button ID="buttonupdate" runat="server" Text="更 新" onclick="buttonupdate_Click" />--%>

  </div>
   
  </form>
      </div>
      </div>
   </div>
 </div>
</div><!--CmnContents-->

  
</asp:Content>
