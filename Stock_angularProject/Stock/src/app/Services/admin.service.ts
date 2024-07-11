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
  FillCropByCategoryId(SelectedCropCatagory: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillCropByCategoryId?SelectedCropCatagory=` + SelectedCropCatagory, {
      withCredentials: true
    });
  }
  fillCurrentstockPosition(SelectedFinancialYear: any, SelectedSeason: any, SelectedAgency: any, SelectedCropCatagory: any, SelectedCrop: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/fillCurrentstockPosition?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedAgency=` + SelectedAgency + `&SelectedCropCatagory=` + SelectedCropCatagory + `&SelectedCrop=` + SelectedCrop, {
      withCredentials: true
    });
  }
  fillGodownWiseStock(SelectedFinancialYear: any, SelectedSeason: any, SelectedAgency: any, SelectedCropCatagory: any, SelectedCrop: any, DistCode: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/fillGodownWiseStock?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedAgency=` + SelectedAgency + `&SelectedCropCatagory=` + SelectedCropCatagory + `&SelectedCrop=` + SelectedCrop + `&DistCode=` + DistCode, {
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
  FillCropByStock_Farmer(SelectedFinancialYear: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillCropByStock_Farmer?SelectedFinancialYear=` + SelectedFinancialYear, {
      withCredentials: true
    });
  }
  dealerPacsSale(object: any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/dealerPacsSale`, object, {
      withCredentials: true
    });
  }
  dailyProgressReport(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/dailyProgressReport`, {
      withCredentials: true
    });
  }
  getPFMSStatus(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getPFMSStatus`, {
      withCredentials: true
    });
  }
  distwisestockdetails(SelectedFinancialYear: any, SelectedSeason: any, SelectedCrop: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/distwisestockdetails?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedCrop=` + SelectedCrop, {
      withCredentials: true
    });
  }
  blockwisestockdetails(SelectedFinancialYear: any, SelectedSeason: any, SelectedCrop: any, DIST_CODE: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/blockwisestockdetails?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedCrop=` + SelectedCrop + `&SelectedDistrict=` + DIST_CODE, {
      withCredentials: true
    });
  }
  previousYeardailyProgressReport(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/previousYeardailyProgressReport`, {
      withCredentials: true
    });
  }
  dealerwisestockdetails(SelectedFinancialYear: any, SelectedSeason: any, SelectedCrop: any, BLK_CODE: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/dealerwisestockdetails?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedCrop=` + SelectedCrop + `&SelectedBlock=` + BLK_CODE, {
      withCredentials: true
    });
  }
  schemewisedetails(SelectedFinancialYear: any, SelectedSeason: any, SelectedCrop: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/schemewisedetails?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&SelectedCrop=` + SelectedCrop, {
      withCredentials: true
    });
  }
  dealerwisewisesaledetails(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/dealerwisewisesaledetails`, data, {
      withCredentials: true
    });
  }
  getPFSMTransctionDetails(enteredCPAID: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getPFSMTransctionDetails?enteredCPAID=` + enteredCPAID, {
      withCredentials: true
    });
  }
  getPFSMTransctionDetailsDealerCodeWise(enteredCPAID: any, enteredDealerCode: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getPFSMTransctionDetailsDealerCodeWise?enteredCPAID=` + enteredCPAID + `&enteredDealerCode=` + enteredDealerCode, {
      withCredentials: true
    });
  }
  dealerPacsPaymentdetails(SelectedFinancialYear: any, SelectedSeason: any, enteredDealerPacsId: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/dealerPacsPaymentdetails?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason + `&enteredDealerPacsId=` + enteredDealerPacsId, {
      withCredentials: true
    });
  }
  subsidyInvolovementdetails(SelectedFinancialYear: any, SelectedSeason: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/subsidyInvolovementdetails?SelectedFinancialYear=` + SelectedFinancialYear + `&SelectedSeason=` + SelectedSeason, {
      withCredentials: true
    });
  }
  getAllUserType(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getAllUserType`, {
      withCredentials: true
    });
  }
  getUserId(SelectedUserType: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getUserId?SelectedUserType=` + SelectedUserType, {
      withCredentials: true
    });
  }
  resetPassword(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/resetPassword`, data, {
      withCredentials: true
    });
  }
  getprebookingDtl(): Observable<any> {
    console.log('hhhhh');
    
    return this.http.get(`${this.serverURL}/admin/getprebookingDtl`, {
      withCredentials: true
    });
  }
  getSearchprebookingDtl(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/getSearchprebookingDtl`, data, {
      withCredentials: true
    });
  }
  getPrebookingDistrict(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getPrebookingDistrict`, {
      withCredentials: true
    });
  }
  getVariey(selectedCrop: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getVariey?selectedCrop=${selectedCrop}`, {
      withCredentials: true
    });
  }
  getSearchErupidata(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getSearchErupidata`, {
      withCredentials: true
    });
  }
  getErupiDistrict(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getErupiDistrict`, {
      withCredentials: true
    });
  }
  getErupiBlock(selectedDistrict: any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/getErupiBlock?selectedDistrict=${selectedDistrict}`, {
      withCredentials: true
    });
  }
  getParticularSearchErupidata(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/getParticularSearchErupidata`, data, {
      withCredentials: true
    });
  }
  geterupiStockDetails(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/admin/geterupiStockDetails`, data, {
      withCredentials: true
    });
  }
}
