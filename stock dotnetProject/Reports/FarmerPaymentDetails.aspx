<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/AAO_.master" AutoEventWireup="true" CodeFile="FarmerPaymentDetails.aspx.cs" Inherits="Reports_FarmerPaymentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">

    <script type="text/javascript" lang="javascript">
        $(document).ready(function () {
            $('#divLoader').hide();
            $('.hideMe').hide();

            $.ajax({
                url: 'FarmerPaymentDetails.aspx/GetDistCode',
                type: 'GET',
                contentType: 'application/json;charset=utf-8',
                //data: { fID: JSON.stringify(farID) },
                dataType: 'json',
                success: function (data) { 
                   // $('#ctl00_ContentPlaceHolder2_lblDistCode').html(data.d);
                    $("#<%=lblDistCode.ClientID%>").html(data.d);
                }
            
            });


            $('#btnDetails').click(function () {

                $('#divLoader').show();
                $('#tblFarmerStockDetails tr').detach();
                var txtValue = $("#<%=txtFarmerID.ClientID%>").val();
                
                if (!isNaN(parseFloat(txtValue)) && isFinite(txtValue)) {

                    var farID = $("#<%=lblDistCode.ClientID%>").text()+"/"+$("#<%=txtFarmerID.ClientID%>").val().trim();

                    <%--$.ajax({
                        url: 'FarmerPaymentDetails.aspx/GetCount',
                        type: 'GET',
                        contentType: 'application/json;charset=utf-8',
                        data: { fID: JSON.stringify(farID) },
                        dataType: 'json',
                        success: function (dataCount) {

                            if (dataCount.d == 0) {
                                $('#divLoader').hide();
                                alert('Farmer ID not available.')
                                $("#<%=txtFarmerID.ClientID%>").val("");
                            }
                            else {--%>

                                $.ajax({
                                    url: 'FarmerPaymentDetails.aspx/GetStockDetails',
                                    type: 'GET',
                                    contentType: 'application/json;charset=utf-8',
                                    data: { fID: JSON.stringify(farID) },
                                    dataType: 'json',
                                    success: function (data) {
                                        $('#divLoader').hide();
                                        if (data.d.length > 0) {
                                            $('.hideMe').show();

                                            $('#tblFarmerStockDetails').append('<tr><th>Transaction No.</th><th>Transaction Date</th><th>Farmer Name</th><th>Bag Size(In KG)</th><th>No. Of Bags</th><th>Total Qtl (In QTL.)</th><th>Amount</th><th>Status</th><th>Paid Status</th></tr>');
                                            $.each(data.d, function (key, value) {
                                                $.each(value.lstPaymentStatus, function (i, item) {
                                                    $('#tblFarmerStockDetails').append('<tr><td>' + item.uniqtransID + '</td><td>' + item.transDate + '</td><td>' + item.farmerName + '</td><td>' + item.bagSize + '</td><td>' + item.noOfBags + '</td><td>' + item.totQTL + '</td><td>' + item.amount + '</td><td>' + item.status + '</td><td>' + item.paidStatus + '</td></tr>');
                                                });
                                            });
                                        }
                                        else {
                                            alert('Records not available');
                                            $("#<%=txtFarmerID.ClientID%>").val("");
                                        }
                                    }
                                });

                            //}

                    //    }
                    //});
                }
                else {
                    $('#divLoader').hide();
                    alert('Please enter numbers only.');
                    $("#<%=txtFarmerID.ClientID%>").val("");
                }
            });
        });
    </script>

    <div style="height: 100%; width: 100%">
        <asp:HiddenField ID="hfSession" runat="server" Visible="false" />
        <fieldset style="margin-top: 20px; width: 90%; margin-left: 70px">
            <legend>
                <h4 style="color: black; font-weight: 700">Farmer Stock,Amount & Status Details</h4>
            </legend>
            <div style="margin-left: 80px">
                <div style="width: 100%">
                    Enter Farmer ID : 
                        <asp:Label ID="lblDistCode" runat="server" ClientIDMode="Static"></asp:Label>
                    <asp:TextBox ID="txtFarmerID" runat="server" ClientIDMode="Static"></asp:TextBox>
                    
                    <input type="button" id="btnDetails" value="GetDetails" style="margin-left: 40px" />
                </div>
            </div>
            <div style="margin-top: 20px">
                <div class="col-sm-12" id="divFarmerStockDetails">
                    <table id="tblFarmerStockDetails" class="hideMe" border="1" style="width: 100%; margin-top: 10px; margin-bottom: 15px">
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

