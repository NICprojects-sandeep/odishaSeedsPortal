import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AaoService {
  serverURL: string = environment.serverURL;

  constructor(private http: HttpClient) { }
  paymentStatusByFarmeId(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/aao/paymentStatusByFarmeId`,data, {
      withCredentials: true
    });
  }
  GetDistCodeFromAOO(): Observable<any> {
    return this.http.get(`${this.serverURL}/aao/GetDistCodeFromAOO`, {
      withCredentials: true
    });
  }
  fillfARMERiD(): Observable<any> {
    return this.http.get(`${this.serverURL}/aao/fillfARMERiD`, {
      withCredentials: true
    });
  }
  gerFarmerDetailsForSamallMarginUpdatation(NICFARMERID:any): Observable<any> {
    return this.http.get(`${this.serverURL}/aao/gerFarmerDetailsForSamallMarginUpdatation?NICFARMERID=${NICFARMERID}`, {
      withCredentials: true
    });
  }
  farmerTypeUpdate(data: any,): Observable<any> {
    return this.http.post(`${this.serverURL}/aao/farmerTypeUpdate`, data, {
      withCredentials: true
    });
  }
  delalerwisestockCheack(data: any,): Observable<any> {
    return this.http.post(`${this.serverURL}/aao/delalerwisestockCheack`, data, {
      withCredentials: true
    });
  }
  FillCategoryId(): Observable<any> {
    return this.http.get(`${this.serverURL}/aao/FillCategoryId`, {
      withCredentials: true
    });
  }
  physicalsaleentrydata(data: any,): Observable<any> {
    return this.http.post(`${this.serverURL}/aao/physicalsaleentrydata`, data, {
      withCredentials: true
    });
  }
  inserphysicalsaleentrydata(data: any,): Observable<any> {
    return this.http.post(`${this.serverURL}/aao/inserphysicalsaleentrydata`, data, {
      withCredentials: true
    });
  }
}
