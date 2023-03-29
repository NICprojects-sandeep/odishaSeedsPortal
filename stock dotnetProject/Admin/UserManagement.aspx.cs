using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;

public partial class Admin_UserManagement : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void add_user(object sender, EventArgs e)
    {
        bal obj = new bal();
        Page page = HttpContext.Current.CurrentHandler as Page;
        string imei = imeiText.Value;
        string password = ConvertStringtoMD5(passwordText.Value);

        string systemId = systemidText.Value;
        string employeeName = employeeNameText.Value;
        string employeeCode = empoyeecodeText.Value;

        User myobj = new User();
        myobj.imei = imei;
        myobj.passwordHash = password;
        myobj.System_Id = systemId;
        myobj.Employee_Name = employeeName;
        myobj.Employee_Code = employeeCode;
        var k = obj.addUser(myobj);
        if(k==true)
        {
        page.Response.Write("<script>alert('New User Added to the BAS Systems with IMEI: "+imei+"');</script>");
        } else {
            page.Response.Write("<script>alert('The Operation was unsuccessful');</script>");
        }
    }

    public static string ConvertStringtoMD5(string strword)
    {
        MD5 md5 = MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(strword);
        byte[] hash = md5.ComputeHash(inputBytes);
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }
        return sb.ToString();
    }
}