<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Scheme.master" AutoEventWireup="true" CodeFile="PFMSdetails1.aspx.cs" Inherits="Reports_PFMSdetails1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
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

    <center>
    <div class="panel panel-success">
    <br />
    
    <div class="col-lg-3 col-md-3 col-sm-3" style="text-align:center">
                        <b>Enter CPAID :</b>
                    
    <asp:TextBox ID="txtSearch" runat="server"></asp:TextBox>
    <asp:Button ID="btnsrch" Text="Search" runat="server" onclick="btnsrch_Click" />
    <%--<asp:Button ID="btnsrch" Text="Search" runat="server" OnClick="SearchCustomers"/>--%>
    </div>
    <br />
    <div>
    <asp:GridView ID="gvpfms" runat="server" AutoGenerateColumns="false" CssClass="Grid"
    DataKeyNames="DEALER_CODE" OnRowDataBound="OnRowDataBound" style="width:70%">
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <img alt = "" style="cursor: pointer" src="../images/plus.png" />
                <asp:Panel ID="pnlOrders" runat="server" Style="display: none">
                    <asp:GridView ID="gvtrans" runat="server" AutoGenerateColumns="false" CssClass = "ChildGrid" ShowFooter="true" OnRowCommand="gvtrans_RowCommand">
                        <Columns>
                   <asp:TemplateField HeaderText="SL NO"  HeaderStyle-Width="20px"  HeaderStyle-HorizontalAlign="Left"  >
                  <HeaderStyle HorizontalAlign="Left" Font-Size="small" />
              <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="small" />
                   <ItemTemplate>
                    <%# ((GridViewRow)Container).RowIndex + 1%>
                </ItemTemplate>
                </asp:TemplateField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="UPDATED_ON" HeaderText="TRANSACTION DATE AND TIME" />
                            <%--<asp:BoundField ItemStyle-Width="150px" DataField="TRANSACTION_ID" HeaderText="TRANSACTION ID" />--%>
                            <asp:TemplateField HeaderText="TRANSACTION ID">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_TRANSACTION_ID" runat="server" Text='<%#Eval("TRANSACTION_ID")%>' CausesValidation="false" CommandName="Select"></asp:LinkButton>                                        
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:TemplateField>
                            <asp:BoundField ItemStyle-Width="150px" DataField="FARMER_ID" HeaderText=" FARMER ID" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Beneficiary_Name" HeaderText=" FARMER NAME" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Crop_Name" HeaderText=" CROP NAME" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Variety_Name" HeaderText=" VARIETY NAME" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="TOT_QTL" HeaderText=" QUANTITY (In QTL)" DataFormatString="{0:N2}" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="Total_Payment_Amount" HeaderText=" SUBSIDY TO BE PAID" DataFormatString="{0:N2}" />   
                                             
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <HeaderStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="Small" />
            <ItemStyle HorizontalAlign="Left" VerticalAlign="Middle" Font-Size="Smaller" />
            <HeaderTemplate>DEALER CODE</HeaderTemplate>
            <ItemTemplate>
                <asp:Label runat="server" ID="lbldlrcode" Font-Size="Medium" Text='<%# System.Web.HttpUtility.HtmlEncode((string)Eval("DEALER_CODE").ToString()) %>'></asp:Label>
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
    </div>
    </div>
    </center>
</asp:Content>

