using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class OssopcaLotApproval : System.Web.UI.Page
{
    BLL_DropDown objUserBEL;
    DLL_DropDown objUserDLL;
    DataSet ds = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.Page.IsPostBack)
        {
            FillLots();
        }
    }
    private void FillLots()
    {
        objUserBEL = new BLL_DropDown();
        objUserBEL.FIN_YR = "";
        objUserBEL.SEASON = "";
        objUserBEL.UPDATED_BY = "";
        objUserBEL.TypeId = 2;

        int Cnt = 0;
        StringBuilder str = new StringBuilder("");
        objUserDLL = new DLL_DropDown();
        ds = objUserDLL.ClassSel(objUserBEL);
        if (ds != null)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                str.Append("<table class='table table-bordered'");
                str.Append("<thead>");
                str.Append("<tr>");
                str.Append("<th>SL NO</th>");
                str.Append("<th>LOT NUMBER</th>");
                str.Append("<th>QUANTITY</th>");
                str.Append("<th>SOURCE</th>");
                str.Append("<th>STATUS</th>");
                str.Append("</tr>");
                str.Append("</thead>");
                str.Append("<tbody>");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Cnt += 1;
                        str.Append("<tr>");
                        str.Append("<td>" + Cnt.ToString() + "</td>");
                        str.Append("<td>" + dr["LOT_NO"].ToString() + "</td>");
                        str.Append("<td>" + dr["QTY"].ToString() + "</td>");
                        str.Append("<td>" + dr["SOURCE"].ToString() + "</td>");
                        str.Append("<td>" + dr["IS_OSSOPCA"].ToString() + "</td>");
                        //gvClass.DataSource = ds;
                        //gvClass.DataBind();
                        str.Append("</tr>");
                    }
                }
                str.Append("</tbody>");
                str.Append("</table>");
            }
        }
        litReport.Text = str.ToString();

    }
}