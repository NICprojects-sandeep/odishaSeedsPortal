import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import * as XLSX from 'xlsx';
@Component({
  selector: 'app-statestockposition',
  templateUrl: './statestockposition.component.html',
  styleUrls: ['./statestockposition.component.css']
})
export class StatestockpositionComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedCropCatagory: any = [];
  SelectedDistrict: any = ''
  SelectedCrop: any = [];
  selectedToDate: any = ''
  maxdate: any;
  showpage: boolean = false;
  fileName:any='';

  sumTotalOSSC_Recv: any = 0.00;
  sumTotalOSSC_SaleDealer: any = 0.00;
  sumTotalOSSC_SalePacks: any = 0.00;
  sumTotalOSSC_Stock: any = 0.00;
  SumTotalOSSC_SaleDealerOSSC_SalePacks:any=0.00;

  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  getAllDistrict: any = [];
  stateStockPositionData: any = [];
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillCropCategory();
    this.FillDistrict();
    this.maxdate = this.getDate();
  }
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  FillFinYr() {
    this.getAllFinYr = []
    this.service.allFillFinYr().subscribe(data => {
      this.getAllFinYr = data;
    })
  }
  FillCropCategory() {
    this.getAllCatagory = []
    this.service.FillCropCategory().subscribe(data => {
      this.getAllCatagory = data;
    })
  }
  FillDistrict() {
    this.getAllDistrict = []
    this.service.FillDistrict().subscribe(data => {
      this.getAllDistrict = data;
      this.getAllDistrict.unshift({
        Dist_Code: 0, Dist_Name: 'All'
      });
      this.SelectedDistrict = this.getAllDistrict[0].Dist_Code;
    })
  }
  FillCropByCategoryId() {
    this.getAllCrop = []
    this.service.FillCropByCategoryId(this.SelectedCropCatagory).subscribe(data => {
      this.getAllCrop = data;
    })
  }
  fillStateStockPosition() {
    if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
      && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
      && this.SelectedCropCatagory !== null && this.SelectedCropCatagory !== '' && this.SelectedCropCatagory !== undefined
      && this.SelectedDistrict !== null && this.SelectedDistrict !== '' && this.SelectedDistrict !== undefined
      && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined) {
      this.spinner.show();
      let data = {
        SelectedFinancialYear: this.SelectedFinancialYear,
        SelectedSeason: this.SelectedSeason,
        SelectedCropCatagory: this.SelectedCropCatagory,
        SelectedCrop: this.SelectedCrop,
        selectedToDate: this.selectedToDate,
        SelectedDistrict: this.SelectedDistrict
      }
      this.service.fillStateStockPosition(data).subscribe(data => {
        // console.log(data);
        this.stateStockPositionData = data;

        this.stateStockPositionData.forEach((i: any) => {

          var a = (i.OSSC_SaleDealer == undefined || i.OSSC_SaleDealer == null || i.OSSC_SaleDealer == '') ? 0 : i.OSSC_SaleDealer;
          var b = (i.OSSC_SalePacks == undefined || i.OSSC_SalePacks == null || i.OSSC_SalePacks == '') ? 0 : i.OSSC_SalePacks;
          var c = (i.OSSC_GtransOwnTrPend == undefined || i.OSSC_GtransOwnTrPend == null || i.OSSC_GtransOwnTrPend == '') ? 0 : i.OSSC_GtransOwnTrPend;
          var d = (i.OSSC_OthrGtransOwnTrPend == undefined || i.OSSC_OthrGtransOwnTrPend == null || i.OSSC_OthrGtransOwnTrPend == '') ? 0 : i.OSSC_OthrGtransOwnTrPend;
          var e = (i.OAIC_SalePacks == undefined || i.OAIC_SalePacks == null || i.OAIC_SalePacks == '') ? 0 : i.OAIC_SalePacks;
          var f = (i.OSSC_Stock == undefined || i.OSSC_Stock == null || i.OSSC_Stock == '') ? 0 : i.OSSC_Stock;
          var g = (i.OAIC_Stock == undefined || i.OAIC_Stock == null || i.OAIC_Stock == '') ? 0 : i.OAIC_Stock;



          // i.OSSC_GtransOwnTrPend=parseFloat(c);
          // i.OSSC_OthrGtransOwnTrPend=parseFloat(d);

          i.OSSC_SaleDealerOSSC_SalePacks = parseFloat(a) + parseFloat(b);
          i.OSSC_GtransOwnTrPendOSSC_OthrGtransOwnTrPend = parseFloat(c) + parseFloat(d);
          i.OAIC_SalePacksOSSC_SalePacks = parseFloat(e) + parseFloat(b);
          i.OAIC_SalePacksOSSC_SalePacksOSSC_SaleDealer = parseFloat(e) + parseFloat(b) + parseFloat(a);
          i.OSSC_StockOAIC_Stock = parseFloat(f) + parseFloat(g);

        })
        this.stateStockPositionData.forEach((i: any) => {
          if (i.hasOwnProperty('OSSC_Recv') && i.hasOwnProperty('OSSC_SaleDealer') && i.hasOwnProperty('OSSC_SalePacks') && i.hasOwnProperty('OSSC_Stock')) {

            if (i.hasOwnProperty('OSSC_Recv')) {
              var m = (i.OSSC_Recv == undefined || i.OSSC_Recv == null || i.OSSC_Recv == '') ? 0 : i.OSSC_Recv;
              this.sumTotalOSSC_Recv = (parseFloat(this.sumTotalOSSC_Recv) + parseFloat(m)).toFixed(2);

            }
            if (i.hasOwnProperty('OSSC_SaleDealer')) {
              var n = (i.OSSC_SaleDealer == undefined || i.OSSC_SaleDealer == null || i.OSSC_SaleDealer == '') ? 0 : i.OSSC_SaleDealer;
              this.sumTotalOSSC_SaleDealer = (parseFloat(this.sumTotalOSSC_SaleDealer) + parseFloat(n)).toFixed(2);
            }
            if (i.hasOwnProperty('OSSC_SalePacks')) {
              var o = (i.OSSC_SalePacks == undefined || i.OSSC_SalePacks == null || i.OSSC_SalePacks == '') ? 0 : i.OSSC_SalePacks;
              this.sumTotalOSSC_SalePacks = (parseFloat(this.sumTotalOSSC_SalePacks) + parseFloat(o)).toFixed(2);
            }
            if (i.hasOwnProperty('OSSC_SalePacks')) {
              var o = (i.OSSC_SalePacks == undefined || i.OSSC_SalePacks == null || i.OSSC_SalePacks == '') ? 0 : i.OSSC_SalePacks;
              this.SumTotalOSSC_SaleDealerOSSC_SalePacks = (parseFloat(this.sumTotalOSSC_SalePacks) + parseFloat(this.sumTotalOSSC_SaleDealer)).toFixed(2);
            }
            if (i.hasOwnProperty('OSSC_SaleDealer')) {
              var p = (i.OSSC_Stock == undefined || i.OSSC_Stock == null || i.OSSC_Stock == '') ? 0 : i.OSSC_Stock;
              this.sumTotalOSSC_Stock = (parseFloat(this.sumTotalOSSC_Stock) + parseFloat(p)).toFixed(2);
            }
          }

        });

        this.showpage = true;
        this.spinner.hide();
        // this.getAllCrop = data;
      })
    }
    else {
      this.toastr.warning('Please select all field.');
    }
  }
  exportexcel(): void {    
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();

    this.fileName = 'StateStockReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('tables');
    const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

    /* generate workbook and add the worksheet */
    const wb: XLSX.WorkBook = XLSX.utils.book_new();
    XLSX.utils.book_append_sheet(wb, ws, 'DealerReport');

    /* save to file */
    XLSX.writeFile(wb, this.fileName);

  }
}