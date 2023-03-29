<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/NICAdmin.master" AutoEventWireup="true" CodeFile="DBQuery.aspx.cs" Inherits="NICAdmin_DBQuery" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table style="width:100%;">
        <tr>
            <td style="width:20%;">
                <asp:DropDownList ID="ddlDB" runat="server" OnSelectedIndexChanged="ddlDB_SelectedIndexChanged" Width="100%"></asp:DropDownList>
            </td>
            <td style="width:70%;">
                <asp:TextBox ID="txtQry" runat="server" Width="100%" Height="243px"></asp:TextBox>
            </td>
            <td style="width:10%;">
                <asp:Button ID="btnQuery" runat="server" Text="Execute" Width="100%" OnClick="btnQuery_Click" />
            </td>
        </tr>
        <tr>            
            <td colspan="3">

                <asp:GridView ID="gvResult" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
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

            </td>
        </tr>
    </table>
    
</asp:Content>

