import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import { promise } from 'protractor';
import { resolve } from 'dns';

import { NgxSpinnerService } from 'ngx-spinner'
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
  STARVCHACCOUNTNO: any;
  vchBankName: any;
  TRANSACTION_ID: any;
  TOT_AMT: any;
  SUB_AMT: any;
  todateDate: any;
  prebookingcheack: any = '';
  insertedBy: any;
  printPage: boolean = false;
  viewpage: boolean = true;
  Prebookedamount: any;
  totalPaybleamount: any;
  prebookingtype: boolean = false;
  prebookedsale: boolean = false;
  prebookingApplicationId:any='';
  minutes: number = 10;
  seconds: number = 0;
  interval: any;
  resendotp:boolean=true;
  constructor(private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) {

    this.title = 'Activate Implements111';
    this.breadcrumbs = ['Activate Implements', 'Activation - De-activation or Removal of Implements'];


  }
  ngOnInit(): void {
    this.PrintReport()
    // this.route.queryParams
    //   .subscribe((params) => {
    //     this.insertedBy = params.userID;
    //     localStorage.setItem('userId', params.userID);
    //     if (this.insertedBy != "undefined" && this.insertedBy != undefined) {          
    this.service.GetDistCodeFromLicNo().subscribe(data => {
      this.FarmerIdPre = data[0].SHORT_NAME;

      // });
      // this.FILLDEALERSTOCK();
      //   }
      //   else {          
      //     window.location.href = 'https://agrisnetodisha.ori.nic.in/stock/login.aspx';
      //     localStorage.removeItem('userId');
      //   }
    }
    );




    // this.service.GetDistCodeFromLicNo().subscribe(data => {
    //   this.FarmerIdPre = data[0].SHORT_NAME;
    // });
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
    this.prebookingtype = false;
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
    this.printPage = false;
    this.prebookedsale = false;
    (document.getElementById("farmerid") as HTMLInputElement).value = '';
    this.FarmerId = '';
  }
  getFarmerPerDtl() {
    var num1 = ((document.getElementById("farmerid") as HTMLInputElement).value);
    if (num1 == '') {
      this.toastr.warning(`Farmer ID Can not be Blank.`);
    }
    else {
      this.spinner.show();
      this.farmerDetails = [];
      this.FarmerId = this.FarmerIdPre + '/' + num1.toString();
      this.service.GetFarmerInfo(this.FarmerId).subscribe(data => {
        this.farmerDetails = data;
        if(this.farmerDetails.length > 0){
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
          this.FillFinYr();
          this.service.GetFarmerRecvCrop(this.FarmerId, this.FinYr, this.Season).subscribe(data => {
            this.FarmerCropInfo = data;
          })
        }
       else{
        this.toastr.warning(`Plase enter valid Farmer ID.`);
       }

        this.spinner.hide();
      });
     
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
    this.service.FillCrops(this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME).subscribe(data => {
      this.allFillCrops = data;
      console.log(this.allFillCrops);

      // this.allFillCrops = [{ CROP_CODE: 'C003', CROP_NAME: 'PaddyDhan1' }, { CROP_CODE: 'C002', CROP_NAME: 'PaddyDhan' }];
    })
  };
  FillVariety = () => {
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.allFillVariety = [];
        this.allFillVariety = await this.service.FillVariety(this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME, this.selectedCrop.CROP_CODE).toPromise()
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
    this.service.FILLDEALERSTOCK( this.selectedFinancialYear, this.selectedSeasons.SHORT_NAME, this.selectedCrop.CROP_CODE, this.selectedVariety.VARIETY_CODE, 'OSSC').subscribe(data => {
      this.allFILLDEALERSTOCK = data;
      this.allFILLDEALERSTOCK.forEach((a: any) => {
        a.ischeacked = true;
      });
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
      console.log(this.getAllPreBookingDetails);

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
    this.resendotp=true;
    this.minutes=10;
    this.seconds=0;
    clearInterval(this.interval);
    this.interval = setInterval(() => {
      if (this.seconds > 0) {
        this.seconds--;
      } else {
        if (this.minutes > 0) {
          this.minutes--;
          this.seconds = 59;
        } else {
          clearInterval(this.interval);
          this.resendotp=false;
          console.log("Countdown finished");
          // Timer has reached 0
        }
      }
    }, 1000);
    this.sendotplabel = true;
    this.changebutton = false;
    this.otplabel = false;
    this.spinner.show();
    // this.service.sendOtp(this.FarmerId, this.farmerDetails[0].VCHMOBILENO, ).subscribe(data => {      
    //   if (data == 1) {
    this.spinner.hide();
    this.toastr.success(`OTP has been sent successfully (Valid for 10min)`);
    // }
    // else {
    //   this.toastr.error(`Please try another time`);
    // }
    // })
  }

  ValidateOTP() {
    // this.service.ValidateOTP(this.FarmerId, this.enteredOtp,).subscribe(data => {      
    //   if (data == 1) {
    this.showfarmerdetails1 = true;
    this.showfarmerdetails2 = false;
    this.showfarmerdetails3 = false;
    this.sendotplabel = false;
    this.toastr.success(`OTP Matched successfully !!`);
    this.prebookingtype = true;
    if (this.getAllPreBookingDetails.length > 0) {
      this.showCheackBox = true;
    }
    // }
    // else {
    //   this.toastr.warning(`Incorrect OTP Entered!!`);
    // }

    // })

  }
  addinaList(LOT_NO: any, Receive_Unitname: any, BAG_SIZE_IN_KG: any, enteredNoOfBags: any, QunitalinQtl: any, Amount: any, RECEIVE_UNITCD: any, AVL_QUANTITY: any, All_in_cost_Price: any, i: any, TOT_SUBSIDY: any, AVL_BAGS: any,ischeacked:any) {
    if (enteredNoOfBags != null && enteredNoOfBags != undefined && enteredNoOfBags != '' && enteredNoOfBags != 0 && enteredNoOfBags != '0') {
      console.log(enteredNoOfBags, AVL_BAGS, AVL_BAGS >= enteredNoOfBags, AVL_BAGS <= enteredNoOfBags);
      if (AVL_BAGS >= enteredNoOfBags) {
        let x: any = {}
        x.CROP_ID = this.selectedCrop.CROP_CODE;
        x.Crop_Name = this.selectedCrop.CROP_NAME;
        x.CROP_VERID = this.selectedVariety.VARIETY_CODE;
        x.Crop_VerName = this.selectedVariety.VARIETY_NAME;
        x.LOT_NO = LOT_NO;
        x.Receive_Unitcd = RECEIVE_UNITCD;
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
        this.allFILLDEALERSTOCK[i].ischeacked = true;
        if (!this.allDatainalist.some((j: any) => j.CROP_ID == x.CROP_ID && x.CROP_VERID == j.CROP_VERID && x.LOT_NO == j.LOT_NO)) {

          this.allDatainalist.push(x);
          this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
          this.allFILLDEALERSTOCK[i].Amount = 0;
          this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';

          this.allDatainalist.forEach((y: any, index: any) => {
            if (y.hasOwnProperty('QUANTITY')) {
              var a = (y.QUANTITY == undefined || y.QUANTITY == null || y.QUANTITY == '') ? 0.00 : y.QUANTITY;
              this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);

              if (index + 1 == this.allDatainalist.length) {
                this.toastr.success(`Stock Added Sucessfully.`);
                this.allFILLDEALERSTOCK[i].AVL_QUANTITY = (this.allFILLDEALERSTOCK[i].AVL_QUANTITY - QunitalinQtl).toFixed(2);
                this.allFILLDEALERSTOCK[i].AVL_BAGS = this.allFILLDEALERSTOCK[i].AVL_BAGS - parseInt(enteredNoOfBags);
                
              }
            }
            if (y.hasOwnProperty('Amount')) {
              var b = (y.Amount == undefined || y.Amount == null || y.Amount == '') ? 0.00 : y.Amount;
              this.sumAmount = (parseFloat(this.sumAmount) + parseFloat(b)).toFixed(2);
            }
          })

          this.showfarmerdetails2 = true;
          this.showfarmerdetails3 = true;
          this.selectedIndex1 = undefined;
          this.inputfiled = true;
        }
        else {
          let index = this.allDatainalist.findIndex((y: any) => y.CROP_ID === x.CROP_ID && x.CROP_VERID == y.CROP_VERID && x.LOT_NO == y.LOT_NO);
          console.log(AVL_BAGS >= x.NO_OF_BAGS,AVL_BAGS , x.NO_OF_BAGS);
          
          if (AVL_BAGS >= x.NO_OF_BAGS) {
            this.allDatainalist[index].Amount = (parseFloat(x.Amount) + parseFloat(this.allDatainalist[index].Amount)).toFixed(2);
            this.allDatainalist[index].NO_OF_BAGS = x.NO_OF_BAGS + this.allDatainalist[index].NO_OF_BAGS;
            this.allDatainalist[index].QUANTITY = (parseFloat(x.QUANTITY) + parseFloat(this.allDatainalist[index].QUANTITY)).toFixed(2);
            console.log(this.allDatainalist[index].NO_OF_BAGS, AVL_BAGS, 'AVL_BAGS');

            this.allDatainalist.forEach((y: any, index: any) => {
              if (y.hasOwnProperty('QUANTITY')) {
                var a = (y.QUANTITY == undefined || y.QUANTITY == null || y.QUANTITY == '') ? 0.00 : y.QUANTITY;
                this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
                if (index + 1 == this.allDatainalist.length) {
                  this.toastr.success(`Stock Added Sucessfully.`);
                  this.allFILLDEALERSTOCK[i].AVL_QUANTITY = (this.allFILLDEALERSTOCK[i].AVL_QUANTITY - QunitalinQtl).toFixed(2);
                  this.allFILLDEALERSTOCK[i].AVL_BAGS = this.allFILLDEALERSTOCK[i].AVL_BAGS - parseInt(enteredNoOfBags);
                  
                }
              }
              if (y.hasOwnProperty('Amount')) {
                var b = (y.Amount == undefined || y.Amount == null || y.Amount == '') ? 0.00 : y.Amount;
                this.sumAmount = (parseFloat(this.sumAmount) + parseFloat(b)).toFixed(2);
              }
            })
            this.inputfiled = true;
            this.selectedIndex1 = undefined;
            this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
            this.allFILLDEALERSTOCK[i].Amount = 0;
            this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';
          } else {
            this.toastr.warning(`Insufficient stocK.`);
            this.inputfiled = true;
            this.selectedIndex1 = undefined;
            this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
            this.allFILLDEALERSTOCK[i].Amount = 0;
            this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';
          }

        }

      }
      else {        
        this.toastr.warning(`Insufficient stocK.`);
        this.inputfiled = true;
        this.selectedIndex1 = undefined;
        this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
        this.allFILLDEALERSTOCK[i].Amount = 0;
        this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';
        this.allFILLDEALERSTOCK[i].ischeacked=true;        
      }



    }
    else {
      this.toastr.warning(`Please Enter Total number of Bags.`);
    }

  }


  removeinaList(x: any) {
    this.allDatainalist.forEach((item: any, index: any) => {
      if (item === x) this.allDatainalist.splice(index, 1);
      this.sumQunitalinQtl = this.sumQunitalinQtl - x.QUANTITY;
      this.sumAmount = this.sumAmount - x.Amount;
    });
    this.allFILLDEALERSTOCK.forEach((items: any, index: any) => {
      console.log(x);
      console.log(items);
      

      if (items.LOT_NO == x.LOT_NO && items.CROP_VERID == x.CROP_VERID) {
        
        items.AVL_BAGS += x.NO_OF_BAGS;
        items.AVL_QUANTITY = parseFloat(items.AVL_QUANTITY) + parseFloat(x.QUANTITY);
      }
    });
  }
  changequnital(BAG_SIZE_IN_KG: any, enteredNoOfBags: any, i: any, All_in_cost_Price: any) {
    if (enteredNoOfBags != null && enteredNoOfBags != undefined) {

      if (this.prebookingcheack) {
        if (this.selectedEnterNoofBags >= enteredNoOfBags) {
          this.allFILLDEALERSTOCK[i].QunitalinQtl = (BAG_SIZE_IN_KG * enteredNoOfBags) / 100;
          this.allFILLDEALERSTOCK[i].Amount = (this.allFILLDEALERSTOCK[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
          if (enteredNoOfBags == 0) {
            this.allFILLDEALERSTOCK[i].QunitalinQtl = 0.00;
            this.allFILLDEALERSTOCK[i].Amount = 0.00;
          }
        }
        else {
          this.toastr.warning(`Plase enter valid number.`);
          this.allFILLDEALERSTOCK[i].enteredNoOfBags = this.selectedEnterNoofBags;
          this.allFILLDEALERSTOCK[i].QunitalinQtl = (BAG_SIZE_IN_KG * this.selectedEnterNoofBags) / 100;
          this.allFILLDEALERSTOCK[i].Amount = (this.allFILLDEALERSTOCK[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
        }
      }
      else {
        this.allFILLDEALERSTOCK[i].QunitalinQtl = (BAG_SIZE_IN_KG * enteredNoOfBags) / 100;
        this.allFILLDEALERSTOCK[i].Amount = (this.allFILLDEALERSTOCK[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
      }

    }

  }
  InsertSaleDealer() {
    this.spinner.show();
    const alldata = {
      FARMER_ID: this.selectedFarmerId,
      SEASON: this.selectedSeasons.SEASSION_NAME,
      FINYR: this.selectedFinancialYear,
      VALUES: this.allDatainalist,
      PrebookingorNot: this.prebookedsale,
      applicationId: this.prebookedsale == true ? this.prebookingApplicationId : '',
    };

    this.service.InsertSaleDealer(alldata).subscribe(data => {
      this.spinner.hide();
      console.log(data);
      if (data.result == "True") {
        
        this.TRANSACTION_ID = data.TRANSACTION_ID;
        this.toastr.success(`Transaction Completed & Transaction Id is  ${data.TRANSACTION_ID}.`);
        this.PrintReport();
        this.printPage = true;
        this.viewpage = false;
      }
     else {
        this.toastr.warning(`Some Errors Occurred!!!`);
      }
      // this.allFILLDEALERSTOCK = data;
    })
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
          this.prebookingcheack = true;
          this.prebookingApplicationId = value.applicationID;
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
          this.prebookingcheack = false;
          this.toastr.warning(`This Stock Is not available`);
        }


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
        this.prebookingcheack = false;
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
      this.prebookingcheack = false;
      this.FillCrops();
      this.FillVariety();
    }



    // this.allFillCrops.forEach((item: any) => {
    //   // const isPresentInArry1 = value.Crop_Code == item.CROP_CODE;
    //   if (value.Crop_Code != item.CROP_CODE) {
    //     // this.toastr.warning(`This Stock Is not present`);
    //   }
    //   else {
    //     this.scrop = this.allFillCrops.find((x: any) => x.CROP_CODE === value.Crop_Code);
    //     // this.selectedCrop.CROP_NAME=value.Crop_Name;
    //     // this.selectedCrop.CROP_CODE=value.Crop_Code;
    //     // this.selectedCrop.CROP_NAME = this.allFillCrops.find((x: any) => x.CROP_NAME === value.Crop_Name);
    //     // this.allFillCrops[0].


    //   }


    // });


  }

  changeSelection1(event: any, index: any, value: any) {
    this.selectedIndex1 = event.target.checked ? index : undefined;
    if (this.selectedIndex1 != undefined) {
      this.allFILLDEALERSTOCK.forEach((x: any) => {
        if (x.LOT_NO == this.allFILLDEALERSTOCK[index].LOT_NO) {
          x.ischeacked = false;
          x.enteredNoOfBags = this.selectedEnterNoofBags;
          this.changequnital(value.BAG_SIZE_IN_KG, value.enteredNoOfBags, index, value.All_in_cost_Price);
          this.inputfiled = false;
        }
        else if (x.LOT_NO != this.allFILLDEALERSTOCK[index].LOT_NO) {
          x.ischeacked = true;
          x.enteredNoOfBags = '';
          x.QunitalinQtl = 0.00;
          x.Amount = 0.00;
          this.inputfiled = false;
        }

      });

    }
    else {
      this.allFILLDEALERSTOCK.forEach((x: any) => {
        x.ischeacked = true;
        x.enteredNoOfBags = '';
        this.changequnital(value.BAG_SIZE_IN_KG, 0, index, value.All_in_cost_Price);
        this.inputfiled = true;
      });
    }


  }
  // GetFirmName(){
  //   this.service.GetFirmName(this.LicNo).subscribe(data => {
  //     this.deliveredFrom=data.result[0].APP_FIRMNAME
  //   });
  // }

  PrintReport() {
    this.FarmerId = this.FarmerId
    this.service.GetFirmName().subscribe(data => {
      this.deliveredFrom = data.result[0].APP_FIRMNAME;
      this.LicNo= data.result[0].LIC_NO;
    });
    this.service.GetFarmerInvHdr(this.FarmerId).subscribe(data1 => {
      if(data1.length > 0){
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
    this.service.GetFarmerInv(this.TRANSACTION_ID).subscribe(data2 => {
      console.log(data2);
      if(data2.length > 0){
        this.todateDate = data2[0].SALE_DATE;
        this.TOT_AMT = data2[0].TOT_AMT;
        this.SUB_AMT = data2[0].SUB_AMT;
        this.Prebookedamount = data2[0].prebookedAmount;
        this.totalPaybleamount = ( parseFloat(data2[0].TOT_AMT) - parseFloat(data2[0].prebookedAmount)).toFixed(2)
      }
      
     
    });
  }
  newSale() {
    window.location.reload();
  }
  noramlSale() {
    this.prebookedsale = false;
    this.cropCheack = false;
    this.cropCheackfalse = true;
    this.VarietyCheack = false;
    this.VarietyCheackfalse = true;
    this.selectedEnterNoofBags = '';
    this.selectedCrop = {};
    this.selectedVariety = {};
    this.prebookingcheack = false;
    this.FillCrops();
    this.FillVariety();
    this.selectedIndex = undefined;
    this.selectedIndex1 = undefined;
    this.allFILLDEALERSTOCK = [];
    this.allDatainalist = [];
    this.sumQunitalinQtl = 0;
    this.sumAmount = 0;
    this.showfarmerdetails2 = false;
    this.showfarmerdetails3 = false;


  }
  prebooksale() {
    this.prebookedsale = true;
    this.allFILLDEALERSTOCK = [];
    this.allDatainalist = [];
    this.selectedIndex = undefined;
    this.selectedIndex1 = undefined;
    this.cropCheack = true;
    this.cropCheackfalse = false;
    this.VarietyCheack = true;
    this.VarietyCheackfalse = false;
    this.selectedVariety.CROP_NAME = '';
    this.selectedVariety.VARIETY_NAME = '';
    this.sumQunitalinQtl = 0;
    this.sumAmount = 0;
    this.showfarmerdetails2 = false;
    this.showfarmerdetails3 = false;
  }
}
