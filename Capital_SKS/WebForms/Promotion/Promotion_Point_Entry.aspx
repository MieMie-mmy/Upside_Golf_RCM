<%@ Page Title="商品管理システム＜ポイント設定＆検索＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Promotion_Point_Entry.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Promotion_Point_Entry" %>
<%@ Register src="../../UCGrid_Paging.ascx" tagname="UCGrid_Paging" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/jscript"></script>
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css" />

<%--
	<link href="../../Styles/promotion_base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion_common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion_manager_style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion_promotion.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion_Item_style.css" rel="stylesheet" type="text/css" />--%>
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
<script language="javascript" type="text/javascript">
    function doClick(buttonName, e) {
        //the purpose of this function is to allow the enter key to 
        //point to the correct button to click.
        var key;

        if (window.event)
            key = window.event.keyCode;     //IE
        else
            key = e.which;     //firefox

        if (key == 13) {
            //Get the button the user wants to have clicked
            var btn = document.getElementById(buttonName);
            if (btn != null) { //If we find the button click it
                btn.click();
                event.keyCode = 0
            }
        }
    }
</script>

<script type="text/javascript">
    window.document.onkeydown = function (e) {
        if (!e) e = event;
        if (e.keyCode == 27) {
            document.getElementById("<%=txtitemcode.ClientID%>").value = null;
            document.getElementById("<%=txtitemname.ClientID%>").value = null;
            document.getElementById("<%=txtbrandname.ClientID%>").value = null;
            document.getElementById("<%=txtRakupointPeriod.ClientID%>").value = null;
            document.getElementById("<%=txtRakupointPeriod2.ClientID%>").value = null;
            document.getElementById("<%=txtcopmpetition.ClientID%>").value = null;
          
            document.getElementById("<%=txtyahooperiod.ClientID%>").value = null;
            document.getElementById("<%=txtyahooperiod2.ClientID%>").value = null;
            document.getElementById("<%=txtclassification.ClientID%>").value = null;
            document.getElementById("<%=txtyear.ClientID%>").value = null;
            document.getElementById("<%=txtponpareperiod.ClientID%>").value = null;
            document.getElementById("<%=txtponpareperiod2.ClientID%>").value = null;
            document.getElementById("<%=txtseason.ClientID%>").value = null;
            document.getElementById("<%=txtinstructionno.ClientID%>").value = null;
            var drp1 = document.getElementById("<%=ddlshopstatus.ClientID%>");
            var drp2 = document.getElementById("<%=listshop.ClientID%>");
            var drp3 = document.getElementById("<%=ddlRakumgpoint.ClientID%>");
            var drp4 = document.getElementById("<%=ddlyahoomgpoint.ClientID%>");
            var drp5 = document.getElementById("<%=ddlponparemgpoint.ClientID%>");
            var drp6 = document.getElementById("<%=chkcheck.ClientID%>");
            drp1.selectedIndex = 0;
            drp2.selectedIndex = -1;
            drp3.selectedIndex = 0;
            drp4.selectedIndex = 0;
            drp5.selectedIndex = 0;
            drp6.checked = false;
        }
    }
</script>
<script type="text/javascript" language="javascript">
function pageLoad(sender, args) {
$(document).ready(function () {
$("#<%=txtRakupointPeriod.ClientID %>").datepicker(
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
$("#<%=txtyahooperiod.ClientID %>").datepicker(
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

$(document).ready(function () {
$("#<%=txtponpareperiod.ClientID %>").datepicker(
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

$(document).ready(function () {
$("#<%=txtRperiodfrom.ClientID %>").datepicker(
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


$(document).ready(function () {
$("#<%=txtYperiodfrom.ClientID %>").datepicker(
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

$(document).ready(function () {
$("#<%=txtPperiodfrom.ClientID %>").datepicker(
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

$(document).ready(function () {
$("#<%=txtRperiodto.ClientID %>").datepicker(
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


$(document).ready(function () {
$("#<%=txtYperiodto.ClientID %>").datepicker(
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

$(document).ready(function () {
$("#<%=txtPperiodto.ClientID %>").datepicker(
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

$(document).ready(function () {
$("#<%=txtRakupointPeriod2.ClientID %>").datepicker(
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


$(document).ready(function () {
$("#<%=txtyahooperiod2.ClientID %>").datepicker(
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

$(document).ready(function () {
$("#<%=txtponpareperiod2.ClientID %>").datepicker(
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
}
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CmnWrapper"> 
<!-- /HEADER -->

<p id="toTop"><a href="#divtop">▲TOP</a></p>

<script type="text/javascript">
	$(function () {
		$(".prmPoint > h2").on("click", function () {
			$("div.ptSarOpen").slideToggle();
			$(this).toggleClass("active");
		});
		$(".prmPointSet > h2").on("click", function () {
			$("div.ptSetOpen").slideToggle();
			$(this).toggleClass("active");
		});
	});
</script>

<div id="CmnContents">
	<div id="ComBlock" style="margin-top: 60px;">
	<div class="iconSet iconCheck" id ="confirm" runat="server" visible="false">
			<h1>商品別ポイント設定</h1>
		</div>
		<div class="setListBox inlineSet iconSet iconList" id="title1" runat="server">
		<h1>商品別ポイント設定</h1>

		<div class="prmCmnSet prmPoint resetValue searchBox" id="title2" runat="server">
			<h2>ポイント検索</h2>
			<div class="ptSarOpen">
				<div class="block1" id ="b1" runat="server">


<%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
	<ContentTemplate>--%>
<dl>
						<dt>商品番号<br/>完全<asp:CheckBox ID="chkcheck" runat="server" /></dt>
<dd><asp:TextBox ID="txtitemcode" runat="server" TextMode="MultiLine"></asp:TextBox></dd>
	

<dt>商品名</dt>
  <dd>  <asp:TextBox ID="txtitemname" runat="server" Width="140px"></asp:TextBox></dd>

  <dt>ショップステータス</dt>
  <dd> <asp:DropDownList ID="ddlshopstatus" runat="server">
  <asp:ListItem></asp:ListItem>
  <asp:ListItem Value="n">未掲載</asp:ListItem>
<asp:ListItem Value="u">掲載中</asp:ListItem>
<asp:ListItem Value="d">削除</asp:ListItem>
	</asp:DropDownList></dd> 
	  <dt>対象ブランド</dt>
   <dd> <asp:TextBox ID="txtbrandname" runat="server"></asp:TextBox></dd>
	</dl>
				</div>
				
				<div class="block2" id ="b2" runat="server">
					<dl>
						<dt>対象ショップ</dt>
						<dd> <asp:ListBox ID="listshop" runat="server" SelectionMode="Multiple" ></asp:ListBox></dd>
					</dl>
				</div>

<div class="block3" id ="b3" runat="server">
					<dl>
<dt>楽天ポイント倍率</dt>
						<dd>
	<asp:DropDownList ID="ddlRakumgpoint" runat="server">
	 <asp:ListItem></asp:ListItem>
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
	</asp:DropDownList> </dd>

</dl>
					<dl>
						<dt>ヤフーポイント倍率</dt>
						<dd>
	<asp:DropDownList ID="ddlyahoomgpoint" runat="server">
	 <asp:ListItem></asp:ListItem>
	<asp:ListItem>1</asp:ListItem>
	<asp:ListItem>2</asp:ListItem>
	<asp:ListItem>3</asp:ListItem>
	<asp:ListItem>4</asp:ListItem>
	<asp:ListItem>5</asp:ListItem>
	<asp:ListItem>6</asp:ListItem>
	<asp:ListItem>7</asp:ListItem>
	<asp:ListItem>8</asp:ListItem>
	<asp:ListItem>9</asp:ListItem>
	<asp:ListItem>010</asp:ListItem>
	</asp:DropDownList></dd>
					</dl>
			  
<dl>
						<dt>ポンパレポイント倍率</dt>
						<dd>
	<asp:DropDownList ID="ddlponparemgpoint" runat="server">
	 <asp:ListItem></asp:ListItem>
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
	</asp:DropDownList></dd>
	</dl>
				</div>

<div class="block4" id="b4" runat="server">
					<dl>
						<dt>楽天開催期間</dt>
	 
  <dd>  <asp:TextBox ID="txtRakupointPeriod" runat="server" ReadOnly="true"/>
  <asp:ImageButton ID="ImageButton1" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton1_Click" ImageAlign="AbsBottom" /> 
  ～
	<asp:TextBox ID="txtRakupointPeriod2" runat="server" ReadOnly="true"/>
			<asp:ImageButton ID="ImageButton2" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton2_Click" ImageAlign="AbsBottom" />
<%--	 <asp:CompareValidator ID="CompareValidator42" runat="server" ErrorMessage="* Invalid Date" 
				ControlToCompare="txtRakupointPeriod" ControlToValidate="txtRakupointPeriod2" ForeColor="Red" Operator="GreaterThan" />--%>
	</dd>


<dl>
						<dt>ヤフー開催期間</dt>
  <dd>  <asp:TextBox ID="txtyahooperiod" runat="server" ReadOnly="true"/>
	<asp:ImageButton ID="ImageButton3" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton3_Click" ImageAlign="AbsBottom" /> 
  ～
	<asp:TextBox ID="txtyahooperiod2" runat="server" ReadOnly="true"/>
	  <asp:ImageButton ID="ImageButton4" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton4_Click" ImageAlign="AbsBottom" /> 
<%--	<asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="* Invalid Date" 
				ControlToCompare="txtyahooperiod" ControlToValidate="txtyahooperiod2" ForeColor="Red" Operator="GreaterThan" />--%>
	</dd>

</dl>				
					<dl>
			<dt>ポンパレ開催期間</dt>
   <dd><asp:TextBox ID="txtponpareperiod" runat="server" ReadOnly="true"/>
   <asp:ImageButton ID="ImageButton5" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton5_Click" ImageAlign="AbsBottom" /> 
   ～
	<asp:TextBox ID="txtponpareperiod2" runat="server" ReadOnly="true"/>
	<asp:ImageButton ID="ImageButton6" runat="server" Width="15px" Height="15px"
				ImageUrl="~/Styles/clear.png" onclick="ImageButton6_Click" ImageAlign="AbsBottom" /> 
<%--	  <asp:CompareValidator ID="CompareValidator3" runat="server" ErrorMessage="* Invalid Date" 
				ControlToCompare="txtponpareperiod" ControlToValidate="txtponpareperiod2" ForeColor="Red" Operator="GreaterThan" /> --%>
	</dd>

</dl>				
				</div>

<div class="block5" id="b5" runat="server">
					<dl>
	<dt>競技</dt>
	<dd>
	<asp:TextBox ID="txtcopmpetition" runat="server"></asp:TextBox></dd>
 <dt>分類</dt>
   <dd> <asp:TextBox ID="txtclassification" runat="server"></asp:TextBox></dd>
<dt>年度</dt>
  <dd>  <asp:TextBox ID="txtyear" runat="server"></asp:TextBox></dd>
<dt>シーズン</dt>
 <dd>   <asp:TextBox ID="txtseason" runat="server"></asp:TextBox></dd>

 <dt>指示書番号</dt>
  <dd>  <asp:TextBox ID="txtinstructionno" runat="server"></asp:TextBox></dd>
</dl>
				</div>
<p>
 <asp:Button ID="btnsearch" runat="server" Text="検 索" 
		   onclick="btnsearch_Click" /></p>
</div>

<div class="prmPointSet">
				<h2 class="active">ポイント設定</h2>
				<div class="ptSetOpen">
					<div class="block3" id="b6" runat="server">
						<dl>
<dt>楽天ポイント倍率</dt>
							<dd>
	<asp:DropDownList ID="ddlRpoint" runat="server">
	<asp:ListItem></asp:ListItem>
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
	</asp:DropDownList>
	</dd>
	</dl>
<dl>
							<dt>ヤフーポイント倍率</dt>
							<dd>
	<asp:DropDownList ID="ddlYpoint" runat="server">
	 <asp:ListItem></asp:ListItem>
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
	</asp:DropDownList>
	</dd>
						</dl>

<dl>
							<dt>ポンパレポイント倍率</dt>
							<dd>
<asp:DropDownList ID="ddlPpoint" runat="server">
 <asp:ListItem></asp:ListItem>
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
	</asp:DropDownList>
	</dd>
						</dl>
					</div>


<div class="block4" id="b7" runat="server">
						<dl>

	<dt>楽天開催期間</dt>
							<dd>
	<asp:TextBox ID="txtRperiodfrom" runat="server" ReadOnly="true"></asp:TextBox>
	<asp:DropDownList runat="server" ID="ddlRPeriodFromHour">
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
				   
					</asp:DropDownList>:
	<asp:DropDownList runat="server" ID="ddlRPeriodFromMinute">
					<asp:ListItem>00</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>30</asp:ListItem>
					<asp:ListItem>45</asp:ListItem>
					<asp:ListItem>59</asp:ListItem>
					</asp:DropDownList>
                    <asp:ImageButton ID="ImageButton9" runat="server" Width="15px" Height="15px"
		            ImageUrl="~/Styles/clear.png" onclick="ImageButton9_Click" ImageAlign="AbsBottom" /> 
					  ～
	<asp:TextBox ID="txtRperiodto" runat="server" ReadOnly="true"></asp:TextBox>
	<asp:DropDownList runat="server" ID="ddlRperiodtohour">
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
				   
					</asp:DropDownList>:
	<asp:DropDownList runat="server" ID="ddlRPeriodToMinute">
					<asp:ListItem>00</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>30</asp:ListItem>
					<asp:ListItem>45</asp:ListItem>
					<asp:ListItem>59</asp:ListItem>
					</asp:DropDownList>
                    <asp:ImageButton ID="ImageButton10" runat="server" Width="15px" Height="15px"
		            ImageUrl="~/Styles/clear.png" onclick="ImageButton10_Click" ImageAlign="AbsBottom" /> 
		</dd>
		</dl>
			
   <dl>
							<dt>ヤフー開催期間</dt>
							<dd>
	<asp:TextBox ID="txtYperiodfrom" runat="server" ReadOnly="true"></asp:TextBox>
	<asp:DropDownList runat="server" ID="ddlYperiodfromhour">
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
				   
					</asp:DropDownList>:
	<asp:DropDownList runat="server" ID="ddlYperiodFromMinute">
					<asp:ListItem>00</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>30</asp:ListItem>
					<asp:ListItem>45</asp:ListItem>
					<asp:ListItem>59</asp:ListItem>
					</asp:DropDownList>
                    <asp:ImageButton ID="ImageButton11" runat="server" Width="15px" Height="15px"
		            ImageUrl="~/Styles/clear.png" onclick="ImageButton11_Click" ImageAlign="AbsBottom" />
	 ～
	<asp:TextBox ID="txtYperiodto" runat="server" ReadOnly="true"></asp:TextBox>
	 <asp:DropDownList runat="server" ID="ddlYperiodtohour">
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
				   
					</asp:DropDownList>:
	<asp:DropDownList runat="server" ID="ddlYperiodToMinute">
					<asp:ListItem>00</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>30</asp:ListItem>
					<asp:ListItem>45</asp:ListItem>
					<asp:ListItem>59</asp:ListItem>
					</asp:DropDownList>
                    <asp:ImageButton ID="ImageButton12" runat="server" Width="15px" Height="15px"
		            ImageUrl="~/Styles/clear.png" onclick="ImageButton12_Click" ImageAlign="AbsBottom" />
			
			</dd>
						</dl>		
	  <dl>
							<dt>ポンパレ開催期間</dt>
							<dd>
		<asp:TextBox ID="txtPperiodfrom" runat="server" ReadOnly="true"></asp:TextBox>
		<asp:DropDownList runat="server" ID="ddlPperiodfromhour">
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
				   
					</asp:DropDownList>:
		<asp:DropDownList runat="server" ID="ddlPperiodfromMinute">
					<asp:ListItem>00</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>30</asp:ListItem>
					<asp:ListItem>45</asp:ListItem>
					<asp:ListItem>59</asp:ListItem>
					</asp:DropDownList>
                    <asp:ImageButton ID="ImageButton13" runat="server" Width="15px" Height="15px"
		                ImageUrl="~/Styles/clear.png" onclick="ImageButton13_Click" ImageAlign="AbsBottom" />
					 ～
		<asp:TextBox ID="txtPperiodto" runat="server" ReadOnly="true"></asp:TextBox>
		<asp:DropDownList runat="server" ID="ddlPPeriodtohour">
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
				   
					</asp:DropDownList>:
		<asp:DropDownList runat="server" ID="ddlPperiodToMinute">
					<asp:ListItem>00</asp:ListItem>
					<asp:ListItem>15</asp:ListItem>
					<asp:ListItem>30</asp:ListItem>
					<asp:ListItem>45</asp:ListItem>
					<asp:ListItem>59</asp:ListItem>
					</asp:DropDownList>
                    <asp:ImageButton ID="ImageButton14" runat="server" Width="15px" Height="15px"
		                ImageUrl="~/Styles/clear.png" onclick="ImageButton14_Click" ImageAlign="AbsBottom" />

</dd>
						</dl>				
					</div>
<p><asp:Button ID="btnsetting" runat="server" Text="一括設定" 
		onclick="btnsetting_Click" /></p>
	
	</div>			
			</div><!-- /.prmPointSet -->
			
		</div><!-- /.searchBox -->

	</div><!--setListBox-->
</div><!--ComBlock-->
</div><!--CmnContents-->



<script type="text/javascript">
	$(window).on('load resize', function () {
		var w = $(window).width() - 2;
		$('.listTable').css('width', w + 'px');
	});
</script>




<%--
</ContentTemplate>
</asp:UpdatePanel>--%>

<div id="CmnContents2">

<div id="ComBlock2">

	<div class="widthhMax iconEx operationBtn">
     <asp:HiddenField ID="hfcheckvalue" runat="server" />
        <asp:HiddenField ID="hfpageno" runat="server" />
		<asp:HiddenField ID="hfsetting" runat="server" />

		<div class="operationBtn">
		<p>
	<asp:Button ID="btnselectall" runat="server" Text="全て選択" 
		onclick="btnselectall_Click" />&nbsp;
	<asp:Button ID="btncancelall" runat="server" Text="全て解除" 
		onclick="btncancelall_Click" /></p>
			<p class="itemPage">
				<asp:DropDownList ID="ddlpage" runat="server" 
					onselectedindexchanged="ddlpage_SelectedIndexChanged" AutoPostBack="true">
				<asp:ListItem>30</asp:ListItem>
				<asp:ListItem>50</asp:ListItem>
				<asp:ListItem>100</asp:ListItem>
				</asp:DropDownList>
			</p>
			</div><!-- /.operationBtn -->
	</div><!-- /.widthhMax -->

</div><!-- /#ComBlock2 -->
	<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>--%>
	
<div class="prmCmnSet resetValue listBox">
	<asp:GridView ID="gvview" runat="server" AutoGenerateColumns="False" 
		CellPadding="4" ForeColor="#333333" GridLines="None" CssClass="prmCamList prmPointList managementList listTable"
		EmptyDataText="There is no data to display!" ShowHeaderWhenEmpty="True"  
			AllowPaging="True"  PageSize="30" onrowdatabound="gvview_RowDataBound">
	  
		<Columns>
		  <asp:TemplateField >
			
			 
			<ItemTemplate>
					<asp:CheckBox ID="chktype" runat="server" >
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
			  <asp:TemplateField HeaderText="ポイント倍率">
			<ItemTemplate>
					<asp:Label ID="lblPoint" runat="server" Text='<%#Eval("Point_magnification") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="ポイント期間">
			<ItemTemplate>
					<asp:Label ID="lblPointPeriod" runat="server" Text='<%#Eval("Point_period") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="指示書番号">
			<ItemTemplate>
					<asp:Label ID="lblInstructionNo" runat="server" Text='<%#Eval("Instruction_No") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="ブランド">
			<ItemTemplate>
					<asp:Label ID="lblBrandName" runat="server" Text='<%#Eval("Brand_Name") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="年度">
			<ItemTemplate>
					<asp:Label ID="lblYear" runat="server" Text='<%#Eval("Year") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="シーズン">
			<ItemTemplate>
					<asp:Label ID="lblSeason" runat="server" Text='<%#Eval("Season") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="ID" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblID" runat="server" Text='<%#Eval("ID") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="Mall_ID" Visible="false">
			<ItemTemplate>
					<asp:Label ID="lblMallID" runat="server" Text='<%#Eval("Mall_ID") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="ID" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblRStartTime" runat="server" Text='<%#Eval("Rakuten_StartTime") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="rendtime" Visible="false">
			<ItemTemplate>
					<asp:Label ID="lblREndTime" runat="server" Text='<%#Eval("Rakuten_EndTime") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="ystarttime" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblYStartTime" runat="server" Text='<%#Eval("Yahoo_StartTime") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="yendtime" Visible="false">
			<ItemTemplate>
					<asp:Label ID="lblYEndTime" runat="server" Text='<%#Eval("Yahoo_EndTime") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="pstarttime" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblPStartTime" runat="server" Text='<%#Eval("Ponpare_StartTime") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="pendtime" Visible="false">
			<ItemTemplate>
					<asp:Label ID="lblPEndTime" runat="server" Text='<%#Eval("Ponpare_EndTime") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="pstarttime" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblRStartDate" runat="server" Text='<%#Eval("Rakuten_StartDate") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="pendtime" Visible="false">
			<ItemTemplate>
					<asp:Label ID="lblREndDate" runat="server" Text='<%#Eval("Rakuten_EndDate") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="pstarttime" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblYStartDate" runat="server" Text='<%#Eval("Yahoo_StartDate") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="pendtime" Visible="false">
			<ItemTemplate>
					<asp:Label ID="lblYEndDate" runat="server" Text='<%#Eval("Yahoo_EndDate") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>

			<asp:TemplateField HeaderText="pstarttime" Visible = "false">
			<ItemTemplate>
					<asp:Label ID="lblPStartDate" runat="server" Text='<%#Eval("Ponpare_StartDate") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
			  <asp:TemplateField HeaderText="pendtime" Visible="false">
			<ItemTemplate>
					<asp:Label ID="lblPEndDate" runat="server" Text='<%#Eval("Ponpare_EndDate") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
            <asp:TemplateField HeaderText="shopID" Visible="false">
			<ItemTemplate>
					<asp:Label ID="lblShop_ID" runat="server" Text='<%#Eval("ShopID") %>'></asp:Label>
				</ItemTemplate>
			</asp:TemplateField>
		</Columns>

		

		<PagerSettings Visible="False" />

		

	</asp:GridView>
  
	</div><!-- /.prmCmnSet -->
	  <div align="center">
	   <asp:Button runat="server" ID="btnSave" Visible="false" OnClick="btnSave_Click" Text="確認画面へ"  />
	   </div>
	<%--</ContentTemplate>
	</asp:UpdatePanel>--%>
	</div><!-- /#CmnContents2 -->


<div class="btn">
<script type="text/javascript">
	function CallTab1() {
		document.forms[0].target = "";
	}
</script>


	<div class="btn">
	<uc1:UCGrid_Paging ID="gp" runat="server" visible="true"/>
</div>
</div><!-- #CmnWrapper -->
	</div>
	</div>
	</div>
	</div>
	</div>
	</div>
</asp:Content>


