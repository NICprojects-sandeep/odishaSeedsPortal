<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="UpdatedList.aspx.cs" Inherits="Masters_UpdatedList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:GridView ID="gvList" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White"></AlternatingRowStyle>

        <Columns>
            <asp:BoundField DataField="LIC_NO" HeaderText="LIC_NO"></asp:BoundField>         
            <asp:BoundField DataField="vchBankName" HeaderText="BANK NAME"></asp:BoundField>
            <asp:BoundField DataField="vchBranchName" HeaderText="BRANCH NAME"></asp:BoundField>
            <asp:BoundField DataField="ACC_HOLDERNAME" HeaderText="A/C HOLDER NAME"></asp:BoundField>
            <asp:BoundField DataField="ACC_NO" HeaderText="ACCOUNT NUMBER"></asp:BoundField>
            <asp:BoundField DataField="IFSC_CODE" HeaderText="IFSC CODE"></asp:BoundField>            
            <asp:BoundField DataField="UPDATED_ON" HeaderText="UPDATED ON"></asp:BoundField>
        </Columns>

        <EditRowStyle BackColor="#7C6F57"></EditRowStyle>

        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></FooterStyle>

        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White"></HeaderStyle>

        <PagerStyle HorizontalAlign="Center" BackColor="#666666" ForeColor="White"></PagerStyle>

        <RowStyle BackColor="#E3EAEB"></RowStyle>

        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333"></SelectedRowStyle>

        <SortedAscendingCellStyle BackColor="#F8FAFA"></SortedAscendingCellStyle>

        <SortedAscendingHeaderStyle BackColor="#246B61"></SortedAscendingHeaderStyle>

        <SortedDescendingCellStyle BackColor="#D4DFE1"></SortedDescendingCellStyle>

        <SortedDescendingHeaderStyle BackColor="#15524A"></SortedDescendingHeaderStyle>
    </asp:GridView>
</asp:Content>

