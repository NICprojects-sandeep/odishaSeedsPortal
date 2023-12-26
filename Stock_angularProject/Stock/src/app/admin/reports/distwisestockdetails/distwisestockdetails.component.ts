import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import * as XLSX from 'xlsx';

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
  sumTotalACTUAL_RECEIVE: any = 0;
  sumTotalACTUAL_SALE: any = 0;
  sumTotalSaleQty: any = 0;
  fileName:any='';
  showpage: boolean = false;

  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillDistrict();
    this.FillCategoryId();
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
    this.sumTotalSaleQty =0
    this.service.distwisestockdetails(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedCrop,).subscribe(data => {
      this.distwisestockdetailsdata = data;
      console.log(this.distwisestockdetailsdata);
      this.showpage=true;
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
          var n = (this.distwisestockdetailsdata[i].ACTUAL_RECEIVE == undefined || this.distwisestockdetailsdata[i].ACTUAL_RECEIVE == null || this.distwisestockdetailsdata[i].ACTUAL_RECEIVE == '') ? 0 : this.distwisestockdetailsdata[i].ACTUAL_RECEIVE;
          console.log(parseFloat(this.sumTotalACTUAL_RECEIVE) , parseFloat(n));
          
          this.sumTotalACTUAL_RECEIVE = parseFloat(this.sumTotalACTUAL_RECEIVE) + parseFloat(n);
        }
        if (this.distwisestockdetailsdata[i].hasOwnProperty('ACTUAL_SALE')) {
          var n = (this.distwisestockdetailsdata[i].ACTUAL_SALE == undefined || this.distwisestockdetailsdata[i].ACTUAL_SALE == null || this.distwisestockdetailsdata[i].ACTUAL_SALE == '') ? 0 : this.distwisestockdetailsdata[i].ACTUAL_SALE;
          this.sumTotalACTUAL_SALE = parseFloat(this.sumTotalACTUAL_SALE) + parseFloat(n);
        }
        if (this.distwisestockdetailsdata[i].hasOwnProperty('SaleQty')) {
          var n = (this.distwisestockdetailsdata[i].SaleQty == undefined || this.distwisestockdetailsdata[i].SaleQty == null || this.distwisestockdetailsdata[i].SaleQty == '') ? 0 : this.distwisestockdetailsdata[i].SaleQty;
          this.sumTotalSaleQty = parseFloat(this.sumTotalSaleQty) + parseFloat(n);
        }
      }
      
    })}
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
}
