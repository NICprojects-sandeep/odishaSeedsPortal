<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASScheme.master" CodeFile="ApprovedRequest.aspx.cs" Inherits="SchemeOfficer_ApprovedRequest" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        APPROVED REQUESTS
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active"> Approved Requests</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12">
                <asp:GridView ID="mPaymentList"  runat="server" AutoGenerateColumns="false" CssClass="hidden">
                        <Columns>
                            <asp:BoundField DataField="Unique_Message_Id" HeaderText="UNIQUE MESSEGE ID" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Records_Count" HeaderText="COUNT" ItemStyle-Width="200px" />
                            <asp:BoundField DataField="Control_Sum" HeaderText="TOTAL SUM" ItemStyle-Width="200px" />
                            <asp:ButtonField Text="Select" CommandName="Select" ItemStyle-Width="150" />
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
