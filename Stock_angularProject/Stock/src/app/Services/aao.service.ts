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
    console.log(data);
    console.log(`${this.serverURL}`);
    
    
    return this.http.post(`${this.serverURL}/aao/paymentStatusByFarmeId`,data, {
      withCredentials: true
    });
  }
}
