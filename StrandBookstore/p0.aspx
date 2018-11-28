<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="p0.aspx.cs" Inherits="StrandBookstore.p0" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Shopping Cart</h1>
     <br />
        <asp:Label ID="Status" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <br />
        <br />
        <asp:GridView ID="GridView1" runat="server"
            AutoGenerateColumns="False"
            Width="1000px"
            BorderStyle="None"            
            CaptionAlign="Top"
            CellPadding="4"
            GridLines="Vertical"
            DataKeyNames="OrderID" BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" ForeColor="Black">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="OrderID">
                    <ItemTemplate>
                        <asp:Label ID="Label_myOrderID" runat="server" Text='<%# Bind("OrderID") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Image">
                    <ItemTemplate>
                        <image src="images/<%# Eval("ISBN") %>.jpg" width="90" height="100"></image>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Title">
                    <ItemTemplate>
                        <asp:Label ID="Label_myTitle" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ISBN">
                    <ItemTemplate>
                        <asp:Label ID="Label_myISBN" runat="server" Text='<%# Bind("ISBN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Author">
                    <ItemTemplate>
                        <asp:Label ID="Label_myAuthor" runat="server" Text='<%# Bind("Author") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Price">
                    <ItemTemplate>
                        <asp:Label ID="Label_myPrice" runat="server" Text='<%# Bind("Price", "{0:C}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Quantity" SortExpression="Quantity">
                    <ItemTemplate>
                        <asp:DropDownList ID="DropDownList1" AutoPostBack="true" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" runat="server">
                            <asp:ListItem>0</asp:ListItem>
                            <asp:ListItem>1</asp:ListItem>
                            <asp:ListItem>2</asp:ListItem>
                            <asp:ListItem>3</asp:ListItem>
                            <asp:ListItem>4</asp:ListItem>
                            <asp:ListItem>5</asp:ListItem>
                        </asp:DropDownList>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:TemplateField>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="true" OnCheckedChanged="CheckBox1_CheckedChanged" />
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="Button_DeleteItem" runat="server" Text="Remove Item" OnClick="Remove_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle Width="20px" HorizontalAlign ="Center" BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>

        <asp:Label ID="Label2" runat="server" Text="" ForeColor="Red"></asp:Label><br />

        <span>Subtotal:  $ </span>
        <asp:Label ID="Label1" runat="server"></asp:Label>

        <br />
        <br />
        <asp:Button ID="CheckOut" runat="server" Text="CheckOut" OnClick="CheckOut_Click" />
</asp:Content>
