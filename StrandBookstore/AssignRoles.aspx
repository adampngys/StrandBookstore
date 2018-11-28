<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssignRoles.aspx.cs" Inherits="StrandBookstore.AssignRoles" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <br />
            <br />
            <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
            <br />
            <br />
            <asp:RadioButtonList ID="RadioButtonList1" runat="server"></asp:RadioButtonList>
            <br />
            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Assign" />
        </div>
    </form>
</body>
</html>
