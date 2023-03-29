<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="AAOFarmerList.aspx.cs" Inherits="Reports_AAORjctFarmerList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 50%;
        }

        .Border {
            border: dashed black 2px;
            width: 900px;
            clear: both;
        }

        .leg {
            font-family: Tahoma;
            font-size: 14px;
            background-color: #E6EBED;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 2px;
        }

        .tbl {
            font-family: Tahoma;
            font-size: 14px;
            background-color: #E6EBED;
            width: 90%;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 5px;
            margin-bottom: 10px;
        }

        .Header {
            padding-top: 10px;
            padding-bottom: 10px;
            font-size: 24px;
            text-decoration: underline;
            text-align: center;
        }

        .nb {
            font-size: 14px;
            font-family: Comic Sans MS;
            text-align: left;
            width: 100%;
            padding-left: 20px;
            padding-top: 20px;
        }

        .btn {
            background-color: #E6EBED;
            padding: 5px;
            color: black;
            font-family: Tahoma;
            font-size: 12px;
            width: 100px;
            font-weight: bold;
            border-radius: 5px;
            border-collapse: collapse;
        }

            .btn:hover {
                background-color: #CED6D9;
                padding: 5px;
                color: black;
                font-family: Tahoma;
                font-size: 12px;
                width: 100px;
                font-weight: bold;
                border-radius: 5px;
                border-collapse: collapse;
            }

        .txtbox {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000000;
            width: 280px;
            padding: 5px;
        }

        .tabletxt {
            font-family: Tahoma, Geneva, sans-serif;
            color: #000;
            font-size: 12px;
            padding: 5px;
        }

        .normaltd {
            padding: 6px;
        }
    </style>
    <script>

        function Valid() {
            var gp = document.getElementById("<%=drpGp.ClientID %>");
                if (gp.value.trim() == "---Select---") {
                    alert('Please Select Gram Panchayat !');
                    gp.focus();
                    return false;
                }
            }
    </script>


    <asp:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="width: 100%" class="auto-style1">
                <strong><em>
                    <asp:Literal ID="Literal1" runat="server" Text="FARMER LIST"></asp:Literal></em></strong>
            </div>
            <div style="width: 100%" class="auto-style1">
                <fieldset class="tbl">

                    <legend style="margin-left: 10px;" class="leg">Select any type</legend>

                    <table style="width: 90%; padding: 15px;">
                        <tr>

                            <td class="normaltd">Gram Panchayat</td>
                            <td>
                                <asp:DropDownList ID="drpGp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpGp_SelectedIndexChanged" Width="150px">
                                </asp:DropDownList>
                            </td>

                            <td class="normaltd">Village</td>
                            <td>
                                <asp:DropDownList ID="drpVillage" runat="server" Width="150px">
                                </asp:DropDownList>
                            </td>

                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self'; return Valid();" /></td>
                        </tr>
                    </table>
                </fieldset>
            </div>
            <div style="margin-bottom: 10px;">
                <asp:Label ID="lblMsg" runat="server" Style="font-style: italic; font-weight: bold; color: #ff0000;" Text="Note : For Status UIID- Account is invalid but adhar enabled account is vallid. So AAO are requested to check the adhar no. and mail to dbt.odisha for its confirmation."></asp:Label>

            </div>

            <asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
                AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" Width="90%" ForeColor="Black">
                <RowStyle BackColor="White" CssClass="tbl" />
                <Columns>

                    <asp:TemplateField HeaderText="Sl No." HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate><%#Container.DataItemIndex+1 %> </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Voter Id" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>

                            <asp:HiddenField ID="hfVCard" runat="server" Value='<%#Eval("VCHVOTERIDCARDNO")%>' />
                            <%--       <asp:LinkButton ID="lnkVCard" runat="server" Text='<%#Eval("VCHVOTERIDCARDNOV")%>' CausesValidation="false" OnClientClick="aspnetForm.target ='_blank';" OnClick="lnkVCard_Click"></asp:LinkButton>--%>
                            <asp:Label ID="lbl_VCHVOTERIDCARDNOV" runat="server" Text='<%#Eval("VCHVOTERIDCARDNOV")%>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
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
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Father's Name" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbl_VCHFATHERNAME" runat="server" Text='<%#Eval("VCHFATHERNAME")%>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Category_Value" runat="server" Text='<%#Eval("Category_Value")%>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Gender" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Gender_Value" runat="server" Text='<%#Eval("Gender_Value")%>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Mobile" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbl_VCHMOBILENO" runat="server" Text='<%#Eval("VCHMOBILENOV")%>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Status" runat="server" Text='<%#Eval("StatusDesc")%>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>

                </Columns>
                <FooterStyle BackColor="#B2C629" />
                <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                <EmptyDataTemplate>
                    <asp:Label ID="Label1" runat="server" Text="Data not found !"
                        Font-Bold="true" ForeColor="red"></asp:Label>
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
            </asp:GridView>
            <br />
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

