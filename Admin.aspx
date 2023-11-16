<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="MSDAssignment4.Admin" %>
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
            <td class="auto-style3">
                <asp:GridView ID="AdminMemberView" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:ButtonField CommandName="Cancel" Text="Select" />
                    </Columns>
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
            <td class="auto-style2">                Add Member<br />
                <asp:TextBox ID="MemberFirstName" runat="server"></asp:TextBox>&nbsp;<asp:Label ID="Label1" runat="server" Text="First Name"></asp:Label><br />
                <asp:TextBox ID="MemberLastName" runat="server"></asp:TextBox>&nbsp;<asp:Label ID="Label2" runat="server" Text="Last Name"></asp:Label><br />
                <asp:TextBox ID="MemberPhoneNumber" runat="server"></asp:TextBox>&nbsp;<asp:Label ID="Label3" runat="server" Text="Phone Number"></asp:Label><br />
                <asp:TextBox ID="MemberDateJoined" runat="server"></asp:TextBox>&nbsp;<asp:Label ID="Label4" runat="server" Text="Date Joined(MM/DD/YYYY)"></asp:Label><br />
                <asp:Button ID="AddMemberButton" runat="server" Text="Add Member" OnClick="AddMemberButton_Click" />
            </td>
            <td class="auto-style4">Delete Selected Member<br />
                <br />
                <asp:Button ID="DeleteMemberButton" runat="server" Text="Delete Member" />
            </td>
            <td>Assign Selected Members<br />
                <br />
                <asp:TextBox ID="SectionAssignMember" runat="server"></asp:TextBox>
&nbsp;<asp:Label ID="Label7" runat="server" Text="Section"></asp:Label>
                <br />
                <asp:Button ID="AssignMembersButton" runat="server" Text="Assign Members" />
                <br />
            </td>
        </tr>
        <tr>
            <td class="auto-style3">
                <br />
                <asp:GridView ID="AdminInstructorView" runat="server" CellPadding="4" CssClass="auto-style1" ForeColor="#333333" GridLines="None" >
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:ButtonField CommandName="Cancel" Text="Select" />
                    </Columns>
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
            <td class="auto-style2">Add Instructor<br />
                <asp:TextBox ID="InstructorFirstName" runat="server"></asp:TextBox>
&nbsp;<asp:Label ID="Label5" runat="server" Text="First Name"></asp:Label>
                <br />
                <asp:TextBox ID="InstructorLastName" runat="server"></asp:TextBox>
&nbsp;<asp:Label ID="Label6" runat="server" Text="Last Name"></asp:Label>
                <br />
                <asp:Button ID="AddInstructorButton" runat="server" Text="Add Instructor" />
            </td>
            <td class="auto-style4">Delete Selected Instructor<br />
                <br />
                <asp:Button ID="DeleteInstructorButton" runat="server" Text="Delete Instructor" />
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
