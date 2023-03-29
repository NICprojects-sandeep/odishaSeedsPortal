using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


/// <summary>
/// Summary description for Complain_Bal
/// </summary>
public class Complain_Bal
{
	public Complain_Bal()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    stockEntities2 C_Orm = new stockEntities2();

    public List<Complain_Type_Table> GetAllComplainType()
    {
        List<Complain_Type_Table> ComplainType = (from c in C_Orm.Complain_Type_Table where c.Complain_type_Id == "3" select c).ToList();
        return ComplainType;
    }

    public bool AddToComplain(Complain_Table ct)
    {
        C_Orm.Complain_Table.Add(ct);
        C_Orm.SaveChanges();
        return true;
    }
    
    public int GetComplainNo(string asco)
    {
        int CoNo = (from c in C_Orm.Complain_Table where c.Asco_Code == asco orderby c.Complain_No descending select c.Complain_No).FirstOrDefault();
        return CoNo;
    }

    public int GetSCOComplainNo(string sco)
    {
        int CoNo = (from c in C_Orm.Complain_Table where c.Sco_code == sco orderby c.Complain_No descending select c.Complain_No).FirstOrDefault();
        return CoNo;
    }

}