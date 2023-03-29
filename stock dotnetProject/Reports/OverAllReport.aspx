<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="OverAllReport.aspx.cs" Inherits="Reports_OverAllReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp; Farmer Registration Breakup
    </div>
    <br />
    <table style="width: 70%; margin: 0 auto">
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 31px">
                <strong>Total Registered</strong></td>
            <td style="height: 31px">
                <asp:LinkButton ID="HyperLink1" runat="server" OnClick="HyperLink1_Click"></asp:LinkButton>

            </td>
            <td style="height: 31px"></td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 34px">
                <strong>Rejected or Deleted By PACS/LAMPS</strong></td>
            <td style="height: 34px">
                <asp:LinkButton ID="HyperLink2" runat="server" OnClick="HyperLink2_Click"></asp:LinkButton></td>
            <td style="height: 34px"></td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Farmer ID Generated</strong></td>
            <td style="height: 39px">
                <asp:LinkButton ID="HyperLink3" runat="server" OnClick="HyperLink3_Click"></asp:LinkButton></td>
            <td style="height: 39px"></td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Total Rejected or Pending</strong></td>
            <td style="height: 39px">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label></td>
            <td style="height: 39px"></td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Rejected By CCB</strong></td>
            <td style="height: 39px">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click"></asp:LinkButton></td>
            <td style="height: 39px"></td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Pending at CCB</strong></td>
            <td style="height: 39px">
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click"></asp:LinkButton></td>
            <td style="height: 39px"></td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Pending at NIC/Dept</strong></td>
            <td style="height: 39px">
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click"></asp:LinkButton></td>
            <td style="height: 39px"></td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Rejected by PFMS (without out the CCB cases)</strong></td>
            <td style="height: 39px">
                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click"></asp:LinkButton></td>
            <td style="height: 39px"></td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Pending at PFMS</strong></td>
            <td style="height: 39px">
                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click"></asp:LinkButton></td>
            <td style="height: 39px"></td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Rejected by NIC/Dept</strong></td>
            <td style="height: 39px">
                <asp:LinkButton ID="LinkButton6" runat="server" OnClick="LinkButton6_Click"></asp:LinkButton></td>
            <td style="height: 39px"></td>
        </tr>
        <tr>
            <td style="font-size: medium; height: 39px"><strong>Rejected due to non integrated Banks (Federal Bank, URBAN COOP BANK, State Bank of India [Evening Branch])</strong></td>
            <td style="height: 39px">
                <asp:LinkButton ID="LinkButton7" runat="server" OnClick="LinkButton7_Click"></asp:LinkButton></td>
            <td style="height: 39px"></td>
        </tr>

    </table>

</asp:Content>

