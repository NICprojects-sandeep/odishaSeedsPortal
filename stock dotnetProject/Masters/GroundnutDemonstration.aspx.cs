using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_GroundnutDemonstration : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            string UserId= Session["UserID"].ToString();
            //Response.Redirect("http://localhost:4300/public/transferOfSeedSubsidy?userID=" + UserId);
            Response.Redirect("http://164.100.140.77/users/#/public/transferOfSeedSubsidy?userID=" + UserId);
        }
    }
}