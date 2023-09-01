import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';

@Component({
  selector: 'app-farmerinvoice',
  templateUrl: './farmerinvoice.component.html',
  styleUrls: ['./farmerinvoice.component.css']
})
export class FarmerinvoiceComponent implements OnInit {
  LicNo: any = 'ODGAN6/2014-15/0004';
  deliveredFrom: any = '';
  TRANSACTION_ID: any = '';
  TOT_AMT: any;
  SUB_AMT: any;
  todateDate: any;
  Prebookedamount: any;
  totalPaybleamount: any;
  newId: any = '';
  STARVCHACCOUNTNO: any;
  vchBankName: any;
  farmerName: any;
  FathersName: any;
  MobileNumber: any;
  Village: any;
  GP: any;
  Block: any;
  Dist: any;
  constructor(private route: ActivatedRoute,
    private service: FarmersaleService,) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      // this.TRANSACTION_ID=params.TRANSACTION_ID;
      const lastIndex = params.TRANSACTION_ID.lastIndexOf("-");

      if (lastIndex !== -1) {
        this.TRANSACTION_ID = params.TRANSACTION_ID.slice(0, lastIndex);
      } else {
        this.TRANSACTION_ID = params.TRANSACTION_ID;
      }
      // this.service.GetFirmName().subscribe(data => {
      //   this.deliveredFrom = data.result[0].APP_FIRMNAME;
      // });

      this.service.GetFirmName().subscribe(data => {
        this.deliveredFrom = data.result[0].APP_FIRMNAME;
        this.LicNo = data.result[0].LIC_NO;
      });


      console.log(this.TRANSACTION_ID);

      this.service.GetFarmerInv(this.TRANSACTION_ID).subscribe(data2 => {
        if (data2.length > 0) {
          this.todateDate = data2[0].SALE_DATE;
          this.TOT_AMT = data2[0].TOT_AMT;
          this.SUB_AMT = data2[0].SUB_AMT;
          this.Prebookedamount = data2[0].prebookedAmount;
          this.totalPaybleamount = (parseFloat(data2[0].TOT_AMT) - parseFloat(data2[0].prebookedAmount)).toFixed(2);
          this.service.GetFarmerInvHdr(data2[0].FARMER_ID).subscribe(data1 => {
            if (data1.length > 0) {
              this.STARVCHACCOUNTNO = data1[0].STARVCHACCOUNTNO;
              this.vchBankName = data1[0].vchBankName
              this.farmerName = data1[0].VCHFARMERNAME;
              this.FathersName = data1[0].VCHFATHERNAME;
              this.MobileNumber = data1[0].VCHMOBILENO;
              this.Village = data1[0].villg_name;
              this.GP = data1[0].GP_Name;
              this.Block = data1[0].BLOCK_NAME;
              this.Dist = data1[0].Dist_Name;
            }

          });
          this.newId = this.TRANSACTION_ID.lastIndexOf("-");
          console.log(this.newId);
        }


      });

    });



  }

}
