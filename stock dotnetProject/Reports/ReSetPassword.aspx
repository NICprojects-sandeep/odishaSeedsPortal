<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="ReSetPassword.aspx.cs" Inherits="Reports_ReSetPassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table>
        <tr>
            <td>
                User Type
            </td>
            <td>
                :
            </td>
            <td>
                <asp:DropDownList ID="ddlUserType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUserType_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="ddlUser" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                User ID  :
            </td>
            <td>
                <asp:Label ID="lblUserID" runat="server" Font-Bold="True"></asp:Label>
                <asp:Label ID="lblUserID1" runat="server" Visible="false" ></asp:Label>
                <asp:Label ID="lblUserType1" runat="server" Visible="false"></asp:Label>
            </td>
            </tr>
            <tr>
            <td colspan="2">                
                <asp:Button ID="btnGenerate" runat="server" Text="Generate Password" OnClick="btnGenerate_Click" />                
            </td>
                </tr>
                <tr>
            <td>
                New Password  :
            </td>
            <td>
                <asp:Label ID="lblPassword" runat="server" Font-Bold="True"></asp:Label>
            </td>
        </tr>        
    </table>
</asp:Content>

