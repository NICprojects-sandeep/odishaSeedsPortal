<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="RptCropDistSale.aspx.cs" Inherits="Reports_RptCropDistSale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <div class="tbltxt">
            <div style="float: left; height: 50px;">
                Financial Year &nbsp;:&nbsp;
              <asp:DropDownList ID="ddlFinYear" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="2021-22">2021-22</asp:ListItem>
                                        <asp:ListItem Value="2022-23">2022-23</asp:ListItem>
                                    </asp:DropDownList>
                Season &nbsp;:&nbsp;
               <asp:DropDownList ID="ddlSeason" runat="server" CssClass="DropdownCss">
                                        <asp:ListItem Value="R">Rabi</asp:ListItem>
                                        <asp:ListItem Value="K">Kharif</asp:ListItem>
                                    </asp:DropDownList>                        
                <asp:Button ID="btnSearch" runat="server" Text="Search" Width="128px" OnClick="btnSearch_Click" />
                <asp:Button ID="btnExport" runat="server" Text="Export to Excel" Width="128px" OnClick="btnToExcel_Click" />
            </div>
            <br />
            <asp:Literal ID="litReport" runat="server"></asp:Literal>
        </div>
    <%--<table>
        <tr >    
            <td rowspan="2" style="text-align:center;"></td>
            <td colspan="6"></td>
            <td></td>
        </tr>
        <tr>    
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
        <tr>    
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
            <td></td>
        </tr>
    </table>--%>
</asp:Content>

