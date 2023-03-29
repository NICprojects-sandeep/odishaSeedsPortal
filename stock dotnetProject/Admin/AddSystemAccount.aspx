<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddSystemAccount.aspx.cs" MasterPageFile="~/MasterPages/BASAdmin.master" Inherits="Admin_AddSystemAccount" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
      <section class="content-header">
      <h1>
        Bank Details
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Admin</a></li>
        <li class="active">Bank Details for a system</li>
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
              <h3 class="box-title">Configure a bank for a System</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <%--<form role="form" runat="server">--%>
              <div class="box-body">
                <div class="form-group col-md-3">
                  <label for="imeiText">System</label>
                  <select class="form-control" id="system-select">
                      <option>Select System</option>
                  </select>
                </div>
                <div class="form-group col-md-3">
                  <label for="passwordText">Bank</label>
                  <select class="form-control" id="bank-select">
                      <option>Select Bank</option>
                  </select>
                </div>
                <div class="form-group col-md-3">
                  <label for="systemidText">Account No</label>
                  <input type="text" class="form-control" id="account-text" placeholder="Enter Account No" required>
                </div>

                <div class="form-group col-md-3">
                  <label for="systemidText">Opening Balance</label>
                  <input type="text" class="form-control" id="obalance-text" placeholder="Enter Opening Balance" required>
                </div>

                <div class="form-group col-md-3">
                  <label for="systemidText">Bank URL</label>
                  <input type="text" class="form-control" id="bank-url" placeholder="Enter Bank URL" required>
                </div>

                <div class="form-group col-md-3">
                  <label for="type-select">Type</label>
                  <select class="form-control" id="type-select">
                      <option value="ftp">FTP</option>
                      <option value="sftp">SFTP</option>
                  </select>
                </div>

                <div class="form-group col-md-3">
                  <label for="username">Username</label>
                  <input type="text" class="form-control" id="username" placeholder="Enter Username" required>
                </div>

                <div class="form-group col-md-3">
                  <label for="password">Password</label>
                  <input type="password" class="form-control" id="password" placeholder="Enter Password" required>
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
              <h3 class="box-title">Systemwise Bank Details</h3>
            </div>
            <!-- /.box-header -->
            <div class="box-body">
              <table id="example1" class="table table-bordered table-striped">
                <thead>
                <tr>
                  <th>SYSTEM ID</th>
                  <th>BANK CODE</th>
                  <th>ACCOUNT NO</th>
                  
                </tr>
                </thead>
                <tbody id="myList">
             
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
                        url: "../Admin/AddSystemAccount.aspx/AddNewSystemAccount",

                        data: {
                            sysID: JSON.stringify($('#system-select').val()),
                            bankID: JSON.stringify($('#bank-select').val()),
                            accountNo: JSON.stringify($('#account-text').val()),
                            oBalance: JSON.stringify($('#obalance-text').val()),
                            bankUrl: JSON.stringify($('#bank-url').val()),
                            type: JSON.stringify($('#type-select').val()),
                            username: JSON.stringify($('#username').val()),
                            password: JSON.stringify($('#password').val())
                            },
                        contentType: "application/json",
                        dataType: "json",
                        success: OnSuccess,
                        failure: function (response) {
                            alert("No");
                        }
                    });
                    function OnSuccess(response) {
                        alert("System Bank Account Added Successfully!");
                        location.reload();
                    }
                });
               
            });
        });
    </script>
</asp:Content>