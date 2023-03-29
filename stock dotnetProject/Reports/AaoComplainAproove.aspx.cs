using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_AaoComplainAproove : System.Web.UI.Page
{
    stockEntities2 C_Orm = new stockEntities2();
    protected void Page_Load(object sender, EventArgs e)
    {





        Response.AddHeader("Pragma", "no-cache");
        Response.AddHeader("Cache-Control", "Private,no-cache,must-revalidate,no-store");
        try
        {
            if ((this.Session["UserType"].ToString() == "AAOO") || (this.Session["FirstLogin"].ToString() == "N"))
            {

                if (!IsPostBack)
                {

                }

            }
            else
            {
                Response.Redirect("~/LogOut.aspx", false);
                Response.StatusCode = 302;

            }
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
    }

    public void Bindgrid()
    {
        string Aproove = string.Empty;
        string Sco_Id = Session["UserID"].ToString();
        if (Ddl_Complain.SelectedValue == "2")
        {
            Aproove = "Yes";
        }
        else if (Ddl_Complain.SelectedValue == "1")
        {
            Aproove = "No";
        }

        else if (Ddl_Complain.SelectedValue == "3")
        {
            Aproove = "Res";
        }
        else if (Ddl_Complain.SelectedValue == "4")
        {
            Aproove = "Rej";
        }
        else if (Ddl_Complain.SelectedValue == "5")
        {
            Aproove = "Rejh";
        }

        var ComplainAproove = C_Orm.Complain_Table.Where(a => a.Sco_code == Sco_Id && a.Complain_Aproove == Aproove).Select(c => c).ToList();
        if (ComplainAproove.Count > 0)
        {

            Grid_status.DataSource = ComplainAproove;
            Grid_status.DataBind();
            foreach (GridViewRow grd in Grid_status.Rows)
            {
                Label AprooveStatus = (Label)grd.FindControl("lblAproov");
                Label AprooveDate = (Label)grd.FindControl("lblAprooveDate");
                Label AprooveComplainNo = (Label)grd.FindControl("lblcomplainno");
                int AcNo = Convert.ToInt32(AprooveComplainNo.Text);
                var ComplainNoApprove = C_Orm.Complain_Table.Where(a => a.Complain_No == AcNo).Select(b => b).FirstOrDefault();
                if (ComplainNoApprove.Complain_Aproove == "Yes")
                {
                    AprooveStatus.Text = "Aprooved";

                    DateTime dt = Convert.ToDateTime(ComplainNoApprove.Approve_DateTime);
                    AprooveDate.Text = dt.ToString("dd/MM/yyyy");
                }
                else if (ComplainNoApprove.Complain_Aproove == "Rej" || ComplainNoApprove.Complain_Aproove == "Rejh")
                {
                    AprooveStatus.Text = "Rejected";
                    DateTime dt = Convert.ToDateTime(ComplainNoApprove.Approve_DateTime);
                    AprooveDate.Text = dt.ToString("dd/MM/yyyy");
                }
                else if (ComplainNoApprove.Complain_Aproove == "Res")
                {
                    AprooveStatus.Text = "Resolved";
                    DateTime dt = Convert.ToDateTime(ComplainNoApprove.Approve_DateTime);
                    AprooveDate.Text = dt.ToString("dd/MM/yyyy");
                }

                else if (ComplainNoApprove.Complain_Aproove == "No")
                {
                    AprooveStatus.Text = "Registered";
                    AprooveDate.Text = "N/A";
                }
            }
        }
        else
        {
            Grid_status.DataSource = null;
            Grid_status.DataBind();
        }
    }

    protected void Ddl_Complain_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Ddl_Complain.SelectedValue != "0")
        {
            Bindgrid();
        }
    }

    protected void Grid_status_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            Grid_status.PageIndex = e.NewPageIndex;
            Bindgrid();
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
    }
}