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
   getblockWiseDealer(blockCode: any,LGDistrict:any): Observable <any>{
    return this.http.get(`${this.serverURL}/publicR/getblockWiseDealer?BlockCode=${blockCode}&DistrictCode=${LGDistrict}`);
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
   loadSeason(year:any): Observable <any>{
    return this.http.get(`https://osscpayment.nic.in/transferPortal/api/getSeason?finYear=${year}`);
   }
   loadAllCrop(year:any,season:any): Observable <any>{
    return this.http.get(`https://osscpayment.nic.in/transferPortal/api/loadAllCrop?year=${year}&season=${season}`);
   }
   loadVariety(crop:any,year:any,season:any): Observable <any>{
    return this.http.get(`https://osscpayment.nic.in/transferPortal/api/loadVariety?crop=${crop}&year=${year}&season=${season}`);
   }
   loadDistricts(): Observable <any>{
    return this.http.get(`https://osscpayment.nic.in/transferPortal/api/loadDistricts`);
   }
   districtWiseData(crop:any,year:any,season:any): Observable <any>{
    return this.http.get(`https://osscpayment.nic.in/transferPortal/api/districtWiseData?year=${year}&season=${season}&crop=${crop}`);
   }
   
   loadSaleData(crop:any,year:any,season:any): Observable <any>{
    return this.http.get(`https://osscpayment.nic.in/transferPortal/api/districtWiseSaleData?year=${year}&season=${season}&crop=${crop}`);
   }
   loadPendingData(crop:any,year:any,season:any): Observable <any>{
    return this.http.get(`https://osscpayment.nic.in/transferPortal/api/pendingData?year=${year}&season=${season}&crop=${crop}`);
   }
   getPacsLamps(): Observable <any>{
    return this.http.get(`https://odishaagrilicense.nic.in/user/getPacsLamps?appKey=JHR788DD42EWD5S5SD45XP5Y`);
   } 
   getDealerCount(): Observable <any>{
    return this.http.get(`https://odishaagrilicense.nic.in/user/getDealerCount?appKey=JHR788DD42EWD5S5SD45XP5Y`);
   }
}
