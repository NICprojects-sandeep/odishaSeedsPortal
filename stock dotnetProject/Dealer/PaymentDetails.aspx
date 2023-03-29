<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dealer.master" AutoEventWireup="true" CodeFile="PaymentDetails.aspx.cs" Inherits="Dealer_PaymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #td_LicNo
        {
            font-weight: 700;
        }
        .style2
        {
            text-decoration: underline;
        }
    .style5
    {
        text-decoration: underline;
    }
    .style6
    {
        color: #FF0000;
    }
    .style7
    {
        color: #CC3300;
    }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <fieldset>
        <legend>Search</legend>
        <table width="100%">
            <tr>
                <td>From Date
                </td>
                <td>
                    <asp:TextBox ID="txtFromDate" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" SetFocusOnError="True"
                        ControlToValidate="txtFromDate" EnableClientScript="False" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                        ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                </td>
                <td>To Date
                </td>
                <td>
                    <asp:TextBox ID="txtToDate" runat="server" TextMode="Date"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" SetFocusOnError="True"
                        ControlToValidate="txtToDate" EnableClientScript="False" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                        ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" />
                </td>
                <td>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" Width="100px" />
                </td>
            </tr>
        </table>
    </fieldset>
    <br />
    <asp:GridView ID="gvdealersale" runat="server" AutoGenerateColumns="False"
        Width="100%" GridLines="None"
        CellPadding="4" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="Transaction No.">
                <ItemTemplate>
                    <asp:Label ID="lblTransNo" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("TRANSACTION_ID").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Transaction Date">
                <ItemTemplate>
                    <asp:Label ID="lblTransDate" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("TRANSACTION_DATE").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Farmer Id">
                <ItemTemplate>
                    <asp:Label ID="lblLotNo" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("NICFARMERID").ToString())%>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Farmer Name(A/C No)">
                <ItemTemplate>
                    <asp:Label ID="lblFarmername" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("VCHFARMERNAME").ToString())%>'></asp:Label>
                </ItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="Bag Size(in KG)">
                <ItemTemplate>
                    <asp:Label ID="lblBagSize" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("BAG_SIZE_KG").ToString())%>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="right" />
                <HeaderStyle HorizontalAlign="right" />
                <ItemStyle HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="No of Bags">
                <ItemTemplate>
                    <asp:Label ID="lblNoOfBags" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("NO_OF_BAGS").ToString())%>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="right" />
                <HeaderStyle HorizontalAlign="right" />
                <ItemStyle HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Total Qty(in qtl)">
                <ItemTemplate>
                    <asp:Label ID="lblTotal" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("TOT_QTL").ToString())%>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="right" />
                <HeaderStyle HorizontalAlign="right" />
                <ItemStyle HorizontalAlign="right" />
            </asp:TemplateField>
            <%--<asp:TemplateField HeaderText="Subsidy Amount" >
                    <ItemTemplate>
                        <asp:Label ID="lblsamount" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("TOT_SUB_AMOUNT_GOI").ToString())%>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="right" />
                    <HeaderStyle HorizontalAlign="right" />
                    <ItemStyle HorizontalAlign="right" />
                </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="lblstatus" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Record_Status").ToString())%>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="right" />
                <HeaderStyle HorizontalAlign="right" />
                <ItemStyle HorizontalAlign="right" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Reason of Rejection">
                <ItemTemplate>
                    <asp:Label ID="lblrejection" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Rejection_Narration").ToString())%>'></asp:Label>
                </ItemTemplate>
                <FooterStyle HorizontalAlign="right" />
                <HeaderStyle HorizontalAlign="right" />
                <ItemStyle HorizontalAlign="right" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#7C6F57" />
        <EmptyDataTemplate>
            <center><span class="style1"><strong>No Data Found</strong></span></center>
        </EmptyDataTemplate>
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
</asp:Content>

