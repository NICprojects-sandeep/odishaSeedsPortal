<%@ Page Title="::FARMER REPORT::" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="DBTFarmerRpt.aspx.cs" Inherits="DBT_DBTFarmerRpt" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <link href="style.css" rel="stylesheet" />
    <table width="100%" align="center" class="text">
        <tr>
            <td width="25%">
                <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
            </td>
            <td width="25%"></td>
            <td width="25%"></td>
            <td width="25%"></td>
        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>

            <td colspan="4" align="center">
                <h3 style="color: #990000">:: Registered farmer Details :: </h3>
                <br />
                <hr />
            </td>


        </tr>
        <tr>
            <td colspan="4">&nbsp;</td>
        </tr>
        <tr>
            <td colspan="4" align="center">
                <asp:GridView ID="grdReport" runat="server" AutoGenerateColumns="false" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="SL NO">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Registration No">
                            <ItemTemplate>
                                <%#Eval("VCHFARMERCODE") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Farmer Name">
                            <ItemTemplate>
                                <%#Eval("VCHFARMERNAME") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Father Name">
                            <ItemTemplate>
                                <%#Eval("VCHFATHERNAME") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile No.">
                            <ItemTemplate>
                                <%#Eval("VCHMOBILENO") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="GP">
                            <ItemTemplate>
                                <%#Eval("vch_GPName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Village">
                            <ItemTemplate>
                                <%#Eval("vch_VillageName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bank Name">
                            <ItemTemplate>
                                <%#Eval("vchBankName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch name">
                            <ItemTemplate>
                                <%#Eval("vchBranchName") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acc. Holder Name">
                            <ItemTemplate>
                                <%#Eval("VCHACCHOLDERNM") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acc. No.">
                            <ItemTemplate>
                                <%#Eval("VCHACCOUNTNO") %>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Date Of registration">
                            <ItemTemplate>
                                <%#Eval("DTMCREATEDON") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                     <asp:TemplateField HeaderText="Farmer Id">
                            <ItemTemplate>
                                <%#Eval("NICFARMERID")%>
                            </ItemTemplate>
                        </asp:TemplateField>


                    </Columns>
                    <HeaderStyle BackColor="#CCCCFF" />
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>
</asp:Content>

