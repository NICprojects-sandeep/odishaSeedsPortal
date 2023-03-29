﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealerSaleDtl.aspx.cs" Inherits="Reports_DealerSaleDtl" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="../css/sms.css" type="text/css" />
    <link rel="stylesheet" href="../css/otb-admin.css" media="all" type="text/css" />
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlPrintArea.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
</head>
<body style="background-image:none;">
    <form id="form1" runat="server">
        <div>
            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="return PrintPanel();" />
            <asp:Panel ID="pnlPrintArea" runat="server" Width="100%">
                <fieldset class="tbl">

                    <legend style="margin-left: 10px; font-weight: bold;">Sale Details</legend>
                    <asp:Literal ID="litReport" runat="server"></asp:Literal>
                </fieldset>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
