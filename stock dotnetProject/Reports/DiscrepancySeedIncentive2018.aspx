<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Scheme.master" AutoEventWireup="true" CodeFile="DiscrepancySeedIncentive2018.aspx.cs" Inherits="Reports_DiscrepancySeedIncentive2018" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table width="100%">
        <tr>
            <td style="font-weight:bold;color:maroon;text-align:center;font-size:15px;">
                Agency : <asp:Label ID="lblAgency" runat="server" Text="" ForeColor="Green"></asp:Label>,&nbsp;&nbsp;&nbsp;&nbsp;
                Crop : <asp:Label ID="lblCrop" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                Financial Year : <asp:Label ID="lblFinYr" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />&nbsp;&nbsp;&nbsp;&nbsp;
                Variety Type : Less than 10 Years
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow: scroll; width: 1170px;">
                                <asp:Literal ID="litReport" runat="server"></asp:Literal>
                            </div>
</asp:Content>

