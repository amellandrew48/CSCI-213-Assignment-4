<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Instructor.aspx.cs" Inherits="MSDAssignment4.Instructor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            margin-left: 0px;
        }

        .auto-style2 {
            width: 377px;
        }

        .auto-style3 {
            width: 444px;
        }

        .auto-style4 {
            width: 287px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
    <tr>
        <td class="auto-style1">
            <asp:Label ID="InstructorFirstNameLabel" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            <asp:Label ID="InstructorLastNameLabel" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            <asp:GridView ID="InstructorGridView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <EditRowStyle BackColor="#7C6F57" />
                <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#E3EAEB" />
                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F8FAFA" />
                <SortedAscendingHeaderStyle BackColor="#246B61" />
                <SortedDescendingCellStyle BackColor="#D4DFE1" />
                <SortedDescendingHeaderStyle BackColor="#15524A" />
            </asp:GridView>
        </td>
    </tr>
    </table>
</asp:Content>
