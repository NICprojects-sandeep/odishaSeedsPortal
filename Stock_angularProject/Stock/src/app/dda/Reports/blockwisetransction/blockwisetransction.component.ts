import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import { DdaService } from 'src/app/Services/dda.service';


@Component({
  selector: 'app-blockwisetransction',
  templateUrl: './blockwisetransction.component.html',
  styleUrls: ['./blockwisetransction.component.css']
})
export class BlockwisetransctionComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedCrop: any = [];
  getAllCrop: any = [];
  getAllFinYr: any = [];
  showpage: boolean = false;
  showpage1: boolean = false;
  blockwisestockdata: any = [];
  blockwiseStockData1: any = [];
  finyr: any = '';
  season: any = '';
  crop: any = '';
  sumTotalACTUAL_RCV: any = 0;
  sumTotalSaleQty: any = 0;
  sumTotalACTUAL_SALE: any = 0;

  constructor(private fb: FormBuilder,
    private service: DdaService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

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
  blockwisetransctiondetails() {
    this.blockwisestockdata = [];
    this.finyr = this.SelectedFinancialYear;
    this.season = this.SelectedSeason;
    this.crop = this.SelectedCrop;
    this.sumTotalACTUAL_RCV=0.00;
this.sumTotalSaleQty=0.00;
this.sumTotalACTUAL_SALE=0.00;
    if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
      && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
      && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined) {
      this.spinner.show();

      this.service.blockwisetransctiondetails(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedCrop).subscribe(data => {
        console.log(data);

        this.blockwisestockdata = data;
        this.showpage = true;
        this.showpage1 = false;

        for (let i = 0; i < this.blockwisestockdata.length; i++) {
          console.log(this.blockwisestockdata[i].ACTUAL_RCV);
          if (this.blockwisestockdata[i].ACTUAL_RCV === undefined) {
            this.blockwisestockdata[i].ACTUAL_RCV = 0;
          }
          if (this.blockwisestockdata[i].SaleQty === undefined) {
            this.blockwisestockdata[i].SaleQty = 0;
          }
          if (this.blockwisestockdata[i].ACTUAL_SALE === undefined) {
            this.blockwisestockdata[i].ACTUAL_SALE = 0;
          }
          
          this.sumTotalACTUAL_RCV += parseFloat(this.blockwisestockdata[i].ACTUAL_RCV);
          this.sumTotalSaleQty += parseFloat(this.blockwisestockdata[i].SaleQty);
          this.sumTotalACTUAL_SALE += parseFloat(this.blockwisestockdata[i].ACTUAL_SALE);
        }
      })
    }
    else {
      this.toastr.warning('Please select all field.');
    }
  }
  blockwiseStockDetails(x: any) {
    this.blockwiseStockData1 = [];
    this.service.blockwiseStockDetails(this.finyr, this.season, this.crop, x).subscribe(data => {
      this.blockwiseStockData1 = data;
      console.log(this.blockwiseStockData1);
      this.showpage1=true;
      this.showpage=false;


    })

  }
  back(){
    this.showpage=true;
    this.showpage1=false;

  }
}
