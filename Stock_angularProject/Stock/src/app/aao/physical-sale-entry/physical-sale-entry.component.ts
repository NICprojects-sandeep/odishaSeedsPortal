import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { AaoService } from 'src/app/Services/aao.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-physical-sale-entry',
  templateUrl: './physical-sale-entry.component.html',
  styleUrls: ['./physical-sale-entry.component.css']
})
export class PhysicalSaleEntryComponent implements OnInit {

  aaoFarmerSearchForm: FormGroup;
  paymentStatusByFarmeIdDetails:any=[];
  FarmerIdPre:any='';
  loader:boolean=false;
  showdetails:boolean=false;
  constructor(
    private fb: FormBuilder,
    private aaoService: AaoService,
    private toastr: ToastrService
  ) {
    this.aaoFarmerSearchForm = this.fb.group({
      selectedFinancialYear: ['', Validators.required],
      selectedSeasons: ['', Validators.required],
      selectedCrop:['',Validators.required]
    });
   }

  ngOnInit(): void {
  }
  delalerwisestockCheack(){
    if ( this.aaoFarmerSearchForm.valid) {
      this.aaoService.delalerwisestockCheack(this.aaoFarmerSearchForm.value).subscribe(data => {
        this.loader = false;
       
      });
      this.toastr.success(`Please select Type of Farmer.`);
    }
    else{
      this.toastr.warning(`Please select Type of Farmer.`);
    }
  }
}
