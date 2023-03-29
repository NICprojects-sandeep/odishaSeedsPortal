<%@ Page Title="Subsidy Involved" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="Rpt_SubsidyInvolved.aspx.cs" Inherits="Reports_Rpt_SubsidyInvolved" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <table width="50%" class="tabletxt">
        <tr>
            <td>
               Year
            </td>
            <td>
                <asp:DropDownList ID="ddlFinYr" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFinYr_SelectedIndexChanged" style="height: 22px"></asp:DropDownList>
            </td>
            <td>
                Season
            </td>
            <td>
                <asp:DropDownList ID="ddlSeassion" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            </td>
            <td>
                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow: scroll; width: 1170px;">
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
    </div>
</asp:Content>

