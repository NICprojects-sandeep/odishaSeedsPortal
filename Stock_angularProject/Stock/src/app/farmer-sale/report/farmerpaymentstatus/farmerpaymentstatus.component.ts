import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-farmerpaymentstatus',
  templateUrl: './farmerpaymentstatus.component.html',
  styleUrls: ['./farmerpaymentstatus.component.css']
})
export class FarmerpaymentstatusComponent implements OnInit {

  aaoFarmerSearchForm: FormGroup;
  paymentStatusByFarmeIdDetails:any=[];
  FarmerIdPre:any='';
  loder:boolean=false;
  showdetails:boolean=false;
  constructor(
    private fb: FormBuilder,
    private service: FarmersaleService,
    private toastr: ToastrService, private spinner: NgxSpinnerService

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
      console.log(data);
          
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
      this.spinner.show();
    const obj = { selectedFinancialYear: '', selectedSeasons: '', selectedFarmerId: '' };
    obj.selectedFinancialYear = this.aaoFarmerSearchForm.value!.selectedFinancialYear;
    obj.selectedSeasons = this.aaoFarmerSearchForm.value!.selectedSeasons;


    obj.selectedFarmerId = this.FarmerIdPre+'/'+this.aaoFarmerSearchForm.value.selectedFarmerId;
    this.service.paymentStatusByFarmeId(obj).subscribe(data => {      
      this.paymentStatusByFarmeIdDetails = data;
      this.showdetails=true;
      this.spinner.hide();
    });
  }
  else{
    this.toastr.warning(`Please select all field. `);
  }
  }
}