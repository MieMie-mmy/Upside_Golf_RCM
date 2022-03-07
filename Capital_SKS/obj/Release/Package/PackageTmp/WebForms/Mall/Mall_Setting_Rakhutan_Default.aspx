<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Mall_Setting_Rakhutan_Default.aspx.cs" Inherits="ORS_RCM.Mall_Setting_Rakhutan_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
     <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/style.css" rel="stylesheet" type="text/css" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<p id="toTop"><a href="#CmnContents">▲TOP</a></p>
    
    <div id="CmnContents">
	<div id="ComBlock">

<!-- Default Value -->
	    <div class="setDetailBox defaultSet iconSet iconRakuten">
        <h1>楽天デフォルト値編集<span>全て必須項目。ただし、ブランク可</span></h1>

<table class="shopCmnSet editTable"  style="border-bottom:0">

<tr>    
    <th>ショップ名</th>
    <td>
        <asp:Label ID="lblShopName" runat="server"></asp:Label></td>
    </tr>
    <tr>
                 <th>出品モール</th>
        <td style="padding-left:20px">
            <asp:Label ID="lblMall_Name" runat="server"></asp:Label></td>
        </tr>
        <tr>
        
        
        <th>送料</th>
            <td>
                <%--<asp:RadioButtonList ID="rdowarehouse" runat="server" RepeatDirection ="Horizontal">
                            <asp:ListItem Selected ="True"  Value="0">販売中 </asp:ListItem>
                            <asp:ListItem  Value="1">倉庫に入れる</asp:ListItem>
                            </asp:RadioButtonList>--%>
                <asp:RadioButton ID="rdopostage1" runat="server"  Text="送料別"  GroupName="rdoPostage"  Checked="true"/> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rdopostage2" runat="server"   Text="送料込"  GroupName="rdoPostage"/>
                <asp:Label ID="lblPostage" runat="server"  Visible="false"></asp:Label>
            </td>
            </tr>
            <tr>
            
                       <th>個別送料</th>
                <td style="padding-left:20px">
                    <asp:TextBox ID="txtship" runat="server"></asp:TextBox>
                    <asp:Label ID="lblShippingCost" runat="server"  Visible="false"></asp:Label>
                       </td>
                </tr>
                <tr>
                    <th>代引料</th>
                    <td>
                        <%--<asp:RadioButtonList ID="rdosearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected ="True"  Value="0">表示する  </asp:ListItem>
                                <asp:ListItem  Value="1">表示しない</asp:ListItem>
                                </asp:RadioButtonList>--%>
                        <asp:RadioButton ID="rdodelivery1" runat="server" GroupName="rdoDelivery"  Text="代引料別"  Checked="true"/>&nbsp;&nbsp;
                        <asp:RadioButton ID="rdodelivery2" runat="server" GroupName="rdoDelivery"  Text="代引料込"  />
                        <asp:Label ID="lblDaibikiryō" runat="server"  Visible="false"></asp:Label>
                    </td>
                    </tr>
                    <tr>
                            <th>倉庫指定</th>
                        <td>
                            <%--<asp:RadioButtonList ID="rdowarehouse" runat="server" RepeatDirection ="Horizontal">
                            <asp:ListItem Selected ="True"  Value="0">販売中 </asp:ListItem>
                            <asp:ListItem  Value="1">倉庫に入れる</asp:ListItem>
                            </asp:RadioButtonList>--%>
                           
                            <asp:RadioButton ID="rdowarehouse1" runat="server"   GroupName="rdoWarehouse"      Text="販売中 "  Checked="true"/>&nbsp;&nbsp;&nbsp;
                          
                          
                            <asp:RadioButton ID="rdowarehouse2" runat="server" GroupName="rdoWarehouse"  Text="倉庫に入れる" />
                            <asp:Label ID="lblWarehousespecified" runat="server"  Visible="false"></asp:Label>
                        </td>
                        </tr>
                        <tr>
                                             <th>サーチ非表示</th>
                            <td>
                                <%--<asp:RadioButtonList ID="rdosearch" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Selected ="True"  Value="0">表示する  </asp:ListItem>
                                <asp:ListItem  Value="1">表示しない</asp:ListItem>
                                </asp:RadioButtonList>--%>
                                <asp:RadioButton ID="rdosearch1" runat="server" GroupName="rdoSearch" Text="表示する" Checked="true" />&nbsp;&nbsp;
                                <asp:RadioButton ID="rdosearch2" runat="server" GroupName="rdoSearch" Text="表示しない"  />
                                <asp:Label ID="lblSearchhide" runat="server" Visible="false"></asp:Label>
                                             </td>
            
                            </tr>
                            <tr>
                                              <th>闇市パスワード</th>
                                <td style="padding-left:20px">
                                    <asp:TextBox ID="txtpassword" runat="server" ></asp:TextBox>
                                    <asp:Label ID="lblBlackmarketPassword" runat="server"  Visible="false"></asp:Label>
                                              </td>
                                </tr>


                                <tr>
                                              <th>二重価格文言管理番号</th>
                                    <td style="padding-left:20px">
                                       
                                        <asp:RadioButtonList ID="rdodualprice" runat="server" 
                                            RepeatDirection="Horizontal"  Width="380"  BorderStyle="NotSet" BorderWidth="0px">
                                        <asp:ListItem Selected="True"  Value="0">自動選択  </asp:ListItem>
                                        <asp:ListItem  Value="1">当店通常価格</asp:ListItem>
                                        <asp:ListItem  Value="2">メーカー希望小売価格</asp:ListItem>
                                        </asp:RadioButtonList>


                                    <asp:Label ID="lblDoublepricewordingControlNumber" runat="server"  Visible="False"></asp:Label>
                                    </td>

                                    </tr>



                                    <tr>

                                    <th>目玉商品</th>
                                    
                                    
                                    <td>
                                    
                                    
                                    
                                        <asp:TextBox ID="txtFeatureItem" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblfeatureItem" runat="server"></asp:Label>
                                    
                                    
                                    
                                    
                                    </td>
                                    
                                    
                                    
                                    
                                    
                                    </tr>





                                    
                                     <div class="btn">
                                         <input type="submit" id="btnpopup" onclick="" runat="server" style="width:200px"  value="確認画面へ"/>
                                        <asp:Button ID="btnConfirm_Save" runat="server" Text="登録する" onclick="btnsave_Click" 
                                             />
                                        </div>
                                        </td>
                                      
</table>
</div>
</div>
</div>

</asp:Content>
