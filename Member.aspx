﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Member.aspx.cs" Inherits="MSDAssignment4.Member" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style3 {
        width: 277px;
    }
    .auto-style4 {
        width: 86px;
    }
    .auto-style5 {
        margin-left: 74px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table style="width:100%;">
    <tr>
        <td class="auto-style4">
            <asp:Label ID="MemberFirstNameLabel" runat="server" Text="Label"></asp:Label>
        </td>
        <td class="auto-style3">
            <asp:Label ID="MemberLastNameLabel" runat="server" Text="Label"></asp:Label>
        </td>
        <td>
            <asp:GridView ID="MemberGridView" runat="server" CellPadding="4" CssClass="auto-style5" ForeColor="#333333" GridLines="None">
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
