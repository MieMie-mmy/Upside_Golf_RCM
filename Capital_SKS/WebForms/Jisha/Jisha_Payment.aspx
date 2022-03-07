<%@ Page Title="" Language="C#" MasterPageFile="~/JishaMaster.Master" AutoEventWireup="true" CodeBehind="Jisha_Payment.aspx.cs" Inherits="ORS_RCM.WebForms.Jisha.Jisha_Payment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="~/Styles/styles.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<script type="text/javascript">
    function movetoNext(current, nextFieldID) {
        if (current.value.length >= current.maxLength) {
            document.getElementById(nextFieldID).focus();
        }
    }
</script>

<h5>お支払い方法の選択</h5>
<table width="100%">
            <tr>
            <td>
            <asp:RadioButton ID="rdoCreditCard" runat="server" Text="クレジットカード決済" GroupName="GroupRdo" Checked="true"/>
            </td>
            <td>
            <asp:Label ID="Label1" runat="server" Text="カード番号 ："></asp:Label>&nbsp;
             <input type="text" id="txtFirstCreditNo" name="txtFirstCreditNo"  size="4" onkeyup="movetoNext(this, 'txtSecondCreditNo')" maxlength="4" />－
            <input type="text" id="txtSecondCreditNo" name="txtSecondCreditNo" size="4" onkeyup="movetoNext(this, 'txtThirdCreditNo')" maxlength="4" />－
            <input type="text" id="txtThirdCreditNo" name="txtThirdCreditNo" size="4" onkeyup="movetoNext(this, 'txtFourthCreditNo')" maxlength="4" />－
            <input type="text" id="txtFourthCreditNo" name="txtFourthCreditNo" size="4" maxlength="4" />
            <asp:Label ID="lblCreditValidator" runat="server" ForeColor="Red"></asp:Label>
            <br /><br />
            <asp:Label ID="Label2" runat="server" Text="有効期限　："></asp:Label>&nbsp;
            <asp:DropDownList ID="ddlMonth" runat="server">
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
            </asp:DropDownList>/
            <asp:DropDownList ID="ddlYear" runat="server">
                <asp:ListItem>2012</asp:ListItem>
                <asp:ListItem>2013</asp:ListItem>
                <asp:ListItem>2014</asp:ListItem>
                <asp:ListItem>2015</asp:ListItem>
                <asp:ListItem>2016</asp:ListItem>
                <asp:ListItem>2017</asp:ListItem>
                <asp:ListItem>2018</asp:ListItem>
                <asp:ListItem>2019</asp:ListItem>
                <asp:ListItem>2020</asp:ListItem>
            </asp:DropDownList>
            <br /><br />
            <asp:Label ID="Label3" runat="server" Text="支払い方法:"></asp:Label>&nbsp;
          
            
            <asp:DropDownList ID="ddlPaymentMethod" runat="server">
            <asp:ListItem>一括</asp:ListItem>
            <asp:ListItem>分割</asp:ListItem>
            <asp:ListItem>ボーナス一括</asp:ListItem>
            <asp:ListItem>ボーナス分割</asp:ListItem>
            <asp:ListItem>リボ</asp:ListItem>
            </asp:DropDownList>



            <br /><br />
            <asp:Label ID="Label4" runat="server" Text="支払い回数："></asp:Label>&nbsp;
            <asp:DropDownList ID="ddlNoOfPayment" runat="server" Enabled="true"></asp:DropDownList>
            <br /><br />
            <asp:Label ID="lblCreditCardWarningI" runat="server" Text="弊社ではクレジット番号をデータベースに残しません。" ForeColor="Red"></asp:Label><br />
            <asp:Label ID="lblCreditCardWarningII" runat="server" Text="恐れ入りますが、以前に一度入力いただいた場合でも お買物の都度「必要情報(番号：名義人：有効期限等)」 を入力くださいますようお願い申し上げます。"></asp:Label>&nbsp;
            </td>
            </tr>
            <tr>
            <td>
            <asp:RadioButton ID="rdoCashOn" runat="server" Text="代引き" GroupName="GroupRdo"/>
            </td>
            <td>
            <asp:Label ID="lblCashOn" runat="server" Text="30,000円以上(割引後)お買い上げの場合は無料。<br />9,999円までは一律324円となります。 "></asp:Label>
            </td>
            </tr>
            <tr>
            <td>
            <asp:RadioButton ID="rdoBankTransfer" runat="server" Text="銀行振込(後払い) " GroupName="GroupRdo"/>
            </td>
            <td>
            <asp:Label ID="lblBankTransferI" runat="server" Text="振込先銀行は請求書に記載いたしております" ForeColor="Red"></asp:Label><br />
            <asp:Label ID="lblBankTransferII" runat="server" Text="手数料はお客様のご負担でお願いいたします。"></asp:Label>
            </td>
            </tr>
            <tr>
            <td>
            <asp:RadioButton ID="rdoConvenienceStore" runat="server" Text="コンビニ決済・郵便振替(後払い)" GroupName="GroupRdo"/>
            </td>
            <td>
            <asp:Label ID="lblConvenienceStoreI" runat="server" Text="最寄りのコンビニから簡単入金。　振込用紙はご依頼主様へ送ります。" ForeColor="Red"></asp:Label><br />
            <asp:Label ID="lblConvenienceStoreII" runat="server" Text="手数料は当社負担です。ご希望の方は郵便振替用紙を発送いたします。 "></asp:Label><br />
            <asp:Label ID="lblConvenienceStoreIII" runat="server" Text="最寄りの郵便局から簡単入金。" ForeColor="Red"></asp:Label><br />
            <asp:Label ID="lblConvenienceStoreIV" runat="server" Text="手数料は当社負担です。ご希望の方は郵便振替用紙を発送いたします。 "></asp:Label>
            </td>
            </tr>
            <tr>
            <td colspan="2">
            <h5>
            その他の情報の入力
            </h5>
            </td>
            </tr>
            <tr>
            <td align="justify" colspan="2">
            <asp:Label ID="lblOtherInfoI" runat="server" ForeColor="Red" Text="お得情報満載！"></asp:Label><br />
            <asp:Label ID="lblOtherInfoII" runat="server" ForeColor="Green" Text="ラケプラのキャンペーンやセールの情報をまとめてお届け！"></asp:Label><br /><br />
            <asp:Label ID="lblOtherInfoIII" runat="server" ForeColor="Red" Text="メルマガ会員限定キャンペーン！"></asp:Label><br />
            <asp:Label ID="lblOtherInfoIV" runat="server" ForeColor="Green" Text="メルマガ会員限定のキャンペーンやプレゼントが目白押し！"></asp:Label><br /><br />
            <asp:Label ID="lblOtherInfoVI" runat="server" ForeColor="Red" Text="さらに！先取り情報も！？"></asp:Label><br />
            <asp:Label ID="lblOtherInfoVII" runat="server" ForeColor="Green" Text="メールにていち早くお得情報もお届け！見逃してるとソンしちゃうかも！？"></asp:Label><br />
            </td>
            </tr>
            <tr>
            <td>
            メールマガジン(ほぼ毎週発行)
            </td>
            <td>
            <asp:RadioButtonList ID="rdoNewsLetter" runat="server">
                <asp:ListItem Value="1">メールマガジンを受け取る</asp:ListItem>
                <asp:ListItem Value="2">メールマガジンを受け取らない</asp:ListItem>
                <asp:ListItem Value="3">購読済み</asp:ListItem>
            </asp:RadioButtonList>
            </td>
            </tr>
            <tr>
            <td>
            ご意見、ご要望
            </td>
            <td>
            <asp:TextBox ID="txtOpinionRequest" runat="server" Height="79px" 
                    TextMode="MultiLine" Width="279px"></asp:TextBox>
            </td>
            </tr>
            <tr>
            <td>
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="button" 
                            onclick="btnSubmit_Click" ValidationGroup="Group1"/>&nbsp;
                            <input id="btnCancel" type="reset" value="Cancel" class="button" />
            </td>
            </tr>
            </table>
                        
    <%--<div id="outlinebox">
        
        <div id="content" style="width:100%">
            <hr width="100%" />
            
            

            <br />
            <table width="100%" >
            <tr>
                    <td width="160">
                        Shop ID</td>
                    <td>                     
                        <asp:TextBox ID="txtShopID" runat="server" CssClass="paymentinfo-text" 
                            Width="150px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="160">
                        Shop Pass</td>
                    <td>                     
                        <asp:TextBox ID="txtShopPass" runat="server" CssClass="paymentinfo-text" 
                            Width="150px" >hp2mq3ny</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td width="160">
                        Order ID</td>
                    <td>                     
                        <asp:TextBox ID="txtOrderNo" runat="server" CssClass="paymentinfo-text" 
                            Width="150px" ></asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <td width="160">
                        Amount to Charge</td>
                    <td>                     
                        <asp:TextBox ID="txtAmount" runat="server" CssClass="paymentinfo-text" 
                            Width="150px"></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <td>
                        Card Number <span class="star">*</span></td>
                    <td>
                        <asp:TextBox ID="txtCardNumber" runat="server" CssClass="paymentinfo-text" 
                            Width="150px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                            ControlToValidate="txtCardNumber" Display="Dynamic" CssClass="error-text" ErrorMessage="Required" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name on Card <span class="star">*</span></td>
                    <td>
                        <asp:TextBox ID="txtNameOnCard" runat="server" CssClass="paymentinfo-text" 
                            Width="150px"></asp:TextBox>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtNameOnCard" Display="Dynamic" CssClass="error-text" ErrorMessage="Required" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Expiration <span class="star">*</span></td>
                    <td>
                        &nbsp;<asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="ddlMonth" Display="Dynamic" CssClass="error-text" 
                            ErrorMessage="Month required"></asp:RequiredFieldValidator>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" 
                            ControlToValidate="ddlYear" Display="Dynamic" CssClass="error-text" 
                            ErrorMessage=" Year required"></asp:RequiredFieldValidator>
                    </td>
                </tr>
               <tr>
                    <td valign="top">
                        Card Security Code <span class="star">*</span></td>
                    <td align="left">
                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr><td width="50">
                        <asp:TextBox ID="txtCvv" runat="server" CssClass="paymentinfo-text" 
                            Width="35px"></asp:TextBox>
                        &nbsp; 
                            </td><td valign="top" class="cvv-text">
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtCvv" Display="Dynamic" CssClass="error-text" 
                            ErrorMessage="Required" ValidationGroup="Group1"></asp:RequiredFieldValidator>
                            A code that is printed (not imprinted) on the back of 
                                a credit card. It consist of 3 or 4 digits.
                        </td></tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        
                        &nbsp;
                        
                        
                        </td>
                </tr>
                
                
            </table><br />
            <hr width="100%" />
            <asp:Label ID="lblResult" runat="server" Width="100%" CssClass="standard-text"></asp:Label>
 
        </div>
        <div id="footer"></div>
    </div>--%>

</asp:Content>
