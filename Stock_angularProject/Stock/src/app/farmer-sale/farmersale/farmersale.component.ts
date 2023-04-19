import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import { promise } from 'protractor';
import { resolve } from 'dns';

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
  LicNo: any = 'ODPUR3/2018-19/0018';// ODPUR3/2018-19/0018 ODGAN4/2014-15/0002
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
  selectedCrop: any = {};
  selectedVariety: any = {};
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
  deliveredFrom: any;
  farmerName: any;
  FathersName: any;
  MobileNumber: any;
  Village: any;
  GP: any;
  Block: any;
  Dist: any;
  selectedIndex: number | undefined;
  showCheackBox: boolean = false;
  scrop: any;
  cropCheack: boolean = false;
  cropCheackfalse: boolean = true;
  svariety: any;
  VarietyCheack: boolean = false
  VarietyCheackfalse: boolean = true;
  selectedIndex1: any;
  inputfiled: boolean = true;
  selectedEnterNoofBags: any;
  enteredOtp: any = ''
  farmerDetails: any = [];
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
    this.changebutton = true;
    this.sendotplabel = false;
    this.otplabel = true;
    this.mobilenolabelshow = true;
    this.mobilenolabelhide = false;


    this.showCheackBox = false;
    this.scrop = '';
    this.cropCheack = false;
    this.cropCheackfalse = true;
    this.svariety = '';
    this.VarietyCheack = false
    this.VarietyCheackfalse = true;
    this.inputfiled = true;
    this.selectedEnterNoofBags = '';
    this.selectedIndex = undefined;
    this.selectedIndex1 = undefined;
    this.farmerDetails = [];
    this.enteredOtp = '';

    (document.getElementById("farmerid") as HTMLInputElement).value = '';
    this.FarmerId = '';
  }
  getFarmerPerDtl() {
    var num1 = ((document.getElementById("farmerid") as HTMLInputElement).value);
    if (num1 == '') {
      this.toastr.warning(`Farmer ID Can not be Blank.`);
    }
    else {
      this.farmerDetails = [];
      this.FarmerId = this.FarmerIdPre + '/' + num1.toString();
      this.service.GetFarmerInfo(this.FarmerId).subscribe(data => {
        console.log(data);
        this.farmerDetails = data;
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
      // this.allFillCrops = data;
      this.allFillCrops = [{ CROP_CODE: 'C003', CROP_NAME: 'PaddyDhan1' }, { CROP_CODE: 'C002', CROP_NAME: 'PaddyDhan' }];
    })
  };
  FillVariety = () => {
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.allFillVariety = [];
        this.allFillVariety = await this.service.FillVariety(this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME, this.selectedCrop.CROP_CODE, this.LicNo).toPromise()
        this.inputfiled = true;
        resolve(this.allFillVariety)
      } catch (e) {
        console.error(e);

        reject()
      }
    })
  };
  FILLDEALERSTOCK() {
    this.allFILLDEALERSTOCK = [];
    this.service.FILLDEALERSTOCK(this.LicNo, this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME, this.selectedCrop.CROP_CODE, this.selectedVariety.VARIETY_CODE, 'OSSC').subscribe(data => {
      this.allFILLDEALERSTOCK = data;
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
      this.MobileNo = '';
    }
    else {
      if (this.MobileNo.length == 10) {
        this.otplabel = true;
        this.isDisabled = true
        this.changebutton = false;
        this.mobilenolabelshow = true;
        this.mobilenolabelhide = false;
      }
      else {
        this.toastr.warning(`Please Enter Valid Mobile Number.`);

      }

    }
  }
  sendotp() {
    this.sendotplabel = true;
    this.changebutton = false;
    this.otplabel = false;
    this.service.sendOtp(this.FarmerId, this.farmerDetails[0].VCHMOBILENO, this.LicNo).subscribe(data => {
      if (data == 1) {
        this.toastr.success(`OTP has been sent successfully (Valid for 10min)`);
      }
      else {
        this.toastr.error(`Please try another time`);
      }
    })
  }

  ValidateOTP() {
    this.service.ValidateOTP(this.FarmerId, this.enteredOtp, this.LicNo).subscribe(data => {
      if (data == 1) {
        this.showfarmerdetails1 = true;
        this.showfarmerdetails2 = false;
        this.showfarmerdetails3 = false;
        this.sendotplabel = false;
        this.toastr.success(`OTP Matched successfully !!`);
        if (this.getAllPreBookingDetails.length > 0) {
          this.showCheackBox = true;
        }
      }
      else {
        this.toastr.warning(`Incorrect OTP Entered!!`);
      }

    })

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
    if (enteredNoOfBags != null && enteredNoOfBags != undefined && enteredNoOfBags != '') {
      let x: any = {}
      x.CROP_ID = this.selectedCrop.CROP_CODE;
      x.Crop_Name = this.selectedCrop.CROP_NAME;
      x.CROP_VERID = this.selectedVariety.VARIETY_CODE;
      x.Crop_VerName = this.selectedVariety.VARIETY_NAME;
      x.LOT_NO = LOT_NO;
      x.Receive_Unitcd = parseInt(RECEIVE_UNITCD)
      x.Receive_Unitname = Receive_Unitname;
      x.BAG_SIZE_KG = parseInt(BAG_SIZE_IN_KG);
      x.NO_OF_BAGS = parseInt(enteredNoOfBags);
      x.QUANTITY = QunitalinQtl.toFixed(2);
      x.AVL_QUANTITY = AVL_QUANTITY;
      x.PRICE_QTL = All_in_cost_Price;
      x.SUBSIDY_QTL = TOT_SUBSIDY;
      x.Amount = (All_in_cost_Price * QunitalinQtl).toFixed(2);

      this.sumQunitalinQtl = 0;
      this.sumAmount = 0;
      this.allDatainalist.push(x);
      this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
      this.allFILLDEALERSTOCK[i].Amount = 0;
      this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';

      this.allDatainalist.forEach((i: any) => {
        if (i.hasOwnProperty('QUANTITY')) {
          var a = (i.QUANTITY == undefined || i.QUANTITY == null || i.QUANTITY == '') ? 0.00 : i.QUANTITY;
          this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
        }
        if (i.hasOwnProperty('Amount')) {
          var b = (i.Amount == undefined || i.Amount == null || i.Amount == '') ? 0.00 : i.Amount;
          this.sumAmount = (parseFloat(this.sumAmount) + parseFloat(b)).toFixed(2);
        }
      })

      this.showfarmerdetails2 = true;
      this.showfarmerdetails3 = true;
      this.selectedIndex1 = undefined;
      this.inputfiled = true;
    }
    else {
      this.toastr.warning(`Please Enter Total number of Bags.`);
    }

  }
  removeinaList(x: any) {
    this.allDatainalist.forEach((item: any, index: any) => {
      if (item === x) this.allDatainalist.splice(index, 1);
    });
  }
  changequnital(BAG_SIZE_IN_KG: any, enteredNoOfBags: any, i: any, All_in_cost_Price: any) {
    if (enteredNoOfBags != null && enteredNoOfBags != undefined && enteredNoOfBags != 0) {
      this.allFILLDEALERSTOCK[i].QunitalinQtl = (BAG_SIZE_IN_KG * enteredNoOfBags) / 100;
      this.allFILLDEALERSTOCK[i].Amount = (this.allFILLDEALERSTOCK[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
    }

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
        this.toastr.success(`Transaction Completed!!!`);
        this.PrintReport()
      }
      if (data.Val == '0') {
        this.toastr.warning(`Some Errors Occurred!!!`);
      }
      // this.allFILLDEALERSTOCK = data;
    })
  }
  PrintReport() {
    this.FarmerId = 'GAN/163047'
    this.service.GetFirmName(this.LicNo).subscribe(data => {
      this.deliveredFrom = data.result[0].APP_FIRMNAME
    });
    this.service.GetFarmerInvHdr(this.FarmerId).subscribe(data1 => {
      this.farmerName = data1[0].VCHFARMERNAME;
      this.FathersName = data1[0].VCHFATHERNAME;
      this.MobileNumber = data1[0].VCHMOBILENO;
      this.Village = data1[0].villg_name;
      this.GP = data1[0].GP_Name;
      this.Block = data1[0].BLOCK_NAME;
      this.Dist = data1[0].Dist_Name;
    });
  }
  changeSelection = async (event: any, index: any, value: any) => {
    this.selectedIndex = event.target.checked ? index : undefined;
    if (event.target.checked) {
      this.scrop = this.allFillCrops.find((x: any) => x.CROP_CODE === value.Crop_Code);
      if (this.scrop != undefined) {
        this.cropCheack = true;
        this.cropCheackfalse = false;
        this.VarietyCheack = true;
        this.VarietyCheackfalse = false;
        this.selectedCrop.CROP_CODE = value.Crop_Code
        this.selectedCrop.CROP_NAME = value.Crop_Name
        const FillVariety = await this.FillVariety();

        this.svariety = this.allFillVariety.find((x: any) => x.VARIETY_CODE == value.Variety_Code);
        if (this.svariety != undefined) {
          this.selectedVariety.VARIETY_CODE = value.Variety_Code
          this.selectedVariety.VARIETY_NAME = value.Variety_Name;
          this.selectedEnterNoofBags = value.noOfBag;
          this.FILLDEALERSTOCK();
          this.inputfiled = true;
        }
        else {
          this.allFillCrops = [];
          this.allFILLDEALERSTOCK = [];
          this.allFillVariety = [];
          this.cropCheack = false;
          this.cropCheackfalse = true;
          this.VarietyCheack = false;
          this.VarietyCheackfalse = true;
          this.FillCrops();
          this.FillVariety();
          this.selectedCrop = {};
          this.selectedVariety = {};
          this.selectedEnterNoofBags = '';
          this.inputfiled = true;
          this.toastr.warning(`This Stock Is not available`);
        }
        // const promise1 = new Promise((resolve, reject) => {
        //   resolve(this.FillVariety());
        // });
        // promise1.then((x) => {
        //   console.log(x);

        //   setTimeout(() => {
        //     console.log(this.allFillVariety, value.Variety_Code);

        //     this.svariety = this.allFillVariety.find((x: any) => x.VARIETY_CODE == value.Variety_Code);
        //     console.log(this.svariety);

        // if (this.svariety != undefined) {
        //   this.selectedVariety.VARIETY_CODE = value.Variety_Code
        //   this.selectedVariety.VARIETY_NAME = value.Variety_Name;
        //   this.selectedEnterNoofBags = value.noOfBag;
        //   this.FILLDEALERSTOCK();
        // }
        // else {
        //   this.allFillCrops = [];
        //   this.allFILLDEALERSTOCK = [];
        //   this.allFillVariety = [];
        //   this.cropCheack = false;
        //   this.cropCheackfalse = true;
        //   this.VarietyCheack = false;
        //   this.VarietyCheackfalse = true;
        //   this.FillCrops();
        //   this.FillVariety();
        //   this.selectedCrop = {};
        //   this.selectedVariety = {};
        //   this.selectedEnterNoofBags = '';
        //   this.toastr.warning(`This Stock Is not available1`);
        // }

        //   }, 50);
        // });


      }
      else {
        this.allFillCrops = [];
        this.allFILLDEALERSTOCK = [];
        this.allFillVariety = [];
        this.cropCheack = false;
        this.cropCheackfalse = true;
        this.VarietyCheack = false;
        this.VarietyCheackfalse = true;
        this.FillCrops();
        this.FillVariety();
        this.selectedEnterNoofBags = '';
        this.selectedCrop = {};
        this.selectedVariety = {};
        this.inputfiled = true;
        this.toastr.warning(`This Stock Is not available`);
      }



      // this.FillVariety()
    }
    else {
      this.cropCheack = false;
      this.cropCheackfalse = true;
      this.VarietyCheack = false;
      this.VarietyCheackfalse = true;
      this.selectedEnterNoofBags = '';
      this.selectedCrop = {};
      this.selectedVariety = {};
      this.inputfiled = true;
      this.FillCrops();
      this.FillVariety();
    }



    // this.allFillCrops.forEach((item: any) => {
    //   // const isPresentInArry1 = value.Crop_Code == item.CROP_CODE;
    //   console.log(value.Crop_Code === item.CROP_CODE, 'jjjj');
    //   if (value.Crop_Code != item.CROP_CODE) {
    //     // this.toastr.warning(`This Stock Is not present`);
    //   }
    //   else {
    //     this.scrop = this.allFillCrops.find((x: any) => x.CROP_CODE === value.Crop_Code);
    //     console.log(this.scrop);

    //     // this.selectedCrop.CROP_NAME=value.Crop_Name;
    //     // this.selectedCrop.CROP_CODE=value.Crop_Code;
    //     // this.selectedCrop.CROP_NAME = this.allFillCrops.find((x: any) => x.CROP_NAME === value.Crop_Name);
    //     // this.allFillCrops[0].


    //   }


    // });


  }

  changeSelection1(event: any, index: any, value: any) {
    this.selectedIndex1 = event.target.checked ? index : undefined;
    this.allFILLDEALERSTOCK[this.selectedIndex1].enteredNoOfBags = this.selectedEnterNoofBags;
    this.changequnital(value.BAG_SIZE_IN_KG, value.enteredNoOfBags, index, value.All_in_cost_Price);

    this.inputfiled = false;
  }
  // GetFirmName(){
  //   this.service.GetFirmName(this.LicNo).subscribe(data => {
  //     this.deliveredFrom=data.result[0].APP_FIRMNAME
  //   });
  // }
}
