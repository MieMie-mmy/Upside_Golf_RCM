<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Smart_Template_Entry.aspx.cs" Inherits="ORS_RCM.Smart_Template_Entry" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
	<link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
	<link href="../../Styles/item.css" rel="stylesheet" type="text/css" />

	<link href="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" />
	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />

	<title>商品管理システム＜スマートテンプレート登録＞</title>
</asp:Content>
 <asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	 <p id="toTop"><a href="#CmnContents">▲TOP</a></p>
	<div id="CmnContents">
		<div id="ComBlock">
			<div class="setListBox inlineSet iconSet iconEdit">

            <h1 runat="server" id="head"></h1>
             
		<%--		<h1>スマートテンプレート登録</h1>--%>
				<!-- User list -->
				<div class="smartTmp entryBox">
					<form>			
						<div>
							<table>
								<tbody>
									<tr>
										<th>ステータス</th>
										<td>
											<asp:CheckBox ID="chkstatus" runat="server" Text="有効"  ToolTip="(Status)"/>
											<asp:Label runat="server" ID="lblcheckStatus" Text="有効" Visible="false"></asp:Label>
										</td>
									</tr>
									<tr>
										<th>テンプレートID</th>
										<td valign="top">
											<asp:TextBox ID="txtSmartTemplate" runat="server" MaxLength="50" />
											<asp:Label runat="server" ID="lblSmartTemplate" Visible="false"></asp:Label>
											<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*" ControlToValidate="txtSmartTemplate" ForeColor="#FF3300" ToolTip="TemplateID" ></asp:RequiredFieldValidator>
										</td>
									</tr>
                                    <tr>
                                    <th>テンプレート名</th>
                                    <td>
                                        <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
                                        <asp:Label ID="lblname" runat="server" Text="" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
								</tbody>
							</table>
						</div>

						<div>
							<asp:DataList ID="dlSmartTemplate" runat="server" CellPadding="4" 
								ForeColor="#333333" onitemdatabound="dlSmartTemplate_ItemDataBound" >
								<ItemTemplate>
									<h2><asp:Label runat="server" ID="lbltemplateNo" Text="スマートテンプレート#"></asp:Label></h2>
									<table>
										<tbody>
											<tr>
												<th>ショップ</th>
												<td valign="middle">
													<asp:Label ID="lblShopID" runat="server" Text='<%#Bind("Shop_ID") %>' Visible="false"></asp:Label>
													<asp:TextBox ID="txtShop_Name" runat="server" Text='<%#Bind("Shop_Name") %>'  ToolTip="Shop" ReadOnly="true"></asp:TextBox>
													<asp:Label runat="server" ID="lblShopName" Text='<%#Bind("Shop_Name") %>' ToolTip="Shop" Visible="false"></asp:Label>
												</td>
											</tr>
											<tr>
												<th>テンプレート</th><td><asp:TextBox ID="txtTemplateDesc" runat="server" Text='<%#Bind("Template_Description") %>' placeholder="半角英数字／-（ハイフン）／_（アンダーバー）が利用可能"   TextMode="MultiLine"   ToolTip="Template Description" width="600px"></asp:TextBox></td>
											</tr>
										</tbody>
									</table>
								</ItemTemplate>
							</asp:DataList>
						</div>

                        <%--<div class="btn"><input type="button" ></div>--%>

						<div class="btn">
                       
                        <input type="submit" id="btnpopup" onclick="" runat="server" style="width:200px"  value="確認画面へ" />
                       
                        <asp:Button runat="server" Width="130px" ID="btnConfirm_Save" OnClick="btnConfirm_Save_Click" Text="確認画面へ" Visible="false" />
                        </div>
					</form>
				</div><!--User list-->
			</div>
		</div>
	</div>
	 <asp:HiddenField ID="hfCount" Value="1" runat="server" />
</asp:Content>
