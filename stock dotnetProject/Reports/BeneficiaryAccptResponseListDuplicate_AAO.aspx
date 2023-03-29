<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="BeneficiaryAccptResponseListDuplicate_AAO.aspx.cs" Inherits="Reports_BeneficiaryAccptResponseListDuplicate_AAO" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" Runat="Server">

    <script>
        $(document).ready(function () {
            $('#tblBeneficiaryAccptResponseListDuplicate').hide();
            $.ajax({
                url: 'BeneficiaryAccptResponseListDuplicate_AAO.aspx/GetBeneficiaryResponseListDuplicate',
                type: 'GET',
                contentType: 'application/json;charset=utf-8',
                dataType: 'json',
                success: function (data) {
                    $('#divLoader').hide();
                    $('#tblBeneficiaryAccptResponseListDuplicate').show();
                    $('#tblBeneficiaryAccptResponseListDuplicate').append('<tr><th>Account No</th><th>Farmer Name</th><th>Father Name</th><th>Account Holder Name</th><th>Farmer Code</th><th>Farmer ID</th><th>IFSC Code</th></tr>');

                    $.each(data.d, function (i, item) {
                        $('#tblBeneficiaryAccptResponseListDuplicate').append('<tr><td>' + item.accountNo + '</td><td>' + item.farmerName + '</td><td>' + item.fatherName + '</td><td>' + item.accountHolderName + '</td><td>' + item.farmerCode + '</td><td>' + item.farmerID + '</td><td>' + item.IFSCCode + '</td><tr>')
                    });
                }

            });

        });

    </script>


      <div style="height: 100%; width: 100%">
         <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
        <fieldset style="margin-top: 20px; width: 90%; margin-left: 100px">
            <legend>
                <h4 style="color: black; font-weight: 700">Beneficiary Response List</h4>
            </legend>
            
            <div style="margin-top: 20px">
                <div class="col-sm-12" id="divApprove">
                    <span id="spanBeneficiaryAccptResponseListDuplicate" class="hideMe" style="font-size: 15px"></span>
                    <table id="tblBeneficiaryAccptResponseListDuplicate" class="hideMe" border="1" width="100%" style="margin-top: 10px; margin-bottom: 15px">
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

