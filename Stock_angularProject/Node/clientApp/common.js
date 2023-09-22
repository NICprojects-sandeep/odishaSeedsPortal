(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["common"],{

/***/ "3nLL":
/*!************************************************************!*\
  !*** ./src/app/custom-directives/numbersonly.directive.ts ***!
  \************************************************************/
/*! exports provided: NumbersOnlyDirective */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "NumbersOnlyDirective", function() { return NumbersOnlyDirective; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");

class NumbersOnlyDirective {
    constructor(_el) {
        this._el = _el;
        this.specialKeys = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight', 'Del', 'Delete', 'Enter'];
    }
    onInputChange(event) {
        console.log('hiiii');
        if (this.specialKeys.indexOf(event.key) !== -1) {
            return;
        }
        const initialValue2 = this._el.nativeElement.value;
        const initialValue1 = initialValue2.replace(/^[00]+$/g, '0');
        if ((/^[0][1-9]+$/g).test(initialValue1)) {
            const initialValue = initialValue1.replace(/^0+/g, '');
            this._el.nativeElement.value = initialValue.replace(/[^\d]+$/g, '');
            if (initialValue !== this._el.nativeElement.value) {
                event.stopPropagation();
            }
        }
        else {
            this._el.nativeElement.value = initialValue1.replace(/[^\d]+$/g, '');
            if (initialValue1 !== this._el.nativeElement.value) {
                event.stopPropagation();
            }
        }
    }
}
NumbersOnlyDirective.ɵfac = function NumbersOnlyDirective_Factory(t) { return new (t || NumbersOnlyDirective)(_angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdirectiveInject"](_angular_core__WEBPACK_IMPORTED_MODULE_0__["ElementRef"])); };
NumbersOnlyDirective.ɵdir = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineDirective"]({ type: NumbersOnlyDirective, selectors: [["", "appNumbersonly", ""]], hostBindings: function NumbersOnlyDirective_HostBindings(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵlistener"]("input", function NumbersOnlyDirective_input_HostBindingHandler($event) { return ctx.onInputChange($event); });
    } } });


/***/ }),

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


/***/ }),

/***/ "PCNd":
/*!*****************************************!*\
  !*** ./src/app/shared/shared.module.ts ***!
  \*****************************************/
/*! exports provided: SharedModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SharedModule", function() { return SharedModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _custom_directives_numbersonly_directive__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ../custom-directives/numbersonly.directive */ "3nLL");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "fXoL");



class SharedModule {
}
SharedModule.ɵfac = function SharedModule_Factory(t) { return new (t || SharedModule)(); };
SharedModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineNgModule"]({ type: SharedModule });
SharedModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineInjector"]({ imports: [[
            _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"]
        ]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵsetNgModuleScope"](SharedModule, { declarations: [_custom_directives_numbersonly_directive__WEBPACK_IMPORTED_MODULE_1__["NumbersOnlyDirective"]], imports: [_angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"]], exports: [_custom_directives_numbersonly_directive__WEBPACK_IMPORTED_MODULE_1__["NumbersOnlyDirective"]] }); })();


/***/ })

}]);
//# sourceMappingURL=common.js.map