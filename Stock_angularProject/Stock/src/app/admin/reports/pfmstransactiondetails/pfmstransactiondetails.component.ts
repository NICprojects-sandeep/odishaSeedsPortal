import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import * as XLSX from 'xlsx';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-pfmstransactiondetails',
  templateUrl: './pfmstransactiondetails.component.html',
  styleUrls: ['./pfmstransactiondetails.component.css']
})
export class PfmstransactiondetailsComponent implements OnInit {
  paymentStatusByFarmeIdDetails: any = [];
  enteredCPAID: any = '';//CP00251807220001
  PFSMTransctionDetailsDealerCodeWiseDetails: any = [];
  paymentStatusShowInExcelsheet: any = [];
  fileName: any = '';
  expandedIndex: number = 0;
  showpage:boolean=false
  constructor(
    private service: AdminService,
    private spinner: NgxSpinnerService,
    private toastr: ToastrService,
  ) {
  }

  ngOnInit(): void {
  }
  getPFSMTransctionDetails() {
    this.paymentStatusByFarmeIdDetails = [];
    this.paymentStatusShowInExcelsheet = [];
    if (this.enteredCPAID) {
      this.spinner.show();
      this.service.getPFSMTransctionDetails(this.enteredCPAID).subscribe(data => {
        this.paymentStatusByFarmeIdDetails = data.pfms;
        if (data.dealerdata) {
          this.paymentStatusShowInExcelsheet = data.dealerdata;
        }
        this.showpage=true;
        this.spinner.hide();
      })
    } else {
      this.toastr.warning(`Please Enter CPAID.`);
    }



  }
  getPFSMTransctionDetailsDealerCodeWise(x: any) {
    this.spinner.show();
    this.PFSMTransctionDetailsDealerCodeWiseDetails = [];
    this.service.getPFSMTransctionDetailsDealerCodeWise(this.enteredCPAID, x).subscribe(data => {
      this.PFSMTransctionDetailsDealerCodeWiseDetails = data;
      this.spinner.hide();
    })
  }
  exportexcel(): void {
    this.fileName = '';
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();
    this.fileName = 'PFMSTransctionReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('tableId');
    if (element !== null && element !== undefined) {
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);
      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'PFMSTransctionReport');
      /* save to file */
      XLSX.writeFile(wb, this.fileName);
    }
  }
}
