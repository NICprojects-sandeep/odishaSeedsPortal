<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Scheme.master" AutoEventWireup="true" CodeFile="OSSC_Farmer_Incentive_Pulse.aspx.cs" Inherits="Reports_OSSC_Farmer_Incentive_Pulse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">    
    <table width="100%">
        <tr>
            <td style="font-weight:bold;color:maroon;text-align:center;font-size:15px;">
                Agency : <asp:Label ID="lblAgency" runat="server" Text="" ForeColor="Green"></asp:Label>,&nbsp;&nbsp;&nbsp;&nbsp;
                Crop : <asp:Label ID="lblCrop" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                Financial Year : <asp:DropDownList ID="ddlFinYear" runat="server">
                    <asp:ListItem Text="2017-18" Value="2017-18" />
                    <asp:ListItem Text="2018-19" Value="2018-19" />
                    <asp:ListItem Text="2019-20" Value="2019-20" />
                    <asp:ListItem Text="2020-21" Value="2020-21" />
                                 </asp:DropDownList>
                <asp:Label ID="lblFinYr" runat="server" Text="" ForeColor="Green" Visible="false"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
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

