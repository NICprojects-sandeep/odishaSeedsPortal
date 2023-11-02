import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from '../dashboard.service';
import { from, Observable } from 'rxjs';
@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css']
})
export class ContainerComponent {
  getSeason: any = [];
  crops: any = [];
  varieties: any = [];
  districts: any = [];
  distWiseData: any = [];
  quantityArray: any = [];
  quantity: any;
  showTable: boolean = false;
  backButton:boolean=false;
  saleData: any = [];
  pendingData: any = [];
  PacsLamps: number = 0;
  DealerCount: number = 0;
  year: any = '';
  season: any = '';
  crop: any = '';
  invoiceItems2: any = [];
  invoiceItems1: any = [];
  resultArray: any = [];
  getItemName: any = [];
  dealerwisedata: any = [];
  invoiceItems: any = [];
  sisSeason: any = '';
  sumByVarietyCode: any = {}
  sumByVarietyCode1: any = {}
  constructor(private router: Router,
    private service: DashboardService,
  ) { }
  ngOnInit(): void {
    this.loadSeason();
    this.loadDistricts();
    this.getPacsLamps();
    this.getDealerCount();
  };

  loadSeason() {
    this.service.loadSeason(this.year).subscribe(async result => {
      this.getSeason = result.cropSeason;
    }, err => console.log(err));
  }
  loadAllCrop() {
    this.service.loadAllCrop(this.year, this.season).subscribe(async result => {
      this.crops = result;
      console.log(this.crops);

    }, err => console.log(err));
  }

  loadVariety() {
    console.log(this.crop, this.year, this.season);

    this.service.loadVariety(this.crop, this.year, this.season).subscribe(async result => {
      this.varieties = result;
      this.districtWiseData();
    }, err => console.log(err));
  }
  loadDistricts() {
    this.service.loadDistricts().subscribe(async result => {
      this.districts = result;
    }, err => console.log(err));
  }
  districtWiseData() {
    this.service.districtWiseData(this.crop, this.year, this.season).subscribe(async result => {
      this.distWiseData = result.result;
      this.loadSaleData();
    }, err => console.log(err));
  }
  loadSaleData() {
    this.service.loadSaleData(this.crop, this.year, this.season).subscribe(async result => {
      this.saleData = result;
      this.loadPendingData();
    }, err => console.log(err));
  }
  loadPendingData() {
    console.log(this.crop, this.year, this.season);

    this.service.loadPendingData(this.crop, this.year, this.season).subscribe(async result => {
      console.log(result, 'pending');
      this.pendingData = result;
    }, err => console.log(err));
  }
  getMatchingQuantity(districtCode: any, varietyId: any, category: any) {
    this.quantityArray = [];

    switch (category) {
      case 'pending':
        this.quantityArray = this.pendingData;
        break;
      case 'available':
        this.quantityArray = this.distWiseData;
        break;
      case 'sold':
        this.quantityArray = this.saleData;
        break;
      default:
        return 0;
    }
    if (!this.quantityArray) {
      return 0;
    }
    else {
      this.quantity = this.quantityArray.find(
        (q: any) => q._id.Variety === varietyId && q._id.Dist === districtCode
      );

      return this.quantity ? this.quantity.Qty / 100 : 0;
    }


  };
  showtable() {
    this.showTable = true;
    this.backButton=false;
  }
  back_Button(){
    this.backButton=false;
  }

  getPacsLamps() {
    this.service.getPacsLamps().subscribe(async result => {
      this.PacsLamps = result[0].seedPacsLamps;
      console.log(this.PacsLamps);

    }, err => console.log(err));
  }
  getDealerCount() {
    this.service.getDealerCount().subscribe(async result => {
      console.log(result[0]);

      this.DealerCount = result[0].seedDlrcount;
      console.log(this.DealerCount, 'DealerCount');

    }, err => console.log(err));
  }
  incrementAcademicYear(academicYear: any) {

    const [year, yearSuffix] = academicYear.split("-");

    let currentYear = parseInt(year, 10) + 1;
    let nextYearSuffix = parseInt(yearSuffix, 10) + 1;

    if (nextYearSuffix > 99) {
      currentYear += 1;
      nextYearSuffix = nextYearSuffix % 100;
    }

    const updatedYear = currentYear.toString();
    const updatedYearSuffix = nextYearSuffix.toString().padStart(2, "0");

    return `${updatedYear}-${updatedYearSuffix}`;
  }
  dealerwisedata12(district: any) {
    this.backButton=true;
    this.getItemName = [];
    this.dealerwisedata = [];
    this.invoiceItems = [];
    this.invoiceItems1 = [];
    this.invoiceItems2 = [];
    if (this.season.slice(0, 6) == 'KHARIF') {
      this.sisSeason = 'K'
    }
    if (this.season.slice(0, 4) == 'RABI') {
      this.sisSeason = 'R'
    }
    let sisFinYear = this.incrementAcademicYear(this.year);

    this.service.dealerwisedata(sisFinYear, this.sisSeason, this.crop, district.districtCode).subscribe(async result => {
      this.dealerwisedata = result;
      if (this.dealerwisedata.length > 0) {

        console.log(this.dealerwisedata);

        // var margeList = Enumerable.From(this.dealerwisedata)
        //     .GroupBy(function (item:any) { return item.LICENCE_NO })
        //     .Select(function (item:any) {
        //         return item.source;
        //     })
        //     .ToArray();
        var margeList: any[] = this.dealerwisedata.reduce((result: any, item: any) => {
          const existingGroup = result.find((group: any) => group[0].LICENCE_NO === item.LICENCE_NO);
          if (existingGroup) {
            existingGroup.push(item);
          } else {
            result.push([item]);
          }
          return result;
        }, []);
        this.invoiceItems = margeList;


        const addMissingVarieties = (sourceArray: any, targetArrays: any) => {
          sourceArray.forEach((item: any) => {
            const varietyCode = item.Variety_Code;
            let foundInAnyArray = false;

            targetArrays.forEach((targetArray: any) => {
              const existsInTarget = targetArray.some((targetItem: any) => targetItem.Variety_Code === varietyCode);
              if (!existsInTarget) {
                const newVariety = {
                  ...item,
                  rcvnoofbags: 0,
                  avlnoofbags: 0,
                  APP_FIRMNAME: targetArray[0].APP_FIRMNAME, LICENCE_NO: targetArray[0].LICENCE_NO
                };
                targetArray.push(newVariety);
                foundInAnyArray = true;
              }
            });

            if (!foundInAnyArray) {
              const newArray = [
                {
                  ...item,
                  rcvnoofbags: 0,
                  avlnoofbags: 0
                }
              ];
              targetArrays.push(newArray);

            }
          });
        };
        for (let i = 0; i < margeList.length; i++) {
          const currentArray = margeList[i];
          const otherArrays = [...margeList.slice(0, i), ...margeList.slice(i + 1)];
          addMissingVarieties(currentArray, otherArrays);
        }

        margeList.forEach((array: any) => {
          array.sort((a: any, b: any) => a.Variety_Name.localeCompare(b.Variety_Name));
        });
        console.log(margeList);
        this.invoiceItems1 = margeList;

        // var margeList1 = Enumerable.From(this.invoiceItems1)
        //   .GroupBy(function (item: any) { return item.LICENCE_NO })
        //   .Select(function (item: any) {
        //     return item.source;
        //   })
        //   .ToArray();
          var margeList1: any[] = this.invoiceItems1.reduce((result: any, item: any) => {
            const existingGroup = result.find((group: any) => group[0].LICENCE_NO === item.LICENCE_NO);
            if (existingGroup) {
              existingGroup.push(item);
            } else {
              result.push([item]);
            }
            return result;
          }, []);
          console.log(margeList1);
          
        this.invoiceItems2 = margeList1[0];
        this.sumByVarietyCode = {};
        this.sumByVarietyCode1 = {};
          
        for (var i = 0; i < this.invoiceItems2.length; i++) {
          var innerArray = this.invoiceItems2[i];
          for (var j = 0; j < innerArray.length; j++) {
            var item = innerArray[j];
            var varietyCode = item.Variety_Code;
            var avlnoofbags = item.avlnoofbags;
            var rcvnoofbags = item.rcvnoofbags;


            // Check if the Variety_Code already exists in the sumByVarietyCode object
            if (this.sumByVarietyCode.hasOwnProperty(varietyCode)) {
              this.sumByVarietyCode[varietyCode] += avlnoofbags;
              this.sumByVarietyCode1[varietyCode] += rcvnoofbags;

            } else {
              this.sumByVarietyCode[varietyCode] = avlnoofbags;
              this.sumByVarietyCode1[varietyCode] = rcvnoofbags;

            }
          }
        }

        this.resultArray = [];
        // for (let varietyCode = 0; varietyCode < this.sumByVarietyCode.length; varietyCode++) {
        //   // for ( i in this.sumByVarietyCode) {
        //   if (this.sumByVarietyCode.hasOwnProperty(varietyCode)) {
        //     var sum = this.sumByVarietyCode[varietyCode];
        //     var sum1 = this.sumByVarietyCode1[varietyCode];
        //     console.log(sum1, "sum1");
        //     this.resultArray.push({ Variety_Code: varietyCode, sum: sum, sum1: sum1 });
        //   }
        // }
        for (const varietyCode in this.sumByVarietyCode) {
          if (this.sumByVarietyCode.hasOwnProperty(varietyCode)) {
            console.log(this.sumByVarietyCode[varietyCode],'this.sumByVarietyCode[varietyCode]');
            
            const sum = this.sumByVarietyCode[varietyCode];
            const sum1 = this.sumByVarietyCode1[varietyCode];
            console.log(sum1, 'sum1');
            this.resultArray.push({ Variety_Code: varietyCode, sum, sum1 });
          }
        }
        console.log(this.resultArray,'resultArray');
        
      }
    }, err => console.log(err));
  }
}
