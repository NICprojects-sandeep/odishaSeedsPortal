import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  serverURL: string = environment.serverURL;
  constructor(private http: HttpClient) { }


  allFillFinYr(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/allFillFinYr`, {
      withCredentials: true
    });
  }
  FillCropCategory(): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillCropCategory`, {
      withCredentials: true
    });
  }
  FillCropByCategoryId(SelectedCropCatagory:any): Observable<any> {
    return this.http.get(`${this.serverURL}/admin/FillCropByCategoryId?SelectedCropCatagory=` + SelectedCropCatagory, {
      withCredentials: true
    });
  }
}
