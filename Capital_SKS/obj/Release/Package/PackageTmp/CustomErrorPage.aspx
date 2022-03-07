<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" ErrorPage="~/CustomErrorPage.aspx" EnableSessionState="True" CodeBehind="CustomErrorPage.aspx.cs" Inherits="ORS_RCM.CustomErrorPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">   
    <style type="text/css">
        .SectionHeader
        {
            text-align: center;
            font-weight: 700;
            text-decoration: underline;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br /><br /><br /><br /><br /><br />
    <div>    
     <table width="50%" border="0" align="center" cellpadding="3" cellspacing="0" bgcolor="#f4f4f4"
                        class="border">
        <tr class="SectionHeader">
            <td height="20" class="SectionHeader">
                 Process Failed!</td>
        </tr>
        <tr>
            <td>
                <p align="center">
                    <br/><span class="setup"><strong><font color="#FF0000">Oops, Error has occured</font></strong></span>
                </p>
                <p align="center">
                    Please contact the System Administrator.
                                </p>
                <p align="center">
                               Please click <a href="javascript:history.back(1)"><strong>BACK</strong></a> 
                                  to previous  screen..<br/>
                    <br/>
                </p>
            </td>
        </tr>
    </table>
    </div>

</asp:Content>
