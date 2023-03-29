<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="FarmersApprovedByDept.aspx.cs" Inherits="Reports_FarmersApprovedByDept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <fieldset class="background" style="border: 3px solid #FF0000">
        <legend align="center">Approve Report</legend>
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
                        EnableModelValidation="True" AutoGenerateColumns="false" Width="70%" AllowPaging="True" OnPageIndexChanging="gv_sub_PageIndexChanging" PageSize="30">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl. No">
                                <ItemTemplate>
                                    <%# Container.DataItemIndex + 1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="FarmerID" DataField="Farmer_ID" />
                            <asp:BoundField HeaderText="PFMS Code" DataField="PFMS_Code" />
                            <asp:BoundField HeaderText="Unique_Message_ID" DataField="Unique_Message_ID" />
                            <asp:BoundField HeaderText="PFMS_Beneficiary_Name" DataField="PFMS_Beneficiary_Name" />
                            <asp:BoundField HeaderText="Registration_Beneficiary_Number" DataField="Registration_Beneficiary_Number" />
                            <asp:BoundField HeaderText="Remark" DataField="Remark" />
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
    </fieldset>
</asp:Content>

