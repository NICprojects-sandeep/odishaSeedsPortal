<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="Rep_FarmerWTOAadhaar.aspx.cs" Inherits="Reports_Rep_FarmerWTOAadhaar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <center>
        <asp:Panel ID="Panel1" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnDist" runat="server" Text="District Wise" OnClick="btnDist_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnBlock" runat="server" Text="Block Wise" OnClick="btnBlock_Click" />
                    </td>
                </tr>
            </table>            
        </asp:Panel>
    <div id="divPrint">
    <asp:Panel ID="pnlContents" runat="server" Width="800px">
    <fieldset>
    <center class="style1"></center>
    <br /><asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
        
        </fieldset>
        </asp:Panel>
        </div>       
    </center>
</asp:Content>

