<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dealer.master" AutoEventWireup="true" CodeFile="RptPaymentReq.aspx.cs" Inherits="Dealer_RptPaymentReq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="tbltxt">
            <div style="float: left; height: 50px;">
                From Date &nbsp;:&nbsp;
              <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="True"
                  CssClass="TextBox" Width="100px" TextMode="Date" />
                To Date &nbsp;:&nbsp;
                <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="True"
                    CssClass="TextBox" Width="100px" TextMode="Date"/>                                
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="128px" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
            </div>
            <br />
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
        </div>
</asp:Content>

