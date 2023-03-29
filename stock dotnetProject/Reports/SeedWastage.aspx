<%@ Page Title="Seed Wastage" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="SeedWastage.aspx.cs" Inherits="Reports_SeedWastage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td style="width: 100px;"></td>
                    <td>
                        <table style="width: 100%">
                            <tr>
                                <td style="font-size: 20px">Financial Year
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFinYear" runat="server" CssClass="DropdownCss" AutoPostBack="true" OnSelectedIndexChanged="ddlFinYear_SelectedIndexChanged" Font-Size="20px" Height="35px"></asp:DropDownList>
                                </td>
                                <td style="font-size: 20px">Seassion
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlSeassion" runat="server" CssClass="DropdownCss" AutoPostBack="true" OnSelectedIndexChanged="ddlSeassion_SelectedIndexChanged" Font-Size="20px" Height="35px"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        
                    </td>
                    <td style="width: 100px;"></td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="font-size: 20px;width:80%;">Wastage of crop at dealer point during financial year
                                    <asp:Label ID="lblDealerFinYr" runat="server" Text=""></asp:Label>
                        and seassion
                                    <asp:Label ID="lblDealerSeassion" runat="server" Text="Label"></asp:Label>
                        in Qtl
                    </td>
                    <td>
                        <asp:Button ID="btnDearer" runat="server" CssClass="ButtonCss" Font-Size="20px" Height="35px" Text="0.00" OnClick="btnDearer_Click"></asp:Button></td>
                </tr>
                <tr>
                    <td style="font-size: 20px;width:80%;">Wastage of crop at supplier point during financial year
                                    <asp:Label ID="lblSupplierFinYr" runat="server" Text=""></asp:Label>
                        and seassion
                                    <asp:Label ID="lblSupplierSeassion" runat="server" Text="Label"></asp:Label>
                        in Qtl
                    </td>
                    <td>
                        <asp:Button ID="btnSupplier" runat="server" CssClass="ButtonCss" Font-Size="20px" Height="35px" Text="0.00" OnClick="btnSupplier_Click"></asp:Button></td>
                </tr>
            </table>
            
            
            <%--<asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="updatepanel"
                DisplayAfter="0">
                <ProgressTemplate>
                    <div style="text-align: center; vertical-align: middle; position: absolute; top: 0px; left: 0px; z-index: 99; width: 100%; height: 1000px; background-color: #ededed; -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)'; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60); -moz-opacity: 0.8; opacity: 0.8;"
                        visible="true">
                        <div style="width: 48px; height: 48px; margin: 0 auto; margin-top: 275px;">
                            <asp:Image ImageUrl="~/images/loading.gif" Width="48px" Height="48px" runat="server"
                                ID="TaskLoadingImage" />
                        </div>
                        <div style="font-family: Trebuchet MS; font-size: 12px; color: Red; text-align: center;">
                            Please Wait ...
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
        </ContentTemplate>
        </ajax:UpdatePanel>
</asp:Content>

