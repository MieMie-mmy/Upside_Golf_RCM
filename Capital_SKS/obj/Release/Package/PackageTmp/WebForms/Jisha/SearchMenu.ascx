<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SearchMenu.ascx.cs" Inherits="ORS_RCM.WebForms.Jisha.SearchMenu" %>

<style type="text/css">
.check
{
  width:100%;
  padding-left:650px;  

 }
 .ck 
 {
  width:500px;
  
       padding-left:820px;  
     }
   
     #pf
     {
      font-size:small;   
     }
     
#pid
{
   font-size:small;color:Red; 
  }
 
</style>


<div  class="ck" >

    <asp:Button ID="btnuserguide" runat="server" Text="ご利用ガイド"  style="width:80px; height: 24px; border:1px #999999 solid; color:; font-weight:;"  />
       <asp:ImageButton ID="imgcart" runat="server" ImageUrl="../../images/h_shopicon01.gif"  onclick="imgcart_Click" />
</div>


<div class="check">

<asp:Image ID="Image" runat="server" ImageUrl="../../images/h_sartitle.gif" />
    <asp:TextBox ID="txtsearch" runat="server"  Width="170px" Height="20px"></asp:TextBox>
    <asp:Button ID="btnsearch" runat="server" Text="商品を検索"  
        style="width:110px; height: 24px; border:1px #999999 solid; color:#463d37; font-weight:bold;" 
        onclick="btnsearch_Click"/>
</div>
<div id="logo">

  <asp:Image ID="Image9" runat="server"  ImageUrl="../../images/Jisha_logo.png"/>  
       
</div>

<div>
    <asp:Image ID="Image1" runat="server" ImageUrl="../../images/h_menu01.jpg" />
   <asp:Image ID="Image3" runat="server" ImageUrl="../../images/h_menu02.jpg"/>
    <asp:Image ID="Image2" runat="server" ImageUrl="../../images/h_menu03.jpg"/>
   <asp:Image ID="Image4" runat="server" ImageUrl="../../images/h_menu04.jpg"/>
    <asp:Image ID="Image5" runat="server" ImageUrl="../../images/h_menu05.jpg"/>
  <asp:Image ID="Image6" runat="server" ImageUrl="../../images/h_menu06.jpg"/>
    <asp:Image ID="Image7" runat="server" ImageUrl="../../images/h_menu07.jpg" />
   <asp:Image ID="Image8" runat="server" ImageUrl="../../images/h_menu08.jpg" />
  
</div>
<hr style="color:#7CFC00 ; background:#7CFC00 ; width: 78%; height: 4px;margin-right:790px; " />

<div>
 
 <table border="0">
   <tr><td>
  <asp:Image ID="Image10" runat="server" ImageUrl="../../images/hp_140827_1.gif"  alt="ディアドラ／14FW"  />   </td>

 <td><asp:Image ID="Image11" runat="server" ImageUrl="../../images/hp_140827_2.gif" alt="ディアドラ／14FW"/></td>
 <td><asp:Image ID="Image12" runat="server" ImageUrl="../../images/hp_140827_3.gif" alt="ディアドラ／14FW"/></td>
  <td><asp:Image ID="Image13" runat="server" ImageUrl="../../images/hp_140827_4.gif" alt="ニューバランス／14FW"/></td>
  <td><asp:Image ID="Image14" runat="server" ImageUrl="../../images/hp_140827_5.gif" alt="ミズノ／キャンペーン"/></td>
  <td ><asp:Image ID="Image15" runat="server" ImageUrl="../../images/hp_140827_6.gif" alt="スリクソン／ボストンバッグ"/></td>
  <td > <asp:Image ID="Image16" runat="server" ImageUrl="../../images/hp_140827_7.gif"  alt="プリンス／シューズ"/></td>
 <td  > <asp:Image ID="Image17" runat="server" ImageUrl="../../images/hp_140827_8.gif"  alt="当店オリジナル／ラケットケース" /></td>
 
    <td > <asp:Image ID="Image18" runat="server" ImageUrl="../../images/j-soryo8000-140331.jpg" Width="300px" Height="80px" /></td>
   </tr>
   <tr>
 <td><span><p id="pf" >14年秋冬ウェア  <br />Diadora</p><p  id ="pid" >新作登場</p></span></td>
    <td><span><p id="pf">14年秋冬ウェア<br />Babolat</p><p id ="pid">新作登場</p></span></td>
     <td><span><p id="pf">14年秋冬ウェア<br />FILA</p><p id ="pid">新作登場</p></span></td>
      <td><span><p id="pf">14年秋冬ウェア<br />Newbalance</p><p id ="pid">新作登場</p></span></td>
       <td><span><p id="pf">お買い得アイテム<br />Mizuno</p><p id ="pid">3,980円～</p></span></td>
        <td><span><p id="pf">スポーツボストン<br />Srixon</p><p id ="pid">3,996円</p></span></td>
         <td><span><p id="pf">バドミントンシューズ<br />Prince</p><p id ="pid">3,990円</p></span></td>
          <td><span><p id="pf">ラケットケース<br />当店オリジナル</p><p id ="pid">1,680円</p></span></td>
   </tr>
   <tr><%--My Link--%>
   <td>
   <asp:Menu ID="Menu1" runat="server"> 
    <Items>
    <asp:MenuItem NavigateUrl="~/LogIn.aspx" Text="SKS Login" />
    <asp:MenuItem NavigateUrl="~/WebForms/Jisha/Jisha_Item_View.aspx" Text="Item_View" />
     <asp:MenuItem NavigateUrl="~/Jisha_Import.aspx" Text="Import Jisha" />
    </Items>
    </asp:Menu>
   </td>
   </tr>
   </table>
</div>

