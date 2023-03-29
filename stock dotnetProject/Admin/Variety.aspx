<%@ Page Title="::Variety::" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="Variety.aspx.cs" Inherits="Admin_Variety" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 300px;
            height: 350px;
            border: 3px solid #0DA9D0;
            border-radius: 12px;
            padding: 0;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup .body
        {
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .style5
        {
            color: #0000FF;
        }
        p.sansserif
        {
            font-family: Arial, Helvetica, sans-serif;
        }
    </style>
    <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdPnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td style="width: 30%;">
                    </td>
                    <td style="width: 40%;">
                        <table style="width: 100%;">
                            <tr>
                                <td align="left">
                                    Financial Year
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlFinYr" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Seassion Year
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlSeassion" runat="server" Width="200px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Crop Category
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCropCategory" runat="server" Width="200px" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddlCropCategory_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    Crop
                                </td>
                                <td>
                                    :
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlCrop" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlCrop_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <br />
                                    <asp:GridView ID="gvVariety" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None"
                                        AutoGenerateColumns="False" CellSpacing="2" Style="height: 138px">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:BoundField DataField="Variety_Code" HeaderText="Variety Code" Visible="False" />
                                            <asp:TemplateField HeaderText="Select">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblVCode" runat="server" Text='<%# (Eval("Variety_Code").ToString()) %>'
                                                        Visible="false" />
                                                    <asp:CheckBox ID="cbStatus" runat="server" Checked='<%# bool.Parse(Eval("IS_ACTIVE").ToString()) %>' />
                                                    <asp:Label ID="lblVName" runat="server" Text='<%# (Eval("Variety_Name").ToString()) %>'
                                                        Visible="false" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Variety_Name" HeaderText="Variety Name">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                             <asp:TemplateField HeaderText="Above_10Yr">
                                                <ItemTemplate>
                                                <%--    <asp:Label ID="lblVarCode" runat="server" Text='<%# (Eval("Variety_Code").ToString()) >
                                                        Visible="false" />--%>
                                                    <asp:CheckBox ID="duartionStatus" runat="server" Checked='<%# bool.Parse(Eval("ABOVE_10YEAR").ToString()) %>' />
                                                   <%-- <asp:Label ID="lblVarName" runat="server" Text='<%# (Eval("Variety_Name").ToString()) %>'
                                                        Visible="false" />--%>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
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
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" OnClick="btnSave_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3">
                                    <br />
                                    <fieldset style="width: 350px;">
                                        <asp:GridView ID="gvVariety1" runat="server" CellPadding="4" ForeColor="#333333"
                                            GridLines="None" AutoGenerateColumns="False" CellSpacing="2" Width="300px">
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:CheckBoxField DataField="IS_ACTIVE" HeaderText="Status" />
                                                <asp:BoundField DataField="Variety_Name" HeaderText="Variety Name">
                                                    <FooterStyle HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                            </Columns>
                                            <EditRowStyle BackColor="#7C6F57" />
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
                                    </fieldset>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 30%;">
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnEditPopUp" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="popupEdit" runat="server" OnOkScript="onOk()" OnCancelScript="onCancel()"
                TargetControlID="btnEditPopUp" PopupControlID="pnlEdit" BackgroundCssClass="modalBackground">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlEdit" runat="server" CssClass="modalPopup">
                <table style="width: 100%;" cellspacing="4">
                    <tr>
                        <td>
                            <p class="sansserif">
                                Are you sure to add these following Varities for the seassion
                                <asp:Label ID="lblSeassionName" runat="server" Font-Bold="True" ForeColor="#0099FF"></asp:Label>
                                and the financial year
                                <asp:Label ID="lblYearName" runat="server" Font-Bold="True" ForeColor="#0099FF"></asp:Label></p>
                            under the crop
                            <asp:Label ID="lblCropName" runat="server" Font-Bold="True" ForeColor="#0099FF"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <asp:Panel ID="pnlSelected" runat="server" ScrollBars="Auto" Height="200px">
                                <fieldset>
                                    <asp:GridView ID="gvSelectedVariety" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                        CellSpacing="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl No" ItemStyle-Width="100">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
                                                </ItemTemplate>
                                                <FooterStyle Width="20px" />
                                                <HeaderStyle Width="20px" />
                                                <ItemStyle Width="20px" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Variety_Code" HeaderText="Variety Code" Visible="False" />
                                            <asp:BoundField DataField="Variety_Name" HeaderText="Variety Name" />
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
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
                                </fieldset>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" Width="100px" OnClick="btnConfirm_Click" />
                            &nbsp;<asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

