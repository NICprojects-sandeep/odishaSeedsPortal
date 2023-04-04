import { Component, OnInit } from '@angular/core';
import { PublicService } from 'src/app/Services/public.service';

@Component({
  selector: 'app-cashmemo-transcationdeleted',
  templateUrl: './cashmemo-transcationdeleted.component.html',
  styleUrls: ['./cashmemo-transcationdeleted.component.css']
})
export class CashmemoTranscationdeletedComponent implements OnInit {
  txtCashMemoNo: any = '';
  transcationDtl: any = [];
  showdetails: boolean = false;
  getFillSaleTransDtls: any = [];
  constructor(private publicservice: PublicService) { }

  ngOnInit(): void {
  }
  getTranscationDtl() {
    this.transcationDtl = [];
    const data1 = { transactionId: this.txtCashMemoNo} //'GANJ/0172/2022-23/R/29' } //this.txtCashMemoNo}
    this.publicservice.getCashMemoNoDtl(data1).subscribe(data => {
      this.transcationDtl = data;
      console.log(this.transcationDtl);
      
      this.showdetails = true;
    })
  }
  FillSaleTransDtls(y: any) {
    this.getFillSaleTransDtls = [];
    const data1 = { SALETRANSID: y.SALETRANSID } //y}

    this.publicservice.FillSaleTransDtls(data1).subscribe(data => {
      this.getFillSaleTransDtls = data;
      this.getFillSaleTransDtls[0].GODOWN_NAME = y.GODOWN_NAME;
      this.getFillSaleTransDtls[0].SALE_DATE = y.SALE_DATE;
      this.getFillSaleTransDtls[0].LOT_NUMBER = y.LOT_NUMBER;
      this.getFillSaleTransDtls[0].SALETRANSID = y.SALETRANSID


    })

  }
  CancelCashMemo(y: any) {    
    const data1 = { SALETRANSID: y.SALETRANSID } //y}

    this.publicservice.CancelCashMemo(data1).subscribe(data => {
      console.log(data);
      
      if (data.VAL == '1') {
        const data2 = {
          GODOWN_ID: y.GODOWN_ID,
          SALE_TO: y.SALE_TO,
          DD_NUMBER: y.DD_NUMBER,
          LOT_NUMBER: y.LOT_NO,
          BAG_SIZE_KG: y.BAG_SIZE1,
          SALE_NO_OF_BAG: y.SALE_NO_OF_BAG1,
          APIKEY: "key01002"
        }

        this.publicservice.sendtoossc(data2).subscribe(data3 => {
          alert('Transaction Completed Successfull !!!');
          this.getTranscationDtl();
        })

      }
      else if (data.VAL == '2') {
        alert('Oops Transaction Can Not be Deleted !!!');
      }

      else if (data.VAL == '3') {
        alert('Oops Transaction Un-Successfull !!!');
      }
    })
  }
}
