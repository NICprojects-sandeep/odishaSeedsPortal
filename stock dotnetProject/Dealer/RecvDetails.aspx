<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dealer.master" AutoEventWireup="true" CodeFile="RecvDetails.aspx.cs" Inherits="Dealer_RecvDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container-fluid">
        <asp:Panel ID="Panel1" runat="server" Style="margin-top: 15px;">
            <div class="card shadow mb-4">
                <div class="card-header py-3">
                    <div class="row">
                        <div class="col-lg-2">
                            From Date
                        </div>
                        <div class="col-lg-3">
                            <asp:TextBox ID="txtFrmDt" runat="server" CausesValidation="false" TextMode="Date"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtFrmDt" EnableClientScript="False" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-lg-2">
                            To Date
                        </div>
                        <div class="col-lg-3">
                            <asp:TextBox ID="txtToDt" runat="server" TextMode="Date"></asp:TextBox>
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="True"
                                ControlToValidate="txtToDt" EnableClientScript="False" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                        </div>
                        <div class="col-lg-2">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                            &nbsp;<asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                        </div>
                    </div>
                </div>
            </div>
            <%--<div class="card shadow mb-4">
                <div class="card-header py-3">
                    <div class="table-responsive">--%>                        
                        
                    <%--</div>
                </div>
            </div>--%>
        </asp:Panel>
    </div>
    <asp:Literal ID="litReport" runat="server"></asp:Literal>
</asp:Content>

