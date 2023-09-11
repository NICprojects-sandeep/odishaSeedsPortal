import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';
import { from, Observable } from 'rxjs';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';

@Component({
  selector: 'app-godownwisestock',
  templateUrl: './godownwisestock.component.html',
  styleUrls: ['./godownwisestock.component.css']
})
export class GodownwisestockComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = [];
  SelectedCropCatagory: any = [];
  SelectedCrop: any = [];

  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  godownwiseStockData: any = [];
  alldata: any = [];
  existingEntry: any = {};
  distinctVarietyNames: any = [];
  totalStock: any = 0.00
  invoiceItems: any = [];
  invoiceItems1: any = []
  invoiceItems2: any = [];
  sumByVarietyCode: any = {};
  sumByVarietyCode1: any = {};
  resultArray: any = [];

  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  transformData(dealerwisedata: any): Observable<any[]> {
    return from(dealerwisedata).pipe(
      groupBy((item: any) => item.Godown_Name),
      mergeMap((group) => group.pipe(
        map((item) => item),
        toArray()
      )),
      toArray()
    );
  }
  ngOnInit(): void {
    this.FillFinYr();
    this.FillCropCategory();
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
  FillCropByCategoryId() {
    this.getAllCrop = []
    this.service.FillCropByCategoryId(this.SelectedCropCatagory).subscribe(data => {
      this.getAllCrop = data;
    })
  }
  addMissingVarieties(sourceArray: any, targetArrays: any) {
    sourceArray.forEach((item: any) => {
      const varietyCode = item.Crop_Verid;
      let foundInAnyArray = false;

      targetArrays.forEach((targetArray: any) => {
        const existsInTarget = targetArray.some((targetItem: any) => targetItem.Crop_Verid === varietyCode);
        // console.log(targetArray[0].LICENCE_NO == 'ODGAN2/2017-18/0006');
        // if (targetArray[0].LICENCE_NO == 'ODGAN2/2017-18/0006') {
        //     console.log(item);
        // }
        if (!existsInTarget) {
          const newVariety = {
            ...item,
            rcvnoofbags: 0,
            avlnoofbags: 0,
            Godown_Name: targetArray[0].Godown_Name, Godown_ID: targetArray[0].Godown_ID
          };
          targetArray.push(newVariety);
          // if(targetArray[0].LICENCE_NO=='ODGAN2/2017-18/0016'){
          // console.log(targetArray);
          // }
          foundInAnyArray = true;
        }
      });

      if (!foundInAnyArray) {
        const newArray = [
          {
            ...item,
            rcvnoofbags: 0,
            avlnoofbags: 0,
            Godown_Name: '',
            Godown_ID: ''
          }
        ];

        targetArrays.push(newArray);

      }
    });
  }
  fillGodownwisestock() {
    this.godownwiseStockData = [];
    this.totalStock = 0.00;
    this.distinctVarietyNames = [];
    this.alldata = [];
    if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
      && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
      && this.SelectedCropCatagory !== null && this.SelectedCropCatagory !== '' && this.SelectedCropCatagory !== undefined
      && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined) {
        this.spinner.show();
        this.service.fillGodownwisestock(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedCropCatagory, this.SelectedCrop).subscribe(data => {
          this.godownwiseStockData = data;
          // Assuming you have imported the necessary modules in your Angular component
    
          this.transformData(this.godownwiseStockData).subscribe((margeList) => {
            // margeList will contain the grouped and transformed data
            console.log(margeList);
            this.invoiceItems = margeList;
    
    
            const addMissingVarieties = (sourceArray: any, targetArrays: any) => {
              sourceArray.forEach((item: any) => {
                const varietyCode = item.Crop_Verid;
                let foundInAnyArray = false;
    
                targetArrays.forEach((targetArray: any) => {
                  const existsInTarget = targetArray.some((targetItem: any) => targetItem.Crop_Verid === varietyCode);
                  if (!existsInTarget) {
                    const newVariety = {
                      ...item,
                      STOCK: 0,
                      Godown_Name: targetArray[0].Godown_Name,
                      Godown_ID: targetArray[0].Godown_ID,
                    };
                    targetArray.push(newVariety);
                    foundInAnyArray = true;
                  }
                });
    
                if (!foundInAnyArray) {
                  const newArray = [
                    {
                      ...item,
                      STOCK: 0,
                      Godown_Name: '',
                      Godown_ID: '',
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
    
            margeList.forEach(array => {
              console.log(array);
    
              array.sort((a: any, b: any) => a.Variety_Name.localeCompare(b.Variety_Name));
            });
    
            this.invoiceItems1 = margeList;
            console.log(this.invoiceItems1);
            this.transformData(this.invoiceItems1).subscribe((margeList1) => {
              this.invoiceItems2 = margeList1[0];
              this.sumByVarietyCode = {};
              this.sumByVarietyCode1 = {};
    
              for (let i = 0; i < this.invoiceItems2.length; i++) {
                const innerArray = this.invoiceItems2[i];
                for (let j = 0; j < innerArray.length; j++) {
                  const item = innerArray[j];
                  let varietyCode = item.Crop_Verid;
                  let avlnoofbags = parseFloat(item.STOCK);
    
                  // Check if the Variety_Code already exists in the sumByVarietyCode object
                  if (this.sumByVarietyCode.hasOwnProperty(varietyCode)) {
                    this.sumByVarietyCode[varietyCode] += avlnoofbags;
                  } else {
                    this.sumByVarietyCode[varietyCode] = avlnoofbags;
                  }
                }
              }
              this.spinner.hide();
              this.resultArray = [];
              for (const Crop_Verid in this.sumByVarietyCode) {
                if (this.sumByVarietyCode.hasOwnProperty(Crop_Verid)) {
                  let sum = this.sumByVarietyCode[Crop_Verid];
                  this.resultArray.push({ Variety_Code: Crop_Verid, sum: sum });
                }
              }
              console.log(this.resultArray);
    
            })
          })
        })
    }
    else{
      this.toastr.warning('Please select all field.');
    }
   
  }

}