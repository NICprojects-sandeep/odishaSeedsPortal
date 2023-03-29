<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="Subsidy25Per.aspx.cs" Inherits="Reports_Subsidy25Per" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Panel ID="pnlHeader" runat="server">
        <table>
                <tr>
                    <td>
                        Crop Name
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCrop" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Variety Age
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAge" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="ddlAge_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>
                        Source
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSource" runat="server" CssClass="DropdownCss">
                            <asp:ListItem>OSSC</asp:ListItem>
                            <asp:ListItem>NSC</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    
                </tr>
            <tr>
                <td>
                        Year
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlYear" runat="server" CssClass="DropdownCss">
                            <asp:ListItem>2020-21</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        Season
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSeason" runat="server" CssClass="DropdownCss">
                            <asp:ListItem>Rabi</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="ButtonCss" Text="Generate" OnClick="btnSearch_Click" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lbtnSummary" runat="server" PostBackUrl="~/Reports/Subsidy25Per_.aspx">View Summary</asp:LinkButton>
                    </td>
            </tr>
            </table>
    </asp:Panel>
    <asp:Panel ID="pnlDetail" runat="server">
        <div style="overflow: scroll; width: 1170px;">
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
            <table width="100%">
                <tr>
                    <td align="right">
                        <asp:Button ID="btnApprove" runat="server" Text="Approve" OnClick="btnApprove_Click" Height="50px" Width="100px" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" Height="50px" Width="100px"/>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>    
</asp:Content>

