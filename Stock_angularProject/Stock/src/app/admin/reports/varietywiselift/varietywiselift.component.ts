import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-varietywiselift',
  templateUrl: './varietywiselift.component.html',
  styleUrls: ['./varietywiselift.component.css']
})
export class VarietywiseliftComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedUserType: any = '';
  SelectedDistrict:any=''
  SelectedCrop: any = [];
  selectedToDate:any='';
  selectedFromDate:any='';
  SelectedMonth:any='';
  maxdate:any;
  mindate:any;

  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  getAllDistrict: any = [];
  stateStockPositionData:any=[];
showpage:boolean=false;
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.maxdate = this.getDate();
    this.FillDistrict();
    this.FillCategoryId();

  }
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  mindatecal(){
    this.selectedToDate='';
    const today = new Date(this.selectedFromDate);
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    this.mindate=year+'-'+month+'-'+day;
    console.log(this.mindate);
    
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
  FillDistrict() {
    this.getAllDistrict = []
    this.service.FillDistrict().subscribe(data => {
      this.getAllDistrict = data;
      this.getAllDistrict.unshift({
        Dist_Code: 0, Dist_Name: 'All'
      });
    })
  }
  getVarietywiseLift() {
    if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
      && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
      && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined
      && this.SelectedUserType !== null && this.SelectedUserType !== '' && this.SelectedUserType !== undefined
      && this.SelectedDistrict !== null && this.SelectedDistrict !== '' && this.SelectedDistrict !== undefined
      && this.SelectedMonth !== null && this.SelectedMonth !== '' && this.SelectedMonth !== undefined) {
      this.spinner.show();
      let data = {
        SelectedFinancialYear: this.SelectedFinancialYear,
        SelectedSeason: this.SelectedSeason,
        SelectedUserType: this.SelectedUserType,
        SelectedCrop: this.SelectedCrop,
        selectedFromDate: this.selectedFromDate,
        selectedToDate: this.selectedToDate,
        SelectedDistrict: this.SelectedDistrict,
        SelectedMonth:this.SelectedMonth
      }
      this.service.getVarietywiseLift(data).subscribe(data => {
        console.log(data);
        this.stateStockPositionData = data;

       
          this.showpage = true;
        this.spinner.hide();
        // this.getAllCrop = data;
      })
    }
    else {
      this.toastr.warning('Please select all field.');
    }
  }
}