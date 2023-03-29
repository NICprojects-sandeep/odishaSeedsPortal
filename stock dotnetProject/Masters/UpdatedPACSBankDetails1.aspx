<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="UpdatedPACSBankDetails.aspx.cs" Inherits="Masters_UpdatedPACSBankDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
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
            <td>District Name
            </td>
            <td>
                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropdownCss" AutoPostBack="true" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <%--<td>Dealer Type
            </td>
            <td>
                <asp:RadioButtonList ID="rbtnlDealerType" CssClass="DropdownCss" AutoPostBack ="true" runat="server" OnSelectedIndexChanged="rbtnlDealerType_SelectedIndexChanged" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Dealer</asp:ListItem>
                    <asp:ListItem>PACS</asp:ListItem>
                    <asp:ListItem>ALL</asp:ListItem>
                </asp:RadioButtonList>
            </td>--%>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvPACS" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="170px" Width="80%" OnRowCommand="gvPACS_RowCommand">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Sl No.">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>            
            <asp:TemplateField HeaderText="Firm Name">
                <ItemTemplate>
                    <asp:Label ID="lbl_FirmName" runat="server" Text='<%#Eval("APP_FIRMNAME") %>'></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="250px" />
                <FooterStyle Width="250px" />
                <HeaderStyle Width="250px" HorizontalAlign="Left" />
                <ItemStyle HorizontalAlign="Left" Width="250px"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Licence Number">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnLicenceNo" Text='<%#Eval("LIC_NO") %>' runat="server" CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>" />
                    <asp:Label ID="lbl_LICNO" runat="server" Text='<%#Eval("LIC_NO") %>' Visible="false"></asp:Label>
                </ItemTemplate>
                <ControlStyle Width="150px" />
                <FooterStyle Width="150px" />
                <HeaderStyle Width="150px" />
                <ItemStyle HorizontalAlign="Center" Width="150px"></ItemStyle>
            </asp:TemplateField>
            <asp:BoundField DataField="vchBankName" HeaderText="Bank Name" />
            <asp:BoundField DataField="VCHBRANCHNAME" HeaderText="Branch Name" >
            <ControlStyle Width="150px" />
            <FooterStyle HorizontalAlign="Left" Width="150px" />
            <HeaderStyle HorizontalAlign="Left" Width="150px" />
            <ItemStyle HorizontalAlign="Left" Width="150px" />
            </asp:BoundField>
            <asp:BoundField DataField="ACC_NO" HeaderText="Account No" >            
            
            <ControlStyle Width="150px" />
            <FooterStyle HorizontalAlign="Left" Width="150px" />
            <HeaderStyle HorizontalAlign="Left" Width="150px" />
            <ItemStyle HorizontalAlign="Left" Width="150px" />
            </asp:BoundField>
            
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup" Style="display: none" />
    <cc1:ModalPopupExtender ID="ModalPopUp" runat="server" PopupControlID="pnlPopUp"
        TargetControlID="btnShow" BackgroundCssClass="modalBackground">
    </cc1:ModalPopupExtender>
    <asp:Panel ID="pnlPopUp" runat="server" CssClass="modalPopup" Style="display: none; width: 500px; border-spacing: 0px; padding: 0px; height: 400px; overflow-y: auto; text-align: center; vertical-align: top;"
        ScrollBars="Auto">
        <center>
            <table border="1" style="width: 100%; height: 100%; border-spacing: 0px; padding: 0px; background-color: lightblue;">
                <tr>
                    <td style="text-align: center;">
                        <table border="0" style="font-size: 10pt; font-family: Verdana; padding: 2px; border-spacing: 0px; width: 500px;">
                            <tr>
                                <td colspan="2" style="text-align: center; height: 20px;">
                                    <h2>Please update the following details of the PACS-<b><asp:Label ID="lblDealer" runat="server"></asp:Label></b></h2>
                                    <hr />
                                    <asp:Label ID="lblLicNo" runat="server" Visible="false"></asp:Label><asp:Label ID="lblDealerName" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>Aadhaar Number
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAadhaar" runat="server" CssClass="TextboxCss" MaxLength="12"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Account Holder Name<span class="style6">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccHolderName" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Account No.<span class="style6">*</span>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Bank Name<span class="style6">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBankName" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>Branch Name<span class="style6">*</span>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlBranchName" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>IFSC Code
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIFSC" runat="server" placeholder="IFSC Code" CssClass="TextboxCss" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="text-align: center; height: 20px;">
                                    <br />
                                    <br />
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

