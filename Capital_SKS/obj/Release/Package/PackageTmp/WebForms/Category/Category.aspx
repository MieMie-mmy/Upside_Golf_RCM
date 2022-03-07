<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Category.aspx.cs" Inherits="ORS_RCM.Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../../Styles/base.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/common.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/manager-style.css" rel="stylesheet" type="text/css" />
   <link href="../../Styles/shop_category.css" rel="stylesheet" type="text/css" />
   <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3"></script>
   <script src="../../Scripts/jquery.page-scroller.js"></script>  
    <link href="http://ajax.googleapis.com/ajax/libs/jquery/1.3/jquery.min.js" />
	<link href="https://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js?ver=1.8.3" />
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8 || charCode == 46 || charCode == 44)
            return true;
        else return false;
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="CmnWrapper">
        <p id="toTop"><a href="#CmnContents">▲TOP</a></p>
    <div id="CmnContents">
	<div id="ComBlock">
<div>
    <asp:Button ID="btntreecontrol" runat="server" Text="Expand" 
        onclick="btntreecontrol_Click" Visible ="False"/>
    <asp:TreeView ID="tvCategory" runat="server" ImageSet="Simple" NodeIndent="10" 
        onselectednodechanged="tvCategory_SelectedNodeChanged" 
        ShowLines="True"   NodeWrap="true" CssClass="tree"  Visible="false"   >
                <NodeStyle CssClass="DefaultNodeStyle" />
                     <SelectedNodeStyle CssClass="SelectedNodeStyle" />
    </asp:TreeView>
    </div>
<div>
    <asp:Label ID="lblparnode" runat="server" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Label ID="lbltreenode" runat="server"   Visible="false"></asp:Label><br />
    <asp:HiddenField ID="hidID" runat="server" />
    <asp:HiddenField ID="hfCatID" runat="server" />
    <asp:HiddenField ID="hfoldserialno" runat="server" />
    <asp:HiddenField ID="hfnewserialno" runat="server" />
    <asp:HiddenField ID="hfnewupdate" runat="server" />
    <asp:HiddenField ID="hfparentid" runat="server"/>
          </div>
    <!-- Shop entry -->
    <div class="setDetailBox shopCate iconSet iconEdit">
    <h1 id="head" runat="server"></h1>
    <table class="shopCate editTable">
    	<tbody>
       <tr id="catid" runat ="server" >
           <th>カテゴリID<span>※必須</span></th>
         <td>    
         <asp:TextBox ID="txtcidadd" runat="server" ReadOnly="true" Visible="false" ></asp:TextBox>
          <asp:Label ID="lblcidadd" runat="server" Text="" Visible ="false"></asp:Label>
         </td>
            </tr>
    <tr>
    <th>カテゴリ名<span>※必須</span></th>  
   <td>
   <asp:TextBox ID="txtnodeadd" runat="server" ></asp:TextBox>
       <asp:Label ID="lbldesc" runat="server" Text="" Visible ="false"></asp:Label>
   </td>
   </tr>
   <tr>
   <th>楽天ディレクトリID&nbsp;<span>※必須</span></th>
    <td><asp:TextBox ID="txtrakuten" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
        <asp:Label ID="lblrakuten" runat="server" Text="" Visible ="false"></asp:Label>
    </td>
    </tr>
    <tr>
     <th>ヤフーカテゴリID &nbsp;<span>※必須</span></th>
    <td><asp:TextBox ID="txtyahoo" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
        <asp:Label ID="lblyahoo" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    </tr>
          <asp:TextBox ID="txtserialno" runat="server" Visible="false"  ></asp:TextBox>
         <asp:Label ID="lblseano" runat="server" Text=""  Visible="false"></asp:Label>
     <%--<tr>
             <th>ポンパレカテゴリID&nbsp;<span>※必須</span></th>
      <td><asp:TextBox ID="txtponpare" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
          <asp:Label ID="lblponpare" runat="server" Text="" Visible="false"></asp:Label>
    </td>
    </tr>--%>
    <tr>
        <th>WowmaカテゴリID&nbsp;<span>※必須</span></th>
        <td>
            <asp:TextBox ID="txtwowma" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
            <asp:Label ID="lblwowma" runat="server" Text="" Visible="false"></asp:Label>
         </td>
    </tr>
            <tr>
                <th>ORSカテゴリID&nbsp;<span>※必須</span></th>
                <td>
                <asp:TextBox ID="txtTennis" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
                <asp:Label ID="lblTennis" runat="server" Text="" Visible="false"></asp:Label>
                </td>
            </tr>
 <tr>
     <th>楽天カテゴリセット番号&nbsp;<span>※必須</span></th>
     <td>
     <asp:TextBox ID="txtRcatno" runat="server" ReadOnly="true" onkeypress="return isNumberKey(event)"></asp:TextBox>
              <asp:Label ID="lblRcatno" runat="server" Text="" Visible="false"></asp:Label>
     </td>
     </tr>
     <tr>
     <th>カテゴリコード&nbsp;<span>※必須</span></th>
     <td>
     <asp:TextBox ID="txtjisha" runat="server" onkeypress="return isNumberKey(event)"></asp:TextBox>
              <asp:Label ID="lbljisha" runat="server" Text="" Visible="false"></asp:Label>
     </td>
     </tr>
     <tr>
     <th>カテゴリ名&nbsp;<span>※必須</span></th>
     <td>
     <asp:TextBox ID="txtJcatno" runat="server"></asp:TextBox>
              <asp:Label ID="lblJcatno" runat="server" Text="" Visible="false"></asp:Label>
     </td>
     </tr>
     </tbody>
</table>
    <div class="btn">
		<div class="userRole">
			<input type="submit" id="btnpopup" onclick="this.form.target='" runat="server" style="width:200px"   value="確認画面へ" />
            <asp:Button ID="btnSave" runat="server" Text="確認画面へ" onclick="btnSave_Click"   Visible="false" />
            <asp:Button ID="btnupdate" runat="server" Text="確認画面へ" Visible="false" 
        onclick="btnupdate_Click" />
		</div>     
	</div>  
</div><!-- /Shop entry -->
</div><!--ComBlock-->
   </div><!--CmnContents-->
    </div><!--CmnWrapper-->
    <div >
      <asp:Button ID="btnadd" runat="server" Text="Add" onclick="btnadd_Click" 
        Width="57px"  Visible ="false"/></div> 
<div class="btn"><asp:Button ID="btndelete" runat="server" Text="Delete" onclick="btndelete_Click" Width="57px"  Visible="false"/>
        </div>
     <div class="btn"><asp:Button ID="btnchild" runat="server" Text="Add ChildNode" 
        onclick="btnchild_Click" Visible ="false" />
        </div> 
    </asp:Content>

     


     


        
   


        

      



      




   

