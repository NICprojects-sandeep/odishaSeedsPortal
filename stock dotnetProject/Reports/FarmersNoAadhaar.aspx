<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="FarmersNoAadhaar.aspx.cs" Inherits="Reports_FarmersNoAadhaar" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <center>
        <asp:UpdatePanel ID="upFarmerDtls" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
    <div id="divPrint">
    <asp:Panel ID="pnlContents" runat="server" Width="800px">
    <fieldset>
    <center class="style1">
        <table>
            <tr>
                <td>
                    Select District:
                </td>
                <td>
                    <asp:DropDownList ID="ddlDistrict" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDistrict_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
        </table>
    </center>
    <br /><asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
        </fieldset>
        </asp:Panel>
        </div>    
                </ContentTemplate>
        </asp:UpdatePanel>   
    </center>
    <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="upFarmerDtls" DisplayAfter="0">
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
    </asp:UpdateProgress>
</asp:Content>

