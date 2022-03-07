<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Item(Master)_Preview.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Master__Preview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Item Preview</title>
    
    <link href="~/Styles/base.css" rel="stylesheet" type="text/css" />


     <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
     <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
    
    
    
    
    </head>

    


<body>
    <form id="form1" runat="server">
    <div>

    <div id="prevBlock">
<div id="pagebody" align="center">

<table cellspacing="2" cellpadding="0" border="0">
<tbody><tr>
<td class="sdtext">




<tr>
<td class="sdtext">
    
   







   
        



    <asp:GridView ID="GridView1"  runat="server"   AutoGenerateColumns="False" 
           ShowHeader="False" Width="300px" CellPadding="4" ForeColor="#333333" 
        GridLines="None">
	    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
	    <Columns>
		<asp:TemplateField HeaderText="ID" Visible="false">
		<ItemTemplate>
		<asp:Label runat="server" ID="lblID" />
		</ItemTemplate>
		</asp:TemplateField>
		<asp:TemplateField>
		<ItemTemplate>
        <asp:TextBox runat="server" ID="txtCTGName" />
		<asp:Label runat="server" ID="lblCTGName" />
		</ItemTemplate>
		</asp:TemplateField>
		</Columns>
	    <EditRowStyle BackColor="#999999" />
        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#E9E7E2" />
        <SortedAscendingHeaderStyle BackColor="#506C8C" />
        <SortedDescendingCellStyle BackColor="#FFFDF8" />
        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
	    </asp:GridView>
   
    
   







   
        



    </td>
    
   







</tr>


<tr>
<td class="sdtext">
    &nbsp;</td>
</tr>




</tbody></table>

<!-- 05:詳細 -->
<br>
<table cellspacing="0" cellpadding="0" border="0">
<tbody><tr>
<td><span class="sale_desc">

<!-- ソース吐き出し-->
<div id="rmsItemSet" align="center" style="width: 765px; margin: 0 auto;">

<!-- 商品画像 -->
<asp:Image ID="image1"  runat="server"   Text="photo1" style="border:none;"  src=""/>
<asp:Image ID="image2"  runat="server" style="border:none;" alt="" src=""/>
<asp:Image ID="image3"  runat="server"  style="border:none;" alt="" src=""/>

<asp:Label ID="lblmessage"  runat="server"   Text="NO Image" Visible="False"  ForeColor="Red"/>

</div>
<div style="float:inherit">
<asp:DataList ID="dlPhoto" runat="server" BackColor="White" BorderColor="#666666"
BorderStyle="None" CellPadding="3" CellSpacing="2" 
Font-Names="Verdana" Font-Size="Small" RepeatColumns="1" 
        RepeatDirection="Horizontal">
<HeaderStyle BackColor="#333333" Font-Bold="True" Font-Size="Large" ForeColor="White"
HorizontalAlign="Center" VerticalAlign="Middle" />
<HeaderTemplate>
<asp:Label runat="server" ID="lblHeader" Text="商品画像" ToolTip="Item Image" />
</HeaderTemplate>
<ItemTemplate>

<%--<asp:Label runat="server" ID="lblImageID" Text='<%# Bind("ID")%>' Visible="false" /> 
<asp:Label runat="server" ID="lblImageName" Text='<%# Bind("Image_Name")%>' Visible="false" />--%>
<asp:Image ID="imgItem" runat="server" Width="80px" Height="80px" ImageUrl='<%# Bind("Image_Name", "~/Item_Image/{0}") %>' 
ControlStyle-Width="120" onmouseover="this.style.cursor='hand'" onmouseout="this.style.cursor='default'" ControlStyle-Height = "100" /><br />
</ItemTemplate>
<ItemStyle HorizontalAlign="center" />
</asp:DataList>
 
 </div>
 
 

 
 
 





<!-- /商品画像 -->

<!-- ライブラリ画像／サイズ表 -->
<div style="width: 600px; text-align: center; margin-bottom: 20px; height: 16px;"><img src=""></div>

    

&nbsp;<!-- 使用と特徴 --><div style="margin: 10px auto; padding: 0; height: 100px;">
        <asp:Literal ID="Sale_Literal" runat="server"></asp:Literal>
<table style="font-size: 13px; color: #444444; width: 610px; line-height: 1.7;border: 1px solid #dcdcdc; text-align: left;" border="0" cellpadding="7" cellspacing="0"><tbody>



</tbody></table></div>

<div style="width: 600px; text-align: center; margin-bottom: 20px;"></div>

<!-- 使用と特徴 -->
<div style="margin: 10px auto; padding: 0;">
<table style="font-size: 13px; color: #444444; width: 610px; line-height: 1.7;border: 1px solid #dcdcdc; text-align: left;" border="0" cellpadding="7" cellspacing="0"><tbody>
<tr><td colspan="2" style="color: #000; background-color: #f5f5f5; width: 610px; vertical-align: middle; padding: 7px 7px;border-bottom: 1px solid #dcdcdc">■仕様と特徴</td></tr>
<tr><td style="border-bottom: 1px solid #dcdcdc ;color: #444444; background-color: #fff; width: 606px; vertical-align: top;"><b><font color="#444444">仕様と特徴</font></b><br>スポーティーなカラーブロックをデザイン基調にした、クルーネックタイプのゲームシャツ。<br>前後に入った切り替えとラインプリントがポイント。<br>素材は、吸汗速乾、UV、ストレッチの効いた「クイックドライピケ」を使用。</td></tr>
<tr><td style="border-bottom: 1px solid #dcdcdc ;color: #444444; background-color: #fff; width: 606px; vertical-align: top;"><b><font color="#444444">テクノロジー</font></b><br>吸汗速乾<br>UVケア<br>ストレッチ</td></tr>
</tbody></table></div>

<!-- Check it out -->
<div style="background-image:  url(http://www.rakuten.ne.jp/gold/racket/lib_img/img/check.gif); padding: 0px; margin: 20px auto 0 ; width: 610px; height: 40px; text-align: center;">






</div>

</div>


    
    </div>
    </form>
</body>
</html>
