<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO.master" AutoEventWireup="true" CodeFile="FarmerDetailsUpdate.aspx.cs" Inherits="Masters_FarmerDetailsUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

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
            background-color: white;
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
            color: #339966;
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
    <br />

    <div>
        <div align="center">
            <%--<h2><u>AAO FOR AADHAAR NO UPDATION</u></h2>--%>
        </div>
        <div align="center">
            <br />
            <b>Farmer Id : </b>
            <asp:TextBox ID="txtfarmerid" CssClass="txtbox" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            <asp:Button ID="btnsearch" CssClass="btn" runat="server" Text="Search" OnClientClick="return Valid1();" OnClick="btnsearch_Click" />
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
            <button type="button" class="collapsible">Edit For Name,Father/Husband Name</button>
            <div style="background-color:white;" class="content">
                <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                <table style="width: 100%; padding:7px;border:1px solid black " >
                   
                    <tr>
                        <td colspan="2" align="center" style="width:50%">
                           
                                Existing Name & Father Name
                        </td>
                        <td colspan="2" align="center" style="width:50%">
                           
                                Change of Name & Father Name
                        </td>
                        
                    </tr>
                   
                    <tr>
                        <td style="width:25%">Farmer Name :</td>
                        <td  runat="server" id="tdEFarmName" align="left" style="width:25%"></td>
                        
                        <td style="width:25%"  align="right">
                            <asp:TextBox ID="txtEFarmerName" runat="server" BackColor="White" placeholder="Enter Farmer Name"></asp:TextBox></td>
                        <td><span class="auto-style1">Account Holder Name-</span><asp:Label ID="lblAccHolderName" runat="server" Text="" style="color: #990033; font-weight: 700"></asp:Label></td>

                    </tr>
                    
                    <tr>
                        <td style="width:25%">Father/Husband Name : </td>
                        <td  runat="server" id="tdEFatherName" align="left" style="width:25%"></td>
                        
                        <td style="width:25%"  align="right">
                            <asp:TextBox ID="txtEFatherName" runat="server" BackColor="White" placeholder="Enter Father Name"></asp:TextBox></td>
                        <td></td>
                    </tr>

                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">

                                <ContentTemplate>
                    <tr >
                        <td style="width:50%" colspan="2"></td>
                        
                        
                        <td  align="right" >
                            <asp:FileUpload ID="updName" runat="server"></asp:FileUpload></td>
                        <td></td>
                    </tr>
                    </ContentTemplate>
                            </asp:UpdatePanel>
                    

                    <tr>
                       <td style="width:25%"></td>
                        <td style="width:25%"></td>
                       
                        <td style="width:50%" align="center" colspan="2">(Upload Scan Copy Of Aadhaar/Voter Id :<span style="color:red">*(.pdf&nbsp;file within 150kb)</span>) &nbsp;
                            <asp:Button ID="btnNameSave" runat="server" Text="Update" OnClick="btnNameSave_Click" BackColor="#999966" BorderColor="Maroon" Style="margin-left: 72px;" /></td>
                    </tr>

                   
                </table>
                        </ContentTemplate>
                    <Triggers>


                        <ajax:PostBackTrigger ControlID="btnNameSave" />

                    </Triggers>
                </asp:UpdatePanel>
                
            </div>
            
            <button type="button" class="collapsible">Change Of Gender</button>
            <div style="background-color:white;" class="content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <table style="width: 100%; padding: 7px; border:1px solid black">
                            <tr>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                            </tr>
                           
                            <tr>
                                <td colspan="2" align="center" style="width:50%">
                                        Existing Gender
                                </td>
                                <td colspan="2" align="center" style="width:50%">
                                        Change of Gender
                                </td>
                            </tr>
                            <tr>
                                <td style="width:25%">Gender :</td>
                                <td align="left" runat="server" id="tdEGender" style="width:25%"></td>
                               
                                <td align="center" colspan="2" style="width:50%">
                                    <asp:DropDownList ID="ddlGender" runat="server">
                                        <asp:ListItem Selected="True" Value="1">Male</asp:ListItem>
                                        <asp:ListItem Value="2">Female</asp:ListItem>
                                    </asp:DropDownList></td>

                            </tr>
                            <tr>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">

                                    <ContentTemplate>
                             
                                <td style="width:50%" colspan="2"></td>
                                <td style="width:50%" colspan="2" align="center">
                                    <asp:FileUpload ID="updGender" runat="server"></asp:FileUpload>
                                    <br />(Upload Scan Copy Of Aadhaar/Voter Id :<span style="color:red">*(.pdf&nbsp;file within 150kb)</span>)
                                </td>
                                </ContentTemplate>

                                </asp:UpdatePanel>
                            </tr>
                            <tr><td colspan="4"><hr /></td></tr>
                            <tr runat="server" id="trGndFmn">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed Farm Machinary</td>
                                <td align="left"> <img src="../images/Right.jpg"  width="20px" height="15px"/></td>
                            </tr>
                            <tr runat="server" id="trGndApicol">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed APICOL</td>
                                <td align="left">
                                    <img src="../images/Right.jpg" width="20px" height="15px"/></td>
                            </tr>
                            <tr>
                                <td style="width:25%"></td>
                                <td style="width:25%"></td>
                               
                                <td style="width:50%" align="center" colspan="2">&nbsp;
                                    <asp:Button ID="btnGender" runat="server" Text="Update" OnClick="btnGender_Click" BackColor="#999966" BorderColor="Maroon" Style="margin-left: 72px;" /></td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>


                        <ajax:PostBackTrigger ControlID="btnGender" />

                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <button type="button" class="collapsible">Change For GP & Village</button>
            <div style="background-color:white;" class="content">
                <asp:UpdatePanel ID="updMain" runat="server">

                    <ContentTemplate>
                        <table style="width: 100%; padding: 7px; border:1px solid black">
                            <tr>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                            </tr>
                            <tr>
                                <td colspan="2" align="center" style="width:50%">
                                        Existing GP & Village
                                </td>
                                <td colspan="2" align="center" style="width:50%">
                                        Change of GP & Village
                                </td>
                            </tr>

                            <tr>
                                <td style="width:25%">GP Name :</td>
                                <td align="left" runat="server" id="tdEGP" style="width:25%"></td>
                                
                                <td align="center" colspan="2" style="width:50%">
                                    <asp:DropDownList ID="ddlGp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlGp_SelectedIndexChanged">
                                    </asp:DropDownList></td>

                            </tr>
                            <tr>
                                <td style="width:25%">Village Name : </td>
                                <td align="left" runat="server" id="tdEVillage" style="width:25%"></td>
                               
                                <td align="center" colspan="2" style="width:50%">
                                    <asp:DropDownList ID="ddlVillage" runat="server">
                                    </asp:DropDownList></td>

                            </tr>

                            <tr>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">

                                    <ContentTemplate>
                                        <td style="width:50%" colspan="2"></td>
                                      
                                        <td align="center" style="width:50%" colspan="2">
                                            <asp:FileUpload ID="UpdVillage" runat="server"></asp:FileUpload></td>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </tr>
                            <tr>
                                <td style="width:25%"></td>
                                <td style="width:25%"></td>
                                
                                <td style="width:50%" align="center" colspan="2">(Upload Scan Copy Of Aadhaar/Voter Id :<span style="color:red">*(.pdf&nbsp;file within 150kb)</span>)
                                    <asp:Button ID="btnGp" runat="server" Text="Update" OnClick="btnGp_Click" BackColor="#999966" BorderColor="Maroon" Style="margin-left: 72px;" /></td>
                            </tr>
                        </table>


                    </ContentTemplate>

                    <Triggers>


                        <ajax:PostBackTrigger ControlID="btnGp" />

                    </Triggers>
                </asp:UpdatePanel>

            </div>
            <button type="button" class="collapsible">Edit Mobile</button>
            <div style="background-color:white;" class="content">
                
                <table style="width: 100%; padding: 7px; border:1px solid black">
                    <tr>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                    </tr>
                    <tr>
                        <td  colspan="2" align="center" style="width:50%">
                                Existing Mobile No
                        </td>
                        <td  colspan="2" align="center" style="width:50%">
                                Change Of Mobile No
                        </td>
                    </tr>
                    <tr>
                        <td style="width:25%">Mobile No :</td>
                        <td align="left" runat="server" id="tdEMobileNo" style="width:25%"></td>
                        <td align="center" colspan="2" style="width:50%">
                            <asp:TextBox ID="txtMobileNo" runat="server" onkeypress="return check(event);" BackColor="White" placeholder="Enter Mobile Number" MaxLength="10"></asp:TextBox>
                            <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="txtMobileNo"></cc:FilteredTextBoxExtender>
                        </td>
                    </tr>
                    <tr>
                        <td style="width:25%"></td>
                        <td style="width:25%"></td>
                        
                        <td style="width:50%" colspan="2">
                            <asp:Button ID="btnMobileNo" runat="server" Text="Update" OnClick="btnMobileNo_Click" BackColor="#999966" BorderColor="Maroon" Style="margin-left: 373px;"/></td>
                    </tr>
                </table>

            </div>
            <button type="button" class="collapsible">Addition/Update Aadhaar No</button>
            <div style="background-color:white;" class="content">
                <%--<asp:UpdatePanel ID="updMain" runat="server" UpdateMode="Conditional">

        <ContentTemplate>--%>
                <table style="width: 100%; padding: 7px; border:1px solid black">
                    <tr>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="width:50%">
                                Existing Aadhaar No
                        </td>
                        <td colspan="2" align="center" style="width:50%">
                                Addition/Update Of AAdhaar No
                        </td>
                    </tr>
                    <tr>
                        <td style="width:25%">Aadhaar No :</td>
                        <td align="left" runat="server" id="tdAadhaarNo0" style="width:25%"></td>
                        
                        <td align="center" style="width:50%" colspan="2">
                            <asp:TextBox ID="txtAadhaar" runat="server" BackColor="White" placeholder="Enter Adhar Number" MaxLength="12"></asp:TextBox>
                            <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="txtAadhaar"></cc:FilteredTextBoxExtender>
                        </td>

                    </tr>
                    <tr>
                        <td style="width:50%" colspan="2"></td>
                       
                        <td align="center" style="width:50%" colspan="2">
                            <asp:FileUpload ID="UpdaAadhar" runat="server"></asp:FileUpload></td>
                    </tr>
                    <tr>
                        <td style="width:25%"></td>
                        <td style="width:25%"></td>
                      
                        <td style="width:50%" colspan="2" align="center">(Upload Scan Copy Of Aadhaar:<span style="color:red">*(.pdf&nbsp;file within 150kb)</span>)
                            <asp:Button ID="btnAadhaar" runat="server" Text="Update" OnClick="btnAadhaar_Click" BackColor="#999966" BorderColor="Maroon" Style="margin-left: 72px;" /></td>
                    </tr>
                </table>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <button type="button" class="collapsible">Change Of Category</button>
            <div style="background-color:white;" class="content">
                <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">

        <ContentTemplate>--%>
                <table style="width: 100%;padding: 7px;border:1px solid black">
                    <tr>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                        <td width="25%"></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center" style="width:50%">
                                Existing Category
                        </td>
                        <td colspan="2" align="center" style="width:50%">
                                Change of Category
                        </td>
                    </tr>
                    <tr>
                        <td style="width:25%">Category :</td>
                        <td align="left" runat="server" id="tdECategory" style="width:25%"></td>
                       
                        <td style="width:50%" colspan="2"  align="center">
                            <asp:DropDownList ID="ddlCategory" runat="server">
                                <asp:ListItem Selected="True" Value="0">--select--</asp:ListItem>
                                <asp:ListItem Value="1">General</asp:ListItem>
                                <asp:ListItem Value="2">SC</asp:ListItem>
                                <asp:ListItem Value="3">ST</asp:ListItem>
                            </asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td style="width:50%" colspan="2"></td>
                      
                    
                        <td style="width:50%" colspan="2" align="center">
                            <asp:FileUpload ID="file_EPF" runat="server"></asp:FileUpload>
                            <br />(Upload Scan Cast Certificate :<span style="color:red">*(.pdf&nbsp;file within 150kb)</span>)
                        </td>

                    </tr>
                    <tr><td colspan="4"><hr /></td></tr>

                    <tr runat="server" id="trCatFmn">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed Farm Machinary</td>
                                <td align="left"> <img src="../images/Right.jpg"  width="20px" height="15px"/></td>
                            </tr>
                            <tr runat="server" id="trCatApicol">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed APICOL</td>
                                <td align="left">
                                    <img src="../images/Right.jpg" width="20px" height="15px"/></td>
                            </tr>
                    <tr runat="server" id="trCatJAL">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed JALANIDHI</td>
                                <td align="left">
                                    <img src="../images/Right.jpg" width="20px" height="15px"/></td>
                            </tr>
                    <tr runat="server" id="trCatSolar">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed Solar</td>
                                <td align="left">
                                    <img src="../images/Right.jpg" width="20px" height="15px"/></td>
                            </tr>
                    <tr>
                        <td style="width:25%"></td>
                        <td style="width:25%"></td>
                        <td  style="width:50%" colspan="2" align="center">
                            <asp:Button ID="btnCategory" runat="server" Text="Update" OnClick="btnCategory_Click" BackColor="#999966" BorderColor="Maroon" Style="margin-left: 72px;" /></td>
                    </tr>
                </table>
                <%--</ContentTemplate>
                </asp:UpdatePanel>--%>
            </div>
            <button type="button" class="collapsible">Correction /Change of bank details</button>
            <div style="background-color:white;" class="content">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>
                        <table style="width: 100%; padding: 7px; border:1px solid black">
                            <tr>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                                <td width="25%"></td>
                            </tr>
                             <%--<tr>
                                
                                <td align="right" colspan="2">Edit For :</td>
                                <td  align="left" colspan="2">
                                    <asp:RadioButtonList ID="radBank" runat="server" AutoPostBack="True" OnSelectedIndexChanged="radBank_SelectedIndexChanged1">
                                        <asp:ListItem Value="a0">Only Account Holder Name</asp:ListItem>
                                        <asp:ListItem Value="a1">Bank Details</asp:ListItem>
                                    </asp:RadioButtonList></td>
                                <td></td>
                            </tr>--%>
                            <tr runat="server" id="trBankDetails">
                                <td colspan="4" width="100%">
                                    <table width="100%">
                                        <tr>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                            <td width="25%"></td>
                                        </tr>
                                         <tr>
                                <td colspan="2" align="center" style="width:50%">
                                        Existing Account Details
                                </td>
                                <td colspan="2" align="center" style="width:50%">
                                        Change Of Account Details
                                </td>
                            </tr>
                           
                            <tr>
                                <td style="width:25%">Account Holder Name :</td>
                                <td align="left" runat="server" id="tdEAccHolderName" style="width:25%"></td>
                              
                                <td style="width:50%" colspan="2" align="center" >
                                    <%--<asp:Label ID="lblAccHolderNameE" runat="server" Text=""></asp:Label>--%>
                                    <asp:TextBox ID="txtAccHolderName" runat="server" CssClass="textCol" placeholder="Acc Holder Name"></asp:TextBox>
                                    </td>
                                

                            </tr>
                            <tr>
                                <td style="width:25%">Account No : </td>
                                <td align="left" runat="server" id="tdEAccNo" style="width:25%"></td>
                               
                                <td align="center" style="width:50%" colspan="2">
                                    <asp:Label ID="lblAccNo" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtAccNo" runat="server" CssClass="textCol" placeholder="Acc. No."></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width:25%">Bank Name : </td>
                                <td align="left" runat="server" id="tdEBrankName" style="width:25%"></td>
                                
                                <td align="center" style="width:50%" colspan="2">
                                <asp:Label ID="lblBankNm" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList ID="ddlBankNm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBankNm_SelectedIndexChanged">
                                        <%----%>
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width:25%">Branch Name : </td>
                                <td align="left" runat="server" id="tdEBranchName" style="width:25%"></td>
                                <td align="center" style="width:50%" colspan="2">
                                <asp:Label ID="lblBranchNM" runat="server" Text=""></asp:Label>
                                    <asp:DropDownList ID="ddlBranchNm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBranchNm_SelectedIndexChanged">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td style="width:25%">IFSC Code : </td>
                                <td align="left" runat="server" id="tdEIfsc" style="width:25%"></td>
                                <td align="center" style="width:50%" colspan="2">
                                <asp:Label ID="lblfsc" runat="server" Text=""></asp:Label>
                                    <asp:TextBox ID="txtIfscCd" runat="server" placeholder="IFSC Code" ReadOnly="true" TextMode="SingleLine"></asp:TextBox></td>

                            </tr>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">

                                <ContentTemplate>
                                    <tr>
                                        <td style="width:50%" colspan="2"></td>
                                        
                                       
                                        <td align="center" style="width:50%" colspan="2">
                                            <asp:FileUpload ID="updPassbook" runat="server"></asp:FileUpload><br />(Upload Scan Copy Of Passbook<span style="color:red">*(.pdf&nbsp; within 150kb)</span>)</td>
                                    </tr>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <tr><td colspan="4"><hr /></td></tr>
                            <tr runat="server" id="trBankFmn">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed Farm Machinary</td>
                                <td align="left"> <img src="../images/Right.jpg"  width="20px" height="15px"/></td>
                            </tr>
                            <tr runat="server" id="trBankApicol">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed APICOL</td>
                                <td align="left">
                                    <img src="../images/Right.jpg" width="20px" height="15px"/></td>
                            </tr>
                    <tr runat="server" id="trBankJal">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed JALANIDHI</td>
                                <td align="left">
                                    <img src="../images/Right.jpg" width="20px" height="15px"/></td>
                            </tr>
                    <tr runat="server" id="trBankSolar">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy availed Solar</td>
                                <td align="left">
                                    <img src="../images/Right.jpg" width="20px" height="15px"/></td>
                            </tr>
                                         <tr runat="server" id="trTrnFalure">
                                <td></td>
                                <td></td>
                                <td align="right">Subsidy Transaction Failure</td>
                                <td align="left">
                                    <img src="../images/Right.jpg" width="20px" height="15px"/></td>
                            </tr>
                            <tr>
                                <td style="width:25%"></td>
                        <td style="width:25%"></td>
                                <td style="width:50%" colspan="2" align="center">
                                    <asp:Button ID="btnBankDetails" runat="server" Text="Update" OnClick="btnBankDetails_Click" BackColor="#999966" BorderColor="Maroon" Style="margin-left: 72px;"/></td>
                            </tr>

                                    </table>
                                </td>
                            </tr>
                            
                           

                        </table>
                    </ContentTemplate>
                    <Triggers>
                        <ajax:PostBackTrigger ControlID="btnBankDetails" />
<asp:PostBackTrigger ControlID="btnBankDetails"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="btnBankDetails"></asp:PostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
     <script type="text/javascript">
         var coll = document.getElementsByClassName("collapsible");
         debugger;
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

