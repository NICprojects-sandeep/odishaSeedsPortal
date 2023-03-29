<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="AAO_REPORT_LIST.aspx.cs" Inherits="Reports_AAO_REPORT_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 50%;
            height: 28px;
        }

        .auto-style2 {
            height: 28px;
        }

        .auto-style3 {
            height: 29px;
        }

        .auto-style4 {
            height: 27px;
        }

        .auto-style5 {
            height: 25px;
        }
    </style>
    <table style="width: 100%;">
        <tr>

            <td style="width: 35%"></td>

            <td><strong><em>AAO REPORT LIST</em></strong></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td colspan="2" class="auto-style1">FARMER REGISTERED</td>

            <td class="auto-style2">

                <asp:LinkButton ID="LinkButton4" runat="server" OnClick="LinkButton4_Click">0</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td colspan="2" class="auto-style1">REJECTED/DELETED ENTRY LEVEL</td>

            <td class="auto-style2">
                <asp:Literal ID="Literal4" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>

            <td colspan="2" class="auto-style3">FARMER ID GENERATED</td>
            <td class="auto-style3">
                <asp:LinkButton ID="LinkButton5" runat="server" OnClick="LinkButton5_Click">LinkButton</asp:LinkButton>
            </td>
        </tr>
        <tr>

            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>

            <td colspan="2" class="auto-style4">PENDING FOR REVALIDATION</td>
            <td class="auto-style4">
                <asp:Literal ID="Literal3" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>

            <td colspan="2" class="auto-style4">REJECTION BY CCB</td>
            <td class="auto-style4">
                <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click">LinkButton</asp:LinkButton>
            </td>
        </tr>
        <tr>

            <td colspan="2" class="auto-style2">REJECTION BY PFMS</td>
            <td class="auto-style2">
                <asp:LinkButton ID="LinkButton2" runat="server" OnClick="LinkButton2_Click">LinkButton</asp:LinkButton>
            </td>
        </tr>
        <tr>

            <td colspan="2" class="auto-style5">REJECTION BY DEPT.</td>
            <td class="auto-style5">
                <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">LinkButton</asp:LinkButton>
            </td>
        </tr>
        <tr>

            <td colspan="3" class="auto-style5">
                <hr />
            </td>
        </tr>
        <tr>

            <td colspan="2">PENDING AT PFMS</td>
            <td>
                <asp:Literal ID="Literal5" runat="server"></asp:Literal>
            </td>
        </tr>
        <tr>

            <td colspan="2">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>

