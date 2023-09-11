import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DealerService } from 'src/app/Services/dealer.service';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-cashmemodetails',
  templateUrl: './cashmemodetails.component.html',
  styleUrls: ['./cashmemodetails.component.css']
})
export class CashmemodetailsComponent implements OnInit {
  cashmemeodetails: any = [];
  appfirmname: any = '';
  SALE_TO: any = '';
  SALE_DATE: any = '';
  CASH_MEMO_NO: any = '';
  DD_NUMBER: any = '';
  sumTotalNoOfBags: any = 0;
  sumQunitalinQtl: any = 0;
  sumAllincostPrice: any = 0;

  constructor(
    private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.cashmemeodetails = [];
    this.sumTotalNoOfBags = 0;
    this.sumQunitalinQtl = 0;
    this.sumAllincostPrice = 0;
    this.SALE_TO='';
    this.route.queryParams
      .subscribe((params) => {
        return new Promise(async (resolve: any, reject: any) => {
          try {
            this.cashmemeodetails = [];
            this.cashmemeodetails = await this.service.cashmemodetails(params.applicationid).toPromise()
            console.log(this.cashmemeodetails);

            if (this.cashmemeodetails.length > 0) {
              if(this.cashmemeodetails[0].SUPPLY_TYPE =='1' || this.cashmemeodetails[0].SUPPLY_TYPE =='6' || this.cashmemeodetails[0].SUPPLY_TYPE =='9' ){
                this.appfirmname = this.cashmemeodetails[0].APP_FIRMNAME;
                this.SALE_TO = this.cashmemeodetails[0].SALE_TO;
               
              }
              else if(this.cashmemeodetails[0].SUPPLY_TYPE =='3' || this.cashmemeodetails[0].SUPPLY_TYPE =='8'){
                let appfirmname = await this.service.getGodownmaster(this.cashmemeodetails[0].SALE_TO).toPromise();
                this.appfirmname = appfirmname[0].Godown_Name;
              }
              this.SALE_DATE = this.cashmemeodetails[0].SALE_DATE;
              this.CASH_MEMO_NO = this.cashmemeodetails[0].CASH_MEMO_NO;
              this.DD_NUMBER = this.cashmemeodetails[0].DD_NUMBER;
              this.cashmemeodetails.forEach((y: any) => {
  
                if (y.hasOwnProperty('Quantity')) {
                  var a = (y.Quantity == undefined || y.Quantity == null || y.Quantity == '') ? 0.00 : y.Quantity;
                  var b = (y.SALE_NO_OF_BAG == undefined || y.SALE_NO_OF_BAG == null || y.SALE_NO_OF_BAG == '') ? 0 : y.SALE_NO_OF_BAG;
                  var c = (y.AMOUNT == undefined || y.AMOUNT == null || y.AMOUNT == '') ? 0 : y.AMOUNT;
  
                  console.log(b, typeof (b), this.sumTotalNoOfBags);
  
                  this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
                  this.sumTotalNoOfBags = parseInt(this.sumTotalNoOfBags) + parseInt(b);
                  this.sumAllincostPrice = (parseFloat(this.sumAllincostPrice) + parseFloat(c)).toFixed(2);
  
  
                }
              })
              resolve(this.cashmemeodetails);
            }

            // this.AvailableStockDetails.forEach((a: any) => {
            //   a.ischeacked = true;
            //   a.QunitalinQtl = 0.00;
            // });
          
          } catch (e) {
            console.error(e);

            reject()
          }
        })
      }
      );
  }
  getGodownNames(): string {
    // return this.cashmemeodetails.map((godown: any) => godown.Godown_Name).join(',');
    const uniqueGodownIds = [...new Set(this.cashmemeodetails.map((godown: any) => godown.Godown_Name))];
    return uniqueGodownIds.join(',');
  }
}
