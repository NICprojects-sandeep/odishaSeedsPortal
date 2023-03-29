using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_VarietyWithin10Years : System.Web.UI.Page
{
    BLL_DropDown BAL;
    DLL_DropDown DAL;
    DataSet ds;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblAgency.Text = "OSSC";
            lblCrop.Text = "Paddy";
            lblFinYr.Text = "2017-18";
            //lblSeason.Text = "Rabi";        
            FillGrid();
        }
    }

    private void FillGrid()
    {
        BAL = new BLL_DropDown();
        BAL.FIN_YR = "2017-18";
        ds = new DataSet();
        DAL = new DLL_DropDown();

        ds = DAL.FillVarietiesWithIn10Years(BAL);
        gvVarieties.DataSource = ds;
        gvVarieties.DataBind();
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=SeedVarieties.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            gvVarieties.AllowPaging = false;
            this.FillGrid();

            gvVarieties.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in gvVarieties.HeaderRow.Cells)
            {
                cell.BackColor = gvVarieties.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in gvVarieties.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = gvVarieties.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = gvVarieties.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            gvVarieties.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}