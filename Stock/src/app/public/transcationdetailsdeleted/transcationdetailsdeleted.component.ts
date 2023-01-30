import { Component, OnInit } from '@angular/core';
import { PublicService } from 'src/app/Services/public.service';
@Component({
  selector: 'app-transcationdetailsdeleted',
  templateUrl: './transcationdetailsdeleted.component.html',
  styleUrls: ['./transcationdetailsdeleted.component.css']
})
export class TranscationdetailsdeletedComponent implements OnInit {
  transactionId: any = '';
  transcationDtl: Array<any> = []
  showdetails: boolean = false;
  constructor(private publicservice: PublicService) { }

  ngOnInit(): void {
  }
  getTranscationDtl() {
    this.transcationDtl = [];
    const data1 = { transactionId: this.transactionId }
    this.publicservice.getTranscationDtl(data1).subscribe(data => {
      this.transcationDtl = data;
      if (data == null) {
        this.transcationDtl = [];
      }
      this.showdetails = true;
    })
  }
  deleteTranscationDtl(y: any) {
    const data1 = { transactionId: this.transactionId }
    this.publicservice.deleteTranscationDtl(data1).subscribe(data => {
      console.log(data);

      if (data.VAL == '1') {
        alert('Payment process already started ! ! !');
      }
      else if (data.VAL == '2') {
        alert('Transaction Deleted ! ! !');
      }
      // alert('Transaction Delete Sucessfully')
      this.getTranscationDtl();
    })
  }
}
