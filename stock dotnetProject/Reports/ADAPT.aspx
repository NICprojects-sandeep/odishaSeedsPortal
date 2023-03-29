<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ADAPT.master" AutoEventWireup="true" CodeFile="ADAPT.aspx.cs" Inherits="Reports_ADAPT" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div>
            <div class="titleHeader">
        &nbsp;
        Crop Wise Sale
    </div>
        <asp:Panel ID="Panel1" runat="server" CssClass="tbltxt">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                    <table style="width: 100%; padding: 10px;" class="tabletxt" cellpadding="2" cellspacing="2">
                        <tr>
                            <td class="auto-style14">Financial Year</td>
                            <td class="auto-style19">:</td>
                            <td class="auto-style15">
                                <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList></td>
                            <td class="auto-style7">Season </td>
                            <td class="auto-style16">:</td>
                            <td class="auto-style17">
                                <asp:DropDownList ID="drpSeassion" runat="server" CssClass="DropdownCss">
                                    <asp:ListItem Value="0">--All--</asp:ListItem>
                                    <asp:ListItem Value="R">Rabi</asp:ListItem>
                                    <asp:ListItem Value="K">Kharif</asp:ListItem>
                                </asp:DropDownList></td>
                            <td class="auto-style8">
                                <asp:Button ID="btnSearch" runat="server" OnClick="btnSearch_Click" OnClientClick="return Valid();" Text="Search" Width="98px" />
                                <asp:Button ID="btnExport" runat="server" OnClick="btnToExcel_Click" Text="Export to Excel" Width="128px" />
                            </td>
                        </tr>
                    </table>
                    <fieldset class="tbl">

                        <legend style="margin-left: 10px; font-weight: bold;">Figures in Qntl.</legend>
                        <div style="overflow: scroll; width: 1170px;">
                            <asp:Literal ID="litReport" runat="server"></asp:Literal>
                        </div>
                    </fieldset>
                </div>
            </div>
            <div style="height: 30px;">&nbsp;</div>
        </asp:Panel>
    </div>
</asp:Content>

