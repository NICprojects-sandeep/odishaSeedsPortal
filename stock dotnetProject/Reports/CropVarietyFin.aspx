<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="CropVarietyFin.aspx.cs" Inherits="Reports_CropVarietyFin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table width="100%">
        <tr>
            <td style="font-weight:bold;color:maroon;text-align:center;font-size:15px;">
                Financial Year :&nbsp;<asp:DropDownList ID="ddlFinYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged"></asp:DropDownList>&nbsp;&nbsp;&nbsp;&nbsp;
                Season :&nbsp; <asp:DropDownList ID="ddlSeason" runat="server"></asp:DropDownList>
                Dealer Type :&nbsp; <asp:DropDownList ID="ddlType" runat="server">
                    <asp:ListItem>All</asp:ListItem>
                    <asp:ListItem>Dealer</asp:ListItem>
                    <asp:ListItem>PACS</asp:ListItem>
                </asp:DropDownList>
                &nbsp;<asp:Label ID="lblFinYr" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                &nbsp;<asp:Label ID="lblSeason" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />&nbsp;&nbsp;&nbsp;&nbsp;
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow: scroll; width: 1170px;">
                                <asp:Literal ID="litReport" runat="server"></asp:Literal>
                            </div>
</asp:Content>

