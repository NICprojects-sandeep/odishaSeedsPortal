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
    return this.http.get(`${this.serverURL}/stock/dealer/GetDealerLicenceByDistCodeUserType`);
   }
}
