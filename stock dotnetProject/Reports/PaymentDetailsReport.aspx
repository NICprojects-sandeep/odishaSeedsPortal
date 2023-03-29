<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SeedAdmin.master" AutoEventWireup="true" CodeFile="PaymentDetailsReport.aspx.cs" Inherits="Reports_PaymentDetailsReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">
             <div class="titleHeader">
        &nbsp; Payment Details
    </div>
    <br />
    <script type="text/javascript">

        $(document).ready(function () {
            $("#divLoader").hide();

            //$.ajax({
            //    url: 'PaymentDetailsReport.aspx/DistName',
            //    type: 'GET',
            //    contentType: 'application/json; charset=utf-8',
            //    dataType: 'json',
            //    success: function (data) {
            //        $.each(data, function (i, item) {
            //            $.each(item, function (key, value) {
            //                $('#ddlDistrict').append("<option value=" + value.ShortName + ">" + value.DistName + "</option>");
            //            });
            //        });
            //    }
            //});

            //$('.ddl').change(function () {

            //    if ($('#ddlDistrict').val() != 'SEL' && $('#ddlDetails').val() != 'SEL') {
            //        $("#divLoader").show();
            //        var dist = $('#ddlDistrict').val();
            //        var rem = $('#ddlDetails').val();
            //        $('#tblFarmer tbody').detach();

            //        $.ajax({
            //            url: 'PaymentDetailsReport.aspx/GetFarmerIDByDist',
            //            type: 'GET',
            //            contentType: 'application/json; charset=utf-8',
            //            dataType: 'json',
            //            data: { shName: JSON.stringify(dist), remark: JSON.stringify(rem) },
            //            success: function (data) {
            //                $("#divLoader").hide();

            //                $('#tblFarmer').append('<tr><th>FID</th><th>Transaction ID</th><th>Transaction Date</th><th>Payment Request Sent By NIC</th><th>Response Received From PFMS</th><th>Bulk Status</th><th>Paid By Bank</th><tr>');
            //                $.each(data, function (i, item) {
            //                    if (item != '') {
            //                        $.each(item, function (key, value) {
            //                            $('#tblFarmer').append('<tr><td>' + value.farmerID + '</td><td>' + value.transactionCode + '</td><td>' + value.transDate + '</td><td>' + value.reqSent +
            //                                   '</td><td>' + value.responseRecvStatus + '</td><td>' + value.bulkStatus + '</td><td>' + value.BankPaid + '</td></tr>');
            //                        });
            //                    }
            //                    else {
            //                        alert('No Records Available.!!!')
            //                    }
            //                });

            //            },

            //            error: function (err) {

            //            }
            //        });
            //    }
            //});

            $('#btnDetails').click(function () {
                if ($("#<%=txtFID.ClientID%>").val() != '' && $('#ddlDetails').val() != 'SEL') {
                    $("#divLoader").show();
                    $('#tblFarmer tr').detach();
                    var fid = $("#<%=txtFID.ClientID%>").val();
                    var rem = $('#ddlDetails').val();
                    $.ajax({
                        url: 'PaymentDetailsReport.aspx/GetFarmerID',
                        type: 'GET',
                        contentType: 'application/json;charset=utf-8',
                        data: { fID: JSON.stringify(fid), remark: JSON.stringify(rem) },
                        dataType: 'json',
                        success: function (data) {
                            $("#divLoader").hide();
                            $('#tblFarmer').append('<tr><th>FID</th><th>Transaction ID</th><th>Req MsgID</th><th>Res MsgID</th><th>Transaction Date</th><th>Payment Request Sent By NIC</th><th>Response Received From PFMS</th><th>Bulk Status</th><th>Paid By Bank</th><tr>');

                            $.each(data, function (i, item) {
                                if (item != '') {
                                    $.each(item, function (key, value) {
                                        $('#tblFarmer').append('<tr><td>' + value.farmerID + '</td><td>' + value.transactionCode + '</br>A/c - ' + value.accountNo + '</td><td>' + value.reqMsgID + '</td><td>' + value.respMsgID + '</td><td>' + value.transDate + '</td><td>' + value.reqSent +
                                               '</td><td>' + value.responseRecvStatus + '</td><td>' + value.bulkStatus + '</td><td>' + value.BankPaid + '</td></tr>');
                                    });
                                }
                                else {
                                    alert('No Records Available.!!!')
                                }
                            });
                        },
                        error: function (err) {
                            console.log(err);
                        }
                    });
                }
                else {
                    alert('Please,Fill All Fields.!!!');
                }
            });

        });
    </script>



     <div class="container" style="height: 100%;width:100%">
          <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
            <fieldset style="margin-top: 20px; width: 90%; margin-left: 100px">
                <legend>
                    <h4 style="color: black; font-weight: 700">Payment Details</h4>
                </legend>
                <div style="width:100%">
                        Farmer ID :
                        <asp:TextBox ID="txtFID" runat="server" ClientIDMode="Static"></asp:TextBox>
                   
                       <span style="margin-left:200px">Select :</span>
                        <select id="ddlDetails" class="ddl" style="width: 300PX">
                            <option value="SEL">---Select---</option>
                            <option value="ALL">All</option>
                            <option value="PAN">Pending At NIC</option>
                            <option value="PAD">Pending At Department</option>
                            <option value="SFNPAP">Sent From NIC Pending At PFMS</option>
                        </select>

                    <input type="button" id="btnDetails" value="GetDetails" />
                </div>
               <%-- <div style="margin-top: 20px">
                    Select District :
                        <select id="ddlDistrict" class="ddl" style="width: 300PX">
                            <option value="SEL">---Select---</option>
                        </select>
                </div>--%>
                <div class="row" style="margin-top: 20px">
                    <div class="col-sm-12">
                        <table id="tblFarmer" border="1" width="100%">
                        </table>
                    </div>
                </div>
                 <div class="row">
                <div id="divLoader" class="loading" align="center">
                    <img src="../images/circleloadinganimation.gif" style="height:200px;width:200px"/>
                </div>

            </div>
            </fieldset>
         </div>
</asp:Content>

