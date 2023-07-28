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
  getDistrictLicenceDetails: any = [];
  getDistrictLicenceofPAcsDetails: any = [];
  afterproceed: boolean = false;
  prebookedsale: boolean = false;
  showCheackBox: boolean = false;
  selectedIndex: number | undefined;
  selectedIndex1: number | undefined;

  getAllPreBookingDetails: any = [];
  allFillFinYr: any = [];
  selectedFinancialYear: any = '';
  allFillSeason: any = [];
  selectedSeasons: any = [];
  godownList: any = [];
  cropCategoryList: any = [];
  cropList: any = [];
  varietyList: any = [];
  selectedGodown: any = '';
  selectedCategory: any = '';
  selectedCrop: any = '';
  selectedVariety: any = '';
  SelectedDealer: any = '';
  SelectedDealerOrPacs: any = '';
  SelectedPacs: any = '';
  sGodown: any = ''
  GodownCheack: boolean = false;
  GodownCheackfalse: boolean = true;
  scategory: any = ''
  categoryCheack: boolean = false;
  categoryCheackfalse: boolean = true;
  scrop: any;
  cropCheack: boolean = false;
  cropCheackfalse: boolean = true;
  svariety: any;
  VarietyCheack: boolean = false
  VarietyCheackfalse: boolean = true;
  AvailableStockDetails:any=[];
  lotdetails:boolean=false;
  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

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
    this.prebookedsale = false;
    this.showCheackBox = false;
    this.prebookingreset();
    this.FILL_GODOWN();
  }
  prebooksale() {
    this.prebookedsale = true;
    this.showCheackBox = true;
    this.getAllPreBookingDetails = [];
    if (this.stockSuppliedToDealer == 1) {
      this.SelectedDealerOrPacs = this.SelectedDealer.LIC_NO1
    }
    else if (this.stockSuppliedToPacs == 1) {
      this.SelectedDealerOrPacs = this.SelectedPacs.LIC_NO1
    }
    else {
      this.SelectedDealerOrPacs = '';
    }
    this.service.prebookingDetailsOfDealer(this.SelectedDealerOrPacs).subscribe(data => {
      this.getAllPreBookingDetails = data;
      this.FILL_GODOWN();

    })
  }
  GetDealerLicenceByDistCodeUserType() {
    this.getDistrictLicenceDetails = []
    this.service.GetDealerLicenceByDistCodeUserType().subscribe(data => {
      this.getDistrictLicenceDetails = data;
    })
  }
  GetDealerLicenceByDistCodeUserTypePacs() {
    this.getDistrictLicenceofPAcsDetails = []
    this.service.GetDealerLicenceByDistCodeUserTypePacs().subscribe(data => {
      this.getDistrictLicenceofPAcsDetails = data;
    })
  }
  proceed() {
    this.afterproceed = true;
    this.FILL_GODOWN();
  }
  changeSelection = async (event: any, index: any, value: any) => {
    console.log(value);

    this.selectedIndex = event.target.checked ? index : undefined;
    if (event.target.checked) {


      const FillGodown = await this.FILL_GODOWN();
      this.sGodown = this.godownList[0];
      if (this.sGodown != undefined) {
        const Fillcategory = await this.FILL_CROPCATAGORY(this.sGodown.Godown_ID);
        this.scategory = this.cropCategoryList.find((x: any) => x.Category_Code == value.Category_Code);
        if (this.scategory != undefined) {
          const Fillcrop = await this.FILLCROPNAME(this.scategory.Category_Code, this.sGodown.Godown_ID);
          this.scrop = this.cropList.find((x: any) => x.Crop_Code == value.Crop_Code);
          if (this.scrop != undefined) {
            const FillVariety = await this.FILLCROPVARIETY(this.scrop.Crop_Code, this.scategory.Category_Code, this.sGodown.Godown_ID);
            this.svariety = this.varietyList.find((x: any) => x.Variety_Code == value.Variety_Code);
            console.log(this.svariety);
            if(this.svariety != undefined){
              const Fillavailblestock = await this.fillAvailableStockDetails(this.svariety.Variety_Code,this.scrop.Crop_Code, this.scategory.Category_Code, this.sGodown.Godown_ID);
              this.GodownCheack = true;
              this.GodownCheackfalse = false;
              this.categoryCheack = true;
              this.categoryCheackfalse = false;
              this.cropCheack = true;
              this.cropCheackfalse = false;
              this.VarietyCheack = true;
              this.VarietyCheackfalse = false;
            }

         
          }

        }

      }
      else {
        this.toastr.warning(`This Stock Is not available`);
        this.FILL_GODOWN();
      }






      // const Fillcategory = await this.FILL_CROPCATAGORY(this.sGodown.Godown_ID);
      // this.scategory = this.cropCategoryList.find((x: any) => x.Category_Code == value.Category_Code);
      // console.log(this.scategory);

      // this.scrop = this.cropList.find((x: any) => x.Crop_Code == value.Crop_Code);
      // console.log(this.scrop);

      // if (this.scrop != undefined) {
      //   this.cropCheack = true;
      //   this.cropCheackfalse = false;
      //   this.VarietyCheack = true;
      //   this.VarietyCheackfalse = false;
      //   this.selectedCrop.Crop_Code = value.Crop_Code
      //   this.selectedCrop.Crop_Name = value.Crop_Name
      //   const FillVariety = await this.FILLCROPVARIETY();

      //   this.svariety = this.varietyList.find((x: any) => x.Variety_Code == value.Variety_Code);
      //   if (this.svariety != undefined) {
      //     this.selectedVariety.Variety_Code = value.Variety_Code
      //     this.selectedVariety.Variety_Name = value.Variety_Name;
      //     // this.selectedEnterNoofBags = value.noOfBag;
      //     // this.FILLDEALERSTOCK();
      //     // this.inputfiled = true;
      //     // this.prebookingcheack = true
      //   }
      //   else {
      //     // this.cropList = [];
      //     // this.allFILLDEALERSTOCK = [];
      //     // this.allFillVariety = [];
      //     // this.cropCheack = false;
      //     // this.cropCheackfalse = true;
      //     // this.VarietyCheack = false;
      //     // this.VarietyCheackfalse = true;
      //     // this.FillCrops();
      //     // this.FillVariety();
      //     // this.selectedCrop = {};
      //     // this.selectedVariety = {};
      //     // this.selectedEnterNoofBags = '';
      //     // this.inputfiled = true;
      //     // this.prebookingcheack = false;
      //     this.toastr.warning(`This Stock Is not available`);
      //   }


      // }
      // else {
      //   this.cropList = [];
      //   // this.allFILLDEALERSTOCK = [];
      //   this.varietyList = [];
      //   this.cropCheack = false;
      //   this.cropCheackfalse = true;
      //   this.VarietyCheack = false;
      //   this.VarietyCheackfalse = true;
      //   this.FILLCROPNAME();
      //   this.FILLCROPVARIETY();
      //   // this.selectedEnterNoofBags = '';
      //   this.selectedCrop = {};
      //   this.selectedVariety = {};
      //   // this.inputfiled = true;
      //   // this.prebookingcheack = false;
      //   this.toastr.warning(`This Stock Is not available`);
      // }



      // this.FillVariety()
    }
    else {
      this.prebookingreset();
    }

  }
  prebookingreset(){
    this.GodownCheack = false;
    this.GodownCheackfalse = true;
    this.categoryCheack = false;
    this.categoryCheackfalse = true;
    this.cropCheack = false;
    this.cropCheackfalse = true;
    this.VarietyCheack = false;
    this.VarietyCheackfalse = true;
  }
  FILL_GODOWN() {
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.godownList = [];
        this.godownList = await this.service.FILL_GODOWN(this.prebookedsale).toPromise()
        resolve(this.godownList)
      } catch (e) {
        console.error(e);

        reject()
      }
    })
  }
  FILL_CROPCATAGORY(selectedGodown: any) {    
    console.log('hiii',selectedGodown);
    
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.cropCategoryList = [];
        this.cropCategoryList = await this.service.FILL_CROPCATAGORY(selectedGodown).toPromise()
        resolve(this.cropCategoryList)
        console.log(this.cropCategoryList);
        
      } catch (e) {
        console.error(e);

        reject()
      }
    })


  }
  FILLCROPNAME(Category_Code: any, selectedGodown: any) {
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.cropList = [];
        this.cropList = await this.service.FILLCROPNAME(Category_Code, selectedGodown).toPromise()
        resolve(this.cropList)
      } catch (e) {
        console.error(e);

        reject()
      }
    })

  }
  FILLCROPVARIETY(selectedCrop: any, selectedCategory: any, selectedGodown: any) {
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.varietyList = [];
        this.varietyList = await this.service.FILLCROPVARIETY(selectedCrop, selectedCategory, selectedGodown).toPromise()
        resolve(this.varietyList)
      } catch (e) {
        console.error(e);

        reject()
      }
    })
  }
  fillAvailableStockDetails(selectedVariety:any,selectedCrop: any, selectedCategory: any, selectedGodown: any){
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.AvailableStockDetails = [];
        this.AvailableStockDetails = await this.service.fillAvailableStockDetails(selectedVariety,selectedCrop, selectedCategory, selectedGodown).toPromise()
        resolve(this.AvailableStockDetails);
        this.lotdetails=true;
        console.log(this.AvailableStockDetails);
        
      } catch (e) {
        console.error(e);

        reject()
      }
    })
  }

  changeSelection1 = async (event: any, index: any, value: any) => {

  }
  changequnital(BAG_SIZE_IN_KG: any, enteredNoOfBags: any, i: any, All_in_cost_Price: any) {
    if (enteredNoOfBags != null && enteredNoOfBags != undefined) {

      // if (this.prebookingcheack) {
      //   if (this.selectedEnterNoofBags >= enteredNoOfBags) {
      //     this.allFILLDEALERSTOCK[i].QunitalinQtl = (BAG_SIZE_IN_KG * enteredNoOfBags) / 100;
      //     this.allFILLDEALERSTOCK[i].Amount = (this.allFILLDEALERSTOCK[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
      //     if (enteredNoOfBags == 0) {
      //       this.allFILLDEALERSTOCK[i].QunitalinQtl = 0.00;
      //       this.allFILLDEALERSTOCK[i].Amount = 0.00;
      //     }
      //   }
      //   else {
      //     this.toastr.warning(`Plase enter valid number.`);
      //     this.allFILLDEALERSTOCK[i].enteredNoOfBags = this.selectedEnterNoofBags;
      //     this.allFILLDEALERSTOCK[i].QunitalinQtl = (BAG_SIZE_IN_KG * this.selectedEnterNoofBags) / 100;
      //     this.allFILLDEALERSTOCK[i].Amount = (this.allFILLDEALERSTOCK[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
      //   }
      // }
      // else {
      //   this.allFILLDEALERSTOCK[i].QunitalinQtl = (BAG_SIZE_IN_KG * enteredNoOfBags) / 100;
      //   this.allFILLDEALERSTOCK[i].Amount = (this.allFILLDEALERSTOCK[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
      // }

    }

  }
 addinaList(LOT_NO: any, Receive_Unitname: any, BAG_SIZE_IN_KG: any, enteredNoOfBags: any, QunitalinQtl: any, Amount: any, RECEIVE_UNITCD: any, AVL_QUANTITY: any, All_in_cost_Price: any, i: any, TOT_SUBSIDY: any, AVL_BAGS: any,ischeacked:any) {
    if (enteredNoOfBags != null && enteredNoOfBags != undefined && enteredNoOfBags != '' && enteredNoOfBags != 0 && enteredNoOfBags != '0') {
      console.log(enteredNoOfBags, AVL_BAGS, AVL_BAGS >= enteredNoOfBags, AVL_BAGS <= enteredNoOfBags);
      // if (AVL_BAGS >= enteredNoOfBags) {
      //   let x: any = {}
      //   x.CROP_ID = this.selectedCrop.CROP_CODE;
      //   x.Crop_Name = this.selectedCrop.CROP_NAME;
      //   x.CROP_VERID = this.selectedVariety.VARIETY_CODE;
      //   x.Crop_VerName = this.selectedVariety.VARIETY_NAME;
      //   x.LOT_NO = LOT_NO;
      //   x.Receive_Unitcd = parseInt(RECEIVE_UNITCD)
      //   x.Receive_Unitname = Receive_Unitname;
      //   x.BAG_SIZE_KG = parseInt(BAG_SIZE_IN_KG);
      //   x.NO_OF_BAGS = parseInt(enteredNoOfBags);
      //   x.QUANTITY = QunitalinQtl.toFixed(2);
      //   x.AVL_QUANTITY = AVL_QUANTITY;
      //   x.PRICE_QTL = All_in_cost_Price;
      //   x.SUBSIDY_QTL = TOT_SUBSIDY;
      //   x.Amount = (All_in_cost_Price * QunitalinQtl).toFixed(2);
      //   this.sumQunitalinQtl = 0;
      //   this.sumAmount = 0;
      //   this.allFILLDEALERSTOCK[i].ischeacked = true;
      //   if (!this.allDatainalist.some((j: any) => j.CROP_ID == x.CROP_ID && x.CROP_VERID == j.CROP_VERID)) {

      //     this.allDatainalist.push(x);
      //     this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
      //     this.allFILLDEALERSTOCK[i].Amount = 0;
      //     this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';

      //     this.allDatainalist.forEach((i: any) => {
      //       if (i.hasOwnProperty('QUANTITY')) {
      //         var a = (i.QUANTITY == undefined || i.QUANTITY == null || i.QUANTITY == '') ? 0.00 : i.QUANTITY;
      //         this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
      //       }
      //       if (i.hasOwnProperty('Amount')) {
      //         var b = (i.Amount == undefined || i.Amount == null || i.Amount == '') ? 0.00 : i.Amount;
      //         this.sumAmount = (parseFloat(this.sumAmount) + parseFloat(b)).toFixed(2);
      //       }
      //     })

      //     this.showfarmerdetails2 = true;
      //     this.showfarmerdetails3 = true;
      //     this.selectedIndex1 = undefined;
      //     this.inputfiled = true;
      //   }
      //   else {
      //     let index = this.allDatainalist.findIndex((y: any) => y.CROP_ID === x.CROP_ID && x.CROP_VERID == y.CROP_VERID);
      //     if (AVL_BAGS >= x.NO_OF_BAGS + this.allDatainalist[index].NO_OF_BAGS) {
      //       this.allDatainalist[index].Amount = (parseFloat(x.Amount) + parseFloat(this.allDatainalist[index].Amount)).toFixed(2);
      //       this.allDatainalist[index].NO_OF_BAGS = x.NO_OF_BAGS + this.allDatainalist[index].NO_OF_BAGS;
      //       this.allDatainalist[index].QUANTITY = (parseFloat(x.QUANTITY) + parseFloat(this.allDatainalist[index].QUANTITY)).toFixed(2);
      //       console.log(this.allDatainalist[index].NO_OF_BAGS, AVL_BAGS, 'AVL_BAGS');

      //       this.allDatainalist.forEach((i: any) => {
      //         if (i.hasOwnProperty('QUANTITY')) {
      //           var a = (i.QUANTITY == undefined || i.QUANTITY == null || i.QUANTITY == '') ? 0.00 : i.QUANTITY;
      //           this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
      //         }
      //         if (i.hasOwnProperty('Amount')) {
      //           var b = (i.Amount == undefined || i.Amount == null || i.Amount == '') ? 0.00 : i.Amount;
      //           this.sumAmount = (parseFloat(this.sumAmount) + parseFloat(b)).toFixed(2);
      //         }
      //       })
      //       this.inputfiled = true;
      //       this.selectedIndex1 = undefined;
      //       this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
      //       this.allFILLDEALERSTOCK[i].Amount = 0;
      //       this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';
      //     } else {
      //       this.toastr.warning(`Insufficient stocK.`);
      //       this.inputfiled = true;
      //       this.selectedIndex1 = undefined;
      //       this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
      //       this.allFILLDEALERSTOCK[i].Amount = 0;
      //       this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';
      //     }

      //   }
      // }
      // else {        
      //   this.toastr.warning(`Insufficient stocK.`);
      //   this.inputfiled = true;
      //   this.selectedIndex1 = undefined;
      //   this.allFILLDEALERSTOCK[i].QunitalinQtl = 0;
      //   this.allFILLDEALERSTOCK[i].Amount = 0;
      //   this.allFILLDEALERSTOCK[i].enteredNoOfBags = '';
      //   this.allFILLDEALERSTOCK[i].ischeacked=true;        
      // }



    }
    else {
      this.toastr.warning(`Please Enter Total number of Bags.`);
    }

  }
}
