﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Import_Item.aspx.cs" Inherits="Upside_Golf_RCM.WebForms.Import.Import_Item_New" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    <link href="../../Styles/database.css" rel="stylesheet" type="text/css" />

    <link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
    <link href="../../Scripts/jquery.page-scroller.js" />
    <title>商品管理システム＜商品マスタデータインポート＞</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div id="CmnContents">
    <div id="ComBlock">
    <div class="setListBox iconSet iconImport">

    <h1>商品マスタデータインポート</h1>

<!-- Link List -->	
        <div class="dbCmnSet iptList inlineSet">
            <form action="#" method="get">
                <dl>
                    <dt>商品マスタデータ</dt>
                    <dd><asp:FileUpload runat="server" ID="uplItemImport"/></dd>
                    <dt>SKUマスタデータ</dt>
                    <dd><asp:FileUpload runat="server" ID="uplSKUImport"/></dd>
                    <dt>在庫データ</dt>
                    <dd><asp:FileUpload runat="server" ID="uplInventoryImport"/></dd>
                     <%--added by ETZ for sks-390 TagID--%>
                    <dt>ディレクトリID/タグID　データ</dt>               
                    <dd><asp:FileUpload runat="server" ID="uplRakutenTagID"/></dd>
                    <dt>Monotoro_Import</dt>               
                    <dd><asp:FileUpload runat="server" ID="uplMonotaroImport"/></dd>
                </dl>
                <p><asp:Button runat="server" Text="インポート開始" ID="btnImport" 
                        onclick="btnImport_Click"/></p>
            </form>
    </div>
<!-- /Link List -->	

    </div><!--setListBox-->



    </div><!--ComBlock-->
</div><!--CmnContents-->
</asp:Content>
