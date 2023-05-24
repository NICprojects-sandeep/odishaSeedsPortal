import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { AaoService } from 'src/app/Services/aao.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-farmer-type-update',
  templateUrl: './farmer-type-update.component.html',
  styleUrls: ['./farmer-type-update.component.css']
})
export class FarmerTypeUpdateComponent implements OnInit {
  getFarmerID:any=[] ;
  getFarmerDetails:any=[];
  DealerSaleForm: FormGroup;
  constructor(
    private fb: FormBuilder,
    private aaoService: AaoService,
    private toastr: ToastrService
  ) { 
    this.DealerSaleForm = this.fb.group({
      technicalDetails:['', [Validators.required]],
      searchText:[''],
    })
  }

  ngOnInit(): void {
    this.fillfARMERiD();
  }
  fillfARMERiD(){
    this.getFarmerID=[];
    this.aaoService.fillfARMERiD().subscribe(data => {         
      this.getFarmerID = data;      
    });
  }
  gerFarmerDetailsForSamallMarginUpdatation(){
    this.getFarmerDetails=[];
    console.log(this.DealerSaleForm.value);
    
    this.aaoService.gerFarmerDetailsForSamallMarginUpdatation(this.DealerSaleForm.value.technicalDetails.NICFARMERID).subscribe(data => {    

      this.getFarmerDetails = data;      
      console.log(this.getFarmerDetails);
      
    });
  }
}
