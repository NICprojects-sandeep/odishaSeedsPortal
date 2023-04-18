using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class Farmer_FarmerInquiry : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (txtVoterId.Text.Trim() != "")
        {
            DataSet ds = new DataSet();

            string sql = "FARMER_DATA_INDIVICUAL";
            string[] param = { "@INPUTDATA" };
            object[] value = { txtVoterId.Text.Trim() };
            ds = dbsFarmer.param_passing_fetchDataSetSP(sql, param, value);
            if (ds != null)
            {
                litMsg.Text = string.Empty;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    grdInactive.Visible = false;
                    StringBuilder str = new StringBuilder("");
                    str.Append("<div  style='font-family:Tahoma; font-size:14px; color:red; padding-top:20px;'>");
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        str.Append("<div>Farmer Name - " + dr["VCHFARMERNAME"].ToString() + "</div>");
                        str.Append("<div>Father's/ Secretary/ President Name - " + dr["VCHFATHERNAME"].ToString() + "</div>");

                        str.Append("<div>Rejection Reason - " + dr["status"].ToString() + "</div>");

                    }
                    str.Append("</div>");
                    litMsg.Text = str.ToString();
                }
                else
                {
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        grdInactive.Visible = true;
                        grdInactive.DataSource = ds.Tables[1];
                        grdInactive.DataBind();
                    }
                    else
                    {
                        Session.Remove("VCHVOTERIDCARDNO");
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowMassage", "alert('Data Not Fount against input value !!');", true);
                    }
                }
            }
            else
            {
                Session.Remove("VCHVOTERIDCARDNO");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowMassage", "alert('Invalid Data Entered !');", true);
            }
        }
        else
        {
            Session.Remove("VCHVOTERIDCARDNO");
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "ShowMassage", "alert('Please enter value !');", true);
        }

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((GridViewRow)((Button)sender).NamingContainer);
        HiddenField AutoId = (HiddenField)row.FindControl("hfVid");
        Session["VCHVOTERIDCARDNO"] = AutoId.Value;
     //   Response.Redirect("FarmerIdentification.aspx");
        Response.Write("<script>window.open ('FarmerIdentification.aspx','_blank');</script>");
    }
}