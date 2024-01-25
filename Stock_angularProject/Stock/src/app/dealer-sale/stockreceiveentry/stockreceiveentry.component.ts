import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { DealerService } from 'src/app/Services/dealer.service';
import { ToastrService } from 'ngx-toastr';
import { DatePipe } from '@angular/common';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: 'app-stockreceiveentry',
  templateUrl: './stockreceiveentry.component.html',
  styleUrls: ['./stockreceiveentry.component.css']
})
export class StockreceiveentryComponent implements OnInit {
  paymentStatusByFarmeIdDetails: any = [];
  FarmerIdPre: any = '';
  loder: boolean = false;
  showdetails: boolean = false;

  selectedDistName: any = {};
  selectedGodown: any = '';
  selectedFinancialYear: any = '';
  selectedSeasons: any = '';
  selectedReceivedFrom: any = '';
  selectedOutsideAgencies: any = '';
  selectedWayBillNumber: any = '';
  selecteddate: any = '';
  selectedChallanNumber: any = '';
  selectedAgencyName: any = '';
  selectedScheme: any = '';
  selectedMouAgencyName: any = '';
  selectedCropCatagory: any = '';
  selectedCrop: any = '';
  selectedClass: any = '';
  CropClass: any = '';
  selectedIndex1: number | undefined;

  getGodownLIst: any = [];
  allFillFinYr: any = [];
  allFillSeason: any = [];
  getAgencyList: any = [];
  getOutSideAgencyList: any = [];
  getDepartmentalFarmAgencyList: any = [];
  getMouFarmAgencyList: any = [];
  getAllCropCategory: any = [];
  getAllCrop: any = [];
  getAllVarietyList: any = [];
  allDatainalist: any = [];

  availableLotNoList: any = [];

  outsideagency: boolean = false;
  departmentalFarm: boolean = false;
  coomonDiv: boolean = false;
  mouAgencyName: boolean = false;
  Scheme: boolean = false;
  proceedButtonClick: boolean = false;
  showBuuton: boolean = true;
  cropData: boolean = false;
  varietyData: boolean = false;
  maxDate: any;

  constructor(
    private service: DealerService,
    private toastr: ToastrService,
    private datePipe: DatePipe,
    private spinner: NgxSpinnerService
  ) {

  }

  ngOnInit(): void {
    this.getSupplyType();
    this.FillGoDownByDistCodeUserType();
    this.FillFinYr();
    this.FillAgencyByOSSC();
    this.maxDate = this.getDate();
  }
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  getSupplyType() {
    this.service.GetDistCodeFromDist().subscribe(data => {
      this.selectedDistName = data;
    })
  }
  FillGoDownByDistCodeUserType() {
    this.getGodownLIst = [];
    this.service.FillGoDownByDistCodeUserType().subscribe(data => {
      this.getGodownLIst = data;
      // this.selectedDistName = data;      
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
    })
  }
  FillAgencyByOSSC() {
    this.getAgencyList = [];
    this.service.FillAgencyByOSSC().subscribe(data => {
      this.getAgencyList = data;
      // this.selectedDistName = data;      
    })
  }
  RecvedFromChanges() {
    this.getOutSideAgencyList = [];
    this.selectedOutsideAgencies = '';
    this.selectedWayBillNumber = '';
    this.selecteddate = '';
    this.selectedChallanNumber = '';
    this.selectedAgencyName = '';
    this.selectedScheme = '';
    this.selectedMouAgencyName = '';
    if (this.selectedReceivedFrom.AgenciesID == '02') {
      this.outsideagency = true;
      this.departmentalFarm = false;
      this.coomonDiv = true;
      this.mouAgencyName = false;
      this.Scheme = false;
      this.service.FillSourceByAgencyIdUserTypeValues(this.selectedReceivedFrom.AgenciesID).subscribe(data => {
        this.getOutSideAgencyList = data;
      })
    }
    else if (this.selectedReceivedFrom.AgenciesID == '05' || this.selectedReceivedFrom.AgenciesID == '06') {
      this.outsideagency = false;
      this.departmentalFarm = true;
      this.coomonDiv = true;
      this.mouAgencyName = false;
      this.Scheme = false;
      this.getDepartmentalFarmAgencyList = [];
      this.service.FillGovtFarmByDistCode(this.selectedReceivedFrom.AgenciesID).subscribe(data => {
        this.getDepartmentalFarmAgencyList = data;
      })
    }
    else if (this.selectedReceivedFrom.AgenciesID == '04') {
      this.outsideagency = false;
      this.departmentalFarm = false;
      this.coomonDiv = true;
      this.mouAgencyName = false;
      this.Scheme = false;
    }

    else if (this.selectedReceivedFrom.AgenciesID == '09') {
      this.outsideagency = false;
      this.departmentalFarm = false;
      this.coomonDiv = true;
      this.mouAgencyName = true;
      this.Scheme = false;
      this.service.FillGovtFarmByDistCode(this.selectedReceivedFrom.AgenciesID).subscribe(data => {
        this.getMouFarmAgencyList = data;
      })
    }
    else if (this.selectedReceivedFrom.AgenciesID == '10') {
      this.outsideagency = false;
      this.departmentalFarm = true;
      this.coomonDiv = true;
      this.mouAgencyName = false;
      this.Scheme = true;
    }
  }
  agencyNameReload() {
    this.selectedAgencyName = '';
    this.getDepartmentalFarmAgencyList = [];
    this.service.agencyNameReload(this.selectedScheme).subscribe(data => {
      this.getDepartmentalFarmAgencyList = data;
    })
  }
  proceed() {
    if (this.selectedDistName && this.selectedFinancialYear && this.selectedSeasons && this.selectedGodown && this.selectedReceivedFrom && this.selecteddate && this.selectedChallanNumber) {
      this.proceedButtonClick = true;
      this.showBuuton = false;
      this.cropData = true;
      this.FillCropCategory();
    }
    else {
      this.toastr.warning(`Please select all field.`);
    }
  }
  cancel() {
    window.location.reload();
  }

  FillCropCategory() {
    this.service.FillCropCategory().subscribe(data => {
      this.getAllCropCategory = data;
    })
  }
  FillCropByCategoryId() {
    this.service.FillCropByCategoryId(this.selectedCropCatagory.Category_Code).subscribe(data => {
      this.getAllCrop = data;
    })
  }
  FillCropVariety() {
    if (this.selectedCropCatagory && this.selectedCrop && this.selectedClass) {
      if (this.selectedClass == "Foundation") {
        this.CropClass = "C01";
      }
      else if (this.selectedClass == "TL") {
        this.CropClass = "C02";
      }
      else if (this.selectedClass == "Certified") {
        this.CropClass = "C03";
      }
      else if (this.selectedClass == "Breeder") {
        this.CropClass = "C04";
      }
      this.getAllVarietyList = [];

      if (this.selectedReceivedFrom.AgenciesID == '02' || this.selectedReceivedFrom.AgenciesID == '04') {
        this.service.FillCropVarietyByOutsideAgencies(this.selectedCrop.Crop_Code).subscribe(data => {
          this.getAllVarietyList = data;
          this.getAllVarietyList.forEach((a: any) => {
            a.ischeacked = true;
          });
        })
      }
      else if (this.selectedReceivedFrom.AgenciesID == '05') {
        this.service.FillCropVarietyByGovtFarm(this.selectedAgencyName.Name_of_agency, this.CropClass, this.selectedCrop.Crop_Code).subscribe(data => {
          this.getAllVarietyList = data;
          this.getAllVarietyList.forEach((a: any) => {
            a.ischeacked = true;
          });
        })
      }
      else if (this.selectedReceivedFrom.AgenciesID == '06') {
        this.service.FillCropVarietyByOUAT(this.selectedAgencyName.Name_of_agency, this.CropClass, this.selectedCrop.Crop_Code).subscribe(data => {
          this.getAllVarietyList = data;
          this.getAllVarietyList.forEach((a: any) => {
            a.ischeacked = true;
          });
        })
      }
      else if (this.selectedReceivedFrom.AgenciesID == '09') {
        this.service.FillCropVarietyByMOUAgency(this.selectedMouAgencyName.Name_of_agency, this.CropClass, this.selectedCrop.Crop_Code).subscribe(data => {
          this.getAllVarietyList = data;
          this.getAllVarietyList.forEach((a: any) => {
            a.ischeacked = true;
          });
        })
      }
      else if (this.selectedReceivedFrom.AgenciesID == '10') {
        this.service.FillCropVarietyByCropIdScheme(this.selectedScheme, this.CropClass, this.selectedCrop.Crop_Code, this.selectedAgencyName.Name_of_agency).subscribe(data => {
          this.getAllVarietyList = data;
          this.getAllVarietyList.forEach((a: any) => {
            a.ischeacked = true;
          });
        })
      }
      this.varietyData = true;
    }


  }
  changeSelection1(event: any, index: any, value: any) {
    this.selectedIndex1 = event.target.checked ? index : undefined;
    let Name_of_agency = '';

    if (this.selectedIndex1 != undefined) {
      this.getAllVarietyList.forEach((x: any) => {
        if (x.Variety_Code == this.getAllVarietyList[index].Variety_Code) {
          x.ischeacked = false;
          // x.enteredNoOfBags = this.selectedEnterNoofBags;
          // this.changequnital(value.Bag_Size_In_kg, value.enteredNoOfBags, index, value.All_in_cost_Price);
          // this.inputfiled = false;

          if (this.selectedReceivedFrom.AgenciesID == '05' || this.selectedReceivedFrom.AgenciesID == '06' || this.selectedReceivedFrom.AgenciesID == '09' || this.selectedReceivedFrom.AgenciesID == '10') {
            if (this.selectedReceivedFrom.AgenciesID == '09') {
              Name_of_agency = this.selectedMouAgencyName.Name_of_agency
            }
            else if (this.selectedReceivedFrom.AgenciesID == '05' || this.selectedReceivedFrom.AgenciesID == '06' || this.selectedReceivedFrom.AgenciesID == '10') {
              Name_of_agency = this.selectedAgencyName.Name_of_agency
            }
            // else if (this.selectedReceivedFrom.AgenciesID == '10') {
            //   Name_of_agency = this.selectedOutsideAgencies.Name_of_agency
            // }
            this.service.FillLotByGovtFarm(Name_of_agency, this.CropClass, x.Variety_Code).subscribe(data => {
              x.getAllLotNoList = data;
            })
          }


        }
        else if (x.Variety_Code != this.getAllVarietyList[index].Variety_Code) {
          x.ischeacked = true;
          x.enteredNoOfBags = '';
          x.QunitalinQtl = 0.00;
          x.Amount = 0.00;
          // this.inputfiled = false;
        }

      });

    }
    else {
      this.getAllVarietyList.forEach((x: any) => {
        x.ischeacked = true;
        x.enteredNoOfBags = '';
        // this.changequnital(value.BAG_SIZE_IN_KG, 0, index, value.All_in_cost_Price);
        // this.inputfiled = true;
      });
    }


  }

  FillRecvBagsByLotByDepartmental(data: any, i: any) {
    console.log(data.enteredLotno.LotNo);
    let lotno = data.enteredLotno.LotNo;
    let Name_of_agency = ''
    if (this.selectedReceivedFrom.AgenciesID == '09') {
      Name_of_agency = this.selectedMouAgencyName.Name_of_agency
    }
    else if (this.selectedReceivedFrom.AgenciesID == '05' || this.selectedReceivedFrom.AgenciesID == '06' || this.selectedReceivedFrom.AgenciesID == '10') {
      Name_of_agency = this.selectedAgencyName.Name_of_agency
    }
    // else if (this.selectedReceivedFrom.AgenciesID == '10') {
    //   Name_of_agency = this.selectedOutsideAgencies.Name_of_agency
    // }
    this.service.fillBagExpiryDate(Name_of_agency, this.CropClass, data.Variety_Code, data.enteredLotno.LotNo, this.selectedReceivedFrom.AgenciesID).subscribe(data => {
      if (data.length > 0) {
        if (data[0].todaydate > data[0].Expire_Date) {
          this.toastr.warning(`This Lot.No Expiry !!!`);
        }
        else {
          this.service.fillBagsFromStockStockDetails(this.selectedGodown.Godown_ID, lotno).subscribe(data1 => {
            this.availableLotNoList = data1;
            if (this.availableLotNoList.length > 0) {
              this.getAllVarietyList[i].enteredOssopcaIssueBags = data[0].Bags;
              this.getAllVarietyList[i].enteredAvlNoOfBags = data[0].Bags - this.availableLotNoList[0].Recv_No_Of_Bags;
            }
            else {
              this.getAllVarietyList[i].enteredOssopcaIssueBags = data[0].Bags;
              this.getAllVarietyList[i].enteredAvlNoOfBags = data[0].Bags;
            }
            this.getAllVarietyList[i].Testing_Date = data[0].Testing_Date;
            this.getAllVarietyList[i].Expire_Date = data[0].Expire_Date;
          })

        }

      }
    })
  }

  changeQuantity(enteredBagsize: any, enteredRecvNoOfBags: any, i: any, enteredAvlNoOfBags: any) {
    if (enteredBagsize && enteredRecvNoOfBags) {
      let y = (enteredBagsize * enteredRecvNoOfBags) / 100;
      if (this.selectedReceivedFrom.AgenciesID == '05' || this.selectedReceivedFrom.AgenciesID == '06' || this.selectedReceivedFrom.AgenciesID == '09' || this.selectedReceivedFrom.AgenciesID == '10') {
        if (enteredAvlNoOfBags >= enteredRecvNoOfBags) {
          this.getAllVarietyList[i].enteredQuqnity = y;
        }
        else {
          this.getAllVarietyList[i].enteredQuqnity = '';
          this.toastr.warning(`Excess no of bags entered !!!`);
        }
      }
      else {
        this.getAllVarietyList[i].enteredQuqnity = y;
      }
    }
    else {
      this.getAllVarietyList[i].enteredQuqnity = '';
    }

  }
  expiryDateCalculate(enteredTestingDate: any, i: any) {
    this.getAllVarietyList[i].enteredExpiryDate = new Date(enteredTestingDate);
    this.getAllVarietyList[i].enteredExpiryDate.setMonth(new Date(enteredTestingDate).getMonth() + 9);
  }
  addinaList(i: any, Variety_Name: any, enteredLotno: any, enteredBagsize: any, enteredTestingDate: any, enteredExpiryDate: any, enteredOssopcaIssueBags: any, enteredAvlNoOfBags: any, enteredRecvNoOfBags: any, enteredQuqnity: any, data: any) {
    let x: any = {};
    console.log(data);
    console.log(enteredLotno);


    if (enteredBagsize && enteredLotno && enteredRecvNoOfBags && enteredQuqnity) {
      x.CATEGORY_ID = this.selectedCropCatagory.Category_Code;
      x.CROP_ID = this.selectedCrop.Crop_Code;
      x.CROP_CLASS = this.selectedClass
      x.Variety_Code = data.Variety_Code;
      x.Variety_Name = Variety_Name;
      x.class = this.selectedClass;
      x.Crop_Name = this.selectedCrop.Crop_Name;
      x.selectedReceivedFrom = this.selectedReceivedFrom.AgenciesID
      if (this.selectedReceivedFrom.AgenciesID == '05' || this.selectedReceivedFrom.AgenciesID == '06' || this.selectedReceivedFrom.AgenciesID == '09' || this.selectedReceivedFrom.AgenciesID == '10') {
        x.enteredLotno = enteredLotno.LotNo;
        x.REF_NO = enteredLotno.Ref_NO;
        x.enteredTestingDate = data.Testing_Date;
        x.enteredExpiryDate = data.Expire_Date;
      }
      else {
        x.enteredLotno = enteredLotno;
        x.enteredTestingDate = enteredTestingDate;
        x.enteredExpiryDate = this.datePipe.transform(new Date(enteredExpiryDate), 'yyyy-MM-dd');

        console.log(this.datePipe.transform(new Date(enteredExpiryDate), 'yyyy-MM-dd'), 'this.datePipe.transform(x.enteredExpiryDate)');

      }
      x.enteredBagsize = enteredBagsize;
      x.enteredRecvNoOfBags = enteredRecvNoOfBags;
      x.enteredQuqnity = enteredQuqnity;

      x.enteredOssopcaIssueBags = enteredOssopcaIssueBags;
      x.enteredAvlNoOfBags = enteredAvlNoOfBags;
      console.log(this.selectedReceivedFrom.AgenciesID);

      if (!this.allDatainalist.some((j: any) => j.enteredLotno == x.enteredLotno)) {
        console.log('55555');

        this.allDatainalist.push(x);
      }
      else {
        this.toastr.warning(`This Lot No already added in a list.`);
      }

    }
    else {
      this.toastr.warning(`Please select all field.`);
    }

  }

  removeinaList(x: any) {
    this.allDatainalist.forEach((item: any, index: any) => {
      if (item === x) {
        this.allDatainalist.splice(index, 1);
        // this.sumQunitalinQtl = (this.sumQunitalinQtl - x.QUANTITY).toFixed(2);
        // this.sumTotalNoOfBags = this.sumTotalNoOfBags - x.NO_OF_BAGS;
        // this.sumAllincostPrice = this.sumAllincostPrice - x.totalAmount;

      }
    });
    this.getAllVarietyList.forEach((items: any, index: any) => {
      if (items.Lot_No == x.LOT_NO && items.Crop_Verid == x.CROP_VERID && items.Godown_ID == x.Godown_ID) {
        // items.RECV_NO_OF_BAGS += x.NO_OF_BAGS;
        // items.Avl_Quantity = parseFloat(items.Avl_Quantity) + parseFloat(x.QUANTITY);
      }
    });
    this.toastr.warning(`Remove in a list.`);
  }
  btn_Submit_Click() {

    return new Promise(async (resolve: any, reject: any) => {
      try {
        if (this.allDatainalist.length > 0) {
          this.spinner.show();
          let objUserBEL: any = {};
          objUserBEL.DistCode = this.selectedDistName.dist_code;
          objUserBEL.Godown_ID = this.selectedGodown.Godown_ID;
          objUserBEL.AgenciesID = this.selectedReceivedFrom.AgenciesID;
          if (this.selectedReceivedFrom.AgenciesID == '05') {
            objUserBEL.Receive_Unitcd = "0121";
            objUserBEL.MOU_REFNO = this.selectedAgencyName.Name_of_agency;
            objUserBEL.AgName = this.selectedAgencyName.AgName
          }
          else if (this.selectedReceivedFrom.AgenciesID == '06') {
            objUserBEL.Receive_Unitcd = "0122";
            objUserBEL.MOU_REFNO = this.selectedAgencyName.Name_of_agency;
            objUserBEL.AgName = this.selectedAgencyName.AgName
          }
          else if (this.selectedReceivedFrom.AgenciesID == '09') {
            objUserBEL.Receive_Unitcd = "0124";
            objUserBEL.MOU_REFNO = this.selectedMouAgencyName.Name_of_agency;
            objUserBEL.AgName = this.selectedMouAgencyName.AgName
          }
          else if (this.selectedReceivedFrom.AgenciesID == '10') {
            if (this.selectedScheme == "BGREI") {
              objUserBEL.Receive_Unitcd = "0125";
            }
            else if (this.selectedScheme == "ICRISAT") {
              objUserBEL.Receive_Unitcd = "0126";
            }
            else if (this.selectedScheme == "NFSM") {
              objUserBEL.Receive_Unitcd = "0127";
            }
            else if (this.selectedScheme == "NMOOP-DAandFP") {
              objUserBEL.Receive_Unitcd = "0128";
            }
            else if (this.selectedScheme == "NSC") {
              objUserBEL.Receive_Unitcd = "0129";
            }
            else if (this.selectedScheme == "RKVY") {
              objUserBEL.Receive_Unitcd = "0130";
            }
            objUserBEL.MOU_REFNO = this.selectedAgencyName.Name_of_agency;
          }
          else {
            objUserBEL.Receive_Unitcd = this.selectedReceivedFrom.AgenciesID;
          }
          objUserBEL.Outagency_Waybillnum_Farm = this.selectedWayBillNumber;
          objUserBEL.Challan_No = this.selectedChallanNumber;
          objUserBEL.mReceive_Date = this.selecteddate;
          objUserBEL.SESSION = this.selectedSeasons.SHORT_NAME;
          objUserBEL.FIN_YR = this.selectedFinancialYear;
          objUserBEL.SourceType = this.selectedReceivedFrom.AgenciesName;

          objUserBEL.XML_Value = this.allDatainalist;
          console.log(objUserBEL);
          const result = await this.service.Stock_Sp_InsReceiveDetails(objUserBEL).toPromise()
          // resolve(this.varietyList)
          console.log(result);

          if (result == true) {
            this.toastr.success(`Receive Entry Successfully Completed !!!`);
            this.spinner.hide();
            setTimeout(() => {
              window.location.reload();
            }, 2000);
          }
        }
      } catch (e) {
        this.spinner.hide();
        console.error(e);

        reject()
      }
    })

  }
}
