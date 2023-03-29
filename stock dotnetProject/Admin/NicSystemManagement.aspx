<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASAdmin.master" CodeFile="NicSystemManagement.aspx.cs" Inherits="Admin_NicSystemManagement" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  
      <section class="content-header">
      <h1>
        NIC SYSTEM MANAGEMENT
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Admin</a></li>
        <li class="active">NIC System Management</li>
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
              <h3 class="box-title">Add a New System</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
              <div class="box-body">
                <div class="col-md-6 form-group">
                  <label for="systemidText">System ID</label>
                  <input type="text" class="form-control" id="systemidText" placeholder="Enter System ID" runat="server" required>
                </div>
                <div class="col-md-6 form-group">
                  <label for="ceilingAmountText">Ceiling Amount</label>
                  <input type="text" class="form-control" id="ceilingAmountText" placeholder="Ceiling Amount" runat="server" required>
                </div>
                <%--<div class="col-md-6 form-group">
                  <label for="nicEmployeeCodeText">NIC Employee Code</label>
                  <input type="text" class="form-control" id="nicEmployeeCodeText" placeholder="Enter NIC Employee Code" runat="server" required>
                </div>--%>

                <div class="col-md-12 form-group">
                  <label for="transactionUrlPathText">Transactions Detail URL</label>
                  <input type="text" class="form-control" id="transactionUrlPathText" placeholder="Enter Transactions Detail URL" runat="server" required>
                </div>
                <div class="col-md-12 form-group" style="border: 1px solid #d2d6de;padding: 18px;margin-left: 15px;margin-right: 16px;width: 97%;">
                  <label for="requestPathText">Request Folder path</label>
                  <input type="text" class="form-control" id="requestPathText" placeholder="Enter Request Folder path" runat="server" required>
                    <div class="radio-inline" id="myRequestType" runat="server">
                      <label><input type="radio" checked = "checked" id="requestlocal" name="local" value="local">Local Folder</label>
                    </div>
                    <div class="radio-inline">
                      <label><input type="radio" id="requestsftp" name="ftp" value="ftp">FTP Folder</label>
                    </div>
                    <div style="display:flex" class="hidden" id="sftprequest">
                        <div style="width: 40%;display:block;margin-right: 3%;">
                            <label for="requestUsernameText">Username</label>
                            <input type="text" class="form-control" id="requestUsernameText" placeholder="User Name" runat="server">
                        </div>
                        <div style="width: 40%;display:block; margin-right: 3%;">
                            <label for="requestPasswordText">Password</label>
                            <input type="password" class="form-control" id="requestPasswordText" placeholder="Password" runat="server">
                        </div>

                        <div style="width: 10%;display:block;">
                            <label for="requestPasswordText">Port No</label>
                            <input type="text" class="form-control" id="reqport" placeholder="Port No" runat="server">
                        </div>
                    </div>
                </div>
                
                <div class="col-md-12 form-group" style="border: 1px solid #d2d6de;padding: 18px;margin-left: 15px;margin-right: 16px;width: 97%;">
                  <label for="bankRequestPath">Bank Request Folder Path</label>
                  <input type="text" class="form-control" id="bankRequestPathText" placeholder="Enter Bank Request Folder Path" runat="server" required>
                 <%--   <div class="radio-inline">
                      <label><input type="radio" checked = "checked" id="banklocal" name="banklocal">Local Folder</label>
                    </div>
                    <div class="radio-inline hidden">
                      <label><input type="radio" id="banksftp" name="banksftp">SFTP Folder</label>
                    </div>
                    <div style="display:flex" class="hidden" id="sftpbank">
                        <div style="width: 47%;display:block;margin-right: 10%;">
                            <label for="bankRequestUsernameText">Username</label>
                            <input type="text" class="form-control" id="bankRequestUsernameText" placeholder="User Name" runat="server">
                        </div>
                        <div style="width: 47%;display:block;">
                            <label for="bankRequestPasswordText">Password</label>
                            <input type="password" class="form-control" id="bankRequestPasswordText" placeholder="Password" runat="server">
                        </div>
                    </div>--%>
                </div>

               
                <!-- /.box-body -->
                <div class="box-footer">
                    <asp:Button type="submit" class="btn btn-primary" runat="server" Text="Submit" OnClick="add_nic_system"></asp:Button>
                </div>
              </div>
            </div>
          <!-- /.box -->


          <!-- Input addon -->
        </div>
        <!--/.col (left) -->
        <!-- right column -->
<%--        <div class="col-md-6">
          <!-- general form elements -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Add a New NIC user</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            
              <div class="box-body">
                <div class="form-group">
                  <label for="imeiText">IMEI</label>
                  <input type="text" class="form-control" id="imeiText" placeholder="Enter IMEI" runat="server" required>
                </div>
                <div class="form-group">
                  <label for="passwordText">Password</label>
                  <input type="password" class="form-control" id="passwordText" placeholder="Enter Password" runat="server" required>
                </div>
                <div class="form-group">
                  <label for="systemidText">System ID</label>
                  <input type="text" class="form-control" id="systemiText" placeholder="Enter System ID" runat="server" required>
                </div>
                <div class="form-group">
                  <label for="employeeNameText">NIC Employee Name</label>
                  <input type="text" class="form-control" id="employeeNameText" placeholder="Enter System ID" runat="server" required>
                </div>
                <div class="form-group">
                  <label for="empoyeecodeText">NIC Employee Code</label>
                  <input type="text" class="form-control" id="empoyeecodeText" placeholder="Enter NIC Employee Code" runat="server" required>
                </div>
              <!-- /.box-body -->

              <div class="box-footer">
                <Button type="submit" class="btn btn-primary" id="add_user_submit" onclick="add_user">Submit</Button>
              </div>
                  </div>

          </div>
          <!-- /.box -->
    <

          <!-- Input addon -->

        </div>--%>
        <!--/.col (right) -->
          
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $("#requestlocal").change(function () {
                if (this.checked) {
                    $("#requestsftp").prop('checked', false);
                    $("#sftprequest").addClass('hidden');
                    $('#<%= requestUsernameText.ClientID %>').prop('required', false);
                    $('#<%= requestPasswordText.ClientID %>').prop('required', false);
                }
            });

            $("#requestsftp").change(function () {
                if (this.checked) {
                    $("#requestlocal").prop('checked', false);
                    $("#sftprequest").removeClass('hidden');
                    $('#<%= requestUsernameText.ClientID %>').attr('required', true);
                    $('#<%= requestPasswordText.ClientID %>').attr('required', true);
                }
            });

           

            $("#banklocal").change(function () {
                if (this.checked) {
                    $("#banksftp").prop('checked', false);
                    $("#sftpbank").addClass('hidden');
                }
            });

            
            $("#banksftp").change(function () {
                if (this.checked) {
                    $("#banklocal").prop('checked', false);
                    $("#sftpbank").removeClass('hidden');
                }
            });
        });
    </script>
</asp:Content>

