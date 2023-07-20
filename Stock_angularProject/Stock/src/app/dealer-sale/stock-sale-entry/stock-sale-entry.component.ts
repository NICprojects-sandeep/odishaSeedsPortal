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
  getDistrictLicenceofPAcsDetails:any=[];
  afterproceed:boolean=false;
  prebookedsale:boolean=false;
  showCheackBox: boolean = false;
  selectedIndex: number | undefined;
  getAllPreBookingDetails:any=[];
  allFillFinYr:any=[];
  selectedFinancialYear:any='';
  allFillSeason:any=[];
  selectedSeasons:any=[];
  godownList:any=[];
  cropCategoryList:any=[];
  cropList:any=[];
  varietyList:any=[];
  selectedGodown:any='';
  selectedCategory:any='';
  selectedCrop:any='';
  selectedVariety:any='';

  constructor( private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService,private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.Dealer();
    this.FillFinYr();
  }
  FillFinYr() {
    this.allFillFinYr = []
    this.service.FillFinYr().subscribe(data => {
      this.allFillFinYr = data;
      console.log(this.allFillFinYr);
      
      this.selectedFinancialYear = this.allFillFinYr[0].FIN_YR;
      this.FillSeason();
    })
  }
  FillSeason() {
    console.log(this.selectedFinancialYear);
    
    this.allFillSeason = []
    this.service.FillSeason(this.selectedFinancialYear).subscribe(data => {
      this.allFillSeason = data;
      console.log(this.allFillSeason);
      
      this.selectedSeasons = this.allFillSeason[0];
      console.log(this.selectedSeasons);
      
      // this.FillCrops();
      // this.getStockReceivedData();
      // this.getPreBookingDetails();
    })
  }
  Dealer() {
    this.spinner.show();
    this.stockSuppliedToDealer = 1;
    this.stockSuppliedToPacs = 0;
    this.stockSuppliedToOaic = 0;
    this.stockSuppliedToDemo = 0;
    this.spinner.hide();
    this.GetDealerLicenceByDistCodeUserType();

  }
  PACS() {
    this.stockSuppliedToDealer = 0;
    this.stockSuppliedToPacs = 1;
    this.stockSuppliedToOaic = 0;
    this.stockSuppliedToDemo = 0;
    this.GetDealerLicenceByDistCodeUserTypePacs();
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
    this.prebookedsale=false;
    this.showCheackBox = false;


  }
  prebooksale() {
    this.prebookedsale=true;
    this.showCheackBox = true;

  }
  GetDealerLicenceByDistCodeUserType(){
    this.getDistrictLicenceDetails = []
    this.service.GetDealerLicenceByDistCodeUserType().subscribe(data => {
      this.getDistrictLicenceDetails = data;      
    })
  }
  GetDealerLicenceByDistCodeUserTypePacs(){
    this.getDistrictLicenceofPAcsDetails = []
    this.service.GetDealerLicenceByDistCodeUserTypePacs().subscribe(data => {
      this.getDistrictLicenceofPAcsDetails = data;      
    })
  }
  proceed(){
    this.afterproceed=true;
    this.FILL_GODOWN();
  }
  changeSelection = async (event: any, index: any, value: any) => {
    this.selectedIndex = event.target.checked ? index : undefined;
   
  }
  FILL_GODOWN(){
    this.godownList = []
    this.service.FILL_GODOWN().subscribe(data => {
      this.godownList = data;      
    })
  }
  FILL_CROPCATAGORY(){
    this.cropCategoryList = []
    this.service.FILL_CROPCATAGORY(this.selectedGodown).subscribe(data => {
      this.cropCategoryList = data;      
    })
  }
  FILLCROPNAME(){
    this.cropList = []
    this.service.FILLCROPNAME(this.selectedCategory,this.selectedGodown).subscribe(data => {
      this.cropList = data;      
    })
  }
  FILLCROPVARIETY(){
    this.varietyList = []
    this.service.FILLCROPVARIETY(this.selectedCrop,this.selectedCategory,this.selectedGodown).subscribe(data => {      
      this.varietyList = data;      
    })
  }
 
}
