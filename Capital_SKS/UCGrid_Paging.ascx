<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UCGrid_Paging.ascx.cs" Inherits="ORS_RCM.UCGrid_Paging" %>

 <link href=" ../../Styles/pagesno.css" rel="stylesheet" type="text/css" />

 <script type="text/javascript">
     function CallTab1() {
         document.forms[0].target = "";
     }
</script>
<script type="text/javascript">
    function isNumberKey(evt) {
        var charCode = (evt.which) ? evt.which : event.keyCode;
        if ((charCode >= 48 && charCode <= 57) || charCode == 8 || charCode == 46)
            return true;
        else return false;
    }
</script>
<div class="btn pageNav">
	<ul id="ListPaging" class="inlineSet">
		<li class="li">
			<a><asp:LinkButton CssClass="lnk"  ID="lnkPagingFirst" OnClientClick="CallTab1()" runat="server" Text="First"></asp:LinkButton></a>
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingGoPrev" OnClientClick="CallTab1()" runat="server" Text="Prev" ></asp:LinkButton>
		</li>
		<li class="li">   
			<asp:LinkButton CssClass="lnk" ID="lnkPagingPrevMore" OnClientClick="CallTab1()" runat="server" Text="..."></asp:LinkButton>             
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingNo1" OnClientClick="CallTab1()" runat="server" Text="1"></asp:LinkButton>
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingNo2" OnClientClick="CallTab1()" runat="server" Text="2"></asp:LinkButton>
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingNo3" OnClientClick="CallTab1()" runat="server" Text="3" ></asp:LinkButton>
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingNo4" OnClientClick="CallTab1()" runat="server" Text="4"></asp:LinkButton>
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingNo5" OnClientClick="CallTab1()" runat="server" Text="5"></asp:LinkButton>
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingNextMore" OnClientClick="CallTab1()" runat="server" Text="..."  ></asp:LinkButton>
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingGoNext" OnClientClick="CallTab1()" runat="server" Text="Next"></asp:LinkButton>
		</li>
		<li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingLast" OnClientClick="CallTab1()" runat="server" Text="Last"></asp:LinkButton>
        </li>
        <li>
            <asp:Label runat="server"  CssClass="pgText" ID="lblShowPage"></asp:Label>
        </li>
        <li>
        <asp:TextBox ID="txtpage" runat="server" Width="50px"  onkeypress="return isNumberKey(event)"></asp:TextBox>
        </li>
        <li class="li">
			<asp:LinkButton CssClass="lnk" ID="lnkPagingno" OnClientClick="CallTab1()" runat="server" Text="Go"></asp:LinkButton>
        </li>
    </ul>
 
 </div>
 <asp:Label runat="server" Visible="false" ID="lblCurrent" Text="1" />
 <asp:Label runat="server" Visible="false" ID="lblTotal" Text="1"/>
 <asp:Label runat="server" Visible="false" ID="lblTotalRecord" Text="0"></asp:Label>

<asp:HiddenField ID="hfCurrentPageNo" Value="1" runat="server" />
<asp:HiddenField ID="hfTotalRecord" Value="0" runat="server" />
<asp:HiddenField ID="hfOnePageRecord" Value="0" runat="server" />
