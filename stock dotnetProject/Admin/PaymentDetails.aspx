<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASAdmin.master" CodeFile="PaymentDetails.aspx.cs" Inherits="Admin_PaymentDetails" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <!-- Content Header (Page header) -->
    <%--<section class="content-header">
      <h1>
        PAYMENT DETAILS
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Admin</a></li>
        <li class="active"> Inactive Requests</li>
      </ol>
    </section>--%>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
            <div class="box">
                <div class="box-header">
                  <h3 class="box-title">PAYMENT DETAILS</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                  <table id="example1" class="table table-bordered table-striped">
                    <thead>
                    <tr>
                      <th>FARMER ID</th>
                      <th>BENEFICIARY NAME</th>
                      <th>FATHER's NAME</th>
                      <th>DISTRICT</th>
                      <th>BLOCK</th>
                      <th>ACCOUNT NO</th>
                      <th>AMOUNT</th>
                      <th>TRANSACTION ID</th>
                    </tr>
                    </thead>
                    <tbody id="myList">
                
               
                    </tbody>
                  </table>
                </div>
            <!-- /.box-body -->
            </div>
        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
	

</asp:Content>

