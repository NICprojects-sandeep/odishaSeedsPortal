using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using tagcheckAModel;
 

/// <summary>
/// Summary description for Class1
/// </summary>
public class Class1
{
    tagchecke orm = new tagchecke();
    //public List<sgWithLot> seedgrowers(string ag_code)
    //{
    //    List<sgWithLot> GrowerData = new List<sgWithLot>();
    //    List<string> ltno = new List<string>();
    //    var sg_det = orm.Seed_Growers.Where(a => a.Name_of_agency == ag_code).ToList();
    //    foreach (var m in sg_det)
    //    {
    //        var cropdetails = orm.Crops.Where(a => a.Ref_NO == m.Ref_No).ToList();
    //        foreach (var crp in cropdetails)
    //        {
    //            var lotdetails = orm.Threashings.Where(a => a.Crop_ID == crp.Crop_id).FirstOrDefault();
    //            if (lotdetails != null)
    //            {
    //                var sampleDetails = orm.Samplings.Where(a => a.LotNo == lotdetails.Lot_No).ToList();
    //                if (sampleDetails.Count > 0)
    //                {
                        
    //                    foreach (var tc in sampleDetails)
    //                    {

    //                        var tagdetails = orm.TagCertificates.Where(a => a.Code == tc.RandomNo).ToList();
                            
    //                        if (tagdetails.Count > 0)
    //                        {

    //                          string  singletc = tc.LotNo + "/" + tc.SubLotNo;
    //                            if (singletc.ToString() != "")
    //                            {
    //                                ltno.Add(singletc);
    //                            }
    //                        }
    //                    }
                       
    //                }
    //            }
    //        }
    //        if (ltno.Count > 0)
    //        {
    //            sgWithLot SingleSgDetail = new sgWithLot();
    //            SingleSgDetail.Ref_name = m.Ref_No;
    //            SingleSgDetail.sg_name = m.SG_Name;
    //            SingleSgDetail.lot = ltno;
    //            GrowerData.Add(SingleSgDetail);

    //        }
    //    }
    //    return GrowerData;
    //}

}

public class AgClass
{
    tagchecke orm = new tagchecke();

    public List<sgWithLot> sgWithref(string ag_name)
    {
        var Ag_code = orm.Agencies.Where(a => a.Secter == ag_name).FirstOrDefault();
        List<sgWithLot> ls = new List<global::sgWithLot>();
        List<string> listLotref = new List<string>();
        var lotreflist = (from a in orm.VW_agencyTag where a.Name_of_agency == Ag_code.Agency_code select a.Ref_NO).Distinct().ToList();
        if (lotreflist.Count > 0)
        {
            foreach (var s in lotreflist)
            {
                sgWithLot sss = (from l in orm.VW_agencyTag where l.Ref_NO == s select new sgWithLot { Ref_name = l.Ref_NO, sg_name = l.SG_Name,ddist_code=l.District_code,sg_season=l.Season }).FirstOrDefault();
                ls.Add(sss);
            }

        }
        return ls;
    }

    public List<listlot> SgLotList(string refno, string vrty, string clss)
    {
       List<listlot> lls =new List<listlot>();
        List<string> ls = orm.VW_agencyTag.Where(a => a.Ref_NO == refno && a.Varity_Code == vrty && a.Class_code == clss).Select(d => d.lotsublot).Distinct().ToList();
        foreach (var v in ls)
        {
            listlot lt = new listlot();
            lt.sg_lotlist=v;
            lls.Add(lt);
        }
        return lls;
    }
}

public class sgWithLot
{
    public string sg_name { get; set; }
    public string Ref_name { get; set; }
    public string ddist_code { get; set; }
    public string sg_season { get; set; }
   
}

public class listlot
{
    public string sg_lotlist { get; set; }
}

