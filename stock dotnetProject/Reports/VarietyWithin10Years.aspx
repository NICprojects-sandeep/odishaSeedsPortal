<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="VarietyWithin10Years.aspx.cs" Inherits="Reports_VarietyWithin10Years" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table width="100%">
        <tr>
            <td style="font-weight:bold;color:maroon;text-align:center;font-size:15px;">
                Agency : <asp:Label ID="lblAgency" runat="server" Text="" ForeColor="Green"></asp:Label>,&nbsp;&nbsp;&nbsp;&nbsp;
                Crop : <asp:Label ID="lblCrop" runat="server" Text="" ForeColor="Green"></asp:Label>,&nbsp;&nbsp;&nbsp;&nbsp;
                Financial Year : <asp:Label ID="lblFinYr" runat="server" Text="" ForeColor="Green"></asp:Label>,&nbsp;&nbsp;&nbsp;&nbsp;
                Varieties Within 10Years
                <asp:Button ID="btnExport" runat="server" Text="Export To Excel" OnClick = "ExportToExcel" />
            </td>
        </tr>
    </table>
    <br />
    <asp:GridView ID="gvVarieties" runat="server" AutoGenerateColumns="False" ShowFooter="True">
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                        <%#Container.DataItemIndex+1 %>
                </ItemTemplate>
            </asp:TemplateField>            
            <asp:BoundField DataField="VARIETY_NAME" HeaderText="Variety Name" />
        </Columns>
    </asp:GridView>
</asp:Content>

