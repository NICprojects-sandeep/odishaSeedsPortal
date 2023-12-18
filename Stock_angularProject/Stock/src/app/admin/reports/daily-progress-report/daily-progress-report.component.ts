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
  fileName:any='';
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService
  ) { }

  ngOnInit(): void {
    this.dailyProgressReport();
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
  dailyProgressReport() {
    this.getAllData = []
    this.sumArray = [];
    this.service.dailyProgressReport().subscribe(data => {
      this.getAllData = data;
      console.log(this.getAllData);

      if (this.getAllData.length > 0) {
        this.transformData(this.getAllData).subscribe((margeList) => {
          // margeList will contain the grouped and transformed data
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
          console.log(margeList);
          margeList.forEach((array: any[]) => {
            array = array.filter((item: any) => item.Crop_Name != null);
            this.invoiceItems3.push(array);

          });
          this.invoiceItems3.forEach((array: any) => {
            array.sort((a: any, b: any) => a.Crop_Name.localeCompare(b.Crop_Name));
          });

          console.log(this.invoiceItems3);


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

          this.distinctVarietyArray = Object.keys(this.distinctVarieties).map((Crop_Name) => ({
            "Crop_Name": Crop_Name,
          }));
          this.distinctDistrictArray = Object.keys(this.distinctDistrict).map((DistrictName) => ({
            "Dist_Name": DistrictName,
          }));
          console.log(this.distinctVarietyArray);
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
              sumTotalFARMER_CNT += parseFloat(this.invoiceItems3[j][i].FARMER_CNT);



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
          console.log(this.sumArray);

        })

      }





    })
  }
  exportexcel(): void {
    this.fileName='';
    let latest_date = new Date().getDate();
    let getmonth = new Date().getMonth() + 1;
    let getFullYear = new Date().getFullYear();
    let getDate = new Date().getDate();

    this.fileName = 'DailyProgressReport_' + ' ' + getDate + '-' + getmonth + '-' + getFullYear + '.xlsx';
    /* table id is passed over here */
    let element = document.getElementById('tableId');    
    if (element !== null && element !== undefined) {
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'DailyProgressReport');
  
      /* save to file */
      XLSX.writeFile(wb, this.fileName);
    }
   

  }
}
