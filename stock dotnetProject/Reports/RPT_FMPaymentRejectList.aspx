<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="RPT_FMPaymentRejectList.aspx.cs" Inherits="Reports_RPT_FMPaymentRejectList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
      <div class="titleHeader">
        &nbsp;
         Payment Reject List
    </div>
    <table width="100%" runat="server" id="tbl_reson">
        <tr>
            <td class="style1">System ID :</td>
            <td>
                <asp:DropDownList ID="ddl_sysid" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_sysid_SelectedIndexChanged">
                    <asp:ListItem Value="0" Text="Select"></asp:ListItem>
                    <asp:ListItem Value="0026" Text="Firm Mechanisation"></asp:ListItem>
                    <asp:ListItem Value="0027" Text="Jalanidhi"></asp:ListItem>
                </asp:DropDownList></td>
            <td class="style1" style="width: 210px">Rejection Reason :</td>
            <td>
                <asp:DropDownList ID="ddl_reson" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddl_reson_SelectedIndexChanged" Height="20px" Width="369px"></asp:DropDownList></td>
            <td class="style2"></td>
            <td></td>
        </tr>
        <tr>            
            <td class="style2"><br /></td>
            <td></td>
        </tr>
    </table>
    <table width="100%" runat="server" id="tbl_content">
        <tr>
            <td class="style1"></td>
            <td></td>
            <td class="style2"></td>
            <td></td>
            <td></td>
            <td></td>
        </tr> 
        <tr>
            <td class="style1" align="center" colspan="6">
                <asp:GridView ID="gv_sub" runat="server" BackColor="White" 
                    BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" AutoGenerateColumns="False" Width="100%" AllowPaging="True" PageSize="30" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl. No" ItemStyle-ForeColor="Black">
                            <ItemTemplate>
                                <%# Container.DataItemIndex + 1 %>
                            </ItemTemplate>

<ItemStyle ForeColor="Black"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Permit No." DataField="unqid" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>
                        <asp:BoundField HeaderText="Farmer Name" DataField="nme" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>
                        <asp:BoundField HeaderText="Amount" DataField="amt" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>
                        <asp:BoundField HeaderText="Bank" DataField="bank" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>
                        <asp:BoundField HeaderText="IFSC" DataField="ifsc" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>
                        <asp:BoundField HeaderText="Account No." DataField="accno" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>
                        <asp:BoundField HeaderText="Status" DataField="sts" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>
                        <asp:BoundField HeaderText="Narration" DataField="nar" >
                            <ItemStyle HorizontalAlign="Center" ForeColor="Black" />
                            </asp:BoundField>
                        <%--<asp:BoundField HeaderText="No. of beneficiary Failed" DataField="authnor" />--%>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </td>
        </tr> 
        <tr>
            <td class="style1"></td>
            <td></td>
            <td class="style2"><b>
                <asp:Label ID="lbl_msg" runat="server" Text="" ForeColor="Red"></asp:Label></b></td>
            <td></td>
            <td></td>
            <td></td>
        </tr> 
    </table>
</asp:Content>

