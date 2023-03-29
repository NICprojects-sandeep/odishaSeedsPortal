<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="NIC_AAO_REPORT_LIST.aspx.cs" Inherits="Reports_NIC_AAO_REPORT_LIST" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 50%;
        }

        .Border {
            border: dashed black 2px;
            width: 900px;
            padding-bottom: 30px;
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
            margin-bottom: 30px;
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


    <asp:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="width: 100%" class="auto-style1">
                <strong><em>
                    <asp:Literal ID="Literal1" runat="server" Text="REJECTED LIST"></asp:Literal></em></strong>
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
                                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self';" /></td>
                        </tr>
                    </table>
                </fieldset>
            </div>

            <asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
                AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" Width="90%" ForeColor="Black">
                <RowStyle BackColor="White" CssClass="tbl" />
                <Columns>
                    <asp:TemplateField HeaderText="Sl.No" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%# Container.DataItemIndex+1 %>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>

                            <asp:LinkButton ID="LinkButton1" runat="server" Visible='<%# Eval("upd").ToString() == "0" %>' OnClick="LinkButton1_Click"><%# Eval("VCHFARMERNAME") %></asp:LinkButton>
                            <%--<a runat="server" id="link" href='<%# Eval("INTFARMERID", "NIC_AAO_REPORT.aspx?ID={0}") %>' visible='<%# Eval("upd").ToString() == "0" %>'><%# Eval("VCHFARMERNAME") %></a>--%>
                            <asp:Label ID="lbl_NICFARMERIDs" runat="server" Text='<%#Eval("VCHFARMERNAME")%>' Visible='<%# Eval("upd").ToString() != "0" %>'></asp:Label>
                            <asp:Label ID="Label2" runat="server" Text='<%#Eval("VCHFARMERCODE")%>' Visible="false"></asp:Label>

                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>

                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Father Name" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%#Eval("VCHFATHERNAME")%>'></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left" Width="150px"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="VoterID_No" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <%#Eval("VCHVOTERIDCARDNO") %>
                        </ItemTemplate>
                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason for rejection" HeaderStyle-HorizontalAlign="Left">
                        <ItemTemplate>
                            <asp:Label ID="lbl_NICFARMERID1" runat="server" Text='<%#(String.IsNullOrEmpty(Eval("status").ToString()) ? "Rejected Due to Invalid of Bank Details" : Eval("status")) %>' Visible='<%# Eval("upd").ToString() == "0" %>'></asp:Label>
                            <asp:Label ID="lbl_NICFARMERID2" runat="server" Text="Edited" Visible='<%# Eval("upd").ToString() != "0" %>' ForeColor="Red"></asp:Label>
                        </ItemTemplate>

                        <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#B2C629" />
                <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                <EmptyDataTemplate>
                    <asp:Label ID="Label1" runat="server" Text="No Records !!!"
                        Font-Bold="true" ForeColor="red"></asp:Label>
                </EmptyDataTemplate>
                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
            </asp:GridView>
            <br />
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress runat="server" ID="Uprogress" AssociatedUpdatePanelID="updatepanel"
        DisplayAfter="0">
        <ProgressTemplate>
            <div style="text-align: center; vertical-align: middle; position: absolute; top: 0px; left: 0px; z-index: 99; width: 100%; height: 1000px; background-color: #ededed; -ms-filter: 'progid:DXImageTransform.Microsoft.Alpha(Opacity=60)'; filter: progid:DXImageTransform.Microsoft.Alpha(opacity=60); -moz-opacity: 0.8; opacity: 0.8;"
                visible="true">
                <div style="width: 48px; height: 48px; margin: 0 auto; margin-top: 275px;">
                    <asp:Image ImageUrl="~/images/loading.gif" Width="48px" Height="48px" runat="server"
                        ID="TaskLoadingImage" />
                </div>
                <div style="font-family: Trebuchet MS; font-size: 12px; color: Red; text-align: center;">
                    Please Wait ...
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</asp:Content>

