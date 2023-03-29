<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASAdmin.master" CodeFile="DistBankDetails.aspx.cs" Inherits="Admin_DistBankDetails" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
      <section class="content-header">
      <h1>
        BANK DETAILS
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Admin</a></li>
        <li class="active">Bank Details</li>
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
              <h3 class="box-title">Configure a bank for a District</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <%--<form role="form" runat="server">--%>
              <div class="box-body">
                <div class="form-group col-md-4">
                  <label for="imeiText">System</label>
                  <select class="form-control" id="system-select">
                      <option>Select System</option>
                  </select>
                </div>
                <div class="form-group col-md-4">
                  <label for="passwordText">Bank</label>
                  <select class="form-control" id="bank-select">
                      <option>Select Bank</option>
                  </select>
                </div>
                <div class="form-group col-md-4">
                  <label for="systemidText">District</label>
                  <select class="form-control" id="dist-select">
                      <option>Select District</option>
                  </select>
                </div>
              <!-- /.box-body -->
                 </div>
              <div class="box-footer">
                <asp:Button type="submit" class="btn btn-primary" id="add_dist_bank_submit" runat="server" style="float:right"  Text="Submit"/>
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
              <h3 class="box-title">Districtwise Bank Details</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="example1" class="table table-bordered table-striped">
                <thead>
                <tr>
                  <th>SYSTEM ID</th>
                  <th>BANK CODE</th>
                  <th>DISTRICT CODE</th>
                  
                </tr>
                </thead>
                <tbody id="myList">
                
               
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
            $('#<%= add_dist_bank_submit.ClientID %>').click(function () {
                var a = $('#system-select').val();

                $(function () {
                    $.ajax({
                        type: "GET",
                        url: "../Admin/DistBankDetails.aspx/AddDistrictWiseBank",

                        data: { sysID: JSON.stringify($('#system-select').val()), bankID: JSON.stringify($('#bank-select').val()), distCode: JSON.stringify($('#dist-select').val()) },
                        contentType: "application/json",
                        dataType: "json",
                        success: OnSuccess,
                        failure: function (response) {
                            alert("No");
                        }
                    });
                    function OnSuccess(response) {
                        alert("Details added Successfully!");
                        location.reload();
                    }
                });
               
            });
        });
    </script>
</asp:Content>
