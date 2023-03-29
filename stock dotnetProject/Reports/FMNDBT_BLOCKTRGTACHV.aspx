<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/ADAPT.master" AutoEventWireup="true" CodeFile="FMNDBT_BLOCKTRGTACHV.aspx.cs" Inherits="Reports_FMNDBT_BLOCKTRGTACHV" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:Panel ID="Panel1" runat="server" Style="padding-top: 30px; height: 500px;">
            <div style="width: 1220px; background-color: #666; padding: 2px; margin: 0 auto;">
                <div style="background-color: #FFF; padding: 10px; width: 1200px;">
                    <table style="width: 100%; padding: 10px;" class="tabletxt">

                          <tr>
                                
                                    <td class="normaltd" style="width: 115px;">F-Year</td>
                                    <td style="width: 5px;">:</td>
                                    <td style="width: 247px">
                                        <asp:DropDownList ID="drpYr" runat="server" CssClass="DropdownCss" AutoPostBack="True" OnSelectedIndexChanged="drpYr_SelectedIndexChanged" >
                                            <asp:ListItem >2016-17</asp:ListItem>
                                            <asp:ListItem >2017-18</asp:ListItem>
                                            <asp:ListItem >2018-19</asp:ListItem>
                                            <asp:ListItem >2019-20</asp:ListItem>
                                             <asp:ListItem >2020-21</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                          </tr>
                         

                          <tr>
                                
                                    <td class="normaltd" style="width: 115px;">Implement</td>
                                    <td style="width: 5px;">:</td>
                                    <td style="width: 247px">
                                        <asp:DropDownList ID="drpImplement" runat="server" CssClass="DropdownCss" AutoPostBack="true" OnSelectedIndexChanged="drpImplement_SelectedIndexChanged"  >
                                        </asp:DropDownList>
                                    </td>
                          </tr>
                        <tr>   
                            <td></td>      
                                    <td colspan="3">
                                  <asp:Button ID="btnExport" Font-Bold="True" CssClass="btn btn-default" runat="server" Text="Export to CSV" OnClick="btnExport_Click" Visible="false" />
                                       
                                    </td>
                             </tr>
     
    
                    </table>
                   <br />
                  <%--   <asp:Panel ID="pnlPrintArea" runat="server" Width="100%">--%>
                    <asp:Literal ID="litReport" runat="server"></asp:Literal>
                         </asp:Panel>
</asp:Content>

