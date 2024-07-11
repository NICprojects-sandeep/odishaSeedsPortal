import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, CanActivateChild, CanDeactivate, CanLoad, Route, Router, RouterStateSnapshot, UrlSegment, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../Services/login.service';
import { AppRoutingModule } from '../app-routing.module';

@Injectable({
  providedIn: 'root'
})
export class AuthguardGuard implements CanActivate {
  constructor(
    private authService: LoginService,
    private router: Router,
    private toastr: ToastrService,
    private route: AppRoutingModule
  ) { }

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return new Promise((resolve, reject) => {
      console.log(route,'route.data.role');
      
      this.authService.getUserDetails(route.data.role).subscribe((result: any) => {  
        console.log(result,'result');
              
        if (!result.isLoggedIn) {
          this.authService.signOut().subscribe((res: any) => {
            this.authService.clearLocalStorage();
            this.router.navigate(['/']);
          }, (error) => this.toastr.error(error.statusText, error.status));
          resolve(false);
        } else {
          resolve(true);
        }
      }, (error) => {
        console.log(error);
        
        reject(new Error('An error has occurred! Please try after sometime.'));
        this.toastr.error(error.statusText, error.status);
      });
    });
  }

  canActivateChild(
    childRoute: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return childRoute.url.length ? this.canActivate(childRoute, state) : true;
  }

  canDeactivate(
    component: unknown,
    currentRoute: ActivatedRouteSnapshot,
    currentState: RouterStateSnapshot,
    nextState?: RouterStateSnapshot
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true;
  }

  canLoad(
    route: Route,
    segments: UrlSegment[]
  ): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    return true;
  }
}
