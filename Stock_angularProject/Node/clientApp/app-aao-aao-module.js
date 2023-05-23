(window["webpackJsonp"] = window["webpackJsonp"] || []).push([["app-aao-aao-module"],{

/***/ "8Eiy":
/*!******************************************************!*\
  !*** ./src/app/aao/dashboard/dashboard.component.ts ***!
  \******************************************************/
/*! exports provided: DashboardComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "DashboardComponent", function() { return DashboardComponent; });
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/core */ "fXoL");

class DashboardComponent {
    constructor() { }
    ngOnInit() {
    }
}
DashboardComponent.ɵfac = function DashboardComponent_Factory(t) { return new (t || DashboardComponent)(); };
DashboardComponent.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵdefineComponent"]({ type: DashboardComponent, selectors: [["app-dashboard"]], decls: 2, vars: 0, template: function DashboardComponent_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementStart"](0, "p");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵtext"](1, "dashboard works!");
        _angular_core__WEBPACK_IMPORTED_MODULE_0__["ɵɵelementEnd"]();
    } }, styles: ["\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbXSwibmFtZXMiOltdLCJtYXBwaW5ncyI6IiIsImZpbGUiOiJkYXNoYm9hcmQuY29tcG9uZW50LmNzcyJ9 */"] });


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
}
AaoService.ɵfac = function AaoService_Factory(t) { return new (t || AaoService)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵinject"](_angular_common_http__WEBPACK_IMPORTED_MODULE_2__["HttpClient"])); };
AaoService.ɵprov = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineInjectable"]({ token: AaoService, factory: AaoService.ɵfac, providedIn: 'root' });


/***/ }),

/***/ "Xzeh":
/*!*******************************************!*\
  !*** ./src/app/aao/aao-routing.module.ts ***!
  \*******************************************/
/*! exports provided: AaoRoutingModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AaoRoutingModule", function() { return AaoRoutingModule; });
/* harmony import */ var _angular_router__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/router */ "tyNb");
/* harmony import */ var _farmerdetailscheack_farmerdetailscheack_component__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./farmerdetailscheack/farmerdetailscheack.component */ "YQse");
/* harmony import */ var _dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./dashboard/dashboard.component */ "8Eiy");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! @angular/core */ "fXoL");





const roleObject = { role: 'AAOO' };
const routes = [
    {
        path: '',
        redirectTo: 'farmerdetailscheck',
        pathMatch: 'full'
    },
    {
        path: 'farmerdetailscheck',
        component: _farmerdetailscheack_farmerdetailscheack_component__WEBPACK_IMPORTED_MODULE_1__["FarmerdetailscheackComponent"],
        data: roleObject
    },
    {
        path: 'dashboard',
        component: _dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_2__["DashboardComponent"],
        data: roleObject
    }
];
class AaoRoutingModule {
}
AaoRoutingModule.ɵfac = function AaoRoutingModule_Factory(t) { return new (t || AaoRoutingModule)(); };
AaoRoutingModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵdefineNgModule"]({ type: AaoRoutingModule });
AaoRoutingModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵdefineInjector"]({ imports: [[_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"].forChild(routes)], _angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_3__["ɵɵsetNgModuleScope"](AaoRoutingModule, { imports: [_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]], exports: [_angular_router__WEBPACK_IMPORTED_MODULE_0__["RouterModule"]] }); })();


/***/ }),

/***/ "YQse":
/*!**************************************************************************!*\
  !*** ./src/app/aao/farmerdetailscheack/farmerdetailscheack.component.ts ***!
  \**************************************************************************/
/*! exports provided: FarmerdetailscheackComponent */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "FarmerdetailscheackComponent", function() { return FarmerdetailscheackComponent; });
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/forms */ "3Pt+");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! @angular/core */ "fXoL");
/* harmony import */ var src_app_Services_aao_service__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! src/app/Services/aao.service */ "CT7D");
/* harmony import */ var ngx_toastr__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ngx-toastr */ "5eHb");
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/common */ "ofXK");






function FarmerdetailscheackComponent_div_78_label_5_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "label", 45);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](1, "b");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](2, "No Record Found !!!!!!!!!");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
} }
function FarmerdetailscheackComponent_div_78_div_6_tr_54_td_44_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](1, "Successfully Transferred");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
} }
function FarmerdetailscheackComponent_div_78_div_6_tr_54_td_45_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
} if (rf & 2) {
    const y_r6 = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵnextContext"]().$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.Reject_Reason);
} }
function FarmerdetailscheackComponent_div_78_div_6_tr_54_td_46_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
} if (rf & 2) {
    const y_r6 = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵnextContext"]().$implicit;
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.pendingat);
} }
function FarmerdetailscheackComponent_div_78_div_6_tr_54_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "tr");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](1, "th", 54);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](3, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](4);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](5, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](6);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](7, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](8);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](9, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](10);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵpipe"](11, "date");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](12, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](13);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](14, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](15);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](16, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](17);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](18, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](19);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](20, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](21);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](22, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](23);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](24, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](25);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](26, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](27);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](28, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](29);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](30, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](31);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](32, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](33);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](34, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](35);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](36, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](37);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](38, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](39);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](40, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](41);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](42, "td");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](43);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](44, FarmerdetailscheackComponent_div_78_div_6_tr_54_td_44_Template, 2, 0, "td", 55);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](45, FarmerdetailscheackComponent_div_78_div_6_tr_54_td_45_Template, 2, 1, "td", 55);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](46, FarmerdetailscheackComponent_div_78_div_6_tr_54_td_46_Template, 2, 1, "td", 55);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
} if (rf & 2) {
    const y_r6 = ctx.$implicit;
    const i_r7 = ctx.index;
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](i_r7 + 1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.FARMER_ID);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.VCHFARMERNAME);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.TRANSACTION_ID);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵpipeBind2"](11, 24, y_r6.UPDATED_ON, "dd-MM-yyyy"));
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](3);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.vch_DistrictName);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.vch_blockname);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.vch_gpname);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.vch_villagename);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.Category_Name);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate"](y_r6.Crop_Name);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.Variety_Name, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.bag_size_kg, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.no_of_bags, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.TOT_QTL, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.TOT_SUB_AMOUNT_GOI, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.TOT_SUB_AMOUNT_SP, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.SUBSIDY_AMOUNT, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.BankPost_Office_Account_number || "--", " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.Bank_Post_Office_Name || "--", " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", y_r6.Status, " ");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngIf", y_r6.Status == "PAID");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngIf", y_r6.Status == "NOT PAID");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngIf", y_r6.Status == "Pending");
} }
function FarmerdetailscheackComponent_div_78_div_6_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "div", 46);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](1, "table", 47);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](2, "thead");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](3, "tr");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](4, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](5, "Sl No.");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](6, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](7, "Farmer ID");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](8, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](9, "Farmer Name");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](10, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](11, "Transaction ID");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](12, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](13, "Purchased Date");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](14, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](15, "District");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](16, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](17, "Block");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](18, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](19, "Gp");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](20, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](21, "Village");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](22, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](23, "Category Name");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](24, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](25, "Crop Name");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](26, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](27, "Variety Name");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](28, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](29, "Bag size");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](30, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](31, "No. of Bags");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](32, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](33, "Purchase QTY (in Quintals)");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](34, "th", 49);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](35, "Subsidy Amount");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](36, "th", 50);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](37, "Bank Deatils");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](38, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](39, "Status");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](40, "th", 48);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](41, "Reason");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](42, "tr");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](43, "th", 51);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](44, "GOI");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](45, "th", 51);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](46, "State Plan");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](47, "th", 51);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](48, "Total");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](49, "th", 51);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](50, "Account No.");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](51, "th", 51);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](52, "Name of Bank");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](53, "tbody");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](54, FarmerdetailscheackComponent_div_78_div_6_tr_54_Template, 47, 27, "tr", 52);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](55, "br");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](56, "tfoot", 53);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r4 = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵnextContext"](2);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](54);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngForOf", ctx_r4.paymentStatusByFarmeIdDetails);
} }
function FarmerdetailscheackComponent_div_78_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "div", 15);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](1, "div", 16);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](2, "h5");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](3, "FarmerDetails");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](4, "div", 42);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](5, FarmerdetailscheackComponent_div_78_label_5_Template, 3, 0, "label", 43);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](6, FarmerdetailscheackComponent_div_78_div_6_Template, 57, 1, "div", 44);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](7, "br");
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
} if (rf & 2) {
    const ctx_r1 = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵnextContext"]();
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](5);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngIf", ctx_r1.paymentStatusByFarmeIdDetails.length == 0);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngIf", ctx_r1.paymentStatusByFarmeIdDetails.length > 0);
} }
function FarmerdetailscheackComponent_div_79_Template(rf, ctx) { if (rf & 1) {
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "div", 56);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](1, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](2, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](3, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](4, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](5, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](6, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](7, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](8, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](9, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](10, "div", 57);
    _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
} }
class FarmerdetailscheackComponent {
    constructor(fb, aaoService, toastr) {
        this.fb = fb;
        this.aaoService = aaoService;
        this.toastr = toastr;
        this.paymentStatusByFarmeIdDetails = [];
        this.FarmerIdPre = '';
        this.loder = false;
        this.showdetails = false;
        this.aaoFarmerSearchForm = this.fb.group({
            selectedFinancialYear: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_0__["Validators"].required],
            selectedSeasons: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_0__["Validators"].required],
            selectedFarmerId: ['', _angular_forms__WEBPACK_IMPORTED_MODULE_0__["Validators"].required]
        });
    }
    ngOnInit() {
        this.getFarmeridPrefix();
    }
    getFarmeridPrefix() {
        this.aaoService.GetDistCodeFromAOO().subscribe(data => {
            this.FarmerIdPre = data.result.Short_Name;
            console.log(data, 'kkkkk');
        });
    }
    paymentStatusByFarmeId() {
        // console.log(this.aaoFarmerSearchForm.value.selectedFarmerId);
        // this.aaoFarmerSearchForm.patchValue({
        //   selectedFarmerId: this.FarmerIdPre+'/'+this.aaoFarmerSearchForm.value.selectedFarmerId
        // });
        this.showdetails = false;
        if (this.aaoFarmerSearchForm.valid) {
            this.loder = true;
            const obj = { selectedFinancialYear: '', selectedSeasons: '', selectedFarmerId: '' };
            obj.selectedFinancialYear = this.aaoFarmerSearchForm.value.selectedFinancialYear;
            obj.selectedSeasons = this.aaoFarmerSearchForm.value.selectedSeasons;
            // obj.selectedFarmerId = 'SAM/52674' //this.FarmerIdPre+'/'+this.aaoFarmerSearchForm.value.selectedFarmerId;
            // obj.selectedFarmerId = 'SAM/2682'
            // obj.selectedFarmerId = 'SAM/64682'
            obj.selectedFarmerId = this.FarmerIdPre + '/' + this.aaoFarmerSearchForm.value.selectedFarmerId;
            this.aaoService.paymentStatusByFarmeId(obj).subscribe(data => {
                this.paymentStatusByFarmeIdDetails = data;
                this.showdetails = true;
                this.loder = false;
            });
        }
        else {
            this.toastr.warning(`Please select all field. `);
        }
    }
}
FarmerdetailscheackComponent.ɵfac = function FarmerdetailscheackComponent_Factory(t) { return new (t || FarmerdetailscheackComponent)(_angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](_angular_forms__WEBPACK_IMPORTED_MODULE_0__["FormBuilder"]), _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](src_app_Services_aao_service__WEBPACK_IMPORTED_MODULE_2__["AaoService"]), _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdirectiveInject"](ngx_toastr__WEBPACK_IMPORTED_MODULE_3__["ToastrService"])); };
FarmerdetailscheackComponent.ɵcmp = _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵdefineComponent"]({ type: FarmerdetailscheackComponent, selectors: [["app-farmerdetailscheack"]], decls: 80, vars: 4, consts: [[1, "page-header"], [1, "page-block"], [1, "row", "align-items-center"], [1, "col-md-8"], [1, "page-header-title", 2, "margin-top", "10px"], [1, "m-b-10"], [1, "col-md-4"], [1, "breadcrumb"], [1, "breadcrumb-item"], ["href", "/aao/farmerdetailscheck"], [1, "fa", "fa-home"], [1, "page-body"], [1, "container-fluid"], [1, "row"], [1, "col-md-12"], [1, "card"], [1, "card-header"], [1, "card-header-left"], [1, "card-header-text"], ["novalidate", "", "autocomplete", "off", "role", "form", 3, "formGroup"], ["aaoFarmerSearchFormID", ""], [1, "form-group", "form-primary", "form-static-label", "col-md-4"], [1, "col-form-label"], [1, "text-danger"], [1, "input-group", 2, "margin-top", "10px"], ["id", "name", 1, "input-group-prepend"], [1, "input-group-text"], [1, ""], ["id", "farmerid", "type", "number", "min", "0", "oninput", "validity.valid||(value='');", "value", "farmerid", "placeholder", "Enter Farmer ID", "title", "Enter your Farmer ID", "data-toggle", "tooltip", "formControlName", "selectedFarmerId", 1, "form-control"], ["name", "select", "formControlName", "selectedFinancialYear", 1, "form-control"], ["value", "", "selected", "", "disabled", ""], ["value", "2021-22"], ["value", "2022-23"], ["value", "2023-24"], ["name", "select", "formControlName", "selectedSeasons", 1, "form-control"], ["value", "K"], ["value", "R"], [1, "form-group", "form-primary", "form-static-label", "col-md-5"], ["type", "button", 1, "btn", "btn-primary", "btn-md", "waves-effect", "waves-light", 3, "click"], [1, "icofont", "icofont-search"], ["class", "card", 4, "ngIf"], ["class", "center", 4, "ngIf"], [1, "card-block", "table-border-style", 2, "text-align", "center"], ["for", "", "style", "color: red; ", 4, "ngIf"], ["class", "tableFixHead table-responsive", 4, "ngIf"], ["for", "", 2, "color", "red"], [1, "tableFixHead", "table-responsive"], [1, "table", "table-bordered", 2, "width", "100%"], ["rowspan", "3"], ["colspan", "3"], ["colspan", "2"], ["scope", "col"], [4, "ngFor", "ngForOf"], [2, "text-align", "center"], ["scope", "row"], [4, "ngIf"], [1, "center"], [1, "wave"]], template: function FarmerdetailscheackComponent_Template(rf, ctx) { if (rf & 1) {
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](0, "div", 0);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](1, "div", 1);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](2, "div", 2);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](3, "div", 3);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](4, "div", 4);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](5, "h4", 5);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](6, "Farmer Payment Report");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](7, "div", 6);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](8, "ul", 7);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](9, "li", 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](10, "a", 9);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](11, "i", 10);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](12, "li", 8);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](13, "a", 9);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](14, "Farmer Payment Report");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](15, "div", 11);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](16, "br");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](17, "div", 12);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](18, "div", 12);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](19, "div", 13);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](20, "div", 14);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](21, "div", 15);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](22, "div", 16);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](23, "div", 17);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](24, "h5", 18);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](25, "Enter Farmer ID ,Financial Year & Season");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](26, "form", 19, 20);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](28, "div", 12);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](29, "div", 13);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](30, "div", 21);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](31, "label", 22);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](32, "Farmer Id ");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](33, "span", 23);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](34, "*");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](35, ":-");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](36, "div", 24);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](37, "span", 25);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](38, "label", 26);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](39, "i", 27);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](40);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](41, "input", 28);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](42, "\u00A0 ");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](43, "div", 21);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](44, "label", 22);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](45, "Financial Year ");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](46, "span", 23);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](47, "*");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](48, ":-");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](49, "select", 29);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](50, "option", 30);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](51, "Select Financial Year");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](52, "option", 31);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](53, "2021-22");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](54, "option", 32);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](55, "2022-23");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](56, "option", 33);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](57, "2023-24");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](58, "div", 21);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](59, "label", 22);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](60, "Seasons ");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](61, "span", 23);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](62, "*");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](63, ":-");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](64, "select", 34);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](65, "option", 30);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](66, "Select Seasons");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](67, "option", 35);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](68, "Kharif");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](69, "option", 36);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](70, "Rabi");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](71, "div", 13);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](72, "div", 37);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](73, "div", 21);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementStart"](74, "button", 38);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵlistener"]("click", function FarmerdetailscheackComponent_Template_button_click_74_listener() { return ctx.paymentStatusByFarmeId(); });
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](75, "i", 39);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtext"](76, "Search");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelement"](77, "div", 21);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](78, FarmerdetailscheackComponent_div_78_Template, 8, 2, "div", 40);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵelementEnd"]();
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtemplate"](79, FarmerdetailscheackComponent_div_79_Template, 11, 0, "div", 41);
    } if (rf & 2) {
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](26);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("formGroup", ctx.aaoFarmerSearchForm);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](14);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵtextInterpolate1"]("", ctx.FarmerIdPre, "/");
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](38);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngIf", ctx.showdetails);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵadvance"](1);
        _angular_core__WEBPACK_IMPORTED_MODULE_1__["ɵɵproperty"]("ngIf", ctx.loder);
    } }, directives: [_angular_forms__WEBPACK_IMPORTED_MODULE_0__["ɵangular_packages_forms_forms_ba"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["NgControlStatusGroup"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["FormGroupDirective"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["NumberValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["DefaultValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["NgControlStatus"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["FormControlName"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["SelectControlValueAccessor"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["NgSelectOption"], _angular_forms__WEBPACK_IMPORTED_MODULE_0__["ɵangular_packages_forms_forms_z"], _angular_common__WEBPACK_IMPORTED_MODULE_4__["NgIf"], _angular_common__WEBPACK_IMPORTED_MODULE_4__["NgForOf"]], pipes: [_angular_common__WEBPACK_IMPORTED_MODULE_4__["DatePipe"]], styles: ["bodY[_ngcontent-%COMP%] {\r\n    margin: 0;\r\n    padding: 0;\r\n    box-sizing: border-box;\r\n  }\r\n  .center[_ngcontent-%COMP%] {\r\n    height: 20vh;\r\n    display: flex;\r\n    justify-content: center;\r\n    align-items: center;\r\n    \r\n  }\r\n  .wave[_ngcontent-%COMP%] {\r\n    width: 5px;\r\n    height: 100px;\r\n    background: linear-gradient(45deg, rgb(240, 11, 125), #dd0f0f);\r\n    margin: 10px;\r\n    animation: wave 1s linear infinite;\r\n    border-radius: 20px;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(2) {\r\n    animation-delay: 0.1s;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(3) {\r\n    animation-delay: 0.2s;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(4) {\r\n    animation-delay: 0.3s;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(5) {\r\n    animation-delay: 0.4s;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(6) {\r\n    animation-delay: 0.5s;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(7) {\r\n    animation-delay: 0.6s;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(8) {\r\n    animation-delay: 0.7s;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(9) {\r\n    animation-delay: 0.8s;\r\n  }\r\n  .wave[_ngcontent-%COMP%]:nth-child(10) {\r\n    animation-delay: 0.9s;\r\n  }\r\n  @keyframes wave {\r\n    0% {\r\n      transform: scale(0);\r\n    }\r\n    50% {\r\n      transform: scale(1);\r\n    }\r\n    100% {\r\n      transform: scale(0);\r\n    }\r\n  }\n/*# sourceMappingURL=data:application/json;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbImZhcm1lcmRldGFpbHNjaGVhY2suY29tcG9uZW50LmNzcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtJQUNJLFNBQVM7SUFDVCxVQUFVO0lBQ1Ysc0JBQXNCO0VBQ3hCO0VBQ0E7SUFDRSxZQUFZO0lBQ1osYUFBYTtJQUNiLHVCQUF1QjtJQUN2QixtQkFBbUI7SUFDbkIsc0JBQXNCO0VBQ3hCO0VBQ0E7SUFDRSxVQUFVO0lBQ1YsYUFBYTtJQUNiLDhEQUE4RDtJQUM5RCxZQUFZO0lBQ1osa0NBQWtDO0lBQ2xDLG1CQUFtQjtFQUNyQjtFQUNBO0lBQ0UscUJBQXFCO0VBQ3ZCO0VBQ0E7SUFDRSxxQkFBcUI7RUFDdkI7RUFDQTtJQUNFLHFCQUFxQjtFQUN2QjtFQUNBO0lBQ0UscUJBQXFCO0VBQ3ZCO0VBQ0E7SUFDRSxxQkFBcUI7RUFDdkI7RUFDQTtJQUNFLHFCQUFxQjtFQUN2QjtFQUNBO0lBQ0UscUJBQXFCO0VBQ3ZCO0VBQ0E7SUFDRSxxQkFBcUI7RUFDdkI7RUFDQTtJQUNFLHFCQUFxQjtFQUN2QjtFQUVBO0lBQ0U7TUFDRSxtQkFBbUI7SUFDckI7SUFDQTtNQUNFLG1CQUFtQjtJQUNyQjtJQUNBO01BQ0UsbUJBQW1CO0lBQ3JCO0VBQ0YiLCJmaWxlIjoiZmFybWVyZGV0YWlsc2NoZWFjay5jb21wb25lbnQuY3NzIiwic291cmNlc0NvbnRlbnQiOlsiYm9kWSB7XHJcbiAgICBtYXJnaW46IDA7XHJcbiAgICBwYWRkaW5nOiAwO1xyXG4gICAgYm94LXNpemluZzogYm9yZGVyLWJveDtcclxuICB9XHJcbiAgLmNlbnRlciB7XHJcbiAgICBoZWlnaHQ6IDIwdmg7XHJcbiAgICBkaXNwbGF5OiBmbGV4O1xyXG4gICAganVzdGlmeS1jb250ZW50OiBjZW50ZXI7XHJcbiAgICBhbGlnbi1pdGVtczogY2VudGVyO1xyXG4gICAgLyogYmFja2dyb3VuZDogIzAwMDsgKi9cclxuICB9XHJcbiAgLndhdmUge1xyXG4gICAgd2lkdGg6IDVweDtcclxuICAgIGhlaWdodDogMTAwcHg7XHJcbiAgICBiYWNrZ3JvdW5kOiBsaW5lYXItZ3JhZGllbnQoNDVkZWcsIHJnYigyNDAsIDExLCAxMjUpLCAjZGQwZjBmKTtcclxuICAgIG1hcmdpbjogMTBweDtcclxuICAgIGFuaW1hdGlvbjogd2F2ZSAxcyBsaW5lYXIgaW5maW5pdGU7XHJcbiAgICBib3JkZXItcmFkaXVzOiAyMHB4O1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoMikge1xyXG4gICAgYW5pbWF0aW9uLWRlbGF5OiAwLjFzO1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoMykge1xyXG4gICAgYW5pbWF0aW9uLWRlbGF5OiAwLjJzO1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoNCkge1xyXG4gICAgYW5pbWF0aW9uLWRlbGF5OiAwLjNzO1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoNSkge1xyXG4gICAgYW5pbWF0aW9uLWRlbGF5OiAwLjRzO1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoNikge1xyXG4gICAgYW5pbWF0aW9uLWRlbGF5OiAwLjVzO1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoNykge1xyXG4gICAgYW5pbWF0aW9uLWRlbGF5OiAwLjZzO1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoOCkge1xyXG4gICAgYW5pbWF0aW9uLWRlbGF5OiAwLjdzO1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoOSkge1xyXG4gICAgYW5pbWF0aW9uLWRlbGF5OiAwLjhzO1xyXG4gIH1cclxuICAud2F2ZTpudGgtY2hpbGQoMTApIHtcclxuICAgIGFuaW1hdGlvbi1kZWxheTogMC45cztcclxuICB9XHJcbiAgXHJcbiAgQGtleWZyYW1lcyB3YXZlIHtcclxuICAgIDAlIHtcclxuICAgICAgdHJhbnNmb3JtOiBzY2FsZSgwKTtcclxuICAgIH1cclxuICAgIDUwJSB7XHJcbiAgICAgIHRyYW5zZm9ybTogc2NhbGUoMSk7XHJcbiAgICB9XHJcbiAgICAxMDAlIHtcclxuICAgICAgdHJhbnNmb3JtOiBzY2FsZSgwKTtcclxuICAgIH1cclxuICB9Il19 */"] });


/***/ }),

/***/ "n4wo":
/*!***********************************!*\
  !*** ./src/app/aao/aao.module.ts ***!
  \***********************************/
/*! exports provided: AaoModule */
/***/ (function(module, __webpack_exports__, __webpack_require__) {

"use strict";
__webpack_require__.r(__webpack_exports__);
/* harmony export (binding) */ __webpack_require__.d(__webpack_exports__, "AaoModule", function() { return AaoModule; });
/* harmony import */ var _angular_common__WEBPACK_IMPORTED_MODULE_0__ = __webpack_require__(/*! @angular/common */ "ofXK");
/* harmony import */ var _aao_routing_module__WEBPACK_IMPORTED_MODULE_1__ = __webpack_require__(/*! ./aao-routing.module */ "Xzeh");
/* harmony import */ var _dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_2__ = __webpack_require__(/*! ./dashboard/dashboard.component */ "8Eiy");
/* harmony import */ var _farmerdetailscheack_farmerdetailscheack_component__WEBPACK_IMPORTED_MODULE_3__ = __webpack_require__(/*! ./farmerdetailscheack/farmerdetailscheack.component */ "YQse");
/* harmony import */ var _angular_forms__WEBPACK_IMPORTED_MODULE_4__ = __webpack_require__(/*! @angular/forms */ "3Pt+");
/* harmony import */ var _angular_core__WEBPACK_IMPORTED_MODULE_5__ = __webpack_require__(/*! @angular/core */ "fXoL");






class AaoModule {
}
AaoModule.ɵfac = function AaoModule_Factory(t) { return new (t || AaoModule)(); };
AaoModule.ɵmod = _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdefineNgModule"]({ type: AaoModule });
AaoModule.ɵinj = _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵdefineInjector"]({ imports: [[
            _angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
            _aao_routing_module__WEBPACK_IMPORTED_MODULE_1__["AaoRoutingModule"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_4__["FormsModule"],
            _angular_forms__WEBPACK_IMPORTED_MODULE_4__["ReactiveFormsModule"],
        ]] });
(function () { (typeof ngJitMode === "undefined" || ngJitMode) && _angular_core__WEBPACK_IMPORTED_MODULE_5__["ɵɵsetNgModuleScope"](AaoModule, { declarations: [_dashboard_dashboard_component__WEBPACK_IMPORTED_MODULE_2__["DashboardComponent"],
        _farmerdetailscheack_farmerdetailscheack_component__WEBPACK_IMPORTED_MODULE_3__["FarmerdetailscheackComponent"]], imports: [_angular_common__WEBPACK_IMPORTED_MODULE_0__["CommonModule"],
        _aao_routing_module__WEBPACK_IMPORTED_MODULE_1__["AaoRoutingModule"],
        _angular_forms__WEBPACK_IMPORTED_MODULE_4__["FormsModule"],
        _angular_forms__WEBPACK_IMPORTED_MODULE_4__["ReactiveFormsModule"]] }); })();


/***/ })

}]);
//# sourceMappingURL=app-aao-aao-module.js.map