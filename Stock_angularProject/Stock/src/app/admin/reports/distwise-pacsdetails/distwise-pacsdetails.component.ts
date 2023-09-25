import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
@Component({
  selector: 'app-distwise-pacsdetails',
  templateUrl: './distwise-pacsdetails.component.html',
  styleUrls: ['./distwise-pacsdetails.component.css']
})
export class DistwisePACSDetailsComponent implements OnInit {

  SelectedUserType: any = '';
  SelectedDistrict:any=''
  
  getAllDistrict:any=[];
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillDistrict();
  }
  FillDistrict() {
    this.getAllDistrict = []
    this.service.FillDistrict().subscribe(data => {
      this.getAllDistrict = data;
         })
  }
}