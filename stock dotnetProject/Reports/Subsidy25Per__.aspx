<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="Subsidy25Per__.aspx.cs" Inherits="Reports_Subsidy25Per__" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table>
                <tr>
                    <td>
                        Crop Name
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCrop" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Variety Age
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAge" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlAge_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Source
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSource" runat="server" CssClass="DropdownCss">
                        </asp:DropDownList>
                    </td>
                    
                </tr>
            <tr>
                <td>
                        Year
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="DropdownCss">
                            <asp:ListItem>2020-21</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        Season
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSeason" runat="server" CssClass="DropdownCss">
                            <asp:ListItem>Kharif</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonCss" Text="Generate" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnDownload" runat="server" CssClass="ButtonCss" Text="Download" OnClick="btnDownload_Click" />
                    </td>
            </tr>
            </table>
</asp:Content>

