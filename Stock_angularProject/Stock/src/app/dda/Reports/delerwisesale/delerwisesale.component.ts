import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import { DdaService } from 'src/app/Services/dda.service';

@Component({
  selector: 'app-delerwisesale',
  templateUrl: './delerwisesale.component.html',
  styleUrls: ['./delerwisesale.component.css']
})
export class DelerwisesaleComponent implements OnInit {
  getAllDealerList: any = [];
  getAllDealerwisePaymentList: any = [];
  showpage: boolean = false;
  showpage1: boolean = false;
  SelectedFormDate: any = '';
  SelectedToDate: any = '';
  SelectedLicNo: any = '';
  sumTotalNO_OF_BAGS: any = 0;
  sumTotalTOT_QTL: any = 0;
  sumTotalSUBSIDY_AMOUNT: any = 0;
  constructor(private fb: FormBuilder,
    private service: DdaService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.delaeListDistWise();
  }
  delaeListDistWise() {
    this.getAllDealerList = [];
    this.SelectedToDate = '';
    this.SelectedFormDate = '';

    this.service.delaeListDistWise().subscribe(data => {
      this.showpage = true;
      this.showpage1 = false;
      this.getAllDealerList = data;
    })
  }
  dealerwisePaymentDetails(LIC_NO: any) {
    this.spinner.show();
    this.SelectedLicNo = LIC_NO;
    this.getAllDealerwisePaymentList = [];
    this.sumTotalNO_OF_BAGS = 0;
    this.sumTotalTOT_QTL = 0.00;
    this.sumTotalSUBSIDY_AMOUNT = 0.00;


    this.service.dealerwisePaymentDetails(LIC_NO).subscribe(data => {
      this.spinner.hide();
      this.getAllDealerwisePaymentList = data;
      this.getAllDealerwisePaymentList.forEach((i: any) => {
        console.log(i);
        
        if (i.hasOwnProperty('NO_OF_BAGS')) {
          var a = (i.NO_OF_BAGS == undefined || i.NO_OF_BAGS == null || i.NO_OF_BAGS == '') ? 0.00 : i.NO_OF_BAGS;
          console.log(a);
          
          this.sumTotalNO_OF_BAGS = parseInt(this.sumTotalNO_OF_BAGS) + parseInt(a);
        }
        if (i.hasOwnProperty('TOT_QTL')) {
          var b = (i.TOT_QTL == undefined || i.TOT_QTL == null || i.TOT_QTL == '') ? 0.00 : i.TOT_QTL;
          this.sumTotalTOT_QTL = parseFloat(this.sumTotalTOT_QTL) + parseFloat(b);
        }
        if (i.hasOwnProperty('SUBSIDY_AMOUNT')) {
          var c = (i.SUBSIDY_AMOUNT == undefined || i.SUBSIDY_AMOUNT == null || i.SUBSIDY_AMOUNT == '') ? 0.00 : i.SUBSIDY_AMOUNT;
          this.sumTotalSUBSIDY_AMOUNT = parseFloat(this.sumTotalSUBSIDY_AMOUNT) + parseFloat(c);
        }
      })
      this.showpage1 = true;
      this.showpage = false;
    })
  }
  back() {
    this.showpage = true;
    this.showpage1 = false;

  }
}
