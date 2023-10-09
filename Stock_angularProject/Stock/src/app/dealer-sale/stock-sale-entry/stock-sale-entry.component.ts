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
  AvailableStockDetails: any = [];
  lotdetails: boolean = false;
  selectedEnterNoofBags: any;
  inputfiled: boolean = true;
  sumQunitalinQtl: any = 0;
  allDatainalist: any = [];
  SelectedDate: any = '';
  maxDate: any;
  getAllSupplyType: any = [];
  supllytype: any = '';
  SelectedCollectNo: any = '';
  SelectedDDOrUTRNo: any = '';
  SelectedAmount: any = '';
  sumTotalNoOfBags: any = '';
  prebookingApplicationId: any = '';
  sumAllincostPrice: any = '';
  PACSRebate: any = '';
  proceedButton: boolean = true;
  disableValues = false;
  viewpage: boolean = true;
  printpage: boolean = false;
  cashmemeodetails: any = [];
  appfirmname: any = '';
  SALE_TO: any = '';
  SALE_DATE: any = '';
  CASH_MEMO_NO: any = '';
  DD_NUMBER: any = '';
  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    // this.Dealer();
    this.FillFinYr();
    this.getSupplyType();
    this.maxDate = this.getDate();
    this.SelectedCollectNo = 'DU'
  }
  getSupplyType() {
    this.getAllSupplyType = []
    this.service.getSupplyType().subscribe(data => {
      this.getAllSupplyType = data;
      this.Dealer(this.getAllSupplyType[0]);
    })
  }
  FillFinYr() {
    this.allFillFinYr = []
    this.service.FillFinYr().subscribe(data => {
      this.allFillFinYr = data;
      this.selectedFinancialYear = this.allFillFinYr[0].FIN_YR;
      this.FillSeason();
    })
  }
  FillSeason() {
    this.allFillSeason = []
    this.service.FillSeason(this.selectedFinancialYear).subscribe(data => {
      this.allFillSeason = data;
      this.selectedSeasons = this.allFillSeason[0];
      // this.FillCrops();
      // this.getStockReceivedData();
      // this.getPreBookingDetails();
    })
  }
  Dealer(x: any) {
    this.supllytype = x;
    this.spinner.show();
    if (x.SUPPLY_ID == 6) {
      this.stockSuppliedToDealer = 1;
      this.stockSuppliedToPacs = 0;
      this.stockSuppliedToOaic = 0;
      this.stockSuppliedToDemo = 0;
      this.GetDealerLicenceByDistCodeUserType();
    }
    else if (x.SUPPLY_ID == 9) {
      this.stockSuppliedToDealer = 0;
      this.stockSuppliedToPacs = 1;
      this.stockSuppliedToOaic = 0;
      this.stockSuppliedToDemo = 0;
      this.GetDealerLicenceByDistCodeUserTypePacs();
    }
    else if (x.SUPPLY_ID == 10) {
      this.stockSuppliedToDealer = 0;
      this.stockSuppliedToPacs = 0;
      this.stockSuppliedToOaic = 1;
      this.stockSuppliedToDemo = 0;
    }
    else if (x.SUPPLY_ID == 13) {
      this.stockSuppliedToDealer = 0;
      this.stockSuppliedToPacs = 0;
      this.stockSuppliedToOaic = 0;
      this.stockSuppliedToDemo = 1;
    }
    this.spinner.hide();
  }
  noramlSale() {
    this.prebookedsale = false;
    this.showCheackBox = false;
    this.prebookingreset();
    this.FILL_GODOWN();
    if (this.stockSuppliedToDealer == 1) {
      this.SelectedDealerOrPacs = this.SelectedDealer;
      this.proceed();
    }
    else if (this.stockSuppliedToPacs == 1) {
      this.SelectedDealerOrPacs = this.SelectedPacs;
      this.proceed();
    }
    else {
      this.SelectedDealerOrPacs = '';
    }
  }
  prebooksale() {
    this.prebookedsale = true;
    this.showCheackBox = true;
    this.getAllPreBookingDetails = [];
    if (this.stockSuppliedToDealer == 1) {
      this.SelectedDealerOrPacs = this.SelectedDealer
    }
    else if (this.stockSuppliedToPacs == 1) {
      this.SelectedDealerOrPacs = this.SelectedPacs
    }
    else {
      this.SelectedDealerOrPacs = '';
    }

    this.service.prebookingDetailsOfDealer(this.SelectedDealerOrPacs.LIC_NO1).subscribe(data => {
      this.getAllPreBookingDetails = data;
      this.FILL_GODOWN();

    })
  }
  GetDealerLicenceByDistCodeUserType() {
    this.PACSRebate = '';
    this.getDistrictLicenceDetails = []
    this.service.GetDealerLicenceByDistCodeUserType().subscribe(data => {
      this.getDistrictLicenceDetails = data;
    })
  }
  GetDealerLicenceByDistCodeUserTypePacs() {
    this.PACSRebate = '';
    this.getDistrictLicenceofPAcsDetails = []
    this.service.GetDealerLicenceByDistCodeUserTypePacs().subscribe(data => {
      this.getDistrictLicenceofPAcsDetails = data;
    })
  }
  proceed() {
    if (this.SelectedDealerOrPacs !== null && this.SelectedDealerOrPacs !== '' && this.SelectedDealerOrPacs !== undefined
      && this.SelectedCollectNo !== null && this.SelectedCollectNo !== '' && this.SelectedCollectNo !== undefined
      && this.SelectedDDOrUTRNo !== null && this.SelectedDDOrUTRNo !== '' && this.SelectedDDOrUTRNo !== undefined
      && this.SelectedAmount !== null && this.SelectedAmount !== '' && this.SelectedAmount !== undefined
      && this.SelectedDate !== null && this.SelectedDate !== '' && this.SelectedDate !== undefined) {
      console.log(this.stockSuppliedToPacs);

      if (this.stockSuppliedToPacs === 1) {
        if (this.PACSRebate != null && this.PACSRebate != undefined && this.PACSRebate != '') {
          this.afterproceed = true;
          this.FILL_GODOWN();
          this.proceedButton = false;
          this.disableValues = true;
        }
        else {
          this.toastr.warning('Please select all field.');
        }
      }
      else {
        this.afterproceed = true;
        this.FILL_GODOWN();
        this.proceedButton = false;
        this.disableValues = true;
      }

    }
    else {
      this.toastr.warning('Please select all field.');
    }

  }
  changeSelection = async (event: any, index: any, value: any) => {
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
            if (this.svariety != undefined) {
              const Fillavailblestock = await this.fillAvailableStockDetails(this.svariety.Variety_Code, this.scrop.Crop_Code, this.scategory.Category_Code, this.sGodown.Godown_ID);
              this.GodownCheack = true;
              this.GodownCheackfalse = false;
              this.categoryCheack = true;
              this.categoryCheackfalse = false;
              this.cropCheack = true;
              this.cropCheackfalse = false;
              this.VarietyCheack = true;
              this.VarietyCheackfalse = false;
              this.selectedEnterNoofBags = value.noOfBag;
              this.prebookingApplicationId = value.applicationID;
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

      // this.scrop = this.cropList.find((x: any) => x.Crop_Code == value.Crop_Code);

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
      //     // this.AvailableStockDetails = [];
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
      //   // this.AvailableStockDetails = [];
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
  prebookingreset() {
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
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.cropCategoryList = [];
        this.cropCategoryList = await this.service.FILL_CROPCATAGORY(selectedGodown).toPromise()
        resolve(this.cropCategoryList)
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
  fillAvailableStockDetails(selectedVariety: any, selectedCrop: any, selectedCategory: any, selectedGodown: any) {
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.AvailableStockDetails = [];
        this.AvailableStockDetails = await this.service.fillAvailableStockDetails(selectedVariety, selectedCrop, selectedCategory, selectedGodown).toPromise()
        this.AvailableStockDetails.forEach((a: any) => {
          a.ischeacked = true;
          a.QunitalinQtl = 0.00;
        });
        resolve(this.AvailableStockDetails);
        this.lotdetails = true;
      } catch (e) {
        console.error(e);

        reject()
      }
    })
  }
  changeSelection1(event: any, index: any, value: any) {
    this.selectedIndex1 = event.target.checked ? index : undefined;
    if (this.selectedIndex1 != undefined) {
      this.AvailableStockDetails.forEach((x: any) => {
        if (x.Lot_No == this.AvailableStockDetails[index].Lot_No) {
          x.ischeacked = false;
          x.enteredNoOfBags = this.selectedEnterNoofBags;
          this.changequnital(value.Bag_Size_In_kg, value.enteredNoOfBags, index, value.All_in_cost_Price);
          this.inputfiled = false;
        }
        else if (x.Lot_No != this.AvailableStockDetails[index].Lot_No) {
          x.ischeacked = true;
          x.enteredNoOfBags = '';
          x.QunitalinQtl = 0.00;
          x.Amount = 0.00;
          this.inputfiled = false;
        }

      });

    }
    else {
      this.AvailableStockDetails.forEach((x: any) => {
        x.ischeacked = true;
        x.enteredNoOfBags = '';
        this.changequnital(value.BAG_SIZE_IN_KG, 0, index, value.All_in_cost_Price);
        this.inputfiled = true;
      });
    }


  }
  changequnital(Bag_Size_In_kg: any, enteredNoOfBags: any, i: any, All_in_cost_Price: any) {
    if (enteredNoOfBags != null && enteredNoOfBags != undefined) {

      if (this.prebookedsale) {
        if (parseInt(this.selectedEnterNoofBags) >= parseInt(enteredNoOfBags)) {
          this.AvailableStockDetails[i].QunitalinQtl = (Bag_Size_In_kg * enteredNoOfBags) / 100;
          this.AvailableStockDetails[i].Amount = (this.AvailableStockDetails[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
          if (enteredNoOfBags == 0) {
            this.AvailableStockDetails[i].QunitalinQtl = 0.00;
            this.AvailableStockDetails[i].Amount = 0.00;
          }
        }
        else {
          this.toastr.warning(`Plase enter valid number.`);
          this.AvailableStockDetails[i].enteredNoOfBags = this.selectedEnterNoofBags;
          this.AvailableStockDetails[i].QunitalinQtl = (Bag_Size_In_kg * this.selectedEnterNoofBags) / 100;
          this.AvailableStockDetails[i].Amount = (this.AvailableStockDetails[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
        }
      }
      else {
        this.AvailableStockDetails[i].QunitalinQtl = (Bag_Size_In_kg * enteredNoOfBags) / 100;
        this.AvailableStockDetails[i].Amount = (this.AvailableStockDetails[i].QunitalinQtl * All_in_cost_Price).toFixed(2);
      }

    }

  }
  addinaList(i: any, Lot_No: any, Receive_Unitname: any, Bag_Size_In_kg: any, enteredNoOfBags: any, QunitalinQtl: any, Avl_Quantity: any, RECV_NO_OF_BAGS: any, ischeacked: any, All_in_cost_Price: any, Class: any, totalAmount: any, Receive_Unitcd: any) {
    if (enteredNoOfBags != null && enteredNoOfBags != undefined && enteredNoOfBags != '' && enteredNoOfBags != 0 && enteredNoOfBags != '0') {
      if (RECV_NO_OF_BAGS >= enteredNoOfBags) {
        let x: any = {}
        if (this.prebookedsale) {
          x.Godown_ID = this.sGodown.Godown_ID;
          x.Godown_Name = this.sGodown.Godown_Name;
          x.CATEGORY_ID = this.scategory.Category_Code;
          x.CROP_ID = this.scrop.Crop_Code;
          x.Crop_Name = this.scrop.Crop_Name;
          x.CROP_VERID = this.svariety.Variety_Code;
          x.Crop_VerName = this.svariety.Variety_Name;
          x.prebookingNoofBags = this.selectedEnterNoofBags
        } else {
          x.Godown_ID = this.selectedGodown.Godown_ID;
          x.Godown_Name = this.selectedGodown.Godown_Name;
          x.CATEGORY_ID = this.selectedCategory.Category_Code;
          x.CROP_ID = this.selectedCrop.Crop_Code;
          x.Crop_Name = this.selectedCrop.Crop_Name;
          x.CROP_VERID = this.selectedVariety.Variety_Code;
          x.Crop_VerName = this.selectedVariety.Variety_Name;
          x.prebookingNoofBags = 0;
        }
        x.LOT_NO = Lot_No;
        x.Receive_Unitname = Receive_Unitname;
        x.BAG_SIZE_KG = parseInt(Bag_Size_In_kg);
        x.NO_OF_BAGS = parseInt(enteredNoOfBags);
        x.QUANTITY = QunitalinQtl.toFixed(2);
        x.AVL_QUANTITY = Avl_Quantity;
        x.All_in_cost_Price = All_in_cost_Price;
        x.Class = Class;
        x.totalAmount = totalAmount;
        x.Receive_Unitcd = Receive_Unitcd;

        x.totalAmount = (x.QUANTITY * All_in_cost_Price).toFixed(2);
        this.sumQunitalinQtl = 0;
        this.sumTotalNoOfBags = 0;
        this.sumAllincostPrice = 0;

        this.AvailableStockDetails[i].ischeacked = true;
        if (!this.allDatainalist.some((j: any) => j.CROP_ID == x.CROP_ID && x.CROP_VERID == j.CROP_VERID && x.LOT_NO == j.LOT_NO)) {
          this.allDatainalist.push(x);
          console.log(this.allDatainalist);

          this.allDatainalist.forEach((y: any, index: any) => {
            if (y.hasOwnProperty('QUANTITY')) {
              var a = (y.QUANTITY == undefined || y.QUANTITY == null || y.QUANTITY == '') ? 0.00 : y.QUANTITY;
              var b = (y.NO_OF_BAGS == undefined || y.NO_OF_BAGS == null || y.NO_OF_BAGS == '') ? 0 : y.NO_OF_BAGS;
              var c = (y.totalAmount == undefined || y.totalAmount == null || y.totalAmount == '') ? 0 : y.totalAmount;

              if (this.prebookedsale) {
                if (x.prebookingNoofBags >= this.sumTotalNoOfBags + b) {
                  this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
                  this.sumTotalNoOfBags = this.sumTotalNoOfBags + b;
                  this.sumAllincostPrice = (parseFloat(this.sumAllincostPrice) + parseFloat(c)).toFixed(2);


                  if (index + 1 == this.allDatainalist.length) {
                    this.toastr.success(`Stock Added Sucessfully.`);
                    this.AvailableStockDetails[i].Avl_Quantity = (this.AvailableStockDetails[i].Avl_Quantity - QunitalinQtl).toFixed(2);
                    this.AvailableStockDetails[i].RECV_NO_OF_BAGS = this.AvailableStockDetails[i].RECV_NO_OF_BAGS - parseInt(enteredNoOfBags)
                  }
                }
                else {
                  this.allDatainalist.splice(index, 1);
                  this.toastr.warning(`Excess No of Bags Added.`);
                }
              }
              else {
                this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
                this.sumTotalNoOfBags = this.sumTotalNoOfBags + b;
                this.sumAllincostPrice = (parseFloat(this.sumAllincostPrice) + parseFloat(c)).toFixed(2);
                if (index + 1 == this.allDatainalist.length) {
                  this.AvailableStockDetails[i].Avl_Quantity = (this.AvailableStockDetails[i].Avl_Quantity - QunitalinQtl).toFixed(2);
                  this.AvailableStockDetails[i].RECV_NO_OF_BAGS = this.AvailableStockDetails[i].RECV_NO_OF_BAGS - parseInt(enteredNoOfBags);
                  this.toastr.success(`Stock Added Sucessfully.`);
                }
              }

            }
          })
          this.AvailableStockDetails[i].QunitalinQtl = 0;
          this.AvailableStockDetails[i].Amount = 0;
          this.AvailableStockDetails[i].enteredNoOfBags = '';
          this.selectedIndex1 = undefined;
          this.inputfiled = true;

        }
        else {
          let index = this.allDatainalist.findIndex((y: any) => y.CROP_ID === x.CROP_ID && x.CROP_VERID == y.CROP_VERID && x.LOT_NO == y.LOT_NO);
          if (RECV_NO_OF_BAGS >= x.NO_OF_BAGS) {
            this.allDatainalist[index].Amount = (parseFloat(x.Amount) + parseFloat(this.allDatainalist[index].Amount)).toFixed(2);
            this.allDatainalist[index].NO_OF_BAGS = x.NO_OF_BAGS + this.allDatainalist[index].NO_OF_BAGS;
            this.allDatainalist[index].QUANTITY = (parseFloat(x.QUANTITY) + parseFloat(this.allDatainalist[index].QUANTITY)).toFixed(2);
            this.allDatainalist.forEach((y: any) => {
              if (y.hasOwnProperty('QUANTITY')) {
                var a = (y.QUANTITY == undefined || y.QUANTITY == null || y.QUANTITY == '') ? 0.00 : y.QUANTITY;
                var b = (y.NO_OF_BAGS == undefined || y.NO_OF_BAGS == null || y.NO_OF_BAGS == '') ? 0 : y.NO_OF_BAGS;
                var c = (y.totalAmount == undefined || y.totalAmount == null || y.totalAmount == '') ? 0 : y.totalAmount;

                if (this.prebookedsale) {

                  if (x.prebookingNoofBags >= this.sumTotalNoOfBags + b) {
                    this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
                    this.sumTotalNoOfBags = this.sumTotalNoOfBags + b;
                    this.sumAllincostPrice = (parseFloat(this.sumAllincostPrice) + parseFloat(c)).toFixed(2);

                    if (index + 1 == this.allDatainalist.length) {
                      this.toastr.success(`Stock Added Sucessfully.`);
                      this.AvailableStockDetails[i].Avl_Quantity = (this.AvailableStockDetails[i].Avl_Quantity - QunitalinQtl).toFixed(2);
                      this.AvailableStockDetails[i].RECV_NO_OF_BAGS = this.AvailableStockDetails[i].RECV_NO_OF_BAGS - parseInt(enteredNoOfBags)
                    }
                  }
                  else {
                    this.allDatainalist[index].Amount = (parseFloat(this.allDatainalist[index].Amount) - parseFloat(x.Amount)).toFixed(2);
                    this.allDatainalist[index].NO_OF_BAGS = this.allDatainalist[index].NO_OF_BAGS - x.NO_OF_BAGS;
                    this.allDatainalist[index].QUANTITY = (parseFloat(this.allDatainalist[index].QUANTITY) - parseFloat(x.QUANTITY)).toFixed(2);
                    // this.allDatainalist.splice(index, 1);
                    this.toastr.warning(`Excess No of Bags Added.`);
                  }


                } else {
                  this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
                  this.sumTotalNoOfBags = this.sumTotalNoOfBags + b;
                  this.sumAllincostPrice = (parseFloat(this.sumAllincostPrice) + parseFloat(c)).toFixed(2);
                  if (index + 1 == this.allDatainalist.length) {
                    this.AvailableStockDetails[i].RECV_NO_OF_BAGS = this.AvailableStockDetails[i].RECV_NO_OF_BAGS - parseInt(enteredNoOfBags);
                    this.AvailableStockDetails[i].Avl_Quantity = (this.AvailableStockDetails[i].Avl_Quantity - QunitalinQtl).toFixed(2);
                    this.toastr.success(`Stock Added Sucessfully.`);
                  }
                }
              }
            })
            this.inputfiled = true;
            this.selectedIndex1 = undefined;
            this.AvailableStockDetails[i].QunitalinQtl = 0;
            this.AvailableStockDetails[i].Amount = 0;
            this.AvailableStockDetails[i].enteredNoOfBags = '';
            // this.AvailableStockDetails[i].RECV_NO_OF_BAGS = this.AvailableStockDetails[i].RECV_NO_OF_BAGS - parseInt(enteredNoOfBags);
            // this.AvailableStockDetails[i].Avl_Quantity = (this.AvailableStockDetails[i].Avl_Quantity - QunitalinQtl).toFixed(2);
            // this.toastr.success(`Stock Added Sucessfully2.`);

          } else {
            this.toastr.warning(`Insufficient stocK.`);
            this.inputfiled = true;
            this.selectedIndex1 = undefined;
            this.AvailableStockDetails[i].QunitalinQtl = 0;
            this.AvailableStockDetails[i].Amount = 0;
            this.AvailableStockDetails[i].enteredNoOfBags = '';
          }

        }
      }
      else {
        this.toastr.warning(`Insufficient stocK.`);
        this.inputfiled = true;
        this.selectedIndex1 = undefined;
        this.AvailableStockDetails[i].QunitalinQtl = 0;
        this.AvailableStockDetails[i].Amount = 0;
        this.AvailableStockDetails[i].enteredNoOfBags = '';
        this.AvailableStockDetails[i].ischeacked = true;
      }
    }
    else {
      this.toastr.warning(`Please Enter Total number of Bags.`);
    }

  }
  removeinaList(x: any) {
    this.allDatainalist.forEach((item: any, index: any) => {
      if (item === x) {
        this.allDatainalist.splice(index, 1);
        this.sumQunitalinQtl = (this.sumQunitalinQtl - x.QUANTITY).toFixed(2);
        this.sumTotalNoOfBags = this.sumTotalNoOfBags - x.NO_OF_BAGS;
        this.sumAllincostPrice = this.sumAllincostPrice - x.totalAmount;

      }
    });
    this.AvailableStockDetails.forEach((items: any, index: any) => {
      if (items.Lot_No == x.LOT_NO && items.Crop_Verid == x.CROP_VERID && items.Godown_ID == x.Godown_ID) {
        items.RECV_NO_OF_BAGS += x.NO_OF_BAGS;
        items.Avl_Quantity = parseFloat(items.Avl_Quantity) + parseFloat(x.QUANTITY);
      }
    });
    this.toastr.warning(`Remove in a list.`);
  }
  getGodownNames(): string {
    return this.allDatainalist.map((godown: any) => godown.Godown_Name).join(',');
  }
  minDate: any = "";
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  confirm() {
    return new Promise(async (resolve: any, reject: any) => {
      console.log(this.selectedGodown);
      console.log(this.sGodown, this.SelectedDealerOrPacs);


      try {
        this.spinner.show();
        const data = {
          MOU_REFNO: '',
          IS_PACS: this.stockSuppliedToPacs == 1 ? 'true' : 'false',
          SUPPLY_TYPE: this.supllytype.SUPPLY_ID,
          CREDIT_BILL_NO: '',
          mDATE: '',
          DEPT_TYPE: '',
          GODOWN_ID: this.sGodown.Godown_ID || this.selectedGodown.Godown_ID,
          SALE_DATE: this.SelectedDate,
          SALE_TO: this.SelectedDealerOrPacs.LIC_NO,
          APP_FIRMNAME: this.SelectedDealerOrPacs.APP_FIRMNAME,
          DD_NUMBER: this.SelectedCollectNo + "/" + this.SelectedDDOrUTRNo,
          AMOUNT: this.SelectedAmount,
          CONFIRM_STATUS: 'Y',
          SEASSION: this.selectedSeasons.SHORT_NAME,
          FIN_YR: this.selectedFinancialYear,
          PACSRebate: this.PACSRebate,
          applicationId: this.prebookedsale == true ? this.prebookingApplicationId : '',
          VALUES: this.allDatainalist,
          PrebookingorNot: this.prebookedsale,
          TotalNoOfBags: this.sumTotalNoOfBags,
          TotalNoOfQuantity: this.sumQunitalinQtl

        };
        console.log(data);

        const result = await this.service.fillDealerSaleDeatils(data).toPromise()
        // resolve(this.varietyList)
        console.log(result);
        if (result.result == 'True') {
          this.toastr.success(`Sucessfully Transfered and Cashmemo no is ${result.CASH_MEMO_NO}`);
          this.spinner.hide();

          this.PrintReport(result.CASH_MEMO_NO);
          this.printpage = true;
          this.viewpage = false;
          // this.router.navigate(['dealer/cashmemodetails'], { queryParams: { applicationid: result.CASH_MEMO_NO} });
          // window.open(`http://localhost:4300/#/dealer/cashmemodetails?applicationid=` + result.CASH_MEMO_NO);
        }

      } catch (e) {
        this.spinner.hide();
        console.error(e);

        reject()
      }
    })
  }





  PrintReport(CASH_MEMO_NO: any) {
    this.spinner.show();
    return new Promise(async (resolve: any, reject: any) => {
      try {
        this.cashmemeodetails = [];
        this.cashmemeodetails = await this.service.cashmemodetails(CASH_MEMO_NO).toPromise()
        console.log(this.cashmemeodetails);

        if (this.cashmemeodetails.length > 0) {
          if (this.cashmemeodetails[0].SUPPLY_TYPE == '1' || this.cashmemeodetails[0].SUPPLY_TYPE == '6' || this.cashmemeodetails[0].SUPPLY_TYPE == '9') {
            this.appfirmname = this.cashmemeodetails[0].APP_FIRMNAME;
            this.SALE_TO = this.cashmemeodetails[0].SALE_TO;

          }
          else if (this.cashmemeodetails[0].SUPPLY_TYPE == '3' || this.cashmemeodetails[0].SUPPLY_TYPE == '8') {
            let appfirmname = await this.service.getGodownmaster(this.cashmemeodetails[0].SALE_TO).toPromise();
            this.appfirmname = appfirmname[0].Godown_Name;
          }
          this.SALE_DATE = this.cashmemeodetails[0].SALE_DATE;
          this.CASH_MEMO_NO = this.cashmemeodetails[0].CASH_MEMO_NO;
          this.DD_NUMBER = this.cashmemeodetails[0].DD_NUMBER;
          this.cashmemeodetails.forEach((y: any) => {

            if (y.hasOwnProperty('Quantity')) {
              var a = (y.Quantity == undefined || y.Quantity == null || y.Quantity == '') ? 0.00 : y.Quantity;
              var b = (y.SALE_NO_OF_BAG == undefined || y.SALE_NO_OF_BAG == null || y.SALE_NO_OF_BAG == '') ? 0 : y.SALE_NO_OF_BAG;
              var c = (y.AMOUNT == undefined || y.AMOUNT == null || y.AMOUNT == '') ? 0 : y.AMOUNT;

              console.log(b, typeof (b), this.sumTotalNoOfBags);

              this.sumQunitalinQtl = (parseFloat(this.sumQunitalinQtl) + parseFloat(a)).toFixed(2);
              this.sumTotalNoOfBags = parseInt(this.sumTotalNoOfBags) + parseInt(b);
              this.sumAllincostPrice = (parseFloat(this.sumAllincostPrice) + parseFloat(c)).toFixed(2);


            }
          })
          this.spinner.hide();
          resolve(this.cashmemeodetails);
        }

        // this.AvailableStockDetails.forEach((a: any) => {
        //   a.ischeacked = true;
        //   a.QunitalinQtl = 0.00;
        // });

      } catch (e) {
        console.error(e);

        reject()
      }
    })
  }
  newSale(){
    window.location.reload();
  }
}
