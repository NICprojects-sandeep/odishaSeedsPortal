import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
@Component({
  selector: 'app-dealerpacspayment',
  templateUrl: './dealerpacspayment.component.html',
  styleUrls: ['./dealerpacspayment.component.css']
})
export class DealerpacspaymentComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
  }

}
