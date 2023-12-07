import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import * as XLSX from 'xlsx';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-prebookingdetails',
  templateUrl: './prebookingdetails.component.html',
  styleUrls: ['./prebookingdetails.component.css']
})
export class PrebookingdetailsComponent implements OnInit {
  getFillFarmerPrebooking:any=[];
  getFillDealerPrebooking:any=[];
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.FillDealerPrebooking();
    this.FillFarmerPrebooking();
  }
  FillDealerPrebooking() {
    this.getFillDealerPrebooking = [];
    let beneficiaryType='D';
    this.service.FillPrebooking(beneficiaryType).subscribe(data => {
      this.getFillDealerPrebooking = data;  
      console.log(this.getFillDealerPrebooking);
            
    })
  }
  FillFarmerPrebooking() {
    this.getFillFarmerPrebooking = [];
    let beneficiaryType='F';
    this.service.FillPrebooking(beneficiaryType).subscribe(data => {
      this.getFillFarmerPrebooking = data;        
    })
  }
 
}
