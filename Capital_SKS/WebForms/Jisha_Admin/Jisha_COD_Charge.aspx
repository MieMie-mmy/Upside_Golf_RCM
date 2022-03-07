<%@ Page Title="ラケットプラザ＜管理画面／代引料設定＞" Language="C#" MasterPageFile="~/Jisha_Admin_Master.Master" AutoEventWireup="true" CodeBehind="Jisha_COD_Charge.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_COD_Charge" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<link href="../../Styles/Jisha_base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Jisha_common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/Jisha_style.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8)
            return true;
        else return false;
    } 
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div id="CmnWrapper">
<div id="CmnContents">
	<div id="ComBlock">
	
	<div class="soryoSet inlineSet">
	<h1>送料設定</h1>
    	<%--<form>--%>
	<h2>代引料（都道府県 × 注文金額別））</h2>
<dl>
			<dt>優先順位 1</dt>
			<dd>
				<p>
                
			<asp:DropDownList ID="ddlpriority1" runat="server">
             
                </asp:DropDownList>
			
                  <asp:Label ID="dp1" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="txttotal" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lbltotal" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
                   <asp:TextBox ID="txtdelivertycharge" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                      <asp:Label ID="lbldelivery" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>

<dl>
			<dt>優先順位 2</dt>
			<dd>
				<p>
               
			 <asp:DropDownList ID="ddlpriority2" runat="server">
                 
                    </asp:DropDownList>
				
                   <asp:Label ID="dp2" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:TextBox ID="txttotal2" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lbltotal2" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
                  <asp:TextBox ID="txtdelivery2" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lbldelivery2" runat="server" Text="" Visible="false" Width="100px"></asp:Label>	円
			</dd>
		</dl>

<dl>
			<dt>優先順位 3</dt>
			<dd>
				<p>
             <asp:DropDownList ID="ddlpriority3" runat="server">
               
                    </asp:DropDownList>
				
                    <asp:Label ID="dp3" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
           <asp:TextBox ID="txttotal3" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lbltotal3" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
                 <asp:TextBox ID="txtdelivery3" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lbld3" runat="server" Text="" Visible="false" Width="100px"></asp:Label>		円
			</dd>
		</dl>

<dl>
			<dt>優先順位 4</dt>
			<dd>
				<p>
            <asp:DropDownList ID="ddlpriority4" runat="server">
              
                 
                   
                </asp:DropDownList>
				
                   <asp:Label ID="dp4" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txttotal4" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
          <asp:Label ID="lbltotal4" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
               <asp:TextBox ID="txtdelivery4" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lbld4" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
<dl>
			<dt>優先順位 5</dt>
			<dd>
				<p>
           <asp:DropDownList ID="ddlpriority5" runat="server">
                
                    </asp:DropDownList>
				
               <asp:Label ID="dp5" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="txttotal5" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lblt5" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
               <asp:TextBox ID="txtdelivery5" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lbld5" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
<dl>
			<dt>優先順位 6</dt>
			<dd>
				<p>
           <asp:DropDownList ID="ddlpriority6" runat="server">
                
                    </asp:DropDownList>
				
                 <asp:Label ID="dp6" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
       <asp:TextBox ID="txttotal6" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
        <asp:Label ID="lblt6" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
                <asp:TextBox ID="txtdelivery6" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lbld6" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
 <dl>
			<dt>優先順位 7</dt>
			<dd>
				<p>
        <asp:DropDownList ID="ddlpriority7" runat="server">
                   
                    </asp:DropDownList>
				
                  <asp:Label ID="dp7" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
       <asp:TextBox ID="txttotal7" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lblt7" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
              <asp:TextBox ID="txtdelivery7" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lbld7" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
 <dl>
			<dt>優先順位 8</dt>
			<dd>
				<p>
             <asp:DropDownList ID="ddlpriority8" runat="server">
                    
                    </asp:DropDownList>
				  <asp:Label ID="dp8" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
       <asp:TextBox ID="txttotal8" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lblt8" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
            <asp:TextBox ID="txtdelivery8" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lbld8" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
 <dl>
			<dt>優先順位 9</dt>
			<dd>
				<p>
              <asp:DropDownList ID="ddlpriority9" runat="server">
                  
                    </asp:DropDownList>
			 <asp:Label ID="dp9" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
       <asp:TextBox ID="txttotal9" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lblt9" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
           <asp:TextBox ID="txtdelivery9" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lbld9" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
  <dl>
			<dt>優先順位 10</dt>
			<dd>
				<p>
                 <asp:DropDownList ID="ddlpriority10" runat="server">
                    </asp:DropDownList>
			 <asp:Label ID="dp10" runat="server" Text="" Visible="false"></asp:Label>
                </p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
       <asp:TextBox ID="txttotal10" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lblt10" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                
            <asp:TextBox ID="txtdelivery10" runat="server" onkeypress="return isNumberKey(event)"> </asp:TextBox>
                  <asp:Label ID="lbld10" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
        <br /><br />
<%--</div>
<div class="soryoBox">--%>
  <dl>
			<dt>優先順位 997</dt>
			<dd>
				<p>上記以外の県</p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="txttotal997" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lblt997" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:TextBox ID="txtdelivery997" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lbld997" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
  <dl>
			<dt>優先順位 998</dt>
			<dd>
				<p>上記以外の県</p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
             <asp:TextBox ID="txttotal998" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lblt998" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
               <asp:TextBox ID="txtdelivery998" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lbld998" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
  <dl>
			<dt>優先順位 999</dt>
			<dd>
				<p>上記以外の県</p>
			</dd>
			<dd>
				合計金額が &nbsp;&nbsp;&nbsp;&nbsp;
              <asp:TextBox ID="txttotal999" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lblt999" runat="server" Text="" Visible="false" Width="100px"></asp:Label>&nbsp;&nbsp;
                円以上で送料 &nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:TextBox ID="txtdelivery999" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                  <asp:Label ID="lbld999" runat="server" Text="" Visible="false" Width="100px"></asp:Label>円
			</dd>
		</dl>
    </div>
      <div align="center">
        	<p class="f_btn">
            <input type="submit" id="btnpopup" onclick="this.form.target='_blank';return true;" runat="server" style="width:200px" Visible="false" />
            <asp:Button ID="btnsubmit" runat="server" Text="確認画面"  
                    OnClientClick="this.form.target='_blank';return true;" Width="117px" 
                    Height="31px" />
                <asp:Button ID="btnSave" runat="server" Text="更　新" onclick="btnSave_Click"  Visible= "false"  Width="117px" Height="31px" />
   
            
            </p>
            </div>

</div><!--inlineSet-->

</div><!--ComBlock-->
</div><!--CmnContents-->


</div><!--CmnWrapper-->
</asp:Content>
