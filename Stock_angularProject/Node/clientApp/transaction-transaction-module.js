(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["transaction-transaction-module"],{

/***/ "DOs9":
/*!************************************************************!*\
  !*** ./src/app/seed-dbt/transaction/transaction.module.ts ***!
  \************************************************************/
/*! exports provided: TransactionModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TransactionModule", function() { return TransactionModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _transaction_routing_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./transaction-routing.module */ "M/nT");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! @angular/core */ "fXoL");



class TransactionModule {
}
TransactionModule.ɵfac = function TransactionModule_Factory(t) { return new (t || TransactionModule)(); };
TransactionModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineNgModule"]({ type: TransactionModule });
TransactionModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵdefineInjector"]({ imports: [[
            _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
            _transaction_routing_module__WEBPACK_IMPORTED_MODULE_1__["TransactionRoutingModule"]
        ]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_2__["ɵɵsetNgModuleScope"](TransactionModule, { imports: [_angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
        _transaction_routing_module__WEBPACK_IMPORTED_MODULE_1__["TransactionRoutingModule"]] }); })();


/***/ }),

/***/ "M/nT":
/*!********************************************************************!*\
  !*** ./src/app/seed-dbt/transaction/transaction-routing.module.ts ***!
  \********************************************************************/
/*! exports provided: TransactionRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "TransactionRoutingModule", function() { return TransactionRoutingModule; });
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "fXoL");



const routes = [
    {
        path: 'farmerSale',
        loadChildren: () => Promise.all(/*! import() | farmersale-farmersale-module */[__webpack_require__.e("default~app-farmer-sale-farmer-sale-module~farmersale-farmersale-module"), __webpack_require__.e("farmersale-farmersale-module")]).then(__webpack_require__.bind(null, /*! ./farmersale/farmersale.module */ "bCaG")).then(module => module.FarmersaleModule)
    },
];
class TransactionRoutingModule {
}
TransactionRoutingModule.ɵfac = function TransactionRoutingModule_Factory(t) { return new (t || TransactionRoutingModule)(); };
TransactionRoutingModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineNgModule"]({ type: TransactionRoutingModule });
TransactionRoutingModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjector"]({ imports: [[_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"].forChild(routes)], _angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵsetNgModuleScope"](TransactionRoutingModule, { imports: [_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]], exports: [_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]] }); })();


/***/ })

}]);
//# sourceMappingURL=transaction-transaction-module.js.map