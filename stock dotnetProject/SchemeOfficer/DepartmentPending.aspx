<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASScheme.master" CodeFile="DepartmentPending.aspx.cs" Inherits="SchemeOfficer_DepartmentPending" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="myModal" class="modal fade" role="dialog">
  <div class="modal-dialog">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Reson of rejection :</h4>
      </div>
      <div class="modal-body" id="dialog">
          <p>
              <input type="hidden" name="UmID" id="hiddenumid">
        <input type="text" class="form-control" name="usr" />
              </p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          <button type="button" class="btn btn-success" id="submit" data-dismiss="modal">Submit</button>
      </div>
    </div>

  </div>
</div>

    <!-- Content Header (Page header) -->
    <section class="content-header">
      <h1>
        PENDING REQUESTS
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active"> Pending Requests</li>
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
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
	    <script type="text/javascript" language="javascript">
        $(function () {
            $('.approve').click(function () {
                $(this).parent().find("img").css("visibility", "visible");
                $(this).parent().find(".approve-icon").css("visibility", "hidden");
                var UmIDa = $(this).attr('value');
                //alert(UmID);
                $.ajax({
                    type: "GET",
                    url: "../SchemeOfficer/DepartmentPending.aspx/approveReq",
                    data: { umid:JSON.stringify(UmIDa) },
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: OnSuccess,
                    failure: function (response) {
                        
                    }
                });
                function OnSuccess(response) {
                    $(this).parent().find("img").css("visibility", "hidden");
                    alert("Messege Id " +UmIDa+ " has been approved for payment.");
                    window.location.reload();
                   
                }
            });
        });
    </script>
    <script>
        jQuery(document).ready(function ($) {
          $.noConflict();
            $(function () {
                $(".reject").click(function (event) {
                    var UmIDa = $(this).attr('value');
                    $('#hiddenumid').val(UmIDa);
                });
                $("#submit").click(function (event) {

                    var text = $('#dialog').find('input[name="usr"]').val();
                    var global = $('#hiddenumid').val();
                   
                    if (text == "") {
                        alert("Try again!");
                    } else {
                   
                        $.ajax({
                            type: "GET",
                            url: "../SchemeOfficer/DepartmentPending.aspx/rejectReq",
                            data: { umid: JSON.stringify(global), res: JSON.stringify(text) },
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: OnSuccess,
                            failure: function (response) {
                                alert(response);
                            }
                        });
                        function OnSuccess(response) {
                            window.location.reload();
                        }
                    }
                    return;
                });
            });
           
        });
        

    </script>

</asp:Content>