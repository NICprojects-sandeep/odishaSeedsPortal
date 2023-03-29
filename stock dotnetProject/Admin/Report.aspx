<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MasterPages/BASAdmin.master" CodeFile="Report.aspx.cs" Inherits="Admin_Report" %>



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
                <div class="col-md-3 col-lg-3 col-sm-3">
                    <select id="systemselect" class="form-control" name="systemselect">
                    </select>
                </div>
                <div class="col-md-3 col-lg-3 col-sm-3" style="padding-top: 3px;">
                    <strong>From Date :</strong><input type="date" id="from" name="frdt" runat="server" />
                </div>

                <div class="col-md-3 col-lg-3 col-sm-3" style="padding-top: 3px;">
                    <strong>To Date :</strong><input type="date" id="todate" name="todt" runat="server" />
                </div>
                <div class="col-md-3 col-lg-3 col-sm-3">
                    <asp:Button ID="Button1" runat="server" Text="Get Ledger" onclick="loadLedger" cssClass="btn btn-primary" />

                    <%--<input type="submit" id="payment" value="Get Ledger" class="btn btn-primary" />--%>
                </div>
            </div>
            <br />
            <%--<div>
                <table>
                    <tr>
                        <td></td>
                        <td><strong>From Date :</strong></td>
                        <td style="padding-left:6px"><input type="date" id="from" name="frdt" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4"><br /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td><strong>To Date :</strong></td>
                        <td style="padding-left:6px"><input type="date" id="todate" name="todt" /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="4"><br /></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td><input type="submit" id="payment" value="Get Ledger" class="btn btn-success" /></td>
                        <td></td>
                    </tr>
                </table>
            </div>--%>
            <br />
            <%--<table style="border:groove">
                <tr style="border:groove">
                    <td style="border:groove">Date</td>
                    <td style="padding-left:10px;border:groove">Cr. Amount</td>
                    <td style="padding-left:10px;border:groove">Dr. Amount</td>
                </tr>
                @foreach (var s in Model)
                {

                    <tr style="border:groove">
                        <td style="border:groove">@s.date.ToString("dd/MM/yyyy")</td>
                        <td style="padding-left:10px;border:groove">@s.crAmt</td>
                        <td style="padding-left:10px;border:groove">@s.drAmt</td>
                    </tr>

                }
            </table>--%>
            <div class="box">
                <div class="box-body">
                      <table id="example1" class="table table-bordered table-striped">
                        <thead>
                        <tr>
                          <th>DATE</th>
                          <th>CR. AMOUNT</th>
                          <th>DR. AMOUNT</th>
                        </tr>
                        </thead>
                        <tbody id="myList">
                
               
                        </tbody>
                      </table>
                </div>
             </div>
        </section>
    </asp:Content>