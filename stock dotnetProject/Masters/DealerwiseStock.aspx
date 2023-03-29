<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true"
    CodeFile="DealerwiseStock.aspx.cs" Inherits="Masters_DealerwiseStock" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <script>
        function blockNonNumbers(obj, e, allowDecimal, allowNegative) {
            var key;
            var isCtrl = false;
            var keychar;
            var reg;

            if (window.event) {
                key = e.keyCode;
                isCtrl = window.event.ctrlKey
            }
            else if (e.which) {
                key = e.which;
                isCtrl = e.ctrlKey;
            }

            if (isNaN(key)) return true;

            keychar = String.fromCharCode(key);

            // check for backspace or delete, or if Ctrl was pressed
            if (key == 8 || isCtrl) {
                return true;
            }

            reg = /\d/;
            var isFirstN = allowNegative ? keychar == '-' && obj.value.indexOf('-') == -1 : false;
            var isFirstD = allowDecimal ? keychar == '.' && obj.value.indexOf('.') == -1 : false;

            return isFirstN || isFirstD || reg.test(keychar);
        }

    </script>
      <script>

          function Valid() {
             
              var Session = document.getElementById("<%=drpSeason.ClientID %>");
              if (Session.value.trim() == "0") {
                  alert('Please Select Season !');
                  Session.focus();
                  return false;
              }
              var drpCropName = document.getElementById("<%=drpCropName.ClientID %>");
              if (drpCropName.value.trim() == "--Select--") {
                  alert('Please Select Crop Name !');
                  drpCropName.focus();
                  return false;
              }
        }
    </script>
    <ajax:UpdatePanel ID="updatepanel" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table style="width: 100%; padding: 0px;" class="tabletxt" cellpadding="0" cellspacing="10">
                <tr>

                    <td style="color: red; font-weight: bold;" class="normaltd" colspan="10">Please enter the cumulative figure only as on date : 
                        <asp:Literal ID="litDt" runat="server"></asp:Literal>
                    </td>

                </tr>
                <tr>
                     <td style="width: 81px;" class="normaltd">Session Year</td>
                                <td style="width: 5px;">:</td>
                    <td style="width: 108px">
                        <asp:DropDownList ID="drpSession" runat="server" CssClass="DropdownCss">
                                </asp:DropDownList></td>
                    <td style="width: 51px;" class="normaltd">Season</td>
                    <td style="width: 5px;">:</td>
                    <td style="width: 119px">

                        <asp:DropDownList ID="drpSeason" runat="server" CssClass="DropdownCss" Width="120px">
                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                            <asp:ListItem Value="K">Kharif</asp:ListItem>
                            <asp:ListItem Value="R">Rabi</asp:ListItem>
                        </asp:DropDownList>

                    </td>

                    <td style="width: 72px;" class="normaltd">Crop Name</td>
                    <td style="width: 5px;">:</td>
                    <td style="width: 207px">

                        <asp:DropDownList ID="drpCropName" runat="server" CssClass="DropdownCss">
                        </asp:DropDownList></td>

                    <td>

                        <asp:Button ID="btnSearch" runat="server" Text="Search" Width="98px" OnClick="btnSearch_Click" OnClientClick="return Valid();" />
                    </td>
                </tr>
                <tr>
                    <td colspan="10">

                        <asp:GridView ID="grdDealerwiseStock" runat="server" BackColor="#B2C629"
                            AutoGenerateColumns="False" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" DataKeyNames="LIC_NO"
                            CellPadding="4" Width="90%" ForeColor="Black">
                            <RowStyle BackColor="White" CssClass="tbl" />
                            <Columns>

                                <asp:TemplateField HeaderText="Sl No">
                                    <ItemTemplate>
                                        <%# Container.DataItemIndex + 1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gram Panchayat" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpan_name" runat="server" Text='<%#Eval("pan_name")%>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dealer's Name" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDealer" runat="server" Text='<%#Eval("APP_FIRMNAME")%>'></asp:Label>
                                        -&nbsp; ( 
                                <asp:Label ID="lblLIC_NO" runat="server" Text='<%#Eval("LIC_NO")%>'></asp:Label>
                                        )
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mobile No" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMob" runat="server" Text='<%#Eval("APPMOB_NO")%>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAPP_TYPE" runat="server" Text='<%#Eval("APP_TYPE")%>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lifting Done(Received)" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblACTUAL_RECEIVE" runat="server" Text='<%#Eval("ACTUAL_RECEIVE")%>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Physical Sale" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPhySale" AutoPostBack="true" runat="server" onkeypress="return blockNonNumbers(this, event, true, false);" MaxLength="6" OnTextChanged="txtPhySale_TextChanged" Text='<%#Eval("SALE_QTY")%>'></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Online Sale Entry" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblACTUAL_SALE" runat="server" Text='<%#Eval("ACTUAL_SALE")%>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Offline Sale" HeaderStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" runat="server" Text='<%#Eval("balance")%>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Left"></HeaderStyle>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#B2C629" />
                            <PagerStyle HorizontalAlign="Left" CssClass="pagination" />
                            <EmptyDataTemplate>
                                <asp:Label ID="Label1" runat="server" Text="Data not found for this Crop"
                                    Font-Bold="true" ForeColor="red"></asp:Label>
                            </EmptyDataTemplate>
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#E6EBED" Font-Bold="True" ForeColor="black" />
                        </asp:GridView>
                    </td>
                </tr>

            </table>

            <div style="text-align: center;">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
            </div>
        </ContentTemplate>
    </ajax:UpdatePanel>
</asp:Content>
