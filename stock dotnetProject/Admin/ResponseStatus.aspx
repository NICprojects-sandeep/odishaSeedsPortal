<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASAdmin.master" CodeFile="ResponseStatus.aspx.cs" Inherits="ResponseStatus" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="https://ajax.googleapis.com/ajax/libs/angularjs/1.6.4/angular.min.js"></script>
    <style>
        th, td {
           //border: 1px solid;
            text-align:center;
                padding-left: 2.5px;
    padding-right: 2.5px;
        }
    </style>
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <%--<h1>
        Response Status  
          
        <small></small>
      </h1>--%>
        <div class="form-group">
                  <label for="employeeNameText">END to END ID: </label>
                  <input type="text" id="etetext" runat="server" />
            <asp:Button type="submit" class="btn btn-primary" id="endtoendbtn" runat="server" style="margin-right:10px"  Text="Submit" OnClick="endtoendbtn_Click"/>
                </div>
          
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active"> Response Status</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
         
              <asp:GridView ID="mResponseStatus" runat="server" AutoGenerateColumns="false" style="border-collapse:collapse;background: white;" AllowPaging="true" OnPageIndexChanging="OnPaging" PageSize="10">
                   
                  <Columns>
                    <asp:BoundField DataField="Unique_Credit_Transaction_Id" HeaderText="RequestID" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Beneficiary_Name" HeaderText="Beneficiary Name" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Bank_Account_number" HeaderText="Bank Account number" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Total_Payment_Amount" HeaderText="Total Payment Amount" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Record_Status" HeaderText="Record Status" ItemStyle-Width="200px" />
                    <asp:BoundField DataField="Rejection_Narration" HeaderText="Rejection Narration" ItemStyle-Width="200px" />
                  </Columns>

                </asp:GridView>
            <asp:Literal ID="litTable" runat="server" />

        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
    
</asp:Content>

