using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SchemeOfficer_ApprovedRequest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        paymentlist();
    }

    public void paymentlist()
    {
        bal obj = new bal();
        mPaymentList.DataSource = obj.getSchemeWiseApprovedList(this.Session["SchemeRole"].ToString());
        mPaymentList.DataBind();

        StringBuilder htmlTable = new StringBuilder();
        if (mPaymentList.Rows.Count == 0)
        {
            htmlTable.Append("<p style='text-align: center;line-height: 480px;font-size: 1.5em;font-weight: bold;'>NO DATA FOUND</p>");
            litTable.Text = htmlTable.ToString();
        }
        else
        {
            for (int i = 0; i < mPaymentList.Rows.Count; i++)
            {

                var UmIdText = mPaymentList.Rows[i].Cells[0].Text;
                var countText = mPaymentList.Rows[i].Cells[1].Text;
                var totalText = mPaymentList.Rows[i].Cells[2].Text;
                var list = obj.getErrorList(UmIdText);
                var errorCount = list.Count();

                htmlTable.Append("<div class='box' style='border-top: 0;'><div class='box-body' style='padding: 0;'><table id='example2' class='table table-bordered table-hover'><tr style='background-color:#3c8dbc; color: White;'><th>UNIQUE MSG ID.</th><th>COUNT</th><th>TOTAL SUM</th><th>VIEW</th></tr><tr style='font-weight: bold;'><td>" + UmIdText + "</td><td>" + countText + "</td><td>Rs. " + totalText + " /-</td><td><a href ='PaymentDetails.aspx?UmID=" + UmIdText + "' style='padding: 6px;border-radius: 4px;'><img src='../images/view.png' style='height: 25px;'></a></td></tr>");
                if (errorCount > 0)
                {
                    htmlTable.Append("<tr id='rowum'><td colspan='4'><ul style='color: red;font-weight: bold;list-style-type: none;'>");
                    foreach (var mylist in list)
                    {
                        var error = mylist.Error_Msg.ToString();
                        htmlTable.Append("<li>" + error + "</li>");
                    }
                    htmlTable.Append("</ul>");
                }
                htmlTable.Append("</td></tr></table></div></div>");
                litTable.Text = htmlTable.ToString();
            }
        }
    }
}