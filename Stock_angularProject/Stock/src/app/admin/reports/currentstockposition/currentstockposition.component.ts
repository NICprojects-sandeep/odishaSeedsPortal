import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
@Component({
  selector: 'app-currentstockposition',
  templateUrl: './currentstockposition.component.html',
  styleUrls: ['./currentstockposition.component.css']
})
export class CurrentstockpositionComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedCropCatagory: any = [];
  SelectedAgency:any=''
  SelectedCrop: any = [];
  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillCropCategory();
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
    this.service.FillCropByCategoryId(this.SelectedCropCatagory).subscribe(data => {
      this.getAllCrop = data;
    })
  }
}
