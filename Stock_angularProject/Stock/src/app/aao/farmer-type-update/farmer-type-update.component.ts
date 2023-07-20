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
  getFarmerID: any = [];
  getFarmerDetails: any = [];
  DealerSaleForm: FormGroup;
  farmerType: any = '';
  loader: boolean = false;
  buttonclick: boolean = false;
  constructor(
    private fb: FormBuilder,
    private aaoService: AaoService,
    private toastr: ToastrService
  ) {
    this.DealerSaleForm = this.fb.group({
      technicalDetails: ['', [Validators.required]],
      searchText: [''],
    })
  }

  ngOnInit(): void {
    this.fillfARMERiD();
  }
  fillfARMERiD() {
    this.getFarmerID = [];
    this.aaoService.fillfARMERiD().subscribe(data => {
      this.getFarmerID = data;
    });
  }
  gerFarmerDetailsForSamallMarginUpdatation() {
    this.getFarmerDetails = [];
    this.aaoService.gerFarmerDetailsForSamallMarginUpdatation(this.DealerSaleForm.value.technicalDetails.NICFARMERID).subscribe(data => {
      this.getFarmerDetails = data;
    });
  }
  farmerTypeUpdate() {
    if (this.farmerType != null && this.farmerType != undefined && this.farmerType != '') {
      this.buttonclick = true;
      const data = { NICFARMERID: '', farmerType: '' };

      this.loader = true;
      data.NICFARMERID = this.DealerSaleForm.value.technicalDetails.NICFARMERID;
      data.farmerType = this.farmerType
      this.aaoService.farmerTypeUpdate(data).subscribe(data => {
        this.loader = false;
        this.toastr.success(`Updated sucessfully.`);
        this.buttonclick = false;
        this.fillfARMERiD();
        this.getFarmerDetails=[]
        this.getFarmerID = [];
        this.getFarmerDetails = [];
        this.DealerSaleForm.reset();
      });
    }
    else {
      this.toastr.warning(`Please select Type of Farmer.`);
    }

  }
}
