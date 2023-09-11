import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DealerService {
  serverURL: string = environment.serverURL;
  constructor(private http: HttpClient) { }

  GetDealerLicenceByDistCodeUserType(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/GetDealerLicenceByDistCodeUserType`, {
      withCredentials: true
    });
  }
  GetDealerLicenceByDistCodeUserTypePacs(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/GetDealerLicenceByDistCodeUserTypePacs`, {
      withCredentials: true
    });
  }
  FillFinYr(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FILLFINYR`, {
      withCredentials: true
    });
  }
  FillSeason(FinYr: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FILLSEASSION?FIN_YR=` + FinYr, {
      withCredentials: true
    });
  }
  FILL_GODOWN(prebookedsale: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FILL_GODOWN?prebookedsale=` + prebookedsale, {
      withCredentials: true
    });
  }
  FILL_CROPCATAGORY(selectedGodown: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FILL_CROPCATAGORY?selectedGodown=` + selectedGodown, {
      withCredentials: true
    });
  }
  FILLCROPNAME(selectedCategory: any, selectedGodown: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FILLCROPNAME?selectedCategory=` + selectedCategory + `&selectedGodown=` + selectedGodown, {
      withCredentials: true
    });
  }
  FILLCROPVARIETY(selectedCrop: any, selectedCategory: any, selectedGodown: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FILLCROPVARIETY?selectedCrop=` + selectedCrop + `&selectedCategory=` + selectedCategory + `&selectedGodown=` + selectedGodown, {
      withCredentials: true
    });
  }
  prebookingDetailsOfDealer(SelectedDealerOrPacs: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/prebookingDetailsOfDealer?SelectedDealerOrPacs=` + SelectedDealerOrPacs, {
      withCredentials: true
    });
  }
  fillAvailableStockDetails(selectedVariety: any, selectedCrop: any, selectedCategory: any, selectedGodown: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/fillAvailableStockDetails?selectedVariety=` + selectedVariety + `&selectedCrop=` + selectedCrop + `&selectedCategory=` + selectedCategory + `&selectedGodown=` + selectedGodown, {
      withCredentials: true
    });
  }
  getSupplyType(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/getSupplyType`, {
      withCredentials: true
    });
  }
  fillDealerSaleDeatils(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dealer/fillDealerSaleDeatils`, data, {
      withCredentials: true
    });
  }
  cashmemodetails(applicationid: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/cashmemodetails?applicationid=` + applicationid, {
      withCredentials: true
    });
  }
  FillLots(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FillLots`, {
      withCredentials: true
    });
  }
  FillCrop(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FillCrop`, {
      withCredentials: true
    });
  }
  FillVariety(selectedCrop:any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FillVariety?selectedCrop=` + selectedCrop, {
      withCredentials: true
    });
  }
  addinClass(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dealer/addinClass`, data, {
      withCredentials: true
    });
  }
  allFillFinYr(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/allFillFinYr`, {
      withCredentials: true
    });
  }
  FillCropCategory(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FillCropCategory`, {
      withCredentials: true
    });
  }
  FillCropByCategoryId(SelectedCropCatagory:any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/FillCropByCategoryId?SelectedCropCatagory=` + SelectedCropCatagory, {
      withCredentials: true
    });
  }
  fillGodownwisestock(SelectedFinancialYear:any,SelectedSeason:any,SelectedCropCatagory:any,SelectedCrop:any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/fillGodownwisestock?SelectedFinancialYear=` + SelectedFinancialYear+ `&SelectedSeason=` + SelectedSeason + `&SelectedCropCatagory=` + SelectedCropCatagory + `&SelectedCrop=` + SelectedCrop, {
      withCredentials: true
    });
  }
  receivedetails(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dealer/receivedetails`, data, {
      withCredentials: true
    });
  }
  dateWiseSaleDetails(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dealer/dateWiseSaleDetails`, data, {
      withCredentials: true
    });
  }
  dateWiseGodownTransferDetails(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dealer/dateWiseGodownTransferDetails`, data, {
      withCredentials: true
    });
  }
  saledetails(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dealer/saledetails`, data, {
      withCredentials: true
    });
  }
  getGodownmaster(SALE_TO: any): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/getGodownmaster?SALE_TO=` + SALE_TO, {
      withCredentials: true
    });
  }
  GetDistCodeFromDist(): Observable<any> {
    return this.http.get(`${this.serverURL}/dealer/GetDistCodeFromDist`, {
      withCredentials: true
    });
  }
  paymentStatusByFarmeId(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dealer/paymentStatusByFarmeId`,data, {
      withCredentials: true
    });
  }
  submitSeedSubsidyOfGrountnut(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/dealer/submitSeedSubsidyOfGrountnut`, data, {
      withCredentials: true
    });
  }
}
