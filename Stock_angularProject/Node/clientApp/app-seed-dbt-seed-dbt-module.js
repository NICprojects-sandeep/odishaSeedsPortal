(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["app-seed-dbt-seed-dbt-module"],{

/***/ "EKW6":
/*!*********************************************!*\
  !*** ./src/app/seed-dbt/seed-dbt.module.ts ***!
  \*********************************************/
/*! exports provided: SeedDBTModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SeedDBTModule", function() { return SeedDBTModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _seed_dbt_routing_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./seed-dbt-routing.module */ "V94N");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "fXoL");



class SeedDBTModule {
}
SeedDBTModule.ɵfac = function SeedDBTModule_Factory(t) { return new (t || SeedDBTModule)(); };
SeedDBTModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineNgModule"]({ type: SeedDBTModule });
SeedDBTModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineInjector"]({ imports: [[
            _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
            _seed_dbt_routing_module__WEBPACK_IMPORTED_MODULE_1__["SeedDBTRoutingModule"]
        ]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵsetNgModuleScope"](SeedDBTModule, { imports: [_angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
        _seed_dbt_routing_module__WEBPACK_IMPORTED_MODULE_1__["SeedDBTRoutingModule"]] }); })();


/***/ }),

/***/ "V94N":
/*!*****************************************************!*\
  !*** ./src/app/seed-dbt/seed-dbt-routing.module.ts ***!
  \*****************************************************/
/*! exports provided: SeedDBTRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "SeedDBTRoutingModule", function() { return SeedDBTRoutingModule; });
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "fXoL");



const routes = [
    {
        path: 'transaction',
        loadChildren: () => __webpack_require__.e(/*! import() | transaction-transaction-module */ "transaction-transaction-module").then(__webpack_require__.bind(null, /*! ./transaction/transaction.module */ "DOs9")).then(module => module.TransactionModule)
    },
];
class SeedDBTRoutingModule {
}
SeedDBTRoutingModule.ɵfac = function SeedDBTRoutingModule_Factory(t) { return new (t || SeedDBTRoutingModule)(); };
SeedDBTRoutingModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineNgModule"]({ type: SeedDBTRoutingModule });
SeedDBTRoutingModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjector"]({ imports: [[_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"].forChild(routes)], _angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵsetNgModuleScope"](SeedDBTRoutingModule, { imports: [_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]], exports: [_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]] }); })();


/***/ })

}]);
//# sourceMappingURL=app-seed-dbt-seed-dbt-module.js.map