import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  constructor(private http: HttpClient)  { }


  getStockPricelist(): Observable <any>{
    return this.http.get(`http://localhost:3000/publicR/getStockPricelist`);
   }
   getDealerDetails(DistrictCode: any): Observable <any>{
    return this.http.get(`http://localhost:3000/publicR/getDealerDetails?DistrictCode=${DistrictCode}`);
   }
   getDistrict(): Observable <any>{
    return this.http.get(`http://localhost:3000/publicR/getDistrict`);
   }
   getBlock(distCode: any): Observable <any>{
    return this.http.get(`http://localhost:3000/publicR/getBlock?DistrictCode=${distCode}`);
   }
   getblockWiseDealer(blockCode: any): Observable <any>{
    return this.http.get(`http://localhost:3000/publicR/getblockWiseDealer?BlockCode=${blockCode}`);
   }
   getcropList(): Observable <any>{
    return this.http.get(`http://localhost:3000/publicR/getcropList`);
   }
   graphVariety( CropID: any): Observable <any>{
    return this.http.get(`http://localhost:3000/publicR/graphVariety?CropID=${CropID}`);
   }
   getStockPricelistAfter(): Observable <any>{
    return this.http.get(`http://localhost:3000/publicR/getStockPricelistAfter`);
   }
}
