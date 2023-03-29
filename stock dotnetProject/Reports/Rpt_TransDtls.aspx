<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Rpt_TransDtls.aspx.cs" Inherits="Reports_Rpt_TransDtls" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="gvTrans" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField DataField="FARMER_ID" HeaderText="Farmer Id" />
                    <asp:BoundField DataField="TRANSACTION_ID" HeaderText="Transaction Id" />
                    <asp:BoundField DataField="Crop_Name" HeaderText="Crop Name" />
                    <asp:BoundField DataField="Variety_Name" HeaderText="Variety Name" />
                    <asp:BoundField DataField="Receive_Unitname" HeaderText="Source" />
                    <asp:BoundField DataField="LOT_NUMBER" HeaderText="Lot Number" />
                    <asp:BoundField DataField="TOT_QTL" HeaderText="Total Qty" />
                    <asp:BoundField DataField="ADMISSIBLE_SUBSIDY" HeaderText="Subsidy Qty" />
                    <asp:BoundField DataField="SCHEME_NAME" HeaderText="GOI Scheme Name" />
                    <asp:BoundField DataField="TOT_SUB_AMOUNT_GOI" HeaderText="GOI Amount" />
                    <asp:BoundField DataField="SCHEME_NAMESP" HeaderText="SP Scheme Name" />
                    <asp:BoundField DataField="TOT_SUB_AMOUNT_SP" HeaderText="SP Amount" />
                    <asp:BoundField DataField="SUBSIDY_AMOUNT" HeaderText="Subsidy Amount" />
                    <asp:BoundField DataField="FIN_YEAR" HeaderText="Year" />
                    <asp:BoundField DataField="SEASON" HeaderText="Season" />
                </Columns>
                <EditRowStyle BackColor="#7C6F57" />
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
        </div>
    </form>
</body>
</html>
