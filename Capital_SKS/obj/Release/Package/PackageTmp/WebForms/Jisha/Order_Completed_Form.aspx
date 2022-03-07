<%@ Page Title="" Language="C#" MasterPageFile="~/JishaMaster.Master" AutoEventWireup="true" CodeBehind="Order_Completed_Form.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Order_Completed_Form" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
       
        .my_btn{ 
  font-family:Arial; 
  font-size:10pt; 
  font-weight:normal; 
  border:0px;
  background-image:url('../../images/btn_Confirm.jpg'); 
  cursor:pointer;
}
      
      
 .my_btn1{ 
  font-family:Arial; 
  font-size:10pt; 
  font-weight:normal; 
  border:0px;
  background-image:url('../../images/back.jpg'); 
  cursor:pointer;
} 


       </style>


    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<table summary="" class="center" border="0" height="90px" width="710px" cellpadding="0" cellspacing="0">
	<tbody><tr><td><img src="http://racket.co.jp/images/cart/cartnavi03.gif"></td></tr>
	</tbody></table>

<div class="clear"></div>
<!--div class="grey_bar">
	<ul class="steps">
    	<li>(1) お届け先情報の入力　≫　</li>
    	<li>(2) お支払い方法の選択　≫　</li>
    	<li class="active">(3) ご注文内容のご確認　≫　</li>
    	<li>(4) ご注文完了</li>
    </ul>
</div-->

<div class="input_form">
	

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
             <HeaderStyle CssClass="mybtn" />
            </asp:TemplateField>

            <asp:TemplateField HeaderText="商　品　情　報">
            <ItemTemplate>
            <asp:Label runat="server" ID="Label3" Text='<%# Eval("Item_Name") +"<br/>￭"+ "サイズ:"+Eval("Size_Name") + "カラー:"+Eval("Color_Name") %>'  />
            </ItemTemplate>
            <FooterTemplate>
            </FooterTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText=" 	単　価">
            <ItemTemplate>
            <asp:Label runat="server" ID="Label4" Text='<%# "￥"+ Eval("Price")%>'  />
            </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText=" 	数　量 	">
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
            <asp:Label runat="server" ID="lblShippingAmount" ForeColor="Red"/><br />

            <asp:Label runat="server" ID="Label21" Text=" 代引手数料： " ForeColor="Red"/>
            <asp:Label runat="server" ID="lblCODAmount" ForeColor="Red"/>
            <asp:Label runat="server" ID="Label22" Text="円" ForeColor="Red"/><br />
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
	<form method="post" action="." onsubmit="document.cconf.submit_button.disabled=true;return true;" name="cconf" id="cconf">
		<input name="step" value="4" type="hidden">
		<input value="3" name="paymode" type="hidden">

		<div class="inp1">
		<div id="cart_ttl6">ご注文者情報の入力</div>

		<table summary="ご注文者情報の入力" border="0" cellpadding="0" cellspacing="0">
		<tbody>


           <tr>
		<th class="leftbox2">
            <asp:Label ID="lblOrganizationType" runat="server" Text="団体種別"></asp:Label>
            </th>
		<td class="middlebox2">
            <asp:Label ID="lblOrganization_Type" runat="server"></asp:Label>
               </td>
		</tr>




        
        <tr>
		<th class="leftbox2">
            <asp:Label ID="lblOrganizationName" runat="server" Text="団体名"></asp:Label>
            </th>
		<td class="middlebox2">
            <asp:Label ID="Label23" runat="server" Text="lblOrganization_Name"></asp:Label>
            </td>
		</tr>


        <tr>
		<th class="leftbox2">
            <asp:Label ID="lblName" runat="server">お名前（団体の場合は担当者名）*</asp:Label>
            </th>
		<td class="leftbox2">     <asp:Label ID="lbl_Name" runat="server"></asp:Label></td>
		</tr>

                
        <tr>
		<th class="leftbox2">
            <asp:Label ID="Label8" runat="server">フリガナ</asp:Label>
            </th>
		<td class="leftbox2">     <asp:Label ID="lbl_Phonetic" runat="server"></asp:Label></td>
		</tr>



               
        <tr>
		<th class="leftbox2">
            <asp:Label ID="lblZipCode" runat="server" Text="郵便番号"></asp:Label>
            </th>
		<td class="leftbox2"> <asp:Label ID="lblPostal_Code" runat="server"></asp:Label></td>
		</tr>


        <tr>
		<th class="leftbox2">
            <asp:Label ID="lblState" runat="server" Text="都道府県"></asp:Label>
            </th>
		<td class="middlebox2"><asp:Label ID="lblPrefecture" runat="server"></asp:Label>
      
            <asp:DataList ID="DataList1" runat="server">
            </asp:DataList>
           
            </td>

        
		</tr>

          <tr>
			<th class="leftbox2">
                <asp:Label ID="lblCity" runat="server" Text="市区町村" ToolTip="City"></asp:Label>
            </th>
			<td class="middlebox2"><asp:Label ID="lbl_City" runat="server"></asp:Label></td>
		</tr>


         <tr>
		<th class="leftbox2">
                <asp:Label ID="lblAddress" runat="server" Text="番地"></asp:Label>
             </th>
		<td class="middlebox2"><asp:Label ID="lbl_Address" runat="server"></asp:Label>
          </td>
		</tr>


         <tr>
		<th class="leftbox2">
                <asp:Label ID="lblBuilding" runat="server" Text="ビル、マンション名"></asp:Label>
             </th>
		<td class="middlebox2">
            <asp:Label ID="lblAddress_2" runat="server"></asp:Label>
            </td>
		</tr>



         <tr>
		<th class="style1">
            電話番号(自宅)*</th>
		<td class="style1">
          <asp:Label ID="lblPhone_no" runat="server"></asp:Label>
             </td>
		</tr>


        
         <tr>
		<th class="leftbox2">
            <asp:Label ID="Label24" runat="server" Text="電話番号(携帯)"></asp:Label>
             </th>
		<td class="middlebox2">
            <asp:Label ID="lblHand_Phone" runat="server"></asp:Label>
            </td>
		</tr>


          <tr>
		<th class="leftbox2">
                <asp:Label ID="lblMailAddress" runat="server" Text="メールアドレス"></asp:Label>
            </th>
		<td class="middlebox2">
            <asp:Label ID="lblemail_add" runat="server"></asp:Label></td>
		</tr>






     




		
		
		<tr>
		<th class="style1">
            <asp:Label ID="lblPayment_method" runat="server"></asp:Label>
            </th>
		<td class="style1"></td>
		</tr>
		<tr>
		<th class="leftbox2">
            <asp:Label ID="lblChargeAmount" runat="server"></asp:Label>
            </th>
		<td class="middlebox2">&nbsp;</td>
		</tr>
		


		


		<tr>
			<th class="leftbox2">
                
                <asp:Label ID="lblDeliveryAmount" runat="server"></asp:Label>
                
            </th>
			<td class="middlebox2">&nbsp;</td>
		</tr>
		<tr>
			<th class="leftbox2">
                <asp:Label ID="lblCOD_Amount" runat="server"></asp:Label>
            </th>
			<td class="middlebox2"></td>
		</tr>
		<tr>
			<th class="leftbox2">&nbsp;</th>
			<td class="middlebox2">
				&nbsp;</td>
		</tr>
		<tr>
		<th class="leftbox2">
            &nbsp;</th>
			<td class="middlebox2">
				 &nbsp;</td>
		</tr>
		<tr>
			<th class="leftbox2">
                &nbsp;</th>
			<td class="middlebox2">
				inaoka@racket.co.jp<br>
				<br>携帯電話のメールアドレスを入力する場合「capy.jp」からのメールを受信できる設定に変更してください。
			</td>
		</tr>
		</tbody>
		</table>
		</div>

		<div class="inp1">
		<div id="cart_ttl7">お届け情報</div>
		<table summary="ご注文者情報の入力" border="0" cellpadding="0" cellspacing="0">
		<tbody><tr>
		<th class="leftbox2">お届け先</th>
		<td id="DeliToggle" class="middlebox2">
		
			上記と同じ住所に送る
		
		</td>
		</tr>
		</tbody></table>
		</div>

		<div class="inp1">
		<div id="cart_ttl8">お支払い方法</div>
		<table summary="お支払い方法の選択" border="0" cellpadding="0" cellspacing="0">
		<tbody><tr>
		<th class="leftbox2">
			
			代引き
			
			
		</th>
		<td class="middlebox2">
			
		</td>
		</tr>
		</tbody></table>
		</div>


		<div class="inp1">
		<div id="cart_ttl9">その他の情報</div>
		<table id="Other" border="0" cellpadding="0" cellspacing="0">
		<tbody><tr>
		<th class="leftbox2">メールマガジン(ほぼ毎週発行)</th>
		<td class="middlebox2">
			受け取る
			
			
		</td>
		</tr>
		<tr>
		<th class="leftbox2">ご意見、ご要望</th>
		<td class="middlebox2">
			
		</td>
		</tr>
		</tbody></table>
		</div>
		<br>

		<input value="4" name="flg" type="hidden">

		<div style="text-align:center; padding:10px; margin-top:1em; font-size:14px;">
			<!--input type="button" onclick="javascript:history.back(1);" value="前の画面へ戻る" style="height:30px;"-->
			<!--input type="submit" name="submit_button" id="submit_button" value="注文を確定する" style="margin-left:1em; font-weight:bold; height:30px"-->
	<table summary="" class="center" border="0" cellpadding="0" cellspacing="10">
	<tbody><tr><td>    
           <asp:Button ID="btnBack" runat="server"   Width="300px"  CssClass="my_btn1" Height="50"
                onclick="btnBack_Click"/></td>
	<td>   <asp:Button ID="btnSave" runat="server"   Width="323px"  CssClass="my_btn"  Height="50"
                onclick="btnSave_Click"/></td></tr>
	</tbody></table>
		</div>

	
		<!--
		{u&#39;cpay_card_no3&#39;: [u&#39;&#39;], u&#39;changeitem&#39;: [u&#39;&#39;], u&#39;payment_method&#39;: [u&#39;2&#39;], u&#39;memo&#39;: [u&#39;&#39;], u&#39;cpay_yuko_y&#39;: [u&#39;12&#39;], u&#39;mail_magazine&#39;: [u&#39;00&#39;], u&#39;paymode&#39;: [u&#39;2&#39;], u&#39;cpay_card_no2&#39;: [u&#39;&#39;], u&#39;cpay_card_no1&#39;: [u&#39;&#39;], u&#39;installment_type&#39;: [u&#39;1&#39;], u&#39;cpay_card_no4&#39;: [u&#39;&#39;], u&#39;step&#39;: [u&#39;3&#39;], u&#39;cpay_yuko_m&#39;: [u&#39;01&#39;]}
		-->

        <div>
        <table>
        <tr>
        
           <td>
       
           
             
            </td>

            <td>

           
              
            </td>


                </tr>
                </table>
        </div>

        	<p style="text-align:center; color:red; font-weight:bold">
		<a href="#"><font color="red">返品・交換について（ご利用ガイド）</font></a>
		</p>
      
	</form>
</div>


</asp:Content>
