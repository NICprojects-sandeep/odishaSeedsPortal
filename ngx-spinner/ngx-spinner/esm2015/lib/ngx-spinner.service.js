import { __decorate, __metadata } from "tslib";
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import { filter } from 'rxjs/operators';
import { NgxSpinner, PRIMARY_SPINNER } from './ngx-spinner.enum';
import * as i0 from "@angular/core";
import * as ɵngcc0 from '@angular/core';
let NgxSpinnerService = class NgxSpinnerService {
    /**
     * Creates an instance of NgxSpinnerService.
     * @memberof NgxSpinnerService
     */
    constructor() {
        /**
         * Spinner observable
         *
         * @memberof NgxSpinnerService
         */
        this.spinnerObservable = new ReplaySubject(1);
    }
    /**
    * Get subscription of desired spinner
    * @memberof NgxSpinnerService
    **/
    getSpinner(name) {
        return this.spinnerObservable.asObservable().pipe(filter((x) => x && x.name === name));
    }
    /**
     * To show spinner
     *
     * @memberof NgxSpinnerService
     */
    show(name = PRIMARY_SPINNER, spinner) {
        const showPromise = new Promise((resolve, _reject) => {
            if (spinner && Object.keys(spinner).length) {
                spinner['name'] = name;
                this.spinnerObservable.next(new NgxSpinner(Object.assign(Object.assign({}, spinner), { show: true })));
                resolve(true);
            }
            else {
                this.spinnerObservable.next(new NgxSpinner({ name, show: true }));
                resolve(true);
            }
        });
        return showPromise;
    }
    /**
    * To hide spinner
    *
    * @memberof NgxSpinnerService
    */
    hide(name = PRIMARY_SPINNER, debounce = 0) {
        const hidePromise = new Promise((resolve, _reject) => {
            setTimeout(() => {
                this.spinnerObservable.next(new NgxSpinner({ name, show: false }));
                resolve(true);
            }, debounce);
        });
        return hidePromise;
    }
};
NgxSpinnerService.ɵfac = function NgxSpinnerService_Factory(t) { return new (t || NgxSpinnerService)(); };
NgxSpinnerService.ɵprov = i0.ɵɵdefineInjectable({ factory: function NgxSpinnerService_Factory() { return new NgxSpinnerService(); }, token: NgxSpinnerService, providedIn: "root" });
NgxSpinnerService = __decorate([ __metadata("design:paramtypes", [])
], NgxSpinnerService);
/*@__PURE__*/ (function () { ɵngcc0.ɵsetClassMetadata(NgxSpinnerService, [{
        type: Injectable,
        args: [{
                providedIn: 'root'
            }]
    }], function () { return []; }, null); })();
export { NgxSpinnerService };

//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoibmd4LXNwaW5uZXIuc2VydmljZS5qcyIsInNvdXJjZXMiOlsibmc6L25neC1zcGlubmVyL2xpYi9uZ3gtc3Bpbm5lci5zZXJ2aWNlLnRzIl0sIm5hbWVzIjpbXSwibWFwcGluZ3MiOiI7QUFBQSxPQUFPLEVBQUUsVUFBVSxFQUFFLE1BQU0sZUFBZSxDQUFDO0FBQzNDLE9BQU8sRUFBYyxhQUFhLEVBQUUsTUFBTSxNQUFNLENBQUM7QUFDakQsT0FBTyxFQUFFLE1BQU0sRUFBRSxNQUFNLGdCQUFnQixDQUFDO0FBQ3hDLE9BQU8sRUFBRSxVQUFVLEVBQUUsZUFBZSxFQUFXLE1BQU0sb0JBQW9CLENBQUM7OztBQUsxRSxJQUFhLGlCQUFpQixHQUE5QixNQUFhLGlCQUFpQjtJQU81Qjs7O09BR0c7SUFDSDtRQVZBOzs7O1dBSUc7UUFDSyxzQkFBaUIsR0FBRyxJQUFJLGFBQWEsQ0FBYSxDQUFDLENBQUMsQ0FBQztJQUs3QyxDQUFDO0lBQ2pCOzs7T0FHRztJQUNILFVBQVUsQ0FBQyxJQUFZO1FBQ3JCLE9BQU8sSUFBSSxDQUFDLGlCQUFpQixDQUFDLFlBQVksRUFBRSxDQUFDLElBQUksQ0FBQyxNQUFNLENBQUMsQ0FBQyxDQUFhLEVBQUUsRUFBRSxDQUFDLENBQUMsSUFBSSxDQUFDLENBQUMsSUFBSSxLQUFLLElBQUksQ0FBQyxDQUFDLENBQUM7SUFDckcsQ0FBQztJQUNEOzs7O09BSUc7SUFDSCxJQUFJLENBQUMsT0FBZSxlQUFlLEVBQUUsT0FBaUI7UUFDcEQsTUFBTSxXQUFXLEdBQUcsSUFBSSxPQUFPLENBQUMsQ0FBQyxPQUFPLEVBQUUsT0FBTyxFQUFFLEVBQUU7WUFDbkQsSUFBSSxPQUFPLElBQUksTUFBTSxDQUFDLElBQUksQ0FBQyxPQUFPLENBQUMsQ0FBQyxNQUFNLEVBQUU7Z0JBQzFDLE9BQU8sQ0FBQyxNQUFNLENBQUMsR0FBRyxJQUFJLENBQUM7Z0JBQ3ZCLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxJQUFJLENBQUMsSUFBSSxVQUFVLGlDQUFNLE9BQU8sS0FBRSxJQUFJLEVBQUUsSUFBSSxJQUFHLENBQUMsQ0FBQztnQkFDeEUsT0FBTyxDQUFDLElBQUksQ0FBQyxDQUFDO2FBQ2Y7aUJBQU07Z0JBQ0wsSUFBSSxDQUFDLGlCQUFpQixDQUFDLElBQUksQ0FBQyxJQUFJLFVBQVUsQ0FBQyxFQUFFLElBQUksRUFBRSxJQUFJLEVBQUUsSUFBSSxFQUFFLENBQUMsQ0FBQyxDQUFDO2dCQUNsRSxPQUFPLENBQUMsSUFBSSxDQUFDLENBQUM7YUFDZjtRQUNILENBQUMsQ0FBQyxDQUFDO1FBQ0gsT0FBTyxXQUFXLENBQUM7SUFDckIsQ0FBQztJQUNEOzs7O01BSUU7SUFDRixJQUFJLENBQUMsT0FBZSxlQUFlLEVBQUUsV0FBbUIsQ0FBQztRQUN2RCxNQUFNLFdBQVcsR0FBRyxJQUFJLE9BQU8sQ0FBQyxDQUFDLE9BQU8sRUFBRSxPQUFPLEVBQUUsRUFBRTtZQUNuRCxVQUFVLENBQUMsR0FBRyxFQUFFO2dCQUNkLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxJQUFJLENBQUMsSUFBSSxVQUFVLENBQUMsRUFBRSxJQUFJLEVBQUUsSUFBSSxFQUFFLEtBQUssRUFBRSxDQUFDLENBQUMsQ0FBQztnQkFDbkUsT0FBTyxDQUFDLElBQUksQ0FBQyxDQUFDO1lBQ2hCLENBQUMsRUFBRSxRQUFRLENBQUMsQ0FBQztRQUNmLENBQUMsQ0FBQyxDQUFDO1FBQ0gsT0FBTyxXQUFXLENBQUM7SUFDckIsQ0FBQztDQUNGOzBHQUFBOztBQW5EWSxpQkFBaUIsZUFFeEIsS0FMTCxVQUFVLENBQUMsVUFDVixVQUFVLHBDQUVSO0NBRlUsTUFBTSxNQUNuQixDQUFDLFhBQ1csaUJBQWlCLENBbUQ3Qjs7Ozs7O2dEQUNEO1NBcERhLGlCQUFpQiIsInNvdXJjZXNDb250ZW50IjpbImltcG9ydCB7IEluamVjdGFibGUgfSBmcm9tICdAYW5ndWxhci9jb3JlJztcbmltcG9ydCB7IE9ic2VydmFibGUsIFJlcGxheVN1YmplY3QgfSBmcm9tICdyeGpzJztcbmltcG9ydCB7IGZpbHRlciB9IGZyb20gJ3J4anMvb3BlcmF0b3JzJztcbmltcG9ydCB7IE5neFNwaW5uZXIsIFBSSU1BUllfU1BJTk5FUiwgU3Bpbm5lciB9IGZyb20gJy4vbmd4LXNwaW5uZXIuZW51bSc7XG5cbkBJbmplY3RhYmxlKHtcbiAgcHJvdmlkZWRJbjogJ3Jvb3QnXG59KVxuZXhwb3J0IGNsYXNzIE5neFNwaW5uZXJTZXJ2aWNlIHtcbiAgLyoqXG4gICAqIFNwaW5uZXIgb2JzZXJ2YWJsZVxuICAgKlxuICAgKiBAbWVtYmVyb2YgTmd4U3Bpbm5lclNlcnZpY2VcbiAgICovXG4gIHByaXZhdGUgc3Bpbm5lck9ic2VydmFibGUgPSBuZXcgUmVwbGF5U3ViamVjdDxOZ3hTcGlubmVyPigxKTtcbiAgLyoqXG4gICAqIENyZWF0ZXMgYW4gaW5zdGFuY2Ugb2YgTmd4U3Bpbm5lclNlcnZpY2UuXG4gICAqIEBtZW1iZXJvZiBOZ3hTcGlubmVyU2VydmljZVxuICAgKi9cbiAgY29uc3RydWN0b3IoKSB7IH1cbiAgLyoqXG4gICogR2V0IHN1YnNjcmlwdGlvbiBvZiBkZXNpcmVkIHNwaW5uZXJcbiAgKiBAbWVtYmVyb2YgTmd4U3Bpbm5lclNlcnZpY2VcbiAgKiovXG4gIGdldFNwaW5uZXIobmFtZTogc3RyaW5nKTogT2JzZXJ2YWJsZTxOZ3hTcGlubmVyPiB7XG4gICAgcmV0dXJuIHRoaXMuc3Bpbm5lck9ic2VydmFibGUuYXNPYnNlcnZhYmxlKCkucGlwZShmaWx0ZXIoKHg6IE5neFNwaW5uZXIpID0+IHggJiYgeC5uYW1lID09PSBuYW1lKSk7XG4gIH1cbiAgLyoqXG4gICAqIFRvIHNob3cgc3Bpbm5lclxuICAgKlxuICAgKiBAbWVtYmVyb2YgTmd4U3Bpbm5lclNlcnZpY2VcbiAgICovXG4gIHNob3cobmFtZTogc3RyaW5nID0gUFJJTUFSWV9TUElOTkVSLCBzcGlubmVyPzogU3Bpbm5lcikge1xuICAgIGNvbnN0IHNob3dQcm9taXNlID0gbmV3IFByb21pc2UoKHJlc29sdmUsIF9yZWplY3QpID0+IHtcbiAgICAgIGlmIChzcGlubmVyICYmIE9iamVjdC5rZXlzKHNwaW5uZXIpLmxlbmd0aCkge1xuICAgICAgICBzcGlubmVyWyduYW1lJ10gPSBuYW1lO1xuICAgICAgICB0aGlzLnNwaW5uZXJPYnNlcnZhYmxlLm5leHQobmV3IE5neFNwaW5uZXIoeyAuLi5zcGlubmVyLCBzaG93OiB0cnVlIH0pKTtcbiAgICAgICAgcmVzb2x2ZSh0cnVlKTtcbiAgICAgIH0gZWxzZSB7XG4gICAgICAgIHRoaXMuc3Bpbm5lck9ic2VydmFibGUubmV4dChuZXcgTmd4U3Bpbm5lcih7IG5hbWUsIHNob3c6IHRydWUgfSkpO1xuICAgICAgICByZXNvbHZlKHRydWUpO1xuICAgICAgfVxuICAgIH0pO1xuICAgIHJldHVybiBzaG93UHJvbWlzZTtcbiAgfVxuICAvKipcbiAgKiBUbyBoaWRlIHNwaW5uZXJcbiAgKlxuICAqIEBtZW1iZXJvZiBOZ3hTcGlubmVyU2VydmljZVxuICAqL1xuICBoaWRlKG5hbWU6IHN0cmluZyA9IFBSSU1BUllfU1BJTk5FUiwgZGVib3VuY2U6IG51bWJlciA9IDApIHtcbiAgICBjb25zdCBoaWRlUHJvbWlzZSA9IG5ldyBQcm9taXNlKChyZXNvbHZlLCBfcmVqZWN0KSA9PiB7XG4gICAgICBzZXRUaW1lb3V0KCgpID0+IHtcbiAgICAgICAgdGhpcy5zcGlubmVyT2JzZXJ2YWJsZS5uZXh0KG5ldyBOZ3hTcGlubmVyKHsgbmFtZSwgc2hvdzogZmFsc2UgfSkpO1xuICAgICAgICByZXNvbHZlKHRydWUpO1xuICAgICAgfSwgZGVib3VuY2UpO1xuICAgIH0pO1xuICAgIHJldHVybiBoaWRlUHJvbWlzZTtcbiAgfVxufVxuIl19