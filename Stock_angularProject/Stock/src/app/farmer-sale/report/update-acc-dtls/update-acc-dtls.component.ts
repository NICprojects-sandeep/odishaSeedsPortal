import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import * as XLSX from 'xlsx';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-update-acc-dtls',
  templateUrl: './update-acc-dtls.component.html',
  styleUrls: ['./update-acc-dtls.component.css']
})
export class UpdateAccDtlsComponent implements OnInit {
  DealerInfo: any = [];
  CntLicDtl: any = '';
  allBankDetails: any = [];
  allBranchDetails: any = [];
  IFSCDetails: any = [];

  selectedBank: any = '';
  selectedAccountHolderName: any = '';
  selectedAccountNo: any = '';
  selectedBranch: any = '';
  selectedIFSC: any = '';
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.GetDealerInfo();
    this.CntLic();
  }
  CntLic() {
    this.DealerInfo = [];
    this.service.CntLic().subscribe(data => {
      this.CntLicDtl = data;      
      if(this.CntLicDtl.Cnt == 1){
        this.toastr.warning(`Please update your bank details then sale the stock.`);
        this.FillBank();
      }
    })
  }
  GetDealerInfo() {
    this.DealerInfo = [];
    this.service.GetDealerInfo().subscribe(data => {
      this.DealerInfo = data[0];
    })
  }
  FillBank() {
    this.allBankDetails = [];
    this.service.FillBank().subscribe(data => {
      this.allBankDetails = data;
    })
  }
  FillBranchName() {
    this.allBranchDetails = [];
    this.service.FillBranchName(this.selectedBank).subscribe(data => {
      this.allBranchDetails = data;
    })
  }
  FillIFSC() {
    this.selectedIFSC = '';
    this.service.FillIFSC(this.selectedBranch).subscribe(data => {
      this.selectedIFSC = data[0].NEW_IFSC_CODE;
    })
  }
  UpdateDealerBankDetails() {
    let data: any = {}
    data.AADHAAR_NO = this.DealerInfo.AADHAAR_NO;
    data.ACC_HOLDERNAME = this.selectedAccountHolderName;
    data.ACC_NO = this.selectedAccountNo;
    data.BANK_ID = this.selectedBank,
    data.BRANCH_ID = this.selectedBranch,
    data.IFSC_CODE = this.selectedIFSC;
    this.service.UpdateDealerBankDetails(data).subscribe(data1 => {
      this.spinner.hide();
      if (data1.VAL == '1') {

        // this.TRANSACTION_ID = data.TRANSACTION_ID;
        this.toastr.success(`Your Requset Has Been Updated Successfully.`);
        this.GetDealerInfo();
        this.CntLic();
        // this.PrintReport();
        // this.printPage = true;
        // this.viewpage = false;
      }
      else {
        this.toastr.warning(`Provided Data Exists.`);
      }
      // this.allFILLDEALERSTOCK = data;
    })
  }
}
