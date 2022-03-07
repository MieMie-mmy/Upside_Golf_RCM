<%@ Page Title="商品管理システム＜プロモーション編集＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Promotion.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Promotion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
  
<meta http-equiv="X-UA-Compatible" content="IE=edge" />
<!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

<link rel="stylesheet" href="../../Styles/base.css" />
<link rel="stylesheet" href="../../Styles/common.css" />
<link rel="stylesheet" href="../../Styles/manager-style.css" />
<link rel="stylesheet" href="../../Styles/promotion.css" />

<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
<link href="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" />

<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>

<title>商品管理システム＜プロモーション編集＞</title>

<script type="text/javascript" language="javascript">
function pageLoad(sender, args) {
$(document).ready(function () {
$("#<%=txtPeriod_From.ClientID %>").datepicker(
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
$("#<%=txtPeriod_To.ClientID %>").datepicker(
{ 
showOn: 'button',
dateFormat: 'yy/mm/dd',
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

//var disabled = $('#txtPeriod_To').attr('disabled');
//            if (disabled == true)
//            {
//              
//                $("#<%=txtPeriod_To.ClientID %>").datepicker('enable');
//                
//            }
//            else
//            {
//               
//                $("#<%=txtPeriod_To.ClientID %>").datepicker('disable');
//            }

//var disabled = $('#txtPeriod_From').attr('disabled');
//            if (disabled == true)
//            {
//                $("#<%=txtPeriod_From.ClientID %>").datepicker('enable');
//            }
//            else
//            {
//                $("#<%=txtPeriod_From.ClientID %>").datepicker('disable');
//            }

}

function ShowOption(SourceID) {
		var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
		hidSourceID.value = SourceID;
		//declare a string variable
		var retval = "";
		//show modal dialog box and collect its return value
		retval = window.showModalDialog
	  ('../Item/Item_Option_Select1.aspx', window, 'dialogHeight:1000px; dialogWidth:1000px; dialogLeft:200px; dialogRight :200px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
	}   
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server" />--%>
<asp:HiddenField ID="CustomHiddenField" runat="server" />


<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
	<div class="setListBox iconSet iconEdit" runat="server" id ="test">
    
		<h1  runat="server" id="head"></h1>
		<form>
		<div class="cmnEdit prmCmnSet prmEntry inlineSet">

			<div id="block1" class="parts">
				<dl>
					<dt><asp:Label ID="lblPromotionName" runat="server" Text="プロモーション" ToolTip="Promotion_Name" ></asp:Label></dt>
					<dd><asp:TextBox runat="server" ID="txtPromotion_Name" />
                            <asp:Label ID="lblProName" runat="server" Text="" Visible ="false"></asp:Label>
	                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
		                        ControlToValidate="txtPromotion_Name" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    </dd>
				</dl>
				<dl>
					<dt><asp:Label ID="lblCampaingGuidelines" runat="server" Text="キャンペーン要項" ToolTip="Campaign Guidelines"></asp:Label></dt>
					<dd><asp:TextBox runat="server" ID="txtCampaign_Guideline" TextMode="MultiLine"/>
                            <asp:Label ID="lbltxtCam_Guideline" runat="server" Text="" Visible="false"></asp:Label>
                    </dd>
				</dl>
				<dl>
					<dt><asp:Label ID="lblBrandName" runat="server" Text="ブランド" ToolTip="Brand Name"></asp:Label></dt>
					<dd><asp:TextBox ID="txtBrand_Name" runat="server"  /><asp:Label ID="lblbrand_name" runat="server" Text="" Visible="false"></asp:Label></dd>
				</dl>
			   <dl>
					<dt><asp:Label ID="Label3" runat="server" Text="開催期間" ToolTip="Holding Period"></asp:Label></dt>
					<dd>
                    <%--<input type="datetime-local"> ～　<input type="datetime-local">--%>
                    <asp:HiddenField ID="hfperiod" runat="server" />
                    <asp:HiddenField ID="hfperioto" runat="server" />
                    <asp:TextBox ID="txtpfrom" runat="server" ReadOnly="true" Visible="false" />
                    <asp:TextBox ID="txtPeriod_From" runat="server" ReadOnly="true"  />
                    <asp:Label ID="lblPeriod_from" runat="server" Text="" Visible="false"></asp:Label>
                    <asp:DropDownList runat="server" ID="ddlPeriodFromHour">
                    <asp:ListItem>1</asp:ListItem>
                    <asp:ListItem>2</asp:ListItem>
                    <asp:ListItem>3</asp:ListItem>
                    <asp:ListItem>4</asp:ListItem>
                    <asp:ListItem>5</asp:ListItem>
                    <asp:ListItem>6</asp:ListItem>
                    <asp:ListItem>7</asp:ListItem>
                    <asp:ListItem>8</asp:ListItem>
                    <asp:ListItem>9</asp:ListItem>
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
                    <asp:Label runat="server" ID="Label1" Text=":" />
                    <asp:DropDownList runat="server" ID="ddlPeriodFromMinute">
                    <asp:ListItem>00</asp:ListItem>
                    <asp:ListItem>15</asp:ListItem>
                    <asp:ListItem>30</asp:ListItem>
                    <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>59</asp:ListItem>
                    </asp:DropDownList>
                     ～
                     <asp:TextBox ID="txtpto" runat="server" ReadOnly="true" Visible="false"/>
                     <asp:TextBox ID="txtPeriod_To" runat="server" ReadOnly="true"/>
                     <asp:Label ID="lblperiod_to" runat="server" Text="" Visible="false"></asp:Label>
                     <asp:DropDownList runat="server" ID="ddlPeriodToHour">
                    <asp:ListItem>1</asp:ListItem>
	                <asp:ListItem>2</asp:ListItem>
	                <asp:ListItem>3</asp:ListItem>
	                <asp:ListItem>4</asp:ListItem>
	                <asp:ListItem>5</asp:ListItem>
	                <asp:ListItem>6</asp:ListItem>
	                <asp:ListItem>7</asp:ListItem>
	                <asp:ListItem>8</asp:ListItem>
	                <asp:ListItem>9</asp:ListItem>
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
                <asp:Label runat="server" ID="Label2" Text=":" />
                <asp:DropDownList runat="server" ID="ddlPeriodToMinute">
                <asp:ListItem>00</asp:ListItem>
	                <asp:ListItem>15</asp:ListItem>
	                <asp:ListItem>30</asp:ListItem>
	                <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>59</asp:ListItem>
                    
                </asp:DropDownList>
                <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="* Invalid Date" 
                ControlToCompare="txtPeriod_From" ControlToValidate="txtPeriod_To" ForeColor="Red" Operator="GreaterThan" />
                 </dd>
			</dl>
		</div>

			<div id="block2">
				<dl>
					<dt><asp:Label ID="Label4" runat="server" Text="キャンペーン種別" ToolTip="Campaign type"></asp:Label></dt>
					<dd>
						<p><asp:CheckBoxList runat="server" ID="chkCampaingType" RepeatColumns="5" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0">商品別ポイント</asp:ListItem>
                                <asp:ListItem Value="1">店舗別ポイント</asp:ListItem>
                                <asp:ListItem Value="2">商品別クーポン</asp:ListItem>
                                <asp:ListItem Value="3">送料</asp:ListItem>
                                <asp:ListItem Value="4">即日出荷</asp:ListItem>
                                <asp:ListItem Value="5">予約</asp:ListItem>
                                <asp:ListItem Value="6">事前告知</asp:ListItem>
                                <asp:ListItem Value="7">シークレットセール</asp:ListItem>
                                <asp:ListItem Value="8">プレゼントキャンペーン</asp:ListItem>
                                </asp:CheckBoxList></p></dd>
                                <dd>
				
                                 <asp:Label ID="lblCampaingtype" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Label ID="lblCampaingtype1" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Label ID="lblCampaingtype2" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:Label ID="lblCampaingtype3" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Label ID="lblCampaingtype4" runat="server" Text="" Visible="false"></asp:Label></br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                  <asp:Label ID="lblCampaingtype5" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:Label ID="lblCampaingtype6" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                     <asp:Label ID="lblCampaingtype7" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                   <asp:Label ID="lblCampaingtype8" runat="server" Text="" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</dd>
                        
				</dl>
			</div>

			<div id="block3" class="parts">
				<dl>
					<dt><asp:Label ID="Label5" runat="server" Text="ポイント/倍率" ToolTip="Point"></asp:Label></dt>
						<dd>
							<p>楽天</p>
							<asp:DropDownList ID="ddlRakuten_MagnificationID" runat="server" >
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="1">1</asp:ListItem>
                            <asp:ListItem Value="2">2</asp:ListItem>
                            <asp:ListItem Value="3">3</asp:ListItem>
                            <asp:ListItem Value="4">4</asp:ListItem>
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="6">6</asp:ListItem>
                            <asp:ListItem Value="7">7</asp:ListItem>
                            <asp:ListItem Value="8">8</asp:ListItem>
                            <asp:ListItem Value="9">9</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="lblRakuten_ID" runat="server" Text="" Visible="false"></asp:Label>
						</dd>
						<dd>
							<p>Yahoo</p>
                            <asp:DropDownList ID="ddlYahoo_MagnificationID" runat="server" >
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
							<asp:ListItem Value="1">D05</asp:ListItem>
							<asp:ListItem Value="2">Y03</asp:ListItem>
							<asp:ListItem Value="3">Y05</asp:ListItem>
							<asp:ListItem Value="4">X03</asp:ListItem>
						    <asp:ListItem Value="5">Z10</asp:ListItem>
							<asp:ListItem Value="6">Z15</asp:ListItem>
						    <asp:ListItem Value="7">X07</asp:ListItem>
							<asp:ListItem Value="8">X10</asp:ListItem>
							<asp:ListItem Value="9">X15</asp:ListItem>
                            <asp:ListItem Value="10">A10</asp:ListItem>
                            </asp:DropDownList>
                            <br />
                            <asp:Label ID="lblYahoo_ID" runat="server" Text="" Visible="false"></asp:Label>
						</dd>
						<dd>
							<p>ポンパレ</p>
                        <asp:DropDownList ID="ddlPonpare_MagnificationID" runat="server" >
                        <asp:ListItem Value="0">--Select--</asp:ListItem>
                         <asp:ListItem Value="4">4</asp:ListItem>
                        <asp:ListItem Value="5">5</asp:ListItem>
                        <asp:ListItem Value="6">6</asp:ListItem>
                        <asp:ListItem Value="7">7</asp:ListItem>
                        <asp:ListItem Value="8">8</asp:ListItem>
                        <asp:ListItem Value="9">9</asp:ListItem>
                        <asp:ListItem Value="10">10</asp:ListItem>
                        </asp:DropDownList> 
                        <br />
                        <asp:Label ID="lblponpare" runat="server" Text="" Visible="false"></asp:Label>
						</dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label6" runat="server" Text="シークレットID" ToolTip="Secret_ID" ></asp:Label></dt>
					<dd><asp:TextBox runat="server" ID="txtSecret_ID"  />
                    <asp:Label ID="lblSecretID" runat="server" Text="" Visible="false" autocomplete ="off"></asp:Label>
                    </dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label7" runat="server" Text="シークレットパスワード" ToolTip="Secret Password"></asp:Label></dt>
					<dd><asp:TextBox runat="server" ID="txtSecret_Password"  autocomplete="off" />
                     <asp:Label ID="lblSecPassword" runat="server" Text="" Visible="false" />
                    </dd>
				</dl>
			</div>

			<div id="block4" class="parts">
				<dl>
					<dt><asp:Label ID="Label8" runat="server" Text="対象ショップ※複数選択可" ToolTip="Target Shop" ></asp:Label></dt>
					<dd >
                    <asp:ListBox ID="lstTargetShop" runat="server" SelectionMode="Multiple" ></asp:ListBox>
                        <asp:Label ID="lblTargetshop" runat="server" Text="" Visible ="false"></asp:Label>
					</dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label9" runat="server" Text="対象商品" ToolTip="Target Product"></asp:Label></dt>
					<dd><asp:RadioButton ID="All_Items" runat="server" Text="全商品" GroupName="item" />&nbsp;<asp:RadioButton ID="Designated_goods" runat="server" Text="商品別" Checked="true" GroupName="item"/>
						<div><asp:TextBox ID="txtPromotionItem" runat="server" placeholder="複数：カンマ区切り" TextMode="MultiLine" />
                            <asp:Label ID="lblproitem" runat="server" Text="" Visible="false"></asp:Label></div>
					</dd>
				</dl>
			</div>

			<div id="block5">
				<dl>
					<dt><asp:Label ID="Label10" runat="server" Text="商品名装飾" ToolTip="Product Name Decoration" ></asp:Label></dt>
					<dd><asp:TextBox ID="txtproductDecoration" runat="server" ></asp:TextBox>
                    <asp:Label ID="lblprodecoration" runat="server" Text="" Visible="false"></asp:Label>
                    </dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label11" runat="server" Text="キャッチコピー装飾" ToolTip="Catch copy decoration"></asp:Label></dt>
					<dd><asp:TextBox ID="txtcpyDecoration" runat="server" ></asp:TextBox>
                    <asp:Label ID="lblcpydec" runat="server" Text="" Visible="false"></asp:Label>
                    </dd>
				</dl>
			</div>

			<div id="block6">
				<dl>
					<dt><asp:Label ID="Label12" runat="server" Text="オプション" ToolTip="Option"></asp:Label> 
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    
                    </dt>
					<dd>
						項目名&nbsp;&nbsp;<asp:TextBox  ID="txtOption_Name1" runat="server" /><asp:Label ID="lblOp1" runat="server"  Visible="false"></asp:Label>&nbsp;&nbsp;選択肢&nbsp;&nbsp;<asp:TextBox ID="txtOption_Value1" runat="server" /><asp:Label ID="lblOpVal1" runat="server" Text="" Visible="false"></asp:Label>
					</dd>
					<dd>
						項目名&nbsp;&nbsp;<asp:TextBox  ID="txtOption_Name2" runat="server" /><asp:Label ID="lblOp2" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;選択肢&nbsp;&nbsp;<asp:TextBox ID="txtOption_Value2" runat="server" /><asp:Label ID="lblOpVal2" runat="server" Text="" Visible="false"></asp:Label>
					</dd>
					<dd>
						項目名&nbsp;&nbsp;<asp:TextBox  ID="txtOption_Name3" runat="server" /><asp:Label ID="lblOp3" runat="server"  Visible="false"></asp:Label>&nbsp;&nbsp;選択肢&nbsp;&nbsp;<asp:TextBox ID="txtOption_Value3" runat="server" /><asp:Label ID="lblOpVal3" runat="server" Text="" Visible="false"></asp:Label>
					</dd>
				</dl>
			</div>

			<div id="block7" class="parts">
				<dl>
					<dt><asp:Label ID="Label13" runat="server" Text="商品説明文上" ToolTip="Product Description X"></asp:Label></dt>
					<dd><asp:TextBox ID="txtProduct_DescriptionX" runat="server"  TextMode="MultiLine"  /><asp:Label ID="lblpro_desX" runat="server" Text="" Visible="false"></asp:Label></dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label14" runat="server" Text="商品説明文下" ToolTip="Product Description Y" ></asp:Label></dt>
					<dd><asp:TextBox ID="txtProduct_DescriptionY" runat="server" TextMode="MultiLine"/><asp:Label ID="lblpro_descY" runat="server" Text="" Visible="false"></asp:Label></dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label15" runat="server" Text="販売説明文上" ToolTip="Sale Description X" ></asp:Label></dt>
					<dd><asp:TextBox ID="txtSale_DescriptionX" runat="server"  TextMode="MultiLine"/><asp:Label ID="lblsale_descX" runat="server" Text="" Visible="false"></asp:Label></dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label16" runat="server" Text="販売説明文下" ToolTip="Sale Description Y"></asp:Label></dt>
					<dd><asp:TextBox ID="txtSale_DescriptionY" runat="server"  TextMode="MultiLine"/><asp:Label ID="lblsale_descY" runat="server" Text="" Visible="false"></asp:Label></dd>
				</dl>
			</div>

			<div id="block8" class="parts">
				<dl>
					<dt>楽天GOLD<br />FTPアップロード</dt>
					<dd><asp:FileUpload runat="server" ID="fuRakuten_Gold1" /><br /><asp:Label runat="server" ID="lblRakuten_Gold1"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuRakuten_Gold2" /><br /><asp:Label runat="server" ID="lblRakuten_Gold2"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuRakuten_Gold3" /><br /><asp:Label runat="server" ID="lblRakuten_Gold3"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuRakuten_Gold4" /><br /><asp:Label runat="server" ID="lblRakuten_Gold4"></asp:Label></dd>
				</dl>
				<dl>
					<dt>楽天Cabinet<br />FTPアップロード</dt>
					<dd><asp:FileUpload runat="server" ID="fuRakuten_Cabinet1" /><br /><asp:Label runat="server" ID="lblRakuten_Cabinet1"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuRakuten_Cabinet2" /><br /><asp:Label runat="server" ID="lblRakuten_Cabinet2"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuRakuten_Cabinet3" /><br /><asp:Label runat="server" ID="lblRakuten_Cabinet3"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuRakuten_Cabinet4" /><br /><asp:Label runat="server" ID="lblRakuten_Cabinet4"></asp:Label></dd>
				</dl>
				<dl>
					<dt>geocities<br />FTPアップロード</dt>
					<dd><asp:FileUpload runat="server" ID="fuYahoo1" /><br /><asp:Label runat="server" ID="lblYahoo1"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuYahoo2" /><br /><asp:Label runat="server" ID="lblYahoo2"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuYahoo3" /><br /><asp:Label runat="server" ID="lblYahoo3"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuYahoo4" /><br /><asp:Label runat="server" ID="lblYahoo4"></asp:Label></dd>
				</dl>
				<dl>
					<dt>ポンパレ<br />FTPアップロード</dt>
					<dd><asp:FileUpload runat="server" ID="fuPonpare1" /><br /><asp:Label runat="server" ID="lblPonpare1"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuPonpare2" /><br /><asp:Label runat="server" ID="lblPonpare2"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuPonpare3" /><br /><asp:Label runat="server" ID="lblPonpare3"></asp:Label></dd>
					<dd><asp:FileUpload runat="server" ID="fuPonpare4" /><br /><asp:Label runat="server" ID="lblPonpare4"></asp:Label></dd>
				</dl>
			</div>

			<div id="block9" class="parts">
				<dl>
					<dt><asp:Label ID="Label17" runat="server" Text="優先順位" ToolTip="Priority"></asp:Label></dt>
					<dd><asp:TextBox ID="txtPriority" runat="server"  /><asp:Label ID="lblpriority" runat="server" Text="" Visible="false"></asp:Label></dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label18" runat="server" Text="ステータス" ToolTip="Status" ></asp:Label></dt>
					<dd><asp:RadioButtonList runat="server" ID="rdolStatus" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">開催前</asp:ListItem>
                            <asp:ListItem Value="1">開催中</asp:ListItem>
                            <asp:ListItem Value="2">終了</asp:ListItem>
                            <asp:ListItem Value="3">中止</asp:ListItem>
                            </asp:RadioButtonList>
                            <asp:Label ID="lblstatus" runat="server" Text="" Visible ="false"></asp:Label>
                   </dd>
				</dl>
				<dl>
					<dt><asp:Label ID="Label19" runat="server" Text="プロモーション強制終了" ToolTip="IsPromotionClose"></asp:Label></dt>
					<dd><asp:CheckBox runat="server" ID="chkIsPromotionClose" Text="開催中のプロモーションを強制的に終了する"/>
                    <asp:Label ID="lblproclose" runat="server" Text="" Visible ="false"></asp:Label>
                    </dd>
				</dl>
			</div>
		</div>

		<div class="btn">
        <input type="submit" id="btnpopup" onclick="" runat="server"   value="確 認"/>
        <asp:Button runat="server" ID="btnSave" Text="確 認" onclick="btnSave_Click"  Visible="false"/>
        <asp:Button runat="server" ID="Button1" OnClick="btnSave_Click" Text="確 認" Visible="false" />
        </div>
		</form>

	</div><!--setListBox-->


</div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
