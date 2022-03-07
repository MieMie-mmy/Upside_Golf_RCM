<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Exhibition_Backup.aspx.cs" Inherits="ORS_RCM.Admin.Exhibition_Backup" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
<link href="../../Styles/exhibition.css" rel="stylesheet" type="text/css" />
<script src="../../Scripts/jquery.page-scroller.js" type="text/javascript"></script>  
<script src="../../Scripts/calendar1.js" type="text/javascript"></script>
<link href ="../../Styles/Calendarstyle.css" rel="Stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <body>
        <div id="CmnContents">
            <div id="ComBlock">
                <div class="setListBox inlineSet iconSet iconList">
                    <h1>Update Exhibition Flag</h1>
                    <div class="itemCmnSetKnr itemCmnSet resetValue searchBox">
                        <h3 style="color:Red;padding-left:20px;">*****Backup Exhibition Data by Item Code From Item Shop Table*****</h3>
                        <dl style="padding-top:30px;padding-bottom:20px;">
                            <dt>商品番号</dt>
                            <dd><asp:TextBox ID="txtItemCode" runat="server" TextMode="MultiLine"></asp:TextBox></dd>
                            <dt style="width: 0px;"></dt>
                            <dd><asp:CheckBoxList ID="chlShop" runat="server" Width="500px" Height="16px" RepeatDirection="Horizontal" 
                                    RepeatColumns="4" RepeatLayout="Table"></asp:CheckBoxList></dd>
                            <p><asp:Button ID="btnUpdate" runat="server" Text="Backup" OnClick="btnUpdate_Click" Width="110px" ValidationGroup="Group"/>
                                <asp:Button ID="btnRestore" runat="server" Text="Restore" OnClick="btnRestore_Click" Width="110px" ValidationGroup="Group"/></p>
                        </dl>
                    </div>
                </div>
            </div>
        </div>
    </body>
</asp:Content>
