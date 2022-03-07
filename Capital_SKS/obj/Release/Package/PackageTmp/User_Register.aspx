<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="User_Register.aspx.cs" Inherits="ORS_RCM.User_Register" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="~/Styles/base.css" rel="stylesheet" type="text/css" />


     <link href="Styles/style.css" rel="stylesheet" type="text/css" />
     <link href="Styles/base.css" rel="stylesheet" type="text/css" />
     <link href="Styles/common.css" rel="stylesheet" type="text/css" />
     <link href="Styles/manager-style.css" rel="stylesheet" type="text/css" />

    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

                      
                      
                       
                           <div id="CmnContents">
	                            <div id="ComBlock">
                                      <!-- User search -->
	                                     <div class="setDetailBox iconSet iconEdit">
                                         <h1>ユーザ編集</h1>
                                         
                                     
                                       <form action="#" method="get">
                                      <table class="userCmnSet editTable">

                                      <tbody>     
  
        <tr>
          
               <th>ユーザ名</th> 
              
               
                 <td>
                <asp:TextBox ID="txtUserName" runat="server" Width="300px" CssClass="input" 
                    Height="30px" TabIndex="1"></asp:TextBox>
                     <asp:Label ID="lblUser_Name" runat="server" Visible="false"></asp:Label>
                </td>
        </tr>
        <tr>
            
           <th>Login ID</th>

             <td>   
                 <asp:TextBox ID="txtLoginID" runat="server" CssClass="input"  Width="300px"
                     Height="30px" TabIndex="2"></asp:TextBox>

                 <asp:Label ID="lblLoginID" runat="server"  Visible="false"></asp:Label>

            </td>
        </tr>
        <tr>
          
            <th>  <label class="label">パスワード</label></th>
            <td>
                <asp:TextBox ID="txtPassword" runat="server"  
                  Height="30px" TextMode="Password" TabIndex="3"></asp:TextBox>


                <asp:Label ID="lblPassword" runat="server"  Visible="false"></asp:Label>


            </td>

        </tr>

        <tr>
          
           
        <th>ユーザステータス</th>
                               <td>

         
                            <asp:RadioButton ID="rdostatus" runat="server" Text="有効" Value="1" 
                                GroupName="rbdStatus"  Checked="True"/>
                     
                            <asp:RadioButton ID="rdostatus1" runat="server" Text="無効" Value="0" GroupName="rbdStatus" />
                            
                                   <asp:Label ID="lblstatus" runat="server" Text="Label"></asp:Label>
                            
                        </td>
                           
        </tr>
        
       </tbody>


   
   </table>
 
 </form>
  </div><!--ComBlock-->
  </div>   
  <div>
   </div>
   </div>
  
   
 

 
                    <div class="btn">
                    <table>
                    <tr>
                    <td><asp:Button ID="btnsave" runat="server" Text="登録する" Width="300"  
    
                    onclick="btnsave_Click"/> 

                    </td>

                  <td>   <p id="toTop"><a href="#CmnContents">▲TOP</a></p></td>


                           </tr>
                    </tabe>

                    </div>
                   

       

      
       

                              
</asp:Content>
 