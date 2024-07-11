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

  selectedBank: any;
  selectedAccountHolderName: any = '';
  selectedAccountNo: any = '';
  selectedBranch: any;
  selectedIFSC: any = '';
  sbank: any = '';
  sbranch: any = '';
  updatebutton: boolean = false;
  RJCT_REASON: any = '';
  oldaccountdetails: any = [];
  constructor(
    private fb: FormBuilder,
    private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.GetDealerInfo();
    this.CntLic();
  }
  CntLic() {
    this.DealerInfo = [];
    this.service.CntLic().subscribe(data => {
      this.CntLicDtl = data;
      if (this.CntLicDtl.Cnt == 1) {
        this.toastr.warning(`Please update your bank details then sale the stock.`);
        // this.FillBank();
        this.rejectedBankDetails();
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
    return new Promise((resolve, reject) => {
      try {
        this.service.FillBank().subscribe((result: any) => {
          this.allBankDetails = result;
          resolve(result);
        }, (error) => this.toastr.error(error.statusText, error.status));
      } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
      }
    });



  }




  FillBranchName() {
    this.allBranchDetails = [];
    return new Promise((resolve, reject) => {
      try {
        this.service.FillBranchName(this.selectedBank).subscribe((result: any) => {
          this.allBranchDetails = result;
          resolve(result);
        }, (error) => this.toastr.error(error.statusText, error.status));
      } catch (e) {
        reject(new Error(`Oops! An error occurred: ${e}`));
      }
    });
  }
  FillIFSC() {
    this.selectedIFSC = '';
    this.service.FillIFSC(this.selectedBranch).subscribe(data => {
      this.selectedIFSC = data[0].NEW_IFSC_CODE;
    })
  }
  UpdateDealerBankDetails() {
    if (this.selectedAccountHolderName != undefined && this.selectedAccountHolderName != null && this.selectedAccountHolderName != '' &&
      this.selectedAccountNo != undefined && this.selectedAccountNo != null && this.selectedAccountNo != '' &&
      this.selectedBank != undefined && this.selectedBank != null && this.selectedBank != '' &&
      this.selectedBranch != undefined && this.selectedBranch != null && this.selectedBranch != '' &&
      this.selectedIFSC != undefined && this.selectedIFSC != null && this.selectedIFSC != '') {
      this.spinner.show();
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

    else {
      this.toastr.warning(`Please Select all field.`);
    }
  }
  rejectedBankDetails() {
    this.oldaccountdetails = [];
    this.spinner.show();
    this.service.rejectedBankDetails().subscribe(
      async (data: any) => {
        this.oldaccountdetails = data;
        console.log(this.oldaccountdetails);

        await this.FillBank();
        if (data != null) {
          setTimeout(async () => {
            this.sbank = this.allBankDetails.find((x: any) => x.INTID === parseInt(data.BANK_ID));
            this.selectedBank = this.sbank.INTID;
            console.log(this.selectedBank);

            await this.FillBranchName();
            setTimeout(async () => {
              this.sbranch = this.allBranchDetails.find((y: any) => y.INTBRANCHID === parseInt(data.BRANCH_ID));
              this.selectedAccountHolderName = data.ACC_HOLDERNAME;
              this.selectedAccountNo = data.VCHACCOUNTNO;
              this.selectedIFSC = data.bank_post_office_branch;
              this.selectedBranch = this.sbranch.INTBRANCHID;
              this.RJCT_REASON = data.RJCT_REASON;

              this.updatebutton = true;
              this.spinner.hide();
            }, 5);
          }, 5);
        }
        else {
          this.spinner.hide();
        }

      },
      (error) => this.toastr.error(error.statusText, error.status)
    );
  }
  UpdatetheBankDetails() {
    if (this.selectedAccountHolderName != undefined && this.selectedAccountHolderName != null && this.selectedAccountHolderName != '' &&
      this.selectedAccountNo != undefined && this.selectedAccountNo != null && this.selectedAccountNo != '' &&
      this.selectedBank != undefined && this.selectedBank != null && this.selectedBank != '' &&
      this.selectedBranch != undefined && this.selectedBranch != null && this.selectedBranch != '' &&
      this.selectedIFSC != undefined && this.selectedIFSC != null && this.selectedIFSC != '') {
      this.spinner.show();
      let data: any = {};
      data.AADHAAR_NO = this.DealerInfo.AADHAAR_NO;
      data.ACC_HOLDERNAME = this.selectedAccountHolderName;
      data.ACC_NO = this.selectedAccountNo;
      data.BANK_ID = this.selectedBank,
        data.BRANCH_ID = this.selectedBranch,
        data.IFSC_CODE = this.selectedIFSC;
      console.log(data);

      return new Promise((resolve, reject) => {
        try {
          if (this.selectedAccountHolderName == this.oldaccountdetails.ACC_HOLDERNAME && this.selectedAccountNo == this.oldaccountdetails.bank_account_number &&
            this.selectedBank == parseInt(this.oldaccountdetails.BANK_ID) && this.selectedBranch == parseInt(this.oldaccountdetails.BRANCH_ID)) {
            this.toastr.warning(`Please Update Bank Details.`);
            this.spinner.hide();
          }
          else {
            this.service.UpdatetheBankDetails(data).subscribe((result: any) => {
              this.spinner.hide();
              if (result.VAL == '1') {
                this.toastr.success(`Your Requset Has Been Updated Successfully.`);
                this.GetDealerInfo();
                this.CntLic();
              }
              else {
                this.toastr.warning(`Provided Data Exists.`);
              }
            })
          }



        } catch (e) {
          reject(new Error(`Oops! An error occurred: ${e}`));
        }
      });
    }

    else {
      this.toastr.warning(`Please Select all field.`);
      return 0;
    }
  }
}
