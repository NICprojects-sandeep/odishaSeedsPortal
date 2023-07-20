import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class DealerService {
  serverURL: string = environment.serverURL;
  constructor(private http: HttpClient) { }

  GetDealerLicenceByDistCodeUserType(): Observable <any>{
    return this.http.get(`${this.serverURL}/dealer/GetDealerLicenceByDistCodeUserType`,{
      withCredentials: true
    });
   }
   GetDealerLicenceByDistCodeUserTypePacs(): Observable <any>{
    return this.http.get(`${this.serverURL}/dealer/GetDealerLicenceByDistCodeUserTypePacs`,{
      withCredentials: true
    });
   }
   FillFinYr(): Observable <any>{
    return this.http.get(`${this.serverURL}/dealer/FILLFINYR`,{
      withCredentials: true
    });
   }
   FillSeason(FinYr : any): Observable <any>{
    return this.http.get(`${this.serverURL}/dealer/FILLSEASSION?FIN_YR=`+FinYr,{
      withCredentials: true
    });
   }
   FILL_GODOWN(): Observable <any>{
    return this.http.get(`${this.serverURL}/dealer/FILL_GODOWN`,{
      withCredentials: true
    });
   }
   FILL_CROPCATAGORY(selectedGodown:any): Observable <any>{
    return this.http.get(`${this.serverURL}/dealer/FILL_CROPCATAGORY?selectedGodown=`+selectedGodown,{
      withCredentials: true
    });
   }
   FILLCROPNAME(selectedCategory:any): Observable <any>{
    return this.http.get(`${this.serverURL}/dealer/FILLCROPNAME?selectedCategory=`+selectedCategory,{
      withCredentials: true
    });
   }
   FILLCROPVARIETY(selectedCrop:any): Observable <any>{
    return this.http.get(`${this.serverURL}/dealer/FILLCROPVARIETY?selectedCrop=`+selectedCrop,{
      withCredentials: true
    });
   }
}
