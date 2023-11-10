import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { DealerService } from 'src/app/Services/dealer.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-stockreceiveentry',
  templateUrl: './stockreceiveentry.component.html',
  styleUrls: ['./stockreceiveentry.component.css']
})
export class StockreceiveentryComponent implements OnInit {
  aaoFarmerSearchForm: FormGroup;
  paymentStatusByFarmeIdDetails:any=[];
  FarmerIdPre:any='';
  loder:boolean=false;
  showdetails:boolean=false;
  constructor(
    private fb: FormBuilder,
    private service: DealerService,
    private toastr: ToastrService

  ) {
    this.aaoFarmerSearchForm = this.fb.group({
      selectedFinancialYear: ['', Validators.required],
      selectedSeasons: ['', Validators.required],
      selectedFarmerId:['',Validators.required]
    });
   }

  ngOnInit(): void {
  }
  paymentStatusByFarmeId(){}
}
