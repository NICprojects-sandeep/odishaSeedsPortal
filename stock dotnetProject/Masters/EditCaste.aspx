<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/AAO.master" CodeFile="EditCaste.aspx.cs" Inherits="EditCaste" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <h3>Edit Farmer Caste</h3>
        <asp:Label ID="lblResult" runat="server" ForeColor="Green"></asp:Label>
        <asp:Panel ID="pnlEdit" runat="server">
         
    <table border="0" cellpadding="0" cellspacing="10" width="100%">
                <tr runat="server" id="trFarmdetails">
                    <td width="100%" >
                        <fieldset style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid" id="fldMain" runat="server">
                            <legend style="COLOR: #cc0000" id="lgdMain1" runat="server"><strong>FARMER DETAILS</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="10" width="100%" class="text">
                                <tr>
                                    <td width="20%"></td>
                                    <td width="30%">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    <td width="22%">
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </td>
                                    <td width="28%">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td><span class="b">1.</span>District</td>
                                    <td>
                                        <asp:Label ID="lblDist" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label></td>
                                    <td><span class="b">2.</span>Block<span class="c">*</span></td>
                                    <td>
                                        <asp:Label ID="lblBlock" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td><span class="b">3.</span>GP<span class="c">*</span></td>
                                    <td>
                                        <asp:Label ID="lblBlock0" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                    </td>
                                    <td><span class="b">4.</span>Village<span class="c">*</span></td>
                                    <td>
                                        <asp:Label ID="lblBlock1" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">5.</span><span class="c">Farmer ID*</span></td>
                                    <td>
                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ID="rglrPinCode" ValidationExpression="^[0-9]*$" ForeColor="Red" runat="server" ControlToValidate="txtPinCode" ErrorMessage="Pan no. should be Numeric"></asp:RegularExpressionValidator>--%>
                                        <asp:Label ID="lblBlock2" runat="server" Style="font-weight: 700; color: #0033CC; font-style: italic" Text=""></asp:Label>
                                    </td>
                                    <td><span class="b">6.</span>Farmer Name<span class="c">*</span></td>
                                    <td>
                                        <asp:Label ID="lblBlock3" runat="server" Style="font-weight: 700; color: #0033CC; font-style: italic" Text=""></asp:Label>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style1"><span class="b">7.</span>Caste<span class="c">*</span></td>
                                    <td class="auto-style1">
                                        <asp:DropDownList ID="DropDownList1" runat="server" BackColor="#CCFFFF" Height="33px">
                                            <asp:ListItem Value="1">General</asp:ListItem>
                                            <asp:ListItem Value="2">SC</asp:ListItem>
                                            <asp:ListItem Value="3">ST</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td class="auto-style1"><span class="b">8.Father Name</span><span class="c">*</span></td>
                                    <td class="auto-style1">
                                        <asp:Label ID="lblBlock4" runat="server" Style="font-weight: 700; color: #0033CC; font-style: italic" Text=""></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="auto-style1">&nbsp;</td>
                                    <td class="auto-style1">&nbsp;</td>
                                    <td class="auto-style1">&nbsp;</td>
                                    <td class="auto-style1"><asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" /></td>
                                </tr>

                            </table>

                        </fieldset>
                    </td>
                </tr>
               
                

            </table>
    </asp:Panel>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>