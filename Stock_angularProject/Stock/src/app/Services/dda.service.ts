import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DdaService {
  serverURL: string = environment.serverURL;
  constructor(private http: HttpClient) { }
  allFillFinYr(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/allFillFinYr`, {
      withCredentials: true
    });
  }
  FillCropCategory(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/FillCropCategory`, {
      withCredentials: true
    });
  }
  FillCropByCategoryId(SelectedCropCatagory: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/FillCropByCategoryId?SelectedCropCatagory=` + SelectedCropCatagory, {
      withCredentials: true
    });
  }
  fillCurrentstockPosition(SelectedFinancialYear: any, SelectedSeason: any, SelectedAgency: any, SelectedCropCatagory: any, SelectedCrop: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/fillCurrentstockPosition?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedAgency=` + SelectedAgency + `&SelectedCropCatagory=` + SelectedCropCatagory + `&SelectedCrop=` + SelectedCrop, {
      withCredentials: true
    });
  }
  fillGodownWiseStock(SelectedFinancialYear: any, SelectedSeason: any, SelectedAgency: any, SelectedCropCatagory: any, SelectedCrop: any, DistCode: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/fillGodownWiseStock?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedAgency=` + SelectedAgency + `&SelectedCropCatagory=` + SelectedCropCatagory + `&SelectedCrop=` + SelectedCrop + `&DistCode=` + DistCode, {
      withCredentials: true
    });
  }
  FillDistrict(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/FillDistrict`, {
      withCredentials: true
    });
  }
  fillStateStockPosition(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dda/fillStateStockPosition`, data, {
      withCredentials: true
    });
  }
  FillCategoryId(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/FillCategoryId`, {
      withCredentials: true
    });
  }
  blockwisetransctiondetails(SelectedFinancialYear: any, SelectedSeason: any, SelectedCrop: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/blockwisetransctiondetails?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason+ `&SelectedCrop=` + SelectedCrop, {
      withCredentials: true
    });
  }
  blockwiseStockDetails(SelectedFinancialYear: any, SelectedSeason: any, SelectedCrop: any, BlockCode: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/blockwiseStockDetails?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedCrop=` + SelectedCrop + `&BlockCode=` + BlockCode, {
      withCredentials: true
    });
  }
  delaeListDistWise(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/delaeListDistWise`, {
      withCredentials: true
    });
  }
  dealerwisePaymentDetails(LIC_NO:any): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/dealerwisePaymentDetails?LIC_NO=` + LIC_NO, {
      withCredentials: true
    });
  }
  paymentStatusByFarmeId(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dda/paymentStatusByFarmeId`,data, {
      withCredentials: true
    });
  }
  GetDistCodeFromDist(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/GetDistCodeFromDist`, {
      withCredentials: true
    });
  }
  GetFarmerSaleDetails(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/GetFarmerSaleDetails`, {
      withCredentials: true
    });
  }
  getMPRDistrict(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/getMPRDistrict`, {
      withCredentials: true
    });
  }
  FillDistrictWiseBlock(districtCode:any): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/FillDistrictWiseBlock?districtCode=` + districtCode, {
      withCredentials: true
    });
  }
  FillMPRYear(): Observable<any> {
    return this.http.get(`${this.serverURL}/dda/FillMPRYear`, {
      withCredentials: true
    });
  }
  selectedDistrictMPR(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/dda/selectedDistrictMPR`, data, {
      withCredentials: true
    });
  }
  selectedDistrictMPR2(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/dda/selectedDistrictMPR2`, data, {
      withCredentials: true
    });
  }
}
