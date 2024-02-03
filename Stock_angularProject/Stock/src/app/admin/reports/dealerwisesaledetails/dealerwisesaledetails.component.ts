import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-dealerwisesaledetails',
  templateUrl: './dealerwisesaledetails.component.html',
  styleUrls: ['./dealerwisesaledetails.component.css']
})
export class DealerwisesaledetailsComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '0';
  SelectedCrop: any = '0';
  SelectedDealer: any = 0;
  SelectedScheme: any = 'OR1';
  SelectedFromDate: any = '';
  SelectedTodate: any = '';
  SelectedDistrict: any = '0';
  SelectedBlock: any = '0';
  getAllFinYr: any = [];
  getAllCrop: any = [];
  schemewisedata: any = [];
  dealerwisewisesaledata: any = [];
  showpage: boolean = false;
  sumTotalNoOfTransactions: any = 0;
  sumTotalNoOfTransactionsPaid: any = 0;
  sumTotalAmounttobepaid: any = 0;
  sumTotalAmountpaid: any = 0;
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
  dealerwisewisesaledetails() {
    this.dealerwisewisesaledata = [];
    this.spinner.show();
    let data = {
      SelectedFinancialYear: this.SelectedFinancialYear,
      SelectedSeason: this.SelectedSeason,
      SelectedCrop: this.SelectedCrop,
      SelectedDealer: this.SelectedDealer,
      SelectedScheme: this.SelectedScheme,
      SelectedFromDate: this.SelectedFromDate,
      SelectedTodate: this.SelectedTodate,
      SelectedDistrict: this.SelectedDistrict,
      SelectedBlock: this.SelectedBlock
    }
    this.sumTotalNoOfTransactions = 0;
    this.sumTotalNoOfTransactionsPaid = 0;
    this.sumTotalAmounttobepaid = 0;
    this.sumTotalAmountpaid = 0;
    this.service.dealerwisewisesaledetails(data).subscribe(data => {
      this.dealerwisewisesaledata = data;
      this.showpage = true;
      this.spinner.hide();
      console.log(this.dealerwisewisesaledata);
      for (let j = 0; j < this.dealerwisewisesaledata.length; j++) {
        console.log(parseFloat(this.dealerwisewisesaledata[j].NOOFTRANS));
        
        this.sumTotalNoOfTransactions += parseFloat(this.dealerwisewisesaledata[j].NOOFTRANS);
        this.sumTotalNoOfTransactionsPaid += parseFloat(this.dealerwisewisesaledata[j].PAIDTRANS);
        this.sumTotalAmounttobepaid += parseFloat(this.dealerwisewisesaledata[j].AMNT_PAID);
        this.sumTotalAmountpaid += parseFloat(this.dealerwisewisesaledata[j].PAID_AMNT);
      }
    })
  }
  exportexcel() {

  }
}
