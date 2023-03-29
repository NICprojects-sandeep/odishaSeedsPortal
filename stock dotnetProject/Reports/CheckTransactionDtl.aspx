<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Scheme.master" AutoEventWireup="true" CodeFile="CheckTransactionDtl.aspx.cs" Inherits="Reports_CheckTransactionDtl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
       <div class="titleHeader">
        &nbsp;
      Farmer Sale
    </div>
    <br />
    <div>
        <table>
            <tr>
                <td>
                    Transaction ID : 
                </td>
                <td>
                    <asp:TextBox ID="txtTransactionID" runat="server"></asp:TextBox>
                </td>
                <td>
                    <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server"  CssClass="tbltxt" Style="padding-top: 30px;">
            <asp:GridView ID="gvTransactionDtls" runat="server"></asp:GridView>
        </asp:Panel>
    </div>
    
</asp:Content>

