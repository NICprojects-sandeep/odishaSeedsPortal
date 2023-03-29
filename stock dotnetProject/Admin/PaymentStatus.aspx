<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASAdmin.master" CodeFile="PaymentStatus.aspx.cs" Inherits="PaymentStatus" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
<script src="http://code.jquery.com/jquery-latest.js"></script>
<script src="../js/jquery.easyPaginate.js"></script>
    <style>
        th, td {
            border: 1px solid;
            text-align:center;
        }

        .easyPaginateNav {
        margin-left: 15px;
        }

        .page {
        padding-left:2.5px;
        padding-right: 2.5px;
        }
    </style>
    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        Status of Payment Requests
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active"> Inactive Requests</li>
      </ol>
    </section>

    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-xs-12"  id='paginated'>
         
              <asp:GridView ID="mPaymentStatus" runat="server" AutoGenerateColumns="false"  CssClass="hidden">
                   
                  <Columns>

<asp:BoundField DataField="RequestID" HeaderText="RequestID" ItemStyle-Width="200px" />
<asp:BoundField DataField="RequestCount" HeaderText="RequestCount" ItemStyle-Width="200px" />
<asp:BoundField DataField="RequestAmount" HeaderText="RequestAmount" ItemStyle-Width="200px" />
                  </Columns>

                </asp:GridView>
            <asp:Literal ID="litTable" runat="server" />

        </div>
        <!-- /.col -->
      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
<script>
$('#paginated').easyPaginate({
    paginateElement: 'div.row',
    elementsPerPage: 10,

    firstButton:false,
    lastButton:false,

    prevButton:false,
    prevButtonText: '...',
    nextButtonText:'...',
});
</script>
</asp:Content>

