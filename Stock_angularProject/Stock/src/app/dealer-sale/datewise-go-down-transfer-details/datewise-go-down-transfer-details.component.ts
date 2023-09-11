import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';

@Component({
  selector: 'app-datewise-go-down-transfer-details',
  templateUrl: './datewise-go-down-transfer-details.component.html',
  styleUrls: ['./datewise-go-down-transfer-details.component.css']
})
export class DatewiseGoDownTransferDetailsComponent implements OnInit {
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
    let data = {
      selectedFromDate: this.selectedFromDate

    }
    this.service.dateWiseGodownTransferDetails(data).subscribe(data => {
      this.getAlldateWiseGodownTransferDetails=data;
    this.showpage=true});
  }
  gotoInvoicePage(CASH_MEMO_NO:any){
    // window.open(`http://localhost:4300/#/farmersale/farmerinvoice/${TRANSACTION_ID}`, '_blank');

  }
}
