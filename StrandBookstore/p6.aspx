<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="p6.aspx.cs" Inherits="StrandBookstore.p6" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <th>CategoryID</th>
            <th>Title</th>
            <th>Author</th>
            <th>ISBN</th>
            <th>Stock</th>
            <th>Price</th>
        </tr>
        <tr>
            <td>
                <asp:DropDownList ID="insert_ddl_categoryID" runat="server"></asp:DropDownList></td>
            <td>
                <asp:TextBox ID="insert_tb_title" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="insert_tb_author" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="insert_tb_ISBN" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="insert_tb_stock" runat="server"></asp:TextBox></td>
            <td>
                <asp:TextBox ID="insert_tb_price" runat="server"></asp:TextBox></td>
            <td>
                <asp:Button ID="Button2" runat="server" Text="Add New Book" OnClick="Button2_Click" />
            </td>
            <td>
                &nbsp;<asp:Label ID="Label_Validation" runat="server" Text="" ForeColor="Red"></asp:Label>&nbsp;
            </td>
        </tr>
    </table>    
    <br />
    
    <br />
    <hr />
    <asp:Button ID="ButtonViewAllBook" runat="server" Text="View All Books" OnClick="ButtonViewAllBook_Click" />
    <span>Select Author   </span>
    <asp:DropDownList ID="ddl_author" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_author_SelectedIndexChanged" AppendDataBoundItems="true">
    </asp:DropDownList>
    <br />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server" Text="Edit Existing Book"></asp:Label>
    <br />

    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
        DataKeyNames="BookID"
        OnRowDataBound="GridView1_RowDataBound"
        OnRowEditing="GridView1_RowEditing"
        OnRowCancelingEdit="GridView1_RowCancelingEdit"
        OnRowUpdating="GridView1_RowUpdating"
        OnRowDeleting="GridView1_RowDeleting" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="BookID">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" Text='<%# Bind("BookID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <asp:Label ID="Label2" runat="server" Text='<%# Bind("Title") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Author">
                <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("Author") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ISBN">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("ISBN") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="CategoryID">
                <EditItemTemplate>
                    <asp:DropDownList ID="DropDownList1" runat="server"></asp:DropDownList>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("CategoryID") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Stock">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("Stock") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Price">
                <EditItemTemplate>
                    <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                </EditItemTemplate>
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("Price") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ButtonType="Button" ShowEditButton="true" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="true" />
        </Columns>
        <FooterStyle BackColor="#CCCC99" />
        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle BackColor="#F7F7DE" />
        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#FBFBF2" />
        <SortedAscendingHeaderStyle BackColor="#848384" />
        <SortedDescendingCellStyle BackColor="#EAEAD3" />
        <SortedDescendingHeaderStyle BackColor="#575357" />
    </asp:GridView>
    <br />
</asp:Content>
