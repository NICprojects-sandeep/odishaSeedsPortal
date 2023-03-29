using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

public partial class Masters_Receive_Return : System.Web.UI.Page
{
    BLL_DropDown objUserBEL = new BLL_DropDown();
    DLL_DropDown objUserDLL = new DLL_DropDown();
    DataTable dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        // CompareValidator1.ValueToCompare = DateTime.Now.ToShortDateString();
        if (!IsPostBack)
        {
            FillDropDown();
        }
    }
    private void FillDropDown()
    {
        try
        {
            objUserBEL = new BLL_DropDown();
            objUserBEL.DistCode = Session["distcode"].ToString();
            objUserBEL.UserType = Session["UserType"].ToString();
            objUserDLL = new DLL_DropDown();
            DataTable dt = objUserDLL.FillDdlGodown(objUserBEL);
            ddl_godown.DataSource = dt;
            ddl_godown.DataTextField = "Godown_Name";
            ddl_godown.DataValueField = "Godown_ID";
            ddl_godown.DataBind();
            ddl_godown.Items.Insert(0, new ListItem("--Select GoDown--", "0"));
        }
        catch (Exception exception1)
        {
            ExceptionHandler.WriteEx(exception1);
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (ddl_godown.SelectedIndex == 0)
        {
            string popp = "alert('Select Godown!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            ScriptManager.GetCurrent(Page).SetFocus(ddl_godown);
            return;
        }
        else if (string.IsNullOrWhiteSpace(txtfrmdt.Text))
        {
            string popp = "alert('Enter From Date!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            ScriptManager.GetCurrent(Page).SetFocus(txtfrmdt);
            return;
        }
        else if (string.IsNullOrWhiteSpace(txttodt.Text))
        {
            string popp = "alert('Enter To Date!!!')";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", popp, true);
            ScriptManager.GetCurrent(Page).SetFocus(txttodt);
            return;
        }
        else
        {
            bindgrid();
        }

    }
    public void bindgrid()
    {
        try
        {
            string[] fromdate = txtfrmdt.Text.Split('/');
            string fdate = fromdate[1] + "/" + fromdate[0] + "/" + fromdate[2];
            string[] todate = txttodt.Text.Split('/');
            string tdate = todate[1] + "/" + todate[0] + "/" + todate[2];
            objUserBEL = new BLL_DropDown();
            objUserBEL.Godown_ID = ddl_godown.SelectedValue.ToString();
            objUserBEL.FROM_DATE = fdate;
            objUserBEL.TO_DATE = tdate;
            objUserBEL.UserID = Session["UserID"].ToString();
            objUserBEL.SEASON = "R";
            objUserDLL = new DLL_DropDown();
            DataTable dt = objUserDLL.FillGrdRecv(objUserBEL);
            if (dt.Rows.Count > 0)
            {
                grd_RecvDetails.DataSource = dt;
                grd_RecvDetails.DataBind();

                foreach (GridViewRow rw in grd_RecvDetails.Rows)
                {
                    Label lblrecvnoofbags = rw.Cells[6].Controls[0].FindControl("lblrecvnoofbags") as Label;
                    Label lblavlnoofbags = rw.Cells[7].Controls[0].FindControl("lblavlnoofbags") as Label;
                    Button btndel = rw.Cells[14].Controls[0].FindControl("btndel") as Button;
                    int aa = Convert.ToInt32(lblrecvnoofbags.Text);
                    int bb = Convert.ToInt32(lblavlnoofbags.Text);

                    if (bb >= aa && bb != 0 && aa != 0)
                    {
                        //rw.BackColor = System.Drawing.Color.LemonChiffon;
                        rw.ForeColor = Color.Green;
                        btndel.Enabled = true;
                    }
                    else if (bb == 0 && aa == 0 && bb == aa)
                    {
                        //rw.BackColor = System.Drawing.Color.Azure;
                        rw.ForeColor = Color.Red;
                        btndel.Enabled = false;
                    }
                    else
                    {
                        //rw.BackColor = System.Drawing.Color.Azure;
                        rw.ForeColor = Color.Red;
                        btndel.Enabled = false;
                    }
                }
            }
            else
            {
                string ppp = "alert('No Records Found ! ! !');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
                ddl_godown.SelectedIndex = 0;
                txtfrmdt.Text = "";
                txttodt.Text = "";
                grd_RecvDetails.DataSource = null;
                grd_RecvDetails.DataBind();
            }
        }
        catch (Exception exception1)
        {
            ExceptionHandler.WriteEx(exception1);
        }
    }
    protected void btndel_Click(object sender, EventArgs e)
    {
        try
        {
            Button btndel = (Button)sender;
            GridViewRow Grow = (GridViewRow)btndel.NamingContainer;
            string ReceiveTransID = grd_RecvDetails.DataKeys[Grow.RowIndex].Value.ToString();
            objUserBEL = new BLL_DropDown();
            objUserBEL.RECVTRANSID = ReceiveTransID.ToString();
            objUserBEL.UPDATED_BY =  Session["UserID"].ToString(); //"bibhu";
            objUserBEL.UserIP = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            objUserDLL = new DLL_DropDown();
            int outputvalue = 0;
            objUserDLL.ReturnRecvDtls(objUserBEL, out outputvalue);
            if (outputvalue == 1)
            {
                string ppp = "alert('Cancelled ! ! !');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
                bindgrid();
                //Grow.ForeColor = System.Drawing.Color.LemonChiffon;
            }
            if (outputvalue == 2)
            {
                string ppp = "alert('Cant be Deleted ! ! !');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popp", ppp, true);
            }
        }
        catch (Exception exception1)
        {
            ExceptionHandler.WriteEx(exception1);
        }
    }
    protected void grd_RecvDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_RecvDetails.PageIndex = e.NewPageIndex;
        bindgrid();
    }
    protected void grd_RecvDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //foreach (GridViewRow rw in grd_RecvDetails.Rows)
        //{
        //    Label lblrecvnoofbags = rw.Cells[6].Controls[0].FindControl("lblrecvnoofbags") as Label;
        //    Label lblavlnoofbags = rw.Cells[7].Controls[0].FindControl("lblavlnoofbags") as Label;
        //    Button btndel = rw.Cells[14].Controls[0].FindControl("btndel") as Button;
        //    int aa = Convert.ToInt32(lblrecvnoofbags.Text);
        //    int bb = Convert.ToInt32(lblavlnoofbags.Text);
        //    if (bb >= aa)
        //    {
        //        if(bb==0 && aa ==0)
        //        {
        //            rw.BackColor = System.Drawing.Color.Chocolate;
        //            btndel.Enabled = false;
        //        }
        //        rw.BackColor = System.Drawing.Color.BlanchedAlmond;
        //        btndel.Enabled = true;
        //    }
        //    else
        //    {
        //        rw.BackColor = System.Drawing.Color.Chocolate;
        //        btndel.Enabled = false;
        //    }
        //
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            int rcvbags = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "Recv_No_Of_Bags"));
        //            int avlbags = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "AVL_NO_OF_BAGS"));
        //            Button btndel = e.Row.FindControl("btndel") as Button;
        //            if (rcvbags == 0 && avlbags == 0)
        //            {
        //                e.Row.Attributes["style"] = "color: Chocolate";
        //                btndel.Enabled = false;
        //            }
        //        }
        //    }
        //    catch (Exception exception1)
        //    {
        //        ExceptionHandler.WriteEx(exception1);
        //    }
    }
    protected void txttodt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DateTime.Now < Convert.ToDateTime(txttodt.Text))
            {
                string pop = "alert('Received date Cannot be greater than todays date !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(txttodt);
                txttodt.Text = "";
                return;
            }
        }
        catch (Exception exception1)
        {
            ExceptionHandler.WriteEx(exception1);
        }
    }
    protected void btncan_Click(object sender, EventArgs e)
    {
        ddl_godown.SelectedIndex = 0;
        txtfrmdt.Text = "";
        txttodt.Text = "";
        grd_RecvDetails.DataSource = null;
        grd_RecvDetails.DataBind();
    }
    protected void txtfrmdt_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (DateTime.Now < Convert.ToDateTime(txtfrmdt.Text))
            {
                string pop = "alert('Cannot be greater than todays date !!!');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "pop", pop, true);
                ScriptManager.GetCurrent(Page).SetFocus(txtfrmdt);
                txtfrmdt.Text = "";
                return;
            }
        }
        catch (Exception exception1)
        {
            ExceptionHandler.WriteEx(exception1);
        }

    }
}