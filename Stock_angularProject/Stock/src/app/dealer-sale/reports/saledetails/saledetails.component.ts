import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';

@Component({
  selector: 'app-saledetails',
  templateUrl: './saledetails.component.html',
  styleUrls: ['./saledetails.component.css']
})
export class SaledetailsComponent implements OnInit {
  maxdate: any;
  selectedFromDate: any = '';
  showpage: boolean = false;
  getAllsaledetails: any = [];
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
  saledetails() {
    this.spinner.show();
    let data = {
      selectedFromDate: this.selectedFromDate

    }
    this.service.saledetails(data).subscribe(data => {
      this.getAllsaledetails = data;
      this.showpage = true;
      this.spinner.hide();
    });
  }
  gotoInvoicePage(CASH_MEMO_NO: any) {
    window.open(`http://164.100.140.77/#/dealer/cashmemodetails?applicationid=` + CASH_MEMO_NO, '_blank');

  }
}