import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import * as XLSX from 'xlsx';
@Component({
  selector: 'app-dealerpacspayment',
  templateUrl: './dealerpacspayment.component.html',
  styleUrls: ['./dealerpacspayment.component.css']
})
export class DealerpacspaymentComponent implements OnInit {
  getAllFinYr: any = [];
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  enteredDealerPacsId: any = '';
  showpage: boolean = false;
  dealerPacsPaymentdata: any = [];
  sumTotalNO_OF_BAGS:any=0;
sumTotalTOT_QTL:any=0;
sumTotalTotal_Payment_Amount:any=0;
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
  dealerPacsPaymentdetails() {
    this.showpage = false;
    this.sumTotalNO_OF_BAGS=0;
this.sumTotalTOT_QTL=0;
this.sumTotalTotal_Payment_Amount=0;
    if (this.SelectedFinancialYear && this.SelectedSeason) {
      this.spinner.show();
      this.service.dealerPacsPaymentdetails(this.SelectedFinancialYear, this.SelectedSeason, this.enteredDealerPacsId).subscribe(data => {

        this.dealerPacsPaymentdata = data;

        this.spinner.hide();

        this.showpage = true;

        if (this.dealerPacsPaymentdata.length > 0) {
          for (let i = 0; i < this.dealerPacsPaymentdata.length; i++) {
            if (this.dealerPacsPaymentdata[i].hasOwnProperty('NO_OF_BAGS')) {
              var k = (this.dealerPacsPaymentdata[i].NO_OF_BAGS == undefined || this.dealerPacsPaymentdata[i].NO_OF_BAGS == null || this.dealerPacsPaymentdata[i].NO_OF_BAGS == '') ? 0 : this.dealerPacsPaymentdata[i].NO_OF_BAGS;
              this.sumTotalNO_OF_BAGS = (parseFloat(this.sumTotalNO_OF_BAGS) + parseFloat(k)).toFixed(2);
            }
            if (this.dealerPacsPaymentdata[i].hasOwnProperty('TOT_QTL')) {
              var l = (this.dealerPacsPaymentdata[i].TOT_QTL == undefined || this.dealerPacsPaymentdata[i].TOT_QTL == null || this.dealerPacsPaymentdata[i].TOT_QTL == '') ? 0 : this.dealerPacsPaymentdata[i].TOT_QTL;
              this.sumTotalTOT_QTL = (parseFloat(this.sumTotalTOT_QTL) + parseFloat(l)).toFixed(2);
            }
            if (this.dealerPacsPaymentdata[i].hasOwnProperty('Total_Payment_Amount')) {
              var m = (this.dealerPacsPaymentdata[i].Total_Payment_Amount == undefined || this.dealerPacsPaymentdata[i].Total_Payment_Amount == null || this.dealerPacsPaymentdata[i].Total_Payment_Amount == '') ? 0 : this.dealerPacsPaymentdata[i].Total_Payment_Amount;
              this.sumTotalTotal_Payment_Amount = (parseFloat(this.sumTotalTotal_Payment_Amount) + parseFloat(m)).toFixed(2);
            }
          }
        }

      })
    }
    else {
      this.toastr.warning('Please select all field.');
    }
  }
}
