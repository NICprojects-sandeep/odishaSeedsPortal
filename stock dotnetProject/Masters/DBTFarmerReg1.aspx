<%@ Page Title="::FARMER REGISTRATION::" MaintainScrollPositionOnPostback="true" ClientIDMode="Static" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="DBTFarmerReg.aspx.cs" Inherits="DBT_DBTFarmerReg" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
   <link href="../style.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="jscript48/jquery-1.10.2.min.js"></script>
    <script src="jscript48/jquery-ui.js"></script>

    <link href="style.css" rel="stylesheet" />



    <script type="text/javascript" language="javascript">

        debugger;
        /////////////////////
        function checkDate() {
            var dateString = document.getElementById("txtDOB").value;
            var myDate = new Date(dateString);
            var today = new Date();
            if (dateString == "") {
                return true;
            }
            else if (myDate > today) {
                document.getElementById("txtDOB").value = "";
                alert('You cannot select a day earlier than today!')
                document.getElementById("txtDOB").focus();
                return false;
            }
            return true;
        }
        /////////////////////Age shouldnot be less that 1 digits and more than 120 years
        function maxAge() {
            //var txtbox = document.getElementById("txtAge");
            //alert(1);
            var txtbox = $("#txtAge").val();
            if (parseInt(txtbox) < 1) {
                alert('Age must not be less than 1 Year');
                document.getElementById("txtAge").focus();
                return false;
            }
            if (parseInt(txtbox) > 120) {
                alert('Age must not be more than 120 Year');
                document.getElementById("txtAge").focus();
                return false;
            }
        }
        //Age shouldnot be less that 1 digits and more than 120 years in Grid
        function checkAge() {
            var count = 0;
            var numrow = 0;
            var num = 0;
            var rowCount = document.getElementById('grdFamilyDetails').rows.length;
            for (num = 2; num <= rowCount; num++) {
                if (num < 10)
                    numrow = '0' + num;
                else
                    numrow = num;
                var Age = document.getElementById("grdFamilyDetails_ctl" + numrow + "_txtMemAge");
                if (parseInt(Age.value) < 1) {
                    alert('Age must not be less than 1 Year');
                    document.getElementById("grdFamilyDetails_ctl" + numrow + "_txtMemAge").focus();
                    return false;
                }
                if (parseInt(Age.value) > 120) {
                    alert('Age must not be more than 120 Year');
                    document.getElementById("grdFamilyDetails_ctl" + numrow + "_txtMemAge").focus();
                    return false;
                }
            }
        }

        function Validate() {


            var xx = $("#txtAAdhar").val();

            if ($("#txtAAdhar").val() != "") {

                if (!MinimumLengthValidation(xx, '12')) {

                    return false;
                }
                if (!MaximumLengthValidation(xx, '12')) {
                    return false;
                }
                if (!(ValidAadhaarNo($("#txtAAdhar").val()))) {
                    alert('Invalid Aadhar number');
                    return false;
                }

            }
            var Mob = document.getElementById('<%=txtMobileNo.ClientID%>').value;
            if (Mob != "") {

                if (!MinimumLengthValidation(Mob, '10')) {
                    return false;
                }
                if (!MaximumLengthValidation(Mob, '10')) {
                    return false;
                }

                if (!MobileNumber('txtMobileNo')) {
                    return false;
                }
            }



            function MobileNumber(cntr) {
                var Mobile = /^[7-9][0-9]{9}$/
                var entered_no = $('#' + cntr).val();
                if (!Mobile.test(entered_no)) {
                    alert('Enter a valid Mobile Number');
                    $('#' + cntr).focus();
                    return false;
                }
                else
                    return true;
            }
            function MinimumLengthValidation(textbox, length) {


                try {
                    // var textbox = $('#' + textbox);
                    if (textbox.length < length) {
                        alert("Please enter minimum " + length + " characters or numbers");
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
                        alert("Please enter Maximum " + length + " characters or numbers");
                        textbox.focus();
                        return false;
                    }
                    else {
                        return true;
                    }

                } catch (e) {

                }
            }


            function isNumberKey(evt) {
                alert(1);
                var charCode = (evt.which) ? evt.which : event.keyCode
                if (charCode == 8)//back space
                    return true;
                if (charCode < 48 || charCode > 57)//0-9
                {
                    alert("Please Enter Only Numbers.");
                    return false;
                }
                return true;
            }
            function IsWhiteSpace1stPalce(cntr) {
                var strValue = $('#' + cntr).val();
                // alert(strValue);
                if (strValue != "") {
                    var FistChar = strValue.charAt(0);
                    if (FistChar == " ") {
                        alert('White space are not allowed at 1st place !!!');
                        $('#' + cntr).focus();
                        return false;
                    } else { return true; }
                    return true;
                }
                else
                    return true;
            }



            //if (!ConfirmAction('btnSubmit', 'Are you sure want to Submit ?', 'Are you sure want to Update ?')) {
            //    return false;
            //}
        }

    </script>


    <asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">

        <ContentTemplate>
            <table border="0" cellpadding="0" cellspacing="10" width="100%">
                <asp:HiddenField ID="hfSession" runat="server" Visible="false" />

                <tr runat="server" id="trFarmdetails">
                    <td width="100%" >
                        <fieldset style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid" id="fldMain" runat="server">
                            <legend style="COLOR: #cc0000" id="lgdMain1" runat="server"><strong>FARMER DETAILS</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="10" width="100%" class="text">
                                <tr>
                                    <td><span class="b">1.</span>District</td>
                                    <td>
                                        <asp:Label ID="lblDist" runat="server" Text="" Style="font-weight: 700; color: #0033CC; font-style: italic"></asp:Label></td>
                                    <td><span class="b">2.</span>Block<span class="c">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlBlock" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBlock_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr>
                                    <td><span class="b">3.</span>GP<span class="c">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlGp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGp_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                    <td><span class="b">4.</span>Village<span class="c">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlVillage" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">5.</span>PIN Code<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtPinCode" runat="server" MaxLength="6" CssClass="textCol" onkeypress="return check(event);" placeholder="Pin Code"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="validatePinCode" runat="server" ErrorMessage="Pin No. required." ControlToValidate="txtPinCode" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ID="rglrPinCode" ValidationExpression="^[0-9]*$" ForeColor="Red" runat="server" ControlToValidate="txtPinCode" ErrorMessage="Pan no. should be Numeric"></asp:RegularExpressionValidator>--%>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtPinCode"></cc:FilteredTextBoxExtender>
                                    </td>
                                    <td><span class="b">6.</span>Name Of HOF<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtHofNm" runat="server" TextMode="SingleLine" CssClass="textCol" placeholder="HOF Name" AutoPostBack="True" OnTextChanged="txtHofNm_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ReqtxtHofNm" runat="server" ErrorMessage="Enter HOF Name." ControlToValidate="txtHofNm" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">7.</span>Gender<span class="c">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlGender" runat="server">
                                            <asp:ListItem Selected="True" Value="1">Male</asp:ListItem>
                                            <asp:ListItem Value="2">Female</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td><span class="b">8.</span>Category<span class="c">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlCategory" runat="server">
                                            <asp:ListItem Selected="True" Value="0">--select--</asp:ListItem>
                                            <asp:ListItem Value="1">General</asp:ListItem>
                                            <asp:ListItem Value="2">SC</asp:ListItem>
                                            <asp:ListItem Value="3">ST</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">9.</span>Date Of Birth</td>
                                    <td>
                                        <asp:TextBox ID="txtDtBirth" runat="server" CssClass="textCol" placeholder="Date OF Birth" AutoPostBack="True" OnTextChanged="txtDtBirth_TextChanged"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDtBirth" />                                        
                                        <cc:MaskedEditExtender id="MaskedEditExtender1" runat="server" TargetControlID="txtDtBirth" Mask="99/99/9999" MaskType="date"> </cc:MaskedEditExtender>
                                        <%--<cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" TargetControlID="txtDtBirth"> </cc:FilteredTextBoxExtender>--%>

                                    </td>
                                    <td><span class="b">10.</span>Age<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtAge" runat="server" MaxLength="3" placeholder="Age" onBlur="return maxAge();" Width="100px"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ReqAge" runat="server" ErrorMessage="Required Age." ControlToValidate="txtHusbandNm" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">11.</span>Relation With HOF<span class="c">*</span></td>
                                    <td>
                                        <asp:DropDownList ID="ddlRelation" runat="server">
                                            <asp:ListItem Selected="True" Value="0">--select--</asp:ListItem>
                                            <asp:ListItem Value="1">Father</asp:ListItem>
                                            <asp:ListItem Value="2">Husband</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td><span class="b">12.</span>Father/Husband Name<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtHusbandNm" runat="server" CssClass="textCol" TextMode="SingleLine" placeholder="Father/Husband Name"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequHusbandNm" runat="server" ErrorMessage="Required Father/Husband Name." ControlToValidate="txtHusbandNm" ForeColor="Red"></asp:RequiredFieldValidator>

                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">13.</span>Voter Id Card No.<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtVoterId" runat="server" CssClass="textCol" TextMode="SingleLine" placeholder="Voter Id" AutoPostBack="True" OnTextChanged="txtVoterId_TextChanged"
></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="VoterId" runat="server" ErrorMessage="Required Voter Id." ControlToValidate="txtVoterId" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator Display="Dynamic" ID="RegPid" runat="server" ControlToValidate="txtVoterId" ForeColor="Red" ErrorMessage="Allow Alphabets/numbers/special characters(/,-)." ValidationExpression="^([0-9a-zA-Z-/%]+$)"></asp:RegularExpressionValidator>
                                    </td>
                                    <td><span class="b">14.</span>Aadhar No.</td>
                                    <td>
                                        <asp:TextBox ID="txtAAdhar" runat="server" CssClass="textCol" TextMode="Number" placeholder="Aadhar No." OnTextChanged="txtAAdhar_TextChanged" AutoPostBack="true"></asp:TextBox>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtAAdhar"></cc:FilteredTextBoxExtender>
                                       <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server" ErrorMessage="Required Aadhaar No." ControlToValidate="txtAAdhar" ForeColor="Red"></asp:RequiredFieldValidator>--%>

                                    </td>
                                </tr>
                                <tr>
                                    <td><span class="b">15.</span>Mobile No.<span class="c">*</span></td>
                                    <td>
                                        <asp:TextBox ID="txtMobileNo" runat="server" CssClass="textCol" MaxLength="10" onkeypress="return check(event);" placeholder="Mobile No."></asp:TextBox>

                                        <cc:FilteredTextBoxExtender ID="txtPINCode_FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtMobileNo">
                                        </cc:FilteredTextBoxExtender>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="ReqMobileNo" runat="server" ErrorMessage="Required Mobile No." ControlToValidate="txtMobileNo" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <%--<asp:RegularExpressionValidator Display="Dynamic" ID="RegMobile" ValidationExpression="^[0-9]*$" ForeColor="Red" runat="server" ControlToValidate="txtMobileNo" ErrorMessage="Required number only."></asp:RegularExpressionValidator>--%>

                                    </td>
                                    <td><span class="b">16.</span>No. Of Family Members</td>
                                    <td>
                                        <asp:DropDownList ID="ddlNoFamily" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlNoFamily_SelectedIndexChanged">
                                            <asp:ListItem Selected="True" Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                            </table>

                        </fieldset>
                    </td>
                </tr>
               
                <tr id="trFamilyDetails" runat="server">
                    <td align="center">
                        <fieldset id="FIELDSET1" runat="server" style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid">
                            <legend id="LEGEND1" runat="server" style="COLOR: #cc0000"><strong>FAMILY MEMBER DETAILS</strong></legend>
                            <table border="0" cellpadding="0" cellspacing="10" class="text" width="100%">
                                <tr>
                                    <td>
                                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" DataKeyNames="Id" OnRowDataBound="GridView1_RowDataBound" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="SL NO.">
                                                    <ItemTemplate>
                                                        <%#Eval("Id") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NAME OF THE MEMBER(*)">
                                                    <ItemTemplate>
                                                        <%#Eval("txt1") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RELATION WITH HOF(*)">
                                                    <ItemTemplate>
                                                        <%#Eval("dd") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AGE(*)">
                                                    <ItemTemplate>
                                                        <%#Eval("txt2") %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="NOMINATED (PLEASE TICK)">
                                                    <ItemTemplate>
                                                        <%#Eval("chk") %>
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
                                    <td width="50%"></td>
                                    <td width="50%"></td>
                                </tr>
                                <tr>
                                    <td align="right">Account Holder Name&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:Label ID="lblAccountHolderNm" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Account No.<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtAccNo" runat="server" CssClass="textCol" placeholder="Acc. No." AutoPostBack="True" OnTextChanged="txtAccNo_TextChanged"></asp:TextBox>
                                        <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server" ErrorMessage="Required Account No." ControlToValidate="txtAccNo" ForeColor="Red"></asp:RequiredFieldValidator>
                                        <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtAccNo">
                                        </cc:FilteredTextBoxExtender>

                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Bank Name<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlBankNm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBankNm_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">Branch Name<span class="c">*</span>&nbsp; <strong>:</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlBranchNm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchNm_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">IFSC Code &nbsp;<strong> :</strong> &nbsp;</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtIfscCd" runat="server" placeholder="IFSC Code" ReadOnly="true" TextMode="SingleLine"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td>
                                        &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="btnText" Width="100px" OnClick="btnSave_Click" OnClientClick="return Validate();" Text="Submit" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;</td>
                                </tr>

                            </table>
                        </fieldset>
                    </td>
                </tr>



                <tr id="tbFarmdetails" runat="server">
                    <td  >
                        <fieldset id="FIELDSET3" runat="server" style="BORDER-RIGHT: #6f92ca 1px solid; BORDER-TOP: #6f92ca 1px solid; BORDER-LEFT: #6f92ca 1px solid; BORDER-BOTTOM: #6f92ca 1px solid">
                            <legend id="LEGEND3" runat="server" style="COLOR: #cc0000"><strong>ENTERY DETAILS</strong></legend>
                            <table align="center" class="text1" width="100%">
                                <tr>
                                    <td width="25%"></td>
                                    <td width="25%"></td>
                                    <td width="25%"></td>
                                    <td width="25%"></td>
                                </tr>
                                <tr>
                                    <td colspan="4" style="color: #0000CC"><strong style="color: #000099">Before final confirmation please check the details entered by you.</strong></td>
                                </tr>
                                <tr>
                                    <td>Block :</td>
                                    <td id="tdBlock" runat="server"></td>
                                    <td>GP :</td>
                                    <td id="tdGp" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Village :</td>
                                    <td id="tdvillage" runat="server"></td>
                                    <td>Pin Code :</td>
                                    <td id="tdPin" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>HOF Name :</td>
                                    <td id="tdHofNm" runat="server"></td>
                                    <td>Gender :</td>
                                    <td id="tdGender" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Category :</td>
                                    <td id="tdCategory" runat="server"></td>
                                    <td>date Of Birth :</td>
                                    <td id="tdDOB" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Age :</td>
                                    <td id="tdAge" runat="server"></td>
                                    <td>Relation With HOF :</td>
                                    <td id="tdRWHOF" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Father/Husband Name :</td>
                                    <td id="tdFtherNm" runat="server"></td>
                                    <td>Voter Id :</td>
                                    <td id="tdVoterId" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Aadher No :</td>
                                    <td id="tdAadherNo" runat="server"></td>
                                    <td>Mobile No. :</td>
                                    <td id="tdMobileNo" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Account Holder Name :</td>
                                    <td id="tdAccHNm" runat="server"></td>
                                    <td>Account No.</td>
                                    <td id="tdAccNo" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>Bank Name :</td>
                                    <td id="tdBankNm" runat="server"></td>
                                    <td>Branch Name :</td>
                                    <td id="tdBranchNm" runat="server"></td>
                                </tr>
                                <tr>
                                    <td>IFSC Code :</td>
                                    <td id="tdIFSC" runat="server"></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnConform" runat="server" BackColor="#FFFFCC" BorderColor="#990033" OnClick="btnConform_Click" Style="font-weight: 700" Text="Confirm" Width="83px" />
                                        &nbsp;
                                    <asp:Button ID="btnEdit" runat="server" BackColor="#CCFFCC" BorderColor="#CCFF99" OnClick="btnEdit_Click" Style="font-weight: 700" Text="Edit" Width="77px" />
                                    </td>
                                    <td></td>
                                </tr>

                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>


            </table>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="btnConform" />
            <asp:PostBackTrigger ControlID="btnEdit" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

