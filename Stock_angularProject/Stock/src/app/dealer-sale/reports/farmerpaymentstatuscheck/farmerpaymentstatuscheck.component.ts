import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { DealerService } from 'src/app/Services/dealer.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-farmerpaymentstatuscheck',
  templateUrl: './farmerpaymentstatuscheck.component.html',
  styleUrls: ['./farmerpaymentstatuscheck.component.css']
})
export class FarmerpaymentstatuscheckComponent implements OnInit {

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
    this.getFarmeridPrefix();
  }

  getFarmeridPrefix(){
    this.service.GetDistCodeFromDist().subscribe(data => {            
      this.FarmerIdPre = data.Short_Name;      
    });
  }
  paymentStatusByFarmeId(){
    // console.log(this.aaoFarmerSearchForm.value.selectedFarmerId);
    // this.aaoFarmerSearchForm.patchValue({
    //   selectedFarmerId: this.FarmerIdPre+'/'+this.aaoFarmerSearchForm.value.selectedFarmerId
    // });
    this.showdetails=false;
    if (this.aaoFarmerSearchForm.valid) {
    this.loder=true;
    const obj = { selectedFinancialYear: '', selectedSeasons: '', selectedFarmerId: '' };
    obj.selectedFinancialYear = this.aaoFarmerSearchForm.value!.selectedFinancialYear;
    obj.selectedSeasons = this.aaoFarmerSearchForm.value!.selectedSeasons;
    // obj.selectedFarmerId = 'SAM/52674' //this.FarmerIdPre+'/'+this.aaoFarmerSearchForm.value.selectedFarmerId;
    // obj.selectedFarmerId = 'SAM/2682'
    // obj.selectedFarmerId = 'SAM/64682'

    obj.selectedFarmerId = this.FarmerIdPre+'/'+this.aaoFarmerSearchForm.value.selectedFarmerId;
    this.service.paymentStatusByFarmeId(obj).subscribe(data => {      
      this.paymentStatusByFarmeIdDetails = data;
      this.showdetails=true;
      this.loder=false;
    });
  }
  else{
    this.toastr.warning(`Please select all field. `);
  }
  }
}