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
  getAllCrop:any=[];
  getAllDelerData:any=[];
  txtPhySale:any='';
  balance:any=''
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
    this.FillCategoryId();
  }
  FillCategoryId() {
    this.getAllCrop = []
    this.aaoService.FillCategoryId().subscribe(data => {
      this.getAllCrop = data;
    })
  }
  physicalsaleentrydata(){
    this.getAllDelerData=[];
    
    if ( this.aaoFarmerSearchForm.valid) {
      this.aaoService.physicalsaleentrydata(this.aaoFarmerSearchForm.value).subscribe(data => {
        this.getAllDelerData = data;
        console.log(this.getAllDelerData);
        
        this.showdetails=true;
        this.loader = false;
       
      });
    }
    else{
      this.toastr.warning(`Please select All fileds.`);
    }
  }
  changeSaleqty(physicalsale:any,ACTUAL_SALE:any,index:any){
    if(physicalsale){
      this.getAllDelerData[index].balance=parseFloat(physicalsale)-parseFloat(ACTUAL_SALE);
      this.getAllDelerData[index].txtPhySale=physicalsale;
    }
    
    

  }
  inserphysicalsaleentrydata(){
    this.aaoService.inserphysicalsaleentrydata(this.getAllDelerData).subscribe(data => {
      if(data.result =='True'){
        this.toastr.success(`Data updated Sucessfully.`);
      }
      else{
        this.toastr.warning(`Internal server error please try after sometimes`);
      }
      this.physicalsaleentrydata();
    });
    
  }
}
