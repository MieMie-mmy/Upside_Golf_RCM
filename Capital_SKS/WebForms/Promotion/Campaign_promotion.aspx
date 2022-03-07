<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Campaign_promotion.aspx.cs" Inherits="ORS_RCM.Campaign_promotion" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadContent" runat="server">
    <meta charset="UTF-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <!--[if lt IE 9]>
<script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
<![endif]-->

    <link rel="stylesheet" href="../../Styles/base.css" />
    <link rel="stylesheet" href="../../Styles/common.css" />
    <link rel="stylesheet" href="../../Styles/manager-style.css" />
    <link rel="stylesheet" href="../../Styles/promotion.css " />

    <script src="../../Scripts/calendar1.js" type="text/javascript"></script>
    <link href="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
    <link href="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" />

    <script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>

    <title>商品管理システム＜プロモーション編集</title>

    <script type="text/javascript" language="javascript">
        function pageLoad(sender, args) {
            $(document).ready(function () {
                $("#<%=txtPeriod_From.ClientID %>").datepicker(
                    {
                        showOn: 'button',
                        dateFormat: 'yy/mm/dd ',
                        buttonImageOnly: true,
                        buttonImage: '../../images/calendar.gif',
                        changeMonth: true,
                        changeYear: true,
                        yearRange: "1900:2030",
                    }
                );
                $(".ui-datepicker-trigger").mouseover(function () {
                    $(this).css('cursor', 'pointer');
                });
            });

            $(document).ready(function () {
                $("#<%=txtPeriod_To.ClientID %>").datepicker(
                    {
                        showOn: 'button',
                        dateFormat: 'yy/mm/dd',
                        buttonImageOnly: true,
                        buttonImage: '../../images/calendar.gif',
                        changeMonth: true,
                        changeYear: true,
                        yearRange: "1900:2030",
                    });
                $(".ui-datepicker-trigger").mouseover(function () {
                    $(this).css('cursor', 'pointer');
                });
            });
        }

        function ShowOption(SourceID) {
            var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
            hidSourceID.value = SourceID;
            //declare a string variable
            var retval = "";
            //show modal dialog box and collect its return value
            retval = window.showModalDialog('../Item/Item_Option_Select1.aspx', window, 'dialogHeight:1000px; dialogWidth:1000px; dialogLeft:200px; dialogRight :200px; dialogTop:50px; help:no; unadorned:no; resizable:no; status:no; scroll:yes; minimize:no; maximize:yes;modal=yes;center=yes;');
        }
    </script>

    <script type="text/javascript">
        function isNumberKeys(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode;
            if (charCode == 13)
                return false;
            else return true;
        }
    </script>

    <script type="text/javascript">
        function ShowItemList(ctrl) {
            var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
        hidSourceID.value = ctrl.id;
        var left = (screen.width / 2) - (600 / 2);
        var top = (screen.height / 2) - (500 / 2);
        //declare a string variable
        var retval = "";
        //show modal dialog box and collect its return value
        retval = window.open('../Promotion/Product_Search_Popup.aspx', window,'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
    }
    </script>

    <script type="text/javascript">
        function ShowDialog(imagetype, SourceID) {
            var hidSourceID = document.getElementById("<%=CustomHiddenField.ClientID%>");
            hidSourceID.value = SourceID.id;
            //declare a string variable
            var retval = "";
            var left = (screen.width / 2) - (600 / 2);
            var top = (screen.height / 2) - (500 / 2);
            //show modal dialog box and collect its return value
            retval = window.open('../Promotion/FileUpload_Dialog[Campaign_image].aspx?Image_Type=' + imagetype, window, 'status=1,width=600,height=500,scrollbars=1,top=' + top + ',left=' + left);
        }
    </script>

</asp:Content>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:HiddenField ID="CustomHiddenField" runat="server" />
    <asp:HiddenField ID="hfperiod" runat="server" />
    <asp:HiddenField ID="hfperioto" runat="server" />
    <div id="CmnContents">
        <div id="ComBlock" style="margin-top: 60px;">
            <div class="setListBox iconSet iconEdit" runat="server" id="test">
                <h1 runat="server" id="head">キャンペーン登録</h1>
                <div class="cmnEdit prmCmnSet prmEntry inlineSet">
                    <div class="block1">
                        <dl>
                            <dt title="Campaign_ID">キャンペーンID</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtCampaignID" onkeypress="return isNumberKeys(event)" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                    ControlToValidate="txtCampaignID" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </dd>
                            <asp:Label ID="lblCampaignID" runat="server" Visible="true"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="Campaign_Name">キャンペーン名</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtCampaign_Name" onkeypress="return isNumberKeys(event)" />
                            </dd>

                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                ControlToValidate="txtCampaign_Name" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            <asp:Label ID="lblCamName" runat="server" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="Campaign_URL[PC]">キャンペーンURL</dt>
                            <dd>
                                <span>PC用</span>
                                <asp:TextBox runat="server" ID="txtCampaign_url" Height="23px" onkeypress="return isNumberKeys(event)" />
                            </dd>
                            <asp:Label ID="lblCampaignpc" runat="server" Visible="false"></asp:Label>
                            <dd><span>スマホ用</span>
                                <asp:TextBox runat="server" ID="txtcampaignSmrt_url" onkeypress="return isNumberKeys(event)" />
                            </dd>
                            <asp:Label ID="lblCampaign_smart" runat="server" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="Campaign Guidelines">キャンペーン内容</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtCampaign_Guideline" TextMode="MultiLine" /></dd>
                            <asp:Label ID="lbltxtCam_Guideline" runat="server" Text="" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="E-mail magazine event banner">メールマガジンイベントバナー</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtemailmagzine1" Width="150px" onkeypress="return isNumberKeys(event)"></asp:TextBox>

                                <asp:Label ID="lblemailMagzine1" runat="server" Text="" Visible="false" />

                                <asp:TextBox runat="server" ID="txtemailmagzine2" Width="150px" onkeypress="return isNumberKeys(event)"></asp:TextBox>
                                <asp:Label ID="lblemailMagzine2" runat="server" Text="" Visible="false"></asp:Label>

                                <asp:TextBox runat="server" ID="txtemailmagzine3" Width="150px" onkeypress="return isNumberKeys(event)"></asp:TextBox>
                                <asp:Label ID="lblemailMagzine3" runat="server" Visible="false"></asp:Label>
                            </dd>
                        </dl>
                    </div>
                    <div class="block2">
                        <dl>
                            <dt>キャンペーン種別</dt>
                            <dd>
                                <asp:ListBox ID="lstCampaignType" runat="server" Height="150px">
                                    <asp:ListItem Value="0" Text="商品別ポイント"></asp:ListItem>
                                    <asp:ListItem Value="1" Text="店舗別ポイント"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="商品別クーポン"></asp:ListItem>
                                    <asp:ListItem Value="3" Text="送料"></asp:ListItem>
                                    <asp:ListItem Value="4" Text="即日出荷"></asp:ListItem>
                                    <asp:ListItem Value="5" Text="予約"></asp:ListItem>
                                    <asp:ListItem Value="6" Text="事前告知"></asp:ListItem>
                                    <asp:ListItem Value="7" Text="シークレットセール"></asp:ListItem>
                                    <asp:ListItem Value="8" Text="プレゼントキャンペーン"></asp:ListItem>
                                </asp:ListBox>
                            </dd>
                        </dl>
                    </div>
                    <div class="block3">
                        <dl>
                            <dt title="Holding Period">開催期間</dt>
                            <dd>
                                <asp:TextBox ID="txtpfrom" runat="server" ReadOnly="true" Visible="false" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                    ControlToValidate="txtpfrom" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtPeriod_From" runat="server" ReadOnly="true" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                    ControlToValidate="txtPeriod_From" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblPeriod_from" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlPeriodFromHour">
                                    <asp:ListItem>0</asp:ListItem>
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
                                <asp:Label runat="server" ID="Label1" Text=":" />
                                <asp:DropDownList runat="server" ID="ddlPeriodFromMinute">
                                    <asp:ListItem>00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>59</asp:ListItem>
                                </asp:DropDownList>
                                ～
                                <asp:TextBox ID="txtpto" runat="server" ReadOnly="true" Visible="false" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                    ControlToValidate="txtpto" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:TextBox ID="txtPeriod_To" runat="server" ReadOnly="true" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                    ControlToValidate="txtPeriod_To" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                <asp:Label ID="lblperiod_to" runat="server" Text="" Visible="false"></asp:Label>
                                <asp:DropDownList runat="server" ID="ddlPeriodToHour">
                                    <asp:ListItem>0</asp:ListItem>
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
                                <asp:Label runat="server" ID="Label2" Text=":" />
                                <asp:DropDownList runat="server" ID="ddlPeriodToMinute">
                                    <asp:ListItem>00</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>45</asp:ListItem>
                                    <asp:ListItem>59</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="* Invalid Date" 
                ControlToCompare="txtPeriod_From" ControlToValidate="txtPeriod_To" ForeColor="Red" Operator="GreaterThan" />--%>
                            </dd>
                        </dl>
                        <dl>
                            <dt title="status">ステータス</dt>
                            <dd>
                                <asp:RadioButtonList runat="server" ID="rdolStatus" RepeatDirection="Horizontal" Width="300px">
                                    <asp:ListItem Value="0" Selected="True">開催前</asp:ListItem>
                                    <asp:ListItem Value="1">開催中</asp:ListItem>
                                    <asp:ListItem Value="2">終了</asp:ListItem>
                                    <asp:ListItem Value="3">中止</asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:Label ID="lblstatus" runat="server" Text="" Visible="false"></asp:Label>
                            </dd>
                        </dl>
                        <dl>
                            <dt title="promotion_end">キャンペーンの強制終了</dt>
                            <dd>
                                <asp:CheckBox runat="server" ID="chkIsPromotionClose" Text="開催中のプロモーションを強制的に終了する" />
                                <asp:Label ID="lblproclose" runat="server" Text="" Visible="false"></asp:Label>
                            </dd>
                        </dl>
                    </div>

                    <div class="block4">
                        <dl>
                            <dt>対象者</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtsubject" onkeydown="return(event.keyCode!=13);" /></dd>
                            <asp:Label ID="lblSubject" runat="server" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="target_brand">対象ブランド</dt>
                            <dd>
                                <asp:TextBox ID="txtBrand_Name" runat="server" onkeydown="return(event.keyCode!=13);" /></dd>
                            <asp:Label ID="lblbrand_name" runat="server" Text="" Visible="false"></asp:Label>
                        </dl>                        
                        <dl>
                            <dt title="Instructions number">対象指示書番号</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtInstructionNo" onkeypress="return isNumberKeys(event)" />
                                <asp:Label ID="lblInstructionNo" runat="server" Text="" Visible="false"></asp:Label>
                            </dd>
                        </dl>
                        <dl>
                            <dt title="Target_ItemCode">対象商品番号</dt>
                            <asp:Button ID="btnAddItemCode" runat="server" Text="商品" Width="100px" />
                            <asp:GridView ID="gvitem" runat="server" AutoGenerateColumns="False"
                                Width="301px" GridLines="None">
                                <Columns>
                                    <asp:TemplateField>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TextBox1" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblitem" runat="server" Text='<%#Eval("Item_Code") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                            <dt>対象商品</dt>
                            <dd>
                                <asp:TextBox ID="txtPromotionItem" runat="server" placeholder="複数：カンマ区切り" TextMode="MultiLine" />
                                <asp:Label ID="lblproitem" runat="server" Text="" Visible="false"></asp:Label>
                            </dd>
                        </dl>
                        <dl>
                            <dt>対象商品メモ</dt>
                            <dd>
                                <asp:TextBox ID="txtItem_Memo" runat="server" Width="220px" onkeypress="return isNumberKeys(event)" /></dd>
                            <asp:Label ID="lblItem_memo" runat="server" Text="" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="Target Shop">対象ショップ※複数選択可</dt>
                            <dd>
                                <asp:ListBox ID="lstTargetShop" runat="server" SelectionMode="Multiple"
                                    Height="200px"></asp:ListBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                    ControlToValidate="lstTargetShop" ErrorMessage="*Required" ForeColor="Red"></asp:RequiredFieldValidator>
                            </dd>
                            <asp:Label ID="lblTargetshop" runat="server" Text="" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="Application method">応募方法</dt>
                            <dd>
                                <asp:TextBox ID="txtApplicationmethod" runat="server" Width="250px" onkeypress="return isNumberKeys(event)" />
                            </dd>
                            <asp:Label ID="lblApplicationMethod" runat="server" Text="" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="Gift_contents">プレゼント内容</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtGiftContent" onkeypress="return isNumberKeys(event)" />
                            </dd>
                            <asp:Label ID="lblGift_Contents" runat="server" Text="" Visible="false" autocomplete="off"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="wayOfGifts">プレゼント方法</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtGiftway" autocomplete="off" onkeypress="return isNumberKeys(event)" />
                            </dd>
                            <asp:Label ID="lblGiftway" runat="server" Text="" Visible="false" />
                        </dl>
                        <dl>
                            <dt title="Production_target">制作対象</dt>
                            <dd>
                                <asp:TextBox ID="txtProduction_target" runat="server" onkeypress="return isNumberKeys(event)" />
                            </dd>
                            <asp:Label ID="lblProduction_target" runat="server" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="Related Information References">関連情報参照先</dt>
                            <dd>
                                <asp:TextBox ID="txtRelated_information" runat="server" Width="250px" onkeypress="return isNumberKeys(event)" />
                            </dd>
                            <asp:Label ID="lblRelatedInformation" runat="server" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="Gift">プレゼント</dt>
                            <asp:CheckBox ID="chkGift" runat="server" />
                            <asp:Label ID="lblchkGift" runat="server" Visible="false"></asp:Label>
                        </dl>
                        <div class="block4_1">
                            <dl>
                                <dt title="Remarks">備考</dt>
                                <dd>
                                    <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" Width="250px" />
                                </dd>
                                <asp:Label ID="lblRemark" runat="server" Visible="false"></asp:Label>
                            </dl>
                            <dl>
                                <dt title="Production details">制作詳細</dt>
                                <dd>
                                    <asp:TextBox ID="txtproduction_detail" runat="server" TextMode="MultiLine" Width="250px" />
                                </dd>
                                <asp:Label ID="lblproduction_detail" runat="server" Visible="false">    
                                </asp:Label>
                            </dl>
                            <dl>
                                <dt>キャンペーン画像
                                    <asp:Button ID="lnkAddPhoto" runat="server" Text="画像登録" /></dt>
                                <dd>
                                    <p>
                                        <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage1">
                                            <asp:Image runat="server" ID="Image1" />
                                        </asp:HyperLink></p>
                                    <p>
                                        <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage2">
                                            <asp:Image runat="server" ID="Image2" />
                                        </asp:HyperLink></p>
                                    <p>
                                        <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage3">
                                            <asp:Image runat="server" ID="Image3" /></asp:HyperLink></p>
                                    <p>
                                        <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage4">
                                            <asp:Image runat="server" ID="Image4" /></asp:HyperLink></p>
                                    <p>
                                        <asp:HyperLink rel="lightbox[roadtrip]" runat="server" ID="hlImage5">
                                            <asp:Image runat="server" ID="Image5" /></asp:HyperLink></p>
                                </dd>
                            </dl>
                        </div>
                    </div>
                    <div class="block5">
                        <dl>
                            <dt>楽天GOLD<br />
                                FTPアップロード</dt>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuRakuten_Gold1" /><asp:Label runat="server" ID="lblRakuten_Gold1"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuRakuten_Gold2" /><asp:Label runat="server" ID="lblRakuten_Gold2"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuRakuten_Gold3" /><asp:Label runat="server" ID="lblRakuten_Gold3"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuRakuten_Gold4" /><asp:Label runat="server" ID="lblRakuten_Gold4"></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt>楽天Cabinet<br />
                                FTPアップロード</dt>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuRakuten_Cabinet1" /><asp:Label runat="server" ID="lblRakuten_Cabinet1"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuRakuten_Cabinet2" /><asp:Label runat="server" ID="lblRakuten_Cabinet2"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuRakuten_Cabinet3" /><asp:Label runat="server" ID="lblRakuten_Cabinet3"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuRakuten_Cabinet4" /><asp:Label runat="server" ID="lblRakuten_Cabinet4"></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt>geocities<br />
                                FTPアップロード</dt>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuYahoo1" /><asp:Label runat="server" ID="lblYahoo1"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuYahoo2" /><asp:Label runat="server" ID="lblYahoo2"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuYahoo3" /><asp:Label runat="server" ID="lblYahoo3"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuYahoo4" /><asp:Label runat="server" ID="lblYahoo4"></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt>ポンパレ<br />
                                FTPアップロード</dt>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuPonpare1" /><asp:Label runat="server" ID="lblPonpare1"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuPonpare2" /><asp:Label runat="server" ID="lblPonpare2"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuPonpare3" /><asp:Label runat="server" ID="lblPonpare3"></asp:Label></dd>
                            <dd>
                                <asp:FileUpload runat="server" ID="fuPonpare4" /><asp:Label runat="server" ID="lblPonpare4"></asp:Label></dd>
                        </dl>
                    </div>
                    <div class="block6">
                        <dl>
                            <dt>オプション</dt>

                            <dd>
                                <p><b>項目名</b><asp:TextBox ID="txtOption_Name1" runat="server" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblOp1" runat="server" Visible="false"></asp:Label>選択肢<asp:TextBox ID="txtOption_Value1" runat="server" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblOpVal1" runat="server" Text="" Visible="false"></asp:Label></p>

                                <p><b>項目名</b><asp:TextBox ID="txtOption_Name2" runat="server" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblOp2" runat="server" Visible="false"></asp:Label>選択肢<asp:TextBox ID="txtOption_Value2" runat="server" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblOpVal2" runat="server" Text="" Visible="false"></asp:Label></p>

                                <p><b>項目名</b><asp:TextBox ID="txtOption_Name3" runat="server" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblOp3" runat="server" Visible="false"></asp:Label>選択肢<asp:TextBox ID="txtOption_Value3" runat="server" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblOpVal3" runat="server" Text="" Visible="false"></asp:Label></p>
                            </dd>
                        </dl>
                    </div>
                    <div class="block7">
                        <dl>
                            <dt title="pc_campaign1">PCキャンペーン1</dt>
                            <dd>
                                <asp:TextBox ID="txtPCCampaig1" runat="server" TextMode="MultiLine" /><asp:Label ID="lblpro_desX" runat="server" Text="" Visible="false"></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt title="Smart_Campaign1">スマホキャンペーン1</dt>
                            <dd>
                                <asp:TextBox ID="txtSmart_Campaign1" runat="server" TextMode="MultiLine" /><asp:Label ID="lblpro_descY" runat="server" Text="" Visible="false"></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt title="PC_Campaign2">PCキャンペーン2</dt>
                            <dd>
                                <asp:TextBox ID="txtPC_Campaign2" runat="server" TextMode="MultiLine" /><asp:Label ID="lblsale_descX" runat="server" Text="" Visible="false"></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt title="Smart_Campaign2">スマホキャンペーン２</dt>
                            <dd>
                                <asp:TextBox ID="txtSmart_Campaign2" runat="server" TextMode="MultiLine" /><asp:Label ID="lblsale_descY" runat="server" Text="" Visible="false"></asp:Label></dd>
                        </dl>
                    </div>

                    <div class="block8 block4">
                        <dl>
                            <dt title="Product Name decoration">商品名装飾</dt>

                            <dd>
                                <asp:TextBox ID="txtproductname_decoration" runat="server" Width="250px" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblproduct_namedecoration" runat="server" Visible="false" /></dd>
                        </dl>
                        <dl>
                            <dt title="PC catch copy decoration">PCキャッチコピー装飾</dt>
                            <dd>
                                <asp:TextBox ID="txtpc_catchCopy" runat="server" Width="250px" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblpcCatchCopy" runat="server" Visible="false" /></dd>
                        </dl>
                        <dl>
                            <dt title="Smart catch copy decoration">スマホキャッチコピー装飾</dt>
                            <dd>
                                <asp:TextBox ID="txtSmart_Catch_Copy" runat="server" Width="250px" onkeypress="return isNumberKeys(event)" /><asp:Label ID="lblsmart_Catchcopy" runat="server" Visible="false" /></dd>
                        </dl>
                    </div>
                    <div class="block9">
                        <dl>
                            <dt title="Publication">公開</dt>
                            <dd>
                                <asp:DropDownList ID="ddlPublication" runat="server">
                                    <asp:ListItem Text="公開" Value="0">公開</asp:ListItem>
                                    <asp:ListItem Text="非公開" Value="1">非公開</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lblPublic" runat="server" Text="" Visible="false"></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt title="Black market setting">闇市設定</dt>
                            <dd>
                                <asp:DropDownList ID="ddlBlackMarket" runat="server">
                                    <asp:ListItem Text="あり" Value="0">あり</asp:ListItem>
                                    <asp:ListItem Text="なし" Value="0">なし</asp:ListItem>
                                </asp:DropDownList></dd>
                            <asp:Label ID="lblBlackmarket" runat="server" Text="" Visible="false"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="secret">シークレットID</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtSecret_ID" Width="150px" onkeypress="return isNumberKeys(event)" />
                            </dd>
                            <asp:Label ID="lblSecretID" runat="server" Text="" Visible="false" autocomplete="off"></asp:Label>
                        </dl>
                        <dl>
                            <dt title="secret_password">シークレットパスワード</dt>
                            <dd>
                                <asp:TextBox runat="server" ID="txtSecret_Password" autocomplete="off" Width="150px" onkeypress="return isNumberKeys(event)" /></dd>
                            <asp:Label ID="lblSecPassword" runat="server" Text="" Visible="false" />
                        </dl>
                        <dl>
                            <dt title="Priorities">優先順位</dt>
                            <dd>
                                <asp:DropDownList ID="ddlPriorities" runat="server">
                                    <asp:ListItem Text="特" Value="0"></asp:ListItem>
                                    <asp:ListItem Text="高" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="中" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="低" Value="3"></asp:ListItem>
                                </asp:DropDownList>
                            </dd>
                            <asp:Label ID="lblPriority" runat="server" Visible="false"></asp:Label>
                        </dl>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="btn">
        <asp:Button runat="server" ID="btnConfirm" Text="確 認" OnClick="btnConfirm_Click" /></div>
</asp:Content>