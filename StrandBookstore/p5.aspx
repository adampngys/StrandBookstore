<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="p5.aspx.cs" Inherits="StrandBookstore.p5" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ListBox ID="ListBox1" runat="server"></asp:ListBox>
    <br />
    <br />
    <asp:RadioButtonList ID="RadioButtonList1" runat="server"></asp:RadioButtonList>
    <br />
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Assign" />
</asp:Content>
