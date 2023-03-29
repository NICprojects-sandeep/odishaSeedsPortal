using System;
using System.Collections;
using Tamir.SharpSsh;
using System.Data;

public partial class SftpUpload : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }
    public ArrayList GetFilelist(string url, string user, string pass, string path)
    {
        string _ftpURL = url; //Host URL or address of the SFTP server"10.160.34.192"
        string _UserName = user;     //User Name of the SFTP server"cpsms.DOAO"
        string _Password = pass;  //Password of the SFTP server"D0@0@1155"
        int _Port = 22;                 //Port No of the SFTP server (if any)
        string _ftpDirectory =  path; //The directory in SFTP server where the files are presentPaymentData/FromCPSMS
        Sftp oSftp = new Sftp(_ftpURL, _UserName, _Password);
        oSftp.Connect(_Port);
        ArrayList FileList = oSftp.GetFileList(_ftpDirectory);
        oSftp.Close();
        return FileList;
    }
    //public ArrayList GetToFilelist()
    //{
    //    string _ftpURL = txt_url.Text.ToString(); //Host URL or address of the SFTP server
    //    string _UserName = txt_user.Text.ToString();     //User Name of the SFTP server
    //    string _Password = txt_pass.Text.ToString();  //Password of the SFTP server
    //    int _Port = 22;                 //Port No of the SFTP server (if any)
    //    string _ftpDirectory = "PaymentData/'" + txt_to.Text.ToString() + "'"; //The directory in SFTP server where the files are present
    //    Sftp oSftp = new Sftp(_ftpURL, _UserName, _Password);
    //    oSftp.Connect(_Port);
    //    ArrayList FileList = oSftp.GetFileList(_ftpDirectory);
    //    oSftp.Close();
    //    return FileList;
    //}

    public DataTable bankTo(string url, string user, string pass, string path)
    {
        DataTable table = new DataTable("table");
        DataColumn column = new DataColumn();
        DataRow row;
        table.Columns.Add("Request", typeof(string));
        foreach (String item in GetFilelist(url, user, pass, path))
        {
            row = table.NewRow();
            row["Request"] = item;
            table.Rows.Add(row);
        }
        return table;
    }
    public DataTable bankFrom(string url, string user, string pass, string path)
    {
        DataTable table = new DataTable("table");
        DataColumn column = new DataColumn();
        DataRow row;
        table.Columns.Add("Response", typeof(string));
        foreach (String item in GetFilelist(url, user, pass, path))
        {
            row = table.NewRow();
            row["Response"] = item;
            table.Rows.Add(row);
        }
        return table;
    }

    protected void btn_show_Click(object sender, EventArgs e)
    {
        try
        {
            string url = txt_url.Text.ToString();
            string user = txt_user.Text.ToString();
            string pass = txt_pass.Text.ToString();
            string topath = txt_to.Text.ToString();
            string frompath = txt_from.Text.ToString();
            grid_sftp.DataSource = bankTo(url, user, pass, topath);
            grid_sftp.DataBind();
            GridView1.DataSource = bankFrom(url, user, pass, frompath);
            GridView1.DataBind();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}