<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mall_Setting_Ponpare_Fixed.aspx.cs" Inherits="ORS_RCM.Mall_Setting_Ponpare_Fixed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
 
  <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/shop.css" type="text/css"/>

   <link href="../../Styles/Item-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Scripts/jquery-1.3.min.js" rel="stylesheet" type="text/css" />
	<link href="../../Scripts/jquery.droppy.js" rel="stylesheet" type="text/css" />



<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8)
            return true;
        else return false;
    } 
</script>




</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <p id="toTop"><a href="#CmnContents">▲TOP</a></p>
<div id="CmnContents">
	            <div id="ComBlock">

<!-- Fixed Value -->
	     <div class="setDetailBox shopCmnSet defaultSet iconSet iconPon">


                <h1>ポンパレモール固定値編集<span>全て必須項目。ただし、ブランク可</span></h1>

<table class="shopCmnSet editTable">
<tr>
                       <th>ショップ名</th>

<td>

    <asp:Label ID="lblshopname" runat="server"></asp:Label></td>
</tr>

        <tr>
    <th>出品モール</th>

    <td>
        <asp:Label ID="lblmall" runat="server" Text=""></asp:Label></td>
    </tr>

    <tr>


        <th>消費税</th>
       <%-- <td style="border">
            <asp:RadioButtonList ID="rdoconsumptiontax" runat="server" 
                RepeatDirection="Horizontal" BorderStyle="None" Width="200px">
            <asp:ListItem Value="0">消費税込み </asp:ListItem>
            <asp:ListItem Value="1">消費税別</asp:ListItem>
            </asp:RadioButtonList>
            <asp:Label ID="lblcontax" runat="server" Text="" Visible ="false"></asp:Label>
        </td>--%>

         <td>
         
         
         
             <asp:RadioButton ID="rdoconsumptiontax1" runat="server"   Text="消費税込み"   GroupName="consumption_tax"/>
             <asp:RadioButton ID="rdoconsumptiontax2" runat="server"  Text="消費税別"  GroupName="consumption_tax" />
              <asp:Label ID="lblcontax" runat="server"  Visible ="false"></asp:Label>
         
         
         
         </td>

        </tr>
        <tr>
                          <th>  独自送料グループ(1)</th>
           

            
             <td>   <asp:TextBox ID="txtshipg1" runat="server" CssClass="input_text"></asp:TextBox>
                 <asp:Label ID="lblship1" runat="server" Text=""  Visible="false"></asp:Label>
             </td>
            </tr>

            <tr>

                <th>独自送料グループ(2)</th>
                <td>
                    <asp:TextBox ID="txtshipg2" runat="server" CssClass="input_text"></asp:TextBox>
                    <asp:Label ID="lblship2" runat="server" Text="" Visible="false"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <th>のし対応</th> 
                    <td>
                       <%-- <asp:RadioButtonList ID="rdoexpancope" runat="server" 
                            RepeatDirection ="Horizontal" BorderStyle="None" Width="370px">
                        <asp:ListItem  Value="1">対応しない </asp:ListItem>
                        <asp:ListItem  Value="2">対応する</asp:ListItem>
                       
                        </asp:RadioButtonList>
--%>
                        <asp:RadioButton ID="rdoexpancope1" runat="server" GroupName="doexpancope"  Text="対応しない"/>
                        <asp:RadioButton ID="rdoexpancope2" runat="server" GroupName="doexpancope"   Text="対応する"/>
                        <asp:Label ID="lblexpcope" runat="server" Text="" Visible ="false"></asp:Label>
                    </td>
                    </tr>

                    <tr style="border:0">
                    
                 <th>注文ボタン</th>

                        <td>
                            <%--<asp:RadioButtonList ID="rdoorder" runat="server" RepeatDirection="Horizontal" 
                                BorderStyle="None" Width="380px"   BorderColor="White">
                            <asp:ListItem  Value="0">注文ボタンをつけない </asp:ListItem>
                            <asp:ListItem  Value="1">注文ボタンをつける</asp:ListItem>
                            </asp:RadioButtonList>--%>


                            <asp:RadioButton ID="rdoorder1" runat="server"   Value="注文ボタンをつけない"  GroupName="rdoorder" Text="注文ボタンをつけない"/>
                            <asp:RadioButton ID="rdoorder2" runat="server"  Value="注文ボタンをつける"  GroupName="rdoorder" Text="注文ボタンをつける" />


                            <asp:Label ID="lblorder" runat="server" Text="" Visible ="false"></asp:Label>
                        </td>
                        </tr>

                        <tr>
                            <th>商品問い合わせボタン</th>
                            <td>
                             
                                <asp:RadioButton ID="rdoinquery1" runat="server"   Text="お問合せリンクをつけない"    Value="0" GroupName="product_inquire"/>
                                <asp:RadioButton ID="rdoinquery2" runat="server"   Text="お問合せリンクをつける"  Value="1"  GroupName="product_inquire"/>
                             
                                <%--<asp:RadioButtonList ID="rdoinquery" runat="server" 
                                    RepeatDirection="Horizontal" BorderStyle="None" Width="380px" BorderWidth = 0>
                                <asp:ListItem  Value="0">お問合せリンクをつけない </asp:ListItem>
                                <asp:ListItem  Value="1">お問合せリンクをつける</asp:ListItem>
                                </asp:RadioButtonList>--%>

                                <asp:Label ID="lblinquery" runat="server" Text="" Visible="false"></asp:Label>
                            </td>
                            </tr>

                            <tr>
                                                    <th>注文受付数</th>
                                <td>
                                    <asp:TextBox ID="txtnoaccept" runat="server" onkeypress="return isNumberKey(event)"  CssClass="input_text"></asp:TextBox>
                                    <asp:Label ID="lblnoacc" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                </tr>

                                <tr>

                            <th>在庫タイプ</th>
                                    <td>
                                       <%-- <asp:RadioButtonList ID="rdostocktype" runat="server" 
                                            RepeatDirection="Horizontal" BorderStyle="None" BorderWidth="0px" 
                                            Width="270px">
                                        <asp:ListItem  Value="1">通常在庫  </asp:ListItem>
                                        <asp:ListItem  Value="2">SKU在庫 </asp:ListItem>
                                        <asp:ListItem  Value="3">設定なし</asp:ListItem>
                                        </asp:RadioButtonList>--%>
                                        <asp:RadioButton ID="rdostocktype1" runat="server"   Text="通常在庫"   Value="1"   GroupName="rdostocktype"  />
                                        <asp:RadioButton ID="rdostocktype2" runat="server"   Text="SKU在庫"   Value="2"   GroupName="rdostocktype"  />
                                        <asp:RadioButton ID="rdostocktype3" runat="server"  Text="設定なし"    Value="3"   GroupName="rdostocktype"  />

                                        <asp:Label ID="lblstotype" runat="server" Text="" Visible="false"></asp:Label>
                                    </td>
                                    </tr>


                                    <tr>
                                  <th>在庫数</th>
                          
                                    <td>
                                        <asp:TextBox ID="txtstockquantity" runat="server" onkeypress="return isNumberKey(event)"  CssClass="input_text"></asp:TextBox>
                                        <asp:Label ID="lblstockqty" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>


                                    <tr>
                                        <th>在庫数表示</th>
                                        <td>
                                            <asp:RadioButtonList ID="rdostockdisplay" runat="server" RepeatColumns="2" 
                                                RepeatDirection="Horizontal" Width="500px" BorderColor="White" 
                                                BorderWidth="0px">
                                            <asp:ListItem  Value="1">在庫数を表示 </asp:ListItem>
                                            <asp:ListItem  Value="2">在庫数が●個以下になったら「△」の印を表示 </asp:ListItem>
                                             <asp:ListItem  Value="3">在庫数を表示しない</asp:ListItem>
                                             <asp:ListItem  Value="4">在庫設定しない</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="lblstdisplay" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                        </tr>


                                        <tr>
                                                  
                                                  
                                             <th>SKU横軸項目名</th>
                                            <td>
                                                <asp:TextBox ID="txthitemname" runat="server" CssClass="input_text"></asp:TextBox>
                                                <asp:Label ID="lbthname" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>SKU縦軸項目名</th>
                                            
                                            <td>
                                                <asp:TextBox ID="txtvitemname" runat="server" CssClass="input_text"></asp:TextBox>
                                                <asp:Label ID="lblviname" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>

                                            <tr>
                                             <th>SKU在庫用残り表示閾値</th>
                                      
                                             <td> <asp:TextBox ID="txtremainstock" runat="server" CssClass="input_text"></asp:TextBox>
                                                 <asp:Label ID="lblrestock" runat="server" Text="" Visible="false"></asp:Label>
                                             </td>  
                                            </tr>

                                            <tr>
                                             <th>JANコード</th>
                                                <td>
                                                    <asp:TextBox ID="txtjancode" runat="server" CssClass="input_text"></asp:TextBox>
                                                    <asp:Label ID="lbljancode" runat="server" Text="" Visible="false"></asp:Label>
                                                    </td>
                                                </tr>
                                
                                  <div class="btn">
                                      <%--   <asp:Button ID="btnsave" runat="server" Text="確認画面へ" onclick="btnsave_Click"  />--%>
                                         </div> 
                                        
</div>
</div>
</div>

<div class="btn">
		<div class="userRole">
			<input type="submit" id="btnpopup" onclick="" runat="server" style="width:200px"  value="確認画面へ" />
			<asp:Button runat="server" Width="130px" ID="btnsave" OnClick="btnsave_Click" Text="確認画面へ" Visible="false" />
		</div>     
	</div>
</asp:Content>
