import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-dist-wise-crop-age-wise-seed-data',
  templateUrl: './dist-wise-crop-age-wise-seed-data.component.html',
  styleUrls: ['./dist-wise-crop-age-wise-seed-data.component.css']
})
export class DistWiseCropAgeWiseSeedDataComponent implements OnInit {

  constructor(private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
  }

}
