<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dealer.master" AutoEventWireup="true" CodeFile="RptCurrentStock.aspx.cs" Inherits="Dealer_RptCurrentStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script type="text/javascript">

    function getPrint(print_area) {
        var divToPrint = document.getElementById(print_area);
        var printWin = window.open('', 'Mail In', 'left=0,top=0,width=700,height=600,status=0');
        printWin.document.write("<div align='center' style='width:700px;margin:auto;padding-top:100px'>");
        printWin.document.write(divToPrint.outerHTML);
        printWin.document.write("</div>");
        printWin.document.close();
        printWin.focus();
        printWin.print();
    }
    </script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')" />
    <div id="divPrint">
        <center>
            <strong></strong>
        </center>
        <hr />
        <table width="100%" style="font-weight: 700">
            <tr>
                <td align="center" style="width: 100%">
                    Current Stock Report As On : <asp:Label ID="lblReportAsOn" runat="server"
                        Text=""></asp:Label>
                </td>
            </tr>
        </table>
        <asp:GridView ID="gvCropCat" runat="server" AutoGenerateColumns="False" 
            OnRowDataBound="gvCropCat_RowDataBound" ShowHeader="false" Width="100%" GridLines="None">
            <Columns>
                <asp:TemplateField HeaderText="CropCatg_ID" Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblCropCatg_ID" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Category_Code").ToString())%>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="" Visible="true">
                    <ItemTemplate>
                        <tr>
                            <td width="180px" style="border-style: none" align="left">
                                Crop Category Name 
                            </td>
                            <td width="10px" style="border-style: none" align="left">
                                :
                            </td>
                            <td style="border-style: none" align="left">
                                <asp:Label ID="lblCropCatg_Name" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Category_Name").ToString()) %>'
                                    ForeColor="#0099FF" Font-Bold="True"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <tr>
                            <td colspan="100%" style="border-style: none">
                                <asp:GridView ID="gvCrop" runat="server" Width="100%" AutoGenerateColumns="false"
                                    ShowHeader="false" DataKeyNames="Crop_Code" OnRowDataBound="gvCrop_OnRowDataBound"
                                    GridLines="None">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Crop_ID" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblCrop_ID" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Crop_Code").ToString())%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="" Visible="true">
                                            <ItemTemplate>
                                                <tr>
                                                    <td width="180px" style="border-style: none;" align="left">
                                                        Crop Name 
                                                    </td>
                                                    <td width="10px" style="border-style: none;" align="left">
                                                        :
                                                    </td>
                                                    <td style="border-style: none" align="left">
                                                        <asp:Label ID="lblCrop_Name" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Crop_Name").ToString()) %>'
                                                            ForeColor="#0099FF" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <tr>
                                                    <td colspan="100%" style="border-style: none;">
                                                        <asp:GridView ID="gvCropClass" runat="server" Width="100%" AutoGenerateColumns="false"
                                                            ShowHeader="false" OnRowDataBound="gvCropClass_OnRowDataBound" GridLines="None">
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="" Visible="true">
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td width="180px" style="border-style: none;" align="left">
                                                                                Class
                                                                            </td>
                                                                            <td width="10px" style="border-style: none;" align="left">
                                                                                :
                                                                            </td>
                                                                            <td style="border-style: none;" align="left">
                                                                                <asp:Label ID="lblCROP_CLASS" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("CROP_CLASS").ToString()) %>'
                                                                                    ForeColor="#0099FF" Font-Bold="True"></asp:Label>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <tr>
                                                                            <td colspan="100%" style="border-style: none">
                                                                                <asp:GridView ID="gvDtls" runat="server" Width="100%" AutoGenerateColumns="false"
                                                                                    DataKeyNames="Variety_Code">
                                                                                    <AlternatingRowStyle BackColor="White" />
                                                                                    <Columns>
                                                                                        <asp:TemplateField HeaderText="Sl No.">
                                                                                            <ItemTemplate>
                                                                                                <%#Container.DataItemIndex+1 %>
                                                                                            </ItemTemplate>
                                                                                            <ControlStyle Width="50px" />
                                                                                            <FooterStyle Width="50px" />
                                                                                            <HeaderStyle Width="50px" />
                                                                                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                                                                                        </asp:TemplateField>
                                                                                        <asp:TemplateField HeaderText="Crop_Verid" Visible="false">
                                                                                            <ItemTemplate>
                                                                                                <asp:Label ID="lblCrop_Verid" runat="server" Text='<%#System.Web.HttpUtility.HtmlEncode((string)Eval("Variety_Code").ToString())%>'></asp:Label>
                                                                                            </ItemTemplate>
                                                                                        </asp:TemplateField>
                                                                                        <asp:BoundField DataField="Variety_Name" HeaderText="Variety Name">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="STOCK_QUANTITY" HeaderText="Received Stock(In Qtl)">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                        <asp:BoundField DataField="AVL_QUANTITY" HeaderText="Current Stock(In Qtl)">
                                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                                        </asp:BoundField>
                                                                                    </Columns>
                                                                                    <EditRowStyle BackColor="#7C6F57" />
                                                                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                                                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                                                                    <RowStyle BackColor="#E3EAEB" />
                                                                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                                                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                                                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                                                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                                                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                                                                    <EmptyDataTemplate>
                                                                                        <center>
                                                                                            <span class="style1"><strong>No Record Found</strong></span></center>
                                                                                    </EmptyDataTemplate>
                                                                                </asp:GridView>
                                                                            </td>
                                                                        </tr>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                <center>
                                                                    <span class="style1"><strong>No Data Found</strong></span></center>
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <center>
                                            <span class="style1"><strong>No Data Found</strong></span></center>
                                    </EmptyDataTemplate>
                                </asp:GridView>
                            </td>
                        </tr>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                <center>
                    <span class="style1"><strong>No Data Found</strong></span></center>
            </EmptyDataTemplate>
        </asp:GridView>
    </div>
</asp:Content>

