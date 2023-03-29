<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dist.master" AutoEventWireup="true" CodeFile="FarmersWTOAadhaar.aspx.cs" Inherits="Reports_FarmersWTOAadhaar" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <center>
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

