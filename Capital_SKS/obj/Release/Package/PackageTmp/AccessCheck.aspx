<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AccessCheck.aspx.cs" Inherits="ORS_RCM.AccessCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">

.border {
	
	background-color: #f1f1f1;
}

.SectionHeader
{
	color: Black;
	background-color: #CCCCCC;
	font-weight: bold;
	font-size: 12;
	text-align: center;
	height:20px
	}
td {
	font-family: Verdana, Arial, Helvetica, sans-serif;
	font-size: 11px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<br />
<br />
<br />
<br />
<br />
<br />

    <table width="50%" border="0" align="Center" cellpadding="3" cellspacing="0" bgcolor="#f4f4f4"
                        class="border">
        <tr class="SectionHeader">
            <td height="20" class="SectionHeader">
                <strong><font>アクセス権限がありません</font></strong>
            </td>
        </tr>
        <tr>
            <td>
                <p align="center">
                    <br><span class="setup"><strong><font color="#FF0000">制作責任者に確認してください</font></strong></span>
                </p>
                <p align="center">
                                    
                                </p>
                <p align="center">
                                  <a href="javascript:history.back(1)"><strong>戻る</strong></a>を押して前の画面に戻ってください<br>
                    <br>
                </p>
            </td>
        </tr>
    </table>
</asp:Content>
