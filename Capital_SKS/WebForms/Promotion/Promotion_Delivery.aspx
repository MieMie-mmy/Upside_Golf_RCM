<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Promotion_Delivery.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Promotion_Delivery" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/promotion_base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion_common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion_manager_style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion_promotion.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion_Item_style.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
<link href="../../Styles/promotion_pagesno.css" rel="stylesheet" />

<script type="text/javascript">
	function CallTab1() {
		document.forms[0].target = "";
	}
</script>
<script type="text/javascript" language="javascript">
function pageLoad(sender, args) {
$(document).ready(function () {
$("#<%=txtRperiodto.ClientID %>").datepicker(
{ 
showOn: 'button',
dateFormat: 'yy/mm/dd ',
buttonImageOnly: true,
buttonImage:'../../images/calendar.gif',
changeMonth: true,
changeYear: true,
yearRange: "1900:2030" ,
}
);
$(".ui-datepicker-trigger").mouseover(function () {
$(this).css('cursor', 'pointer');
});

});
$(document).ready(function () {
$("#<%=txtRperiodfrom.ClientID %>").datepicker(
{ 
showOn: 'button',
dateFormat: 'yy/mm/dd ',
buttonImageOnly: true,
buttonImage:'../../images/calendar.gif',
changeMonth: true,
changeYear: true,
yearRange: "1900:2030" ,
}
);
$(".ui-datepicker-trigger").mouseover(function () {
$(this).css('cursor', 'pointer');
});

});
}
</script>
	</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<%--<p id="toTop"><a href="#divtop">▲TOP</a></p>--%>
<div id="CmnContents">
	<div id="ComBlock" runat="server" style="margin-top: 60px;">
	 <div class="iconSet iconCheck" id ="confirm" runat="server" visible="false">
			<h1>即日出荷登録設定</h1>
		</div>
			<div class="setListBox inlineSet iconSet iconList" id="title1" runat="server">
		<h1>即日出荷登録設定</h1>

		<div class="prmCmnSet prmSameday resetValue searchBox" id="title2" runat="server">
			<h2>即日出荷登録検索</h2>
			<div class="block1"  id="b1"  runat="server">
				<dl>
					<dt>商品番号</br>完全<asp:CheckBox ID="chkcheck" runat="server" Text=""/></dt>
					<dd> <asp:TextBox ID="txtitemcode" runat="server" Width="231px" Height="45px"></asp:TextBox> </dd>


					<dt>商品名</dt>
					<dd><asp:TextBox ID="txtitemname" runat="server" Width="337px"></asp:TextBox></dd>
					
					<dt>対象ブランド</dt>
					<dd> <asp:TextBox ID="txtbrandname" runat="server" Width="240px"></asp:TextBox></dd>
					
					<dt>即日出荷設定</dt>
					<dd> <asp:RadioButton ID="rdoship2" runat="server" Value="0" GroupName="rbd" Text="なし"/>
	  <asp:RadioButton ID="rdoship1" runat="server" Value="1" GroupName="rbd" 
		  Text="あり" Checked="True" /></dd>
				</dl>
			</div>

			<div class="block2" id="b2" runat ="server">
				<dl>
					<dt>対象ショップ</dt>
					<dd>
						   <asp:ListBox ID="listshop" runat="server" Height="57px" Width="153px" 
		Enabled="True" SelectionMode="Multiple"></asp:ListBox>
					</dd>
				</dl>
			</div>


			<p><asp:Button ID="btnsearch" runat="server" Text="検索" Width="131px"  
		 onclick="btnsearch_Click"/></p>
			

			<div class="prmSamedaySet">
				<h2 class="active">即日出荷登録設定</h2>
				<div class="block3" id="b3" runat="server">
					<dl >					
						<dt>即日出荷設定</dt>
						<dd> <asp:RadioButton ID="rdoship4" runat="server"  Value="0" GroupName="rbd1" Text="なし"/>
 <asp:RadioButton ID="rdoship3" runat="server"  Value="1" GroupName="rbd1" 
		Text="あり" Checked="True"/></dd>

						<dt>開催期間</dt>
						<dd>
							  <asp:TextBox ID="txtRperiodfrom" runat="server" ReadOnly="true" Width="94px"></asp:TextBox>
							  <asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom" />
                		  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"  ErrorMessage="" 
                                 ControlToValidate="txtRperiodfrom"  ForeColor="Red" ValidationGroup="MyValidation"></asp:RequiredFieldValidator>
	 &nbsp;<asp:DropDownList runat="server" ID="ddlPeriodFromHour" Height="22px">
					<asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>01</asp:ListItem>
					<asp:ListItem>02</asp:ListItem>
					<asp:ListItem>03</asp:ListItem>
					<asp:ListItem>04</asp:ListItem>
					<asp:ListItem>05</asp:ListItem>
					<asp:ListItem>06</asp:ListItem>
					<asp:ListItem>07</asp:ListItem>
					<asp:ListItem>08</asp:ListItem>
					<asp:ListItem>09</asp:ListItem>
					<asp:ListItem>10</asp:ListItem>
					<asp:ListItem>11</asp:ListItem>
					<asp:ListItem>12</asp:ListItem>
					<asp:ListItem>13</asp:ListItem>
					<asp:ListItem>14</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>16</asp:ListItem>
					<asp:ListItem>17</asp:ListItem>
					<asp:ListItem>18</asp:ListItem>
					<asp:ListItem>19</asp:ListItem>
					<asp:ListItem>20</asp:ListItem>
					<asp:ListItem>21</asp:ListItem>
					<asp:ListItem>22</asp:ListItem>
					<asp:ListItem>23</asp:ListItem>
				   
					</asp:DropDownList>
							:<asp:DropDownList runat="server" ID="ddlRPeriodFromMinute" Height="24px">
					<asp:ListItem>00</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>30</asp:ListItem>
					<asp:ListItem>45</asp:ListItem>
					<asp:ListItem>59</asp:ListItem>
					</asp:DropDownList>
							:00 ～
								<asp:TextBox ID="txtRperiodto" runat="server" ReadOnly="true" 
		Width="87px"></asp:TextBox>
      
		<asp:ImageButton ID="ImageButton2" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton2_Click" ImageAlign="AbsBottom" />
                  <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                  ErrorMessage="" ControlToValidate="txtRperiodto"  ForeColor="Red" ValidationGroup="MyValidation"></asp:RequiredFieldValidator>
	 <asp:DropDownList runat="server" ID="ddlperiodtohour" Height="25px">
                    <asp:ListItem>00</asp:ListItem>
					<asp:ListItem>01</asp:ListItem>
					<asp:ListItem>02</asp:ListItem>
					<asp:ListItem>03</asp:ListItem>
					<asp:ListItem>04</asp:ListItem>
					<asp:ListItem>05</asp:ListItem>
					<asp:ListItem>06</asp:ListItem>
					<asp:ListItem>07</asp:ListItem>
					<asp:ListItem>08</asp:ListItem>
					<asp:ListItem>09</asp:ListItem>
					<asp:ListItem>10</asp:ListItem>
					<asp:ListItem>11</asp:ListItem>
					<asp:ListItem>12</asp:ListItem>
					<asp:ListItem>13</asp:ListItem>
					<asp:ListItem>14</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>16</asp:ListItem>
					<asp:ListItem>17</asp:ListItem>
					<asp:ListItem>18</asp:ListItem>
					<asp:ListItem>19</asp:ListItem>
					<asp:ListItem>20</asp:ListItem>
					<asp:ListItem>21</asp:ListItem>
					<asp:ListItem>22</asp:ListItem>
					<asp:ListItem>23</asp:ListItem>
						   </asp:DropDownList>
							  :<asp:DropDownList runat="server" ID="ddlRPeriodToMinute" Height="24px">
					<asp:ListItem>00</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>30</asp:ListItem>
					<asp:ListItem>45</asp:ListItem>
					<asp:ListItem>59</asp:ListItem>
					</asp:DropDownList>:59
						</dd>
					</dl>
				</div>
			   
				<p> <asp:Button ID="btnsetting" runat="server" Text="一括設定" Width="126px" 
						onclick="btnsetting_Click"  ValidationGroup="MyValidation"/></p>
			
			</div><!-- /.prmPointSet -->
		</div><!-- /.searchBox -->

	</div><!--setListBox-->
</div><!--ComBlock-->
</div><!--CmnContents-->

<div id="CmnContents2">

<div id="ComBlock2">

	<div class="widthhMax iconEx operationBtn">

		<div class="operationBtn">
			<p>
		<asp:Button ID="btnselectall" runat="server" Text="全て選択" onclick="btnselectall_Click" 
					Width="77px"/>
		<asp:Button ID="btncancelall" runat="server" Text="全て解除" onclick="btncancelall_Click" 
					Width="72px"/>

			</p>
			<p class="itemPage">
				<asp:DropDownList ID="ddlpage" runat="server" AutoPostBack="true" onselectedindexchanged="ddlpage_SelectedIndexChanged">
			<asp:ListItem>30</asp:ListItem>
			<asp:ListItem>50</asp:ListItem>
			<asp:ListItem>100</asp:ListItem>
			</asp:DropDownList>
			</p>
		</div><!-- /.operationBtn -->
	</div><!-- /.widthhMax -->

</div><!-- /#ComBlock2 -->

<div class="prmCmnSet resetValue listBox">
<asp:UpdatePanel runat="server">
<ContentTemplate>
	<asp:GridView ID="gvview" runat="server" AutoGenerateColumns="False" 
		CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="listTable" 
		EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True"  
			AllowPaging="True"  PageSize="30" onrowdatabound="gvview_RowDataBound">
		<AlternatingRowStyle BackColor="White" />
		<Columns>
		  <asp:TemplateField >
			
			 
			<ItemTemplate>
					<asp:CheckBox ID="chktype"  runat="server">
			  </asp:CheckBox> 
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="商品番号">
			<ItemTemplate>
					<asp:Label ID="lblItemCode" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="商品名">
			<ItemTemplate>
					<asp:Label ID="lblItemName" runat="server" Text='<%#Eval("Item_Name") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="Shop ST" ItemStyle-CssClass="stSet sksST shopST" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
			<ItemTemplate>
			<p id="PWait" runat="server" class="wait"></p>
					<p id="POk" runat="server" class="ok"></p>
					<p id="PDel" runat="server" class="del"></p>
					<asp:Label ID="lblCtrlID" runat="server" Text='<%#Eval("Ctrl_ID") %>' Visible="false"></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="対象ショップ" >
			<ItemTemplate>
			   
					<asp:Label ID="lblShopName" runat="server" Text='<%#Eval("Shop_Name") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			   <asp:TemplateField HeaderText="開催期間">
			<ItemTemplate>
					<asp:Label ID="lblHeldPeriod" runat="server" Text='<%#Eval("Held_period") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="項目名">
			<ItemTemplate>
					<asp:Label ID="lblBrandName" runat="server" Text='<%#Eval("Brand_Name") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="即日出荷設定">
			<ItemTemplate>
					<asp:Label ID="lblIsDelivery" runat="server" Text='<%#Eval("IsDelivery") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
		 <asp:TemplateField HeaderText="ID" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
		<asp:TemplateField HeaderText="ShopID" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblShopID" runat="server" Text='<%#Eval("Shop_ID") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
		<asp:TemplateField HeaderText="start" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblStartDate" runat="server" Text='<%#Eval("Delivery_StartDate") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
		<asp:TemplateField HeaderText="end" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblEndDate" runat="server" Text='<%#Eval("Delivery_EndDate") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="starttime" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblStartTime" runat="server" Text='<%#Eval("Delivery_StartTime") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
		<asp:TemplateField HeaderText="endtime" Visible = "false">
			<ItemTemplate>
				<asp:Label ID="lblEndTime" runat="server" Text='<%#Eval("Delivery_EndTime") %>'></asp:Label>
			</ItemTemplate>
		</asp:TemplateField>
		</Columns>
		<PagerSettings Visible="False" />
	</asp:GridView>
</ContentTemplate>
</asp:UpdatePanel>

	<div align="center">
	   <asp:Button runat="server" ID="btnSave" Visible = "false" OnClick="btnSave_Click" Text="確認画面へ"  />
	   </div>
	</div><!-- /.prmCmnSet -->
</div><!-- /#CmnContents2 -->
<script type="text/javascript">
	function CallTab1() {
		document.forms[0].target = "";
	}
</script>
<div class="btn">
		<uc1:ucgrid_paging ID="gp" runat="server" visible="true" />
		</div>
		</asp:Content>


