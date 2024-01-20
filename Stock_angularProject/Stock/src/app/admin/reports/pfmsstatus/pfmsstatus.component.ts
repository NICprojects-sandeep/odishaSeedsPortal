import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-pfmsstatus',
  templateUrl: './pfmsstatus.component.html',
  styleUrls: ['./pfmsstatus.component.css']
})
export class PfmsstatusComponent implements OnInit {
  getAllPFMSStatus: any = [];
  DataReceivedFromCSM: number = 0;
  DatasendtoPFMSforAccountValidation: number = 0;
  DatareturnedfromPFMS: number = 0;
  ValidatedDatafromPFMS: number = 0;
  ValidatedDatafromCCB: number = 0;
  RejectedDatafromCCB: number = 0;
  RejectedbyNIC: number = 0;
  RejectionData: any = [];
  TotalNoofRejection: number = 0;
  constructor(
    private service: AdminService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.getPFMSStatus();
  }
  getPFMSStatus() {
    
    this.spinner.show();
    this.getAllPFMSStatus = []
    this.TotalNoofRejection = 0;
    this.service.getPFMSStatus().subscribe(data => {
      this.DataReceivedFromCSM = data[0][0].NoofData;
      this.DatasendtoPFMSforAccountValidation = data[1][0].NoofAccValid;
      this.DatareturnedfromPFMS = data[2][0].NoofPFMS;
      this.ValidatedDatafromPFMS = data[3][0].NoofAccept;
      this.ValidatedDatafromCCB = data[4][0].NoofCCPAccpt;
      this.RejectedDatafromCCB = data[5][0].NoofCCPRjct;

      this.RejectedbyNIC = (parseInt(data[3][0].NoofAccept) + parseInt(data[4][0].NoofCCPAccpt)) - parseInt(data[7][0].totFarmerApprove);

      this.RejectionData = data[6];
      for (let index = 0; index < this.RejectionData.length; index++) {
        this.TotalNoofRejection += parseInt(this.RejectionData[index].NoofRejection);

      }
      this.spinner.hide();
      // this.DataReceivedFromCSM = data[0][0].totFarmerApprove;

    })
  }
}
