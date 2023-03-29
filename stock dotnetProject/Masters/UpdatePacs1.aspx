<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="UpdatePacs.aspx.cs" Inherits="Masters_UpdatePacs" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
        }
        .style6
        {
            color: #FF0000;
        }
    </style>
    <table>
        <tr>
            <td>
                Select District
            </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            </td>
        </tr>
    </table>
    <asp:GridView ID="gvPACS" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gvPACS_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Sl No">
                <ItemTemplate>
                    <%# Container.DataItemIndex + 1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Mod_dist_name" HeaderText="District Name" />
            <asp:BoundField DataField="APP_FIRMNAME" HeaderText="Firm Name" />
            <asp:TemplateField HeaderText="Licence Number">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnLicenceNo" Text='<%#Eval("LIC_NO") %>' runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />                    
                    <asp:Label ID="lbl_FirmName" runat="server" Text='<%#Eval("APP_FIRMNAME") %>' Visible="false"></asp:Label>
                    <asp:Label ID="lbl_LICNO" runat="server" Text='<%#Eval("LIC_NO") %>' Visible="false"></asp:Label> 
                                       
                </ItemTemplate>
                <ControlStyle Width="150px" />
                <FooterStyle Width="150px" />
                <HeaderStyle Width="150px" />
                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="ACCOUNT_NO" HeaderText="Account Number" />
            <asp:BoundField DataField="ACCOUNT_HOLDER_NAME" HeaderText="A/c Holder Name" />
            <asp:BoundField DataField="IFSC_CODE" HeaderText="IFSC" />
            <asp:BoundField DataField="vchBankName" HeaderText="Bank Name" />
            <asp:BoundField DataField="vchBranchName" HeaderText="Branch Name" />
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#E3EAEB" />
        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F8FAFA" />
        <SortedAscendingHeaderStyle BackColor="#246B61" />
        <SortedDescendingCellStyle BackColor="#D4DFE1" />
        <SortedDescendingHeaderStyle BackColor="#15524A" />
    </asp:GridView>
    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" />
                        <cc1:modalpopupextender ID="ModalPopUp" runat="server" PopupControlID="pnlPopUp"
                            TargetControlID="btnShow" BackgroundCssClass="modalBackground">
                        </cc1:modalpopupextender>
    <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" Style="display: none; width: 500px; border-spacing: 0px; padding: 0px; height: 400px; overflow-y: auto; text-align: center; vertical-align: top;"
                            ScrollBars="Auto">
                            <center>
                                <table border="1" style="width: 100%; height:100%; border-spacing: 0px; padding: 0px; background-color: lightblue;">
                                    <tr>
                                        <td style="text-align: center;">
                                            <table border="0" style="font-size: 10pt; font-family: Verdana; padding: 2px; border-spacing: 0px; width: 500px;">
                                                <tr>
                                                    <td colspan="2" style="text-align: center; height: 20px;">
                                                        <h2>Please update the following details of the PACS-<b><asp:Label ID="lblDealer" runat="server"></asp:Label></b></h2>
                                                        <hr /><asp:Label ID="lblLicNo" runat="server" Visible="false"></asp:Label><asp:Label ID="lblDealerName" runat="server" Visible="false"></asp:Label>
                                                    </td>                                                    
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Aadhaar Number
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAadhaar" runat="server" CssClass="TextboxCss" MaxLength="12"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Account Holder Name<span class="style6">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAccHolderName" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Account No.<span class="style6">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAccNo" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        IFSC Code
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtIFSC" runat="server" placeholder="IFSC Code" CssClass="TextboxCss" Style="text-transform: uppercase;" AutoPostBack="true" OnTextChanged="txtIFSC_TextChanged"></asp:TextBox>    
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Bank Name<span class="style6">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBankName" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lblBankId" runat="server" Text="" Visible="false"></asp:Label>
                                                        <%--<asp:DropDownList ID="ddlBankName" runat="server" CssClass="DropdownCss" AutoPostBack="True" onselectedindexchanged="ddlBankName_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        Branch Name<span class="style6">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblBranchName" runat="server" Text=""></asp:Label>
                                                        <asp:Label ID="lblBranchId" runat="server" Text="" Visible="false"></asp:Label>
                                                        <%--<asp:DropDownList ID="ddlBranchName" runat="server" CssClass="DropdownCss" AutoPostBack="True" onselectedindexchanged="ddlBranchName_SelectedIndexChanged"></asp:DropDownList>--%>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center; height: 20px;">
                                                        <br /><br />
                                                        <asp:Button ID="btnConfirm" runat="server" OnClick="btnConfirm_Click" Text="Confirm"
                                                            Width="100px" />
                                                        &nbsp;
                                                <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                                    Width="100px" />
                                                    </td>
                                                </tr>
                                            </table>                                            
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </asp:Panel>
</asp:Content>

