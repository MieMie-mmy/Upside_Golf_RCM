<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mall_Setting_Rakhutan_Fixed.aspx.cs" Inherits="ORS_RCM.Mall_Setting_Rakhutan_Fixed" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>
 
<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
<link href="../../Styles/style.css" rel="stylesheet" type="text/css" />


 <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
      <link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />

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
            $("#<%=txtbookorder.ClientID %>").datepicker(
	    {
	        showOn: 'button',
	        dateFormat: 'dd/M/yy',
	        buttonImageOnly: true,
	        buttonImage: '/images/calendar.gif',
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

       

  
<!-- Default Value -->
<div class="setDetailBox shopCmnSet defaultSet iconSet iconRakuten">


		                         <h1>楽天固定値編集<span>全て必須項目。ただし、ブランク可</span></h1>

               <table class="editTable">
                <tr>
                
              
               <th>ショップ名</th>
                   
    
                    <td>
                    <asp:Label ID="lblShopName" runat="server"  ></asp:Label>
                        
                     </td>
                 </tr>
         
         
          <tr>
    
              <th>出品モール</th>
        <td>
            <asp:Label ID="lblMallName" runat="server"></asp:Label>
              </td>
        </tr>


        <tr>
      
      <th>タグID</th>
           
           
            <td>
                <asp:TextBox ID="txttagiD" runat="server"></asp:TextBox>
                <asp:Label ID="lblTagID" runat="server"  Visible="False" ></asp:Label>
                </td>
            </tr>

            <tr style="border-style:none">
               
                    <th>消費税</th>
                <td>



            
                    <asp:RadioButton ID="rdoConsumption1" runat="server"   Text="消費税込み" GroupName="rdoConsumption" Checked="true"/>
                    <asp:RadioButton ID="rdoConsumption2" runat="server" Text="消費税別"  GroupName="rdoConsumption" />

            
                    <asp:Label ID="lblConsumptionTax" runat="server"  Visible="False"></asp:Label>

            
                </td>

                </tr>
                <tr>

                     <th>送料区分１</th>
                    <td>
                        <asp:TextBox ID="txtship1" runat="server"></asp:TextBox>
                        <asp:Label ID="lblShippingCategory1" runat="server"  Visible="false"></asp:Label>
                     </td>
                    </tr>

                    <tr>
               
                 <th>送料区分２</th>
                          <td>
                            <asp:TextBox ID="txtship2" runat="server"></asp:TextBox>
                              <asp:Label ID="lblShippingCategory2" runat="server"  Visible="false"></asp:Label>
                        </td>
                        </tr>



                        <tr>


                    <th> 商品情報レイアウト</th>

                        
                        <td>
                                  
                            <asp:TextBox ID="txtproductinfoLayout" runat="server"></asp:TextBox>
                            <asp:Label ID="lblproInfo" runat="server"  Visible="false"></asp:Label>
                        </td>
                      
                        
                        
                        </tr>








              
                     <tr style="border-style:none">
                     <th>注文ボタン</th>
                            
                             
                            
                            <td>
                                
                                <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>


                                <asp:RadioButton ID="rdoOrder1" runat="server" GroupName="rdoOrder" Text="ボタンをつけない"/>
                                <asp:RadioButton ID="rdoOrder2" runat="server" Checked="true"  GroupName="rdoOrder" Text="通常注文ボタンをつける"/>
                                <asp:RadioButton ID="rdoOrder3" runat="server" GroupName="rdoOrder" Text="予約注文ボタンをつける" />


                                <asp:Label ID="lblOrderButton" runat="server"  Visible="false"></asp:Label>


                            </td>



                            </tr>


                            <tr>
                            
                           
                            <th class="style1">資料請求ボタン</th>
                                <td class="style1">
                                    <%--     <div class="btn"><asp:Button ID="btnSave" runat="server" Text="確認画面へ" onclick="btnSave_Click"  Width="300"/>
                                                                                                  </div>--%>


                                    
                                            <asp:RadioButton ID="rdorequest1" runat="server" GroupName="rdoRequest" 
                        Text="ボタンをつけない"/>
                                            <asp:RadioButton ID="rdorequest2" runat="server" GroupName="rdoRequest" 
                        Text="ボタンをつける"  Checked="true"/>


                                    <asp:Label ID="lblRequest" runat="server"  Visible="false"></asp:Label>


                                </td>
                                </tr>

                                <tr>
                                    <th>商品問い合わせボタン</th>
                                       <td>
                                           <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>


                                            <asp:RadioButton ID="rdoProduct_Inquire1" runat="server" GroupName="rdoProduct_Inquire" 
                        Text="ボタンをつけない"  />
                                            <asp:RadioButton ID="rdoProduct_Inquire2" runat="server" GroupName="rdoProduct_Inquire" 
                        Text="ボタンをつける"  Checked="true"/>
                                           <asp:Label ID="lblProductinquiry" runat="server"  Visible="false"></asp:Label>
                                    </td>
                                    </tr>
                                   
                                   
                                    <tr>
                             
                                    <th>再入荷お知らせボタン</th>
                                        <td>
                                           
                                            <%--     <div class="btn"><asp:Button ID="btnSave" runat="server" Text="確認画面へ" onclick="btnSave_Click"  Width="300"/>
                                                                                                  </div>--%>




                                            <asp:RadioButton ID="rdocoming" runat="server" GroupName="rdoComingSoon" 
                        Text="ボタンをつけない" />
                                            <asp:RadioButton ID="rdocoming2" runat="server" GroupName="rdoComingSoon" 
                        Text="ボタンをつける"   Checked="true"/>




                                            <asp:Label ID="lblComingSoon" runat="server" Visible="false"></asp:Label>
                                            



                                        </td>
                                    </tr>
                                   
                                        <tr>

                              <th>モバイル表示</th>
                                            <td>
                                                <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>
                                                <asp:RadioButton ID="rdomobile1" runat="server"  GroupName="rdoMobile" Checked="true" Text="表示する"/>
                                                <asp:RadioButton ID="rdomobile2" runat="server"  GroupName="rdoMobile" Text="表示しない" />
                                                <asp:Label ID="lblMobileDisplay" runat="server" Visible="false"></asp:Label>
                                            </td>
                                      </tr>
                               
                                            <tr style="border-style:none">
                                      
                                        <th>のし対応</th>
                                                <td>
                                                    <%--     <div class="btn"><asp:Button ID="btnSave" runat="server" Text="確認画面へ" onclick="btnSave_Click"  Width="300"/>
                                                                                                  </div>--%>
                                                    <asp:RadioButton ID="rdoexpand1" runat="server"  GroupName="Works_corresponding"  Text="対応しない"/>
                                                    <asp:RadioButton ID="rdoexpand2" runat="server"  GroupName="Works_corresponding"  Text="対応する"  Checked="true"/>
                                                    <asp:Label ID="lblworkCorresponding" runat="server" Visible="false"></asp:Label>
                                                </td>
                                                </tr>

                                                <tr>

                                              <th>動画</th>
                                                     <td>
                                                        <asp:TextBox ID="txtanimation" runat="server" ></asp:TextBox>
                                                         <asp:Label ID="lblAnimation" runat="server"  Visible="false"></asp:Label>
                                                    </td>
                                                    </tr>

                                                <tr>
                                                <th>注文受付数</th>
                                                        <td>
                                                            <asp:TextBox ID="txtaccept" runat="server" ></asp:TextBox>
                                                            <asp:Label ID="lblnoOfAcceptance" runat="server" Visible="false"></asp:Label>
                                                    </td>
                                                        </tr>
                                                    <tr>
                                            <th>在庫タイプ</th>
                                                            <td>
                                                                <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>
                                                                <asp:RadioButton ID="rdoStockType1" runat="server"  Text="在庫設定しない"    GroupName="StockType"/>
                                                                <asp:RadioButton ID="rdoStockType2" runat="server" Text="通常在庫設定"  GroupName="StockType"/>
                                                                <asp:RadioButton ID="rdoStockType3" runat="server" Text="項目選択肢別在庫設定" GroupName="StockType" Checked="true"/>
                                                                <asp:Label ID="lblStockType" runat="server" visible="false"></asp:Label>
                                                            </td>
                                                            </tr>

                                                            <tr>
                                                                   <th>在庫数</th>
                                                                <td>
                                                                    <asp:TextBox ID="txtstockno" runat="server"  
                                                                        onkeypress="return isNumberKey(event)"></asp:TextBox>
                                                                    <asp:Label ID="lblStockQuantity" runat="server"  Visible="false"></asp:Label>
                                                                   </td>
                                                                </tr>

                                                                <tr>

                                                      <th>在庫数表示</th>
                                                                    <td>
                                                                        <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>
                                                                        <asp:RadioButton ID="rdostockquantity1" runat="server"  Text="残り在庫数表示しない" GroupName="StockQuantity"/>
                                                                        <asp:RadioButton ID="rdostockquantity2" runat="server" Text="残り在庫数表示する"  Checked="true" GroupName="StockQuantity" />
                                                                        <asp:Label ID="lblStocknoDisplay" runat="server" visible="false"></asp:Label>
                                                              </td>
                                                              </tr>
                                                             
                                                                    <tr>
                                                                               <th> 項目選択肢別在庫用横軸項目名</th>
                                                                                    <td>
                                                                            <asp:TextBox ID="txthozitemname" runat="server"></asp:TextBox>
                                                                                        <asp:Label ID="lblHorizonalItemName" runat="server"   Visible="false"></asp:Label>
                                                                               </td>
                                                                        </tr>
                                                                        <tr>
                                                                             <th>項目選択肢別在庫用縦軸項目名</th>
                                                                              <td>
                                                                               <asp:TextBox ID="txtverticalitemname" runat="server"></asp:TextBox>
                                                                                  <asp:Label ID="lblVerticalItemName" runat="server"  Visible="false"></asp:Label>
                                                                               </td> 
                                                                            </tr>

                                                                            <tr>
                                                                                           <th>項目選択肢別在庫用残り表示閾値</th>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtremainstock" runat="server"></asp:TextBox>
                                                                                    <asp:Label ID="lblRemainStock" runat="server"  Visible="false"></asp:Label>
                                                                                           </td>
                                                                                </tr>
                                                                      
                                                                            <tr>
                                                                                  <th>RAC番号</th>
                                                                                    <td>
                                                                                        <asp:TextBox ID="txtRACno" runat="server"></asp:TextBox>
                                                                                        <asp:Label ID="lblRacno" runat="server" Visible="false"></asp:Label>
                                                                                  </td>
                                                                                    </tr>
                                                                                    <tr>
                                                                                                    <th>カタログID</th>
                                                                                        <td>
                                                                                            <asp:TextBox ID="txtcatalogID" runat="server"></asp:TextBox>
                                                                                            <asp:Label ID="lblCatalogID" runat="server"  Visible="False"></asp:Label>
                                                                                                    </td>
                                                                                        </tr>

                                                                                        <tr>
                                                                                               <th>在庫戻しフラグ"</th>
                                                                                            <td>
                                                                                                <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>



                                                                                                <asp:RadioButton ID="rdoFlagback1" runat="server"  Text="利用しない" GroupName="rdoFlagback" />
                                                                                               <asp:RadioButton ID="rdoFlagback2" runat="server"  Text="利用する" GroupName="rdoFlagback" Checked="true"/>
                                                                                                <asp:Label ID="lblFlagBack" runat="server"  Visible="false"></asp:Label>
                                                                                          </td>
                                                                                            </tr>
                                                                                            <tr>
                                                                                                    <th>在庫切れ時の注文受付</th>
                                                                                                <td>
                                                                                                    
                                                                                              <%--     <div class="btn"><asp:Button ID="btnSave" runat="server" Text="確認画面へ" onclick="btnSave_Click"  Width="300"/>
                                                                                                  </div>--%>


                                                                                                    <asp:RadioButton ID="rdoAccept1" runat="server"  Text="受け付けない" GroupName="rdoAccept_atOutofproduct"/>
                                                                                                    <asp:RadioButton ID="rdoAccept2" runat="server" Text="受け付ける"    Checked="true"   GroupName="rdoAccept_atOutofproduct" />



                                                                                                    <asp:Label ID="lblAcceptAtOutofStockTime" runat="server" Visible="False"></asp:Label>



                                                                                                </td>
                                                                                         
                                                                                                </tr>

                                                                                                <tr>
                                                                                                    <th>在庫あり時納期管理番号</th>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtdeliveryctrno" runat="server"></asp:TextBox>
                                                                                                        <asp:Label ID="lblDeliveryControlNo" runat="server"  Visible="false"></asp:Label>
                                                                                                    </td>
                                                                                                    </tr>

                                                                                                    
                                                                                                <tr>
                                                                                                    <th>在庫切れ時納期管理番号</th>
                                                                                                    <td>
                                                                                                        <asp:TextBox ID="txtdeliveryctrno_outofstock" runat="server"></asp:TextBox>
                                                                                                        <asp:Label ID="lbldeliveryctrno_outofstock" runat="server"  Visible="false"></asp:Label>
                                                                                                    </td>
                                                                                                    </tr>


                                                                                                    
                                                                                                       <tr>
                                                                                                   <th>予約商品発売日</th>
                                                                                                       
                                                                                                       <asp:UpdatePanel runat="server" ID="uppnl1">
                                                                                                       <ContentTemplate>
                                                                                                                     <td>
                                                                                                                         <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>
                                                                                                            <asp:TextBox ID="txtbookorder" runat="server" ReadOnly="true"></asp:TextBox>   &nbsp;&nbsp;
                                                                                                            
                                                                                                             <asp:ImageButton ID="ImageButton1" runat="server" Width="19px" Height="16px" 
                                                                                                              ImageUrl="~/images/clear.png" onclick="ImageButton1_Click"  />

                                                                                        
                                                                                                      <asp:Label ID="lblReservationReleasedDate" runat="server"  Visible="false"></asp:Label>
                                                                                                         
                                                                                                            </td>
                                                                                                            </ContentTemplate>
                                                                                                            </asp:UpdatePanel>
                                                                                                        </tr>
                                                                                                        
                                                                                                        <tr>
                                                                                                                <th>ヘッダー・フッター・レフトナビ"</th>
                                                                                                            <td>
                                                                                                                <asp:TextBox ID="txthfooter" runat="server"></asp:TextBox>
                                                                                                                <asp:Label ID="lblheaderfooter" runat="server"  Visible="false"></asp:Label>
                                                                                                                </td>
                                                                                                            </tr>
                                                                                                            <tr>
                                                                                                                <th>表示項目の並び順</th>
                                                                                                                <td>
                                                                                                                    <asp:TextBox ID="txtdisplayorder" runat="server"></asp:TextBox>
                                                                                                                    <asp:Label ID="lblDisplayOrder" runat="server"  Visible="false"></asp:Label>
                                                                                                                </td>
                                                                                                                </tr>

                                                                                                                <tr>
                                                                                                                  <th>共通説明文(小)</th>
                                                                                                                    <td>
                                                                                                                        <asp:TextBox ID="txtcomon1" runat="server"></asp:TextBox>
                                                                                                                        <asp:Label ID="lblCommomDescri" runat="server"  Visible="false"></asp:Label>
                                                                                                                    </td>
                                                                                                                    </tr>

                                                                                                                    <tr>
                                                                                                                 
                                                                                                                        <th>共通説明文(大)</th>
                                                                                                                        <td>
                                                                                                                            <asp:TextBox ID="txtcommon2" runat="server" ></asp:TextBox>
                                                                                                                            <asp:Label ID="lblCommonDescrLarge" runat="server"  Visible="false"></asp:Label>
                                                                                                                        </td>
                                                                                                                        </tr>
                                                                                                                        <tr style="border-style:none">
                                                                                                                            <th>レビュー本文表示</th>
                                                                                                                            
                                                                                                                             <td>
                                                                                                                                <%--<asp:RadioButtonList ID="rdorewiewtext" runat="server" 
                                                                                                                                     RepeatDirection="Horizontal" BorderStyle="None" >
                                                                                                                                <asp:ListItem>表示しない </asp:ListItem>
                                                                                                                                <asp:ListItem Selected="True">表示する </asp:ListItem>
                                                                                                                                <asp:ListItem>デザイン設定での設定を使用</asp:ListItem>
                                                                                                                                </asp:RadioButtonList>--%>

                                                                                                                                     <asp:RadioButton ID="rdoReview1" runat="server" GroupName="rdorewiewtext" Text="表示しない"/>
                                                                                                                                     <asp:RadioButton ID="rdoReview2" runat="server" Checked="true"  GroupName="rdorewiewtext" Text="表示する"/>
                                                                                                                                    <asp:RadioButton ID="rdoReview3" runat="server"  GroupName="rdorewiewtext" Text="デザイン設定での設定を使用"/>

                                                                                                                                 <asp:Label ID="lblReviewTextDisplay" runat="server" Visible="false"></asp:Label>

                                                                                                                            </td>


                                                                                                                            </tr>
                                                                                                                            <tr>
                                                                                                                               <th>海外配送管理番号</th>
                                                                                                                                <td>
                                                                                                                                    <asp:TextBox ID="txtoversea" runat="server"></asp:TextBox>
                                                                                                                                    <asp:Label ID="lblOverseaDeliveryCtrlNo" runat="server"  Visible="False"></asp:Label>
                                                                                                                                </td>
                                                                                                                                </tr>

                                                                                                                                <tr>
                                                                                                                             <th>サイズ表リンク</th>
                                                                                                                                    <td>
                                                                                                                                        <asp:TextBox ID="txtsizechart" runat="server"></asp:TextBox>
                                                                                                                                        <asp:Label ID="lblSizeChartLink" runat="server"  Visible="false"></asp:Label>
                                                                                                                                    </td>
                                                                                                                                    </tr>
                                                                                                                                    <tr>

                                                                                                                           <th>医薬品説明文</th>
                                                                                                                                        <td>
                                                                                                                                            <asp:TextBox ID="txtdrugdesc" runat="server" ></asp:TextBox>
                                                                                                                                            <asp:Label ID="lblDrugDescript" runat="server"  Visible="false"></asp:Label>
                                                                                                                                        </td>
                                                                                                                                        </tr>
                                                                                                                                        <tr>
                                                                                                                  <th>医薬品注意事項</th>
                                                                                                                                            <td>
                                                                                                                                                <asp:TextBox ID="txtdrugnote" runat="server" ></asp:TextBox>
                                                                                                                                                <asp:Label ID="lblDrugNote" runat="server" Visible="false"></asp:Label>
                                                                                                                                            </td>
                                                                                                                                            </tr>

                                                                                                    
                                                                                                         <tr>
                                                                                                                                          
                                                                                                  
                                                                                                  <div class="btn">
                                                                                                  
                                                                                                  
                                                                                                     <input type="submit" id="btnpopup" onclick="" runat="server" style="width:200px" value="確認画面へ"/>
                                                                                                  <asp:Button ID="btnConfirm_Save" runat="server" Text="確認画面へ" 
                                                                                                          onclick="btnSave_Click"/>
                                                                                                  </div>


                                                                                                                                                
                                                                                                    </tr>
</table>

</div>
</div>
</div>


</asp:Content>
