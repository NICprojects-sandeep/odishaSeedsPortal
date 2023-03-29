<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="RPT_DistwisePaymentDetails.aspx.cs" Inherits="Reports_RPT_DistwisePaymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp;  Distwise Payment Details
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

    <asp:UpdatePanel ID="upStockOB" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            Session Year&nbsp; :&nbsp; 
            <asp:DropDownList ID="drpSessionYr" runat="server" CssClass="DropdownCss">
                                    </asp:DropDownList>
            &nbsp; Season&nbsp; :&nbsp;
            <asp:DropDownList ID="drpSeason" CssClass="DropdownCss" runat="server">
                <asp:ListItem Value="0">--All--</asp:ListItem>
                <asp:ListItem Value="K">Kharif</asp:ListItem>
                <asp:ListItem Value="R">Rabi</asp:ListItem>
            </asp:DropDownList>
            &nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
            <%--<asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" />--%>
            <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
            <%--<div id="divPrint">
                
            </div>--%>
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="upStockOB"
        DisplayAfter="0">
        <ProgressTemplate>
            <div style="text-align: center; vertical-align: middle; position: absolute; top: 0px; left: 0px; z-index: 99; width: 100%; height: 1000px; background-color: #ededed; -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)'; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60); -moz-opacity: 0.8; opacity: 0.8;"
                visible="true">
                <div style="width: 48px; height: 48px; margin: 0 auto; margin-top: 275px;">
                    <asp:Image ImageUrl="~/images/loading.gif" Width="48px" Height="48px" runat="server"
                        ID="TaskLoadingImage" />
                </div>
                <div style="font-family: Trebuchet MS; font-size: 12px; color: Red; text-align: center;">
                    Please Wait ...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

