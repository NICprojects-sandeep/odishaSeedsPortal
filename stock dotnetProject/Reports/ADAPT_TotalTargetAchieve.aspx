<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ADAPT.master" AutoEventWireup="true" CodeFile="ADAPT_TotalTargetAchieve.aspx.cs" Inherits="Reports_ADAPT_TotalTargetAchieve" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 115px;
            height: 20px;
        }

        .auto-style2 {
            width: 5px;
            height: 20px;
        }

        .auto-style3 {
            width: 247px;
            height: 20px;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <Triggers>

            <asp:PostBackTrigger ControlID="btnExport" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px; height: 500px;">
                <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                    <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                        <table style="width: 100%; padding: 10px;" class="tabletxt">

                            <tr>

                                <td class="auto-style1" style="color: #000000; font-weight: bold;">F-Year</td>
                                <td style="color: #000000; font-weight: bold;" class="auto-style2">:</td>
                                <td style="color: #000000; font-weight: bold;" class="auto-style3">
                                    <asp:DropDownList ID="drpYr" runat="server" CssClass="DropdownCss" OnSelectedIndexChanged="drpYr_SelectedIndexChanged" AutoPostBack="True">
                                        <asp:ListItem>2016-17</asp:ListItem>
                                        <asp:ListItem>2017-18</asp:ListItem>
                                        <asp:ListItem>2018-19</asp:ListItem>
                                        <asp:ListItem>2019-20</asp:ListItem>
                                        <asp:ListItem>2020-21</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>


                            <tr>

                                <td class="normaltd" style="width: 115px; font-weight: bold; color: #000000;">Implement</td>
                                <td style="width: 5px; color: #000000; font-weight: bold;">:</td>
                                <td style="width: 247px">
                                    <asp:DropDownList ID="drpImplement" runat="server" CssClass="DropdownCss" AutoPostBack="true" OnSelectedIndexChanged="drpImplement_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td class="normaltd" style="width: 115px; font-weight: bold; color: #000000;">Scheme</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 247px">
                                    <asp:DropDownList ID="ddl_scheme" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_scheme_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>

                            </tr>
                              <tr>   
                            <td></td>      
                                    <td colspan="3">
                                  <asp:Button ID="btnExport" Font-Bold="True" CssClass="btn btn-default" runat="server" Text="Export to CSV" OnClick="btnExport_Click" Visible="false" />
                                       
                                    </td>
                             </tr>
     
                            <tr>

                                <td colspan="3">
                                    <center>
                   
                   <asp:UpdateProgress id="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                                            <progresstemplate>
<IMG style="WIDTH: 102px; HEIGHT: 81px" src="images/processing-please-wait.gif" /> 
</progresstemplate>
                                        </asp:UpdateProgress> 
                   
                </center>
                                </td>
                            </tr>


                            <tr>
                                <td class="normaltd" colspan="3" id="ShowData" runat="server" visible="false">
                                    <asp:GridView ID="grddisttarget" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found" Font-Bold="true" HorizontalAlign="Center" Width="100%" ShowFooter="true">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr No" HeaderStyle-Width="5%" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1 %>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle  HorizontalAlign="Left"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table border="0" width="100%">
                                                        <tr>
                                                            <td align="center" colspan="3" rowspan="2">District </td>
                                                        </tr>

                                                    </table>
                                                </HeaderTemplate>
                                                <HeaderStyle Width="20%" />
                                                <ItemTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" width="100%">

                                                                <asp:Label ID="lblDistrict" runat="server" Text='<%#Eval("Mod_dist_name") %>' Width="70px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </ItemTemplate>

                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table border="0" width="100%">
                                                        <tr>
                                                            <td align="center" colspan="3" rowspan="2">Block </td>
                                                        </tr>

                                                    </table>
                                                </HeaderTemplate>
                                                <HeaderStyle Width="20%" />
                                                <ItemTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" width="100%">

                                                                <asp:Label ID="lblBlock" runat="server" Text='<%#Eval("block_name") %>' Width="70px"></asp:Label>
                                                            </td>

                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                                <FooterTemplate>Total</FooterTemplate>
                                                <ItemStyle Width="20%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" colspan="4">Permit Issue </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" width="25%">General </td>
                                                            <td align="center" width="25%">Sc </td>
                                                            <td align="center" width="25%">ST </td>
                                                            <td align="center" width="25%">Total </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <HeaderStyle Width="18%" />
                                                <ItemTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblGeneralTotal" runat="server" Text='<%#Eval("GeneralTotal") %>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblScTotal" runat="server" Text='<%#Eval("ScTotal") %>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblStTotal" runat="server" Text='<%#Eval("StTotal") %>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblTotalIssued" runat="server" Text='<%#Eval("TotalIssued") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblGeneralTotalFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblScTotalFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblStTotalFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblTotalIssuedFooter" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemStyle Width="18%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" colspan="4">Machine Supply </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" width="25%">General </td>
                                                            <td align="center" width="25%">Sc </td>
                                                            <td align="center" width="25%">ST </td>
                                                            <td align="center" width="25%">Total </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <HeaderStyle Width="18%" />
                                                <ItemTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblGeneralTSupplied" runat="server" Text='<%#Eval("GeneralTSupplied") %>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblScTSupplied" runat="server" Text='<%#Eval("ScTSupplied") %>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblStTSupplied" runat="server" Text='<%#Eval("StTSupplied") %>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblTotalSupplied" runat="server" Text='<%#Eval("TotalSupplied") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblGeneralTSuppliedFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblScTSuppliedFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblStTSuppliedFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblTotalSuppliedFooter" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemStyle Width="18%" />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" colspan="4">Subsidy Release </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" width="25%">General </td>
                                                            <td align="center" width="25%">Sc </td>
                                                            <td align="center" width="25%">ST </td>
                                                            <td align="center" width="25%">Total </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <HeaderStyle Width="18%" />
                                                <ItemTemplate>
                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblGeneralTPayment" runat="server" Text='<%#Eval("GeneralTPayment") %>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblScTPayment" runat="server" Text='<%#Eval("ScTPayment")%>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblStTPayment" runat="server" Text='<%#Eval("StTPayment") %>'></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblTotalPayment" runat="server" Text='<%#Eval("TotalPayment") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <table border="1" width="100%">
                                                        <tr>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblGeneralTPaymentFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblScTPaymentFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblStTPaymentFooter" runat="server"></asp:Label>
                                                            </td>
                                                            <td align="center" width="25%">
                                                                <asp:Label ID="lblTotalPaymentFooter" runat="server"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>
                                                <ItemStyle Width="18%" />
                                            </asp:TemplateField>

                                        </Columns>
                                        <FooterStyle HorizontalAlign="Center" Font-Bold="true" />
                                        <HeaderStyle BackColor="#E0E0E0" />
                                        <SelectedRowStyle ForeColor="DarkRed" />
                                    </asp:GridView>



                                </td>
                            </tr>


                        </table>

                        <%--   <asp:Panel ID="pnlPrintArea" runat="server" Width="100%">--%>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

