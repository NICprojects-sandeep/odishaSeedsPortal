import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import { DdaService } from 'src/app/Services/dda.service';

@Component({
  selector: 'app-currentstockpostion',
  templateUrl: './currentstockpostion.component.html',
  styleUrls: ['./currentstockpostion.component.css']
})
export class CurrentstockpostionComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedCropCatagory: any = [];
  SelectedAgency: any = 'OSSC'
  SelectedCrop: any = [];
  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  currentstockPositionData: any = [];
  godownwiseStockData: any = [];
  showpage: boolean = false;
  invoiceItems: any = [];
  invoiceItems1: any = []
  invoiceItems2: any = [];

  invoice_Items: any = [];
  invoice_Items1: any = []
  invoice_Items2: any = [];

  sumByVarietyCode: any = {};
  sumByVarietyCode1: any = {};

  sumBy_VarietyCode: any = {};
  sumBy_VarietyCode1: any = {};
  resultArray: any = [];
  result_Array: any = [];
  finyr: any = '';
  season: any = '';
  agency: any = '';
  category: any = '';
  crop: any = '';
  constructor(
    private fb: FormBuilder,
    private service: DdaService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

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
  fillCurrentstockPosition() {
    this.finyr = this.SelectedFinancialYear;
    this.season = this.SelectedSeason;
    this.agency = this.SelectedAgency;
    this.category = this.SelectedCropCatagory;
    this.crop = this.SelectedCrop;
    this.currentstockPositionData = [];
    this.invoiceItems1=[];
    this.invoiceItems2=[];
    if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
      && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
      && this.SelectedCropCatagory !== null && this.SelectedCropCatagory !== '' && this.SelectedCropCatagory !== undefined
      && this.SelectedAgency !== null && this.SelectedAgency !== '' && this.SelectedAgency !== undefined
      && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined) {
      this.spinner.show();
      this.service.fillCurrentstockPosition(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedAgency, this.SelectedCropCatagory, this.SelectedCrop).subscribe(data => {
        this.currentstockPositionData = data;
        this.showpage = true;
        // Assuming you have imported the necessary modules in your Angular component
        if (this.currentstockPositionData.length > 0) {
          this.transformData(this.currentstockPositionData).subscribe((margeList) => {
            // margeList will contain the grouped and transformed data
            this.invoiceItems = margeList;


            const addMissingVarieties = (sourceArray: any, targetArrays: any) => {
              sourceArray.forEach((item: any) => {
                const varietyCode = item.Crop_Verid;
                let foundInAnyArray = false;

                targetArrays.forEach((targetArray: any, index: number) => {
                  const existsInTarget = targetArray.some((targetItem: any) => targetItem.Crop_Verid === varietyCode);
                  if (!existsInTarget) {
                    const newVariety = {
                      ...item,
                      STOCK: 0,
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
                      STOCK: 0,
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

            margeList.forEach(array => {
              array.sort((a: any, b: any) => a.Variety_Name.localeCompare(b.Variety_Name));
            });

            this.invoiceItems1 = margeList;
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
              console.log(this.invoiceItems2);
              
              this.spinner.hide();
              this.resultArray = [];
              for (const Crop_Verid in this.sumByVarietyCode) {
                if (this.sumByVarietyCode.hasOwnProperty(Crop_Verid)) {
                  let sum = this.sumByVarietyCode[Crop_Verid];
                  this.resultArray.push({ Variety_Code: Crop_Verid, sum: sum });
                }
              }
            })
          })

        }
        else {
          this.spinner.hide();

        }
      })
    }
    else {
      this.toastr.warning('Please select all field.');
    }
  }
  trans_formData(dealerwisedata: any): Observable<any[]> {
    return from(dealerwisedata).pipe(
      groupBy((item: any) => item.Godown_ID),
      mergeMap((group) => group.pipe(
        map((item) => item),
        toArray()
      )),
      toArray()
    );
  }
  godowstock(x: any) {
    this.service.fillGodownWiseStock(this.finyr, this.season, this.agency, this.category, this.crop, x).subscribe(data => {
      this.godownwiseStockData = data;
      this.showpage = true;
      this.spinner.show();
      // Assuming you have imported the necessary modules in your Angular component
      if (this.godownwiseStockData.length > 0) {
        this.trans_formData(this.godownwiseStockData).subscribe((margeList) => {
          // margeList will contain the grouped and transformed data
          this.invoice_Items = margeList;


          const addMissingVarieties = (sourceArray: any, targetArrays: any) => {
            sourceArray.forEach((item: any) => {
              const varietyCode = item.Crop_Verid;
              let foundInAnyArray = false;

              targetArrays.forEach((targetArray: any, index: number) => {
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

          margeList.forEach(array => {
            array.sort((a: any, b: any) => a.Variety_Name.localeCompare(b.Variety_Name));
          });

          this.invoice_Items1 = margeList;
          this.transformData(this.invoice_Items1).subscribe((margeList1) => {
            this.invoice_Items2 = margeList1[0];
            this.sumBy_VarietyCode = {};
            this.sumBy_VarietyCode1 = {};

            for (let i = 0; i < this.invoice_Items2.length; i++) {
              const innerArray = this.invoice_Items2[i];
              for (let j = 0; j < innerArray.length; j++) {
                const item = innerArray[j];
                let varietyCode = item.Crop_Verid;
                let avlnoofbags = parseFloat(item.STOCK);

                // Check if the Variety_Code already exists in the sumBy_VarietyCode object
                if (this.sumBy_VarietyCode.hasOwnProperty(varietyCode)) {
                  this.sumBy_VarietyCode[varietyCode] += avlnoofbags;
                } else {
                  this.sumBy_VarietyCode[varietyCode] = avlnoofbags;
                }
              }
            }
            console.log(this.invoice_Items1);

            this.spinner.hide();
            this.result_Array = [];
            for (const Crop_Verid in this.sumBy_VarietyCode) {
              if (this.sumBy_VarietyCode.hasOwnProperty(Crop_Verid)) {
                let sum = this.sumBy_VarietyCode[Crop_Verid];
                this.result_Array.push({ Variety_Code: Crop_Verid, sum: sum });
              }
            }

          })
        })

      }
      else {
        this.spinner.hide();

      }
    })

  }
}