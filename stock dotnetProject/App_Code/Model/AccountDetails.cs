using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for F_ApproveDetails
/// </summary>
/// 
public class RegDetails_M_Reg
{
    //public string nicFarmerID { get; set; }
    public string farmerCode { get; set; }
    public string farmerName { get; set; }
    public string fatherName { get; set; }
    public string mobNo { get; set; }
    public string accountNo { get; set; }
    public string bankName { get; set; }
    public string branchName { get; set;}
    public string IFSCCode { get; set; }
    public string pfmsStatus { get; set; }
}
public class ReqSentDetails
{
    public string uniqmsgID { get; set; }
    public string Name { get; set; }
    public string fatherName { get; set; }
    public string accountNo { get; set; }
    public string schemeSpecificID { get; set; }
    public string IFSCCode { get; set; }
    public string mobNo { get; set; }
    
}

public class F_ApproveDetails
{
    public string fID { get; set; }
    public string FCode { get; set; }
    public string status { get; set; }
    //public string Name { get; set; }
    //public string fatherName { get; set; }
}

public class F_NotInApprove
{
    public string fID { get; set; }
    public string fCode { get; set; }
    //public string fName { get; set; }
    public string status { get; set; }
    public string reason { get; set; }
    public string accNo { get; set; }
    public string IFSCCode { get; set; }
}