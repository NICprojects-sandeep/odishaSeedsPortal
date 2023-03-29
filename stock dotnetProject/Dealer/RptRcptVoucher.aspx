<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dealer.master" AutoEventWireup="true" CodeFile="RptRcptVoucher.aspx.cs" Inherits="Dealer_RptRcptVoucher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    Cash Memo Number : <asp:Label ID="lblCashmemono" runat="server" Text="Label"></asp:Label>
    <br />
    <asp:Literal ID="litReport" runat="server"></asp:Literal>
</asp:Content>

