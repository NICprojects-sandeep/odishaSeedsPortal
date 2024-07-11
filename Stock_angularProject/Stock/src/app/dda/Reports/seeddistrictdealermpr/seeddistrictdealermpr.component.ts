import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import { DdaService } from 'src/app/Services/dda.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-seeddistrictdealermpr',
  templateUrl: './seeddistrictdealermpr.component.html',
  styleUrls: ['./seeddistrictdealermpr.component.css']
})
export class SeeddistrictdealermprComponent implements OnInit {
  SelectedDistrict: any = '';
  SelectedBlock: any = '';
  SelectedYear: any = '';
  SelectedMonth: any = '';
  selectedDistrictMPRDetails: any = [];
  getFillDistrictList: any = [];
  getgetMPRDistrictList: any = [];
  getFillDistrictWiseBlockList: any = [];
  getFillMPRYearList: any = [];
  getselectedDistrictMPRList: any = [];
  getselectedDistrictMPRList1: any = [];
  getselectedDistrictMPRList2:any=[];
  fileName: any = '';
  sDistrict: any = '';
  opStOssc: any = 0;
  opStOther: any = 0;
  opStOSSCRcv: any = 0;
  opStOtherRcv: any = 0;
  opStOSSCSold: any = 0;
  opStOtherSold: any = 0;
  opStOSSCTotal: any = 0;
  opStOtherTotal: any = 0;
  firststpage: boolean = false;
  Secondpage: boolean = false;
  Thirdpage:boolean=false;
  allpage:boolean=false;
  name:any=''
  constructor(
    private fb: FormBuilder,
    private service: DdaService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.getMPRDistrict();
  }
  getMPRDistrict() {
    this.getgetMPRDistrictList = [];
    this.service.getMPRDistrict().subscribe(
      async (data: any) => {
        this.getgetMPRDistrictList = data.mprDistrict;

        if (data != null) {
          setTimeout(async () => {
            this.sDistrict = this.getgetMPRDistrictList.find((x: any) => parseInt(x.districtCode) === data.logindistrict);
            this.SelectedDistrict = this.sDistrict;
            this.FillDistrictWiseBlock();
            this.FillMPRYear();
          }, 5);
        }
        else {
          this.spinner.hide();
        }

      },
      (error) => this.toastr.error(error.statusText, error.status)
    );

  }
  FillDistrictWiseBlock() {
    this.getFillDistrictWiseBlockList = [];
    return new Promise((resolve, reject) => {
      try {
        this.service.FillDistrictWiseBlock(this.sDistrict.districtCode).subscribe((result: any) => {
          this.getFillDistrictWiseBlockList = result;
          this.getFillDistrictWiseBlockList.unshift({
            blockCode: 'All', blockName: 'All'
          });
          resolve(result);
        }, (error) => this.toastr.error(error.statusText, error.status));
      } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
      }
    });
  }
  FillMPRYear() {
    this.getFillMPRYearList = [];
    return new Promise((resolve, reject) => {
      try {
        this.service.FillMPRYear().subscribe((result: any) => {
          this.getFillMPRYearList = result;
          resolve(result);
        }, (error) => this.toastr.error(error.statusText, error.status));
      } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
      }
    });
  }
  selectedDistrictMPR() {
    if (this.SelectedDistrict.districtName != undefined && this.SelectedDistrict.districtName != null && this.SelectedDistrict.districtName != '' &&
      this.SelectedBlock.blockCode != undefined && this.SelectedBlock.blockCode != null && this.SelectedBlock.blockCode != '' &&
      this.SelectedYear.year != undefined && this.SelectedYear.year != null && this.SelectedYear.year != '' &&
      this.SelectedMonth != undefined && this.SelectedMonth != null && this.SelectedMonth != ''
    ) {
      let data = {
        SelectedDistrict: this.SelectedDistrict.districtName,
        SelectedBlock: this.SelectedBlock.blockCode,
        SelectedYear: this.SelectedYear.year,
        SelectedMonth: this.SelectedMonth,

      }
      this.getselectedDistrictMPRList = [];
      this.getselectedDistrictMPRList1 = [];
      this.getselectedDistrictMPRList2 =[];
      this.firststpage = true;
      this.Secondpage = false;
      this.Thirdpage=false;
      this.allpage=true;
      return new Promise((resolve, reject) => {
        try {
          this.service.selectedDistrictMPR(data).subscribe((result: any) => {
            this.getselectedDistrictMPRList = result[0].compositeReport;
            this.getselectedDistrictMPRList1 = result[0].detailReport;
            console.log(this.getselectedDistrictMPRList);
            this.getselectedDistrictMPRList.forEach((element: any) => {
              if (element.prev.OSSCPrev) {
                this.opStOssc = this.opStOssc + element.prev.OSSCPrev
              }
              if (element.prev.OtherPrev) {
                this.opStOther = this.opStOther + element.prev.OtherPrev
              }
              if (element.rcv.OSSCRcv) {
                this.opStOSSCRcv = this.opStOSSCRcv + element.rcv.OSSCRcv
              }
              if (element.rcv.OtherRcv) {
                this.opStOtherRcv = this.opStOtherRcv + element.rcv.OtherRcv
              }
              if (element.sold.OSSCSold) {
                this.opStOSSCSold = this.opStOSSCSold + element.sold.OSSCSold
              }
              if (element.sold.OtherSold) {
                this.opStOtherSold = this.opStOtherSold + element.sold.OtherSold
              }
              if (element.total.OSSCTotal) {
                this.opStOSSCTotal = this.opStOSSCTotal + element.total.OSSCTotal
              }
              if (element.total.OtherTotal) {
                this.opStOtherTotal = this.opStOtherTotal + element.total.OtherTotal
              }
            });

            resolve(result);
          }, (error) => this.toastr.error(error.statusText, error.status));
        } catch (e) {
          reject(new Error(`Oops! An error occurred: ${e}`));
        }
      });
    }
    else {
      this.toastr.warning(`Please Select all field.`);
      return 0;
    }

  }
  details() {
    console.log('jiii');
    
    this.firststpage = false;
    this.Secondpage = true;
    this.Thirdpage=false;
    this.allpage=true;
  }
  back() {
    this.Secondpage = false;
    this.firststpage = true;
    this.Thirdpage=false;
    this.allpage=true;
  }
  back1() {
    this.getselectedDistrictMPRList2 =[];
    this.Secondpage = true;
    this.firststpage = false;
    this.Thirdpage=false;
    this.allpage=true;
  }
  selectedDistrictMPR2(x: any) {
    console.log(x);

    let data = {
      SelectedYear: this.SelectedYear.year,
      SelectedMonth: this.SelectedMonth,
      tempNo:x.tempNo
    }
    this.getselectedDistrictMPRList2 = [];
    this.firststpage=false;
    this.Secondpage=false;
    this.Thirdpage=true;
    this.allpage=false;
    this.name=x.firmName;

    return new Promise((resolve, reject) => {
      try {
        this.service.selectedDistrictMPR2(data).subscribe((result: any) => {
          this.getselectedDistrictMPRList2 = result[0].productAmount;
          console.log(this.getselectedDistrictMPRList2);
          

          resolve(result);
        }, (error) => this.toastr.error(error.statusText, error.status));
      } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
      }
    });


  }
  exportexcel(): void {
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();

    this.fileName = 'MPRReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('table1');    
    if (element !== null && element !== undefined) {
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'MPRReport_');
  
      /* save to file */
      XLSX.writeFile(wb, this.fileName);
    }
   

  }
  exportexcel1(): void {
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();

    this.fileName = 'MPRReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('table2');    
    if (element !== null && element !== undefined) {
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'MPRReport');
  
      /* save to file */
      XLSX.writeFile(wb, this.fileName);
    }
   

  }
}
