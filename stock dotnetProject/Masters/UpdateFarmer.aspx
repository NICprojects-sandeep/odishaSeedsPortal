<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/VAW.master" AutoEventWireup="true" CodeFile="UpdateFarmer.aspx.cs" Inherits="Jalanidhi_VAW_UpdateFarmer" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #A1DCF2;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $("[id*=grdAll] td").hover(function () {
                $("td", $(this).closest("tr")).addClass("hover_row");
            }, function () {
                $("td", $(this).closest("tr")).removeClass("hover_row");
            });
        });
    </script>
    <asp:GridView ID="grdAll" runat="server" BackColor="#B2C629"
        AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
        CellPadding="4" Width="99%" ForeColor="Black" OnRowDataBound="grdAll_RowDataBound">
        <RowStyle BackColor="White" CssClass="tbl" />
        <Columns>

            <asp:TemplateField HeaderText="Sl No." HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate><%#Container.DataItemIndex+1 %> </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Village Name" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="villg_name" runat="server" Text='<%#Eval("villg_name")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
               <asp:TemplateField HeaderText="Farmer Id" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%#Eval("NICFARMERID")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Farmer Name" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lbl_VCHFARMERNAME" runat="server" Text='<%#Eval("VCHFARMERNAME")%>'></asp:Label>
                    <asp:HiddenField ID="hfFid" runat="server" Value='<%#Eval("NICFARMERID")%>' />
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Father's Name" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lbl_VCHFATHERNAME" runat="server" Text='<%#Eval("VCHFATHERNAME")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>



            <%-- <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lbl_Category_Value" runat="server" Text='<%#Eval("Category_Value")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>--%>
            <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lbl_Gender_Value" runat="server" Text='<%#Eval("Gender_Value")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Label ID="lbl_VCHMOBILENO" runat="server" Text='<%#Eval("VCHMOBILENO")%>'></asp:Label>
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="hfSMY" runat="server" Text="Small/Marginal" GroupName="t" Checked="true" />
                    <br />
                    <asp:RadioButton ID="hfSMN" runat="server" Text="Other" GroupName="t" />
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Incone Tax Pay" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="hfITY" runat="server" Text="Yes" GroupName="i" />
                    <asp:RadioButton ID="hfITN" runat="server" Text="No" GroupName="i" Checked="true" />
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Gov. Servant" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="hfGSY" runat="server" Text="Yes" GroupName="g" Checked="true" />
                    <asp:RadioButton ID="hfGSN" runat="server" Text="No" GroupName="g" />
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="HOF" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:RadioButton ID="hfHODY" runat="server" Text="Yes" GroupName="h" Checked="true" />
                    <asp:RadioButton ID="hfHODN" runat="server" Text="No" GroupName="h" />
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Update Adhaar No." HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                     <asp:Label ID="lblAvl" runat="server" ForeColor="Red" Text="Available"></asp:Label>
                    <asp:TextBox ID="txtAdhaar" runat="server"></asp:TextBox>
                    <asp:HiddenField ID="hfAdhar" runat="server" Value='<%#Eval("VCHAADHARNO")%>' />
                </ItemTemplate>

                <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Update" HeaderStyle-HorizontalAlign="Left">
                <ItemTemplate>
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
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
</asp:Content>

