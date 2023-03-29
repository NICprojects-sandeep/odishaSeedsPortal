<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="ZeroTransReport.aspx.cs" Inherits="Reports_ZeroTransReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
                 <div class="titleHeader">
        &nbsp;Zero Transaction List
    </div>
    <br />
    <center>
        <div style="width: 100%" class="auto-style1"><strong><em>
            <asp:Literal ID="Literal1" runat="server" Text="LIST OF ZERO TRANSACTION DISTRICT WISE"></asp:Literal></em></strong></div>
        &nbsp;<asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" Width="90%" ForeColor="Black" ShowFooter="True" OnRowDataBound="grd_FarmerList_RowDataBound">
            <RowStyle BackColor="White" CssClass="tbl" />
            <Columns>
                <asp:TemplateField HeaderText="Sl.No" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <%# Container.DataItemIndex+1 %>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="DISTRICT NAME" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>


                        <asp:Label ID="lbl_NICFARMERIDs" runat="server" Text='<%#Eval("dist_name")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        SUB-TOTAL
                    </FooterTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TOTAL TRANSACTION" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%# Eval("total").ToString()==""?"0":Eval("total")%>'></asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblTotal" runat="server" Font-Bold="true" />
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UPDATED TRANSACTION" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>

                        <%#Eval("updated").ToString()==""?"0":Eval("updated") %>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblupd" runat="server" Font-Bold="true" />
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>



                <asp:TemplateField HeaderText="BALANCE" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NICFARMERID1" runat="server" Text=''></asp:Label>
                        <%# Convert.ToInt32(Eval("total").ToString()==""?"0":Eval("total"))- Convert.ToInt32(Eval("updated").ToString()==""?"0":Eval("updated")) %>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblbalance" runat="server" Font-Bold="true" />
                    </FooterTemplate>
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>


            </Columns>
            <FooterStyle BackColor="#B2C629" />
            <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" Text="Data not found for this VILLAGE,Select another VILLAGE OR GP"
                    Font-Bold="true" ForeColor="red"></asp:Label>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
        </asp:GridView>
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
    </center>
</asp:Content>

