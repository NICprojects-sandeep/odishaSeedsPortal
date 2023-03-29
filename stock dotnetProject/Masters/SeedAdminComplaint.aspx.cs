using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Masters_SeedAdminComplaint : System.Web.UI.Page
{
    stockEntities2 C_Orm = new stockEntities2();
    StockEntities obj = new StockEntities();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Response.AddHeader("Pragma", "no-cache");
            Response.AddHeader("Cache-Control", "Private,no-cache,must-revalidate,no-store");
            if (!IsPostBack)
            {

                if (Session["ADMIN"] != null)
                {
                    //PageLoad();
                    //GrdFetchComplain();
                }
                else
                {
                    Response.Redirect("~/LogOut.aspx", false);
                    Response.StatusCode = 302;
                }
            }
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
    }


    protected void btnapprove_Click(object sender, EventArgs e)
    {

        GridViewRow grd = (GridViewRow)((Control)sender).Parent.Parent;
        Button B1 = new Button();
        B1 = (Button)grd.FindControl("btnapprove");
        int w = Convert.ToInt32(B1.CommandArgument);
        var Corvett = (from c1 in C_Orm.Complain_Table where c1.Complain_No == w select c1).FirstOrDefault();
        Label l1 = (Label)grd.FindControl("lblcomplaintno");
        l1.Text = Corvett.Complain_No.ToString();
        Label l2 = (Label)grd.FindControl("lblcomplaindate");
        l2.Text = Corvett.Complain_date.ToString();
        Label l3 = (Label)grd.FindControl("ascocircle");
        l3.Text = Corvett.Sco_code;

        Button BReject = new Button();
        BReject = (Button)grd.FindControl("btnapprove");
        int Complain_No = Convert.ToInt32(BReject.CommandArgument);
        ViewState["Cno"] = Complain_No;
        if (Corvett.Complain_status == "C")
        {
            this.ModalPopupExtender2.Show();
            //string yes = "Yes";               ////Recent
            //Corvett.Complain_Aproove = yes;
            //Corvett.Approve_DateTime = System.DateTime.Now;
            //C_Orm.SaveChanges();
            //Response.Write("<script>alert('Succesfully Approved')</script>");
            //PageLoad();
            // GrdFetchComplain();
            
        }
        else
        {
            Response.Write("<script>alert('Please Download Complain File....')</script>");
            return;
        }

    }

    protected void btn_Approve_Click(object sender, EventArgs e)
    {
        try
        {
            if (Text_approve.Text != "")
            {
                int CCno = Convert.ToInt32(ViewState["Cno"].ToString());
                var RejectComplain = (from c in C_Orm.Complain_Table where c.Complain_No == CCno select c).FirstOrDefault();
                RejectComplain.Complain_Aproove = "APP";
                RejectComplain.Remark = Text_approve.Text;
                RejectComplain.Approve_DateTime = System.DateTime.Now;
                C_Orm.SaveChanges();
                string pop = "alert('Complain Approved!!!...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                PageLoad();
                // GrdFetchComplain();
            }
            else
            {
                string pop = "alert('Please Give Approval Reason!!!...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                this.ModalPopupExtender2.Show();
            }

        }

        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }

        //GridViewRow grd = (GridViewRow)((Control)sender).Parent.Parent;
        //Button B1 = new Button();
        //B1 = (Button)grd.FindControl("btnapprove");
        //int w = Convert.ToInt32(B1.CommandArgument);
        //var Corvett = (from c1 in C_Orm.Complain_Table where c1.Complain_No == w select c1).FirstOrDefault();
        //Label l1 = (Label)grd.FindControl("lblcomplaintno");
        //l1.Text = Corvett.Complain_No.ToString();
        //Label l2 = (Label)grd.FindControl("lblcomplaindate");
        //l2.Text = Corvett.Complain_date.ToString();
        //Label l3 = (Label)grd.FindControl("ascocircle");
        //l3.Text = Corvett.Sco_code;

        //string tt = "No";

        //string c = "C";
        //var ford = (from f1 in C_Orm.Complain_Table where f1.Complain_Aproove == yes && f1.Complain_type == c select f1).FirstOrDefault();
        //Corvett.Complain_status =c;
        
        

    }
    protected void btndownload_Click(object sender, EventArgs e)
    {
        GridViewRow grd = (GridViewRow)((Control)sender).Parent.Parent;
        Button Button = new Button();
        Button = (Button)grd.FindControl("btndownload");
        int cno = Convert.ToInt32(Button.CommandArgument);
        var MVAGUSTA = (from c1 in C_Orm.Complain_Table where c1.Complain_No == cno select c1).FirstOrDefault();
        string c = "C";
        MVAGUSTA.Complain_status = c;
        C_Orm.SaveChanges();
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
                RejectComplain.Complain_Aproove = "Rej";
                RejectComplain.Remark = Txt_Reject.Text;
                RejectComplain.Approve_DateTime = System.DateTime.Now;
                C_Orm.SaveChanges();
                string pop = "alert('Complain Rejected!!!...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                PageLoad();
                // GrdFetchComplain();
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

    protected void Ddl_Complain_SelectedIndexChanged(object sender, EventArgs e)
    {


        if (Ddl_Complain.SelectedValue == "1")
        {
            PageLoad();
        }
        else
        {
            AproveBind();
        }
        //else if (Ddl_Complain.SelectedValue == "2")
        //{
        //    Comp_App = "Yes";
        //    AproveBind(Comp_App);
        //    //var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == "Yes" select i1).ToList();
        //    //if (i.Count > 0)
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = i;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //    fetchapprove();
        //    //    foreach (GridViewRow grd in grdAdminApproveComplaint.Rows)
        //    //    {
        //    //        Label l1 = (Label)grd.FindControl("lbl_Apr");
        //    //        l1.Text = "Approved By Admin";
        //    //    }


        //    //}
        //    //else
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = null;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //}

        //}
        //else if (Ddl_Complain.SelectedValue == "3")
        //{

        //    Comp_App = "Res";
        //    AproveBind(Comp_App);
        //    //var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == "Res" select i1).ToList();
        //    //if (i.Count > 0)
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = i;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //    fetchapprove();
        //    //    foreach (GridViewRow grd in grdAdminApproveComplaint.Rows)
        //    //    {
        //    //        Label l1 = (Label)grd.FindControl("lbl_Apr");
        //    //        l1.Text = "Completed";
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = null;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //}
        //}
        //else if (Ddl_Complain.SelectedValue == "4")
        //{

        //    Comp_App = "Rej";
        //    AproveBind(Comp_App);
        //    //var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == "Rej" select i1).ToList();
        //    //if (i.Count > 0)
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = i;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //    fetchapprove();
        //    //    foreach (GridViewRow grd in grdAdminApproveComplaint.Rows)
        //    //    {
        //    //        Label l1 = (Label)grd.FindControl("lbl_Apr");
        //    //        l1.Text = "Rejected By Admin";
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = null;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //}
        //}
        //else if (Ddl_Complain.SelectedValue == "5")
        //{
        //    Comp_App = "Rejh";
        //    AproveBind(Comp_App);
        //    //var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == "Rejh" select i1).ToList();
        //    //if (i.Count > 0)
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = i;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //    fetchapprove();
        //    //    foreach (GridViewRow grd in grdAdminApproveComplaint.Rows)
        //    //    {
        //    //        Label l1 = (Label)grd.FindControl("lbl_Apr");
        //    //        l1.Text = "Rejected By HelpDesk";
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = null;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //}
        //}
        //else if (Ddl_Complain.SelectedValue == "6")
        //{
        //    Comp_App = "Yes";
        //    AproveBind(Comp_App);
        //    //var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == "Yes" select i1).ToList();
        //    //if (i.Count > 0)
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = i;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //    fetchapprove();
        //    //    foreach (GridViewRow grd in grdAdminApproveComplaint.Rows)
        //    //    {
        //    //        Label l1 = (Label)grd.FindControl("lbl_Apr");
        //    //        l1.Text = "Pending By Helpdesk";
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    grdAdminApproveComplaint.DataSource = null;
        //    //    grdAdminApproveComplaint.DataBind();
        //    //}
        //  }
    }

    protected void grdAdminApproveComplaint_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        try
        {
            grdAdminApproveComplaint.PageIndex = e.NewPageIndex;
            AproveBind();
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
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

    public void fetchapprove()
    {
        foreach (GridViewRow grd in grdAdminApproveComplaint.Rows)
        {
            Label l1 = (Label)grd.FindControl("lblcomplainno");
            int s1 = Convert.ToInt32(l1.Text);
            var Audi = (from a1 in C_Orm.Complain_Table where a1.Complain_No == s1 select new { a1.Sco_code }).FirstOrDefault();
            string Scocode = Audi.Sco_code;
            var Astonmartin = (from b1 in obj.Stock_UserProfile where b1.User_Type == "AAOO" && b1.UserId == Scocode select new { b1.FullName }).FirstOrDefault();
            Label l3 = (Label)grd.FindControl("lblascocircle");
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

    public void PageLoad()
    {
        grdAdminApproveComplaint.DataSource = null;
        grdAdminApproveComplaint.DataBind();
        grdAdminApproveComplaint.Visible = false;
        grdAdminComplain.Visible = true;
        //var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == "no" && (i1.Complain_type == "1" || i1.Complain_type == "2") select new { i1.Complain_No, i1.Complain_date, i1.Problem, i1.Problem_file }).ToList();

        var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == "no" select new { i1.Complain_No, i1.Complain_date, i1.Problem, i1.Problem_file }).ToList();
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

    public void AproveBind()
    {
        string Aproove = string.Empty;
        grdAdminComplain.DataSource = null;
        grdAdminComplain.DataBind();
        grdAdminComplain.Visible = false;
        grdAdminApproveComplaint.Visible = true;
        if (Ddl_Complain.SelectedValue == "2")
        {
            Aproove = "Yes";
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

        var i = (from i1 in C_Orm.Complain_Table where i1.Complain_Aproove == Aproove select i1).ToList();
        if (i.Count > 0)
        {
            grdAdminApproveComplaint.DataSource = i;
            grdAdminApproveComplaint.DataBind();
            fetchapprove();
            foreach (GridViewRow grd in grdAdminApproveComplaint.Rows)
            {
                Label l2 = (Label)grd.FindControl("Lbl_compA");
                Label l1 = (Label)grd.FindControl("lbl_Apr");
                if (l2.Text == "Yes")
                    l1.Text = "Approved";
                else if (l2.Text == "Res")
                    l1.Text = "Resolved";
                else if (l2.Text == "Rej")
                    l1.Text = "Rejected By Admin";
                else if (l2.Text == "Rejh")
                    l1.Text = "Rejected By HelpDesk";
            }


        }
        else
        {
            grdAdminApproveComplaint.DataSource = null;
            grdAdminApproveComplaint.DataBind();
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
    protected void grdAdminComplain_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hf = (HiddenField)e.Row.FindControl("hfDownload");
            Button btn = (Button)e.Row.FindControl("btndownload");
            if (hf.Value=="")
            {
                btn.Text = "No File Uploaded";
                btn.Enabled = false;
                btn.Attributes.Add("style","color:red;");
            }
           

        }
    }

    protected void Btn_Return_Click(object sender, EventArgs e)
    {
        GridViewRow grd = (GridViewRow)((Control)sender).Parent.Parent;
        Button BReject = new Button();
        BReject = (Button)grd.FindControl("Btn_Return");
        int Complain_No = Convert.ToInt32(BReject.CommandArgument);
        ViewState["Cno"] = Complain_No;
        var RejectCno = (from c in C_Orm.Complain_Table where c.Complain_No == Complain_No select c).FirstOrDefault();
        if (RejectCno.Complain_status == "C")
        {
            ModalPopupExtender1.Show();
        }
        else
        {
            Response.Write("<script>alert('Please Download Complain File....')</script>");
            return;
        }
    }

    protected void btn_Return_Click(object sender, EventArgs e)
    {
        try
        {
            if (txtReturn.Text != "")
            {
                int CCno = Convert.ToInt32(ViewState["Cno"].ToString());
                var RejectComplain = (from c in C_Orm.Complain_Table where c.Complain_No == CCno select c).FirstOrDefault();
                RejectComplain.Complain_Aproove = "Ret";
                RejectComplain.Remark = txtReturn.Text;
                RejectComplain.Approve_DateTime = System.DateTime.Now;
                C_Orm.SaveChanges();
                string pop = "alert('Complain Returned!!!...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                PageLoad();
                // GrdFetchComplain();
            }
            else
            {
                string pop = "alert('Please Give Return Reason!!!...');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                this.ModalPopupExtender1.Show();
            }
        }
        catch (Exception ee)
        {
            Response.Redirect("~/CustomError/DefaultErrorPage.aspx?Msg=" + ee.Message.ToString().Replace("\r\n", ""));
        }
    }
}