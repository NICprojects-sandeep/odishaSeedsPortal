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
  SelectedUserType: any = '';
  SelectedDistrict: any = ''
  SelectedCrop: any = [];
  selectedToDate: any = '';
  selectedFromDate: any = '';
  SelectedMonth: any = '';
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
  latestarray:any=[];
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
    // if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
    //   && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
    //   && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined
    //   && this.SelectedUserType !== null && this.SelectedUserType !== '' && this.SelectedUserType !== undefined
    //   && this.SelectedDistrict !== null && this.SelectedDistrict !== '' && this.SelectedDistrict !== undefined
    //   && this.SelectedMonth !== null && this.SelectedMonth !== '' && this.SelectedMonth !== undefined) {
    //   this.spinner.show();

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
      // console.log(data);
      this.stateStockPositionData = data;


    // if (this.stateStockPositionData.length > 0) {

    // this.stateStockPositionData = [
    //   {
    //     "Dist_Code": "01",
    //     "Dist_Name": "ANGUL",
    //     "CROP_VERID": "V009",
    //     "Variety_Name": "KHANDAGIRI",
    //     "Type": "PACS",
    //     "sale": "1.20",
    //     "USER_TYPE": "OSSC"
    //   }
    //   ,
    //   {
    //     "Dist_Code": "01",
    //     "Dist_Name": "ANGUL",
    //     "CROP_VERID": "V015",
    //     "Variety_Name": "MTU 1075 (IET 18482)",
    //     "Type": "PACS",
    //     "sale": "1.60",
    //     "USER_TYPE": "OSSC"
    //   },
    //   {
    //     "Dist_Code": "01",
    //     "Dist_Name": "ANGUL",
    //     "CROP_VERID": "V016",
    //     "Variety_Name": "MTU-7029(SWARNA)",
    //     "Type": "Dealer",
    //     "sale": "950.40",
    //     "USER_TYPE": "OSSC"
    //   },
    //   {
    //     "Dist_Code": "01",
    //     "Dist_Name": "ANGUL",
    //     "CROP_VERID": "V016",
    //     "Variety_Name": "MTU-7029(SWARNA)",
    //     "Type": "PACS",
    //     "sale": "96604.80",
    //     "USER_TYPE": "OSSC"
    //   },
    //   {
    //     "Dist_Code": "01",
    //     "Dist_Name": "ANGUL",
    //     "CROP_VERID": "V045",
    //     "Variety_Name": "MTU-1010 (COTTONDORA SANNALU)",
    //     "Type": "Dealer",
    //     "sale": "3.20",
    //     "USER_TYPE": "OSSC"
    //   },
    //   {
    //     "Dist_Code": "01",
    //     "Dist_Name": "ANGUL",
    //     "CROP_VERID": "V082",
    //     "Variety_Name": "MRUNALINI (OR 1898-18) IET 18649",
    //     "Type": "PACS",
    //     "sale": "173.60",
    //     "USER_TYPE": "OSSC"
    //   }
    // ]
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
        console.log(item);
        
        const cropVerid = item.CROP_VERID;

        if (!groupedData.has(cropVerid)) {
          groupedData.set(cropVerid, []);
        }
        const group = groupedData.get(cropVerid);
        if (group){
          group.push(item);
        }
      });
      groupedData.forEach((group, cropVerid) => {
        console.log(group);
        
        const hasPacs = group.some(item => item.Type === 'PACS');
        const hasDealer = group.some(item => item.Type === 'Dealer');
        console.log(hasPacs , hasDealer,hasPacs && !hasDealer,!hasPacs && hasDealer);
        
        if (hasPacs && !hasDealer) {
          // Add a new object with "DEALER" type and sale value 0
          group.push({
            "Dist_Code": group[0].Dist_Code,
            "Dist_Name": group[0].Dist_Name,
            "CROP_VERID": cropVerid,
            "Variety_Name": group[0].Variety_Name,
            "Type": "Dealer",
            "sale": "0.00",
            "USER_TYPE": "OSSC"
          });
          // const newVariety = {
          //   "Dist_Code": group[0].Dist_Code,
          //   "Dist_Name": group[0].Dist_Name,
          //   "CROP_VERID": cropVerid,
          //   "Variety_Name": group[0].Variety_Name,
          //   "Type": "Dealer",
          //   "sale": "0.00",
          //   "USER_TYPE": "OSSC"
          // };
          // this.latestarray.push(newVariety);
          
        }
        else if (!hasPacs && hasDealer) {
          // Add a new object with "DEALER" type and sale value 0
          group.push({
            "Dist_Code": group[0].Dist_Code,
            "Dist_Name": group[0].Dist_Name,
            "CROP_VERID": cropVerid,
            "Variety_Name": group[0].Variety_Name,
            "Type": "PACS",
            "sale": "0.00",
            "USER_TYPE": "OSSC"
          });

          // const newVariety = {
          //   "Dist_Code": group[0].Dist_Code,
          //   "Dist_Name": group[0].Dist_Name,
          //   "CROP_VERID": cropVerid,
          //   "Variety_Name": group[0].Variety_Name,
          //   "Type": "PACS",
          //   "sale": "0.00",
          //   "USER_TYPE": "OSSC"
          // };
          // this.latestarray.push(newVariety);
        }
        // else{
        //   const newArray = [
        //     {
        //       ...group,
              
        //     },
        //   ];

        //   this.latestarray.push(newArray);
        // }
      });
      console.log(this.latestarray);
      console.log(groupedData);
      

      const valuesData = [];
      let desiredValue = (groupedData: any) => {
        let output = [];
        for (let item of groupedData) {
            output.push(item[1]);
        }
        return output;
    };
    let result = desiredValue(groupedData);

    console.log(result);
    
      // // Iterate through the array and extract the 'value' property
      // for (const item of groupedData) {
        
      //   if (item.hasOwnProperty('value')) {
      //     console.log('if');
          
      //     valuesData.push(item.values);
      //   }
      // }
      // console.log(valuesData);
      
      // Flattening the grouped data back into an array
      // const result = Array.from(groupedData.values()).flat();
      // const resultData = Object.values(dataByCropVerid).flat();
      // const resultData = ([] as any[]).concat(...Object.values(groupedData.values()));
      // console.log(resultData);






      //other 

      // this.invoiceItems1.forEach((item: any) => {
      //   const cropVerid = item["CROP_VERID"];
      //   if (!this.dataByCropVerid[cropVerid]) {
      //     this.dataByCropVerid[cropVerid] = [];
      //   }

      //   this.dataByCropVerid[cropVerid].push(item);
      // });
      //             for (const cropVerid in this.dataByCropVerid) {
      //               console.log(cropVerid);

      //               const cropData = this.dataByCropVerid[cropVerid];
      //               console.log(cropData,this.dataByCropVerid);

      //               const typePresent = cropData.some((item: any) => "Type" in item);
      // console.log(typePresent,cropData);

      //               if (!typePresent) {
      //                 cropData.forEach((item: any) => {
      //                   item["sale"] = "0";
      //                 });
      //               }
      //             }
      //             const resultData = ([] as any[]).concat(...Object.values(this.dataByCropVerid));
      //             console.log(resultData);
      // resultData.forEach(array => {
      //   array.sort((a: any, b: any) => a.Variety_Name.localeCompare(b.Variety_Name));
      // });
    })


    //miising types
    // const addMissingTypes = (sourceArray: any, targetArrays: any) => {
    //   sourceArray.forEach((item: any) => {
    //     const Type = item.Type;
    //     let foundInAnyArray = false;

    //     targetArrays.forEach((targetArray: any, index: number) => {
    //       const existsInTarget = targetArray.some((targetItem: any) => targetItem.Type === Type);
    //       console.log(existsInTarget);
    //       console.log(targetArray);
    //       console.log(item);



    //       if (!existsInTarget) {
    //         const newVariety = {
    //           ...item,
    //           sale: 0,
    //           Type: targetArray[0].Type,
    //           Dist_Name: targetArray[0].Dist_Name,
    //           Dist_Code: targetArray[0].Dist_Code,
    //         };
    //         targetArray.push(newVariety);
    //         foundInAnyArray = true;
    //       }
    //     });

    //     if (!foundInAnyArray) {
    //       const newArray = [
    //         {
    //           ...item,
    //           sale: 0,
    //           Type: '',
    //           Dist_Name: '',
    //           Dist_Code: '',
    //         },
    //       ];

    //       targetArrays.push(newArray);
    //     }
    //   });
    // };
    // for (let i = 0; i < this.invoiceItems.length; i++) {
    //   const currentArray = this.invoiceItems[i];
    //   const otherArrays = [...this.invoiceItems.slice(0, i), ...this.invoiceItems.slice(i + 1)];
    //   addMissingTypes(currentArray, otherArrays);
    // }

    // this.invoiceItems.forEach((array:any) => {
    //   array.sort((a: any, b: any) => a.Type.localeCompare(b.Type));
    // });
    // this.invoiceItems2 = this.invoiceItems;
    // console.log(this.invoiceItems2);
    // }
    // else {
    //   this.spinner.hide();

    // }



    this.showpage = true;
    this.spinner.hide();
    this.getAllCrop = data;
    })
    // }
    // else {
    //   this.toastr.warning('Please select all field.');
    // }
  }
}