<%@ Page Title="::Date Wise Receive::" Language="C#" MasterPageFile="~/MasterPages/STOCK.master" AutoEventWireup="true" CodeFile="RptDateWiseRcv.aspx.cs" Inherits="Reports_RptDateWiseRcv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server"> 
    <script type="text/javascript">

        function getPrint(print_area) {
            var divToPrint = document.getElementById(print_area);
            var printWin = window.open('', 'Mail In', 'left=0,top=0,width=1000,height=600,status=0');
            printWin.document.write("<div align='center' style='width:1000px;margin:auto;padding-top:10px'>");
            printWin.document.write(divToPrint.outerHTML);
            printWin.document.write("</div>");
            printWin.document.close();
            printWin.focus();
            printWin.print();
        }
</script> 
        <asp:Panel ID="Panel1" runat="server" BackColor="White">
            <asp:UpdatePanel ID="UpdPnl" runat="server">
                <ContentTemplate>

                                <table style="border:solid;" >
            <tr>
                <td style="width:100px;">
                    Receive Date
                </td>
                <td style="width:100px;">
                    
                    <asp:TextBox ID="txtFromDate" runat="server" AutoPostBack="True" 
                                        CssClass="TextBox" ontextchanged="txtFromDate_TextChanged" Width="100px" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" 
                                        TargetControlID="txtFromDate" WatermarkCssClass="watermarked" 
                                        WatermarkText="dd/mm/yyyy">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" 
                                        TargetControlID="txtFromDate">
                                    </asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                                        ControlToValidate="txtFromDate" Display="Dynamic" ErrorMessage="invalid date" 
                                        ForeColor="Red" SetFocusOnError="true" 
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$">Date Format (dd/mm/yyyy)</asp:RegularExpressionValidator>
                </td>
                <td style="width:30px;text-align:center;">
                    -
                </td>
                <td style="width:100px;">                    
                    <asp:TextBox ID="txtToDate" runat="server" AutoPostBack="True" 
                                        CssClass="TextBox" ontextchanged="txtToDate_TextChanged" Width="100px" />
                                    <asp:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender2" runat="server" 
                                        TargetControlID="txtToDate" WatermarkCssClass="watermarked" 
                                        WatermarkText="dd/mm/yyyy">
                                    </asp:TextBoxWatermarkExtender>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" 
                                        TargetControlID="txtToDate">
                                    </asp:CalendarExtender>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                        ControlToValidate="txtToDate" Display="Dynamic" ErrorMessage="invalid date" 
                                        ForeColor="Red" SetFocusOnError="true" 
                                        ValidationExpression="^(0[1-9]|[12][0-9]|3[01])[-/.](0[1-9]|1[012])[-/.](19|20)\d\d$">Date Format (dd/mm/yyyy)</asp:RegularExpressionValidator>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" Width="100px" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table> 
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="getPrint('divPrint')"/>
                                        </td>
                                    </tr>
                                </table>  
                                <div id="divPrint">
                                <asp:Label ID="lblText" runat="server" Text=""></asp:Label>
                                <asp:GridView ID="gv_DTL" runat="server" AutoGenerateColumns="False" CellPadding="3" OnPreRender="gv_DTL_PreRender" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2" >
                        <Columns>
                            <asp:BoundField DataField="RECV_ID" HeaderText="RECV_ID" Visible="False" />                            
                            <asp:BoundField DataField="RECV_DATE" HeaderText="Receive Date" />
                            <asp:BoundField DataField="CHALLAN_NO" HeaderText="Challan No" />
                            <asp:BoundField DataField="Receive_Unitname" HeaderText="Source" />
                            <asp:BoundField DataField="Category_Name" HeaderText="Category" />
                            <asp:BoundField DataField="Crop_Name" HeaderText="Crop" />
                            <asp:BoundField DataField="Variety_Name" HeaderText="Variety" />
                            <asp:BoundField DataField="CLASS" HeaderText="Class" />
                            <asp:BoundField DataField="LOT_NO" HeaderText="Lot No">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="BAG_SIZE_IN_KG" HeaderText="Bag Size(Kg)">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="RECV_NO_OF_BAGS" HeaderText="No of Bags">
                            <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Recv_Quantity" HeaderText="Quantity(Qtl)"/>
                        </Columns>
                        <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                        <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                        <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                        <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#FFF1D4" />
                        <SortedAscendingHeaderStyle BackColor="#B95C30" />
                        <SortedDescendingCellStyle BackColor="#F1E5CE" />
                        <SortedDescendingHeaderStyle BackColor="#93451F" />
                    </asp:GridView>
                                    </div>                            
                </ContentTemplate>
            </asp:UpdatePanel>        
    </asp:Panel>
</asp:Content>

