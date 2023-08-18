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


  GetDistCodeFromLicNo(): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETDISTCODEFROMLICNO`, {
      withCredentials: true
    });
   }
//ODSON1/2014-15/0001/E
  CheckAccessMode(RefNo : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/CHECKACCESSMODE?REF_NO=`+RefNo, {
      withCredentials: true
    });
   }

   GetFarmerInfo(FarmerId : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETFARMERINFO?FARMER_ID=`+FarmerId, {
      withCredentials: true
    });
   }

   FillFinYr(status : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/FILLFINYR?STATUS=`+status, {
      withCredentials: true
    });
   }

   FillSeason(FinYr : any,status : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/FILLSEASSION?FIN_YR=`+FinYr+'&STATUS='+status, {
      withCredentials: true
    });
   }
   FillCrops(FinYr : any,Seasons:any): Observable <any>{    
    return this.http.get(`${this.serverURL}/stock/home/FillCrops?FIN_YR=`+FinYr+'&Seasons='+Seasons, {
      withCredentials: true
    });
   }
   FillVariety(FinYr : any,Seasons:any,crop:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/FillVariety?FIN_YR=`+FinYr+'&Seasons='+Seasons+'&Crop='+crop, {
      withCredentials: true
    });
   }
   GetFarmerRecvCrop(FarmerId:any,FinYr : any,Season : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETFARMERRECVCROP?FARMER_ID=`+FarmerId+'&FIN_YR='+FinYr+'&SEASON='+Season, {
      withCredentials: true
    });
   }

   GetDealerStockCrop(LicenceNo:any,FinYr : any,Season : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETDEALERSTOCKCROP?LicNo=`+LicenceNo+'&FinYr='+FinYr+'&Season='+Season, {
      withCredentials: true
    });
   }

   GetDealerStockVariety(LicenceNo:any,FinYr : any,Season : any,CropId:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETDEALERSTOCKVARIETY?LicNo=`+LicenceNo+'&FinYr='+FinYr+'&Season='+Season+'&CropId='+CropId, {
      withCredentials: true
    });
   }

   GetDealerStock(LicenceNo:any,FinYr : any,Season : any,CropId:any, VarietyId:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GETDEALERSTOCK?LicNo=`+LicenceNo+'&FinYr='+FinYr+'&Season='+Season+'&CropId='+CropId+'&VarietyId='+VarietyId, {
      withCredentials: true
    });
   }
   
   FILLDEALERSTOCK(FinYr : any,Season : any,CropId:any, VarietyId:any,userTypes:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/FILLDEALERSTOCK?FIN_YR=`+FinYr+'&SEASSION='+Season+'&CROP_CODE='+CropId+'&CROP_VERID='+VarietyId+'&USER_TYPE='+userTypes, {
      withCredentials: true
    });
   }
   getStockReceivedData(FinYr : any,Season : any,FarmerId:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/getStockReceivedData?FIN_YR=`+FinYr+'&SEASSION='+Season+'&FarmerId='+FarmerId, {
      withCredentials: true
    });
   }
   getPreBookingDetails(FinYr : any,Season : any,FarmerId:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/getPreBookingDetails?FIN_YR=`+FinYr+'&SEASSION='+Season+'&FarmerId='+FarmerId, {
      withCredentials: true
    });
   }
   InsertSaleDealer(alldata:any): Observable <any>{
    return this.http.post(`${this.serverURL}/stock/home/InsertSaleDealer`,alldata, {
      withCredentials: true
    });
   }
   GetFirmName(): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GetFirmName`, {
      withCredentials: true
    });
   }
   GetFarmerInvHdr(farmerID : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GetFarmerInvHdr?farmerID=`+farmerID, {
      withCredentials: true
    });
   }
   sendOtp(FarmerId:any,MobileNo:any,LicNo:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/sendOtp?FarmerId=`+FarmerId+'&MobileNo='+MobileNo+'&LicNo='+LicNo, {
      withCredentials: true
    });
   }
   ValidateOTP(FarmerId:any,enteredOtp:any,LicNo:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/ValidateOTP?FarmerId=`+FarmerId+'&enteredOtp='+enteredOtp+'&LicNo='+LicNo, {
      withCredentials: true
    });
   }
   GetFarmerInv(TRANSACTION_ID : any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/GetFarmerInv?TRANSACTION_ID=`+TRANSACTION_ID, {
      withCredentials: true
    });
   }
   RptDateWiseSale(selectedFromDate : any,selectedToDate:any,LicNo:any): Observable <any>{
    return this.http.get(`${this.serverURL}/stock/home/RptDateWiseSale?selectedFromDate=`+selectedFromDate+'&selectedToDate='+selectedToDate+'&LicNo='+LicNo, {
      withCredentials: true
    });
   }
}
