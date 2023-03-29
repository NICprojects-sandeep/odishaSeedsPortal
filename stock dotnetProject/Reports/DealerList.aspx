<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dist.master" AutoEventWireup="true" CodeFile="DealerList.aspx.cs" Inherits="Reports_DealerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <script type="text/javascript">
        $(function () {
            $("[id*=grd_FarmerList] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #EDE9E8;
            font-weight: bold;
        }
    </style>
    <asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
        AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
        CellPadding="4" Width="90%" ForeColor="Black">
        <RowStyle BackColor="White" CssClass="tbl" />
        <Columns>

            <asp:TemplateField HeaderText="Sl. No." ItemStyle-Width="50">
                <ItemTemplate>
                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Farm Name" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>

                    <asp:HiddenField ID="hfLIC_NO" runat="server" Value='<%#Eval("LIC_NO")%>' />
                    <asp:LinkButton ID="lnkFname" runat="server" Text='<%#Eval("APP_FIRMNAME")%>' OnClick="lnkVCard_Click" OnClientClick="aspnetForm.target ='_blank';"></asp:LinkButton>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Applicant Status" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lbltype" runat="server" Text='<%#Eval("type")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Licence Number" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblLIC_NO" runat="server" Text='<%#Eval("LIC_NO")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Mob. Number" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lblappmob_no" runat="server" Text='<%#Eval("appmob_no")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>



        </Columns>
        <FooterStyle BackColor="#B2C629" />
        <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
        <EmptyDataTemplate>
            <asp:Label ID="Label1" runat="server" Text="Records not found !"
                Font-Bold="true" ForeColor="red"></asp:Label>
        </EmptyDataTemplate>
        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
    </asp:GridView>

</asp:Content>

