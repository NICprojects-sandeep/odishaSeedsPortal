import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-dealerpacssale',
  templateUrl: './dealerpacssale.component.html',
  styleUrls: ['./dealerpacssale.component.css']
})
export class DealerpacssaleComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedUserType: any = '';
  SelectedDistrict:any=''
  SelectedCrop: any = [];
  selectedToDate:any='';
  selectedFromDate:any='';
  SelectedMonth:any='';
  SelectedScheme:any='';
  maxdate:any;
  mindate:any;

  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  getAllDistrict:any=[];
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillCropCategory();
    this.maxdate = this.getDate();
    this.FillDistrict();
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
  FillCropCategory() {
    this.getAllCatagory = []
    this.service.FillCropCategory().subscribe(data => {
      this.getAllCatagory = data;
    })
  }
  FillCropByCategoryId() {
    this.getAllCrop = []
    this.service.FillCropByCategoryId(this.SelectedUserType).subscribe(data => {
      this.getAllCrop = data;
    })
  }
  FillDistrict() {
    this.getAllDistrict = []
    this.service.FillDistrict().subscribe(data => {
      this.getAllDistrict = data;
         })
  }
}
