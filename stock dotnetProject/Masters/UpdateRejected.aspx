<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="UpdateRejected.aspx.cs" Inherits="Masters_UpdateRejected" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    
    <link href="../bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <div class="row">
        <div class="col-2">

        </div>
        <div class="col-8">
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
            <%--<table class="table table-bordered">
            <thead>
              <tr>
                <th>Licence Number</th>
                <th>Firm Name</th>
                <th>Dealer / PACS</th>
                <th>Reject Reason</th>
                <th>Action</th>
              </tr>
            </thead>
            <tbody>
              <tr>
                <td>John</td>
                <td>Doe</td>
                <td>john@example.com</td>
                  <td>Doe</td>
                <td><a href='DealerAccDetails.aspx'>john@example.com</a></td>
              </tr>
              <tr>
                <td>Mary</td>
                <td>Moe</td>
                <td>mary@example.com</td>
              </tr>
              <tr>
                <td>July</td>
                <td>Dooley</td>
                <td>july@example.com</td>
              </tr>
            </tbody>
          </table>--%>
        </div>
        <div class="col-2">

        </div>
    </div>
    <%--<table width="80%">
        <tr>
            <td>
                Licence Number
            </td>
            <td>
                <asp:DropDownList ID="ddlDealer" runat="server" CssClass="DropdownCss" AutoPostBack="true" OnSelectedIndexChanged="ddlDealer_SelectedIndexChanged"></asp:DropDownList>
            </td>
            <td>

            </td>
        </tr>
    </table>
    <br />
    <table>
        <tr>
            <td>
                Licence Number
            </td>
            <td>
                <asp:TextBox ID="txtLicNo" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
                Firm Name
            </td>
            <td>
                <asp:TextBox ID="txtFirmName" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Aadhaar Number
            </td>
            <td>
                <asp:TextBox ID="txtAadhaar" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
                A/cHolder Name
            </td>
            <td>
                <asp:TextBox ID="txtAcName" runat="server" Width="100%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Applicant Type
            </td>
            <td style="margin-left: 40px">

                <asp:DropDownList ID="ddlAppType" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAppType_SelectedIndexChanged">
                    <asp:ListItem Selected="True">PACS</asp:ListItem>
                    <asp:ListItem>Dealer</asp:ListItem>
                </asp:DropDownList>

            </td>
            <td>
                &nbsp;</td>
            <td>

            </td>
        </tr> 
        <tr>
            <td>
                A/c Number
            </td>
            <td>
                <asp:TextBox ID="txtAcNo" runat="server" Width="100%"></asp:TextBox>
            </td>
            <td>
                IFSC Code
            </td>
            <td>
                <asp:TextBox ID="txtIFSC" runat="server" AutoPostBack="True" OnTextChanged="txtIFSC_TextChanged" Width="100%"></asp:TextBox>
            </td>
        </tr>       
        <tr>
            <td>
                Bank Name
            </td>
            <td>
                <asp:TextBox ID="txtBankName" runat="server" Width="100%"></asp:TextBox>
                <asp:Label ID="lblBankId" runat="server" Visible="False"></asp:Label>
            </td>
            <td>
                Branch Name
            </td>
            <td>
                <asp:TextBox ID="txtBranchName" runat="server" Width="100%"></asp:TextBox>
                <asp:Label ID="lblBranchId" runat="server" Visible="False"></asp:Label>
            </td>
        </tr> 
        <tr>
            <td>

            </td>
            <td colspan="3">
                <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
            </td>            
        </tr>       
        <tr>
            <td>

            </td>
            <td colspan="2">
                <asp:Button ID="btnEdit" runat="server" Text="Edit" Width="100px" OnClick="btnEdit_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="100px" OnClick="btnUpdate_Click"></asp:Button>&nbsp;
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="100px" OnClick="btnCancel_Click"></asp:Button>
            </td>
            <td>

            </td>
        </tr>
    </table>--%>
</asp:Content>

