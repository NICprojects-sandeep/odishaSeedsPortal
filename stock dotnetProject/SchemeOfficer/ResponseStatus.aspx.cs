
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class ResponseStatus : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            paymentStatusDetails();
            endtoendbtn.Click += new EventHandler(this.endtoendbtn_Click);
        }

        public void paymentStatusDetails()
        {
            PaymentStatusBal obj = new PaymentStatusBal();
            var Res = obj.getAllResponseList();
            mResponseStatus.DataSource = Res;
            mResponseStatus.DataBind();


        }

    
        protected void endtoendbtn_Click(object sender, EventArgs e)
        {
            String ete = etetext.Value.ToString();
            PaymentStatusBal obj = new PaymentStatusBal();
            var Res = obj.getAllResponseETE(ete);
            mResponseStatus.DataSource = Res;
            mResponseStatus.DataBind();
        }


        protected void OnPaging(object sender, GridViewPageEventArgs e)
{
    mResponseStatus.PageIndex = e.NewPageIndex;
    paymentStatusDetails();
}
    }
