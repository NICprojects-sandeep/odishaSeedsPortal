<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="AccountDetails.aspx.cs" Inherits="Reports_AccountDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
             <div class="titleHeader">
        &nbsp; Account Details
    </div>
    <br />

    <script type="text/javascript">
        $(document).ready(function () {
            $("#divLoader").hide();
            $('.hideMe').hide();;

            $('#btnDetails').click(function () {

                $('#tblRegDetails tr').detach();
                $('#tblReqSentDetails tr').detach();
                $('#tblFarmerApprovedDetails tr').detach();
                $('#tblAckMessageDetails tr').detach();
                $('#tblAckMessageDetailsD tr').detach();
                $('#tblAckMessageDetailsD2 tr').detach();
                $('.hideMe').hide();
                $("#divLoader").show();

                var AccNo = $("#<%=txtAccountNo.ClientID%>").val();
                var MobNo = $("#<%=txtMobileNo.ClientID%>").val();
                $.ajax({
                    url: 'AccountDetails.aspx/GetRegisteredFarmer',
                    type: 'GET',
                    contentType: 'application/json;charset=utf-8',
                    data: { acNo: JSON.stringify(AccNo), mobNo: JSON.stringify(MobNo) },
                    dataType: 'json',
                    success: function (data) {
                        if (data.d.length != 0) {
                            $('#spanReg').show();
                            $('#tblRegDetails').show();

                            $('#tblRegDetails').append('<tr><th>Farmer Code</th><th>Farmer Name</th><th>Father Name/Husband</th><th>Mobile No</th><th>Account No</th><th>Bank Name</th><th>Branch Name</th><th>IFSC Code</th><th>Status</th><tr>');
                            $.each(data.d, function (i, item) {
                                $('#tblRegDetails').append('<tr><td>' + item.farmerCode + '</td><td>' + item.farmerName + '</td><td>' + item.fatherName + '</td><td>' + item.mobNo + '</td><td>' + item.accountNo + '</td><td>' + item.bankName + '</td><td>' + item.branchName + '</td><td>' + item.IFSCCode + '</td><td>' + item.pfmsStatus + '</td></tr>');
                            });
                        }
                    }
                });

                $.ajax({
                    url: 'AccountDetails.aspx/GetReqSentDetails',
                    type: 'GET',
                    contentType: 'application/json;charset=utf-8',
                    data: { acNo: JSON.stringify(AccNo), mobNo: JSON.stringify(MobNo) },
                    dataType: 'json',
                    success: function (data) {
                        if (data.d.length != 0) {
                            $('#spanReqSent').show();
                            $('#tblReqSentDetails').show();

                            $('#tblReqSentDetails').append('<tr><th>Unique Msg ID</th><th>Name</th><th>Father Name</th><th>Account No</th><th>Scheme Specific ID</th><th>IFSC Code</th><th>Mobile No</th><tr>');
                            $.each(data.d, function (i, item) {
                                $('#tblReqSentDetails').append('<tr><td>' + item.uniqmsgID + '</td><td>' + item.Name + '</td><td>' + item.fatherName + '</td><td>' + item.accountNo + '</td><td>' + item.schemeSpecificID + '</td><td>' + item.IFSCCode + '</td><td>' + item.mobNo + '</td></tr>');
                            });
                        }
                    }
                });


                $.ajax({
                    url: 'AccountDetails.aspx/GetApproveFarmerDetails',
                    type: 'GET',
                    contentType: 'application/json;charset=utf-8',
                    data: { acNo: JSON.stringify(AccNo), mobNo: JSON.stringify(MobNo) },
                    dataType: 'json',
                    success: function (data) {

                        $("#divLoader").hide();
                        if (data.d.length != 0) {
                            $('#spanApproved').show();
                            $('#tblFarmerApprovedDetails').show();
                            $('#tblFarmerApprovedDetails').append('<tr><th>Farmer ID</th><th>Farmer Code</th><th>Status</th><tr>');

                            $.each(data.d, function (i, item) {
                                $('#tblFarmerApprovedDetails').append('<tr><td>' + item.fID + '</td><td>' + item.FCode + '</td><td>' + item.status + '</td></tr>');
                            });
                        }
                        else {

                            $.ajax({
                                url: 'AccountDetails.aspx/GetAllNotApprovedData',
                                type: 'GET',
                                contentType: 'application/json;charset=utf-8',
                                data: { acNo: JSON.stringify(AccNo), mobNo: JSON.stringify(MobNo) },
                                dataType: 'json',
                                success: function (data1) {
                                    $("#divLoader").hide();
                                    if (data1.d.RBAM.length != 0) {
                                        $('#spanAckMessage').show();
                                        $('#tblAckMessageDetails').show();
                                        $('#tblAckMessageDetails').append('<tr><th>Farmer ID</th><th>Farmer Code</th><th>Status</th><th>Reason</th><tr>');

                                        $.each(data1.d.RBAM, function (key, value) {
                                            $('#tblAckMessageDetails').append('<tr><td>' + value.fID + '</td><td>' + value.fCode + '</td><td>' + value.status + '</td><td>' + value.reason + '(' + value.accNo + ')' + '</td></tr>');
                                        });
                                    }
                                    if (data1.d.RBAMD.length != 0) {
                                        $('#spanAckMessageD').show();
                                        $('#tblAckMessageDetailsD').show();
                                        $('#tblAckMessageDetailsD').append('<tr><th>FCode</th><th>Name</th><th>Status</th><th>Reason</th><tr>');
                                        $.each(data1.d.RBAMD, function (key, value) {
                                            $('#tblAckMessageDetailsD').append('<tr><td>' + value.fCode + '</td><td>' + value.fName + '</td><td>' + value.status + '</td><td>' + value.reason + '(' + value.accNo + ')' + '</td></tr>');
                                        });
                                    }
                                    if (data1.d.RBAMD2.length = 0) {
                                        $('#spanAckMessageD2').show();
                                        $('#tblAckMessageDetailsD2').show();
                                        $('#tblAckMessageDetailsD2').append('<tr><th>FCode</th><th>Name</th><th>Status</th><th>Reason</th><tr>');
                                        $.each(data1.d.RBAMD2, function (key, value) {
                                            $('#tblAckMessageDetailsD2').append('<tr><td>' + value.fCode + '</td><td>' + value.fName + '</td><td>' + value.status + '</td><td>' + value.reason + '(' + value.accNo + ')' + '</td></tr>');
                                        });
                                    }
                                }
                            });
                        }
                    }
                });
            });
        });
    </script>

    <div style="height: 100%; width: 100%">
         <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
        <fieldset style="margin-top: 20px; width: 90%; margin-left: 100px">
            <legend>
                <h4 style="color: black; font-weight: 700">Account Details</h4>
            </legend>
            <div style="margin-left: 80px">
                <div style="width: 100%">
                    Enter Account No : 
                        <asp:TextBox ID="txtAccountNo" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <span style="margin-left: 25px; margin-right: 25px; font-size: 15px; color: red">OR</span>
                    Enter Mobile No : 
                        <asp:TextBox ID="txtMobileNo" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <input type="button" id="btnDetails" value="GetDetails" style="margin-left: 40px" />
                </div>
            </div>
            <div style="margin-top: 20px">
                <div class="col-sm-12" id="divApprove">
                    <span id="spanReg" class="hideMe" style="font-size: 15px">M_FARMER_REGISTRATION (Online Registered Farmer)</span>
                    <table id="tblRegDetails" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>
                    <span id="spanReqSent" class="hideMe" style="font-size: 15px">Request_tbl_Beneficiary_Message (Request Sent to PFMS)</span>
                    <table id="tblReqSentDetails" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>
                    <span id="spanApproved" class="hideMe" style="font-size: 15px">Tbl_FarmerApprove (Farmer is Approved)</span>
                    <table id="tblFarmerApprovedDetails" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>
                    <span id="spanAckMessage" class="hideMe" style="font-size: 15px; margin-top: 40px">Response_tbl_Beneficiary_Ack_Message (Response Received from PFMS-CCB for Account Validation)</span>
                    <table id="tblAckMessageDetails" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>
                    <span id="spanAckMessageD" class="hideMe" style="font-size: 15px; margin-top: 40px">Response_tbl_Beneficiary_Ack_Message_D (Duplicate Response)</span>
                    <table id="tblAckMessageDetailsD" class="hideMe" border="1" width="100%" style="margin-top: 10PX; margin-bottom: 15px">
                    </table>
                    <span id="spanAckMessageD2" class="hideMe" style="font-size: 15px; margin-top: 40px">Response_tbl_Beneficiary_Ack_Message_D2 (Double Response Received)</span>
                    <table id="tblAckMessageDetailsD2" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>
                </div>
            </div>
            <div class="row">
                <div id="divLoader" class="loading" align="center">
                    <img src="../images/circleloadinganimation.gif" style="height: 200px; width: 200px" />
                </div>

            </div>
        </fieldset>
    </div>
</asp:Content>

