import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
@Component({
  selector: 'app-varietywiselift',
  templateUrl: './varietywiselift.component.html',
  styleUrls: ['./varietywiselift.component.css']
})
export class VarietywiseliftComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedUserType: any = 'OSSC';
  SelectedDistrict: any = ''
  SelectedCrop: any = [];
  selectedToDate: any = '';
  selectedFromDate: any = '';
  SelectedMonth: any = '0';
  maxdate: any;
  mindate: any;

  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  getAllDistrict: any = [];
  stateStockPositionData: any = [];
  showpage: boolean = false;

  invoiceItems: any = [];
  invoiceItems1: any = []
  invoiceItems2: any = [];
  sumByVarietyCode: any = {};
  sumByVarietyCode1: any = {};
  resultArray: any = [];
  dataByCropVerid: any = {};
  latestarray: any = [];
  cropVeridMap: any = {};
  distinctVarieties: any = {};
  distinctVarietyArray: any = [];
  distinctDistrict: any = {};
  distinctDistrictArray: any = [];
  alldata: any = [];
  colorClasses: string[] = ['color1', 'color2', 'color3'];
  sumArray: number[] = [];
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.maxdate = this.getDate();
    this.FillDistrict();
    this.FillCategoryId();

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
  FillCategoryId() {
    this.getAllCrop = []
    this.service.FillCategoryId().subscribe(data => {
      this.getAllCrop = data;
    })
  }
  FillDistrict() {
    this.getAllDistrict = []
    this.service.FillDistrict().subscribe(data => {
      this.getAllDistrict = data;
      this.getAllDistrict.unshift({
        Dist_Code: 0, Dist_Name: 'All'
      });
      this.SelectedDistrict= this.getAllDistrict[0].Dist_Code
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
  getVarietywiseLift() {
    this.stateStockPositionData=[];
    this.invoiceItems=[];
    this.invoiceItems1=[];
    this.alldata=[];
    this.distinctVarieties=[];
    this.sumArray=[];
    if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
      && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
      && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined
      && this.SelectedUserType !== null && this.SelectedUserType !== '' && this.SelectedUserType !== undefined
      && this.SelectedDistrict !== null && this.SelectedDistrict !== '' && this.SelectedDistrict !== undefined
      && this.SelectedMonth !== null && this.SelectedMonth !== '' && this.SelectedMonth !== undefined) {
      this.spinner.show();

      let data = {
        SelectedFinancialYear: this.SelectedFinancialYear,
        SelectedSeason: this.SelectedSeason,
        SelectedUserType: this.SelectedUserType,
        SelectedCrop: this.SelectedCrop,
        selectedFromDate: this.selectedFromDate,
        selectedToDate: this.selectedToDate,
        SelectedDistrict: this.SelectedDistrict,
        SelectedMonth: this.SelectedMonth
      }
      this.service.getVarietywiseLift(data).subscribe(data => {
        this.stateStockPositionData = data;
        console.log(this.stateStockPositionData);
        


        if (this.stateStockPositionData.length > 0) {


          this.transformData(this.stateStockPositionData).subscribe((margeList) => {
            // margeList will contain the grouped and transformed data
            this.invoiceItems = margeList;


            const addMissingVarieties = (sourceArray: any, targetArrays: any) => {
              sourceArray.forEach((item: any) => {
                const varietyCode = item.CROP_VERID;
                const Type = item.Type;

                let foundInAnyArray = false;

                targetArrays.forEach((targetArray: any, index: number) => {
                  const existsInTarget = targetArray.some((targetItem: any) => targetItem.CROP_VERID === varietyCode && targetItem.Type === Type);
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
            // groupedData.forEach((group, cropVerid) => {
            //   const hasPacs = group.some(item => item.Type === 'PACS');
            //   const hasDealer = group.some(item => item.Type === 'Dealer');
            //   if (hasPacs && !hasDealer) {
            //     // Add a new object with "DEALER" type and sale value 0
            //     group.push({
            //       "Dist_Code": group[0].Dist_Code,
            //       "Dist_Name": group[0].Dist_Name,
            //       "CROP_VERID": cropVerid,
            //       "Variety_Name": group[0].Variety_Name,
            //       "Type": "Dealer",
            //       "sale": "0.00",
            //       "USER_TYPE": "OSSC"
            //     });

            //     // const newVariety = {
            //     //   "Dist_Code": group[0].Dist_Code,
            //     //   "Dist_Name": group[0].Dist_Name,
            //     //   "CROP_VERID": cropVerid,
            //     //   "Variety_Name": group[0].Variety_Name,
            //     //   "Type": "Dealer",
            //     //   "sale": "0.00",
            //     //   "USER_TYPE": "OSSC"
            //     // };
            //     // this.latestarray.push(newVariety);

            //   }
            //   else if (!hasPacs && hasDealer) {
            //     // Add a new object with "DEALER" type and sale value 0
            //     group.push({
            //       "Dist_Code": group[0].Dist_Code,
            //       "Dist_Name": group[0].Dist_Name,
            //       "CROP_VERID": cropVerid,
            //       "Variety_Name": group[0].Variety_Name,
            //       "Type": "PACS",
            //       "sale": "0.00",
            //       "USER_TYPE": "OSSC"
            //     });

            //     // const newVariety = {
            //     //   "Dist_Code": group[0].Dist_Code,
            //     //   "Dist_Name": group[0].Dist_Name,
            //     //   "CROP_VERID": cropVerid,
            //     //   "Variety_Name": group[0].Variety_Name,
            //     //   "Type": "PACS",
            //     //   "sale": "0.00",
            //     //   "USER_TYPE": "OSSC"
            //     // };
            //     // this.latestarray.push(newVariety);
            //   }
            //   // else{
            //   //   const newArray = [
            //   //     {
            //   //       ...group,

            //   //     },
            //   //   ];

            //   //   this.latestarray.push(newArray);
            //   // }
            // });


            // const valuesData = [];
            // let desiredValue = (groupedData: any) => {
            //   let output = [];
            //   for (let item of groupedData) {
            //     output.push(item[1]);
            //   }
            //   return output;
            // };
            // let result = desiredValue(groupedData);
            this.alldata = this.invoiceItems1;

            this.invoiceItems1.forEach((array: any) => {
              array.sort((a: any, b: any) => a.Type.localeCompare(b.Type));
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
                    "Dist_Code": this.alldata[i][j].Dist_Code,
                    "Dist_Name": this.alldata[i][j].Dist_Name,
                    "CROP_VERID": this.alldata[i][j].CROP_VERID,
                    "Type": "Total",
                    "USER_TYPE": this.alldata[i][j].USER_TYPE,
                    "Variety_Name": this.alldata[i][j].Variety_Name,
                    "sale": +this.alldata[i][j].sale + +this.alldata[i][j + 1].sale

                  }
                  this.alldata[i].push(arrayofobh);
                  k = ++k;
                  j = j + 1;
                }
                // this.alldata[i].push({
                //   "Dist_Code": this.alldata[i][j].Dist_Code,
                //   "Dist_Name": this.alldata[i][j].Dist_Name,
                //   "CROP_VERID": this.alldata[i][j].CROP_VERID,
                //   "Variety_Name": this.alldata[i][j].Variety_Name,
                //   "Type": "Total",
                //   "sale": parseFloat(this.alldata[i][j].sale) + parseFloat(this.alldata[i][j + 1].sale),
                //   "USER_TYPE": "OSSC"
                // });


              }

            }
            this.alldata.forEach((array: any) => {
              array.sort((a: any, b: any) => a.Type.localeCompare(b.Type));
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
            console.log(this.distinctVarieties);
            
            // Create a new array with distinct "Variety_Name" values
            this.distinctVarietyArray = Object.keys(this.distinctVarieties).map((varietyName) => ({
              "Variety_Name": varietyName,
            }));

            this.distinctDistrictArray = Object.keys(this.distinctDistrict).map((DistrictName) => ({
              "Dist_Name": DistrictName,
            }));










            // // Iterate through the array and extract the 'value' property
            // for (const item of groupedData) {

            //   if (item.hasOwnProperty('value')) {

            //     valuesData.push(item.values);
            //   }
            // }
            // Flattening the grouped data back into an array
            // const result = Array.from(groupedData.values()).flat();
            // const resultData = Object.values(dataByCropVerid).flat();
            // const resultData = ([] as any[]).concat(...Object.values(groupedData.values()));






            //other 

            // this.invoiceItems1.forEach((item: any) => {
            //   const cropVerid = item["CROP_VERID"];
            //   if (!this.dataByCropVerid[cropVerid]) {
            //     this.dataByCropVerid[cropVerid] = [];
            //   }

            //   this.dataByCropVerid[cropVerid].push(item);
            // });
            //             for (const cropVerid in this.dataByCropVerid) {
            //               const cropData = this.dataByCropVerid[cropVerid];

            //               const typePresent = cropData.some((item: any) => "Type" in item);

            //               if (!typePresent) {
            //                 cropData.forEach((item: any) => {
            //                   item["sale"] = "0";
            //                 });
            //               }
            //             }
            //             const resultData = ([] as any[]).concat(...Object.values(this.dataByCropVerid));
            // resultData.forEach(array => {
            //   array.sort((a: any, b: any) => a.Variety_Name.localeCompare(b.Variety_Name));
            // });
          })
          for (let i = 0; i < this.alldata[0].length; i++) {
            let sum = 0;
            for (let j = 0; j < this.alldata.length; j++) {
              sum += parseFloat(this.alldata[j][i].sale);
            }
            this.sumArray.push(sum);
          }
        }


        this.showpage = true;
        this.spinner.hide();
      })
    }
    else {
      this.toastr.warning('Please select all field.');
    }
  }
}