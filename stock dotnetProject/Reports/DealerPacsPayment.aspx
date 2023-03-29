<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="DealerPacsPayment.aspx.cs" Inherits="Reports_DealerPacsPayment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="titleHeader">
        &nbsp; Dealers PACS Payment report
    </div>
     <br />
    <script>
        function getPrint(print_area) {
            var divToPrint = document.getElementById(print_area);
            var printWin = window.open('', 'Mail In', 'left=0,top=0,width=1000,height=600,status=0');
            printWin.document.write("<div align='center' style='width:1000px;margin:auto;padding-top:10px'>");
            printWin.document.write(divToPrint.outerHTML);
            printWin.document.write("</div>");
            printWin.document.close();
            printWin.focus();
            printWin.print();
        }
    </script>
    <table>
        <tr><td>
            <asp:DropDownList ID="ddlScheme" runat="server">
                <asp:ListItem Enabled="true" Text="All" Value="1"></asp:ListItem>
                <asp:ListItem Text="NFSM" Value="OR1"></asp:ListItem>
                <asp:ListItem Text="State Plan" Value="OR119"></asp:ListItem>
            </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClientClick="return Valid();" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnExport_Click" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" />
            </td>
        </tr>
    </table>
    <div id="divPrint">
    <asp:Literal ID="litReport1" runat="server"></asp:Literal>
        <asp:Literal ID="litReport2" runat="server"></asp:Literal>
        </div>
</asp:Content>

