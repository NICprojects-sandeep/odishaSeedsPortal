<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SE4.master" AutoEventWireup="true" CodeFile="CCBADVACE-PAY.aspx.cs" Inherits="Reports_CCBADVACE_PAY" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <%--<link type="text/css" href="http://code.jquery.com/ui/1.10.4/themes/ui-lightness/jquery-ui.css" rel="stylesheet">
      <script type="text/javascript" src="http://code.jquery.com/jquery-1.10.2.js"></script>
      <script type="text/javascript" src="http://code.jquery.com/ui/1.10.4/jquery-ui.js"></script>
      
      <script type="text/javascript">
          $(function () {
              $("#txtfrm").datepicker();
              $("#txtfrm").datepicker("show");
          });
      </script>--%>
    <center>
            <fieldset class="tbl">

                <legend style="margin-left:10px;" class="leg">Select any type</legend>
           
             <table style="width: 90%; padding: 15px;" >
                            <tr>
                                <td style="width: 175px;" class="normaltd">Date</td>
                                <td style="width: 5px;">:</td>
                                <td style="width: 250px;">
                                    <asp:TextBox ID="txtfrm" runat="server" ClientIDMode="Static"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" SetFocusOnError="True"
                                        ControlToValidate="txtfrm" EnableClientScript="False" ValidationExpression="^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$"
                                        ErrorMessage="Enter Proper Date"></asp:RegularExpressionValidator>
                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtfrm"
                                        Format="dd/MM/yyyy">
                                    </cc1:CalendarExtender>

                                </td>
                                <td style="width: 115px;" class="normaltd">&nbsp;</td>
                                <td style="width: 5px;">&nbsp;</td>
                                
                                <td rowspan="3"><asp:Button id="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self'; return Valid();"/>

                                    <asp:Button id="btnConfirm" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
                                </td>
                            </tr>
                            
                 </table>
                 </fieldset>
    
        <asp:GridView ID="grd_FarmerList" runat="server" BackColor="#B2C629"
            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px"
            CellPadding="4" Width="90%" ForeColor="Black">
            <RowStyle BackColor="White" CssClass="tbl" />
            <Columns>

<asp:TemplateField HeaderText="Sl No." HeaderStyle-HorizontalAlign="Left"> <ItemTemplate> <%#Container.DataItemIndex+1 %> </ItemTemplate> </asp:TemplateField>
               
                <asp:TemplateField HeaderText="Request-ID" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_NICFARMERID" runat="server" Text='<%#Eval("CPAID")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NO-OF-FARMER" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_VCHFARMERNAME" runat="server" Text='<%#Eval("nofarmer")%>'></asp:Label>
                    </ItemTemplate>

                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Amount" HeaderStyle-HorizontalAlign="Left">
                    <ItemTemplate>
                        <asp:Label ID="lbl_Category_Value" runat="server" Text='<%#Eval("amount")%>'></asp:Label>
                    </ItemTemplate>
                    
                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                </asp:TemplateField>
               
                
            </Columns>
            <FooterStyle BackColor="#B2C629" />
            <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
            <EmptyDataTemplate>
                <asp:Label ID="Label1" runat="server" Text="Data not found for this Variety,Select another Variety"
                    Font-Bold="true" ForeColor="red"></asp:Label>
            </EmptyDataTemplate>
            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
        </asp:GridView>
        <asp:Literal ID="litReport" runat="server"></asp:Literal>
        <h2>TOTAL AMOUNT TO BE PAID:<asp:Label ID="Label2" runat="server" Text="0"></asp:Label></h2>
        </center>
  
</asp:Content>

