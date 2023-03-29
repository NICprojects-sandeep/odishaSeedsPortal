<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Scheme.master" AutoEventWireup="true" CodeFile="RPT_nfsmre.aspx.cs" Inherits="Reports_RPT_nfsmre" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link type="text/css" href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
    <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>

    <script type="text/javascript">
        $(function () {
            $("#txtfrm").datepicker({ dateFormat: 'dd-mm-yy' });
            //$("#txtfrm").datepicker("show");
        });
    </script>
    <center>
        <fieldset class="tbl">

            <legend style="margin-left: 10px;" class="leg">Select any type</legend>

            <table style="width: 90%; padding: 15px;">
                <tr>
                    <td style="width: 195px;" class="normaltd">Date(Payment Period From)</td>
                    <td style="width: 5px;">:</td>
                    <td style="width: 230px;">
                        <asp:TextBox ID="txtfrm" runat="server" ClientIDMode="Static"></asp:TextBox></td>
                    <td style="width: 115px;" class="normaltd">
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self'; return Valid();" /></td>
                    <td style="width: 5px;">&nbsp;</td>
                    <td style="width: 315px;">&nbsp;</td>
                    <td rowspan="2">&nbsp;</td>
                </tr>

            </table>
        </fieldset>

        <asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" Width="90%" ForeColor="Black">
            <RowStyle BackColor="White" CssClass="tbl" />
            <Columns>

                <asp:TemplateField HeaderText="Sl No." HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate><%#Container.DataItemIndex+1 %> </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="District" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%#Eval("DistName")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Catecory" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VCHFARMERNAME" runat="server" Text='<%#Eval("category")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NO-Of-Transaction" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Category_Value" runat="server" Text='<%#Eval("No_trans")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VCHFATHERNAME" runat="server" Text='<%#Eval("Amount")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>


            </Columns>
            <FooterStyle BackColor="#B2C629" />
            <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" Text="Data not found for this Variety,Select another Variety"
                    Font-Bold="true" ForeColor="red"></asp:Label>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
        </asp:GridView>
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
    </center>
</asp:Content>

