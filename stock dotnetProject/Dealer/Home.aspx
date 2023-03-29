<%@ Page Title="Dealer Home" Language="C#" MasterPageFile="~/MasterPages/Dealer.master" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Dealer_Home" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="container">
  <div class="row">
    <div class="col-sm-6"><asp:Literal ID="litDealer" runat="server"></asp:Literal></div>
    <div class="col-sm-6"><asp:Literal ID="litFarmer" runat="server"></asp:Literal></div>
  </div>
</div>
</asp:Content>

