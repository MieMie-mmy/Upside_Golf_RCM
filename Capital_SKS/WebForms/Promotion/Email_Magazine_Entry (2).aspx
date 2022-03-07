<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Email_Magazine_Entry.aspx.cs" Inherits="ORS_RCM.WebForms.Promotion.Email_Magazine_Entry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/promotion.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
<script type="text/javascript" language="javascript">
$(document).ready(function () {
$("#<%=txtDeliveryDate.ClientID%>").datepicker(
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
</script>

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
<style type="text/css">

    .style1
    {
        width: 171px;
    }

    .style2
    {
        width: 372px;
    }
    .style3
    {
        width: 247px;
    }

    .style4
    {
        width: 411px;
    }

</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#divtop">▲TOP</a></p>
<div id="CmnWrapper">
<div id="CmnContents">
<div id="ComBlock">
<div class="setListBox inlineSet iconSet iconList">
	<h1>メールマガジン登録画面</h1>
    <div class="prmCmnSet resetValue searchBox">
			<h2>メールマガジン登録画面</h2>

  <table>
  <tr>
  <td class="style3">
  メールマガジンID
    </td>
    <td>
  メルマガ名
   </td>
   <td>
    配信予定日
    </td>
  </tr>
  
  <tr>
  <td class="style3">
  <asp:TextBox runat="server" ID="txtEmailMagazineID" Width="120px"/>
  </td>
  <td>
    <asp:TextBox runat="server" ID="txtDeliveryDate" ReadOnly="true" Width="120px"/>
    <asp:DropDownList ID="ddlHour" runat="server" Height="21px" Width="49px">
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
    <asp:DropDownList ID="ddlMinute" runat="server" Height="23px" Width="50px">
                    <asp:ListItem>00</asp:ListItem>
	                <asp:ListItem>15</asp:ListItem>
	                <asp:ListItem>30</asp:ListItem>
	                <asp:ListItem>45</asp:ListItem>
                    <asp:ListItem>59</asp:ListItem>
    </asp:DropDownList>
  </td>
  <td>
      <asp:ListBox ID="listTarget_Shop" runat="server" SelectionMode="Multiple" 
          Width="200px" Height="38px"></asp:ListBox>
  </td>
  </tr>
 </table>
 <table>
  <tr>
  <td class="style2">
  キャンペーンID
</td>  <td>
   <asp:Button ID="btnCampaignName" runat="server" Height="20px" Text="キャンペーン名の表示" 
              Width="177px" onclick="btnCampaignName_Click" />
  </td> 
  </tr>
  <tr>
  <td class="style2">
    <asp:TextBox runat="server" ID="TextBox1" Width="120px"/>
     
     </td>
     <td>
     <asp:TextBox runat="server" ID="TextBox2" Width="120px"/>
  </td>
  </tr>
  <tr>
  <td class="style2">
    <asp:TextBox runat="server" ID="TextBox3" Width="120px"/>
    </td>
    <td>
    <asp:TextBox runat="server" ID="TextBox4" Width="120px"/>
  </td>
  </tr>
  <tr>
  <td class="style2">
     <asp:TextBox runat="server" ID="TextBox5" Width="120px"/>
   </td>
   <td>
     <asp:TextBox runat="server" ID="TextBox6" Width="120px"/>
  </td>
  </tr>
  <tr>
  <td class="style2">
  <asp:TextBox runat="server" ID="TextBox7" Width="120px"/>
  </td>
  <td>
  
   <asp:TextBox runat="server" ID="TextBox8" Width="120px"/>
  
  </td>
  </tr>
   <tr>
  <td class="style2">
  <asp:TextBox runat="server" ID="TextBox9" Width="120px"/>
</td>
<td>
    <asp:TextBox runat="server" ID="TextBox10" Width="120px"/>
  </td>
  </tr> 
  </table>
  <table>
  <tr>
  <td class="style1">
  タイトル
  </td>
  <td>
   モール
  </td>
  <td>
  検索条件1
  </td>
  <td>
  検索条件2
  </td>
   <td>
  検索条件3
  </td>
  </tr>

  <tr>
  <td class="style1">
  <asp:TextBox runat="server" ID="txtTitle" Width="145px"/>
  
  </td>
    <td>
  <asp:DropDownList ID="ddlMall" runat="server" Height="19px" Width="85px"></asp:DropDownList>
  </td>
    <td>
   <asp:DropDownList ID="ddlSearch1" runat="server" Height="18px" Width="92px"></asp:DropDownList>
  
  </td>
    <td>
    <asp:DropDownList ID="ddlSearch2" runat="server" Height="16px" Width="95px"></asp:DropDownList>
  
  </td>
  <td>
      <asp:DropDownList ID="ddlSearch3" runat="server" Height="16px" Width="84px"></asp:DropDownList>
                        </td>
  </tr>

  <tr>
  <td class="style1">
  商品番号
  </td>
  <td>
  
  </td>
  <td>
  商品ページ・検索URL
  </td>
  <td>
  カテゴリページURL
  </td>
  
  
  </tr>

  <tr>
  <td class="style1">
     <asp:TextBox runat="server" ID="txtItemNumber" Width="145px"/>
  </td>
  
  <td>
  <asp:Button ID="btnCreateURL" runat="server" Text="URLの作成" Height="19px" 
          Width="168px" onclick="btnCreateURL_Click" />
  </td>
  <td>
  <asp:TextBox runat="server" ID="txtProductPage" Width="183px"/>
  </td>
  <td>
       <asp:TextBox runat="server" ID="txtCategoryPage" Width="163px"/>
  </td>
  </tr>
  <tr>
  <td class="style1">
  タイトル
  </td>
  <td>
  
  モール
  </td>
  <td>
  検索条件1
  
  </td>
  <td>
  検索条件2
  
  </td>
  <td>
  検索条件3
  
  </td>
  </tr>
  <tr>
  <td class="style1">
   <asp:TextBox runat="server" ID="txtTitle1" Width="145px"/>
  </td>
    <td>
  
  <asp:DropDownList ID="ddlMall1" runat="server" Height="19px" Width="85px"></asp:DropDownList>
  </td>
    <td>
   <asp:DropDownList ID="ddlSearch4" runat="server" Height="18px" Width="92px"></asp:DropDownList>
  </td>
    <td>
   <asp:DropDownList ID="ddlSearch5" runat="server" Height="16px" Width="99px"> </asp:DropDownList>
  </td>
    <td>
  <asp:DropDownList ID="ddlSearch6" runat="server" Height="16px" Width="84px"></asp:DropDownList>
  </td>
  </tr>

  <tr>
  <td class="style1">
  商品番号
  </td>
  <td>
  
  </td>
   <td>
  商品ページ・検索URL
  </td>
   <td>
  カテゴリページURL
  </td>
  </tr>
 <tr>
 <td class="style1">
 <asp:TextBox runat="server" ID="txtItemNumber1" Width="145px"/>
 </td>
 <td>
 <asp:Button ID="btnCreateURL1" runat="server" Text="URLの作成" Height="22px" 
         Width="161px" onclick="btnCreateURL1_Click" />
 </td>
  <td>
 <asp:TextBox runat="server" ID="txtProductPage1" Width="183px"/>
 </td>
  <td>
   <asp:TextBox runat="server" ID="txtCategoryPage1" Width="163px"/>
 </td>
 </tr>
 <tr>
 <td class="style1">
  メールマガジンタイトル
 </td>
 </tr>
 <tr>
 <td class="style1">
 <asp:TextBox runat="server" ID="txtEmailTitle" Width="207px"/>	
 </td>
 </tr>
 </table>
 <table>
 <tr>
 <td class="style4">
  メールマガジン内容(HTML)
 </td>
  <td>
  メールマガジン内容(Text)
 </td>
 </tr>
 <tr>
 <td class="style4">
        <asp:TextBox runat="server" ID="txtEmailContent" Width="207px" TextMode="MultiLine"/>
 </td>
  <td>
 <asp:TextBox runat="server" ID="txtEmailContent1" Width="207px" TextMode="MultiLine"/>
 </td>
 
 </tr>
  </table>
				
		<div align="center">  <asp:Button ID="btnPreview" runat="server" Text="プレビュー" 
                OnClick="btnPreview_Click" BackColor="#F2F2F2" Width="132px"/>&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnConfirm" runat="server" 
                Text="確認画面へ" OnClick="btnConfirm_Click" BackColor="#F2F2F2" Width="124px"/>
       </div>                
</div>
</div>
</div>
</div>
    </div>
</asp:Content>
