import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { element } from 'protractor';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {
  private graphVarietyList: any[] = [];
  constructor(private http: HttpClient) {

   }

  //  Menu:any;
  //  SubMenu:any;
   
  //  fillLogoDetailsO(): Observable <any>{
  //   return this.http.get('/home/osp/FillLogoDetails?LANG_TYPE=O');
  //  }

  //  fillLogoDetailsE(): Observable <any>{
  //   return this.http.get('/home/osp/FillLogoDetails?LANG_TYPE=E');
  //  }

  //  fillMenuO(): Observable <any>{
  //   return this.http.get('/home/osp/FillMenu?LANG_TYPE=O&ParentMenuId=0');
  //  }

  //  fillMenuE(): Observable <any>{
  //   this.Menu = this.http.get('/osp/home/FillMenu?LANG_TYPE=E&ParentMenuId=0');    
  //   return this.Menu;
  //  }

  //  fillSubMenuO(): Observable <any>{
  //   this.SubMenu = this.http.get('/osp/home/FillSubMenu?LANG_TYPE=O');    
  //   return this.SubMenu;
  //  }   

  //  fillSubMenuE(): Observable <any>{
  //   this.SubMenu = this.http.get('/osp/home/FillSubMenu?LANG_TYPE=E');    
  //   return this.SubMenu;
  //  }

  // //  fillSubMenuE(i: any):Observable <any>{
  // //   return this.http.get('/home/FillMenu?LANG_TYPE=E&ParentMenuId='+i);
  // //  }

  //  fillNewsE(): Observable <any>{
  //   return this.http.get('/osp/home/FillNews?LANG_TYPE=E');
  //  }

  //  fillNewsO(): Observable <any>{
  //   return this.http.get('/osp/home/FillNews?LANG_TYPE=O');
  //  }

  //  fillContentE(): Observable <any>{
  //   return this.http.get('/osp/home/FillSlider?LANG_TYPE=E&WidgetId=3');
  //  }

  //  fillContentO(): Observable <any>{
  //   return this.http.get('/osp/home/FillSlider?LANG_TYPE=O&WidgetId=3');
  //  }

  //  Graph10Variety(): Observable <any>{
  //   return this.http.get('/osp/home/GRAPH10VARIETY');
  //  }

  //  Graph10Lic(): Observable <any>{
  //   return this.http.get('/osp/home/GRAPH10LIC');
  //  }

  //  GraphVarietiesList(Variety_Name : any): Observable <any>{
  //   return this.http.get('/osp/home/GRAPHVARIETYDTL?VARIETY_NAME='+Variety_Name);
  //  }

   getStockPricelist(): Observable <any>{
    return this.http.get(`/publicR/getStockPricelist`);
   }
   getDealerDetails(DistrictCode: any): Observable <any>{
    return this.http.get(`/publicR/getDealerDetails?DistrictCode=${DistrictCode}`);
   }
   getDistrict(): Observable <any>{
    return this.http.get(`/publicR/getDistrict`);
   }
   getBlock(distCode: any): Observable <any>{
    return this.http.get(`/publicR/getBlock?DistrictCode=${distCode}`);
   }
   getblockWiseDealer(blockCode: any): Observable <any>{
    return this.http.get(`/publicR/getblockWiseDealer?BlockCode=${blockCode}`);
   }
   getcropList(): Observable <any>{
    return this.http.get(`/publicR/getcropList`);
   }
   graphVariety( CropID: any): Observable <any>{
    return this.http.get(`/publicR/graphVariety?CropID=${CropID}`);
   }
   getStockPricelistAfter(): Observable <any>{
    return this.http.get(`/publicR/getStockPricelistAfter`);
   }

   setGraphVarietyList(data: any[]) {
    this.graphVarietyList = data;
  }

  getGraphVarietyList() {
    return this.graphVarietyList;
  }
}
