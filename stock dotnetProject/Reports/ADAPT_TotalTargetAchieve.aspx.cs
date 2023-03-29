using System;
using System.Web.UI.WebControls;
using System.Data;

public partial class Reports_ADAPT_TotalTargetAchieve : System.Web.UI.Page
{
    BLL_ACC_LYR obj = new BLL_ACC_LYR();
    BLL_Obj bll_obj = new BLL_Obj();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            fillScheme();
            fillImplement();
        }
    }
    protected void fillScheme()
    {
        bll_obj = new BLL_Obj();
        bll_obj.Query = "select * from FMNDBT_SCHEME order by SCHMID";
        DataSet dsScheme = obj.returnDataset(bll_obj);
        if (dsScheme.Tables[0].Rows.Count > 0)
        {

            ddl_scheme.DataSource = dsScheme;
            ddl_scheme.DataTextField = "SCHEME_NAME";
            ddl_scheme.DataValueField = "SCHEME_CODE";
            ddl_scheme.DataBind();
            ddl_scheme.Items.Insert(0, "ALL");
        }
        else
        {
            ddl_scheme.Items.Insert(0, "--select--");
        }
    }
    protected void fillImplement()
    {
        bll_obj = new BLL_Obj();
        bll_obj.Query = "select * from Implement  where Status='A' ";
        DataSet dsImp = obj.returnDataset(bll_obj);
        if (dsImp.Tables[0].Rows.Count > 0)
        {
            drpImplement.DataSource = dsImp;
            drpImplement.DataTextField = "Implement";
            drpImplement.DataValueField = "Impl_id";
            drpImplement.DataBind();
            drpImplement.Items.Insert(0, "--select--");
        }
        else
        {
            drpImplement.Items.Insert(0, "--select--");
        }
    }
    protected void drpImplement_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (drpImplement.SelectedIndex.ToString() != "0")
            {
                ShowData.Visible = true;

                grddisttarget.Visible = true;

                bll_obj = new BLL_Obj();
                DataSet ds = new DataSet();
                bll_obj.Query = "ADAPTGetAchievementAdmin";
                bll_obj.ParamList = new string[] { "@F_YEAR", "@IMPL_ID", "@scheme_code" };
                bll_obj.ParamobjList = new object[] { drpYr.SelectedValue.Trim(), drpImplement.SelectedValue.Trim(), ddl_scheme.SelectedValue.ToString() == "ALL" ? System.Convert.DBNull : ddl_scheme.SelectedValue.ToString() };
                ds = obj.param_passing_fetch_storeprocedure(bll_obj);
                Session["dt"] = ds.Tables[0];
                grddisttarget.DataSource = ds;
                grddisttarget.DataBind();
                btnExport.Visible = true;

                int sumIssuedGen = 0, sumIssuedSC = 0, sumIssuedST = 0, sumIssuedTot = 0, sumSuppliedGen = 0, sumSuppliedSc = 0, sumSuppliedST = 0,
                    sumSuppliedTot = 0, sumPaymentGen = 0, sumPaymentSc = 0, sumPaymentSt = 0, sumPaymentTot = 0;

                int totalIssuedGen = 0, totalIssuedSC = 0, totalIssuedST = 0, totalIssuedTot = 0, totalSuppliedGen = 0, totalSuppliedSc = 0,
                    totalSuppliedST = 0, totalSuppliedTot = 0, totalPaymentGen = 0, totalPaymentSc = 0, totalPaymentSt = 0, totalPaymentTot = 0;

                foreach (GridViewRow gr in grddisttarget.Rows)
                {

                    Label lblsumIssuedGen = (Label)gr.FindControl("lblGeneralTotal");
                    sumIssuedGen = Convert.ToInt32(lblsumIssuedGen.Text);
                    totalIssuedGen = totalIssuedGen + sumIssuedGen;


                    Label lblsumIssuedSC = (Label)gr.FindControl("lblScTotal");
                    sumIssuedSC = Convert.ToInt32(lblsumIssuedSC.Text);
                    totalIssuedSC = totalIssuedSC + sumIssuedSC;

                    Label lblsumIssuedST = (Label)gr.FindControl("lblStTotal");
                    sumIssuedST = Convert.ToInt32(lblsumIssuedST.Text);
                    totalIssuedST = totalIssuedST + sumIssuedST;

                    Label lblsumIssuedTot = (Label)gr.FindControl("lblTotalIssued");
                    sumIssuedTot = Convert.ToInt32(lblsumIssuedTot.Text);
                    totalIssuedTot = totalIssuedTot + sumIssuedTot;

                    Label lblsumSuppliedGen = (Label)gr.FindControl("lblGeneralTSupplied");
                    sumSuppliedGen = Convert.ToInt32(lblsumSuppliedGen.Text);
                    totalSuppliedGen = totalSuppliedGen + sumSuppliedGen;

                    Label lblsumSuppliedSc = (Label)gr.FindControl("lblScTSupplied");
                    sumSuppliedSc = Convert.ToInt32(lblsumSuppliedSc.Text);
                    totalSuppliedSc = totalSuppliedSc + sumSuppliedSc;

                    Label lblsumSuppliedST = (Label)gr.FindControl("lblStTSupplied");
                    sumSuppliedST = Convert.ToInt32(lblsumSuppliedST.Text);
                    totalSuppliedST = totalSuppliedST + sumSuppliedST;

                    Label lblsumSuppliedTot = (Label)gr.FindControl("lblTotalSupplied");
                    sumSuppliedTot = Convert.ToInt32(lblsumSuppliedTot.Text);
                    totalSuppliedTot = totalSuppliedTot + sumSuppliedTot;

                    Label lblsumPaymentGen = (Label)gr.FindControl("lblGeneralTPayment");
                    sumPaymentGen = Convert.ToInt32(lblsumPaymentGen.Text);
                    totalPaymentGen = totalPaymentGen + sumPaymentGen;

                    Label lblsumPaymentSc = (Label)gr.FindControl("lblScTPayment");
                    sumPaymentSc = Convert.ToInt32(lblsumPaymentSc.Text);
                    totalPaymentSc = totalPaymentSc + sumPaymentSc;

                    Label lblsumPaymentSt = (Label)gr.FindControl("lblStTPayment");
                    sumPaymentSt = Convert.ToInt32(lblsumPaymentSt.Text);
                    totalPaymentSt = totalPaymentSt + sumPaymentSt;

                    Label lblsumPaymentTot = (Label)gr.FindControl("lblTotalPayment");
                    sumPaymentTot = Convert.ToInt32(lblsumPaymentTot.Text);
                    totalPaymentTot = totalPaymentTot + sumPaymentTot;



                }
                Label lblGeneralTotalFooter = (Label)grddisttarget.FooterRow.FindControl("lblGeneralTotalFooter");
                Label lblScTotalFooter = (Label)grddisttarget.FooterRow.FindControl("lblScTotalFooter");
                Label lblStTotalFooter = (Label)grddisttarget.FooterRow.FindControl("lblStTotalFooter");
                Label lblTotalIssuedFooter = (Label)grddisttarget.FooterRow.FindControl("lblTotalIssuedFooter");
                Label lblGeneralTSuppliedFooter = (Label)grddisttarget.FooterRow.FindControl("lblGeneralTSuppliedFooter");
                Label lblScTSuppliedFooter = (Label)grddisttarget.FooterRow.FindControl("lblScTSuppliedFooter");
                Label lblStTSuppliedFooter = (Label)grddisttarget.FooterRow.FindControl("lblStTSuppliedFooter");
                Label lblTotalSuppliedFooter = (Label)grddisttarget.FooterRow.FindControl("lblTotalSuppliedFooter");
                Label lblGeneralTPaymentFooter = (Label)grddisttarget.FooterRow.FindControl("lblGeneralTPaymentFooter");
                Label lblScTPaymentFooter = (Label)grddisttarget.FooterRow.FindControl("lblScTPaymentFooter");
                Label lblStTPaymentFooter = (Label)grddisttarget.FooterRow.FindControl("lblStTPaymentFooter");
                Label lblTotalPaymentFooter = (Label)grddisttarget.FooterRow.FindControl("lblTotalPaymentFooter");

                lblGeneralTotalFooter.Text = totalIssuedGen.ToString();
                lblScTotalFooter.Text = totalIssuedSC.ToString();
                lblStTotalFooter.Text = totalIssuedST.ToString();
                lblTotalIssuedFooter.Text = totalIssuedTot.ToString();
                lblGeneralTSuppliedFooter.Text = totalSuppliedGen.ToString();
                lblScTSuppliedFooter.Text = totalSuppliedSc.ToString();
                lblStTSuppliedFooter.Text = totalSuppliedST.ToString();
                lblTotalSuppliedFooter.Text = totalSuppliedTot.ToString();
                lblGeneralTPaymentFooter.Text = totalPaymentGen.ToString();
                lblScTPaymentFooter.Text = totalPaymentSc.ToString();
                lblStTPaymentFooter.Text = totalPaymentSt.ToString();
                lblTotalPaymentFooter.Text = totalPaymentTot.ToString();


            }

        }
        catch (Exception ex)
        {

        }
    }
    protected void drpYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        drpImplement.SelectedIndex = 0;
        ShowData.Visible = false;

    }

    protected void ddl_scheme_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {

            drpImplement_SelectedIndexChanged(null, null);

        }
        catch (Exception ex)
        {

        }
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string csv = string.Empty;
            DataTable dt = (DataTable)Session["dt"];
            foreach (DataColumn column in dt.Columns)
            {
                //Add the Header row for CSV file.
                csv += column.ColumnName + ',';
            }

            //Add new line.
            csv += "\r\n";

            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Data rows.
                    csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                }

                //Add new line.
                csv += "\r\n";
            }

            //Download the CSV file.
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=" + drpImplement.SelectedItem.Text + "" + drpYr.SelectedValue.Trim() + ".csv");
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.Output.Write(csv);
            Response.Flush();
            Response.End();


        }
        catch (Exception ex) { }
    }
}