<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="StrandBookstore._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div style="margin-left: auto; margin-right: auto; text-align: center">
        <asp:AdRotator ID="AdRotator1" runat="server" DataSourceID="XmlDataSource1" />
        <asp:XmlDataSource ID="XmlDataSource1" runat="server" DataFile="/Ad.xml"></asp:XmlDataSource>
        <br />
        <br />
        <asp:Table ID="Table1" runat="server" HorizontalAlign="Center">
            <asp:TableRow>
                <asp:TableCell ForeColor="White" Font-Bold="True" Font-Size="18px" HorizontalAlign="Center" Height="400px" Width="400px" BackColor="OrangeRed">
                    <div style="font-size: 50px">Best Selling</div>
                    <br />
                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/9780425285176.jpg" OnClick="Image1Button_Click" />
                    <br />
                    Talking as Fast as I Can<br />
                    by Lauren Graham
                </asp:TableCell>
                <asp:TableCell ForeColor="White" Font-Size="18px" Font-Bold="True" HorizontalAlign="Center" Height="400px" Width="400px" BackColor="IndianRed">
                    <div style="font-size: 50px;">New Release</div>
                    <br />
                    <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="images/9780062498533.jpg" OnClick="Image2Button_Click" />
                    <br />
                    The Hate U Give<br />
                    by Angie Thomas
                </asp:TableCell>
                <asp:TableCell ForeColor="White" Font-Size="Large" Font-Bold="True" HorizontalAlign="Center" Height="400px" Width="400px" BackColor="MediumVioletRed">
                    For order enquiries:<br />
                    (p) 5555 5555
                <br />
                    (e)
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="mailto:hello@strandbookstore.com" ForeColor="White" Font-Underline="True">
                    hello@strandbookstore.com
                </asp:HyperLink>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
    </div>
</asp:Content>
