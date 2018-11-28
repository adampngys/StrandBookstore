<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="p1.aspx.cs" Inherits="StrandBookstore.p1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Purchase successful!</h1>
    <h3>Total Amount Paid: 
        <asp:Label ID="Total" runat="server" ValidateRequestMode="Enabled"></asp:Label>
    </h3>
</asp:Content>
