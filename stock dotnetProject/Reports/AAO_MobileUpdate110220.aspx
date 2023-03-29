<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="AAO_MobileUpdate.aspx.cs" Inherits="Reports_AAO_MobileUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <style>
        .tbl th {
            font: bold 11px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
            color: #4f6b72;
            border-right: 1px solid #C1DAD7;
            border-bottom: 1px solid #C1DAD7;
            border-top: 1px solid #C1DAD7;
            letter-spacing: 2px;
            text-transform: uppercase;
            text-align: left;
            padding: 6px 6px 6px 6px;
            background: #D5EDEF;
        }

        .tbl td {
            border-right: 1px solid #C1DAD7;
            border-bottom: 1px solid #C1DAD7;
            background: #fff;
            padding: 6px 6px 6px 6px;
            color: #4f6b72;
        }

            .tbl td.alt {
                background: #F5FAFA;
                color: #797268;
            }

            .tbl td.boldtd {
                font: bold 13px "Trebuchet MS", Verdana, Arial, Helvetica, sans-serif;
                background: #D5EDEF;
                color: #797268;
            }
    </style>
    <div style="width: 100%" class="auto-style1">
        <fieldset class="tbl">

            <legend style="margin-left: 10px;" class="leg">Enter Your FarmerID</legend>

            <table style="width: 90%; padding: 15px;">
                <tr>
                    <td class="normaltd">Farmer ID           
                    </td>
                    <td>
                        <asp:TextBox ID="txtfarid" runat="server" CssClass="txtbox"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="*Required" ControlToValidate="txtfarid"></asp:RequiredFieldValidator>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Verify" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self' vali;" />
                    </td>
                </tr>

            </table>
        </fieldset>
    </div>
    <div id="divDetails" runat="server" visible="false">
        <table border="0" cellpadding="0" cellspacing="10" width="100%">
            <tr runat="server" id="trFarmdetails">
                <td width="100%">
                    <fieldset style="border-right: #6f92ca 1px solid; border-top: #6f92ca 1px solid; border-left: #6f92ca 1px solid; border-bottom: #6f92ca 1px solid" id="fldMain" runat="server">
                        <legend style="color: #cc0000" id="lgdMain1" runat="server"><strong>FARMER DETAILS</strong></legend>
                        <table border="0" cellpadding="0" cellspacing="10" width="100%" class="text">
                            <tr>

                                <td>Farmer Name</td>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Style="font-weight: 700; color: #0033CC; font-style: italic" Text=""></asp:Label>
                                </td>
                                <td class="auto-style1"><span class="b">Father Name</span></td>
                                <td class="auto-style1">
                                    <asp:Label ID="Label2" runat="server" Style="font-weight: 700; color: #0033CC; font-style: italic" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>District</td>
                                <td>
                                    <asp:Label ID="lblDist" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                </td>
                                <td>Block</td>
                                <td>
                                    <asp:Label ID="lblBlock" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                </td>
                            </tr>

                            <tr>
                                <td>GP</td>
                                <td>
                                    <asp:Label ID="lblBlock0" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                </td>
                                <td>Village</td>
                                <td>
                                    <asp:Label ID="lblBlock1" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1"><span class="b"></span>mobile NO</td>
                                <td>
                                    <asp:Label ID="iblmob" runat="server" Text="" Style="font-weight: 700; color: #0033CC"></asp:Label>
                                </td>

                            </tr>

                            <tr>
                                <td class="auto-style1">New Mobile No</td>

                                <td>
                                    <asp:TextBox ID="txtMobile" runat="server" Style="font-weight: 700; color: #0033CC"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="*Required" ControlToValidate="txtMobile" ValidationGroup="mob"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtMobile" ErrorMessage="Enter Valid Mobile Number"
                                        ValidationExpression="[0-9]{10}" ValidationGroup="mob"></asp:RegularExpressionValidator>

                                </td>
                                <td class="auto-style1">&nbsp;</td>
                                <td class="auto-style1">
                                    <asp:Button ID="btnSave" runat="server" Text="update" OnClick="btnSave_Click" ValidationGroup="mob" />
                                </td>
                            </tr>
                        </table>

                    </fieldset>
                </td>
            </tr>
        </table>
    </div>
    <div align="center" id="DivError" runat="server" visible="false">
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
    </div>
</asp:Content>

