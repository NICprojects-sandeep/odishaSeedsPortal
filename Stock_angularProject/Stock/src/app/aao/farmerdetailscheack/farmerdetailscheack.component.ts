import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
@Component({
  selector: 'app-farmerdetailscheack',
  templateUrl: './farmerdetailscheack.component.html',
  styleUrls: ['./farmerdetailscheack.component.css']
})
export class FarmerdetailscheackComponent implements OnInit {
  addModifyImplementPriceForm: FormGroup;
  constructor(
    private fb: FormBuilder
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

}
