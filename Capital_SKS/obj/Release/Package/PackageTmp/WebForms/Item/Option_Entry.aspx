<%@ Page Title="オプション登録＜商品管理システム＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Option_Entry.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Option_Entry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">


<link rel="stylesheet" href="../../Styles/item.css" />

<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server" >

<div id="CmnContents">
	<div id="ComBlock">
		<div class="setListBox iconSet iconEdit">


			<div id="first_header" runat="server">
            	<asp:HiddenField ID="hdfgroupname" runat="server" />
				<h1>オプション登録・編集</h1>
			</div>
			<div id="pop" runat="server" visible="false">
				<h1>オプションテンプレート確認</h1>
			</div>

			<div id="PopContents" class="OptionBlock2 pop2_Option inlineSet">
				<div id="sect" runat="server" class="OptionBlock resetValue">
				
					<h2>オプション登録</h2>
					<dl>
						<dt>グループ名</dt><dd><asp:TextBox ID="txtgroupName" runat="server" /><asp:Label runat="server" ID="lblgroupName" Visible="false" /></dd>
					</dl>
				<dl>
						<dt>項目名</dt><dd><asp:TextBox ID="txtOptionname" runat="server" /><asp:Label runat="server" ID="lblOptionname" Visible="false" /></dd>
						<dt>選択肢</dt><dd><asp:TextBox ID="txtOptionValue" runat="server" /><asp:Label runat="server" ID="lblOptionValue" Visible="false" /></dd>
					</dl>
					<dl>
						<dt>項目名</dt><dd><asp:TextBox ID="txtOptionname1" runat="server" /><asp:Label runat="server" ID="lblOptionname1" Visible="false" /></dd>
						<dt>選択肢</dt><dd><asp:TextBox ID="txtOptionValue1" runat="server" /><asp:Label runat="server" ID="lblOptionValue1" Visible="false" /></dd>
					</dl>
					<dl>
						<dt>項目名</dt><dd><asp:TextBox ID="txtOptionname2" runat="server" /><asp:Label runat="server" ID="lblOptionname2" Visible="false" /></dd>
						<dt>選択肢</dt><dd><asp:TextBox ID="txtOptionValue2" runat="server" /><asp:Label runat="server" ID="lblOptionValue2" Visible="false" /></dd>
					</dl>
				</div><!-- #sect -->
	
				<div class="iconList h2">
					<h2>オプションリスト</h2>
				</div>
				<asp:DataList ID="DataList1" runat="server" 
					  onitemdatabound="DataList1_ItemDataBound">
				<ItemTemplate>
					<div class="OptionBlock">
						<dl>
							<dt>グループ名<asp:Label ID="Label1" runat="server" Text='<%# Container.ItemIndex+1 %>'></asp:Label></dt>
							<dd><asp:TextBox ID="txtoptGroup" runat="server" Text='<%#Eval("Option_GroupName")%>'/></dd>
						</dl>
						   <asp:Label ID="lblID" runat="server" Text='<%#Eval("ID")%>'  Visible ="false"></asp:Label>
						   <asp:Label ID="lblt1" runat="server" Text='<%#Eval("type1")%>'  Visible ="false"></asp:Label>
						<dl>
							<dt>項目名</dt><dd><asp:TextBox ID="txtname1" runat="server"  Text='<%#Eval("name1")%>' ></asp:TextBox></dd>
							<dt>選択肢</dt><dd><asp:TextBox ID="txtvalue1" runat="server"  Text='<%#Eval("value1")%>' ></asp:TextBox></dd>
						</dl>
							<asp:Label ID="lblID2" runat="server" Text='<%#Eval("ID2")%>'  Visible ="false"></asp:Label>
							<asp:Label ID="lblt2" runat="server" Text='<%#Eval("type2")%>'  Visible ="false"></asp:Label>
						<dl>
							<dt>項目名</dt><dd><asp:TextBox ID="txtname2" runat="server" Text='<%#Eval("name2")%>' ></asp:TextBox></dd>
							<dt>選択肢</dt><dd><asp:TextBox ID="txtvalue2" runat="server"  Text='<%#Eval("value2")%>' ></asp:TextBox></dd>
						</dl>
							<asp:Label ID="lblID3" runat="server" Text='<%#Eval("ID3")%>'  Visible ="false"></asp:Label>
							<asp:Label ID="lblt3" runat="server" Text='<%#Eval("type3")%>'  Visible ="false"></asp:Label>
						<dl>
							<dt>項目名</dt><dd><asp:TextBox ID="txtname3" runat="server" Text='<%#Eval("name3")%>' ></asp:TextBox></dd>
							<dt>選択肢</dt><dd><asp:TextBox ID="txtvalue3" runat="server" Text='<%#Eval("value3")%>' ></asp:TextBox></dd>
						</dl>
					</div>
				</ItemTemplate>
				</asp:DataList>

				<div class="btn">
					<input type="submit" id="btnpopup" onclick="" runat="server" value="確認画面"/>
					<asp:Button ID="btnsave" runat="server" onclick="btnsave_Click" Visible="false"/> 
				</div>

			</div><!-- /#PopContents -->

		</div><!-- /.setListBox iconSet iconEdit -->
	</div><!-- /#ComBlock -->
</div><!-- /#CmnContents -->

</asp:Content>
