<%@ Page Title="商品管理システム＜ショップ内カテゴリ登録項目定義＞" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Shop.aspx.cs" Inherits="ORS_RCM.Shop" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
   <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
   <link rel="stylesheet" href="../../Styles/shop.css"  type="text/css"/>
  <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"  type="text/javascript"></script>
  <script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>
  <script type="text/javascript">
      function isNumberKey(evt) {
          var charCode = (evt.which) ? evt.which : event.keyCode
          if (charCode > 31 && (charCode < 48 || charCode > 57))
              return false;
          return true;
      } 
    </script>
  <script type="text/javascript" language="javascript">
        function Comma(Num) { //function to add commas to textboxes
            Num += '';
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            Num = Num.replace(',', ''); Num = Num.replace(',', ''); Num = Num.replace(',', '');
            x = Num.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1))
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            return x1 + x2;
        }
        function sumCalc() // function to remove comma and then add to third textbox
        {
            var txtfreeShipping = document.getElementById('<%=txtfreeShipping.ClientID %>').value.replace(/,/g, "");
        }
</script>
  </asp:Content>
  <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
  <div id="CmnContents">
  <div id="ComBlock">
     <div  id="edit_Confirm"  runat="server" class="setDetailBox iconSet iconCheck"  visible="false">
		<h1>ショップ編集 確認</h1>
    </div>
        <div id="register_Confirm"   runat="server"  class="setDetailBox iconSet iconCheck"   visible="false">
		    <h1>ショップ登録 確認</h1>
        </div>
        <div  id="edit"  runat="server"  class="setDetailBox iconSet iconEdit">
            <h1 runat="server" id="head"></h1>
        </div>
        <div  class="setDetailBox iconSet iconEdit">
            <p id="toTop" style="margin-bottom:-15px;"><a href="#CmnContents">▲TOP</a></p>
                <table class="shopCmnSet editTable">
                     <tr>
                        <td></td>
                        <td></td>
                        <td><asp:ValidationSummary ID="ValidationSummary1" runat="server" HeaderText="" EnableClientScript="true" ForeColor="Red" ValidationGroup="check" ShowMessageBox="false" DisplayMode="BulletList" ShowSummary="true"/></td>
                     </tr>
                     <tr>
                         <th style="z-index:-1">ショップ名&nbsp;<span>※必須</span></th>
                         <td><asp:TextBox ID="txtshopname" runat="server" CssClass="searchBox"></asp:TextBox>	
                                <asp:RequiredFieldValidator ID="rvfName" runat="server" style="margin-left:-16px" ControlToValidate="txtshopname" ErrorMessage="Shop_Name" Text="*"  ForeColor="#FF3300" ValidationGroup="check"></asp:RequiredFieldValidator>
                                <asp:Label runat="server" ID="lblShopName"   Visible="false"></asp:Label></td>
                     </tr>
                     <tr>
                        <th>出店モール&nbsp;<span>※必須</span></th> 
                        <td><asp:DropDownList ID="ddlmall" runat="server"  Width="100" Height="30"></asp:DropDownList>
                                <asp:Label runat="server" ID="lblmall" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>ショップID&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtshopid" runat="server"  CssClass="searchBox"></asp:TextBox>
                                <asp:Label runat="server" ID="lblShopId" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>FTPホスト&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtfhost" runat="server"></asp:TextBox>
                                <asp:Label runat="server" ID="lblftpHost" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>FTP アカウント&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtfacc" runat="server"  CssClass="searchBox"></asp:TextBox>
                                <asp:Label runat="server" ID="lblFtpAccount" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>パスワード&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtfpass" runat="server" CssClass="searchBox"></asp:TextBox>
                                <asp:Label runat="server" ID="lblFTPpassword" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>商品ページURL&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtshurl" runat="server"  CssClass="searchBox"></asp:TextBox>
                                <asp:Label runat="server" ID="lblshpurl"  Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>画像URL&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txturl" runat="server"  CssClass="searchBox"></asp:TextBox>
                                <asp:Label runat="server" ID="lblimgurl"  Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>ライブラリ画像FTPホスト&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtliburl" runat="server"></asp:TextBox>
                                <asp:Label ID="lblliburl" runat="server" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>ライブラリ画像FTPアカウント&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtlibacc" runat="server"></asp:TextBox>
                                <asp:Label ID="lbllibaacc" runat="server" Visible="false"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>ライブラリ画像FTPパスワード&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtlblpass" runat="server"></asp:TextBox>
                                <asp:Label ID="lbllibpass" runat="server" Visible="false"></asp:Label></td>
                    </tr>
                  <tr>
                        <th>ライブラリ画像ディレクトリ&nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtlibdirectory" runat="server"></asp:TextBox>
                                <asp:Label ID="lbllibdirectory" runat="server" Visible="false"></asp:Label></td>
                  </tr>
                  <tr>
                        <th>送料無料条件 &nbsp;<span>※必須</span></th>
                        <td><asp:TextBox ID="txtfreeShipping" runat="server"  onkeypress= "return isNumberKey(event)" onkeyup="javascript:this.value=Comma(this.value);"></asp:TextBox>
                                <asp:Label runat="server" ID="lblFreeshipping"  Visible="false"></asp:Label>
                                <asp:Label runat="server" ID="Label1"  Text="円以上(税抜)"></asp:Label></td>
                  </tr>
                  <tr>
                        <th>ユーザステータス</th>
                        <td>
                                <table style="width:60px;float:left; border:0;">
                                    <tr><asp:RadioButton ID="rdostatus" runat="server" Value="1" Text="有効" GroupName="rbdStatus"  Checked="true" ForeColor="Black" Font-Size="Medium" Width="60px"/>
                                           <asp:RadioButton ID="rdostatus1" runat="server" Text="無効" Value="0" GroupName="rbdStatus"  ForeColor="Black" Font-Size="Medium" Width="60px"/>
                                           <asp:Label ID="lblStatus" runat="server" Visible="false"></asp:Label>
                                    </tr> 
                                </table>
                        </td>
                  </tr>
                    <tr>
                        <th>Category</th>
                        <td>
                                <table style="width:60px;float:left; border:0">
                                    <tr><asp:RadioButton ID="rdb3" runat="server" Text="あり"  Value="1" GroupName="groupcategory"  Checked="true" ForeColor="Black" Font-Size="Medium" Width="60px"/>
                                           <asp:RadioButton ID="rdb4" runat="server" Text="なし" Value="0" GroupName="groupcategory"  ForeColor="Black" Font-Size="Medium" Width="60px"/>
                                           <asp:Label ID="lblcategory" runat="server" Visible="false"></asp:Label>
                                    </tr> 
                                </table>
                        </td>
                  </tr>
</table>
<div class="btn">
            <input type="submit" id="btnpopup" onclick="" runat="server"  value="確認画面へ"/>
            <asp:Button ID="btnConfirm_Save" runat="server" Text="確認画面へ"  style="margin-left:500px;"  ValidationGroup="check" onclick="btnsave_Click"   Visible="false"/>
</div>
</div>
</div>
</div>
</asp:Content>
