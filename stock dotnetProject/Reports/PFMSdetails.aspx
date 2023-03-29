<%@ Page Language="C#" AutoEventWireup="true"  MasterPageFile="~/MasterPages/SeedAdmin.master" CodeFile="PFMSdetails.aspx.cs" Inherits="PFMSdetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Grid td
        {
            background-color: #ef9d39;
            color: black;
            font-size: 10pt;
            line-height:200%
        }
        .Grid th
        {
            background-color: #6396ed;
            color: White;
            font-size: 10pt;
            line-height:200%
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height:200%
        }
    </style>

<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    $("[src*=plus]").live("click", function () {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "../images/minus.png");
    });
    $("[src*=minus]").live("click", function () {
        $(this).attr("src", "../images/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>

    <center runat="server">
    <div class="panel panel-success" runat="server">
    <br />
    
    <div style="text-align:center" runat="server">
                        <b>Enter CPAID :</b>
                    
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
    <asp:Button ID="btnsrch" Text="Search" runat="server" onclick="btnsrch_Click" />
    <%--<asp:Button ID="btnsrch" Text="Search" runat="server" OnClick="SearchCustomers"/>--%>
    </div>
    <br />
    <div runat="server">
    <asp:GridView ID="gvpfms" runat="server" AutoGenerateColumns="false" CssClass="Grid"
    DataKeyNames="DEALER_CODE" OnRowDataBound="OnRowDataBound" style="width:70%" >
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="../images/plus.png" />
                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                    <asp:GridView ID="gvtrans" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid" ShowFooter="true" >
                        <Columns>
                   <asp:TemplateField HeaderText="SL NO"  HeaderStyle-Width="20px"  HeaderStyle-HorizontalAlign="Left"  >
                  <HeaderStyle HorizontalAlign="Left" Font-Size="small" />
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="small" />
                   <ItemTemplate>
                    <%# ((GridViewRow)Container).RowIndex + 1%>
                </ItemTemplate>
                </asp:TemplateField>
                            <asp:BoundField ItemStyle-Width="180px" DataField="UPDATED_ON" HeaderText="TRANSACTION DATE AND TIME" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="TRANSACTION_ID" HeaderText="TRANSACTION ID" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="FARMER_ID" HeaderText=" FARMER ID" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Beneficiary_Name" HeaderText=" FARMER NAME" />
                            <asp:BoundField ItemStyle-Width="120px" DataField="Crop_Name" HeaderText=" CROP NAME" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Variety_Name" HeaderText=" VARIETY NAME" />
                            <asp:BoundField ItemStyle-Width="90px" DataField="TOT_QTL" HeaderText=" QUANTITY (In QTL)" DataFormatString="{0:N1}" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Total_Payment_Amount" HeaderText=" SUBSIDY TO BE PAID" DataFormatString="{0:N2}" />   
                                             
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="Small" />
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  Font-Size="Smaller" />
              <HeaderTemplate>DEALER CODE</HeaderTemplate>
              <ItemTemplate>
              <asp:Label runat="server" ID="lbldlrcode" Font-Size="Medium" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("DEALER_CODE").ToString()) %>' ></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>
              <asp:TemplateField>
        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="Small" />
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle"  Font-Size="Smaller" />
              <HeaderTemplate>DEALER NAME</HeaderTemplate>
              <ItemTemplate>
              <asp:Label runat="server" ID="lbldlrname" Font-Size="Medium" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("DealerName").ToString()) %>' ></asp:Label>
              </ItemTemplate>
              </asp:TemplateField>
        <%--<asp:BoundField ItemStyle-Width="150px" DataField="DEALER_CODE" HeaderText="DEALER CODE" />--%>
        <%--<asp:BoundField ItemStyle-Width="150px" DataField="Beneficiary_Name" HeaderText="FARMER NAME" />--%>
    </Columns>
    
</asp:GridView>
<br />

<asp:Button ID="btnExport" runat="server" Text="Export To Excel" 
            onclick="btnExport_Click" />

    </div>
    </div>
    </center>
</asp:Content>
