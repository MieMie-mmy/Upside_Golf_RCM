<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mall_Setting_Yahoo_Fixed.aspx.cs" Inherits="ORS_RCM.Mall_Setting_Yahoo_Fixed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
  

   <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
      <link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />

  
 
 <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
<link href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
--%>
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8)
            return true;
        else return false;
    } 
</script>

  <script type="text/javascript" language="javascript">
    function pageLoad(sender, args) {
        $(document).ready(function () {
            $("#<%=txtRedate.ClientID %>").datepicker(
	    {
	        showOn: 'button',
	        dateFormat: 'dd/M/yy',
	        buttonImageOnly: true,
	        buttonImage: '../../images/calendar.gif',
	        changeMonth: true,
	        changeYear: true,
	        yearRange: "2013:2020",
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
<asp:HiddenField ID="CustomHiddenField" runat="server" />
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
<div id="CmnContents">
	<div id="ComBlock">

<!-- Fixed Value -->
	     <div class="setDetailBox shopCmnSet defaultSet iconSet iconYahoo">
		<h1>ヤフー固定値編集<span>全て必須項目。ただし、ブランク可</span></h1>
 
 
  <table class="editTable">
 <tr>
 
 <th>ショップ名</th>
 

 <td>
 
   <asp:Label ID="lblShopName" runat="server" Font-Size="Medium"></asp:Label>
   <asp:TextBox ID="txtshopID"  runat="server" Visible="False"></asp:TextBox>

 
  </td>



  </tr>

  <tr>
 
 <th>出品モール</th>
 

 <td>
     <asp:Label ID="lblMallName" runat="server" Font-Size="Medium"></asp:Label>
   
 
  </td>



  </tr>

  
  <tr>
 <th>特価</th>
 
 


 <td >
 
     <asp:TextBox ID="txtprice"  runat="server"  CssClass="input_text" onkeypress="return isNumberKey(event)"></asp:TextBox>
 
     <asp:Label ID="lblSpecialPrice" runat="server"  Visible="false"></asp:Label>
 
 </td>

  </tr>

  <tr>
 <th>ひと言コメント</th>
 
 
 <td>
 
     <asp:TextBox ID="txtcomment"  runat="server"></asp:TextBox>
 
     <asp:Label ID="lblComment" runat="server"  Visible="false"></asp:Label>
 
 </td>

  </tr>

  <tr>
 
 <th>課税対象</th>
 

<td>
                 <asp:RadioButton ID="rdoTax"     runat="server" GroupName="rdoTaxstatus" 
                        Text="課税"  Checked="true" />
                    <asp:RadioButton ID="rdoExempt" runat="server" GroupName="rdoTaxstatus" 
                        Text="非課税" />
                  


                 <asp:Label ID="lblTaxable" runat="server"  Visible="false"></asp:Label>
                  


</td>


<td>
 


    &nbsp;</tr>

  <tr>

 
     <th>発売日</th>
 
 <asp:UpdatePanel runat="server" ID="uppnl1">
                                                                                                       <ContentTemplate>
 <td>
  
  <asp:TextBox ID="txtRedate"  runat="server" ReadOnly="true"></asp:TextBox>

  
  <asp:ImageButton ID="ImageButton1" runat="server" Width="19px" Height="16px" 
                                                                                                              ImageUrl="~/images/clear.png" onclick="ImageButton1_Click"  />






     <asp:Label ID="lblReleaseDate" runat="server"   Visible="false"></asp:Label>
 </td>
 </ContentTemplate>
                                                                                                            </asp:UpdatePanel>

 </tr>

 


  <tr>
 <th> 仮ポイント期間</th>

 <td>
 <asp:TextBox ID="txtProperiod"  runat="server"></asp:TextBox>
     <asp:Label ID="lblProvisionalPeriodPoint" runat="server"  Visible="false"></asp:Label>
</td>
  </tr>
           
  <tr>
 <th>使用中のテンプレート</th>

 <td>
 
 <asp:TextBox ID="txtTemplate"  runat="server"></asp:TextBox>
 
     <asp:Label ID="lblTemplateInUse" runat="server"  Visible="false"></asp:Label>
 
 </td>
  </tr>

  <tr>
 <th>購入数制限</th>
 <td>
 
      <asp:TextBox ID="txtPurchaseAmount"  runat="server"></asp:TextBox>
 
     <asp:Label ID="lblLimitingthenumberofpurchase" runat="server"  Visible="False"></asp:Label>
 
 </td>


  </tr>

   <tr>
 
 
<th>商品の状態</th>
 
 

 <td>
 
                    <asp:RadioButton ID="rdoNew" runat="server" GroupName="rdoProductState" 
                        Text="新品"  Checked="true"/>
                    <asp:RadioButton ID="rdoUsed" runat="server" GroupName="rdoProductState" 
                        Text="中古" />
 
     <asp:Label ID="lblStateofTheProduct" runat="server"  Visible="false"></asp:Label>
 
 </td>
 <td>
  
                  
 
 </td>

  </tr>


   <tr>
 
 
    <th>淘日本への掲載</th>
 



 <td>
 
 <asp:TextBox ID="txtJapanlisting"   runat="server"></asp:TextBox>
 
     <asp:Label ID="lblListingToJapan" runat="server"  Visible="false"></asp:Label>
 
 </td>


 
  </tr>

  <tr>

  
 
  <div class="btn">  
  
     <input type="submit" id="btnpopup" onclick="" runat="server" style="width:200px" value="確認画面へ" />  
      <asp:Button ID="btnConfirm_Save" runat="server" Text="確認画面へ" 
            onclick="btnConfirm_Click" Height="33px" Font-Size="Medium" 
         /></div>    
 
 
  
  
  </tr>
  
 </table>


 </div>
</div>
</div>




</asp:Content>
