import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';
@Component({
  selector: 'app-stock-sale-entry',
  templateUrl: './stock-sale-entry.component.html',
  styleUrls: ['./stock-sale-entry.component.css']
})
export class StockSaleEntryComponent implements OnInit {
  stockSuppliedToDealer: any = '';
  stockSuppliedToPacs: any = '';
  stockSuppliedToOaic: any = '';
  stockSuppliedToDemo: any = '';
  getDistrictLicenceDetails:any=[];

  constructor( private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService,private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.Dealer();
  }

  Dealer() {
    this.spinner.show();
    this.stockSuppliedToDealer = 1;
    this.stockSuppliedToPacs = 0;
    this.stockSuppliedToOaic = 0;
    this.stockSuppliedToDemo = 0;
    this.spinner.hide();

  }
  PACS() {
    this.stockSuppliedToDealer = 0;
    this.stockSuppliedToPacs = 1;
    this.stockSuppliedToOaic = 0;
    this.stockSuppliedToDemo = 0;
  }
  OAIC() {
    this.stockSuppliedToDealer = 0;
    this.stockSuppliedToPacs = 0;
    this.stockSuppliedToOaic = 1;
    this.stockSuppliedToDemo = 0;
  }
  Demonstration() {
    this.stockSuppliedToDealer = 0;
    this.stockSuppliedToPacs = 0;
    this.stockSuppliedToOaic = 0;
    this.stockSuppliedToDemo = 1;
  }
  noramlSale() {
   


  }
  prebooksale() {
   
  }
  GetDealerLicenceByDistCodeUserType(){
    this.getDistrictLicenceDetails = []
    this.service.GetDealerLicenceByDistCodeUserType().subscribe(data => {
      this.getDistrictLicenceDetails = data;
    })
  }
}
