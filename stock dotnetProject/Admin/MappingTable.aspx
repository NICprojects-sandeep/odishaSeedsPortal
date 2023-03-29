<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/STOCK.master" AutoEventWireup="true" CodeFile="MappingTable.aspx.cs" Inherits="Admin_MappingTable" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Panel ID="Panel1" runat="server">
        <table style="width:100%;background-color:white;">
            <tr>
                <td style="text-align:center;width:25%;">
                    </td>
                <td style="text-align:center;width:50%;">
                    <table  style="text-align:left">
                        <tr>
                            <td>

                                Financial Year</td>
                            <td>

                                <asp:DropDownList ID="ddlFinYear" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>

                                Seassion</td>
                            <td>

                                <asp:DropDownList ID="ddlSeassion" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>

                                District</td>
                            <td>

                                <asp:DropDownList ID="ddlDistrict" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>

                                Crop Name</td>
                            <td>

                                <asp:DropDownList ID="ddlCropName" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>

                                Map Code</td>
                            <td>

                                <asp:DropDownList ID="ddlMapCode" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td>

                            </td>
                            <td>

                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" Width="100px" />

                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align:center;width:25%;">
                    </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>

