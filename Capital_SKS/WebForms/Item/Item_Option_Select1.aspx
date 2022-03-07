<%@ Page Title="商品管理システム＜オプション＞" Language="C#" AutoEventWireup="true" CodeBehind="Item_Option_Select1.aspx.cs" Inherits="ORS_RCM.WebForms.Item.Item_Option_Select1" %>

<!DOCTYPE html>
<html>
<head runat="server">
<meta charset="UTF-8">
<meta http-equiv="X-UA-Compatible" content="IE=edge" />

<link rel="stylesheet" href="../../Styles/base.css" />
<link rel="stylesheet" href="../../Styles/common.css" />
<link rel="stylesheet" href="../../Styles/manager-style.css" />
<link rel="stylesheet" href="../../Styles/item.css" />

 <script type="text/javascript">
        function RadioCheck(rb) {
            var dl = document.getElementById("<%=DataList1.ClientID%>");
            var rbs = dl.getElementsByTagName("input");
            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }
        }
</script>

<title>商品管理システム＜オプション登録＞</title>
</head>
<body class="clNon">
    <div id="PopWrapper">
            <h1>オプション登録</h1>
            <div id="PopContents" class="pop2_Option inlineSet">
            <form id="form1" runat="server">
                <div class="OptionBlock">
                <asp:DataList ID="DataList1" runat="server" CssClass="OptionBlock"  RepeatColumns="1" RepeatDirection="Vertical">
                    <ItemTemplate>
                        <asp:RadioButton  ID="rdoOption" runat="server" onclick="RadioCheck(this)"/>
                        <dl>
                            <dt><asp:Label runat="server" ID="lblType" Text="グループ名"></asp:Label></dt>
                            <dd><asp:Label runat="server" ID="lblGroup" Text='<%# Eval("Option_GroupName") %>'></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt>項目名</dt><dd><asp:Label ID="lblName" runat="server" Text='<%# Eval("name1") %>'></asp:Label></dd>
                            <dt>選択肢</dt><dd><asp:Label ID="lblValue" runat="server" Text=' <%# Eval("value1") %>'></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt>項目名</dt><dd><asp:Label ID="TextBox1" runat="server" Text='<%# Eval("name2") %>'></asp:Label></dd>
                            <dt>選択肢</dt><dd><asp:Label ID="TextBox2" runat="server" Text=' <%# Eval("value2") %>'></asp:Label></dd>
                        </dl>
                        <dl>
                            <dt>項目名</dt><dd><asp:Label ID="TextBox3" runat="server" Text='<%# Eval("name3") %>'></asp:Label></dd>
                            <dt>選択肢</dt><dd><asp:Label ID="TextBox4" runat="server" Text=' <%# Eval("value3") %>'></asp:Label></dd>
                        </dl>
            </ItemTemplate>
        </asp:DataList>
<div class="btn">
<asp:Button runat="server" ID="btnClose" Text="決定" Width="150px" OnClick="btnClose_Click" />
</div>
</div>
</form>
            </div>
    </div>
</body>
</html>
