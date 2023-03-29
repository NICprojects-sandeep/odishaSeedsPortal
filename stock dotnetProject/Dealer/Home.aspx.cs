using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.IO;
using System.Data;

public partial class Dealer_Home : System.Web.UI.Page
{
    BLL_Dealer objUserBELDIST;
    DLL_Dealer objUserDLLDIST;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillDealerPrebooking();
            FillFarmerPrebooking();
        }
    }
    private void FillDealerPrebooking()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserBELDIST.USER_TYPE = "D";
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetPrebooking(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder str = new StringBuilder("");
                str.Append("<div> Pre-Booking details of the dealer");
                str.Append("<table width='100%' border='1' cellpadding='5' cellspacing='0' class='table table-bordered'>");
                str.Append("<tr>");
                str.Append("<td><b>Crop Name</b></td>");
                str.Append("<td><b>Variety Name</b></td>");
                str.Append("<td><b>Qty(in qtl)</b></td>");
                str.Append("</tr>");
                int i = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str.Append("<tr>");
                    str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
                    str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
                    str.Append("<td>" + dr["QUANTITY"].ToString() + "</td>");
                    str.Append("</tr>");
                    i++;
                }
                str.Append("</table>");
                str.Append("</div>");
                litDealer.Text = str.ToString();
            }
        }
    }

    private void FillFarmerPrebooking()
    {
        objUserBELDIST = new BLL_Dealer();
        objUserBELDIST.LICENCENO = Session["LIC_NO"].ToString();
        objUserBELDIST.USER_TYPE = "F";
        objUserDLLDIST = new DLL_Dealer();
        ds = new DataSet();
        ds = objUserDLLDIST.GetPrebooking(objUserBELDIST);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                StringBuilder str = new StringBuilder("");
                str.Append("<div> Pre-Booking details of farmers");
                str.Append("<table width='100%' border='1' cellpadding='5' cellspacing='0' class='table table-bordered'>");
                str.Append("<tr>");
                str.Append("<td><b>Crop Name</b></td>");
                str.Append("<td><b>Variety Name</b></td>");
                str.Append("<td><b>Qty(in qtl)</b></td>");
                str.Append("</tr>");
                int i = 1;
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    str.Append("<tr>");
                    str.Append("<td>" + dr["Crop_Name"].ToString() + "</td>");
                    str.Append("<td>" + dr["Variety_Name"].ToString() + "</td>");
                    str.Append("<td>" + dr["QUANTITY"].ToString() + "</td>");
                    str.Append("</tr>");
                    i++;
                }
                str.Append("</table>");
                str.Append("</div>");
                litFarmer.Text = str.ToString();
            }
        }
    }
}