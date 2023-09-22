import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';

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
  showpage:boolean=true;

  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  getAllDistrict: any = [];
  stateStockPositionData:any=[];
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