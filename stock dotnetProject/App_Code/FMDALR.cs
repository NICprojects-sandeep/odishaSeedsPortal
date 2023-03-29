using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FMDAL
/// </summary>
public class FMDALR
{
    public FMDALR()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public DataTable GetErrroName(string sysid)
    {
        DataTable dt = new DataTable();
        string query = "select distinct Rejection_Narration from [BAS].[dbo].[Response_Payment_Authorization_Message] where Rejection_Narration!='' and left(Unique_Message_Id,4)='" + sysid + "'";
        dt = DbConfigueAGRIS.GetTableDetails(query);
        return dt;
    }
    public DataTable GetDetails(string narration)
    {
        DataTable dt = new DataTable();
        string query = "select left(Unique_Credit_Transaction_Id,len(Unique_Credit_Transaction_Id)-4) unqid,Beneficiary_Name nme,Total_Payment_Amount amt,Bank_Post_Office_Name bank, " +
                        "Bank_Post_Office_Branch ifsc, Bank_Account_number accno,Record_Status sts, Rejection_Narration nar from[dbo].[Response_Payment_Authorization_Message] where " +
                        "Rejection_Narration = '" + narration + "' and Resent_Status is null";
        dt = DbConfigueAGRIS.GetTableDetails(query);
        return dt;
    }
}