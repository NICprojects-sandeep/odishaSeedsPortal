import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import * as XLSX from 'xlsx';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-paymentresponse',
  templateUrl: './paymentresponse.component.html',
  styleUrls: ['./paymentresponse.component.css']
})
export class PaymentresponseComponent implements OnInit {
  selectedFromDate: any = '';
  selectedToDate: any = '';
  maxdate: any;
  mindate: any;
  showpage: boolean = false
  getAllPaymentResponseData: any = [];
  constructor(private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.getPaymentResponse();
    this.maxdate = this.getDate();
  }
  mindatecal() {
    this.selectedToDate = '';
    const today = new Date(this.selectedFromDate);
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    this.mindate = year + '-' + month + '-' + day;
    console.log(this.mindate);

  }
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  getPaymentResponse() {
    if (this.selectedFromDate != null && this.selectedFromDate != undefined && this.selectedFromDate != '' && this.selectedToDate != null && this.selectedToDate != undefined && this.selectedToDate != '') {

      this.spinner.show();
      this.getAllPaymentResponseData = [];
      this.service.getPaymentResponse(this.selectedFromDate, this.selectedToDate).subscribe(data => {
        console.log(data);
        this.getAllPaymentResponseData = data;
        this.showpage = true;
        this.spinner.hide();
      })
    }

  }
  // SELECT A.LICENCE_NO,d.Category_Code,d.Category_Name,c.Crop_Code,c.Crop_Name,B.Variety_Code,B.Variety_Name,CLASS,SUM(STOCK_QUANTITY)STOCK_QUANTITY,SUM(AVL_QUANTITY)AVL_QUANTITY FROM STOCK_DEALERSTOCK A 
  // LEFT OUTER JOIN mCropVariety B ON A.CROP_VERID = B.Variety_Code 
  // LEFT OUTER JOIN mCrop C ON B.Crop_Code = C.Crop_Code 
  // LEFT OUTER JOIN mCropCategory D ON C.Category_Code = D.Category_Code 
  // INNER JOIN mFINYR E ON A.FIN_YR = E.FIN_YR AND E.IS_ACTIVE = 1 
  // INNER JOIN mSEASSION F ON A.SEASSION = F.SHORT_NAME AND F.IS_ACTIVE = 1  having SUM(AVL_QUANTITY) > 0
  //             GROUP BY A.LICENCE_NO,d.Category_Code,d.Category_Name,c.Crop_Code,c.Crop_Name,B.Variety_Code,B.Variety_Code,B.Variety_Name,CLASS

  //       select Top 10 * from STOCK_DEALERSTOCK
}
