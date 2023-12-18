import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  serverURL: string = environment.serverURL;
  constructor(private http: HttpClient) { }


  allFillFinYr(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/allFillFinYr`, {
      withCredentials: true
    });
  }
  FillCropCategory(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillCropCategory`, {
      withCredentials: true
    });
  }
  FillCropByCategoryId(SelectedCropCatagory:any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillCropByCategoryId?SelectedCropCatagory=` + SelectedCropCatagory, {
      withCredentials: true
    });
  }
  fillCurrentstockPosition(SelectedFinancialYear:any,SelectedSeason:any,SelectedAgency:any,SelectedCropCatagory:any,SelectedCrop:any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/fillCurrentstockPosition?SelectedFinancialYear=` + SelectedFinancialYear+ `&SelectedSeason=` + SelectedSeason +`&SelectedAgency=`+SelectedAgency + `&SelectedCropCatagory=` + SelectedCropCatagory + `&SelectedCrop=` + SelectedCrop, {
      withCredentials: true
    });
  }
  fillGodownWiseStock(SelectedFinancialYear:any,SelectedSeason:any,SelectedAgency:any,SelectedCropCatagory:any,SelectedCrop:any,DistCode:any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/fillGodownWiseStock?SelectedFinancialYear=` + SelectedFinancialYear+ `&SelectedSeason=` + SelectedSeason +`&SelectedAgency=`+SelectedAgency + `&SelectedCropCatagory=` + SelectedCropCatagory + `&SelectedCrop=` + SelectedCrop +`&DistCode=`+DistCode, {
      withCredentials: true
    });
  }
  FillDistrict(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillDistrict`, {
      withCredentials: true
    });
  }
  fillStateStockPosition(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/fillStateStockPosition`, data, {
      withCredentials: true
    });
  }
  FillCategoryId(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillCategoryId`, {
      withCredentials: true
    });
  }
  getVarietywiseLift(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/getVarietywiseLift`, data, {
      withCredentials: true
    });
  }
  FillCropByStock_Farmer(SelectedFinancialYear:any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillCropByStock_Farmer?SelectedFinancialYear=` + SelectedFinancialYear, {
      withCredentials: true
    });
  }
  dealerPacsSale(object:any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/dealerPacsSale`, object, {
      withCredentials: true
    });
  }
  dailyProgressReport(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/dailyProgressReport`, {
      withCredentials: true
    });
  }
}
