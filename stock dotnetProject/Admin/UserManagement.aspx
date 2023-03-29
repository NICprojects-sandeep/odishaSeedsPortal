<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASAdmin.master" CodeFile="UserManagement.aspx.cs" Inherits="Admin_UserManagement" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
  
      <section class="content-header">
      <h1>
        USER MANAGEMENT
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Admin</a></li>
        <li class="active">User Management</li>
      </ol>
    </section>
    <!-- Main content -->
    <section class="content">
      <div class="row">
        <!-- left column -->
        <div class="col-md-6">
          <!-- general form elements -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Add a New NIC user</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <%--<form role="form" runat="server">--%>
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
                  <input type="text" class="form-control" id="systemidText" placeholder="Enter System ID" runat="server" required>
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
                <asp:Button type="submit" class="btn btn-primary" id="add_user_submit" runat="server" onclick="add_user" Text="Submit"/>
              </div>
                  </div>
            <%--</form>--%>
          </div>
          <!-- /.box -->


          <!-- Input addon -->

        </div>
        <!--/.col (left) -->
        <!-- right column -->
         <div class="col-md-6">
          <!-- general form elements -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Example</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->

              <div class="box-body">
                <div class="form-group">
                  <label for="imeiText">IMEI</label>
                  <input type="text" class="form-control" id="Text1" placeholder="Enter IMEI" readonly>
                </div>
                <div class="form-group">
                  <label for="passwordText">Password</label>
                  <input type="password" class="form-control" id="password1" placeholder="Enter Password" readonly>
                </div>
                <div class="form-group">
                  <label for="systemidText">System ID</label>
                  <input type="text" class="form-control" id="Text2" placeholder="Enter System ID" readonly>
                </div>
                <div class="form-group">
                  <label for="employeeNameText">NIC Employee Name</label>
                  <input type="text" class="form-control" id="Text3" placeholder="Enter System ID" readonly>
                </div>
                <div class="form-group">
                  <label for="empoyeecodeText">NIC Employee Code</label>
                  <input type="text" class="form-control" id="Text4" placeholder="Enter NIC Employee Code" readonly>
                </div>
              <!-- /.box-body -->

              <div class="box-footer">
                <button type="submit" class="btn btn-primary" id="Button1" disabled>Submit</button>
              </div>
                  </div>
          </div>
          <!-- /.box -->


          <!-- Input addon -->

        </div>
        <!--/.col (right) -->
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
</asp:Content>
