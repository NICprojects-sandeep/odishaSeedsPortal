<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="SubsidyView.aspx.cs" Inherits="Reports_SubsidyView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script>
        function goBack() {
            window.history.back();
        }
    </script>
    <fieldset class="background" style="border: 3px solid #FF0000">
        <legend align="center">Subsidy Report</legend>
        <table width="100%">
            <tr>
                <td class="style1"></td>
                <td></td>
                <td class="style2"></td>
                <td></td>
                <td></td>
                <td>
                    <asp:Button ID="btnBack" runat="server" Text="Back" OnClientClick="goBack()" /></td>
            </tr>
            <tr>
                <td class="style1" align="center" colspan="6">
                    <asp:GridView ID="gv_sub" runat="server" BackColor="White"
                        BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                        EnableModelValidation="True" AutoGenerateColumns="false" Width="70%" AllowPaging="True" OnPageIndexChanging="gv_sub_PageIndexChanging" PageSize="30">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="FID" DataField="FID" />
                            <asp:BoundField HeaderText="Name" DataField="Name" />
                            <asp:BoundField HeaderText="Bank Name" DataField="bank_name" />
                            <asp:BoundField HeaderText="Branch Name" DataField="branch_name" />
                            <asp:BoundField HeaderText="Account No." DataField="accno" />
                            <asp:BoundField HeaderText="Transaction ID" DataField="tranid" />
                            <asp:BoundField HeaderText="Transaction Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy}" />
                            <asp:BoundField HeaderText="Amount" DataField="amt" />
                            <asp:BoundField HeaderText="Scheme" DataField="scheme" />
                            <asp:BoundField HeaderText="Payment Status" DataField="sts" />
                            <asp:BoundField HeaderText="Rejection Reason" DataField="res" />
                        </Columns>
                        <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                        <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                        <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                        <RowStyle BackColor="White" ForeColor="#003399" />
                        <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td class="style1" align="center" colspan="6">
                    <asp:Label ID="lbl_count" runat="server" Visible="false"></asp:Label>
                </td>
            </tr>
        </table>
    </fieldset>
</asp:Content>

