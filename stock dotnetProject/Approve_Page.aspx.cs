using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;



public partial class Approve_Page : System.Web.UI.Page
{
    stockEntities2 C_Orm = new stockEntities2();
    Complain_Bal C_Bal = new Complain_Bal();
    StockEntities obj = new StockEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
         if (!IsPostBack)
            {
                PageLoad();
            } 
              
         
    }

    protected void btndownload_Click(object sender, EventArgs e)
    {
        GridViewRow grd = (GridViewRow)((Control)sender).Parent.Parent;
        Button Button = new Button();
        Button = (Button)grd.FindControl("btndownload");
        int cno = Convert.ToInt32(Button.CommandArgument);
        //var MVAGUSTA = (from c1 in C_Orm.Complain_Table where c1.Complain_No == cno select c1).FirstOrDefault();
        //string c = "C";
        //MVAGUSTA.Complain_status = c;
        //C_Orm.SaveChanges();
        DownloadFile(cno);
    }

    protected void Btn_Reject_Click(object sender, EventArgs e)
    {
        GridViewRow grd = (GridViewRow)((Control)sender).Parent.Parent;
        Button BReject = new Button();
        BReject = (Button)grd.FindControl("Btn_Reject");
        int Complain_No = Convert.ToInt32(BReject.CommandArgument);
        ViewState["Cno"] = Complain_No;
        var RejectCno = (from c in C_Orm.Complain_Table where c.Complain_No == Complain_No select c).FirstOrDefault();
        if (RejectCno.Complain_status == "C")
        {
            this.mpe1.Show();
        }
        else
        {
            Response.Write("<script>alert('Please Download Complain File....')</script>");
            return;
        }
    }

    protected void btn_RJCT_Click(object sender, EventArgs e)
    {
        try
        {
            if (Txt_Reject.Text != "")
            {
                int CCno = Convert.ToInt32(ViewState["Cno"].ToString());
                var RejectComplain = (from c in C_Orm.Complain_Table where c.Complain_No == CCno select c).FirstOrDefault();
                RejectComplain.Complain_Aproove = "Rejh";
                RejectComplain.Remark = Txt_Reject.Text;
                RejectComplain.Approve_DateTime = System.DateTime.Now;
                C_Orm.SaveChanges();
                string pop = "alert('Complain Rejected!!!...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                PageLoad();
                GrdFetchComplain();
            }
            else
            {
                string pop = "alert('Please Give Reject Reason!!!...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                this.mpe1.Show();
            }
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
    }

    protected void Btn_Resolve_Click(object sender, EventArgs e)
    {
        GridViewRow grd = (GridViewRow)((Control)sender).Parent.Parent;
        Button BReject = new Button();
        BReject = (Button)grd.FindControl("Btn_Resolve");
        int Complain_No = Convert.ToInt32(BReject.CommandArgument);
        var RejectComplain = (from c in C_Orm.Complain_Table where c.Complain_No == Complain_No select c).FirstOrDefault();
        RejectComplain.Complain_Aproove = "RES";
        RejectComplain.Remark = "Complete";
        C_Orm.SaveChanges();
        string pop = "alert('Complain Resolved!!!...');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
        PageLoad();
        GrdFetchComplain();
    }

    protected void grdAdminComplain_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAdminComplain.PageIndex = e.NewPageIndex;
            PageLoad();
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
    }

    public void DownloadFile(int cno)
    {
        try
        {
            {
                var doc = C_Orm.Complain_Table.Where(x => x.Complain_No == cno).FirstOrDefault();

                Byte[] lbytes = (Byte[])doc.Problem_file;
                HttpContext.Current.Response.Buffer = true;

                HttpContext.Current.Response.Charset = "";
                HttpContext.Current.Response.BufferOutput = false;
                HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
                HttpContext.Current.Response.ContentType = doc.File_Type;
                string filename = doc.File_Name.Replace(" ", "_");
                HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename);
                HttpContext.Current.Response.BinaryWrite(lbytes);
                HttpContext.Current.Response.Flush();
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }
        catch (Exception ex)
        {
            //string errorString = ExceptionHandler.CreateErrorMessage(ex);
            //ExceptionHandler.WriteLog(errorString);
            throw ex;
        }
    }

    public void PageLoad()
    {
       
        var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == "Yes" select new { i1.Complain_No, i1.Complain_date, i1.Problem }).ToList();//where i1.Complain_Aproove == "no"
        if (i.Count > 0)
        {
            grdAdminComplain.DataSource = i;
            grdAdminComplain.DataBind();
            GrdFetchComplain();
        }
        else
        {
            grdAdminComplain.DataSource = null;
            grdAdminComplain.DataBind();
        }
    }

    public void GrdFetchComplain()
    {
        foreach (GridViewRow grd in grdAdminComplain.Rows)
        {
            Label l1 = (Label)grd.FindControl("lblcomplaintno");
            int s1 = Convert.ToInt32(l1.Text);
            Label l2 = (Label)grd.FindControl("lblcomplaindate");
            DateTime dt = Convert.ToDateTime(l2.Text);

            //var Audi = (from a1 in C_Orm.Complain_Table where a1.Complain_No == s1 select new { a1.Asco_Code }).FirstOrDefault();
            //string ascocode = Audi.Asco_Code;
            //var Astonmartin = (from b1 in C_Orm.ASCOINFOes where b1.ASCO_CODE == ascocode select new { b1.CERCLE }).FirstOrDefault();
            //Label l3 = (Label)grd.FindControl("ascocircle");
            //if (Astonmartin != null)
            //{
            //    l3.Text = Astonmartin.CERCLE;
            //}
            //else
            //{
            //    string qq = "0";
            //    if (qq == "0")
            //    {
            //        l3.Text = "-NIL-";
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}
            var Audi = (from a1 in C_Orm.Complain_Table where a1.Complain_No == s1 select new { a1.Sco_code }).FirstOrDefault();
            string Scocode = Audi.Sco_code;
            var Astonmartin = (from b1 in obj.Stock_UserProfile where b1.User_Type == "AAOO" && b1.UserId == Scocode select new { b1.FullName }).FirstOrDefault();
            Label l3 = (Label)grd.FindControl("ascocircle");
            if (Astonmartin != null)
            {
                l3.Text = Astonmartin.FullName;
            }
            else
            {
                string qq = "0";
                if (qq == "0")
                {
                    l3.Text = "-NIL-";
                }
                else
                {
                    return;
                }
            }


        }
    }
}