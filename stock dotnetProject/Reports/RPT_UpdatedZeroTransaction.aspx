﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="RPT_UpdatedZeroTransaction.aspx.cs" Inherits="Reports_RPT_UpdatedZeroTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style>
        .a {
            white-space: nowrap;
        }

        .B {
            color: white;
            font-weight: bold;
            background-color: green;
            padding-left: 20PX;
        }
    </style>
    <table width="100%" cellpadding="3" cellspacing="3">
        <tr>
            <td class="B">LIST OF UPDATED ZERO TRANSACTION (DEALER/PACS)
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" />
            </td>
        </tr>

        <tr>
            <td>
                <div id="divPrint">
                    <asp:GridView ID="grd_TransactionList" runat="server" BackColor="#B2C629" OnPreRender="grdExamWithclass_PreRender"
                        AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" Width="98%" ForeColor="Black">
                        <RowStyle BackColor="White" CssClass="tbl" />
                        <Columns>

                            <asp:TemplateField HeaderText="SL.No">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:BoundField DataField="LICNO" HeaderText="License No. (Dealer/PACS)" HeaderStyle-Width="100px"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle VerticalAlign="Top" Font-Bold="true" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mFARMER_ID" HeaderText="Farmer Id" HeaderStyle-Width="100px"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="VCHFARMERNAME" HeaderText="Farmer Name" HeaderStyle-Width="100px"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle CssClass="a" />
                            </asp:BoundField>


                            <asp:TemplateField HeaderText="Transaction_ID" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TRANSACTION_ID" runat="server" Text='<%#Eval("TRANSACTION_ID")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="a"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sale Date" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_SALEDATE" runat="server" Text='<%#Eval("SALEDATE")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left" CssClass="a"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CROP NAME" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Crop_Name" runat="server" Text='<%#Eval("Crop_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VARIETY NAME" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Variety_Name" runat="server" Text='<%#Eval("Variety_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BAG SIZE IN KG" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_BAG_SIZE_KG" runat="server" Text='<%#Eval("BAG_SIZE_KG")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NO OF BAGS" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_NO_OF_BAGS" runat="server" Text='<%#Eval("NO_OF_BAGS")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TOT QTL" HeaderStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_TOT_QTL" runat="server" Text='<%#Eval("TOT_QTL")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#B2C629" />
                        <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                        <EmptyDataTemplate>
                            <center>
                                <asp:Label ID="Label1" runat="server" Text="No Records !!!"
                                    Font-Bold="true" ForeColor="red"></asp:Label></center>
                        </EmptyDataTemplate>
                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
                    </asp:GridView>
                </div>
            </td>
        </tr>

    </table>
</asp:Content>
