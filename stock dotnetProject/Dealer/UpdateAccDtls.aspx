<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dealer.master" AutoEventWireup="true" CodeFile="UpdateAccDtls.aspx.cs" Inherits="Dealer_UpdateAccDtls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">    
    .style6
    {
        color: #FF0000;
    }    
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="updpnl" runat="server">
        <ContentTemplate>
    <asp:Panel ID="pnlDealerInfo" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="row">
            <div class="col-6">
                <div class="card shadow mb-4 col-lg-12 ">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Licencee Info</h6>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-borderless" id="dataTable" width="100%" cellspacing="0">
                                <tr>
                                    <td>Licence Number</td>
                                    <td>:</td>
                                    <td id="tdlicno" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Firm Name</td>
                                    <td>:</td>
                                    <td id="tdFirmName" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Contact Number</td>
                                    <td>:</td>
                                    <td id="tdContactNo" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Licence Validity</td>
                                    <td>:</td>
                                    <td id="tdLicValidity" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Access Mode</td>
                                    <td>:</td>
                                    <td id="tdAccessMode" runat="server"></td>
                                </tr>                               
                            </table>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-6">
                <div class="card shadow mb-4 col-lg-12 ">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">BANK ACCOUNT DETAILS</h6>
                    </div>
                    <div class="card-body">
                        <div class="table-responsive">
                            <table class="table table-borderless" id="tbl1" runat="server" width="100%" cellspacing="0">
                                <tr>
                                    <td>Account Holder Name<span class="style6">*</span></td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtAccHolderName" runat="server" Width="200px" CssClass="text"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Account No<span class="style6">*</span></td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtAccNo" runat="server" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Bank Name<span class="style6">*</span></td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlBankName" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Branch Name<span class="style6">*</span></td>
                                    <td>:</td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranchName" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>IFSC Code<span class="style6">*</span></td>
                                    <td>:</td>
                                    <td>
                                        <asp:TextBox ID="txtIFSC" runat="server" placeholder="IFSC Code" Width="200px" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>                                
                                <tr>
                                    <td align="center"></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSave" CssClass="btn-info" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                                    </td>
                                </tr>
                                
                            </table>
                            <table class="table table-borderless" id="tbl2" runat="server" width="100%" cellspacing="0">
                                <tr>
                                    <td>Account Holder Name</td>
                                    <td>:</td>
                                    <td id="tdAccName" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Account No</td>
                                    <td>:</td>
                                    <td id="tdAccNo" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Bank Name</td>
                                    <td>:</td>
                                    <td id="tdBankName" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Branch Name</td>
                                    <td>:</td>
                                    <td id="tdBranchName" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>IFSC Code</td>
                                    <td>:</td>
                                    <td id="tdIFSC" runat="server"></td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>