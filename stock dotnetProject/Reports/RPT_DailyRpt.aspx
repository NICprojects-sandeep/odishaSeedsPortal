<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="RPT_DailyRpt.aspx.cs" Inherits="Reports_RPT_DailyRpt" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <div class="titleHeader">
        &nbsp;
   Daily Report
    </div>
    <br />
  
    <style>
        .tbltxt {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000;
            padding: 3px;
        }

    </style>

    <div>
        <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" CssClass="tbltxt" Style="padding-top: 30px;">
                    <div style="width: 1220px; background-color: #666; padding: 2px;">
                        <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                            <table style="width: 100%; padding: 0px;" class="tabletxt" cellpadding="10" cellspacing="0">
                            
                                <tr>
                               
                                    <td>&nbsp;</td>
                                    <td style="width:200px;" >Select Month-Date(MM-DD)<span class="mandatory">*</span>:<asp:TextBox ID="txtfromdate" Width="100px" runat="server" CssClass="TextboxCss"></asp:TextBox>
                                        <asp:CalendarExtender ID="fromDatecalendarExtender" runat="server" TargetControlID="txtfromdate"
                                            Format="MM-dd">
                                        </asp:CalendarExtender>

                                     
                                    </td>



                                    <td >
                                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                                        <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />

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
                <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="updatepanel"
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
                </asp:UpdateProgress>
            </ContentTemplate>
            <Triggers>

                <ajax:PostBackTrigger ControlID="btnExport" />
<asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
                <asp:PostBackTrigger ControlID="btnExport"></asp:PostBackTrigger>
            </Triggers>
        </ajax:UpdatePanel>
        </div>
</asp:Content>

