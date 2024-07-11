import { Component, OnInit } from '@angular/core';
import { AdminService } from 'src/app/Services/admin.service';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-erupidashboard',
  templateUrl: './erupidashboard.component.html',
  styleUrls: ['./erupidashboard.component.css']
})
export class ErupidashboardComponent implements OnInit {

  prebookingDtl: any = [];
  totalprebookedorder: any = 0;
  totalprebookedquantity: any = 0;
  totalcollectorder: any = 0;
  totalcollectorderquantity: any = 0;
  totalpendingorder: any = 0;
  totalpendingorderquantity: any = 0;
  totalprebookingdtl: any = [];
  sumprebookedquanitybyfarmer: any = 0.00;
  sumprebookedquanitybydealer: any = 0.00;
  search: any = '';
  selectedDistrict: any;
  selectedDealer: any;
  selectedBlock: any;
  selectedSubsidy: any;
  selectedFromDate: any = '';
  selectedToDate: any = '';
  sumtotal_transactions: any = 0;
  sumtotalseedsalenooftxn: any = 0;
  sumswtichtodbtmodenooftxn: any = 0;
  sumpendingTransactionCount: any = 0;
  sumtotalseedsaleamount: any = 0.00;
  sumtotalswitchtodbtmodeamount: any = 0.00;
  sumtotal_payable_amt_switchtodbtmode_farmer: any = 0.00;
  sumTotalSeedsale: any = 0.00;
  sumtotalSeedsaleViaerupi:any=0.00;
  sumtotalSeedsaleViaedbt:any=0.00;

  erupiDistrictList: any = [];
  erupiBlockList: any = [];
  mindate: any;
  maxdate:any;
  geterupiStockData:any=[];

  constructor(private service: AdminService,
    private spinner: NgxSpinnerService) {
    this.selectedDistrict = '0';
    this.selectedDealer = '0';
    this.selectedBlock = '0';
    this.selectedSubsidy = '0';

  }

  ngOnInit(): void {
    this.getSearchErupidata();
    this.getErupiDistrict();
    this.maxdate = this.getDate();
  }
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  mindatecal() {
    this.selectedToDate = '';
    const today = new Date(this.selectedFromDate);
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    this.mindate = year + '-' + month + '-' + day;

  }
  getTotalSeedsale(y: any): any {
    const totalSeedSaleAmount = parseFloat(y.totalseedsaleamount);
    const switchtodbtmodeamount = parseFloat(y.switchtodbtmodeamount);
    const pendingamount = parseFloat(y.total_payable_amt_switchtodbtmode_farmer);
    const sum = totalSeedSaleAmount + switchtodbtmodeamount + pendingamount
    return sum.toFixed(2);
  }
  saleviaeRUPI(y: any): any {
    if (y.totalseedsalenooftxn == 0) {
      return 0
    }
    else {
      const total_transactions = parseInt(y.total_transactions, 10);
      const totalseedsalenooftxn = parseInt(y.totalseedsalenooftxn, 10);
      return ((totalseedsalenooftxn / total_transactions)*100).toFixed(2);
    }
  }
  saleviaDBT(y: any): any {
    if (y.swtichtodbtmodenooftxn == 0) {
      return 0
    }
    else {
      const total_transactions1 = parseInt(y.total_transactions, 10);
      const swtichtodbtmodenooftxn1 = parseInt(y.swtichtodbtmodenooftxn, 10);
      return ((swtichtodbtmodenooftxn1 / total_transactions1)*100).toFixed(2);
    }
  }
  getSearchErupidata() {
    this.sumtotal_transactions = 0;
    this.sumtotalseedsalenooftxn = 0;
    this.sumswtichtodbtmodenooftxn = 0;
    this.sumpendingTransactionCount = 0;
    this.sumtotalseedsaleamount = 0.00;
    this.sumtotalswitchtodbtmodeamount = 0.00;
    this.sumtotal_payable_amt_switchtodbtmode_farmer = 0.00;
    this.sumTotalSeedsale = 0.00;
    this.sumtotalSeedsaleViaerupi=0.00;
    this.sumtotalSeedsaleViaedbt=0.00;
    // this.spinner.show();
    this.sumprebookedquanitybyfarmer = 0.00
    this.prebookingDtl = [];


    this.service.getSearchErupidata().subscribe(data => {
      this.prebookingDtl = data;

      this.prebookingDtl.forEach((i: any) => {
        this.sumTotalSeedsale = 0.00;
        this.sumtotalSeedsaleViaerupi=0.00;
        this.sumtotalSeedsaleViaedbt=0.00;
        if (i.hasOwnProperty('total_transactions')) {
          var a = (i.total_transactions == undefined || i.total_transactions == null || i.total_transactions == '') ? 0.00 : i.total_transactions;
          // this.sumprebookedquanitybyfarmer = (this.sumprebookedquanitybyfarmer) + parseFloat(a);
          this.sumtotal_transactions = parseInt(this.sumtotal_transactions) + parseInt(a);
        }
        if (i.hasOwnProperty('totalseedsalenooftxn')) {
          var b = (i.totalseedsalenooftxn == undefined || i.totalseedsalenooftxn == null || i.totalseedsalenooftxn == '') ? 0.00 : i.totalseedsalenooftxn;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumtotalseedsalenooftxn = parseInt(this.sumtotalseedsalenooftxn) + parseInt(b);

        }
        if (i.hasOwnProperty('swtichtodbtmodenooftxn')) {
          var c = (i.swtichtodbtmodenooftxn == undefined || i.swtichtodbtmodenooftxn == null || i.swtichtodbtmodenooftxn == '') ? 0.00 : i.swtichtodbtmodenooftxn;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumswtichtodbtmodenooftxn = parseInt(this.sumswtichtodbtmodenooftxn) + parseInt(c);

        }
        if (i.hasOwnProperty('pendingTransactionCount')) {
          var d = (i.pendingTransactionCount == undefined || i.pendingTransactionCount == null || i.pendingTransactionCount == '') ? 0.00 : i.pendingTransactionCount;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumpendingTransactionCount = parseInt(this.sumpendingTransactionCount) + parseInt(d);

        }

        if (i.hasOwnProperty('totalseedsaleamount')) {
          var e = (i.totalseedsaleamount == undefined || i.totalseedsaleamount == null || i.totalseedsaleamount == '') ? 0.00 : i.totalseedsaleamount;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumtotalseedsaleamount = (parseFloat(this.sumtotalseedsaleamount) + parseFloat(e)).toFixed(2);

        }
        if (i.hasOwnProperty('switchtodbtmodeamount')) {
          var f = (i.switchtodbtmodeamount == undefined || i.switchtodbtmodeamount == null || i.switchtodbtmodeamount == '') ? 0.00 : i.switchtodbtmodeamount;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumtotalswitchtodbtmodeamount = (parseFloat(this.sumtotalswitchtodbtmodeamount) + parseFloat(f)).toFixed(2);

        }
        if (i.hasOwnProperty('total_payable_amt_switchtodbtmode_farmer')) {
          var g = (i.total_payable_amt_switchtodbtmode_farmer == undefined || i.total_payable_amt_switchtodbtmode_farmer == null || i.total_payable_amt_switchtodbtmode_farmer == '') ? 0.00 : i.total_payable_amt_switchtodbtmode_farmer;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumtotal_payable_amt_switchtodbtmode_farmer = (parseFloat(this.sumtotal_payable_amt_switchtodbtmode_farmer) + parseFloat(g)).toFixed(2);

        }
        this.sumTotalSeedsale = (parseFloat(this.sumtotalseedsaleamount) + parseFloat(this.sumtotalswitchtodbtmodeamount) + parseFloat(this.sumtotal_payable_amt_switchtodbtmode_farmer)).toFixed(2)
        this.sumtotalSeedsaleViaerupi = ((parseInt(this.sumtotalseedsalenooftxn)/parseInt(this.sumtotal_transactions))*100).toFixed(2);
        this.sumtotalSeedsaleViaedbt = ((parseInt(this.sumswtichtodbtmodenooftxn)/parseInt(this.sumtotal_transactions))*100).toFixed(2);


      })
      // this.spinner.hide();
    })
  }
  getParticularSearchErupidata() {
    this.sumtotal_transactions = 0;
    this.sumtotalseedsalenooftxn = 0;
    this.sumswtichtodbtmodenooftxn = 0;
    this.sumpendingTransactionCount = 0;
    this.sumtotalseedsaleamount = 0.00;
    this.sumtotalswitchtodbtmodeamount = 0.00;
    this.sumtotal_payable_amt_switchtodbtmode_farmer = 0.00;
    this.sumTotalSeedsale = 0.00;
    this.sumtotalSeedsaleViaerupi=0.00;
    this.sumtotalSeedsaleViaedbt=0.00;
    // this.spinner.show();
    this.sumprebookedquanitybyfarmer = 0.00
    this.prebookingDtl = [];
    const reqdata = {
      selectedDistrict: this.selectedDistrict,
      selectedBlock: this.selectedBlock,
      selectedSubsidy: this.selectedSubsidy,
      selectedFromDate: this.selectedFromDate,
      selectedToDate: this.selectedToDate,

    }
    this.service.getParticularSearchErupidata(reqdata).subscribe(data => {
      this.prebookingDtl = data;
      console.log(this.prebookingDtl);
      

      this.prebookingDtl.forEach((i: any) => {
        this.sumTotalSeedsale = 0.00;
        this.sumtotalSeedsaleViaerupi=0.00;
        this.sumtotalSeedsaleViaedbt=0.00;
        if (i.hasOwnProperty('total_transactions')) {
          var a = (i.total_transactions == undefined || i.total_transactions == null || i.total_transactions == '') ? 0.00 : i.total_transactions;
          // this.sumprebookedquanitybyfarmer = (this.sumprebookedquanitybyfarmer) + parseFloat(a);
          this.sumtotal_transactions = parseInt(this.sumtotal_transactions) + parseInt(a);
        }
        if (i.hasOwnProperty('totalseedsalenooftxn')) {
          var b = (i.totalseedsalenooftxn == undefined || i.totalseedsalenooftxn == null || i.totalseedsalenooftxn == '') ? 0.00 : i.totalseedsalenooftxn;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumtotalseedsalenooftxn = parseInt(this.sumtotalseedsalenooftxn) + parseInt(b);

        }
        if (i.hasOwnProperty('swtichtodbtmodenooftxn')) {
          var c = (i.swtichtodbtmodenooftxn == undefined || i.swtichtodbtmodenooftxn == null || i.swtichtodbtmodenooftxn == '') ? 0.00 : i.swtichtodbtmodenooftxn;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumswtichtodbtmodenooftxn = parseInt(this.sumswtichtodbtmodenooftxn) + parseInt(c);

        }
        if (i.hasOwnProperty('pendingTransactionCount')) {
          var d = (i.pendingTransactionCount == undefined || i.pendingTransactionCount == null || i.pendingTransactionCount == '') ? 0.00 : i.pendingTransactionCount;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumpendingTransactionCount = parseInt(this.sumpendingTransactionCount) + parseInt(d);

        }

        if (i.hasOwnProperty('totalseedsaleamount')) {
          var e = (i.totalseedsaleamount == undefined || i.totalseedsaleamount == null || i.totalseedsaleamount == '') ? 0.00 : i.totalseedsaleamount;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumtotalseedsaleamount = (parseFloat(this.sumtotalseedsaleamount) + parseFloat(e)).toFixed(2);

        }
        if (i.hasOwnProperty('switchtodbtmodeamount')) {
          var f = (i.switchtodbtmodeamount == undefined || i.switchtodbtmodeamount == null || i.switchtodbtmodeamount == '') ? 0.00 : i.switchtodbtmodeamount;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumtotalswitchtodbtmodeamount = (parseFloat(this.sumtotalswitchtodbtmodeamount) + parseFloat(f)).toFixed(2);

        }
        if (i.hasOwnProperty('total_payable_amt_switchtodbtmode_farmer')) {
          var g = (i.total_payable_amt_switchtodbtmode_farmer == undefined || i.total_payable_amt_switchtodbtmode_farmer == null || i.total_payable_amt_switchtodbtmode_farmer == '') ? 0.00 : i.total_payable_amt_switchtodbtmode_farmer;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumtotal_payable_amt_switchtodbtmode_farmer = (parseFloat(this.sumtotal_payable_amt_switchtodbtmode_farmer) + parseFloat(g)).toFixed(2);

        }
        this.sumTotalSeedsale = (parseFloat(this.sumtotalseedsaleamount) + parseFloat(this.sumtotalswitchtodbtmodeamount) + parseFloat(this.sumtotal_payable_amt_switchtodbtmode_farmer)).toFixed(2)
        this.sumtotalSeedsaleViaerupi = ((parseInt(this.sumtotalseedsalenooftxn)/parseInt(this.sumtotal_transactions))*100).toFixed(2);
        this.sumtotalSeedsaleViaedbt = ((parseInt(this.sumswtichtodbtmodenooftxn)/parseInt(this.sumtotal_transactions))*100).toFixed(2);


      })
    })
  }
  getErupiDistrict() {
    this.service.getErupiDistrict().subscribe(data => {
      this.erupiDistrictList = data;
    })
  }
  getErupiBlock() {
    this.service.getErupiBlock(this.selectedDistrict).subscribe(data => {
      this.erupiBlockList = data;
    })
  }
  geterupiStockDetails(dealerCode:any){
    this.geterupiStockData=[];
    let x={
      dealerCode:dealerCode,
      selectedSubsidy: this.selectedSubsidy,
      selectedFromDate: this.selectedFromDate,
      selectedToDate: this.selectedToDate,
    }
    this.service.geterupiStockDetails(x).subscribe(data => {
      this.geterupiStockData = data;
      console.log(this.geterupiStockData);
      
    })
  }
  // applyFilter(filterValue: any) {
  //   const filter =filterValue.value
  //   filterValue.value = filter.trim(); // Remove whitespace
  //   filterValue.value = filter.toLowerCase(); // MatTableDataSource defaults to lowercase matches
  //   this.totalprebookingdtl = filterValue.value;

  // }
}