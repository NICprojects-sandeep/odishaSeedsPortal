<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="FarmerPaymentCheck.aspx.cs" Inherits="Reports_FarmerPaymentCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table>
        <tr>
            <td>

            </td>
            <td>
                FARMER ID <asp:TextBox ID="txtFarmerId" runat="server"></asp:TextBox><asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
            </td>
            <td>

            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlView" runat="server">
        <asp:GridView ID="gvDetails" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <EmptyDataTemplate>
                <div style="color:red; text-align: center;">
                                            No Record found
                                        </div>                
            </EmptyDataTemplate>
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
        </asp:GridView>
    </asp:Panel>
</asp:Content>

