<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="RTP_FARMERREDGDTL.aspx.cs" Inherits="Reports_RTP_FARMERREDGDTL" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
        <div class="titleHeader">
        &nbsp; Farmer Registration
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
        <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px;" CssClass="tbltxt">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                    <table style="width: 100%; padding: 10px;" class="tabletxt" cellpadding="10" cellspacing="10">
                        <tr>


                            <td style="text-align: right; padding-left: 50px;">
                                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
                            </td>
                        </tr>

                    </table>
                    <fieldset class="tbl">

                        <legend style="margin-left: 10px; font-weight: bold;">Figures in Nos.</legend>
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

