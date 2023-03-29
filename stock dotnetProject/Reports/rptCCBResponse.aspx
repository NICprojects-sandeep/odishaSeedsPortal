<%@ Page Language="C#" AutoEventWireup="true" CodeFile="rptCCBResponse.aspx.cs" Inherits="Reports_rptCCBResponse" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="../css/otb-admin.css" media="all" type="text/css" />
    <script type="text/javascript">

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

        function popUp(URL) {
            day = new Date();
            id = day.getTime();
            eval("page" + id + " = window.open(URL, '" + id + "', 'toolbar=0,scrollbars=1,location=0,statusbar=0,menubar=0,resizable=0,width=900,height=500,left = 190,top = 154');");
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:right;"> <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" /></div>
        <div>
           
            <div id="divPrint">
                <asp:Literal ID="litReport" runat="server"></asp:Literal>
            </div>
        </div>
    </form>
</body>
</html>
