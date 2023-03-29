<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Scheme.master" AutoEventWireup="true" CodeFile="OSSC_Farmer_Incentive.aspx.cs" Inherits="Reports_OSSC_Farmer_Incentive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">    
    <table width="100%">
        <tr>
            <td style="font-weight:bold;color:maroon;text-align:center;font-size:15px;">
                Agency : <asp:Label ID="lblAgency" runat="server" Text="" ForeColor="Green"></asp:Label>,&nbsp;&nbsp;&nbsp;&nbsp;
                Crop : <asp:Label ID="lblCrop" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                Financial Year : <asp:Label ID="lblFinYr" runat="server" Text="" ForeColor="Green"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />&nbsp;&nbsp;&nbsp;&nbsp;
                Variety Type : Less than 10 Years
            </td>
        </tr>
    </table>
    <br />
    <div style="overflow: scroll; width: 1170px;">
                                <asp:Literal ID="litReport" runat="server"></asp:Literal>
                            </div>
    <%--<asp:GridView ID="gvIncentive" runat="server" AutoGenerateColumns="False" ShowFooter="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="FARMER_ID" HeaderText="Farmer ID" />
            <asp:BoundField DataField="FARMER_NAME" HeaderText="SG Name" />
            <asp:BoundField DataField="FATHER_HUSBAND_NAME" HeaderText="Father/Husband Name" />
            <asp:BoundField DataField="DIST_NAME" HeaderText="District Name" />
            <asp:BoundField DataField="VILLG_NAME" HeaderText="Village Name" />
            <asp:BoundField DataField="LOT_NO" HeaderText="Lot Number" />
            <asp:BoundField DataField="REF_NO" HeaderText="Reference Number" />
            <asp:BoundField DataField="SEASON" HeaderText="Season" />
            <asp:BoundField DataField="VARIETY_NAME" HeaderText="Variety Name" />
            <asp:BoundField DataField="INSPECTED_AREA" HeaderText="Inspected Area  (in Acers)" />
            <asp:BoundField DataField="SUBSIDISED_AREA" HeaderText="Subsidised Area (in Acers)" />
            <asp:BoundField DataField="PROCESSED_QTY" HeaderText="Tag Issue Quantity  (in Quintals)" />
            <asp:BoundField DataField="Qtl_Ha" HeaderText="Qtl/Ha" />
            <asp:BoundField DataField="SUBSIDISED_QTY" HeaderText="Admissibe Quantity (in Quintals)" />            
            <asp:BoundField DataField="FARMER_SUBSIDY" HeaderText="SeedGrower Subsidy (in Rupees)" />
            <asp:BoundField DataField="OSSC_SUBSIDY" HeaderText="OSSC Subsidy (in Rupees)" /> 
            <asp:BoundField DataField="ACC_HOLDERNAME" HeaderText="Account Holder Name" />
            <asp:BoundField DataField="ACC_NO" HeaderText="Account Number" />
            <asp:BoundField DataField="VCHBANKNAME" HeaderText="Bank Name" />
            <asp:BoundField DataField="VCHBRANCHNAME" HeaderText="Branch Name" />
            <asp:BoundField DataField="IFSC_CODE" HeaderText="IFSC Code" />           
        </Columns>
    </asp:GridView>--%>
</asp:Content>

