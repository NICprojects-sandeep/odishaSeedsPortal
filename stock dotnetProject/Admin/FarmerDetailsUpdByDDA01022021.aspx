<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Dist.master" AutoEventWireup="true" CodeFile="FarmerDetailsUpdByDDA.aspx.cs" Inherits="Admin_FarmerDetailsUpdByDDA" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
     <style type="text/css">
        .Border {
            border: dashed black 2px;
            width: 900px;
            padding-bottom: 30px;
            clear: both;
        }

        .leg {
            font-family: Tahoma;
            font-size: 14px;
            background-color: #E6EBED;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 2px;
        }

        .tbl {
            font-family: Tahoma;
            font-size: 14px;
            margin-top: 80px;
            background-color: #E6EBED;
            width: 99%;
            border: 1px solid;
            border-radius: 5px;
            border-collapse: collapse;
            padding: 5px;
            margin-bottom: 30px;
        }

        .Header {
            padding-top: 10px;
            padding-bottom: 10px;
            font-size: 24px;
            text-decoration: underline;
            text-align: center;
        }

        .nb {
            font-size: 14px;
            font-family: Comic Sans MS;
            text-align: left;
            width: 100%;
            padding-left: 20px;
            padding-top: 20px;
        }

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

        .tabletxt {
            font-family: Tahoma, Geneva, sans-serif;
            color: #000;
            font-size: 12px;
            padding: 5px;
        }

        .normaltd {
            padding: 6px;
        }

        .collapsible {
            background-color: cadetblue;
            color: white;
            cursor: pointer;
            padding: 14px;
            width: 100%;
            border: none;
            text-align: left;
            outline: none;
            font-size: 13px;
            height: 44px;
        }

            .active, .collapsible:hover {
                background-color: #555;
            }

        .content {
            padding: 0 18px;
            display: none;
            overflow: hidden;
            background-color: #f1f1f1;
        }

        .auto-style1 {
            height: 18px;
        }
         .auto-style1 {
             width: 25%;
         }
    </style>

    <script type="text/javascript">

        function validate(key) {

            var keycode = (key.which) ? key.which : key.keyCode;
            var phn = document.getElementById('txtmobno');

            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
        }
    </script>

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
       <div>
        <div align="center">
            <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
        </div>
        <div align="center">
            <br />
            <b>Farmer Id : </b>
            <asp:DropDownList ID="ddlFarmerId" runat="server" OnSelectedIndexChanged="ddlFarmerId_SelectedIndexChanged" AutoPostBack="True" Width="150px">
            </asp:DropDownList>
            <br />

        </div>
        <div runat="server" id="divfarmDetails">
            <fieldset class="tbl">
                <legend style="margin-left: 10px;" class="leg">Farmer Details
                </legend>
                <div style="margin-left: 80px">
                    <table style="width: 80%; padding: 5px; flex-align: center">
                        <tr>
                            <td style="width: 16%;" class="normaltd">Farmer Name</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdFarmerName"></td>
                            <td style="width: 16%;" class="normaltd">Father Name</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdFatherName"></td>
                        </tr>
                        <tr>
                            <td style="width: 16%;" class="normaltd">District</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdDistrict"></td>
                            <td style="width: 16%;" class="normaltd">Block</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdBlock"></td>
                        </tr>
                        <tr>
                            <td style="width: 16%;" class="normaltd">GP</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdGp"></td>
                            <td style="width: 16%;" class="normaltd">Village</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdVillage"></td>
                        </tr>
                        <tr>
                            <td style="width: 16%;" class="normaltd">Category</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdCategory"></td>
                            <td style="width: 16%;" class="normaltd">Gender</td>
                            <td style="width: 2%;">:</td>
                            <td style="width: 20%;" runat="server" id="tdGender"></td>
                        </tr>

                    </table>
                </div>
            </fieldset>
        </div>
        <div runat="server" id="divDetails">
            <button type="button" class="collapsible" runat="server" id="divCNm">Edit For Name,Father/Husband Name</button>
            <div class="content" runat="server" id="divDNm">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                <table style="width: 80%; padding: 5px; flex-align: center">
                    <tr>
                        <td width="25%" class="auto-style1"></td>
                        <td class="auto-style1"></td>
                        <td width="25%" class="auto-style1"></td>
                        <td width="25%" class="auto-style1"></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <p>
                                Existing Name & Father Name<br />
                                -------------------------------------------
                            </p>
                        </td>
                        <td colspan="2" align="center">
                            <p>
                                Requested To Change Name & Father Name<br />
                                ----------------------------------------------
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Farmer Name :</td>
                        <td align="left" runat="server" id="tdEFarmName" class="auto-style1"></td>
                        <td></td>
                        <td align="left" runat="server" id="tdUpdFarmName">
                           </td>

                    </tr>
                    <tr>
                        <td align="left">Father/Husband Name : </td>
                        <td align="left" runat="server" id="tdEFatherName" class="auto-style1"></td>
                        <td></td>
                        <td align="left" runat="server" id="tdUpdFatherNm">
                            </td>
                    </tr>
                     <tr>
                        <td align="left"></td>
                        <td align="left" runat="server" id="td1" class="auto-style1"></td>
                        <td align="right">Verify Documents :</td>
                        <td align="left"><asp:LinkButton id="linknm" runat="server" OnClick="linknm_Click">View</asp:LinkButton>
                            </td>
                    </tr>
                    
                    <tr>
                        <td align="left"> </td>
                        <td align="left" class="auto-style1" ></td>
                        <td align="right">Are You Sure To Update ? :</td>
                        <td align="left" >
                            <asp:RadioButtonList ID="radName" runat="server" RepeatDirection="Horizontal" Width="118px" AutoPostBack="True" OnSelectedIndexChanged="radName_SelectedIndexChanged">
                                <asp:ListItem Value="a0">Yes</asp:ListItem>
                                <asp:ListItem Value="a1">No</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                    </tr>
                     <tr runat="server" id="trRejNm">
                        <td align="left"></td>
                        <td align="left" runat="server" id="td7" class="auto-style1"></td>
                        <td align="right">Reason For Rejection:</td>
                        <td align="left">
                            <asp:TextBox ID="txtRejName" runat="server" TextMode="MultiLine" Width="166px"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td class="auto-style1"></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnNameSave" runat="server" Text="Update"  BackColor="#999966" BorderColor="Maroon" OnClick="btnNameSave_Click" />
                            &nbsp;<asp:Button ID="btnNameSave0" runat="server" Text="Reject"  BackColor="#666633" BorderColor="#3333CC" OnClick="btnNameSave0_Click"  /></td>
                     <%--   OnClick="btnNameSave_Click"--%>
                    </tr>
                </table>
                                    </ContentTemplate>

                        <Triggers>
                            <ajax:PostBackTrigger ControlID="btnNameSave0" />
                            <ajax:PostBackTrigger ControlID="btnNameSave" />
                            <ajax:PostBackTrigger ControlID="linknm" />
                        </Triggers>
                        </asp:UpdatePanel>
                       
            </div>
            <button type="button" class="collapsible" runat="server" id="divGender">Change Of Gender</button>
            <div class="content" runat="server" id="divGender1">
               <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>

                        <table style="width: 80%; padding: 5px; flex-align: center">
                            <tr>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <p>
                                        Existing Gender<br />
                                        -------------------------------------------
                                    </p>
                                </td>
                                <td colspan="2" align="center">
                                    <p>
                                        Change of Gender<br />
                                        -----------------------------------------
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Gender :</td>
                                <td align="left" runat="server" id="tdEGender"></td>
                                <td></td>
                                <td align="left" runat ="server" id="tdCGender">
                                    </td>

                            </tr>
                            <tr>
                                <td align="left"></td>
                                <td align="left" runat="server" id="td3"></td>
                                <td align="right">Verify Document :</td>
                                <td align="left">
                                    <asp:LinkButton id="LinkGnd" runat="server" OnClick="LinkGnd_Click">View</asp:LinkButton>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                        <td align="left"> </td>
                        <td align="left" class="auto-style1" ></td>
                        <td align="right">Are You Sure To Update ? :</td>
                        <td align="left" >
                            <asp:RadioButtonList ID="radGender" runat="server" RepeatDirection="Horizontal" Width="118px" AutoPostBack="True" OnSelectedIndexChanged="radGender_SelectedIndexChanged">
                                <asp:ListItem Value="a0">Yes</asp:ListItem>
                                <asp:ListItem Value="a1">No</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                    </tr>
                     <tr runat="server" id="trRejGender">
                        <td align="left"></td>
                        <td align="left" runat="server" id="td8" class="auto-style1"></td>
                        <td align="right">Reason For Rejection:</td>
                        <td align="left">
                            <asp:TextBox ID="txtGenderRej" runat="server" TextMode="MultiLine" Width="166px"></asp:TextBox>
                            </td>
                    </tr>

                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnGender" runat="server" Text="Update"  BackColor="#999966" BorderColor="Maroon" OnClick="btnGender_Click" />&nbsp;
                                    <asp:Button ID="btnGender0" runat="server" BackColor="#999966" BorderColor="Maroon"  Text="Reject" OnClick="btnGender0_Click" />
                                </td>
                                <%--OnClick="btnGender_Click"--%>
                            </tr>
                        </table>
                       
                        </ContentTemplate>
                   <Triggers>
                       <ajax:PostBackTrigger ControlID="btnGender" />
                       <ajax:PostBackTrigger ControlID="btnGender0" />
                       <ajax:PostBackTrigger ControlID="LinkGnd" />
                   </Triggers>
                   </asp:UpdatePanel>
            </div>
            <button type="button" class="collapsible" runat="server" id="divVill">Change For GP & Village</button>
            <div class="content" runat="server" id="divVill1">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" >
                    <ContentTemplate>
                        <table style="width: 80%; padding: 5px; flex-align: center">
                            <tr>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <p>
                                        Existing GP & Village<br />
                                        -------------------------------------------
                                    </p>
                                </td>
                                <td colspan="2" align="center">
                                    <p>
                                        Change of GP & Village<br />
                                        -----------------------------------------
                                    </p>
                                </td>
                            </tr>

                            <tr>
                                <td align="left">GP Name :</td>
                                <td align="left" runat="server" id="tdEGP"></td>
                                <td></td>
                                <td align="left" runat="server" id="tdUpdGp">
                                    </td>

                            </tr>
                            <tr>
                                <td align="left">Village Name : </td>
                                <td align="left" runat="server" id="tdEVillage"></td>
                                <td></td>
                                <td align="left" runat="server" id="tdUpdVillage">
                                    </td>

                            </tr>

                           <tr>
                                <td align="left"></td>
                                <td align="left" runat="server" id="td2"></td>
                                <td align="right">Verify Document :</td>
                                <td align="left">
                                    <asp:LinkButton id="lnkGP" runat="server" OnClick="lnkGP_Click">View</asp:LinkButton>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                        <td align="left"> </td>
                        <td align="left" class="auto-style1" ></td>
                        <td align="right">Are You Sure To Update ? ? :</td>
                        <td align="left" >
                            <asp:RadioButtonList ID="radGP" runat="server" RepeatDirection="Horizontal" Width="118px" AutoPostBack="True" OnSelectedIndexChanged="radGP_SelectedIndexChanged">
                                <asp:ListItem Value="a0">Yes</asp:ListItem>
                                <asp:ListItem Value="a1">No</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                    </tr>
                     <tr runat="server" id="trRejGp">
                        <td align="left"></td>
                        <td align="left" runat="server" id="td9" class="auto-style1"></td>
                        <td align="right">Reason For Rejection:</td>
                        <td align="left">
                            <asp:TextBox ID="txtRejGP" runat="server" TextMode="MultiLine" Width="166px"></asp:TextBox>
                            </td>
                    </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnGp" runat="server" Text="Update"  BackColor="#999966" BorderColor="Maroon" OnClick="btnGp_Click" />&nbsp;
                                    <asp:Button ID="btnGp0" runat="server" BackColor="#999966" BorderColor="Maroon"  Text="Reject" OnClick="btnGp0_Click" />
                                </td>
                               <%-- OnClick="btnGp_Click"--%>
                            </tr>
                        </table>

                        </ContentTemplate>
                   <Triggers>
                       <ajax:PostBackTrigger ControlID="btnGp" />
                       <ajax:PostBackTrigger ControlID="btnGp0" />
                       <ajax:PostBackTrigger ControlID="lnkGP" />
                   </Triggers>
                   </asp:UpdatePanel>
                    

            </div>
            <button type="button" class="collapsible" runat="server" id="divMobile">Edit Mobile</button>
            <div class="content" runat="server" id="divMobile1">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
                    <ContentTemplate>
                <table style="width: 80%; padding: 5px; flex-align: center">
                    <tr>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <p>
                                Existing Mobile No<br />
                                -------------------------------------------
                            </p>
                        </td>
                        <td colspan="2" align="center">
                            <p>
                                Change Of Mobile No<br />
                                -----------------------------------------
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Mobile No :</td>
                        <td align="left" runat="server" id="tdEMobileNo"></td>
                        <td align="left" colspan="2" runat="server" id="tdUpdMobile">
                            
                        </td>
                    </tr>
                    <tr>
                        <td align="left"> </td>
                        <td align="left" class="auto-style1" ></td>
                        <td align="right">Are You Sure To Update ? :</td>
                        <td align="left" >
                            <asp:RadioButtonList ID="radMobile" runat="server" RepeatDirection="Horizontal" Width="118px" AutoPostBack="True" OnSelectedIndexChanged="radMobile_SelectedIndexChanged">
                                <asp:ListItem Value="a0">Yes</asp:ListItem>
                                <asp:ListItem Value="a1">No</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                    </tr>
                     <tr runat="server" id="trRejMobile">
                        <td align="left"></td>
                        <td align="left" runat="server" id="td10" class="auto-style1"></td>
                        <td align="right">Reason For Rejection:</td>
                        <td align="left">
                            <asp:TextBox ID="txtRejMobile" runat="server" TextMode="MultiLine" Width="166px"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnMobileNo" runat="server" Text="Update"  BackColor="#999966" BorderColor="Maroon" OnClick="btnMobileNo_Click" />&nbsp;
                            <asp:Button ID="btnMobileNo0" runat="server" Text="Reject"  BackColor="#999966" BorderColor="Maroon" OnClick="btnMobileNo0_Click"  /></td>
                        <%--OnClick="btnMobileNo_Click"--%>
                    </tr>
                </table>
                        
</ContentTemplate>
                   <Triggers>
                       <ajax:PostBackTrigger ControlID="btnMobileNo" />
                       <ajax:PostBackTrigger ControlID="btnMobileNo0" />
                       
                   </Triggers>
                   </asp:UpdatePanel>
            </div>
            <button type="button" class="collapsible" runat="server" id="divAadhar">Addition/Update Aadhaar No</button>
            <div class="content" runat="server" id="divAadha1">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                    <ContentTemplate>
                <table style="width: 80%; padding: 5px; flex-align: center">
                    <tr>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <p>
                                Existing Aadhaar No<br />
                                -------------------------------------------
                            </p>
                        </td>
                        <td colspan="2" align="center">
                            <p>
                                Addition/Update Of AAdhaar No<br />
                                -----------------------------------------
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Aadhaar No :</td>
                        <td align="left" runat="server" id="tdEAadhaar"></td>
                        <td></td>
                        <td align="left" runat="server" id="tdUpdAadhaar">
                            
                        </td>
                        <%--<asp:TextBox ID="txtAadhaar" runat="server" BackColor="#FFFF99"></asp:TextBox>--%>
                    </tr>
                    <tr>
                                <td align="left"></td>
                                <td align="left" runat="server" id="td4"></td>
                                <td align="right">Verify Document :</td>
                                <td align="left">
                                    <asp:LinkButton id="lnkAadhar" runat="server" OnClick="lnkAadhar_Click">View</asp:LinkButton>
                                    &nbsp;</td>
                            </tr>
                    <tr>
                        <td align="left"> </td>
                        <td align="left" class="auto-style1" ></td>
                        <td align="right">Are You Sure To Update ? :</td>
                        <td align="left" >
                            <asp:RadioButtonList ID="radAadhaar" runat="server" RepeatDirection="Horizontal" Width="118px" AutoPostBack="True" OnSelectedIndexChanged="radAadhaar_SelectedIndexChanged">
                                <asp:ListItem Value="a0">Yes</asp:ListItem>
                                <asp:ListItem Value="a1">No</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                    </tr>
                     <tr runat="server" id="trRejAadhaar">
                        <td align="left"></td>
                        <td align="left" runat="server" id="td11" class="auto-style1"></td>
                        <td align="right">Reason For Rejection:</td>
                        <td align="left">
                            <asp:TextBox ID="txtRejAadhaar" runat="server" TextMode="MultiLine" Width="166px"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnAadhaar" runat="server" Text="Update"  BackColor="#999966" BorderColor="Maroon" OnClick="btnAadhaar_Click" />&nbsp;
                            <asp:Button ID="btnAadhaar0" runat="server" Text="Reject"  BackColor="#999966" BorderColor="Maroon" OnClick="btnAadhaar0_Click"  /></td>
                        <%--OnClick="btnAadhaar_Click"--%>
                    </tr>
                </table>
                </ContentTemplate>
                   <Triggers>
                       <ajax:PostBackTrigger ControlID="btnAadhaar" />
                       <ajax:PostBackTrigger ControlID="btnAadhaar0" />
                       <ajax:PostBackTrigger ControlID="lnkAadhar" />
                   </Triggers>
                   </asp:UpdatePanel>
            </div>
            <button type="button" class="collapsible" runat="server" id="divVatagory">Change Of Category</button>
            <div class="content" runat="server" id="divVatagory1">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" >
                    <ContentTemplate>
                <table style="width: 80%; padding: 5px; flex-align: center">
                    <tr>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <p>
                                Existing Category<br />
                                -------------------------------------------
                            </p>
                        </td>
                        <td colspan="2" align="center">
                            <p>
                                Change of Category<br />
                                -----------------------------------------
                            </p>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">Category :</td>
                        <td align="left" runat="server" id="tdECategory"></td>
                        <td align="center"></td>
                        <td runat="server" id="tdUpdCategory" align="left">
                            
                        </td>

                    </tr>
                   <tr>
                                <td align="left"></td>
                                <td align="left" runat="server" id="td5"></td>
                                <td align="right">Verify Document :</td>
                                <td align="center">
                                    <asp:LinkButton id="lnkCast" runat="server" OnClick="lnkCast_Click">View</asp:LinkButton>
                                    &nbsp;</td>
                            </tr>
                    <tr>
                        <td align="left"> </td>
                        <td align="left" class="auto-style1" ></td>
                        <td align="right">Are You Sure To Update ? :</td>
                        <td align="left" >
                            <asp:RadioButtonList ID="radCategory" runat="server" RepeatDirection="Horizontal" Width="118px" AutoPostBack="True" OnSelectedIndexChanged="radCategory_SelectedIndexChanged">
                                <asp:ListItem Value="a0">Yes</asp:ListItem>
                                <asp:ListItem Value="a1">No</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                    </tr>
                     <tr runat="server" id="trRejCategory">
                        <td align="left"></td>
                        <td align="left" runat="server" id="td12" class="auto-style1"></td>
                        <td align="right">Reason For Rejection:</td>
                        <td align="left">
                            <asp:TextBox ID="txtRejCategory" runat="server" TextMode="MultiLine" Width="166px"></asp:TextBox>
                            </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:Button ID="btnCategory" runat="server" Text="Update"  BackColor="#999966" BorderColor="Maroon" OnClick="btnCategory_Click" />&nbsp;
                            <asp:Button ID="btnCategory0" runat="server" Text="Reject"  BackColor="#999966" BorderColor="Maroon" OnClick="btnCategory0_Click"  /></td>
                       <%-- OnClick="btnCategory_Click"--%>
                    </tr>
                </table>
                        </ContentTemplate>
                   <Triggers>
                       <ajax:PostBackTrigger ControlID="btnCategory" />
                       <ajax:PostBackTrigger ControlID="btnCategory0" />
                       <ajax:PostBackTrigger ControlID="lnkCast" />
                   </Triggers>
                   </asp:UpdatePanel>
               
            </div>
            <button type="button" class="collapsible" runat="server" id="divBank">Correction /Change of bank details</button>
            <div class="content" runat="server" id="divBank1">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" >
                    <ContentTemplate>
                        <table style="width: 80%; padding: 5px; flex-align: center">
                            <tr>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center">
                                    <p>
                                        Existing Account Details<br />
                                        -------------------------------------------
                                    </p>
                                </td>
                                <td colspan="2" align="center">
                                    <p>
                                        Change Of Account Details<br />
                                        -----------------------------------------
                                    </p>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">Account Holder Name :</td>
                                <td align="left" runat="server" id="tdEAccHolderName"></td>
                                <td></td>
                                <td align="left" runat="server" id="tdUpAccHolderNm">
                                    </td>

                            </tr>
                            <tr>
                                <td align="left">Account No : </td>
                                <td align="left" runat="server" id="tdEAccNo"></td>
                                <td></td>
                                <td align="left" runat="server" id="tdUpdAccNo">
                                    </td>
                            </tr>
                            <tr>
                                <td align="left">Bank Name : </td>
                                <td align="left" runat="server" id="tdEBrankName"></td>
                                <td></td>
                                <td align="left" runat="server" id="tdUpdBankName"></td>
                                    
                            </tr>
                            <tr>
                                <td align="left">Branch Name : </td>
                                <td align="left" runat="server" id="tdEBranchName"></td>
                                <td></td>
                                <td align="left" runat="server" id="tdUpdBranchNm"></td>
                                    
                            </tr>

                            <tr>
                                <td align="left">IFSC : </td>
                                <td align="left" runat="server" id="tdEIfsc"></td>
                                <td></td>
                                <td align="left" runat="server" id="tdUpdIfsc"></td>
                                    
                            </tr>
                            
                            <tr>
                                <td align="left"></td>
                                <td align="left" runat="server" id="td6"></td>
                                <td align="right">Verify Document :</td>
                                <td align="left">
                                    <asp:LinkButton id="lnkBank" runat="server" OnClick="lnkBank_Click">View</asp:LinkButton>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                        <td align="left"> </td>
                        <td align="left" class="auto-style1" ></td>
                        <td align="right">Are You Sure To Update ? :</td>
                        <td align="left" >
                            <asp:RadioButtonList ID="radBank" runat="server" RepeatDirection="Horizontal" Width="118px" AutoPostBack="True" OnSelectedIndexChanged="radBank_SelectedIndexChanged">
                                <asp:ListItem Value="a0">Yes</asp:ListItem>
                                <asp:ListItem Value="a1">No</asp:ListItem>
                            </asp:RadioButtonList>
                            </td>
                    </tr>
                     <tr runat="server" id="trRejBank">
                        <td align="left"></td>
                        <td align="left" runat="server" id="td13" class="auto-style1"></td>
                        <td align="right">Reason For Rejection:</td>
                        <td align="left">
                            <asp:TextBox ID="txtRejBank" runat="server" TextMode="MultiLine" Width="166px"></asp:TextBox>
                            </td>
                    </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td></td>
                                <td>
                                    <asp:Button ID="btnBankDetails" runat="server" Text="Update"  BackColor="#999966" BorderColor="Maroon" OnClick="btnBankDetails_Click" />&nbsp;
                                    <asp:Button ID="btnBankDetails0" runat="server" BackColor="#999966" BorderColor="Maroon"  Text="Reject" OnClick="btnBankDetails0_Click" />
                                </td>
                                <%--OnClick="btnBankDetails_Click"--%>
                            </tr>
                        </table>
                        </ContentTemplate>
                   <Triggers>
                       <ajax:PostBackTrigger ControlID="btnBankDetails" />
                       <ajax:PostBackTrigger ControlID="btnBankDetails0" />
                       <ajax:PostBackTrigger ControlID="lnkBank" />
                   </Triggers>
                   </asp:UpdatePanel>
                    
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var coll = document.getElementsByClassName("collapsible");
        var i;

        for (i = 0; i < coll.length; i++) {
            coll[i].addEventListener("click", function () {
                this.classList.toggle("active");
                var content = this.nextElementSibling;
                if (content.style.display === "block") {
                    content.style.display = "none";
                } else {
                    content.style.display = "block";
                }
            });
        }
    </script>
</asp:Content>

