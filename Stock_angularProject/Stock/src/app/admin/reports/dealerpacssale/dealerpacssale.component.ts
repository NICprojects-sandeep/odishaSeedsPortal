import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import * as XLSX from 'xlsx';
@Component({
  selector: 'app-dealerpacssale',
  templateUrl: './dealerpacssale.component.html',
  styleUrls: ['./dealerpacssale.component.css']
})
export class DealerpacssaleComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedUserType: any = 'OSSC';
  SelectedDistrict: any = ''
  SelectedCrop: any = [];
  selectedToDate: any = '';
  selectedFromDate: any = '';
  SelectedMonth: any = '';
  SelectedScheme: any = '';
  maxdate: any;
  mindate: any;

  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  getAllDistrict: any = [];
  getAllDealerPacsSale: any = [];

  invoiceItems: any = [];
  invoiceItems1: any = [];
  invoiceItems2: any = [];
  sumByVarietyCode: any = {};
  sumByVarietyCode1: any = {};

  sumBy_VarietyCode: any = {};
  sumBy_VarietyCode1: any = {};
  alldata: any = [];
  distinctVarieties: any = {};
  distinctVarietyArray: any = [];
  distinctDistrict: any = {};
  distinctDistrictArray: any = [];
  sumArray: any = [];
  showpage: boolean = false;
  distinctnooffarmer: any = {};
  distinctnooffarmerArray: any = [];
  dealerpackssale: any[][] = [];
  groupedData5: any = [];
  sumArray1: any = [];
  sumArray2: number = 0;
  sumTotalDealerSale: any = 0.00;
  sumTotalPACSSale: any = 0.00;
  sumTotalTotalSale: any = 0.00;
  fileName: any = '';
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillCropCategory();
    this.maxdate = this.getDate();
    this.FillDistrict();
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
  FillFinYr() {
    this.getAllFinYr = []
    this.service.allFillFinYr().subscribe(data => {
      this.getAllFinYr = data;
    })
  }
  FillCropCategory() {
    this.getAllCatagory = []
    this.service.FillCropCategory().subscribe(data => {
      this.getAllCatagory = data;
    })
  }
  FillCropByStock_Farmer() {
    this.getAllCrop = []
    if (this.SelectedFinancialYear) {
      this.service.FillCropByStock_Farmer(this.SelectedFinancialYear).subscribe(data => {
        this.getAllCrop = data;
      })
    }

  }
  FillDistrict() {
    this.getAllDistrict = []
    this.service.FillDistrict().subscribe(data => {
      this.getAllDistrict = data;
    })
  }
  transformData(dealerwisedata: any): Observable<any[]> {
    return from(dealerwisedata).pipe(
      groupBy((item: any) => item.Dist_Code),
      mergeMap((group) => group.pipe(
        map((item) => item),
        toArray()
      )),
      toArray()
    );
  }
  dealerPacsSale() {
    this.getAllDealerPacsSale = [];
    this.invoiceItems = [];
    this.invoiceItems1 = [];
    this.invoiceItems2 = [];
    this.alldata = [];
    this.distinctVarietyArray = [];
    this.distinctDistrictArray = [];
    this.dealerpackssale = [];
    this.sumArray1 = [];
    this.sumArray2 = 0;
    this.sumTotalDealerSale = 0.00;
    this.sumTotalPACSSale = 0.00;
    this.sumTotalTotalSale = 0.00;
    this.distinctVarietyArray=[];
    this.distinctDistrictArray=[];
    this.distinctVarieties = [];
    this.distinctDistrict=[];
    this.sumArray=[];
    if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
    && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
    && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined && this.SelectedCrop.length > 0 
   ) {


    let object = {
      SelectedFinancialYear: this.SelectedFinancialYear,
      SelectedSeason: this.SelectedSeason,
      SelectedCrop: this.SelectedCrop,
    }
    
    this.service.dealerPacsSale(object).subscribe(data => {
      data.noofdealerpacs.sort((a: any, b: any) => a.Dist_Code.localeCompare(b.Dist_Code));
      data.alldata.sort((a: any, b: any) => a.Dist_Code.localeCompare(b.Dist_Code));


      this.getAllDealerPacsSale = data;

      if (data.alldata.length > 0) {


        this.transformData(data.alldata).subscribe((margeList) => {
          this.invoiceItems = margeList;


          const addMissingVarieties = (sourceArray: any, targetArrays: any) => {
            sourceArray.forEach((item: any) => {
              const varietyCode = item.CROP_VERID;
              const DealerPacks = item.DealerPacks;

              let foundInAnyArray = false;

              targetArrays.forEach((targetArray: any, index: number) => {
                const existsInTarget = targetArray.some((targetItem: any) => targetItem.CROP_VERID === varietyCode && targetItem.DealerPacks === DealerPacks);
                if (!existsInTarget) {
                  const newVariety = {
                    ...item,
                    sale: 0,
                    Dist_Name: targetArray[0].Dist_Name,
                    Dist_Code: targetArray[0].Dist_Code,
                  };
                  targetArray.push(newVariety);
                  foundInAnyArray = true;
                }
              });

              if (!foundInAnyArray) {
                const newArray = [
                  {
                    ...item,
                    sale: 0,
                    Dist_Name: '',
                    Dist_Code: '',
                  },
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
          this.invoiceItems1 = margeList;
          const groupedData = new Map<string, any[]>();

          this.invoiceItems1.forEach((item: any) => {
            const cropVerid = item.CROP_VERID;

            if (!groupedData.has(cropVerid)) {
              groupedData.set(cropVerid, []);
            }
            const group = groupedData.get(cropVerid);
            if (group) {
              group.push(item);
            }
          });
          this.alldata = this.invoiceItems1;

          this.invoiceItems1.forEach((array: any) => {
            array.sort((a: any, b: any) => a.DealerPacks.localeCompare(b.DealerPacks));
          });
          this.invoiceItems1.forEach((array: any) => {
            array.sort((a: any, b: any) => a.Variety_Name.localeCompare(b.Variety_Name));
          });

          for (let i = 0; i < this.alldata.length; i++) {
            var length = (this.alldata[i].length) / 2
            let k = 0
            for (let j = 0; j < this.alldata[i].length; j++) { // Start from the third object in each array

              if (length > k) {
                const arrayofobh = {
                  CROPCATG_ID: this.alldata[i][j].CROPCATG_ID,
                  CROP_VERID: this.alldata[i][j].CROP_VERID,
                  Category_Name: this.alldata[i][j].Category_Name,
                  Crop_Code: this.alldata[i][j].Crop_Code,
                  Crop_Name: this.alldata[i][j].Crop_Name,
                  DealerPacks: "Total",
                  Dist_Code: this.alldata[i][j].Dist_Code,
                  Dist_Name: this.alldata[i][j].Dist_Name,
                  Variety_Name: this.alldata[i][j].Variety_Name,
                  sale: +this.alldata[i][j].sale + +this.alldata[i][j + 1].sale,
                  nooffarmer: this.getAllDealerPacsSale.nooffarmer[i].nooffarmer
                }
                this.alldata[i].push(arrayofobh);
                k = ++k;
                j = j + 1;
              }
            }

          }
          this.alldata.forEach((array: any) => {
            array.sort((a: any, b: any) => a.DealerPacks.localeCompare(b.DealerPacks));
          });
          this.alldata.forEach((array: any) => {
            array.sort((a: any, b: any) => a.Variety_Name.localeCompare(b.Variety_Name));
          });


          for (let i = 0; i < this.alldata[0].length; i++) {
            let varietyName = this.alldata[0][i]["Variety_Name"];
            this.distinctVarieties[varietyName] = true;
          }
          for (let i = 0; i < this.alldata.length; i++) {
            let DistrictName = this.alldata[i][0]["Dist_Name"];
            this.distinctDistrict[DistrictName] = true;
          }
          for (let i = 0; i < this.alldata.length; i++) {
            let nooffarmer = this.alldata[i][2].nooffarmer;
            this.alldata[i][0].nooffarmer = this.alldata[i][2].nooffarmer
          }
          this.distinctVarietyArray = Object.keys(this.distinctVarieties).map((varietyName) => ({
            "Variety_Name": varietyName,
          }));

          this.distinctDistrictArray = Object.keys(this.distinctDistrict).map((DistrictName) => ({
            "Dist_Name": DistrictName,
          }));


        })
        for (let i = 0; i < this.alldata[0].length; i++) {
          let sum = 0;
          for (let j = 0; j < this.alldata.length; j++) {
            sum += parseFloat(this.alldata[j][i].sale);
            if(this.alldata[j][i].DealerPacks == 'Dealer'){
              this.sumTotalDealerSale += parseFloat(this.alldata[j][i].sale);
            }
            if(this.alldata[j][i].DealerPacks == 'PACS'){
              this.sumTotalPACSSale += parseFloat(this.alldata[j][i].sale);
            }
            if(this.alldata[j][i].DealerPacks == 'Total'){
              this.sumTotalTotalSale += parseFloat(this.alldata[j][i].sale);
            }
          }
          this.sumArray.push(sum);
          this.showpage = true;
        }
        this.groupedData5 = this.groupBy(this.getAllDealerPacsSale.noofdealerpacs, 'Dist_Code');

        for (let i = 0; i < 2; i++) {
          let sum = 0;
          for (let j = 0; j < this.groupedData5.length; j++) {
            console.log(this.groupedData5[j][i]);

            if (this.groupedData5[j][i] == undefined) {
            }
            sum += parseFloat(this.groupedData5[j][i].noofd);
            // }
          }
          this.sumArray1.push(sum);
        }
        for (let j = 0; j < this.getAllDealerPacsSale.nooffarmer.length; j++) {
          this.sumArray2 += parseInt(this.getAllDealerPacsSale.nooffarmer[j].nooffarmer);
        }
      }
    })
  }
  else {
    this.toastr.warning('Please select all field.');
  }
  }
  groupBy(array: any[], property: string): any[] {
    return array.reduce((acc, obj) => {
      const key = obj[property];
      const existingGroup = acc.find((group: any) => group[0][property] === key);

      if (existingGroup) {
        existingGroup.push(obj);
      } else {
        acc.push([obj]);
      }

      return acc;
    }, []);
  }
  calculateTotalSale(x: any, i: any) {
    console.log(x);
    
    this.alldata[i].totalDealerSale = 0;
    this.alldata[i].totalPACSSale = 0;
    this.alldata[i].totalTotalSale = 0;
    for (let index = 0; index < x.length; index++) {
      if (index == 0) {
        this.alldata[i].totalDealerSale = 0;
        this.alldata[i].totalPACSSale = 0;
        this.alldata[i].totalTotalSale = 0;
      }
      if (x[index].DealerPacks == 'Dealer') {
        this.alldata[i].totalDealerSale = this.alldata[i].totalDealerSale + parseFloat(x[index].sale);
      }
      if (x[index].DealerPacks == 'PACS') {
        this.alldata[i].totalPACSSale = this.alldata[i].totalPACSSale + parseFloat(x[index].sale)
      }
      if (x[index].DealerPacks == 'Total') {
        this.alldata[i].totalTotalSale = this.alldata[i].totalTotalSale + parseFloat(x[index].sale)
      }

    }

  }
  exportexcel(): void {
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();

    this.fileName = 'DealerPacssaleReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('tables');    
    if (element !== null && element !== undefined) {
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'DealerPacssaleReport');
  
      /* save to file */
      XLSX.writeFile(wb, this.fileName);
    }
   

  }
}
