using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SchemeOfficer_PaymentDetails : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String UmIDString = Request.QueryString["UmID"];
        paymentDetails(UmIDString);
    }

    public void paymentDetails(String UmID)
    {
        bal obj = new bal();
        //mPaymentDetails.DataSource = obj.getRequestDetailsPage(UmID);
        //mPaymentDetails.DataBind();

        var paymentDetailsList = obj.getRequestDetailsPage(UmID);
        foreach (var items in paymentDetailsList)
        {
            string imp=null;
            if(items.Subimpliment==null)
            {
                imp = items.Impliment;
            }
            else
            {
                imp = items.Impliment + " (" + items.Subimpliment + ")";
            }
            string uuu = Convert.ToString('"') + Convert.ToString("popup") + Convert.ToString('"');
            string tUrl = obj.getTransactionUrl(items.UMID.Substring(0, 4));
            string dist = obj.getDistNameForBeneficiary(items.DistID);
            string block = obj.getBlockNameForBeneficiary(items.BlockID);
            string myScript = "<tr><td>" + items.FarmerID + "</td><td>" + items.BenName + "</td><td>" + items.FatherName + "</td><td>" + dist + "</td><td>" + block + "</td><td>" + items.Account + "</td><td>" + items.Amount + "</td><td>" + items.Gender + "</td><td>" + items.Category + "</td><td>" + items.Capacity + "</td><td>" + imp + "</td></tr>";
            //<a href=" + Convert.ToString('"') + tUrl + items.TransactionID + Convert.ToString('"') + " target=" + uuu + ">" + items.TransactionID + "</a>
            ClientScript.RegisterStartupScript(GetType(), items.UMID + items.TransactionID, "$('#myList').append('" + myScript + "');", true);
        }
    }
}