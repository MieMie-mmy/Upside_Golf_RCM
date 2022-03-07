<%@ Page Title="" Language="C#" MasterPageFile="~/JishaMaster.Master" AutoEventWireup="true" CodeBehind="Jisha_Order.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Order" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">

<script type="text/javascript" src="/js/jquery.min.js"></script>
<script type="text/javascript" src="/js/ddaccordion.js"></script>
<script type="text/javascript" src="/js/acordin.js"></script>

<style type="text/css">


    .style1
    {
        height: 30px;
        width:400px;
        
    }

    
     .style1 th
     {
         
         border:0;
     }

       .leftbox
     {
         
         border:0;
         width:300px;
     }
     
     
     .my_btn{ 
  font-family:Arial; 
  font-size:10pt; 
  font-weight:normal; 
  line-height:30px; 
  border:0px;
  background-image:url('../../images/order_btn.jpg'); 
  cursor:pointer;
}


 .my_btn1{ 
  font-family:Arial; 
  font-size:10pt; 
  font-weight:normal; 
  line-height:30px; 
  border:0px;
  background-image:url('../../images/back.jpg'); 
  cursor:pointer;
}

</style>
   
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />
<script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" type="text/javascript"></script>
<script src="../../Scripts/jquery.droppy.js" type="text/javascript"></script>  

<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  

 <link rel="stylesheet" href="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/themes/base/jquery-ui.css" type="text/css" media="all" />
			<link rel="stylesheet" href="http://static.jquery.com/ui/css/demo-docs-theme/ui.theme.css" type="text/css" media="all" />
			<script src="http://ajax.googleapis.com/ajax/libs/jquery/1.4.2/jquery.min.js" type="text/javascript"></script>
			<script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.5/jquery-ui.min.js" type="text/javascript"></script>

<%--<script type="text/javascript" language="javascript">


$(function() {
    $("#<%= txtOrder_date.ClientID %>").datepicker();
});
</script>--%>





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    
                       
                           <div id="CmnContents">
	                            <div id="ComBlock">
                                      <!-- User search -->
     
     <table summary="" class="center" border="0" height="90px" width="710px" cellpadding="0" cellspacing="0">
	<tbody><tr><td><img src="http://racket.co.jp/images/cart/cartnavi01.gif"></td></tr>
	</tbody></table>

<div class="clear"></div>


<div id="cart-box" style="text-align:left; width:100%">
<asp:GridView runat="server" ID="gvCart" AutoGenerateColumns="False" Width="80%" ShowFooter="True"
        
        EmptyDataText="Currently, You have no items in your shopping basket.<br/>Please enjoy shopping by all means! We are looking forward to use." 
        onrowdatabound="gvCart_RowDataBound"  >
        <Columns>
         <asp:BoundField DataField="Item_ID" HeaderText="Item ID" Visible="false" />
         
         <asp:TemplateField HeaderText="画像" >
            <ItemTemplate>
             <asp:Image ID="img"  ImageUrl ='<%# Eval("Image_Name") %>'   runat="server" 
             ControlStyle-Width="80" ControlStyle-Height = "90"   />
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="商　品　情　報">
            <ItemTemplate>
            <asp:Label runat="server" ID="Label3" Text='<%# Eval("Item_Name") +"<br/>￭"+ "サイズ:"+Eval("Size_Name") + "カラー:"+Eval("Color_Name") %>'  />
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="単　価">
            <ItemTemplate>
            <asp:Label runat="server" ID="Label4" Text='<%# "￥"+ Eval("Price")%>'  />
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="数　量">
            <ItemTemplate>
           
             <asp:Label runat="server" ID="lblQuantity" Text='<%# Eval("Quantity") %>' />
             <asp:Label runat="server" ID="Label1" Text="個" />
            </ItemTemplate>
            <FooterTemplate>
            <asp:Label runat="server" ID="lblTotalQuantity" ForeColor="Red"/>
            <asp:Label runat="server" ID="Label2" Text="個" ForeColor="Red"/>
            
            </FooterTemplate>
            </asp:TemplateField>
       
         

         <asp:TemplateField HeaderText="小　計">
            <ItemTemplate>
         
            <asp:Label runat="server" ID="lblAmount" Text='<%# Eval("Amount") %>' />
            <asp:Label runat="server" ID="Label5" Text="円" />
            </ItemTemplate>
            <FooterTemplate>
            <asp:Label runat="server" ID="Label7" Text=" 合計金額： " ForeColor="Red"/>
            <asp:Label runat="server" ID="lblTotalAmount" ForeColor="Red"/>
            <asp:Label runat="server" ID="Label6" Text="円" ForeColor="Red"/>
            </FooterTemplate>
            </asp:TemplateField>
        </Columns>
    <FooterStyle HorizontalAlign="Center" />
    <RowStyle HorizontalAlign="Center" />
</asp:GridView>
	
</div>





	                                     <div class="setDetailBox iconSet iconEdit">

                                    
                                         <h1 style="background-repeat: repeat-x; background-position: center; background-image: url('../../images/th.jpg')">ご注文者情報の入力 </h1>
                                      
                                     
                                 
                                      <table class="userCmnSet editTable">

                                      <tbody>    
                                                                           
                                              <tr>
                                              <td>
                                              
                                              </td>

                                              
                                              </tr> 


                                                   <tr>
           
                                                    <th style="border-style: none; background-image: url('../../images/td.jpg')" 
                                                           width="400">団体種別</th> 
              
                                                         <td>
                                                                  <asp:DropDownList ID="ddlOrganizationType" runat="server" Height="30px" 
                                                                      Width="200px" ToolTip="OrganizationType" TabIndex="1">
                                                                  </asp:DropDownList>
                                                          </td>
                                               </tr>

                                                    <tr>
           
                                                    <th style="background-image: url('../../images/td.jpg'); border-style:hidden;" 
                                                            width="400">団体名</th> 
              
                                                         <td>
                                                                  <asp:TextBox ID="txtOrganiztionName" runat="server" Height="30px"  ToolTip="OrganizationName"  TabIndex="2"></asp:TextBox>
                                                          </td>
                                               </tr>



                                                  <tr>
           
                                                    <th 
                                                          style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">お名前（又は担当者名)</th> 
              
                                                         <td>
           <span>姓</span>     <asp:TextBox ID="txtBill_LastName" runat="server"  
                  Height="30px" TabIndex="3" Width="150px" ToolTip="LastName"></asp:TextBox>
                  <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2"  runat="server" ControlToValidate="txtBill_LastName" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>

                                                          
                            <span>名 </span>                               
                                                             <asp:TextBox ID="txtBill_FirstName" runat="server"  
                  Height="30px"   Width="150px" TabIndex="4" ToolTip="FirstName"></asp:TextBox>   
                <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator3"  runat="server" ControlToValidate="txtBill_FirstName" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>
                                                          </td>
                                               </tr>
       
         <tr>
          
           
        <th style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">フリガナ</th>
                               <td>

         
          <span>セイ</span>      <asp:TextBox ID="txtBillLastName_Kana" runat="server"  
                  Height="30px" TabIndex="5" ToolTip="PhoneticForLastName" Width="135px"></asp:TextBox>
                   
              <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator4"  runat="server" ControlToValidate="txtBillLastName_Kana" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>
            
            
            <span>メイ</span>    <asp:TextBox ID="txtBillFirstName_Kana" runat="server"  
                  Height="30px" TabIndex="6" Width="135px" ToolTip="PhoneticforFirstName"></asp:TextBox>

               <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator5"  runat="server" ControlToValidate="txtBillFirstName_Kana" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>
            
            </td>
                           
        </tr>
        
         <tr>
          
         <th style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">住所</th>
                      
           <td>

                    <span>郵便番号</span><br />
                <asp:TextBox ID="txtBill_ZipCode1" runat="server"  
                  Height="30px" TabIndex="6" ToolTip="ZipCode/Postal_Code" Width="100px"></asp:TextBox>

                    -<asp:TextBox ID="txtBill_ZipCode2" runat="server" Height="30px" Width="100px"></asp:TextBox>

                <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator7"  runat="server" ControlToValidate="txtBill_ZipCode1" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>

                    <br />
                    <br />
                  
                  <span>都道府県</span><br />
                   
                    
                        <asp:DropDownList ID="ddlPrefecture" runat="server" Height="30px" TabIndex="7"  ToolTip="State" Width="200px">
                        </asp:DropDownList>
                    
                    <br />
                    <br />

                    <span>市区町村</span>
                    <br />
                    <br />

                    
                
                                   <asp:TextBox ID="txtBill_City" runat="server"  
                  Height="30px" TabIndex="8" ToolTip="City"></asp:TextBox>

                  
                 <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator9"  runat="server" ControlToValidate="txtBill_City" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>
                    
                  <br />
                  <br />

                  <span>必須番地</span>

                  <br />

                        <asp:TextBox ID="txtBill_Address1" runat="server"  
                  Height="50px" TextMode="MultiLine" TabIndex="9" Width="300px" ToolTip="Address"></asp:TextBox>

              <%--     <asp:RequiredFieldValidator ID="RequiredFieldValidator10"  runat="server" ControlToValidate="txtBill_Address1" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>
--%>
                   
                             

                <br />

                <br />

                <span>ビル、マンション名　部屋番号</span>
                <br />

                <br />
                
                <asp:TextBox ID="txtBillAddress2" runat="server"  
                  Height="50px" TextMode="MultiLine" TabIndex="10" Width="300px"></asp:TextBox>

          <%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator11"  runat="server" ControlToValidate="txtBillAddress2" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>
--%>




                    
                </td>
                
                </tr>       
           
        
        

         
        

      

         
         


 <tr>
          
           
        <th style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">電話番号(自宅)</th>
                               <td>

         
                                   <asp:TextBox ID="txtBillphone1" runat="server" Width="80px" ToolTip="Phone No"></asp:TextBox>
                                   &nbsp;-
                                   <asp:TextBox ID="txtBillphone2" runat="server" Width="80px"  ToolTip="Phone No"></asp:TextBox>
                                   &nbsp;-
                                   <asp:TextBox ID="txtBillphone3" runat="server" Width="80px"  ToolTip="Phone No"></asp:TextBox>
                                   <br />

                                   <span>[例]　06-6843-1321</span>

                <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator12"  runat="server" ControlToValidate="txtBillphone1" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>
                    
            </td>
                           
        </tr>
        

         <tr>
          
           
        <th style="border-right-style: none; border-left-style: none; background-image: url('../../images/td.jpg')">電話番号(携帯)</th>
                               <td>

         
               
                      <asp:TextBox ID="txtBill_EMGphone1" runat="server" Width="80px"></asp:TextBox>
&nbsp;-
               <asp:TextBox ID="txtBill_EMGphone2" runat="server" Width="80px"></asp:TextBox>
&nbsp;-
               <asp:TextBox ID="txtBill_EMGphone3" runat="server" Width="80px"></asp:TextBox>

                  <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator13"  runat="server" ControlToValidate="txtBill_EMGphone1" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>
            

            <br />
            <br />

            <span>[例]　090-1234-567X</span>
            
            </td>
                           
        </tr>



        <tr>
        
        
           <th style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">メールアドレス</th>
                      <td>

             <span>[例]　090-1234-567X090-1234-567X</span><br />
                                                                                                 
         
                <asp:TextBox ID="txtBill_MailAddress" runat="server"  
                  Height="30px" TabIndex="17" ToolTip="Mail_Address"></asp:TextBox>


                  

        <%--           <asp:RequiredFieldValidator ID="RequiredFieldValidator61"  runat="server" ControlToValidate="txtBill_MailAddress" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>
            </td>
                           
        </tr>


         <tr>
        
        
           <th style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">メールアドレス(確認)</th>
                      <td>

             <span>[例]　racket_taro@racket.co.jp</span><br />
                                                                                                 
         
                <asp:TextBox ID="txtMailAddressConfirm" runat="server"  
                  Height="30px" TabIndex="18"  ToolTip="MailAddress_Confrim"></asp:TextBox>

                  
                 <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator07"  runat="server" ControlToValidate="txtBill_MailAddress" Display="Dynamic" ErrorMessage="should not be blank !!"  style="color:Red"></asp:RequiredFieldValidator>--%>
                          
            </td>
                           
        </tr>
        
        
        
        
        
        
        
        

        <tr>
        
        
        <th style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">お届け先</th>
         
            


              <td>
              <div>
              
              <asp:RadioButton ID="rdoSame" runat="server" Checked="True"  value="1"  name="rdoSame"
                      Text="上記と同じ住所に送る" GroupName="MoreAddress" Width="200px"   
                      AutoPostBack="true" oncheckedchanged="rdoSame_CheckedChanged" />
            </div>
                   
               <div>
                    <asp:RadioButton ID="rdoDiff" runat="server" Text="上記と別の住所に送る"  value="0" name="rdoDiff"
                      GroupName="MoreAddress" Width="200px" 
                        oncheckedchanged="rdoDiff_CheckedChanged"  AutoPostBack="true"/>

                 </div> 
                
           
            </td>

   </tr>

   </tbody>
   
   </table>

   </div>
  

 
 
  
				<div id="hideBox" runat="server" visible="false">
			
            



			<table cellspacing="0" cellpadding="0" border="0" summary="ご注文者情報の入力">
	
			<tbody>

        
       
       
         <tr>
          
           
        <th style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none; border-style:hidden;">お名前</th>
              
              
              <td class="middlebox">

        <span>姓</span>      <asp:TextBox ID="txtShip_LastName" runat="server"  
                  Height="30px"  ToolTip="LastName" Width="150px"></asp:TextBox>
            

         
         <span>名</span>  <asp:TextBox ID="txtShip_FirstName" runat="server"  
                  Height="30px" ToolTip="Firstname" Width="150px"></asp:TextBox>
            </td>
                           
        </tr>
        

         <tr>
          
           
        <th style="background-image: url('../../images/td.jpg');  border-bottom-style:hidden;">フリガナ</th>
                               <td class="middlebox">

         
      <span>セイ</span>         <asp:TextBox ID="txtShip_LastName_Kana" runat="server"  
                  Height="30px"  ToolTip="LastName_phonetic" Width="135px"></asp:TextBox>
            
          
           
         <span>メイ</span>   <asp:TextBox ID="txtShipfirstName_Kana" runat="server"  
                  Height="30px" ToolTip="FirstName_phonetic" Width="135px"></asp:TextBox>
            </td>
                           
        </tr>
        

         <tr>
          
           
        <th class="leftbox" 
                 style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">郵便番号</th>
                               <td class="middlebox">

         
                &nbsp;&nbsp;&nbsp;

         
                <asp:TextBox ID="txtShip_ZipCode1" runat="server"  
                  Height="30px"  ToolTip="Postal_Code" Width="80px"></asp:TextBox>-

                    <asp:TextBox ID="txtShip_ZipCode2" runat="server"  
                  Height="30px"  ToolTip="Postal_Code" Width="80px"></asp:TextBox>
            </td>
                           
        </tr>
        
         <tr>
          
           
        <th style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">住所</th>
                               <td>

                              <span>都道府県</span>
                              <br />
                              <br />


                              <asp:DropDownList ID="ddl_Prefecture2" runat="server" Height="30px" ToolTip="State" Width="200px">
                        </asp:DropDownList>


                        <br />
                        <br />

                     <span>市区町村</span>
                     <br />

                     <br />

                        <asp:TextBox ID="txtShip_City" runat="server"  
                  Height="30px"></asp:TextBox>


                  <br />

                  <br />

                <span>番地</span><br />
                <br />
            
                           
               
                <asp:TextBox ID="txtShip_Address1" runat="server"  
                  Height="50px" TextMode="MultiLine" Width="300px"></asp:TextBox>
           
           <br />

           <br />
                           
        
        <span>ビル、マンション名　部屋番号</span><br />

        <br />
         
                <asp:TextBox ID="txtShip_Address2" runat="server"  
                  Height="50px" TextMode="MultiLine"  Width="300px"></asp:TextBox>


            </td>
                           
        </tr>
        
         <tr>
          
           
        <th class="leftbox" 
                 style="background-image: url('../../images/td.jpg'); border-right-style: none; border-left-style: none">電話番号</th>
                               <td class="middlebox">

         
                 <asp:TextBox ID="txtShPhone1" runat="server" Width="80px"></asp:TextBox>
&nbsp;-
               <asp:TextBox ID="txtShPhone2" runat="server" Width="80px"></asp:TextBox>
&nbsp;-
               <asp:TextBox ID="txtShPhone3" runat="server" Width="80px"></asp:TextBox>
            </td>
                           
        </tr>
        



        </tbody>
        </table>






                                      <!-- User search -->

                                     

         <%--<tr>
          
           
        <th>Payment_Method</th>
                               <td>

         
                <asp:TextBox ID="txtPayment_Method" runat="server"  
                  Height="30px" TabIndex="3"></asp:TextBox>
            </td>
                           
        </tr>
        
         <tr>
          --%>
           
        <%--<th>Order_Comment</th>
                               <td>

         
                <asp:TextBox ID="txtOrder_Comment" runat="server"  
                  Height="30px" TabIndex="3"></asp:TextBox>
            </td>
                           
        </tr>
        
            
         <tr>
          
           
        <th>Use_Point</th>
                               <td>

         
                <asp:TextBox ID="txtUse_Point" runat="server"  
                  Height="30px" TabIndex="3"></asp:TextBox>
            </td>
                           
        </tr>
        
         <tr>
          
           
        <th>Sub_Total</th>
                               <td>

         
                <asp:TextBox ID="txtSub_Total" runat="server"  
                  Height="30px" TabIndex="3"></asp:TextBox>
            </td>
                           
        </tr>
        
         <tr>--%>
          <tr>
           
                               <td>

         
                                   &nbsp;</td>
                           
        </tr>

  
    

      </div>
 


 
 

 <table>
           
                   
                   <tr>
                   
                   <td>

                   <asp:Button ID="btnBackClick" runat="server" 
                    onclick="btnback_Click" Height="52px"  width="320px" CssClass="my_btn1" /> 
                   
                   
                   </td>
                                     
                   
                  



                
                 
                    <td><asp:Button ID="btnsave" runat="server" 
                    onclick="btnsave_Click" Height="52px"  width="320px" CssClass="my_btn" /> 
                    
                    </td>
                          
                  <td>   <p id="toTop"><a href="#CmnContents">▲TOP</a></p></td>


                           </tr>
               

               </table>
                   </div>


                   
		
	
	</div>
        
       
          
 
        
      





 

</asp:Content>
