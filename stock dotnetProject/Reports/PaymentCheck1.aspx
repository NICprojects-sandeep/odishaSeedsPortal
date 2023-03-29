<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Finance.master" AutoEventWireup="true" CodeFile="PaymentCheck1.aspx.cs" Inherits="Reports_PaymentCheck1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div>
<br />
            <table style="width:100%">
                <tr>
                    <td >Dealer/Pacs Code* : <asp:TextBox ID="txt_dlrcode" runat="server" Width="154px"></asp:TextBox> </td>
                  <td>Financial Year* : <asp:DropDownList ID="ddlfinyear" runat="server" 
                          AutoPostBack="True" onselectedindexchanged="ddlfinyear_SelectedIndexChanged"></asp:DropDownList></td>
                    <td>Season* : <asp:DropDownList ID="ddlseason" runat="server"></asp:DropDownList>&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" /></td>
                   
                </tr>
                <tr>
                    <td align="center" colspan="3">
                    <br />
                        <asp:GridView ID="gvdlrrpt" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                        AutoGenerateColumns="False" CellSpacing="3" 
                            Style="height: 138px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>  
                                            <asp:TemplateField HeaderText="Sl No" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle Width="20px" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="TRANSACTION_ID" HeaderText="TransactionID" />
                                            <asp:BoundField DataField="UPDATED_ON" HeaderText="Transaction_DT" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="Category_Name" HeaderText="Crop_Category" />
                                            <asp:BoundField DataField="Variety_Name" HeaderText="Crop_Veriety" />
                                            <asp:BoundField DataField="Crop_Name" HeaderText="Crop_Name" />
                                            <asp:BoundField DataField="NO_OF_BAGS" HeaderText="NO_OF_BAGS" />
                                            <asp:BoundField DataField="TOT_QTL" HeaderText="TOT_QTL" />
                                            <asp:BoundField DataField="Scheme_Name" HeaderText="Scheme" />
                                            <asp:BoundField DataField="Total_Payment_Amount" HeaderText="Amount" />
                                            <asp:BoundField DataField="status" HeaderText="Status" />
                                            <asp:BoundField DataField="reject" HeaderText="Rejection_Reason" />
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
                    </td>
                </tr>
            </table>
            <br />

        </div>
</asp:Content>

