<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Admin.master" AutoEventWireup="true" CodeFile="ExtraSubsidy.aspx.cs" Inherits="Reports_ExtraSubsidy" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <table>
        <tr>
            <td>
                Download Excell
            </td>
            <td>
                Download PDF
            </td>
        </tr>
        <tr>
            <td>
                <asp:HyperLink ID="lbtnE1" runat="server" NavigateUrl="~/Docs/2021-22K25Subsidy1-25000.xlsx">1-25000</asp:HyperLink><br />
                <asp:HyperLink ID="lbtnE2" runat="server" NavigateUrl="~/Docs/2021-22K25Subsidy25001-50000.xlsx">25001-50000</asp:HyperLink><br />
                <asp:HyperLink ID="lbtnE3" runat="server" NavigateUrl="~/Docs/2021-22K25Subsidy50001-75000.xlsx">50001-75000</asp:HyperLink><br />
                <asp:HyperLink ID="lbtnE4" runat="server" NavigateUrl="~/Docs/2021-22K25Subsidy75001-100000.xlsx">75001-100000</asp:HyperLink><br />
                <asp:HyperLink ID="lbtnE5" runat="server" NavigateUrl="~/Docs/2021-22K25Subsidy100001-125000.xlsx">100001-125000</asp:HyperLink><br />
                <asp:HyperLink ID="lbtnE6" runat="server" NavigateUrl="~/Docs/2021-22K25Subsidy125001-127552.xls">125001-127552</asp:HyperLink><br />
<%--
                <asp:LinkButton ID="lbtnE1" runat="server" PostBackUrl="~/Docs/2021-22K25Subsidy1-25000.xlsx">1-25000</asp:LinkButton>
                <asp:LinkButton ID="lbtnE2" runat="server">25001-50000</asp:LinkButton>
                <asp:LinkButton ID="lbtnE3" runat="server">50001-75000</asp:LinkButton>
                <asp:LinkButton ID="lbtnE4" runat="server">75001-100000</asp:LinkButton>
                <asp:LinkButton ID="lbtnE5" runat="server">100001-125000</asp:LinkButton>
                <asp:LinkButton ID="lbtnE6" runat="server">125001-127552</asp:LinkButton>--%>
            </td>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Docs/2020-21K25Subidy1-25000.pdf">1-25000</asp:HyperLink><br />
                <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Docs/2020-21K25Subidy25001-50000.pdf">25001-50000</asp:HyperLink><br />
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Docs/2020-21K25Subidy50001-75000.pdf">50001-75000</asp:HyperLink><br />
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Docs/2020-21K25Subidy75001-100000.pdf">75001-100000</asp:HyperLink><br />
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Docs/2020-21K25Subidy100001-125000.pdf">100001-125000</asp:HyperLink><br />
                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Docs/2020-21K25Subidy125001-127552.pdf">125001-127552</asp:HyperLink><br />
                <%--<asp:LinkButton ID="lbtnP1" runat="server">1-25000</asp:LinkButton>
                <asp:LinkButton ID="lbtnP2" runat="server">25001-50000</asp:LinkButton>
                <asp:LinkButton ID="lbtnP3" runat="server">50001-75000</asp:LinkButton>
                <asp:LinkButton ID="lbtnP4" runat="server">75001-100000</asp:LinkButton>
                <asp:LinkButton ID="lbtnP5" runat="server">100001-125000</asp:LinkButton>
                <asp:LinkButton ID="lbtnP6" runat="server">125001-127552</asp:LinkButton>--%>
            </td>
        </tr>
    </table>
</asp:Content>

