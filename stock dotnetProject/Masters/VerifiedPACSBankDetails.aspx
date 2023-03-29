<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="VerifiedPACSBankDetails.aspx.cs" Inherits="Masters_VerifiedPACSBankDetails" %>
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
            <td>Dealer Type
            </td>
            <td>
                <asp:RadioButtonList ID="rbtnlDealerType" CssClass="DropdownCss" AutoPostBack ="true" runat="server" OnSelectedIndexChanged="rbtnlDealerType_SelectedIndexChanged" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Dealer</asp:ListItem>
                    <asp:ListItem>PACS</asp:ListItem>
                    <asp:ListItem>ALL</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvPACS" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" Height="170px" Width="100%" OnRowDataBound="gvPACS_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Sl No.">
                <ItemTemplate>
                    <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:BoundField DataField="LIC_NO" HeaderText="Licence Number" />            
            <asp:BoundField DataField="APP_FIRMNAME" HeaderText="Firm Name" />
            <asp:BoundField DataField="VCHBANKNAME" HeaderText="Bank Name" />
            <asp:BoundField DataField="IFSC_CODE" HeaderText="IFSC Code" />
            <asp:BoundField DataField="ACC_NO" HeaderText="Account No" />
            <asp:BoundField DataField="STATUS" HeaderText="Status" /> 
            <asp:BoundField DataField="Rejection_Reason_Narration" HeaderText="Reason for Rejection" />             
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
    
</asp:Content>

