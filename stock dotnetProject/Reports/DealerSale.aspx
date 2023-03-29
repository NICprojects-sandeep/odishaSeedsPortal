<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DealerSale.aspx.cs" Inherits="Reports_DealerSale" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/sms.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
              <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="tbltxt">
            <div style="float: left; height: 50px;">
                From Date &nbsp;:&nbsp;
              <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="True"
                  CssClass="TextBox" Width="100px" />
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server"
                    TargetControlID="txtFromDate" WatermarkCssClass="watermarked"
                    WatermarkText="dd/mm/yyyy">
                </asp:TextBoxWatermarkExtender>
                <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtFromDate">
                </asp:CalendarExtender>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                    ControlToValidate="txtFromDate" Display="Dynamic" ErrorMessage="invalid date"
                    ForeColor="Red" SetFocusOnError="true"
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$">Date Format (dd/mm/yyyy)</asp:RegularExpressionValidator>


                To Date &nbsp;:&nbsp;
                <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="True"
                    CssClass="TextBox" Width="100px" />
                <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server"
                    TargetControlID="txtToDate" WatermarkCssClass="watermarked"
                    WatermarkText="dd/mm/yyyy">
                </asp:TextBoxWatermarkExtender>
                <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                    TargetControlID="txtToDate">
                </asp:CalendarExtender>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                    ControlToValidate="txtFromDate" Display="Dynamic" ErrorMessage="invalid date"
                    ForeColor="Red" SetFocusOnError="true"
                    ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$">Date Format (dd/mm/yyyy)</asp:RegularExpressionValidator>
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="128px" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
            </div>
            <br />
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
        </div>
    </form>
</body>
</html>
