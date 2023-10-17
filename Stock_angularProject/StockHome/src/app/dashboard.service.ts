import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  serverURL: string = environment.serverURL;
  constructor(private http: HttpClient)  { }


  getStockPricelist(): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/getStockPricelist`);
   }
   getDealerDetails(DistrictCode: any): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/getDealerDetails?DistrictCode=${DistrictCode}`);
   }
   getDistrict(): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/getDistrict`);
   }
   getBlock(distCode: any): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/getBlock?DistrictCode=${distCode}`);
   }
   getblockWiseDealer(blockCode: any): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/getblockWiseDealer?BlockCode=${blockCode}`);
   }
   getcropList(): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/getcropList`);
   }
   graphVariety( CropID: any): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/graphVariety?CropID=${CropID}`);
   }
   getStockPricelistAfter(): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/getStockPricelistAfter`);
   }
   fillnews(): Observable <any>{
    return this.http.get(`${this.serverURL}/osp/home/FillNews`);
   }
}
