<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="DealerAccDetails.aspx.cs" Inherits="Masters_DealerAccDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <table>
        <tr>
            <td style="width:25%;">

            </td>
            <td style="width:50%;">
                <table width="100%" style="border:solid; border-width:thin; background-color:aliceblue">
             <tr>
                <td>Licence No</td>
                <td>:</td>
                <td><asp:Label ID ="lblLicNo" runat="server" CssClass="form-control"></asp:Label></td>
              </tr>           
              <tr>
                <td>Firm Name</td>
                <td>:</td>
                <td><asp:Label ID ="lblFirmName" runat="server" CssClass="form-control"></asp:Label></td>
              </tr>
              <tr>
                <td>Dealer / PACS</td>
                <td>:</td>
                <td><asp:Label ID ="lblDealerType" runat="server" CssClass="form-control"></asp:Label></td>
              </tr>
              <tr>
                <td>Account Holder Name</td>
                <td>:</td>
                <td><asp:TextBox ID="txtName" runat="server" CssClass="form-control"></asp:TextBox></td>
              </tr>
              <tr>
                <td>Account No</td>
                <td>:</td>
                <td><asp:TextBox ID="txtAccNo" runat="server" CssClass="form-control"></asp:TextBox></td>
              </tr>
              <tr>
                <td>Bank Name</td>
                <td>:</td>
                <td><asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>Branch Name</td>
                <td>:</td>
                <td><asp:DropDownList ID="ddlBranchName" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlBranchName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>IFSC Code</td>
                <td>:</td>
                <td><asp:Label ID ="lblIFSC" runat="server" CssClass="form-control"></asp:Label></td>
              </tr>
                    <tr>
                        <td colspan="3" align="center">
                            <br />
<asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="ButtonCss" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnCancel" Text="Cancel" BackColor="Red" runat="server" CssClass="ButtonCss"/>
                        </td>
                    </tr>
         </table>
            </td>
            <td style="width:25%;">

            </td>
        </tr>
    </table>
     
</asp:Content>

