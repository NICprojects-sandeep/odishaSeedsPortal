<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="FarmerAccountInformation.aspx.cs" Inherits="Reports_FarmerAccountInformation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
                     <div class="titleHeader">
        &nbsp;Farmer Account Information
    </div>
    <br />
    <script type="text/javascript">
        $(document).ready(function () {
            $("#divLoader").hide();
            $('.hideMe').hide();
            //$('#ctl00_ContentPlaceHolder2_btnUpdate').hide();

            $('#btnReset').click(function () {

                $('#txtVoterID').val("");
                $("#divLoader").hide();
                $('.hideMe').hide();

            });

            $('#btnDetails').click(function () {
                $("#divLoader").show();
                $('.hideMe').hide();
                //$('#ctl00_ContentPlaceHolder2_btnUpdate').hide();
                
                $('#tblFarmerInformation tr').detach();
                $('#tblAccountDetails tr').detach();
                $("#tblApproveStatus tr").detach();
                $("#tblAAOAAO1 tr").detach();
                $('#tblReject tr').detach();

                var voterid = $("#<%=txtVoterID.ClientID%>").val();
                var accountNo = $("#<%=txtAccountNo.ClientID%>").val();
                $.ajax({
                    url: 'FarmerAccountInformation.aspx/GetFarmerDetails',
                    type: 'GET',
                    contentType: 'application/json;charset=utf-8',
                    data: { voterNo: JSON.stringify(voterid), accNo: JSON.stringify(accountNo) },
                    dataType: 'json',
                    success: function (data)
                    {
                        $("#divLoader").hide();
                        $('#tblFarmerInformation').show();
                        $('#tblAccountDetails').show();
                        $('#spanFarmerInformation').show();
                        $('#spanAccountDetails').show();

                        if (data.d != null) {
                            $('#tblFarmerInformation').append('<tr><th>Farmer Name</th><th>Father Name/Husband</th><th>District</th><th>Block</th><th>GP</th><th>Village</th><th>Mobile No</th><th>Voter ID</th></tr>');
                            $('#tblFarmerInformation').append('<tr><td>' + data.d.farmerName + '</td><td>' + data.d.fatherName + '</td><td>' + data.d.distName + '</td><td>' + data.d.blockName + '</td><td>' + data.d.GP + '</td><td>' + data.d.villName + '</td><td>' + data.d.mobNo + '</td><td>' + data.d.voterID + '</td></tr>');

                            $('#tblAccountDetails').append('<tr><th>Account No</th><th>Account Holder Name</th><th>Bank Name</th><th>Branch</th><th>IFSC</th></tr>');
                            $('#tblAccountDetails').append('<tr><td>' + data.d.accNo + '</td><td>' + data.d.accHolderName + '</td><td>' + data.d.bankName + '</td><td>' + data.d.branchName + '</td><td>' + data.d.IFSC + '</td></tr>');
                        }
                        else {

                            alert('Invalid VoterID. OR Account No.!!!');
                            $('#txtVoterID').val("");
                        }
                    }
                });

                $.ajax({
                    url: 'FarmerAccountInformation.aspx/GetFarmerApproveStatusDetails',
                    type: 'GET',
                    contentType: 'application/json;charset=utf-8',
                    data: { voterNo: JSON.stringify(voterid), accNo: JSON.stringify(accountNo) },
                    dataType: 'json',
                    success: function (data) {
                        if (data.d != null) {
                            $("#spanApproveStatus").show();
                            //data.d.status == "ACCP" || data.d.status == "CCB" || data.d.status == "UIID"
                            //$("#tblApproveStatus").show();
                            //$("#tblAAOAAO1").hide();
                            //$("#tblReject").hide();
                            if (data.d.status == "ACCP" || data.d.status == "CCB") {
                                $("#tblApproveStatus").show();
                                $('#tblApproveStatus').append('<tr><th>Status</th><th>Farmer ID</th></tr>');
                                $('#tblApproveStatus').append('<tr><td>' + data.d.status + '</td><td>' + data.d.farID + '</td></tr>');
                            }
                            else if (data.d.status == "UIID") {
                                $('#tblReject').show();
                                $('#tblReject').append('<tr><th>Status</th><th>Reason</th></tr>');
                                $('#tblReject').append('<tr><td>' + data.d.status + '</td><td>Account is invalid but Adhar enable account is valid.AAO are requested to send mail to dbt.odisha@gov.in for adhar confirmation.</td></tr>');
                            }
                            else if (data.d.status == "RJCT") {
                               
                                $('#tblReject').show();
                                $('#tblReject').append('<tr><th>Status</th><th>Reason</th></tr>');
                                $('#tblReject').append('<tr><td>' + data.d.status + '</td><td>Account holder name mismatch.</td></tr>');
                            }

                            else if (data.d.status == "AAO" || data.d.status == "AAO1") {
                                //$("#spanApproveStatus").show();
                                $('#spanAAOAAO1').show();
                            }
                        }


                            //else {
                            //    $.ajax({
                            //        url: 'FarmerAccountInformation.aspx/GetFarmerAAOAAO1StatusDetails',
                            //        type: 'GET',
                            //        contentType: 'application/json;charset=utf-8',
                            //        data: { voterNo: JSON.stringify(voterid), accNo: JSON.stringify(accountNo) },
                            //        dataType: 'json',
                            //        success: function (dataAAO) {

                            //            if (dataAAO.d != 0) {
                            //                $("#spanApproveStatus").show();
                            //                $('#spanAAOAAO1').show();

                            //            }
                        else {

                            $.ajax({
                                url: 'FarmerAccountInformation.aspx/GetFarmerRejectStatusDetails',
                                type: 'GET',
                                contentType: 'application/json;charset=utf-8',
                                data: { voterNo: JSON.stringify(voterid), accNo: JSON.stringify(accountNo) },
                                dataType: 'json',
                                success: function (dataReject) {

                                    if (dataReject.d != null) {



                                        $("#spanApproveStatus").show();

                                        if (dataReject.d.status == 'ACCP') {
                                            //$('#ctl00_ContentPlaceHolder2_btnUpdate').show();

                                            $("#tblApproveStatus").show();
                                            $('#tblApproveStatus').append('<tr><th>Status</th><th>Reason</th></tr>');
                                            $('#tblApproveStatus').append('<tr><td>Accepted</td><td>PFMS Approved But Department Rejected due to Account Holder Name Mismatch.(Account Holder name returned by Bank is ' + dataReject.d.Name + ')</td></tr>');
                                        }
                                        else if (dataReject.d.status == 'RJCT') {

                                           
                                            $('#tblReject').show();

                                            $('#tblReject').append('<tr><th>Status</th><th>Reason</th></tr>');
                                                                                        $('#tblReject').append('<tr><td>' + dataReject.d.status + '</td><td>' + dataReject.d.reason + '(' + dataReject.d.AccNo + ')' +'</td></tr>');
//                                            $('#tblReject').append('<tr><td>' + dataReject.d.status + '</td><td>' + dataReject.d.reason + '</td></tr>');
                                        }
                                        else if (dataReject.d.status == 'Accepted') {

                                            $("#tblApproveStatus").show();
                                            $('#tblApproveStatus').append('<tr><th>Status</th><th>Reason</th></tr>');
                                            $('#tblApproveStatus').append('<tr><td>' + dataReject.d.status + '</td><td>' + dataReject.d.reason + '</td></tr>');
                                        }
                                    }

                                }
                            });
                        }

                        //        }
                        //    });
                        //}


                    }
                });
            });



        });
    </script>

       <div style="height: 100%; width: 100%">
         <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
        <fieldset style="margin-top: 20px; width: 90%; margin-left: 100px">
            <legend>
                <h4 style="color: black; font-weight: 700">Farmer Account Details</h4>
            </legend>
            <div style="margin-left: 80px">
                <div style="width: 100%">
                    Enter Account No : 
                        <asp:TextBox ID="txtAccountNo" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <span style="margin-left: 25px; margin-right: 25px; font-size: 15px; color: red">OR</span>
                    Enter Voter ID : 
                        <asp:TextBox ID="txtVoterID" runat="server" ClientIDMode="Static"></asp:TextBox>
                    <input type="button" id="btnDetails" value="GetDetails" style="margin-left: 40px" />
                    <input type="button" id="btnReset" value="Reset" style="margin-left: 40px" />
                    <%--<asp:Button ID="btnUpdate" runat="server" Text="Update Farmer Account" style="margin-left: 40px" OnClick="btnUpdate_Click"/>--%>
                   <%-- <input type="button" id="btnUpdate1" value="Update Farmer" style="margin-left: 40px" />--%>
                </div>
            </div>
            <div style="margin-top: 20px">
                <div class="col-sm-12" id="divApprove">
                    <span id="spanFarmerInformation" class="hideMe" style="font-size: 15px">Farmer Information</span>
                    <table id="tblFarmerInformation" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>
                    <span id="spanAccountDetails" class="hideMe" style="font-size: 15px">Account Details</span>
                    <table id="tblAccountDetails" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>
                    <span id="spanApproveStatus" class="hideMe" style="font-size: 15px">Registration Status</span>
                    <table id="tblApproveStatus" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>
                   <span id="spanAAOAAO1" class="hideMe" style="font-size: 15px; margin-top: 40px">Pending For Approval Of AAO/AAO1.</span>
                   <%-- <table id="tblAAOAAO1" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>--%>
                    <%--<span id="spanReject" class="hideMe" style="font-size: 15px; margin-top: 40px">Registration Status</span>--%>
                    <table id="tblReject" class="hideMe" border="1" width="100%" style="margin-top: 10PX; margin-bottom: 15px">
                    </table>
                    <%-- <span id="spanAckMessageD2" class="hideMe" style="font-size: 15px; margin-top: 40px">Response_tbl_Beneficiary_Ack_Message_D2 (Double Response Received)</span>
                    <table id="tblAckMessageDetailsD2" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
                    </table>--%>
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

