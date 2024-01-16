import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import * as XLSX from 'xlsx';
import { MatDialog } from '@angular/material/dialog';
import { BlockwisestockdetailsComponent } from './blockwisestockdetails/blockwisestockdetails.component';


@Component({
  selector: 'app-distwisestockdetails',
  templateUrl: './distwisestockdetails.component.html',
  styleUrls: ['./distwisestockdetails.component.css']
})
export class DistwisestockdetailsComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedCrop: any = '';
  SelectedDistrict: any = '';

  getAllFinYr: any = [];
  getAllCrop: any = [];
  getAllDistrict: any = [];
  distwisestockdetailsdata: any = [];
  blockwisestockdetailsdata: any = [];
  dealerwisestockdetailsdata: any = [];
  sumTotalACTUAL_RECEIVE: any = 0;
  sumTotalACTUAL_SALE: any = 0;
  sumTotalSaleQty: any = 0;
  sumTotalRCVb: any = 0;
  sumTotalSaleQtyb: any = 0;
  sumTotalSALEB: any = 0;
  fileName: any = '';
  showpage: boolean = false;
  BlockWise: boolean = false;
  DealerWise: boolean = false;
  sumTotal_ACTUAL_RECEIVE: any = 0;
  sumTotal_SaleQtyb: any = 0;
  sumTotal_ACTUAL_SALE: any = 0;
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillDistrict();
    this.FillCategoryId();
    this.distwisestockdetails();
  }
  FillFinYr() {
    this.getAllFinYr = []
    this.service.allFillFinYr().subscribe(data => {
      this.getAllFinYr = data;
    })
  }
  FillDistrict() {
    this.getAllDistrict = []
    this.service.FillDistrict().subscribe(data => {
      this.getAllDistrict = data;
      console.log(this.getAllDistrict);
      this.getAllDistrict.unshift({
        Dist_Code: 0, Dist_Name: 'All'
      });
      this.SelectedDistrict = this.getAllDistrict[0].Dist_Code

    })
  }
  FillCategoryId() {
    this.getAllCrop = []
    this.service.FillCategoryId().subscribe(data => {
      this.getAllCrop = data;
    })
  }
  distwisestockdetails() {
    this.distwisestockdetailsdata = [];
    this.sumTotalACTUAL_RECEIVE = 0
    this.sumTotalACTUAL_SALE = 0
    this.sumTotalSaleQty = 0
    // this.service.distwisestockdetails(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedCrop,).subscribe(data => {
    this.service.distwisestockdetails('2023-24', 'R', 'C002').subscribe(data => {

      this.distwisestockdetailsdata = data;
      console.log(this.distwisestockdetailsdata);
      this.showpage = true;
      // for (let i = 0; i < this.distwisestockdetailsdata.length; i++) {
      //   if (i.hasOwnProperty('ACTUAL_RECEIVE')) {
      //     var n = (i.ACTUAL_RECEIVE == undefined || i.ACTUAL_RECEIVE == null || i.ACTUAL_RECEIVE == '') ? 0 : i.ACTUAL_RECEIVE;
      //     this.sumTotalACTUAL_RECEIVE = (this.sumTotalACTUAL_RECEIVE) + parseInt(n);
      //   }
      //   if (i.hasOwnProperty('ACTUAL_SALE')) {
      //     var n = (i.ACTUAL_SALE == undefined || i.ACTUAL_SALE == null || i.ACTUAL_SALE == '') ? 0 : i.ACTUAL_SALE;
      //     this.sumTotalACTUAL_SALE = (this.sumTotalACTUAL_SALE) + parseInt(n);
      //   }
      //   if (i.hasOwnProperty('SaleQty')) {
      //     var n = (i.SaleQty == undefined || i.SaleQty == null || i.SaleQty == '') ? 0 : i.SaleQty;
      //     this.sumTotalSaleQty = (this.sumTotalSaleQty) + parseInt(n);
      //   }

      // }
      for (let i = 0; i < this.distwisestockdetailsdata.length; i++) {
        if (this.distwisestockdetailsdata[i].hasOwnProperty('ACTUAL_RECEIVE')) {
          var k = (this.distwisestockdetailsdata[i].ACTUAL_RECEIVE == undefined || this.distwisestockdetailsdata[i].ACTUAL_RECEIVE == null || this.distwisestockdetailsdata[i].ACTUAL_RECEIVE == '') ? 0 : this.distwisestockdetailsdata[i].ACTUAL_RECEIVE;
          console.log(parseFloat(this.sumTotalACTUAL_RECEIVE), parseFloat(k));

          this.sumTotalACTUAL_RECEIVE = (parseFloat(this.sumTotalACTUAL_RECEIVE) + parseFloat(k)).toFixed(2);
        }
        if (this.distwisestockdetailsdata[i].hasOwnProperty('ACTUAL_SALE')) {
          var l = (this.distwisestockdetailsdata[i].ACTUAL_SALE == undefined || this.distwisestockdetailsdata[i].ACTUAL_SALE == null || this.distwisestockdetailsdata[i].ACTUAL_SALE == '') ? 0 : this.distwisestockdetailsdata[i].ACTUAL_SALE;
          this.sumTotalACTUAL_SALE = (parseFloat(this.sumTotalACTUAL_SALE) + parseFloat(l)).toFixed(2);
        }
        if (this.distwisestockdetailsdata[i].hasOwnProperty('SaleQty')) {
          var m = (this.distwisestockdetailsdata[i].SaleQty == undefined || this.distwisestockdetailsdata[i].SaleQty == null || this.distwisestockdetailsdata[i].SaleQty == '') ? 0 : this.distwisestockdetailsdata[i].SaleQty;
          this.sumTotalSaleQty = (parseFloat(this.sumTotalSaleQty) + parseFloat(m)).toFixed(2);
        }
      }

    })
  }
  blockwisestockdetails(x: any) {
    this.blockwisestockdetailsdata = [];
    this.sumTotalRCVb = 0
    this.sumTotalSaleQtyb = 0
    this.sumTotalSALEB = 0
    this.service.blockwisestockdetails(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedCrop, x.DIST_CODE).subscribe(data => {

      this.blockwisestockdetailsdata = data;
      this.BlockWise = true;
      this.showpage = false;
      this.DealerWise = false;

      for (let i = 0; i < this.blockwisestockdetailsdata.length; i++) {
        if (this.blockwisestockdetailsdata[i].hasOwnProperty('RCV')) {
          var m = (this.blockwisestockdetailsdata[i].RCV == undefined || this.blockwisestockdetailsdata[i].RCV == null || this.blockwisestockdetailsdata[i].RCV == '') ? 0 : this.blockwisestockdetailsdata[i].RCV;
          console.log(parseFloat(this.sumTotalRCVb), parseFloat(m));

          this.sumTotalRCVb = (parseFloat(this.sumTotalRCVb) + parseFloat(m)).toFixed(2);
        }
        if (this.blockwisestockdetailsdata[i].hasOwnProperty('SaleQty')) {
          var n = (this.blockwisestockdetailsdata[i].SaleQty == undefined || this.blockwisestockdetailsdata[i].SaleQty == null || this.blockwisestockdetailsdata[i].SaleQty == '') ? 0 : this.blockwisestockdetailsdata[i].SaleQty;
          this.sumTotalSaleQtyb = (parseFloat(this.sumTotalSaleQtyb) + parseFloat(n)).toFixed(2);
        }
        if (this.blockwisestockdetailsdata[i].hasOwnProperty('SALE')) {
          var o = (this.blockwisestockdetailsdata[i].SALE == undefined || this.blockwisestockdetailsdata[i].SALE == null || this.blockwisestockdetailsdata[i].SALE == '') ? 0 : this.blockwisestockdetailsdata[i].SALE;
          this.sumTotalSALEB = (parseFloat(this.sumTotalSALEB) + parseFloat(o)).toFixed(2);
        }
      }

    })
  }
  exportexcel(): void {
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();

    this.fileName = 'DealerPacssaleReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('tables');
    if (element !== null && element !== undefined) {
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'DealerPacssaleReport');

      /* save to file */
      XLSX.writeFile(wb, this.fileName);
    }


  }
  openBlockWiseStockDetails() {
    this.BlockWise = true;
    this.showpage = false;
  }
  openDealerWiseStockDetails(x: any) {
    console.log(x);
    this.dealerwisestockdetailsdata = [];
    this.sumTotal_ACTUAL_RECEIVE =0
    this.sumTotal_SaleQtyb = 0
    this.sumTotal_ACTUAL_SALE = 0
    this.service.dealerwisestockdetails(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedCrop, x.AAO_CODE).subscribe(data => {

      this.dealerwisestockdetailsdata = data;
      this.BlockWise = false;
      this.showpage = false;
      this.DealerWise = true;

      for (let i = 0; i < this.dealerwisestockdetailsdata.length; i++) {
        if (this.dealerwisestockdetailsdata[i].hasOwnProperty('ACTUAL_RECEIVE')) {
          console.log(this.dealerwisestockdetailsdata[i].ACTUAL_RECEIVE);
          
          var m = (this.dealerwisestockdetailsdata[i].ACTUAL_RECEIVE == undefined || this.dealerwisestockdetailsdata[i].ACTUAL_RECEIVE == null || this.dealerwisestockdetailsdata[i].ACTUAL_RECEIVE == '') ? 0 : this.dealerwisestockdetailsdata[i].ACTUAL_RECEIVE;
          console.log(this.sumTotal_ACTUAL_RECEIVE,this.dealerwisestockdetailsdata[i].ACTUAL_RECEIVE,m);
          
          this.sumTotal_ACTUAL_RECEIVE = (parseFloat(this.sumTotal_ACTUAL_RECEIVE) + parseFloat(m)).toFixed(2);
        }
        if (this.dealerwisestockdetailsdata[i].hasOwnProperty('SaleQty')) {
          var n = (this.dealerwisestockdetailsdata[i].SaleQty == undefined || this.dealerwisestockdetailsdata[i].SaleQty == null || this.dealerwisestockdetailsdata[i].SaleQty == '') ? 0 : this.dealerwisestockdetailsdata[i].SaleQty;
          this.sumTotal_SaleQtyb = (parseFloat(this.sumTotal_SaleQtyb) + parseFloat(n)).toFixed(2);
        }
        if (this.dealerwisestockdetailsdata[i].hasOwnProperty('ACTUAL_SALE')) {
          var o = (this.dealerwisestockdetailsdata[i].ACTUAL_SALE == undefined || this.dealerwisestockdetailsdata[i].ACTUAL_SALE == null || this.dealerwisestockdetailsdata[i].ACTUAL_SALE == '') ? 0 : this.dealerwisestockdetailsdata[i].ACTUAL_SALE;
          this.sumTotal_ACTUAL_SALE = (parseFloat(this.sumTotal_ACTUAL_SALE) + parseFloat(o)).toFixed(2);
        }
      }

    })
  }
  backtoDistWiseStock() {
    this.BlockWise = false;
    this.showpage = true;
    this.DealerWise = false;
  }
  backtoBlockWiseStock() {
    this.BlockWise = true;
    this.showpage = false;
    this.DealerWise = false;
  }
}
