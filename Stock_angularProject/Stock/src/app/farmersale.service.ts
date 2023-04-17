import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class FarmersaleService {
  serverURL: string = environment.serverURL;
  constructor(private http: HttpClient) { }

  status:string='T';
  FinYr:any;
  RefNo:any='ODSON1/2014-15/0001/E';
  LicNo:any='ODBAL1/2014-15/0013';
  setAddLot(addlot:any){
    return localStorage.setItem('addlot',addlot);
  }

  getAddLot(){
    return localStorage.getItem('addlot');
  }


  GetDistCodeFromLicNo(LicNo : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETDISTCODEFROMLICNO?LIC_NO=`+LicNo);
   }
//ODSON1/2014-15/0001/E
  CheckAccessMode(RefNo : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/CHECKACCESSMODE?REF_NO=`+RefNo);
   }

   GetFarmerInfo(FarmerId : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETFARMERINFO?FARMER_ID=`+FarmerId);
   }

   FillFinYr(status : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/FILLFINYR?STATUS=`+status);
   }

   FillSeason(FinYr : any,status : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/FILLSEASSION?FIN_YR=`+FinYr+'&STATUS='+status);
   }
   FillCrops(FinYr : any,Seasons:any,LicNo: any): Observable <any>{    
    return this.http.get(`${this.serverURL}/stock/home/FillCrops?FIN_YR=`+FinYr+'&Seasons='+Seasons+'&LicNo='+LicNo);
   }
   FillVariety(FinYr : any,Seasons:any,crop:any,LicNo: any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/FillVariety?FIN_YR=`+FinYr+'&Seasons='+Seasons+'&Crop='+crop+'&LicNo='+LicNo);
   }
   GetFarmerRecvCrop(FarmerId:any,FinYr : any,Season : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETFARMERRECVCROP?FARMER_ID=`+FarmerId+'&FIN_YR='+FinYr+'&SEASON='+Season);
   }

   GetDealerStockCrop(LicenceNo:any,FinYr : any,Season : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETDEALERSTOCKCROP?LicNo=`+LicenceNo+'&FinYr='+FinYr+'&Season='+Season);
   }

   GetDealerStockVariety(LicenceNo:any,FinYr : any,Season : any,CropId:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETDEALERSTOCKVARIETY?LicNo=`+LicenceNo+'&FinYr='+FinYr+'&Season='+Season+'&CropId='+CropId);
   }

   GetDealerStock(LicenceNo:any,FinYr : any,Season : any,CropId:any, VarietyId:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETDEALERSTOCK?LicNo=`+LicenceNo+'&FinYr='+FinYr+'&Season='+Season+'&CropId='+CropId+'&VarietyId='+VarietyId);
   }
   
   FILLDEALERSTOCK(LicenceNo:any,FinYr : any,Season : any,CropId:any, VarietyId:any,userTypes:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/FillDealerStock?LIC_NO=`+LicenceNo+'&FIN_YR='+FinYr+'&SEASSION='+Season+'&CROP_CODE='+CropId+'&CROP_VERID='+VarietyId+'&USER_TYPE='+userTypes);
   }
   getStockReceivedData(FinYr : any,Season : any,FarmerId:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/getStockReceivedData?FIN_YR=`+FinYr+'&SEASSION='+Season+'&FarmerId='+FarmerId);
   }
   getPreBookingDetails(FinYr : any,Season : any,FarmerId:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/getPreBookingDetails?FIN_YR=`+FinYr+'&SEASSION='+Season+'&FarmerId='+FarmerId);
   }
   InsertSaleDealer(alldata:any): Observable <any>{
    return this.http.post(`${this.serverURL}/stock/home/InsertSaleDealer`,alldata);
   }
   GetFirmName(LicNo : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GetFirmName?LIC_NO=`+LicNo);
   }
   GetFarmerInvHdr(farmerID : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GetFarmerInvHdr?farmerID=`+farmerID);
   }
}
