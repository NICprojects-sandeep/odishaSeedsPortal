import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-subsidyinvolovement',
  templateUrl: './subsidyinvolovement.component.html',
  styleUrls: ['./subsidyinvolovement.component.css']
})
export class SubsidyinvolovementComponent implements OnInit {
  getAllFinYr: any = [];
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  showpage: boolean = false;
  subsidyInvolovementdata: any = [];
  sumTotalDIST_FARMER: any = 0;
  sumTotalSUBSIDY_QTL: any = 0;
  sumTotalSUBSIDY_AMOUNT: any = 0;
  currentfinyr: any = '';
  currentSeason: any = '';
  currentdate:any='';
  fileName:any='';
  constructor(private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
  }
  FillFinYr() {
    this.getAllFinYr = []
    this.service.allFillFinYr().subscribe(data => {
      this.getAllFinYr = data;
    })
  }
  subsidyInvolovementdetails() {
    this.showpage = false;
    this.sumTotalDIST_FARMER = 0;
    this.sumTotalSUBSIDY_QTL = 0;
    this.sumTotalSUBSIDY_AMOUNT = 0;
    this.currentfinyr = '';
    this.currentSeason = '';
    this.currentdate='';
    if (this.SelectedFinancialYear && this.SelectedSeason) {
      this.spinner.show();
      this.service.subsidyInvolovementdetails(this.SelectedFinancialYear, this.SelectedSeason).subscribe(data => {
        this.currentfinyr = data.fyear;
        this.currentSeason = data.season;
        this.currentdate=data.currentdate;
        this.subsidyInvolovementdata = data.result;
        this.spinner.hide();

        this.showpage = true;
        if (this.subsidyInvolovementdata.length > 0) {
          for (let i = 0; i < this.subsidyInvolovementdata.length; i++) {
            if (this.subsidyInvolovementdata[i].hasOwnProperty('DIST_FARMER')) {
              var k = (this.subsidyInvolovementdata[i].DIST_FARMER == undefined || this.subsidyInvolovementdata[i].DIST_FARMER == null || this.subsidyInvolovementdata[i].DIST_FARMER == '') ? 0 : this.subsidyInvolovementdata[i].DIST_FARMER;
              this.sumTotalDIST_FARMER = (parseFloat(this.sumTotalDIST_FARMER) + parseFloat(k)).toFixed(2);
            }
            if (this.subsidyInvolovementdata[i].hasOwnProperty('SUBSIDY_QTL')) {
              var l = (this.subsidyInvolovementdata[i].SUBSIDY_QTL == undefined || this.subsidyInvolovementdata[i].SUBSIDY_QTL == null || this.subsidyInvolovementdata[i].SUBSIDY_QTL == '') ? 0 : this.subsidyInvolovementdata[i].SUBSIDY_QTL;
              this.sumTotalSUBSIDY_QTL = (parseFloat(this.sumTotalSUBSIDY_QTL) + parseFloat(l)).toFixed(2);
            }
            if (this.subsidyInvolovementdata[i].hasOwnProperty('SUBSIDY_AMOUNT')) {
              var m = (this.subsidyInvolovementdata[i].SUBSIDY_AMOUNT == undefined || this.subsidyInvolovementdata[i].SUBSIDY_AMOUNT == null || this.subsidyInvolovementdata[i].SUBSIDY_AMOUNT == '') ? 0 : this.subsidyInvolovementdata[i].SUBSIDY_AMOUNT;
              this.sumTotalSUBSIDY_AMOUNT = (parseFloat(this.sumTotalSUBSIDY_AMOUNT) + parseFloat(m)).toFixed(2);
            }
          }
        }

      })
    }
    else {
      this.toastr.warning('Please select all field.');
    }
  }
  exportexcel(): void {
    this.fileName = '';
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();
    this.fileName = 'SubsidyInvolovementReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('tableId');
    if (element !== null && element !== undefined) {
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'SubsidyInvolovementReport');
      /* save to file */
      XLSX.writeFile(wb, this.fileName);
    }
  }
}