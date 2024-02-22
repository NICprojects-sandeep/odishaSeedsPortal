import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-schemewisedetails',
  templateUrl: './schemewisedetails.component.html',
  styleUrls: ['./schemewisedetails.component.css']
})
export class SchemewisedetailsComponent implements OnInit {

  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedCrop: any = '';

  getAllFinYr: any = [];
  getAllCrop: any = [];
  schemewisedata: any = [];
  showpage: boolean = false;
  sumTotalpacsDataTotFarmer: any = 0;
  sumTotaldealerDataTotFarmer: any = 0;
  sumTotalpacsDataTOT_QTL: any = 0;
  sumTotaldealerDataTOT_QTL: any = 0;
  sumTotalpacsDataTOT_SUB: any = 0;
  sumTotaldealerDataTOT_SUB: any = 0;
  sumTotalpacsDataRESAMOUNT: any = 0;
  sumTotaldealerDataRESAMOUNT: any = 0;
  sumTotalTotFarmer: any = 0;
  sumTotalTOT_QTL: any = 0;
  sumTotalTOT_SUB: any = 0;
  sumTotalRESAMOUNT: any = 0;
  fileName:any='';
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService,
  ) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillCategoryId();
  }
  FillFinYr() {
    this.getAllFinYr = []
    this.service.allFillFinYr().subscribe(data => {
      this.getAllFinYr = data;
    })
  }
  FillCategoryId() {
    this.getAllCrop = []
    this.service.FillCategoryId().subscribe(data => {
      this.getAllCrop = data;
    })
  }
  schemewisedetails() {
    this.schemewisedata = [];
    this.sumTotalpacsDataTotFarmer = 0;
    this.sumTotaldealerDataTotFarmer = 0;
    this.sumTotalpacsDataTOT_QTL = 0;
    this.sumTotaldealerDataTOT_QTL = 0;
    this.sumTotalpacsDataTOT_SUB = 0;
    this.sumTotaldealerDataTOT_SUB = 0;
    this.sumTotalpacsDataRESAMOUNT = 0;
    this.sumTotaldealerDataRESAMOUNT = 0;
    this.sumTotalTotFarmer = 0;
    this.sumTotalTOT_QTL = 0;
    this.sumTotalTOT_SUB = 0;
    this.sumTotalRESAMOUNT = 0;
    this.spinner.show();
    
    this.showpage = false;
    if(this.SelectedFinancialYear && this.SelectedSeason ){
      this.spinner.show();
      this.service.schemewisedetails(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedCrop).subscribe(data => {
  
        this.schemewisedata = data;
  
        this.spinner.hide();
  
        this.showpage = true;
        if(this.schemewisedata.length > 0){
          for (let i = 0; i < this.schemewisedata.length; i++) {
          
            var k = (this.schemewisedata[i].pacsData == undefined || this.schemewisedata[i].pacsData.TotFarmer == null || this.schemewisedata[i].pacsData.TotFarmer == '') ? 0 : this.schemewisedata[i].pacsData.TotFarmer;
            var l = (this.schemewisedata[i].dealerData == undefined || this.schemewisedata[i].dealerData.TotFarmer == null || this.schemewisedata[i].dealerData.TotFarmer == '') ? 0 : this.schemewisedata[i].dealerData.TotFarmer;
            this.schemewisedata[i].schemewiseTotalFarmer= (parseFloat(k)+ parseFloat(l)).toFixed(2);
    
            var m = (this.schemewisedata[i].pacsData == undefined || this.schemewisedata[i].pacsData.TOT_QTL == null || this.schemewisedata[i].pacsData.TOT_QTL == '') ? 0 : this.schemewisedata[i].pacsData.TOT_QTL;
            var n = (this.schemewisedata[i].dealerData == undefined || this.schemewisedata[i].dealerData.TOT_QTL == null || this.schemewisedata[i].dealerData.TOT_QTL == '') ? 0 : this.schemewisedata[i].dealerData.TOT_QTL;
            this.schemewisedata[i].schemewiseTotalQTL= (parseFloat(m)+ parseFloat(n)).toFixed(2);
    
    
            var o = (this.schemewisedata[i].pacsData == undefined || this.schemewisedata[i].pacsData.TOT_SUB == null || this.schemewisedata[i].pacsData.TOT_SUB == '') ? 0 : this.schemewisedata[i].pacsData.TOT_SUB;
            var p = (this.schemewisedata[i].dealerData == undefined || this.schemewisedata[i].dealerData.TOT_SUB == null || this.schemewisedata[i].dealerData.TOT_SUB == '') ? 0 : this.schemewisedata[i].dealerData.TOT_SUB;
            this.schemewisedata[i].schemewiseTotalsubsidy= (parseFloat(o)+ parseFloat(p)).toFixed(2);
    
    
            var q = (this.schemewisedata[i].pacsData == undefined || this.schemewisedata[i].pacsData.RESAMOUNT == null || this.schemewisedata[i].pacsData.RESAMOUNT == '') ? 0 : this.schemewisedata[i].pacsData.RESAMOUNT;
            var r = (this.schemewisedata[i].dealerData == undefined || this.schemewisedata[i].dealerData.RESAMOUNT == null || this.schemewisedata[i].dealerData.RESAMOUNT == '') ? 0 : this.schemewisedata[i].dealerData.RESAMOUNT;
            this.schemewisedata[i].schemewiseRESAMOUNT= (parseFloat(q)+ parseFloat(r)).toFixed(2);
    
          }
          for (let i = 0; i < this.schemewisedata.length; i++) {
            this.sumTotalTotFarmer = 0;
            this.sumTotalTOT_QTL = 0;
            this.sumTotalTOT_SUB = 0;
            this.sumTotalRESAMOUNT = 0;
            if (this.schemewisedata[i].hasOwnProperty('pacsData')) {
              var k = (this.schemewisedata[i].pacsData == undefined || this.schemewisedata[i].pacsData.TotFarmer == null || this.schemewisedata[i].pacsData.TotFarmer == '') ? 0 : this.schemewisedata[i].pacsData.TotFarmer;
            this.sumTotalpacsDataTotFarmer = (parseFloat(this.sumTotalpacsDataTotFarmer) + parseFloat(k)).toFixed(2);
            }
            if (this.schemewisedata[i].hasOwnProperty('dealerData')) {
              var l = (this.schemewisedata[i].dealerData == undefined || this.schemewisedata[i].dealerData.TotFarmer == null || this.schemewisedata[i].dealerData.TotFarmer == '') ? 0 : this.schemewisedata[i].dealerData.TotFarmer;
             this.sumTotaldealerDataTotFarmer = (parseFloat(this.sumTotaldealerDataTotFarmer) + parseFloat(l)).toFixed(2);
            }
            this.sumTotalTotFarmer = (parseFloat(this.sumTotalpacsDataTotFarmer) + parseFloat(this.sumTotaldealerDataTotFarmer)).toFixed(2);
            if (this.schemewisedata[i].hasOwnProperty('pacsData')) {
              var m = (this.schemewisedata[i].pacsData == undefined || this.schemewisedata[i].pacsData.TOT_QTL == null || this.schemewisedata[i].pacsData.TOT_QTL == '') ? 0 : this.schemewisedata[i].pacsData.TOT_QTL;
             this.sumTotalpacsDataTOT_QTL = (parseFloat(this.sumTotalpacsDataTOT_QTL) + parseFloat(m)).toFixed(2);
            }
            if (this.schemewisedata[i].hasOwnProperty('dealerData')) {
              var n = (this.schemewisedata[i].dealerData == undefined || this.schemewisedata[i].dealerData.TOT_QTL == null || this.schemewisedata[i].dealerData.TOT_QTL == '') ? 0 : this.schemewisedata[i].dealerData.TOT_QTL;
             this.sumTotaldealerDataTOT_QTL = (parseFloat(this.sumTotaldealerDataTOT_QTL) + parseFloat(n)).toFixed(2);
            }
            this.sumTotalTOT_QTL = (parseFloat(this.sumTotalpacsDataTOT_QTL) + parseFloat(this.sumTotaldealerDataTOT_QTL)).toFixed(2);
    
            if (this.schemewisedata[i].hasOwnProperty('pacsData')) {
              var o = (this.schemewisedata[i].pacsData == undefined || this.schemewisedata[i].pacsData.TOT_SUB == null || this.schemewisedata[i].pacsData.TOT_SUB == '') ? 0 : this.schemewisedata[i].pacsData.TOT_SUB;
            this.sumTotalpacsDataTOT_SUB = (parseFloat(this.sumTotalpacsDataTOT_SUB) + parseFloat(o)).toFixed(2);
            }
            if (this.schemewisedata[i].hasOwnProperty('dealerData')) {
              var p = (this.schemewisedata[i].dealerData == undefined || this.schemewisedata[i].dealerData.TOT_SUB == null || this.schemewisedata[i].dealerData.TOT_SUB == '') ? 0 : this.schemewisedata[i].dealerData.TOT_SUB;
            this.sumTotaldealerDataTOT_SUB = (parseFloat(this.sumTotaldealerDataTOT_SUB) + parseFloat(p)).toFixed(2);
            }
            this.sumTotalTOT_SUB = (parseFloat(this.sumTotalpacsDataTOT_SUB) + parseFloat(this.sumTotaldealerDataTOT_SUB)).toFixed(2);
    
            if (this.schemewisedata[i].hasOwnProperty('pacsData')) {
              var q = (this.schemewisedata[i].pacsData == undefined || this.schemewisedata[i].pacsData.RESAMOUNT == null || this.schemewisedata[i].pacsData.RESAMOUNT == '') ? 0 : this.schemewisedata[i].pacsData.RESAMOUNT;
             this.sumTotalpacsDataRESAMOUNT = (parseFloat(this.sumTotalpacsDataRESAMOUNT) + parseFloat(q)).toFixed(2);
            }
            if (this.schemewisedata[i].hasOwnProperty('dealerData')) {
              var r = (this.schemewisedata[i].dealerData == undefined || this.schemewisedata[i].dealerData.RESAMOUNT == null || this.schemewisedata[i].dealerData.RESAMOUNT == '') ? 0 : this.schemewisedata[i].dealerData.RESAMOUNT;
              this.sumTotaldealerDataRESAMOUNT = (parseFloat(this.sumTotaldealerDataRESAMOUNT) + parseFloat(r)).toFixed(2);
            }
            this.sumTotalRESAMOUNT = (parseFloat(this.sumTotalpacsDataRESAMOUNT) + parseFloat(this.sumTotaldealerDataRESAMOUNT)).toFixed(2);
    
          }
        }
       
      })
    }
    else{
      this.toastr.warning('Please select all field.');
    }
   
  }
  exportexcel(): void {
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();

    this.fileName = 'SchemeWiseReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('tableId');
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
