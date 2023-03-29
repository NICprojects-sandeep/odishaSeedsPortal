<%@ Page Title="::Crop Category::" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="CropCategory.aspx.cs" Inherits="Admin_CropCategory" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style type="text/css">
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 300px;
            height:350px;
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
        p.sansserif {
    font-family: Arial, Helvetica, sans-serif;
}
    </style>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>--%>
<%--    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>--%>
    <asp:UpdatePanel ID="UpdPnl1" runat="server" UpdateMode="Always">
        <ContentTemplate>
<table style="width:100%;">
<tr>
<td style="width:30%;">

</td>
<td style="width:40%;">
<table style="width:100%;">
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
<td align="center" colspan="3">
<br />
<fieldset style="width:350px;">
    <asp:GridView ID="gvCategory" runat="server" CellPadding="4" ForeColor="#333333" 
        GridLines="None" AutoGenerateColumns="False" CellSpacing="2" 
        style="height: 138px" Width="300px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:BoundField DataField="CATEGORY_CODE" HeaderText="CATEGORY_CODE" Visible="False" />
            <asp:templatefield HeaderText="Select">
                    <itemtemplate>
                        <asp:Label ID="lblCategoryCode" runat="server" Text='<%# (Eval("CATEGORY_CODE").ToString()) %>' Visible="false"/>
                        <asp:checkbox ID="cbStatus" runat="server" Checked='<%# bool.Parse(Eval("IS_ACTIVE").ToString()) %>'/>
                        <asp:Label ID="lblCategoryName" runat="server" Text='<%# (Eval("Category_Name").ToString()) %>' Visible="false"/>

                    </itemtemplate>
                </asp:templatefield>
            <asp:BoundField DataField="Category_Name" HeaderText="Category Name" >
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
<tr>
<td align="center" colspan="3">

    <asp:Button ID="btnSave" runat="server" Text="Save" Width="100px" 
        onclick="btnSave_Click"/>

</td>
</tr>
<tr>
<td  align="center" colspan="3">
<br />
<fieldset style="width:350px;">
    <asp:GridView ID="gvCategory1" runat="server" CellPadding="4" 
        ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" 
        CellSpacing="2" Width="300px">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:CheckBoxField DataField="IS_ACTIVE" HeaderText="Status" />
            <asp:BoundField DataField="Category_Name" HeaderText="Category Name">
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
<td style="width:30%;">

</td>
</tr>
</table>

<asp:Button ID="btnEditPopUp" runat="server" Style="display: none" />
<asp:ModalPopupExtender ID="popupEdit" runat="server" OnOkScript="onOk()" OnCancelScript="onCancel()"
                    TargetControlID="btnEditPopUp" PopupControlID="pnlEdit" BackgroundCssClass="modalBackground">
                </asp:ModalPopupExtender>
<asp:Panel ID="pnlEdit" runat="server" CssClass="modalPopup" Style="display: none">
<table style="width:100%;" cellspacing="4">
<tr>
<td>
<p class="sansserif">Are you sure to add these following categories for the seassion 
    <asp:Label ID="lblSeassionName" runat="server" Font-Bold="True" 
        ForeColor="#0099FF"></asp:Label> and the financial year 
    <asp:Label ID="lblYearName" runat="server" Font-Bold="True" ForeColor="#0099FF"></asp:Label></p>

</td>
</tr>
<tr>
<td align="center">
<br />
<fieldset style="width:300px;">
<asp:GridView ID="gvSelectedGropCategory" runat="server" 
        AutoGenerateColumns="False" CellPadding="4" CellSpacing="4" 
        ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
        <asp:TemplateField HeaderText = "Sl No" ItemStyle-Width="100">
        <ItemTemplate>
            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" />
        </ItemTemplate>
            <FooterStyle Width="20px" />
            <HeaderStyle Width="20px" />
            <ItemStyle Width="20px" />
    </asp:TemplateField>
            <asp:BoundField DataField="CATEGORY_CODE" HeaderText="Category Name" Visible="False" />
            <asp:BoundField DataField="CATEGORY_NAME" HeaderText="Category Name" />
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
<tr>
<td  align="center">
<br />
<asp:Button ID="btnConfirm" runat="server"  Text="Confirm" Width="100px" 
        onclick="btnConfirm_Click" />
&nbsp;<asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" Width="100px" />
</td>
</tr>
</table>
</asp:Panel>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>

