<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Query_Search.aspx.cs" Inherits="ORS_RCM.Admin.Query_Search" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .txtQuery {
            width:500px;
            border: none;
            border-radius: 5px;
            box-shadow: 0 5px #999;
            font-weight:bold;
        }
        .btnQuery {
            width: 150px;
            border-radius: 12px;
            cursor: pointer;
            border: none;
            border-radius: 5px;
            box-shadow: 0 5px #999;
            font-weight:bold;
        }
            .btnQuery:hover {
                box-shadow: 0 12px 16px 0 rgba(0,0,0,0.24), 0 17px 50px 0 rgba(0,0,0,0.19);
                background-color:#999;
            }
            .btnQuery:active {
                background-color: #3e8e41;
                box-shadow: 0 5px #666;
                transform: translateY(4px);
            }
        .row {
             white-space: pre;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <body>
        <div id="CmnContents">
            <div id="ComBlock">
                <div class="setListBox inlineSet iconSet iconList">
                    <h1>Log_Data_Delete</h1>
                    <div class="itemCmnSetKnr itemCmnSet resetValue searchBox">
                        <h3 style="color:Red;padding-left:20px;">*****SQL QUERY AND DATABASE APPLY*****</h3>
                        <div style="margin-left:30px;margin-top:20px;">
                            <asp:TextBox ID="txtQuery" CssClass="txtQuery" runat="server" TextMode="MultiLine" Height="150px"></asp:TextBox>
                            <asp:Button ID="btnQuery" CssClass="btnQuery" runat="server" OnClick="btnQuery_Click" Text="Query" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div>            
            <asp:GridView ID="gvQueryData" runat="server" AutoGenerateColumns="true" EmptyDataText="There is no Data to Display!" AllowPaging="True" PageSize="30"
                 EnableTheming="False" ForeColor="#333333" GridLines="None" CssClass="managementList listTable" >
                <RowStyle CssClass="row"/>
            </asp:GridView>
        </div>
    </body>
</asp:Content>
