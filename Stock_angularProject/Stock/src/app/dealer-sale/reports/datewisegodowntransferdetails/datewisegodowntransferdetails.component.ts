import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';


@Component({
  selector: 'app-datewisegodowntransferdetails',
  templateUrl: './datewisegodowntransferdetails.component.html',
  styleUrls: ['./datewisegodowntransferdetails.component.css']
})
export class DatewisegodowntransferdetailsComponent implements OnInit {
  maxdate: any;
  selectedFromDate: any = '';
  showpage:boolean=false;
  getAlldateWiseGodownTransferDetails:any=[];
  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.maxdate = this.getDate();
  }
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  dateWiseGodownTransferDetails(){
    this.spinner.show();
    let data = {
      selectedFromDate: this.selectedFromDate

    }
    this.service.dateWiseGodownTransferDetails(data).subscribe(data => {
      this.getAlldateWiseGodownTransferDetails=data;
    this.showpage=true;
    this.spinner.hide();
  });
  }
  gotoInvoicePage(CASH_MEMO_NO:any){
    window.open(`http://164.100.140.77/users/#/dealer/cashmemodetails?applicationid=` + CASH_MEMO_NO, '_blank');
  }
}
