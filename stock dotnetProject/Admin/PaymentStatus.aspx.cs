using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public partial class PaymentStatus : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        paymentStatusDetails();

    }


    public void paymentStatusDetails()
    {

        List<PStatus> mList = new List<PStatus>();
        bal obj1 = new bal();
        PaymentStatusBal obj = new PaymentStatusBal();
        var Req = obj1.getAllPaymentList();
        var RCount = Req.Sum(s => s.Records_Count).ToString();
        var RAmount = Req.Sum(s => s.Control_Sum).ToString();

        foreach (var rq in Req)
        {

            var bRequests = obj.getPaymentStatus(rq.Unique_Message_Id);

            PStatus mObj = new PStatus();
            mObj.RequestID = rq.Unique_Message_Id;
            mObj.RequestCount = rq.Records_Count.ToString();
            mObj.RequestAmount = rq.Control_Sum.ToString();
            mList.Add(mObj);

        }
        //return mList;

        mPaymentStatus.DataSource = mList;
        mPaymentStatus.DataBind();

        StringBuilder htmlTable = new StringBuilder();
        if (mPaymentStatus.Rows.Count == 0)
        {
            htmlTable.Append("<p style='text-align: center;line-height: 480px;font-size: 1.5em;font-weight: bold;'>NO DATA FOUND</p>");


            litTable.Text = htmlTable.ToString();
        }
        else
        {
            for (int i = 0; i < mPaymentStatus.Rows.Count; i++)
            {

                var RequestID = mPaymentStatus.Rows[i].Cells[0].Text;
                var RequestCount = mPaymentStatus.Rows[i].Cells[1].Text;
                var RequestAmount = mPaymentStatus.Rows[i].Cells[2].Text;
                htmlTable.Append("<div class='row' style='background: white;margin-left: 2px;margin-right: 2px;padding-top: 11px;padding-bottom: 15px;margin-bottom:20px'> <div class='col-md-4'>Msg ID: " + RequestID + "</div> <div class='col-md-4'>Total Count: " + RequestCount + "</div> <div class='col-md-4'>Total Amount: " + RequestAmount + "</div> <div class='col-md-6'> <h4>Bank Wise Request ID</h4> <table style='border:1px solid;width: 100%;'> <tr> <th class='col-md-4'>Msg ID</th> <th class='col-md-4'>Total Count</th> <th class='col-md-4'>Total Amount</th> </tr> ");
                var pDetails = obj.getPaymentStatus(RequestID);
                var pDetails1 = obj.getPaymentStatusDetails(RequestID);
                foreach (var k in pDetails)
                {
                    htmlTable.Append("<tr> <td>" + k.Unique_Message_Id + "</td> <td>" + k.Records_Count + "</td> <td>" + k.Control_Sum + "</td> </tr> ");
                }
                htmlTable.Append("</table ></div><div class='col-md-6'> <h4>Bank Wise Response ID</h4><table style = 'border:1px solid;width: 100%;'><tr><th class='col-md-4'>Msg ID</th><th class='col-md-4'>Total Count</th> <th class='col-md-4'>Total Amount</th> </tr>");
                foreach (var k in pDetails)
                {
                    htmlTable.Append("<tr> <td>" + k.Unique_Message_Id + "</td> <td>" + pDetails1.Where(s => s.Unique_Message_Id == k.Unique_Message_Id && s.Record_Status == "ACCP").Count().ToString() + "</td> <td>" + pDetails1.Where(s => s.Unique_Message_Id == k.Unique_Message_Id && s.Record_Status == "ACCP").Sum(x => x.Total_Payment_Amount).ToString() + "</td> </tr> ");
                }
                htmlTable.Append(" </table> </div> <div class='col-md-3'> <div class='col-md-12'></div> <div class='col-md-12'></div> </div> <div class='col-md-3'> <div class='col-md-12'></div> <div class='col-md-12'></div> </div> </div>");
                litTable.Text = htmlTable.ToString();
            }
        }
    }

    public class PStatus
    {
        public string TotalReqCount { get; set; }
        public string TotalReqAmount { get; set; }
        public string TotalSuccessCount { get; set; }
        public string TotalSuccessAmount { get; set; }
        public string TotalRejectedCount { get; set; }
        public string TotalRejectedAmount { get; set; }
        public string UmID { get; set; }
        public string RequestID { get; internal set; }
        public string RequestCount { get; set; }
        public string RequestAmount { get; set; }
        public object BankReqID { get; internal set; }
    }

}
