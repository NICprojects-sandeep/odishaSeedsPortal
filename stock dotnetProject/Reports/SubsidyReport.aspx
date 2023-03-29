<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="SubsidyReport.aspx.cs" Inherits="Reports_SubsidyReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <legend align="center">Subsidy Report</legend>
    <table width="100%">
        <tr>
            <td class="style1"></td>
            <td></td>
            <td class="style2"></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td class="style1" align="center" colspan="6">
                <asp:GridView ID="gv_sub" runat="server" BackColor="White"
                    BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" CellPadding="4"
                    EnableModelValidation="True" AutoGenerateColumns="false" Width="100%" AllowPaging="True" OnPageIndexChanging="gv_sub_PageIndexChanging" OnSelectedIndexChanged="gv_sub_SelectedIndexChanged" OnSelectedIndexChanging="gv_sub_SelectedIndexChanging" PageSize="30">
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-ForeColor="Black">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd/MM/yyyy}">
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="NIC Payment file No." ItemStyle-ForeColor="Black">
                            <ItemTemplate>
                                <asp:Label ID="lbl_nicid" runat="server" Text='<%#Eval("NICID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PFMS/DCCB response file No." ItemStyle-ForeColor="Black">
                            <ItemTemplate>
                                <asp:Label ID="lbl_pfmsid" runat="server" Text='<%#Eval("PFMSID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="No of beneficiary in Request file">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_cnt" runat="server" Text='<%#Eval("CNT") %>' OnClick="grid_Click"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText=" Request Amount" DataField="csom" ItemStyle-ForeColor="Black" />
                        <asp:TemplateField HeaderText="No. of beneficiary Success">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_authno" runat="server" Text='<%#Eval("authno") %>' OnClick="grid_ClickNo"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Paid Amount" DataField="authamt" ItemStyle-ForeColor="Black" />
                        <asp:TemplateField HeaderText="No. of beneficiary Failed">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbl_authnor" runat="server" Text='<%#Eval("authnor") %>' OnClick="grid_ClickNoR"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:BoundField HeaderText="No. of beneficiary Failed" DataField="authnor" />--%>
                        <asp:BoundField HeaderText=" Failed Amount" DataField="authamtr" ItemStyle-ForeColor="Black" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                </asp:GridView>
            </td>
        </tr>
    </table>

</asp:Content>

