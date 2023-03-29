<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="FarmerTypeUpdate.aspx.cs" Inherits="Masters_FarmerTypeUpdate" %>
<%--~/MasterPages/AAO.master--%>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div>
        <div align="center">
            <%--<h2><u>AAO FOR AADHAAR NO UPDATION</u></h2>--%>
        </div>
        <div align="center">
            <span style="text-decoration: underline; color: #993300">TYPE OF FARMER UPDATE</span>
            <br />
            <br />
            <b>Farmer Id : <asp:DropDownList ID="ddlFarmerId" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFarmerId_SelectedIndexChanged" Width="200px"></asp:DropDownList></b>
            <br />
            <br />
        </div>
        <div runat="server" id="divfarmDetails">
            <fieldset class="tbl">
                <legend style="margin-left: 10px;" class="leg">Farmer Details
                </legend>
                <div style="margin-left: 80px">
                    <table style="width: 80%; padding: 5px; flex-align: center">
                        <tr>
                            <td style="width: 16%;" class="normaltd">Farmer Name</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdFarmerName"></td>
                            <td style="width: 16%;" class="normaltd">Father Name</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdFatherName"></td>
                        </tr>
                        <tr>
                            <td style="width: 16%;" class="normaltd">District</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdDistrict"></td>
                            <td style="width: 16%;" class="normaltd">Block</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdBlock"></td>
                        </tr>
                        <tr>
                            <td style="width: 16%;" class="normaltd">GP</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdGp"></td>
                            <td style="width: 16%;" class="normaltd">Village</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdVillage"></td>
                        </tr>
                        <tr>
                            <td style="width: 16%;" class="normaltd">Category</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdCategory"></td>
                            <td style="width: 16%;" class="normaltd">Gender</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdGender"></td>
                        </tr>

                    </table>
                </div>
            </fieldset>
        </div>
        <br />
        <div runat="server" id="divUpdate">
            <fieldset class="tbl">
                <legend style="margin-left: 10px;" class="leg">Farmer Details
                </legend>
                <div style="margin-left: 80px">
                    <table style="width: 80%; padding: 5px; flex-align: center">
                        <tr>
                            <td style="width: 16%;" class="normaltd">Type Of Farmer</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" ><asp:DropDownList ID="ddlFarmerType" runat="server">
                                            <asp:ListItem Value="S">Small</asp:ListItem>
                                            <asp:ListItem Value="M">Marginal</asp:ListItem>

                                            <asp:ListItem Value="L">Large</asp:ListItem>
                                            
                                            
                                        </asp:DropDownList></td>
                            <td style="width: 16%;" ></td>
                            <td style="width: 2%;">&nbsp;</td>
                            <td style="width: 20%;" ></td>
                        </tr>
                        <tr>
                            <td style="width: 16%;" class="normaltd">&nbsp;</td>
                            <td style="width: 2%;">&nbsp;</td>
                            <td style="width: 20%;" runat="server" id="td3">
                                <asp:Button ID="btnUpdate" runat="server" BackColor="#CC9900" BorderColor="Maroon" BorderWidth="2px" Text="Update" Width="90px" OnClick="btnUpdate_Click" style="font-weight: 700" />
                            </td>
                            <td style="width: 16%;" class="normaltd">&nbsp;</td>
                            <td style="width: 2%;">&nbsp;</td>
                            <td style="width: 20%;" runat="server" id="td4"></td>
                        </tr>
                       

                    </table>
                </div>
            </fieldset>
        </div>
        
    </div>
</asp:Content>

