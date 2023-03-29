<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="NIC_AAO_REPORT.aspx.cs" Inherits="Reports_NIC_AAO_REPORT" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
    <script src="../jscript48/AadharValidator.js"></script>
     <script language="javascript" type="text/javascript">
         function CnfSubmit() {

             if (confirm("Want to Correct account holdername ?")) {
                 var cur = document.getElementById("ch");
                 document.getElementById("chx").removeAttribute("style");
                 document.getElementsByClassName("up")[0].setAttribute("style", "display:none;");
                 document.getElementsByClassName("up")[1].setAttribute("style", "display:none;");
                 document.getElementsByClassName("up")[2].setAttribute("style", "display:none;");
                 document.getElementsByClassName("up")[3].setAttribute("style", "display:none;");
                 document.getElementsByClassName("up")[4].setAttribute("style", "display:none;");
                 cur.removeAttribute("style");
                 document.getElementById("ctl00_ContentPlaceHolder2_txtAccNo0").readOnly = false;
                 document.getElementById("chan").setAttribute("style", "display:none;");
                 document.getElementById("ctl00_ContentPlaceHolder2_ddlacch").disabled = true;
                 
                 return true;
             }
             else {

                 return false;
             }
         }
         function Validate() {


             var xx = $("[id*=txtAAdhar]").val();
             var Mob = $("[id*=txtMobileNo]").val();
             //alert(Mob);
             if ($("[id*=txtAAdhar]").val() != "") {

                 if (!MinimumLengthValidation(xx, '12')) {

                     return false;
                 }
                 if (!MaximumLengthValidation(xx, '12')) {
                     return false;
                 }
                 if (!(ValidAadhaarNo($("[id*=txtAAdhar]").val()))) {
                     alert('Invalid Aadhar number');
                     return false;
                 }
                 
             }
             
             if (Mob != "") {

                 if (!MinimumLengthValidationm(Mob, '10')) {
                     return false;
                 }
                 if (!MaximumLengthValidationm(Mob, '10')) {
                     return false;
                 }

                 if (!MobileNumber('txtMobileNo')) {
                     return false;
                 }
             }
             else {
                 alert('Enter  Mobile Number');
                 $("[id *= txtMobileNo]").focus();
                 return false;
             }
         }
         function MobileNumber(cntr) {
             var Mobile = /^[7-9][0-9]{9}$/
             var entered_no = $("[id *= txtMobileNo]").val();
             if (!Mobile.test(entered_no)) {
                 alert('Enter a valid Mobile Number');
                 $("[id *= txtMobileNo]").focus();
                 return false;
             }
             else
                 return true;
         }
         function MinimumLengthValidation(textbox, length) {


             try {
                 // var textbox = $('#' + textbox);
                 if (textbox.length < length) {
                     alert("Please enter minimum " + length + " digits in Aadhar field");
                     textbox.focus();
                     return false;
                 }
                 else {
                     return true;
                 }

             } catch (e) {

             }
         }

         function MaximumLengthValidation(textbox, length) {
             try {
                 //var textbox = $('#' + textbox);
                 if (textbox.length > length) {
                     alert("Please enter Maximum " + length + " digits in Aadhar field");
                     textbox.focus();
                     return false;
                 }
                 else {
                     return true;
                 }

             } catch (e) {

             }
         }
         function MinimumLengthValidationm(textbox, length) {


             try {
                 // var textbox = $('#' + textbox);
                 if (textbox.length < length) {
                     alert("Please enter minimum " + length + " digits in mob field");
                     textbox.focus();
                     return false;
                 }
                 else {
                     return true;
                 }

             } catch (e) {

             }
         }

         function MaximumLengthValidationm(textbox, length) {
             try {
                 //var textbox = $('#' + textbox);
                 if (textbox.length > length) {
                     alert("Please enter Maximum " + length + " digits in mob field");
                     textbox.focus();
                     return false;
                 }
                 else {
                     return true;
                 }

             } catch (e) {

             }
         }
    </script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <table border="0" cellpadding="0" cellspacing="10" width="100%">
                <asp:HiddenField ID="hfSession" runat="server" Visible="false" />

                <tr runat="server" id="trFarmdetails">
                    <td width="100%" >
                        <fieldset style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid" id="fldMain" runat="server">
                            <legend style="COLOR: #cc0000" id="lgdMain1" runat="server"><strong>FARMER DETAILS</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="10" width="100%" class="text">
                                <tr>
                                    <td width="20%">
                                        <asp:HiddenField ID="HiddenField4" runat="server" />
                                    </td>
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
                                    <td><span class="b">6.</span>Name Of HOF<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtHofNm" runat="server" TextMode="SingleLine" CssClass="textCol" placeholder="HOF Name" AutoPostBack="True" OnTextChanged="txtHofNm_TextChanged" ReadOnly="True"></asp:TextBox>
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
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ReqAge" runat="server" ErrorMessage="Required Age." ControlToValidate="txtHusbandNm" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">11.</span>Relation With HOF<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtDtBirth0" runat="server" CssClass="textCol" placeholder="Date OF Birth" ReadOnly="True" ></asp:TextBox>
                                    </td>
                                    <td><span class="b">12.</span>Father/Husband Name<span class="c">*</span></td>
                                    <td>
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
                                        <asp:TextBox ID="txtAAdhar" runat="server" CssClass="textCol" TextMode="Number" placeholder="Aadher No."></asp:TextBox>


                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">15.</span>Mobile No.<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="textCol" MaxLength="10" onkeypress="return check(event);" placeholder="Mobile No." ></asp:TextBox>

                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ReqMobileNo" runat="server" ErrorMessage="Required Mobile No." ControlToValidate="txtMobileNo" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegMobile" ValidationExpression="^[0-9]*$" ForeColor="Red" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Required number only."></asp:RegularExpressionValidator>

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
               
                

                <tr id="trBankDetails" runat="server">
                    <td align="center" >
                        <fieldset id="FIELDSET2" runat="server" style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid">
                            <legend id="LEGEND2" runat="server" style="COLOR: #cc0000"><strong>BANK ACCOUNT DETAILS</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="10" class="text" width="100%">
                                <tr>
                                    <td width="50%" align="right">Account Holder Name List:</td>
                                    <td width="50%" align="left">
                                        <asp:DropDownList ID="ddlacch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlacch_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <div runat="server" id="lnk">
                                        <a href="#" id="chan"  onclick="CnfSubmit()">Change Account Holdername</a>
                                        </div>
                                    </td>
                                </tr>
                                <tr id="ch" style="display:none;">
                                    <td align="right">Account Holder Name&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAccNo0" runat="server" CssClass="textCol" placeholder="Acc. hld Name."></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequHusbandNm0" runat="server" ErrorMessage="Required ACC. Name." ControlToValidate="txtAccNo0" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr class="up">
                                    <td align="right">Account No.<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAccNo" runat="server" CssClass="textCol" placeholder="Acc. No."></asp:TextBox>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegAccNo" ValidationExpression="^([0-9a-zA-Z%]+$)" ForeColor="Red" runat="server" ControlToValidate="txtAccNo" ErrorMessage="Required number & alphabit only."></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr class="up">
                                    <td align="right">Bank Name<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlBankNm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBankNm_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="up">
                                    <td align="right">Branch Name<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlBranchNm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchNm_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="up">
                                    <td align="right">IFSC Code &nbsp;<strong> :</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtIfscCd" runat="server" placeholder="IFSC Code" ReadOnly="true" TextMode="SingleLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr class="up">
                                    <td></td>
                                    <td>
                                        &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="btnText" Width="100px" OnClick="btnSave_Click"  Text="Update" OnClientClick="return Validate();" />&nbsp;<%--OnClientClick="return Validate();"--%>
                                        <asp:Button ID="btnSave0" runat="server" CssClass="btnText" Width="100px" OnClick="btnSave0_Click"  Text="Back" />
                                    </td>
                                </tr>
                                <tr id="chx" style="display:none;">
                                    <td></td>
                                    <td>
                                        &nbsp;<asp:Button ID="Button1" runat="server" CssClass="btnText" Width="100px"   Text="Correct" OnClick="Button1_Click" />&nbsp;<%--OnClientClick="return Validate();"--%>
                                        <asp:Button ID="Button2" runat="server" CssClass="btnText" Width="100px"  Text="Back" OnClick="Button2_Click" />
                                    </td>
                                </tr>

                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
    <script type="text/javascript">

        $("body").on("click", "[id*=grd_FarmerList] .Edit", function () {
            var row = $(this).closest("tr");
            var fcode = row.find("[id*=fcode]").val();
            var accno = row.find("[id*=TextBox2]").val();
            var bank = row.find("[id*=ddl_bank]").val();
            var branch = row.find("[id*=ddl_branch]").val();
            var holder = row.find("[id*=TextBox1]").val();
            var ifsc = row.find("[id*=TextBox3]").val();
            var types = $("[id *= HiddenField1]").val();
            var chby = $("[id *= HiddenField2]").val();
            $.ajax({
                type: "POST",
                url: "NIC_AAO_REPORT.aspx/UpdateCustomer",
                data: '{fcode: "' + fcode + '",accno: "' + accno + '",bank: "' + bank + '",branch: "' + branch + '",holder: "' + holder + '",ifsc:"' + ifsc + '",chby: "' + chby + '",types: "' + types + '"}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    row.find(".Edit").hide();
                },
                error: function (response) {
                    alert(response.error);
                }
            });

            //alert(fcode+"/"+accno+"/"+bank+"/"+branch+"/"+holder);
            row.find(".Edit").hide();

            $(this).hide();
            return false;
        });
</script>
</asp:Content>

