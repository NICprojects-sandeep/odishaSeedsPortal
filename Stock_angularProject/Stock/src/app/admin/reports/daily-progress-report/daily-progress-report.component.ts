import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import * as XLSX from 'xlsx';

@Component({
  selector: 'app-daily-progress-report',
  templateUrl: './daily-progress-report.component.html',
  styleUrls: ['./daily-progress-report.component.css']
})
export class DailyProgressReportComponent implements OnInit {
  getAllData: any = [];

  invoiceItems: any = [];
  invoiceItems1: any = []
  invoiceItems2: any = [];
  invoiceItems3: any = [];
  distinctVarieties: any = {};
  distinctVarietyArray: any = [];
  distinctDistrict: any = {};
  distinctDistrictArray: any = [];
  sumArray: any = [];
  fileName: any = '';

  getAllData1: any = [];
  customOrder: any = [];
  resultArray: any = [];
  sum_Array:any=[];
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.dailyProgressReport();
    this.previousYeardailyProgressReport();

    this.customOrder = ["PaddyDhan",
      "Finger millet (Ragi)",
      "Green gram (Mung bean/Moong)",
      "Black gram (Biri)",
      "Bengal gram (Gram/Kabuli/Chana)",
      "Peas (Field pea/ Garden) ", "Ground nut PeanutMung phalli", "Toria", "Sesame (Gingelly/Til)", "Linseed (Alsi)",  /* add other Crop_Names as needed */];
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
  findElementsByCropName(CROP_ID: string[]): any[] {
    return CROP_ID.map(CROP_ID => this.invoiceItems3.find((item: any) => item.CROP_ID === CROP_ID)).filter(Boolean);
  }
  dailyProgressReport() {
    this.getAllData = []
    this.sumArray = [];
    this.service.dailyProgressReport().subscribe(data => {
      this.getAllData = data;

      if (this.getAllData.length > 0) {
        this.transformData(this.getAllData).subscribe((margeList) => {
          this.invoiceItems = margeList;
          const addMissingVarieties = (sourceArray: any, targetArrays: any) => {
            sourceArray.forEach((item: any) => {
              const cropCode = item.CROP_ID;
              const Type = item.Type;

              let foundInAnyArray = false;

              targetArrays.forEach((targetArray: any, index: number) => {
                const existsInTarget = targetArray.some((targetItem: any) => targetItem.CROP_ID === cropCode);
                if (!existsInTarget) {
                  const newVariety = {
                    ...item,
                    DEALER_RCV: 0,
                    DEALER_SALE: 0,
                    FARMER_CNT: 0,
                    PACS_RCV: 0,
                    PACS_SALE: 0,
                    TOT_RCV: 0,
                    TOT_SALE: 0,
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
                    DEALER_RCV: 0,
                    DEALER_SALE: 0,
                    FARMER_CNT: 0,
                    PACS_RCV: 0,
                    PACS_SALE: 0,
                    TOT_RCV: 0,
                    TOT_SALE: 0,
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
          margeList.forEach((array: any[]) => {
            array = array.filter((item: any) => item.Crop_Name != null);
            this.invoiceItems3.push(array);

          });
          this.invoiceItems3.forEach((array: any) => {
            array.sort((a: any, b: any) => a.CROP_ID.localeCompare(b.CROP_ID));
          });
          this.invoiceItems3 = this.invoiceItems3.map((distArray: any) => {
            // Sort each district array based on the custom order
            return distArray.sort((a: any, b: any) => {
              return this.customOrder.indexOf(a.Crop_Name) - this.customOrder.indexOf(b.Crop_Name);
            });
          });


          for (let i = 0; i < this.invoiceItems3[0].length; i++) {
            let Crop_Name = this.invoiceItems3[0][i]["Crop_Name"];
            this.distinctVarieties[Crop_Name] = true;

          }
          if (this.invoiceItems3[0].length > 0) {
            for (let i = 0; i < this.invoiceItems3.length; i++) {
              let DistrictName = this.invoiceItems3[i][0]["Dist_Name"];
              this.distinctDistrict[DistrictName] = true;
            }
          }
          //  const customOrder = ["Bengal gram (Gram/Kabuli/Chana)","Black gram (Biri)","Finger millet (Ragi)","Green gram (Mung bean/Moong)","Ground nut PeanutMung phalli", "Linseed (Alsi)",
          //   "PaddyDhan", "Peas (Field pea/ Garden)","Sesame (Gingelly/Til)", "Toria",  /* add other Crop_Names as needed */];


          this.distinctVarietyArray = this.customOrder
            .filter((cropName: any) => this.distinctVarieties[cropName])
            .map((Crop_Name: any) => ({
              "Crop_Name": Crop_Name,
            }));
          // this.distinctVarietyArray = Object.keys(this.distinctVarieties).map((Crop_Name) => ({
          //   "Crop_Name": Crop_Name,
          // }));
          // this.distinctDistrictArray = Object.keys(this.distinctDistrict).map((DistrictName) => ({
          //   "Dist_Name": DistrictName,
          // }));
          this.distinctDistrictArray = Object.keys(this.distinctDistrict)
            .sort((a, b) => a.localeCompare(b))  // Sort alphabetically
            .map((districtName) => ({
              "Dist_Name": districtName,
            }));
          for (let i = 0; i < this.invoiceItems3[0].length; i++) {
            let sumTotalDEALER_RCV = 0
            let sumTotalPACS_RCV = 0
            let sumTotalTOT_RCV = 0
            let sumTotalDEALER_SALE = 0
            let sumTotalPACS_SALE = 0
            let sumTotalTOT_SALE = 0
            let sumTotalFARMER_CNT = 0

            for (let j = 0; j < this.invoiceItems3.length; j++) {
              sumTotalDEALER_RCV += parseFloat(this.invoiceItems3[j][i].DEALER_RCV);
              sumTotalPACS_RCV += parseFloat(this.invoiceItems3[j][i].PACS_RCV);
              sumTotalTOT_RCV += parseFloat(this.invoiceItems3[j][i].TOT_RCV);
              sumTotalDEALER_SALE += parseFloat(this.invoiceItems3[j][i].DEALER_SALE);
              sumTotalPACS_SALE += parseFloat(this.invoiceItems3[j][i].PACS_SALE);
              sumTotalTOT_SALE += parseFloat(this.invoiceItems3[j][i].TOT_SALE);
              sumTotalFARMER_CNT += parseInt(this.invoiceItems3[j][i].FARMER_CNT);



              // this.sumTotalDEALER_RCV += parseFloat(this.invoiceItems3[j][i].DEALER_RCV);
              // this.sumTotalPACS_RCV += parseFloat(this.invoiceItems3[j][i].PACS_RCV);
              // this.sumTotalTOT_RCV += parseFloat(this.invoiceItems3[j][i].TOT_RCV);
              // this.sumTotalDEALER_SALE += parseFloat(this.invoiceItems3[j][i].DEALER_SALE);
              // this.sumTotalPACS_SALE += parseFloat(this.invoiceItems3[j][i].PACS_SALE);
              // this.sumTotalTOT_SALE += parseFloat(this.invoiceItems3[j][i].TOT_SALE);
              // this.sumTotalFARMER_CNT += parseFloat(this.invoiceItems3[j][i].FARMER_CNT);

            }
            this.sumArray.push(sumTotalDEALER_RCV);
            this.sumArray.push(sumTotalPACS_RCV);
            this.sumArray.push(sumTotalTOT_RCV);
            this.sumArray.push(sumTotalDEALER_SALE);
            this.sumArray.push(sumTotalPACS_SALE);
            this.sumArray.push(sumTotalTOT_SALE);
            this.sumArray.push(sumTotalFARMER_CNT);

          }

        })

      }





    })
  }

  previousYeardailyProgressReport() {
    this.resultArray = [];
    this.sum_Array=[];
    this.service.previousYeardailyProgressReport().subscribe(data => {
      this.getAllData1 = data;
      console.log(data);
      console.log(this.getAllData);
      this.resultArray = mergeArrays(this.getAllData1, this.getAllData);
      this.resultArray = this.resultArray.sort((crop1: any, crop2: any) => {
        let index1 = this.customOrder.indexOf(crop1.Crop_Name);
        let index2 = this.customOrder.indexOf(crop2.Crop_Name);

        // If both items are in the custom order array, sort based on their indices
        if (index1 !== -1 && index2 !== -1) {
          return index1 - index2;
        }

        // If one item is in the custom order array and the other is not, prioritize the one in the array
        if (index1 !== -1) {
          return -1;
        }

        if (index2 !== -1) {
          return 1;
        }

        // If neither item is in the custom order array, perform a regular alphabetical sort
        return crop1.Crop_Name.localeCompare(crop2.Crop_Name);
      });

      console.log(this.resultArray);
      for (let i = 0; i < this.resultArray.length; i++) {

        this.sum_Array.push(this.resultArray[i].DEALER_RCV);
        this.sum_Array.push(this.resultArray[i].PACS_RCV);
        this.sum_Array.push(this.resultArray[i].TOT_RCV);
        this.sum_Array.push(this.resultArray[i].DEALER_SALE);
        this.sum_Array.push(this.resultArray[i].PACS_SALE);
        this.sum_Array.push(this.resultArray[i].TOT_SALE);
        this.sum_Array.push(this.resultArray[i].FARMER_CNT);

      }
    })
  }
  exportexcel(): void {
    this.fileName = '';
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();

    this.fileName = 'DailyProgressReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('tableId');
    if (element !== null && element !== undefined) {
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

      // start




      //end









      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'DailyProgressReport');

      /* save to file */
      XLSX.writeFile(wb, this.fileName);
    }


  }







}
function mergeArrays(a: any, b: any) {
  // Iterate over each element in array b
  b.forEach((bItem: any) => {
    // Find the corresponding element in array a based on "CROP_ID"
    const matchingItemIndex = a.findIndex((aItem: any) => aItem.CROP_ID === bItem.CROP_ID);

    // If a match is found and "CROP_ID" is not null, update the values in array a
    if (matchingItemIndex !== -1 && bItem.CROP_ID !== null) {
      a[matchingItemIndex] = {
        ...a[matchingItemIndex],
        // ...bItem,
      };
    } else if (bItem.CROP_ID !== null) {
      // If no match is found and "CROP_ID" is not null, insert the entry into array a
      // a.push(bItem);
      const newVariety = {
        CROP_ID: bItem.CROP_ID,
        Crop_Name: bItem.Crop_Name,
        DEALER_RCV: 0,
        DEALER_SALE: 0,
        FARMER_CNT: 0,
        PACS_RCV: 0,
        PACS_SALE: 0,
        TOT_RCV: 0,
        TOT_SALE: 0,
      };
      a.push(newVariety);
    }
  });
  a = a.filter((aItem: any) => {
    return b.some((bItem: any) => bItem.CROP_ID === aItem.CROP_ID);
  });
  return a;
}
