<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="FarmerUpdation.aspx.cs" Inherits="FarmerUpdation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <style>
         .btn {
            background-color: #E6EBED;
            padding: 5px;
            color: black;
            font-family: Tahoma;
            font-size: 12px;
            width: 100px;
            font-weight: bold;
            border-radius: 5px;
            border-collapse: collapse;
        }

            .btn:hover {
                background-color: #CED6D9;
                padding: 5px;
                color: black;
                font-family: Tahoma;
                font-size: 12px;
                width: 100px;
                font-weight: bold;
                border-radius: 5px;
                border-collapse: collapse;
            }

        .txtbox {
            font-family: Tahoma, Geneva, sans-serif;
            font-size: 12px;
            color: #000000;
            width: 280px;
            padding: 5px;
        }
        .etxtbox {
           background-color:palevioletred;
        }
    </style>
    <div style="width: 100%" class="auto-style1">
                    <fieldset class="tbl">

            <legend style="margin-left: 10px;" class="leg">Select any type</legend>

            <table style="width: 90%; padding: 15px;">
                <tr>
                   
                        <td class="normaltd">Farmer ID</td>
                    <td>
                        <asp:TextBox ID="txtfarid" runat="server"  CssClass="txtbox"></asp:TextBox>
                    </td>
                    
                    <td>
                        <asp:Button ID="btnSearch" runat="server" CssClass="btn" Text="Search" OnClick="btnSearch_Click" OnClientClick="aspnetForm.target ='_self';" /></td>
                </tr>
                <tr>
                   
                        <td class="normaltd">&nbsp;</td>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" Visible="False">
                            <asp:ListItem Value="0">Father /Husband Name</asp:ListItem>
                            <asp:ListItem Value="1">Family Details</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </fieldset>
    </div>

    <asp:Panel ID="Panel1" runat="server" Visible="False">
        <table border="0" cellpadding="0" cellspacing="10" width="100%">
                <tr runat="server" id="trFarmdetails">
                    <td width="100%" >
                        <fieldset style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid" id="fldMain" runat="server">
                            <legend style="COLOR: #cc0000" id="lgdMain1" runat="server"><strong>FARMER DETAILS</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="10" width="100%" class="text">
                                <tr>
                                    <td width="20%"></td>
                                    <td width="30%">
                                        <asp:HiddenField ID="HiddenField1" runat="server" />
                                    </td>
                                    <td width="22%">
                                        <asp:HiddenField ID="HiddenField2" runat="server" />
                                    </td>
                                    <td width="28%">
                                        <asp:HiddenField ID="HiddenField3" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">1.</span>District</td>
                                    <td>
                                        <asp:Label ID="lblDist" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label></td>
                                    <td><span class="b">2.</span>Block<span class="c">*</span></td>
                                    <td>
                                        <asp:Label ID="lblBlock" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td><span class="b">3.</span>GP<span class="c">*</span></td>
                                    <td>
                                        <asp:Label ID="lblBlock0" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                    </td>
                                    <td><span class="b">4.</span>Village<span class="c">*</span></td>
                                    <td>
                                        <asp:Label ID="lblBlock1" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">5.</span>PIN Code<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6" CssClass="textCol" onkeypress="return check(event);" placeholder="Pin Code" ReadOnly="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="validatePinCode" runat="server" ErrorMessage="Pin No. required." ControlToValidate="txtPinCode" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ID="rglrPinCode" ValidationExpression="^[0-9]*$" ForeColor="Red" runat="server" ControlToValidate="txtPinCode" ErrorMessage="Pan no. should be Numeric"></asp:RegularExpressionValidator>--%>
                                    </td>
                                    <td><span class="b">6.</span>Name Of Farmer<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtHofNm" runat="server" TextMode="SingleLine" CssClass="textCol" placeholder="HOF Name"  ReadOnly="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ReqtxtHofNm" runat="server" ErrorMessage="Enter HOF Name." ControlToValidate="txtHofNm" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">7.</span>Gender<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPinCode0" runat="server" MaxLength="6" CssClass="textCol" onkeypress="return check(event);" placeholder="Pin Code" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td><span class="b">8.</span>Category<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPinCode1" runat="server" MaxLength="6" CssClass="textCol" onkeypress="return check(event);" placeholder="Pin Code" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">9.</span>Date Of Birth</td>
                                    <td>
                                        <asp:TextBox ID="txtDtBirth" runat="server" CssClass="textCol" placeholder="Date OF Birth" ReadOnly="True" ></asp:TextBox>
                                        <%--<cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtDtBirth"> </cc:FilteredTextBoxExtender>--%>

                                    </td>
                                    <td><span class="b">10.</span>Age<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtAge" runat="server" MaxLength="3" placeholder="Age" onBlur="return maxAge();" Width="100px" ReadOnly="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ReqAge" runat="server" ErrorMessage="Required Age." ControlToValidate="txtAge" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 31px"><span class="b">11.</span>Relation With HOF<span class="c">*</span></td>
                                    <td style="height: 31px">
                                        <asp:TextBox ID="txtDtBirth0" runat="server" CssClass="textCol" placeholder="Date OF Birth" ReadOnly="True" ></asp:TextBox>
                                    </td>
                                    <td style="height: 31px"><span class="b">12.</span>Father/Husband Name<span class="c">*</span></td>
                                    <td style="height: 31px">
                                        <asp:TextBox ID="txtHusbandNm" runat="server" CssClass="textCol" TextMode="SingleLine" placeholder="Father/Husband Name" ReadOnly="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequHusbandNm" runat="server" ErrorMessage="Required Father/Husband Name." ControlToValidate="txtHusbandNm" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">13.</span>Voter Id Card No.<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtVoterId" runat="server" CssClass="textCol" TextMode="SingleLine" placeholder="Voter Id" ReadOnly="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="VoterId" runat="server" ErrorMessage="Required Voter Id." ControlToValidate="txtVoterId" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegPid" runat="server" ControlToValidate="txtVoterId" ForeColor="Red" ErrorMessage="Allow Alphabets/numbers/special characters(/,-)." ValidationExpression="^([0-9a-zA-Z-/%]+$)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td><span class="b">14.</span>Aadher No.</td>
                                    <td>
                                        <asp:TextBox ID="txtAAdhar" runat="server" CssClass="textCol" TextMode="Number" placeholder="Aadher No." ReadOnly="True"></asp:TextBox>


                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">15.</span>Mobile No.<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="textCol" MaxLength="10" onkeypress="return check(event);" placeholder="Mobile No." ReadOnly="True"></asp:TextBox>

                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ReqMobileNo" runat="server" ErrorMessage="Required Mobile No." ControlToValidate="txtMobileNo" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ID="RegMobile" ValidationExpression="^[0-9]*$" ForeColor="Red" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Required number only."></asp:RegularExpressionValidator>--%>

                                    </td>
                                    <td><span class="b">16.</span>No. Of Family Members</td>
                                    <td>
                                        <asp:TextBox ID="txtDtBirth1" runat="server" CssClass="textCol" placeholder="Date OF Birth" ReadOnly="True" ></asp:TextBox>
                                    </td>
                                </tr>

                            </table>

                        </fieldset>
                    </td>
                </tr>
               
                

                <tr id="trBankDetails" runat="server" >
                    <td align="center" >
                        <fieldset id="FIELDSET2" runat="server" style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid">
                            <legend id="LEGEND2" runat="server" style="COLOR: #cc0000"><strong>BANK ACCOUNT DETAILS</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="10" class="text" width="100%">
                              
                                <tr>
                                    <td align="right" style="height: 35px">Account Holder Name&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left" style="height: 35px">
                                        <asp:TextBox ID="txtAccNo0" runat="server" CssClass="textCol" placeholder="Acc. hld Name." ReadOnly="True"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequHusbandNm0" runat="server" ErrorMessage="Required ACC. Name." ControlToValidate="txtAccNo0" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                               
                                    <td align="right" style="height: 35px">Account No.<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left" style="height: 35px">
                                        <asp:TextBox ID="txtAccNo" runat="server" CssClass="textCol" placeholder="Acc. No." ReadOnly="True"></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegAccNo" ValidationExpression="^([0-9a-zA-Z%]+$)" ForeColor="Red" runat="server" ControlToValidate="txtAccNo" ErrorMessage="Required number & alphabit only."></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Bank Name<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBanknm" runat="server" CssClass="textCol" placeholder="Acc. hld Name." ReadOnly="True"></asp:TextBox>
                                    </td>
                                
                                    <td align="right">Branch Name<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtbrnm" runat="server" CssClass="textCol" placeholder="Acc. hld Name." ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">IFSC Code &nbsp;<strong> :</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtIfscCd" runat="server" placeholder="IFSC Code" ReadOnly="true" TextMode="SingleLine"></asp:TextBox>
                                    </td>
                               
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="height: 34px"></td>
                                    <td style="height: 34px">
                                        &nbsp;&nbsp;<%--OnClientClick="return Validate();"--%></td>
                               
                                    <td style="height: 34px">
                                        &nbsp;</td>
                                    <td style="height: 34px">
                                        <asp:Button ID="btnupdate" runat="server" OnClick="btnupdate_Click" Text="Update" Visible="False" />&nbsp;
                                        <asp:Button ID="btnupdate0" runat="server"  Text="Cancel" Visible="False" OnClick="btnupdate0_Click" />
                                    </td>
                                </tr>

                                <tr>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td colspan="2">&nbsp;</td>
                                </tr>

                            </table>
                        </fieldset>
                    </td>
                </tr>
            <tr id="familydetaiis" runat="server" >
                    <td align="center" >
                        <fieldset id="FIELDSET1" runat="server" style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid">
                            <legend id="LEGEND1" runat="server" style="COLOR: #cc0000"><strong>FAMILY DETAILS</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="10" class="text" width="100%">
                              
                                <tr>
                                    <td align="center" width="100%">
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Width="50%">
                        <Columns>
                            <asp:TemplateField HeaderText="SL NO.">
                                 <ItemStyle Width="2%" HorizontalAlign="left" />
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="NAME OF THE MEMBER(*)">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="4%" HorizontalAlign="left" />
                                <%--<EditItemTemplate>
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("VCHNAME") %>' />
                                    <asp:TextBox ID="txt1" runat="server" Text='<%#Eval("VCHNAME") %>' />
                                </EditItemTemplate>--%>
                                <ItemTemplate>
                                    <asp:HiddenField ID="HiddenField1" runat="server" Value='<%#Eval("VCHNAME") %>' />
                                    <asp:TextBox ID="txt1" runat="server" Text='<%#Eval("VCHNAME") %>' ReadOnly="true" BackColor="#cccccc"  ForeColor="Blue" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Age">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle Width="1%" HorizontalAlign="left" />
                                <%-- <EditItemTemplate>
                                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("INTAGE") %>' />
                                    <asp:TextBox ID="txt2" runat="server" Text='<%#Eval("INTAGE") %>' />
                                </EditItemTemplate>--%>
                                <ItemTemplate>
                                    <asp:HiddenField ID="HiddenField2" runat="server" Value='<%#Eval("INTAGE") %>' />
                                    <asp:TextBox ID="txt2" runat="server" Text='<%#Eval("INTAGE") %>' ReadOnly="true" BackColor="#cccccc" ForeColor="Blue"/>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="Chk">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle  Width="1%" HorizontalAlign="Center" />

                                <ItemTemplate>

                                    <asp:CheckBox ID="chkSelect" runat="server"
                                        AutoPostBack="true" 
                                        OnCheckedChanged="chkSelect_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="White" ForeColor="#000066" />
                        <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left" />
                        <RowStyle ForeColor="#000066" />
                        <SelectedRowStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#007DBB" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#00547E" />
                    </asp:GridView>

                                    </td>
                                    
                               
                                   
                                </tr>
                                  <tr>
                                   
                                    <td style="height: 34px; text-align: center;">
                                        <asp:Button ID="Button1" runat="server"  Text="Update" Visible="False" OnClick="Button1_Click" />&nbsp;
                                        <asp:Button ID="Button2" runat="server"  Text="Cancel" Visible="False" OnClick="btnupdate0_Click" />
                                    </td>
                                </tr>
                            

                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
    </asp:Panel>
  
</asp:Content>

