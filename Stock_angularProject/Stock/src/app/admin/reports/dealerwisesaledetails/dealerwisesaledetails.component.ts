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
  SelectedSeason: any = '';
  SelectedCrop: any = '';
  SelectedDealer: any = 0;
  SelectedScheme: any = 'OR1';
  SelectedFromDate: any = '';
  SelectedTodate: any = '';
  SelectedDistrict: any = '';
  SelectedBlock: any = '';
  getAllFinYr: any = [];
  getAllCrop: any = [];
  schemewisedata: any = [];
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

  }
  exportexcel() {

  }
}
