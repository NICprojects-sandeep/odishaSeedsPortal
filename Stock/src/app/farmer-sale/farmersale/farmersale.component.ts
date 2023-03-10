import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-farmersale',
  templateUrl: './farmersale.component.html',
  styleUrls: ['./farmersale.component.css']
})
export class FarmersaleComponent implements OnInit {
  title: string;
  breadcrumbs: Array<string>;
  FarmerPerDtls: any;
  AccessMode: any = '0';
  RefNo: any = 'ODSON1/2014-15/0001/E';
  LicNo: any = 'ODGAN4/2014-15/0002';
  FarmerIdPre: any;
  FarmerId: any;
  FarmerName: any;
  FatherName: any;
  Category: any;
  Gender: any;
  FarmerCropInfo: any;
  FinYr: any = '2021-22';
  Season: any = 'R';
  selectedFarmerId: any = '';
  MobileNo: any = '';
  allFillFinYr: any = [];
  allFillSeason: any = [];
  selectedFinancialYear: any = '';
  selectedSeasons: any = '';
  status: any = 'T';
  allFillCrops: any = [];
  selectedCrop: any = '';
  selectedVariety: any = '';
  allFillVariety: any = [];
  allFILLDEALERSTOCK: any = [];
  allDatainalist: any = [];
  mobilenolabelshow: boolean = true;
  mobilenolabelhide: boolean = false;
  sendotplabel: boolean = false;
  otplabel: boolean = true;
  changebutton: boolean = true;
  isDisabled: boolean = false;
  getAllStockReceivedData: any = [];
  getAllPreBookingDetails: any = [];
  sumQunitalinQtl: any = 0;
  sumAmount: any = 0;
  showfarmerdetails: boolean = false;
  showfarmerdetails1: boolean = false;
  showfarmerdetails2: boolean = false;
  showfarmerdetails3: boolean = false;
  searchbtn: boolean = true;
  resetbtn: boolean = false;
  farmeridisDisabled: boolean = false;


  constructor(private router: Router, 
    private service: FarmersaleService,
    private toastr: ToastrService) {
    this.title = 'Activate Implements';
    this.breadcrumbs = ['Activate Implements', 'Activation - De-activation or Removal of Implements'];

  }
  ngOnInit(): void {

    this.service.GetDistCodeFromLicNo(this.LicNo).subscribe(data => {
      this.FarmerIdPre = data[0].SHORT_NAME;
    });
    this.FILLDEALERSTOCK();
  }
  reset() {
    this.farmeridisDisabled = false;
    this.allFillFinYr = [];
    this.allFillSeason = [];
    this.allFillCrops = [];
    this.allFillVariety = [];
    this.allFILLDEALERSTOCK = [];
    this.allDatainalist = [];
    this.getAllStockReceivedData = [];
    this.getAllPreBookingDetails = [];
    this.showfarmerdetails = false;
    this.showfarmerdetails1 = false;
    this.showfarmerdetails2 = false;
    this.showfarmerdetails3 = false;
    this.searchbtn = true;
    this.resetbtn = false;
    this.selectedCrop = '';
    this.selectedVariety = '';
    this.changebutton=true;
    this.sendotplabel=true;
    (document.getElementById("farmerid") as HTMLInputElement).value='';
    this.FarmerId='';
  }
  getFarmerPerDtl() {
    var num1 = ((document.getElementById("farmerid") as HTMLInputElement).value);
    if (num1 == '') {
      // alert("Farmer ID Can not be Blank");
      this.toastr.warning(`Farmer ID Can not be Blank.`);
    }
    else {
      this.FarmerId = this.FarmerIdPre + '/' + num1.toString();
      this.service.GetFarmerInfo(this.FarmerId).subscribe(data => {
        this.selectedFarmerId = this.FarmerId;
        this.FarmerName = data[0].VCHFARMERNAME;
        this.FatherName = data[0].VCHFARMERNAME;
        this.Category = data[0].Category_Value;
        this.Gender = data[0].Gender_Value;
        this.MobileNo = data[0].STARMOBILENO;
        this.showfarmerdetails = true;
        this.showfarmerdetails1 = false;
        this.showfarmerdetails2 = false;
        this.showfarmerdetails3 = false;
        this.searchbtn = false;
        this.resetbtn = true;
        this.farmeridisDisabled = true;
      });
      this.FillFinYr();
      this.service.GetFarmerRecvCrop(this.FarmerId, this.FinYr, this.Season).subscribe(data => {
        this.FarmerCropInfo = data;
      })
      // alert(this.FarmerId.toString());      
    }
  }
  FillFinYr() {
    this.allFillFinYr = []
    this.service.FillFinYr(this.status).subscribe(data => {
      this.allFillFinYr = data;
      this.selectedFinancialYear = this.allFillFinYr[0].FIN_YR;
      this.FillSeason();
    })
  }
  FillSeason() {
    this.allFillSeason = []
    this.service.FillSeason(this.selectedFinancialYear, this.status).subscribe(data => {
      this.allFillSeason = data;
      this.selectedSeasons = this.allFillSeason[0];
      this.FillCrops();
      this.getStockReceivedData();
      this.getPreBookingDetails();
    })
  }
  FillCrops() {
    this.allFillCrops = [];
    this.allFILLDEALERSTOCK = [];
    this.allFillVariety = [];
    this.service.FillCrops(this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME, this.LicNo).subscribe(data => {
      this.allFillCrops = data;
    })
  };
  FillVariety() {
    this.allFillVariety = [];
    this.service.FillVariety(this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME, this.selectedCrop.CROP_CODE, this.LicNo).subscribe(data => {
      this.allFillVariety = data;
    })
  };
  FILLDEALERSTOCK() {
    this.allFILLDEALERSTOCK = [];
    this.service.FILLDEALERSTOCK(this.LicNo, this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME, this.selectedCrop.CROP_CODE, this.selectedVariety.VARIETY_CODE, 'OSSC').subscribe(data => {
      this.allFILLDEALERSTOCK = data;
      console.log(this.allFILLDEALERSTOCK);

      // this.allFILLDEALERSTOCK = [{
      //   LOT_NO: 'NOV/21-18-282-08G72798-1',
      //   Receive_Unitname: 'OSSC',
      //   BAG_SIZE_IN_KG: '20',
      //   AVL_BAGS: '93',
      //   AVL_QUANTITY: '18.60',
      //   All_in_cost_Price: '3537.00',
      //   Subsidy_Qtl: '1127.00',
      //   QunitalinQtl: '0',
      //   Amount: '0'

      // },
      // {
      //   LOT_NO: 'NOV/21-18-282-08G72798-2',
      //   Receive_Unitname: 'OSSC',
      //   BAG_SIZE_IN_KG: '20',
      //   AVL_BAGS: '93',
      //   AVL_QUANTITY: '18.60',
      //   All_in_cost_Price: '3537.00',
      //   Subsidy_Qtl: '1127.00',
      //   QunitalinQtl: '0',
      //   Amount: '0'

      // }]
    })
  };
  getStockReceivedData() {
    this.getAllStockReceivedData = [];
    this.service.getStockReceivedData(this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME, this.FarmerId).subscribe(data => {
      this.getAllStockReceivedData = data;
    })
  }
  getPreBookingDetails() {
    this.getAllPreBookingDetails = [];
    this.service.getPreBookingDetails(this.selectedFinancialYear, this.selectedSeasons.SEASSION_NAME, this.FarmerId).subscribe(data => {
      this.getAllPreBookingDetails = data;
    })
  }
  mobilenumberchanged(x: any) {
    if (x == 1) {
      this.mobilenolabelshow = false;
      this.mobilenolabelhide = true;
      this.otplabel = false;
      this.MobileNo='';
    }
    else {
      this.otplabel = true;
      this.isDisabled = true
      this.changebutton = false;
      this.mobilenolabelshow = true;
      this.mobilenolabelhide = false;
    }
  }
  sendotp() {
    this.sendotplabel = true;
    this.changebutton = false;
    this.otplabel = false;
  }
  otpSubmit() {
    this.showfarmerdetails1 = true;
    this.showfarmerdetails2 = false;
    this.showfarmerdetails3 = false;
    this.sendotplabel=false;
  }
  addinaList(LOT_NO: any, Receive_Unitname: any, BAG_SIZE_IN_KG: any, enteredNoOfBags: any, QunitalinQtl: any, Amount: any, RECEIVE_UNITCD: any, AVL_QUANTITY: any, All_in_cost_Price: any, i: any, TOT_SUBSIDY: any) {
    // new DataColumn("CROP_ID", typeof(string)),
    // new DataColumn("Crop_Name", typeof(string)),
    // new DataColumn("CROP_VERID", typeof(string)),
    // new DataColumn("Crop_VerName", typeof(string)),
    // new DataColumn("LOT_NO", typeof(string)),
    // new DataColumn("Receive_Unitcd",typeof(string)),
    // new DataColumn("Receive_Unitname",typeof(string)),
    // new DataColumn("BAG_SIZE_KG",typeof(string)),
    // new DataColumn("NO_OF_BAGS",typeof(string)),
    // new DataColumn("QUANTITY",typeof(string)),
    // new DataColumn("PRICE_QTL",typeof(string)),
    // new DataColumn("SUBSIDY_QTL",typeof(string)),
    // new DataColumn("AMOUNT",typeof(string))
    let x: any = {}
    x.CROP_ID = this.selectedCrop.CROP_CODE;
    x.Crop_Name = this.selectedCrop.CROP_NAME;
    x.CROP_VERID = this.selectedVariety.VARIETY_CODE;
    x.Crop_VerName = this.selectedVariety.VARIETY_NAME;
    x.LOT_NO = LOT_NO;
    x.Receive_Unitcd = RECEIVE_UNITCD
    x.Receive_Unitname = Receive_Unitname;
    x.BAG_SIZE_KG = BAG_SIZE_IN_KG;
    x.NO_OF_BAGS = enteredNoOfBags;
    x.QUANTITY = QunitalinQtl;
    x.AVL_QUANTITY = AVL_QUANTITY;
    x.PRICE_QTL = All_in_cost_Price;
    x.SUBSIDY_QTL = TOT_SUBSIDY;
    x.Amount = All_in_cost_Price * QunitalinQtl;

    this.sumQunitalinQtl = 0;
    this.sumAmount = 0;
    this.allDatainalist.push(x);
    this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
    this.allFILLDEALERSTOCK[i].Amount = 0;
    this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';

    this.allDatainalist.forEach((i: any) => {
      if (i.hasOwnProperty('QUANTITY')) {
        var a = (i.QUANTITY == undefined || i.QUANTITY == null || i.QUANTITY == '') ? 0.00 : i.QUANTITY;
        this.sumQunitalinQtl = (this.sumQunitalinQtl) + parseFloat(a);
      }
      if (i.hasOwnProperty('Amount')) {
        var b = (i.Amount == undefined || i.Amount == null || i.Amount == '') ? 0.00 : i.Amount;
        this.sumAmount = (this.sumAmount) + parseFloat(b);
      }
    })

    this.showfarmerdetails2 = true;
    this.showfarmerdetails3 = true;
  }
  removeinaList(x: any) {
    this.allDatainalist.forEach((item: any, index: any) => {
      if (item === x) this.allDatainalist.splice(index, 1);
    });
  }
  changequnital(BAG_SIZE_IN_KG: any, enteredNoOfBags: any, i: any, All_in_cost_Price: any) {
    this.allFILLDEALERSTOCK[i].QunitalinQtl = (BAG_SIZE_IN_KG * enteredNoOfBags) / 100;
    this.allFILLDEALERSTOCK[i].Amount = (this.allFILLDEALERSTOCK[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
  }
  InsertSaleDealer() {
    const alldata = {
      FARMER_ID: this.selectedFarmerId,
      SEASON: this.selectedSeasons.SEASSION_NAME,
      FINYR: this.selectedFinancialYear,
      VALUES: this.allDatainalist,
      LICENCE_NO: this.LicNo
    };

    this.service.InsertSaleDealer(alldata).subscribe(data => {
      if (data.Val == '1') {
        alert(`Transaction Completed!!!`)
      }
      if (data.Val == '0') {
        alert(`Some Errors Occurred!!!`)
      }
      // this.allFILLDEALERSTOCK = data;
    })
  }

}
