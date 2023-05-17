import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { AaoService } from 'src/app/Services/aao.service';
@Component({
  selector: 'app-farmerdetailscheack',
  templateUrl: './farmerdetailscheack.component.html',
  styleUrls: ['./farmerdetailscheack.component.css']
})
export class FarmerdetailscheackComponent implements OnInit {
  addModifyImplementPriceForm: FormGroup;
  paymentStatusByFarmeIdDetails:any=[]
  constructor(
    private fb: FormBuilder,
    private aaoService: AaoService,
  ) { 
    this.addModifyImplementPriceForm = this.fb.group({
      selectedFinancialYear: ['', Validators.required],
      selectedSeasons: ['', Validators.required],
      selectedFarmerId:['',Validators.required]
    });
  }

  ngOnInit(): void {
    console.log(window.location.href)
  }
  paymentStatusByFarmeId(){
    console.log(this.addModifyImplementPriceForm.value);
    
    this.aaoService.paymentStatusByFarmeId(this.addModifyImplementPriceForm.value).subscribe(data => {
      this.paymentStatusByFarmeIdDetails = data.result[0].APP_FIRMNAME;
    });
  }
}
