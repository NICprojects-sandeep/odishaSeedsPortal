using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class SCO_Default : System.Web.UI.Page
{
    stockEntities2 C_Orm = new stockEntities2();
    Complain_Bal C_Bal = new Complain_Bal();
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
                    BindComplainType();
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
    public void BindComplainType()
    {
        var C_Type = C_Bal.GetAllComplainType();
        Ddl_ComplainType.DataSource = C_Type;
        Ddl_ComplainType.DataTextField = "Complain_Type";
        Ddl_ComplainType.DataValueField = "Complain_type_Id";
        Ddl_ComplainType.DataBind();
        ListItem lsTp = new ListItem("-Select-", "0");
        Ddl_ComplainType.Items.Insert(0, lsTp);
    }
    protected void Btn_Submit_Click(object sender, EventArgs e)
    {
        if (Txt_Problem.Text != "" && Ddl_ComplainType.SelectedValue != "0")
        {
            if (FileUpload1.HasFile != false || FileUpload1.PostedFile.ContentLength < 4242880)
            {
                string ScoCode = Session["UserID"].ToString();
                string CProblem = Txt_Problem.Text;
                byte[] imageBytes = (byte[])null;
                var filetype=(String)null;
                var filename = (String)null;
                if (FileUpload1.HasFile != false)
                {
                    imageBytes = new byte[FileUpload1.PostedFile.InputStream.Length + 1];
                    FileUpload1.PostedFile.InputStream.Read(imageBytes, 0, imageBytes.Length);
                    filetype = FileUpload1.FileName.Substring(FileUpload1.FileName.LastIndexOf('.'));
                    filename = FileUpload1.FileName;
                }
               
                Complain_Table Ct = new Complain_Table();
                Ct.Sco_code = ScoCode; Ct.Complain_date = System.DateTime.Now; Ct.Complain_status = "P"; Ct.Complain_type = Ddl_ComplainType.SelectedValue;
                Ct.Complain_Aproove = "Pen"; Ct.Problem = CProblem; Ct.Problem_file = imageBytes; Ct.File_Type = filetype; Ct.File_Name = filename;
                if (FileUpload1.HasFile == false)
                {
                    Ct.Complain_status = "C";
                }
                C_Bal.AddToComplain(Ct);
                string ComplainNoo = C_Bal.GetSCOComplainNo(ScoCode).ToString();
                LblComplain.Text = ComplainNoo;
                this.mpe1.Show();
                Reset();
            }

            else
            {
                Response.Write("<script>alert('Please Upload File Or File Size Must Be Less Than 3 mb ! ')</script>");
            }
        }
        else
        {
            Response.Write("<script>alert('Please Select Complain Type:')</script>");
        }
    }

    public void Reset()
    {
        Ddl_ComplainType.SelectedIndex = 0;
        Txt_Problem.Text = "";
    }
}