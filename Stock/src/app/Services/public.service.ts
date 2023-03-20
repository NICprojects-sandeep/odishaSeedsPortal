import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PublicService {
  serverURL: string = environment.serverURL;
  // serverURL: string = 'http://164.100.140.77';
  
  constructor( private http: HttpClient) { }
  submitSeedSubsidyOfGrountnut(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/submitSeedSubsidyOfGrountnut`, data);
  }
  getUserDetails(userID: any): Observable<any> {
    return this.http.get(`${this.serverURL}/publicR/getUserDetails?userID=${userID}`,);
  }
  getSeedsubsidyforgroundnut(userID: any): Observable<any> {
    return this.http.get(`${this.serverURL}/publicR/getSeedsubsidyforgroundnut?userID=${userID}`,);
  }
  getTranscationDtl(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/getTranscationDtl`,data);
  }
  deleteTranscationDtl(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/deleteTranscationDtl`,data);
  }
  getprebookingDtl(): Observable<any> {
    return this.http.get(`${this.serverURL}/publicR/getprebookingDtl`);
  }
  getSearchprebookingDtl(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/getSearchprebookingDtl`,data);
  }
  getprebookingDtlDelerWise(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/getprebookingDtlDelerWise`,data);
  }
  dataDeletePrebookingDtl(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/dataDeletePrebookingDtl`,data);
  }
  getCashMemoNoDtl(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/getCashMemoNoDtl`,data);
  }
  FillSaleTransDtls(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/FillSaleTransDtls`,data);
  }
  CancelCashMemo(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/CancelCashMemo`,data);
  }
  sendtoossc(data:any): Observable<any> {
    return this.http.post(`http://164.100.140.107/addSellStockbySIS/rejectSisSoldLot`,data);
  }
  getPrebookingDistrict(selectedSeason:any): Observable<any> {
    return this.http.get(`${this.serverURL}/publicR/getPrebookingDistrict?selectedSeason=${selectedSeason}`);
  }
  getCrop(): Observable<any> {    
    return this.http.get(`${this.serverURL}/publicR/getCrop`);
  }
  getVariey(selectedCrop:any): Observable<any> {    
    return this.http.get(`${this.serverURL}/publicR/getVariey?selectedCrop=${selectedCrop}`);
  }
  getLivedata(): Observable<any> {
    return this.http.get(`${this.serverURL}/publicR/getLivedata`);
  }
  GetIncentiveOilSeed(): Observable<any> {
    return this.http.get(`${this.serverURL}/publicR/GetIncentiveOilSeed`);
  }
  InsertIncentiveOilSeed(data:any,selectedFinancialYear:any): Observable<any> {
    data[0].selectedFinancialYear=selectedFinancialYear;    
    return this.http.post(`${this.serverURL}/publicR/InsertIncentiveOilSeed`,data);
  }
  FillIncentiveOilSeed(selectedFinancialYear:any,selectedSeasons:any): Observable<any> {
    return this.http.get(`${this.serverURL}/publicR/FillIncentiveOilSeed?selectedFinancialYear=`+selectedFinancialYear+`&selectedSeason=`+selectedSeasons);
  }
  FillPendingIncentiveOilSeed(selectedFinancialYear:any,selectedSeasons:any): Observable<any> {
    return this.http.get(`${this.serverURL}/publicR/FillPendingIncentiveOilSeed?selectedFinancialYear=`+selectedFinancialYear+`&selectedSeason=`+selectedSeasons);
  }
  OsscAprvOilSeed(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/publicR/OsscAprvOilSeed`,data);
  }
}
