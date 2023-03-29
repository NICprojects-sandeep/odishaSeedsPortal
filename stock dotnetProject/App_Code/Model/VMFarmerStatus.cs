using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for VMFarmerStatus
/// </summary>
public class FarmerApproveStatus
{
    public string status { get; set; }
    public string farID { get; set; }
}
//public class FarmerAAOAAO1Status
//{
// public string count { get; set; }
//   // public string Message { get; set; }
//}
public class FarmerRejectStatus
{
    public string Name { get; set; }
    public string status { get; set; }
    public string reason { get; set; }
    public string AccNo { get; set; }
}