using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TestProgram : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        int LabelCount = 10;
        for (int i = 1; i <= LabelCount; i++)
        {
            Label lbl = new Label();

            lbl.Text = "Hello World " + i.ToString() + "<br>";
            lbl.ID = "lbl" + i.ToString();
            form1.Controls.Add(lbl);
        }
    }

   /* protected void btnSubmit_Click(object sender, EventArgs e)
    {
       
    }*/

}