(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["common"],{

/***/ "CT7D":
/*!*****************************************!*\
  !*** ./src/app/Services/aao.service.ts ***!
  \*****************************************/
/*! exports provided: AaoService */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AaoService", function() { return AaoService; });
/* harmony import */ var src_environments_environment__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! src/environments/environment */ "AytR");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var _angular_common_http__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/common/http */ "tk/3");



class AaoService {
    constructor(http) {
        this.http = http;
        this.serverURL = src_environments_environment__WEBPACK_IMPORTED_MODULE_0__["environment"].serverURL;
    }
    paymentStatusByFarmeId(data) {
        return this.http.post(`${this.serverURL}/aao/paymentStatusByFarmeId`, data, {
            withCredentials: true
        });
    }
    GetDistCodeFromAOO() {
        return this.http.get(`${this.serverURL}/aao/GetDistCodeFromAOO`, {
            withCredentials: true
        });
    }
    fillfARMERiD() {
        return this.http.get(`${this.serverURL}/aao/fillfARMERiD`, {
            withCredentials: true
        });
    }
    gerFarmerDetailsForSamallMarginUpdatation(NICFARMERID) {
        return this.http.get(`${this.serverURL}/aao/gerFarmerDetailsForSamallMarginUpdatation?NICFARMERID=${NICFARMERID}`, {
            withCredentials: true
        });
    }
    farmerTypeUpdate(data) {
        return this.http.post(`${this.serverURL}/aao/farmerTypeUpdate`, data, {
            withCredentials: true
        });
    }
    delalerwisestockCheack(data) {
        return this.http.post(`${this.serverURL}/aao/delalerwisestockCheack`, data, {
            withCredentials: true
        });
    }
}
AaoService.ɵfac = function AaoService_Factory(t) { return new (t || AaoService)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵinject"](_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"])); };
AaoService.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjectable"]({ token: AaoService, factory: AaoService.ɵfac, providedIn: 'root' });


/***/ })

}]);
//# sourceMappingURL=common.js.map