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
  deliveredFrom:any='';
  TRANSACTION_ID:any=''
  constructor( private route: ActivatedRoute,
    private service: FarmersaleService,) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {    
      console.log(params);
      this.TRANSACTION_ID=params.TRANSACTION_ID
      this.service.GetFirmName(this.LicNo).subscribe(data => {
        this.deliveredFrom = data.result[0].APP_FIRMNAME;
      });
      });


    
  }
 
}
