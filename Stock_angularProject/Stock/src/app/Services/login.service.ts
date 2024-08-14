import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class LoginService {
  serverURL: string = environment.serverURL;
  constructor(
    private http: HttpClient
  ) { }

  // setCaptchaStatus(code: any) {
  //   this.captchSource.next(code);
  // };

  setRole(role: string) {
    return localStorage.setItem('role', role);
  }

  getRole() {
    return localStorage.getItem('role');
  }

  setUsername(username: string) {
    console.log(username,'usernameusernameusername');
    
    console.log(localStorage.setItem('username', username),'localStorage.setItemusername');
    
    return localStorage.setItem('username', username);
  }

  getUsername() {
    console.log(localStorage.getItem('username'),'localStorage.getItem');
    
    return localStorage.getItem('username');
  }
  setFullname(fullname: string) {
    console.log(fullname,'usernameusernameusernameusernameusername');
    
    return localStorage.setItem('fullname', fullname);
  }

  getFullname() {
    return localStorage.getItem('fullname');
  }

  clearLocalStorage() {
    localStorage.clear();
    return true;
  }

  generateCaptchaAndSalt(type: number, length: number): Observable<any> {
    return this.http.get(`${this.serverURL}/auth/generateCaptchaAndSalt/${type}/${length}`, {
      withCredentials: true
    });
  }

  CheckLogIn(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/auth/CheckLogIn`, data, {
      withCredentials: true
    });
  }

  getUserDetails(role: string): Observable<any> {
    let data={
      role: role,
      username:this.getUsername()
    }
    // console.log(this.getUsername());
    //?role=` + role+ `&username=` + this.getUsername()
    return this.http.post(`${this.serverURL}/auth/getUserDetails`,data, {
      withCredentials: true
    });
  }

  signOut(): Observable<any> {
    return this.http.post(`${this.serverURL}/auth/signOut`, {
    }, {
      withCredentials: true
    });
  }
  getmarqueData(): Observable<any> {
    return this.http.get(`${this.serverURL}/auth/getmarqueData`, {
      withCredentials: true
    });
  }
  OneDealerLogin(data: any): Observable<any> {
    return this.http.post(`${this.serverURL}/auth/OneDealerLogin`, data, {
      withCredentials: true
    });
  }
  sendotp(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/auth/sendotp`,data, {
      withCredentials: true
    });
  }
  confirmotp(data:any): Observable<any> {
    return this.http.post(`${this.serverURL}/auth/confirmotp`,data, {
      withCredentials: true
    });
  }
}
