using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Reports_OverAllReport : System.Web.UI.Page
{
    //protected void Page_Load(object sender, EventArgs e)
    //{
    //    if (!IsPostBack)
    //    {
    //        DataSet ds = new DataSet();
    //        string sql = "select count(*) as nos from [dbo].[M_FARMER_REGISTRATION]";
    //        ds = dbsFarmer.param_passing_fetchDataSet_new(sql);
    //        HyperLink1.Text = ds.Tables[0].Rows[0]["nos"].ToString();
    //        DataSet ds1 = new DataSet();
    //        string sql1 = "select count(*) as nos from [dbo].[M_FARMER_REGISTRATION] where INTREJECTED=1 or INTDELETEDFLAG=1";
    //        ds1 = dbsFarmer.param_passing_fetchDataSet_new(sql1);
    //        HyperLink2.Text = ds1.Tables[0].Rows[0]["nos"].ToString();
    //        DataSet ds2 = new DataSet();
    //        string sql2 = "select count(*) as nos from  [dbo].[Tbl_FarmerApprove] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.Farmer_Code=b.VCHFARMERCODE where a.Status='ACCP' or a.Status='CCB' and b.INTREJECTED=0 and b.INTDELETEDFLAG=0";
    //        ds2 = dbsFarmer.param_passing_fetchDataSet_new(sql2);
    //        HyperLink3.Text = ds2.Tables[0].Rows[0]["nos"].ToString();
    //        DataSet ds3 = new DataSet();
    //        string sql3 = "select (select count(*) from [dbo].[M_FARMER_REGISTRATION])-(select count(*) from [dbo].[M_FARMER_REGISTRATION] where INTREJECTED=1 or INTDELETEDFLAG=1)-( select count(*) from  [dbo].[Tbl_FarmerApprove] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.Farmer_Code=b.VCHFARMERCODE where a.Status='ACCP' or a.Status='CCB' and b.INTREJECTED=0 and b.INTDELETEDFLAG=0) as tot";
    //        ds3 = dbsFarmer.param_passing_fetchDataSet_new(sql3);
    //        Label1.Text = ds3.Tables[0].Rows[0]["tot"].ToString();
    //        DataSet ds4 = new DataSet();
    //        string sql4 = "select count(*) as tot from  [dbo].[Response_CCB_Rjct] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.VCHFARMERCODE=b.VCHFARMERCODE where b.INTREJECTED=0 and b.INTDELETEDFLAG=0";
    //        ds4 = dbsFarmer.param_passing_fetchDataSet_new(sql4);
    //        LinkButton1.Text = ds4.Tables[0].Rows[0]["tot"].ToString();

    //        DataSet ds5 = new DataSet();
    //        string sql5 = "select count(*) as tot from((select a.VCHFARMERCODE as x from [dbo].[M_FARMER_REGISTRATION] a   where a.INTDELETEDFLAG=0 and a.INTREJECTED=0 and a.INTBANKID in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%') except (select Scheme_Specific_ID from [dbo].[Response_CCB1])) except(select VCHFARMERCODE from [dbo].[Response_CCB_Rjct])) as x";
    //        ds5 = dbsFarmer.param_passing_fetchDataSet_new(sql5);
    //        LinkButton2.Text = ds5.Tables[0].Rows[0]["tot"].ToString();

    //        DataSet ds6 = new DataSet();
    //        string sql6 = "select count(*) as tot from M_FARMER_REGISTRATION where INTBANKID in (31,39,61,63,66,69)";
    //        ds6 = dbsFarmer.param_passing_fetchDataSet_new(sql6);
    //        LinkButton3.Text = ds6.Tables[0].Rows[0]["tot"].ToString();

    //        DataSet ds7 = new DataSet();
    //        string sql7 = "select  count(*) as tot from (select a.Scheme_Specific_ID,b.INTBANKID from Response_tbl_Beneficiary_Ack_Message a inner join M_FARMER_REGISTRATION b on a.Scheme_Specific_ID=b.VCHFARMERCODE where b.INTDELETEDFLAG=0 and b.INTREJECTED=0 and a.Beneficiary_Status='RJCT'and b.INTBANKID  not in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%')) as x where INTBANKID  not in (54,20,65)";
    //        ds7 = dbsFarmer.param_passing_fetchDataSet_new(sql7);
    //        LinkButton4.Text = ds7.Tables[0].Rows[0]["tot"].ToString();

    //        DataSet ds8 = new DataSet();
    //        string sql8 = "Total_Rec";
    //        string[] param = { };
    //        object[] value = { };
    //        ds8 = dbsFarmer.param_passing_fetchDataSetSP(sql8, param, value);

    //        LinkButton5.Text = ds8.Tables[0].Rows[0]["tot"].ToString();

    //        DataSet ds9 = new DataSet();
    //        string sql9 = "select ((select count(*)  from (select [Scheme_Specific_ID] from [dbo].[Response_CCB1] except(select Farmer_Code from  [dbo].[Tbl_FarmerApprove] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.Farmer_Code=b.VCHFARMERCODE where ( a.Status='CCB') and b.INTREJECTED=0 and b.INTDELETEDFLAG=0)) as x)+ (select count(*) from (select [Scheme_Specific_ID]  from [dbo].[Response_tbl_Beneficiary_Ack_Message] where Beneficiary_Status='ACCP' except(select Farmer_Code from  [dbo].[Tbl_FarmerApprove] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.Farmer_Code=b.VCHFARMERCODE where (a.Status='ACCP' ) and b.INTREJECTED=0 and b.INTDELETEDFLAG=0)) as y)) as tot";
    //        ds9 = dbsFarmer.param_passing_fetchDataSet_new(sql9);
    //        LinkButton6.Text = ds9.Tables[0].Rows[0]["tot"].ToString();

    //        DataSet ds10 = new DataSet();
    //        string sql10 = "select count(*) as tot from M_FARMER_REGISTRATION where INTBANKID in (54,20,65) and INTDELETEDFLAG=0 and INTREJECTED=0";
    //        ds10 = dbsFarmer.param_passing_fetchDataSet_new(sql10);
    //        LinkButton7.Text = ds10.Tables[0].Rows[0]["tot"].ToString();
    //    }
    //}
    //protected void HyperLink1_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("FarmerStatistics.aspx?id=1");
    //}
    //protected void HyperLink2_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("FarmerStatistics.aspx?id=2");
    //}
    //protected void HyperLink3_Click(object sender, EventArgs e)
    //{
    //    Response.Redirect("FarmerStatistics.aspx?id=3");
    //}
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataSet ds = new DataSet();
            string sql = "select count(*) as nos from [dbo].[M_FARMER_REGISTRATION]";
            ds = dbsFarmer.param_passing_fetchDataSet_new(sql);
            HyperLink1.Text = ds.Tables[0].Rows[0]["nos"].ToString();
            DataSet ds1 = new DataSet();
            string sql1 = "select count(*) as nos from [dbo].[M_FARMER_REGISTRATION] where INTREJECTED=1 or INTDELETEDFLAG=1";
            ds1 = dbsFarmer.param_passing_fetchDataSet_new(sql1);
            HyperLink2.Text = ds1.Tables[0].Rows[0]["nos"].ToString();
            DataSet ds2 = new DataSet();
            string sql2 = "select count(*) as nos from  [dbo].[Tbl_FarmerApprove] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.Farmer_Code=b.VCHFARMERCODE where (a.Status='ACCP' or a.Status='CCB' or a.Status='AAO' or a.Status='AAO1'or a.Status='UIID') and b.INTREJECTED=0 and b.INTDELETEDFLAG=0";
            ds2 = dbsFarmer.param_passing_fetchDataSet_new(sql2);
            HyperLink3.Text = ds2.Tables[0].Rows[0]["nos"].ToString();
            DataSet ds3 = new DataSet();
            string sql3 = "select (select count(*) from [dbo].[M_FARMER_REGISTRATION])-(select count(*) from [dbo].[M_FARMER_REGISTRATION] where INTREJECTED=1 or INTDELETEDFLAG=1)-( select count(*) from  [dbo].[Tbl_FarmerApprove] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.Farmer_Code=b.VCHFARMERCODE where (a.Status='ACCP' or a.Status='CCB' or a.Status='AAO' or a.Status='AAO1'or a.Status='UIID') and b.INTREJECTED=0 and b.INTDELETEDFLAG=0) as tot";
            ds3 = dbsFarmer.param_passing_fetchDataSet_new(sql3);
            Label1.Text = ds3.Tables[0].Rows[0]["tot"].ToString();
            DataSet ds4 = new DataSet();
            string sql4 = "select count(*) as tot from (select a.VCHFARMERCODE as tot from  [dbo].[Response_CCB_Rjct] a inner join [dbo].[M_FARMER_REGISTRATION] b on a.VCHFARMERCODE=b.VCHFARMERCODE where b.INTREJECTED=0 and b.INTDELETEDFLAG=0 except select Farmer_Code from Tbl_FarmerApprove where Status='ACCP' or Status='CCB' or Status='AAO' or Status='AAO1' or Status='UIID') as x";
            ds4 = dbsFarmer.param_passing_fetchDataSet_new(sql4);
            LinkButton1.Text = ds4.Tables[0].Rows[0]["tot"].ToString();

            DataSet ds5 = new DataSet();
            string sql5 = "select count(*) as tot from((select a.VCHFARMERCODE as x from [dbo].[M_FARMER_REGISTRATION] a   where a.INTDELETEDFLAG=0 and a.INTREJECTED=0 and a.INTBANKID in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%') except (select Scheme_Specific_ID from [dbo].[Response_CCB1])) except(select VCHFARMERCODE from [dbo].[Response_CCB_Rjct])) as x";
            ds5 = dbsFarmer.param_passing_fetchDataSet_new(sql5);
            LinkButton2.Text = ds5.Tables[0].Rows[0]["tot"].ToString();

            DataSet ds6 = new DataSet();
            string sql6 = "select count(*) as tot from M_FARMER_REGISTRATION where INTBANKID in (31,39,61,63,66,69)";
            ds6 = dbsFarmer.param_passing_fetchDataSet_new(sql6);
            LinkButton3.Text = ds6.Tables[0].Rows[0]["tot"].ToString();

            DataSet ds7 = new DataSet();
            string sql7 = "select count(*) as tot from(select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in (select  distinct(Scheme_Specific_ID) from (select a.Scheme_Specific_ID,b.INTBANKID from Response_tbl_Beneficiary_Ack_Message a inner join M_FARMER_REGISTRATION b on a.Scheme_Specific_ID=b.VCHFARMERCODE where b.INTDELETEDFLAG=0 and b.INTREJECTED=0 and a.Beneficiary_Status='RJCT' group by a.Scheme_Specific_ID,b.INTBANKID ) as x) and INTBANKID  not in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%') and INTBANKID  not in (54,20,65) except (select VCHFARMERCODE from M_FARMER_REGISTRATION where VCHFARMERCODE in (select  distinct(Scheme_Specific_ID) from (select a.Scheme_Specific_ID,b.INTBANKID from Response_tbl_Beneficiary_Ack_Message a inner join M_FARMER_REGISTRATION b on a.Scheme_Specific_ID=b.VCHFARMERCODE where b.INTDELETEDFLAG=0 and b.INTREJECTED=0 and a.Beneficiary_Status='ACCP' ) as x) and INTBANKID  not in (select Bank_Code from Tbl_Bank where Bank_Name like '%CCB%'or Bank_Name like '%CENTRAL COOP%' or Bank_Name like '%OSCB%') and INTBANKID  not in (54,20,65))) as x";
            ds7 = dbsFarmer.param_passing_fetchDataSet_new(sql7);
            LinkButton4.Text = ds7.Tables[0].Rows[0]["tot"].ToString();

            DataSet ds8 = new DataSet();
            string sql8 = "Total_Rec";
            string[] param = { };
            object[] value = { };
            ds8 = dbsFarmer.param_passing_fetchDataSetSP(sql8, param, value);

            LinkButton5.Text = ds8.Tables[0].Rows[0]["tot"].ToString();

            DataSet ds9 = new DataSet();
            string sql9 = "select((select count(VCHFARMERCODE) from M_FARMER_REGISTRATION where VCHFARMERCODE in(select distinct(Scheme_Specific_ID) from [dbo].[Response_tbl_Beneficiary_Ack_Message] a inner join [M_FARMER_REGISTRATION] b on a.Scheme_Specific_ID=b.VCHFARMERCODE where   Beneficiary_Status='ACCP'  and b.INTDELETEDFLAG=0 and b.INTREJECTED=0 except(select Farmer_Code from Tbl_FarmerApprove where Status='ACCP' or Status='CCB' or Status='AAO' or Status='AAO1'or Status='UIID'))) + (select count(VCHFARMERCODE) from M_FARMER_REGISTRATION where VCHFARMERCODE in(select distinct(Scheme_Specific_ID) from [dbo].[Response_CCB1]a inner join [M_FARMER_REGISTRATION] b on a.Scheme_Specific_ID=b.VCHFARMERCODE except(select Farmer_Code from Tbl_FarmerApprove where Status='ACCP' or Status='CCB' or Status='AAO' or Status='AAO1'or Status='UIID')))) as tot";
            ds9 = dbsFarmer.param_passing_fetchDataSet_new(sql9);
            LinkButton6.Text = ds9.Tables[0].Rows[0]["tot"].ToString();

            DataSet ds10 = new DataSet();
            string sql10 = "select count(*) as tot from M_FARMER_REGISTRATION where INTBANKID in (54,20,65) and INTDELETEDFLAG=0 and INTREJECTED=0";
            ds10 = dbsFarmer.param_passing_fetchDataSet_new(sql10);
            LinkButton7.Text = ds10.Tables[0].Rows[0]["tot"].ToString();
        }
    }
    protected void HyperLink1_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=1");
    }
    protected void HyperLink2_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=2");
    }
    protected void HyperLink3_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=3");
    }
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=4");
    }
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=5");
    }
    protected void LinkButton3_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=6");
    }
    protected void LinkButton4_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=7");
    }
    protected void LinkButton5_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=8");
    }
    protected void LinkButton6_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=9");
    }
    protected void LinkButton7_Click(object sender, EventArgs e)
    {
        Response.Redirect("FarmerStatistics.aspx?id=10");
    }
}