<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserRole.aspx.cs" Inherits="ORS_RCM.UserRole" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="Styles/user.css" rel="stylesheet" type="text/css" />
	<link href="Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<title>商品管理システム＜ユーザ登録＞</title>


    <Style type ="text/css">
        div.btn
        {
            margin: 20px auto -35px auto;  
        }

</Style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<p id="toTop"><a href="#CmnContents">▲TOP</a></p>

<div id="CmnContents">
	<div id="ComBlock">
<!-- User search -->
	
    <div id="edi" runat="server"  class="setDetailBox confSet iconSet iconCheck"  visible="false">
		  
         <h1>ユーザ編集確認画面</h1>
			</div>
    
     <div id="reg_Confirm" runat="server"  class="setDetailBox confSet iconSet iconCheck"  visible="false">
		  
         <h1>ユーザ登録 確認画面</h1>
			</div>
   

    <div id="regi" runat="server"  class="setDetailBox iconSet iconEdit" visible="false">
            <h1 runat="server" id="head"></h1></div>
     
    <div class="setDetailBox confSet iconSet iconCheck">
		<%--<form action="#" method="get">--%>
			<table class="userCmnSet editTable">
				
		
        <tbody>
			<tr>
			<th>ユーザ名&nbsp;<span>※必須</span></th>
			<td>
				<asp:TextBox ID="txtname" runat="server" Width="500px" Height="25px"></asp:TextBox>
                 <asp:Label ID="lbluserName" runat="server"  Visible="false"></asp:Label>
              	<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="※必須" ControlToValidate="txtname"></asp:RequiredFieldValidator>
               
			


			</td>
			</tr>
			<tr>
				<th>ID&nbsp;<span>※必須</span></th>
				<td>
                    <div runat="server" style="float:left" id="divID">
					<asp:TextBox ID="txtID" style="float:left" runat="server" Width="500px" Height="25px" autocomplete="off"></asp:TextBox>
                    </div>
                    <asp:Label ID="lblID" runat="server"  Visible="false"></asp:Label>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="※必須" ControlToValidate="txtID"></asp:RequiredFieldValidator>

				    

				</td>
			</tr>
			<tr>
				<th>パスワード&nbsp;<span>※必須</span></th>
				<td>
                <div runat="server" style="float:left" id="divPass">
					<asp:TextBox ID="txtpassword" style="float:left" runat="server" TextMode ="Password" Width="500px" Height="25px" autocomplete="off"></asp:TextBox>
                    </div>
                      <asp:Label ID="lblPassword" runat="server"  Visible="False" 
                        EnableTheming="False"></asp:Label>
					<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="※必須" ControlToValidate="txtpassword"></asp:RequiredFieldValidator>

				  

				</td>
			</tr>
			<tr>
				<th>ユーザステータス</th>
				<td>
					<asp:RadioButton ID="rdostatus" runat="server" Text="有効" Value="1" 
									GroupName="rbdStatus"  Checked="True"/>
					<asp:RadioButton ID="rdostatus1" runat="server" Text="無効" Value="0" GroupName="rbdStatus" />
                    <asp:Label ID="lblStatus" runat="server"  Visible="false"></asp:Label>
				</td>
			</tr>
				</tbody>
			</table>
		<%--</form>--%>
	</div>
	<div style="margin-left:10px">
		<table class="userCmnSet editTable">
		<tr>
			<th style="width:160px; text-align:left; border-right:1px solid #ddd; border-bottom:1px solid #ddd; border-left:5px solid #73c5ff; padding:5px 5px 5px 10px"  >権限&nbsp;<span>※必須</span></th>				
				<td>
					<asp:UpdatePanel ID="UpdatePanel1" runat="server">
						<ContentTemplate>
							<asp:GridView ID="gvUserRole" runat="server" ShowHeader="false" 
								AutoGenerateColumns="false" GridLines="None" 
								onrowdatabound="gvUserRole_RowDataBound" onselectedindexchanged="gvUserRole_SelectedIndexChanged">
								<Columns>
									<asp:TemplateField Visible="false">
										<HeaderTemplate>
											<asp:Label ID="lblMenuHeader" runat="server" Text="Menu"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblMenu" Text='<%# Eval("Menu_ID")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField Visible="false">
										<HeaderTemplate>
											<asp:Label ID="lblMenuID" runat="server" Text="Menu ID"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblMenuID" Text='<%# Eval("ID")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField Visible="false">
										<HeaderTemplate>
											<asp:Label ID="lblMenu" runat="server" Text="Master ID"></asp:Label>
										</HeaderTemplate>
										<ItemTemplate>
											<asp:Label runat="server" ID="lblMasterID" Text='<%# Eval("Parent_ID")%>' ></asp:Label>
										</ItemTemplate>
									</asp:TemplateField>
									<asp:TemplateField>
										<HeaderTemplate >
											<asp:CheckBox ID="chkAllRead" runat="server"  AutoPostBack="True" OnCheckedChanged="chkReadSelectAll_CheckedChanged"  Visible="false"/>
										</HeaderTemplate>
										<ItemTemplate>
												<asp:Label runat="server" Text="" ID="lblPadding" Visible="false"></asp:Label>
												<asp:CheckBox ID="ckbRead" runat="server"  AutoPostBack="True" OnCheckedChanged="ckbRead_CheckedChanged"/>                                           
												<asp:Label runat="server" ID="lblDescription" Text='<%# Eval("Description")%>'></asp:Label> 

                                                <asp:Label runat="server" ID="lbl_Descrip"></asp:Label> 
                                                

										</ItemTemplate>
									</asp:TemplateField>  
								</Columns>
							</asp:GridView>
						</ContentTemplate>
					</asp:UpdatePanel>
					</td>				
				</tr>

               
                
               
		</table>
	
<!-- /User search -->



	<div class="btn">
       
		<asp:Button ID="btnAddRole" runat="server" Text="確認画面へ"  onclick="btnAddRole_Click"  Width="150px"  Height="30px" style="margin-bottom:30px;"/>
	
        </div>


    </div>

    </div>
    
    </div>
</asp:Content>
