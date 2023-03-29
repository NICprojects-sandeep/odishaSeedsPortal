<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="DataTransaction.aspx.cs" Inherits="Reports_DataTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
         <div class="titleHeader">
        &nbsp; PFMS Status
    </div>
    <br />
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
    </script>
    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" />
    <div id="divPrint">
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
    </div>
</asp:Content>

