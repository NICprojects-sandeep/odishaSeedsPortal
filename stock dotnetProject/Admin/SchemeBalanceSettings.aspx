<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASAdmin.master" CodeFile="SchemeBalanceSettings.aspx.cs" Inherits="Admin_SchemeBalanceSettings" %>


<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <%--Slider used https://github.com/seiyria/bootstrap-slider--%>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.1.1/jquery.min.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <style>
    #ex1Slider .slider-selection {
	    background: #BABABA;
    }
    .slider.slider-horizontal {
    width: 440px !important;
    height: 20px;
}
    </style>
    <section class="content-header">
      <h1>
        Bank Details
        <small></small>
      </h1>
      <ol class="breadcrumb">
        <li><a href="#"><i class="fa fa-dashboard"></i> Home</a></li>
        <li class="active">Bank Details</li>
      </ol>
    </section>
    <!-- Main content -->
    <section class="content">
      <div class="row">
        <div class="col-md-4">
          <!-- general form elements -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title" id="total">Please select a System</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <%--<form role="form" runat="server">--%>
              <div class="box-body">
                <div class="form-group col-md-12">
                  <label for="imeiText">System</label>
                  <select class="form-control" id="system-select">
                      <option>Select System</option>
                  </select>
                </div>


              <!-- /.box-body -->
                 </div>
              
                  
            <%--</form>--%>
          </div>
        </div>

        <div class="col-md-8">
          <!-- general form elements -->
          <div class="box box-primary">
            <div class="box-header with-border">
              <h3 class="box-title">Please adjust the amount</h3>
            </div>
            <!-- /.box-header -->
            <!-- form start -->
            <%--<form role="form" runat="server">--%>
              <div class="box-body" id="list_scheme" style="text-align: right;">
               <div class="form-group col-md-12" style="text-align: center;font-weight: bold;">PLEASE SELECT A SYSTEM TO PROCEEED</div>
              <!-- /.box-body --> 
                 </div>
              <div class="box-footer">
                <a href="#" id="btn_system_select" style="float:right">Select</a>
              </div>
                  
            <%--</form>--%>
          </div>
        </div>

      </div>
      <!-- /.row -->
    </section>
    <!-- /.content -->
    <link rel="stylesheet" href="../Scripts/BSlider/css/bootstrap-slider.css">
    <script src="../Scripts/BSlider/bootstrap-slider.js"></script>

    <script>
        $('#system-select').change(function () {
            $('#list_scheme').empty();
                $(function () {
                    $.ajax({
                        type: "GET",
                        url: "../Admin/SchemeBalanceSettings.aspx/SystemWiseScheme",
                        data: { sysID: JSON.stringify($('#system-select').val()) },
                        contentType: "application/json",
                        dataType: "json",
                        success: OnSuccess,
                        failure: function (response) {
                            alert("No");
                        }
                    });
                    function OnSuccess(response) {

                        var Values = new Array();
                        var slider = new Array();
                        var scode = new Array();
                        for (i = 0; i < response.d.length; i++) {
                            $('#list_scheme').append('<div class="form-group col-md-12"><label style="margin-right: 100px;" for="schemeName-' + response.d[i].SchemeCode + '">' + response.d[i].SchemeName + '</label><input style="margin-left: 62px;" id="ex1-' + response.d[i].SchemeCode + '" data-slider-id="ex1Slider" type="text" data-slider-min="' + response.d[i].ApprovedAmount + '" data-slider-max="' + response.d[i].TotalAmount + '" data-slider-step="1" data-slider-value="' + response.d[i].SlabAmount + '"/></div>')
                            // Call a method on the slider
                            scode[i] = response.d[i].SchemeCode;
                            slider[i] = new Slider('#ex1-' + response.d[i].SchemeCode, {
                                formatter: function (value) {
                                    
                                    return 'Current value: ' + value;
                                }
                            });

                            //slider[i].on("slide", function(slideEvt) {
                            //    Values[i] = slideEvt.value;
                            //    var total = 0;
                            //    for (j = 0; j< response.d.length; j++) {
                            //        total = total + Values[j];
                            //    }
                            //    alert(total);
                            //    $('#total').text(total);
                            //});
                        }

                        $('#btn_system_select').click(function (e) {
                            e.preventDefault();
                            var total = 0;
                            for (i = 0; i < response.d.length; i++) {
                                Values[i] = slider[i].getValue();
                                total = total + Values[i];
                            }
                            
                                if (total > response.d[0].TotalAmount) {
                                    alert("Total Amount must not exceed: " + response.d[0].TotalAmount);
                                } else {
                                    for (i = 0; i < response.d.length; i++) {
                                        $(function () {
                                            $.ajax({
                                                type: "GET",
                                                url: "../Admin/SchemeBalanceSettings.aspx/updateSlabAmount",
                                                data: { schemeCode: JSON.stringify(scode[i]), slab: JSON.stringify(Values[i]) },
                                                contentType: "application/json",
                                                dataType: "json",
                                                success: OnSuccess,
                                                failure: function (response) {
                                                    alert("No");
                                                }
                                            });
                                            function OnSuccess(response) {
                                                
                                            }
                                        })
                                    }
                                    alert("Value has been updated successfully");
                                    location.reload();
                                }
                                
                        })  
                    }
                });
            });
    </script>
</asp:Content>

