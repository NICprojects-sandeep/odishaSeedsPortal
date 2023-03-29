<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTopup.aspx.cs" MasterPageFile="~/MasterPages/BASAdmin.master" Inherits="Admin_AddTopup" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
      <section class="content-header">
      <h1>
        ADD TOPUP
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Admin</a></li>
        <li class="active">Add Topup</li>
      </ol>
    </section>
    <!-- Main content -->
    <section class="content">
      <div class="row">
        <!-- left column -->
        <div class="col-md-12">
          <!-- general form elements -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Enter Bank, Account Details and Amount</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <%--<form role="form" runat="server">--%>
              <div class="box-body">
                <div class="form-group col-md-4">
                  <label for="imeiText">Bank</label>
                  <select class="form-control" id="bank-select">
                      <option>Bank</option>
                  </select>
                </div>
                <div class="form-group col-md-4">
                  <label for="passwordText">Account No</label>
                  <select class="form-control" id="account-select">
                      <option>Select Account No</option>
                  </select>
                </div>
                <div class="form-group col-md-4">
                  <label for="systemidText">Amount (in Rs.)</label>
                  <input type="text" class="form-control" id="amount" placeholder="Enter Amount(in Rs.)" required>
                </div>
              <!-- /.box-body -->
                 </div>
              <div class="box-footer">
                <asp:Button type="submit" class="btn btn-primary" id="add_topup_submit" runat="server" style="float:right"  Text="Submit"/>
              </div>
                  
            <%--</form>--%>
          </div>
          <!-- /.box -->


          <!-- Input addon -->

        </div>
        <!--/.col (left) -->
         
      </div>
          <div class="box">
            <div class="box-header">
              <h3 class="box-title">Topups Added</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table class="table table-bordered table-striped">
                <thead>
                <tr>
                  <th>BANK NAME</th>
                  <th>ACCOUNT NO</th>
                  <th>TOPUP AMOUNT(IN RUPEES)</th>
                </tr>
                </thead>
                <tbody  id="myList">
                
               
                </tbody>
               
              </table>
            </div>
            <!-- /.box-body -->
          </div>
          <!-- /.box -->
      <!-- /.row -->
    </section>
    <!-- /.content -->
    <script>
        $(document).ready(function () {
            $('#bank-select').change(function () {
                $(function () {
                    $.ajax({
                        type: "GET",
                        url: "../Admin/AddTopup.aspx/populateAccountNo",

                        data: { bankID: JSON.stringify($('#bank-select').val()) },
                        contentType: "application/json",
                        dataType: "json",
                        success: OnSuccess,
                        failure: function (response) {
                            alert("No");
                        }
                    });
                    function OnSuccess(response) {
                        for (i = 0; i < response.d.length; i++) {
                            $('#account-select').append('<option value="' + response.d[i].Account_Number + '">' + response.d[i].Account_Number + '</option>');
                        }
                    }
                });
            });
            $('#<%= add_topup_submit.ClientID %>').click(function () {
                var a = $('#system-select').val();
               

                $(function () {
                    $.ajax({
                        type: "GET",
                        url: "../Admin/AddTopup.aspx/topupAdd",

                        data: { bankID: JSON.stringify($('#bank-select').val()), accountNo: JSON.stringify($('#account-select').val()), amount: JSON.stringify($('#amount').val()) },
                        contentType: "application/json",
                        dataType: "json",
                        success: OnSuccess,
                        failure: function (response) {
                            alert("No");
                        }
                    });
                    function OnSuccess(response) {
                        alert("Topup Added Successfully!");
                        location.reload();
                    }
                });
               
            });
        });
    </script>
</asp:Content>
