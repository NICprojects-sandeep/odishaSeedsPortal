import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { groupBy, map, mergeMap, toArray } from 'rxjs/operators';
import { from, Observable } from 'rxjs';
import { DdaService } from 'src/app/Services/dda.service';

@Component({
  selector: 'app-farmersale',
  templateUrl: './farmersale.component.html',
  styleUrls: ['./farmersale.component.css']
})
export class FarmersaleComponent implements OnInit {
  GetFarmerSaleData: any = [];
  invoiceItems: any = [];
  invoiceItems1: any = [];

  constructor(private fb: FormBuilder,
    private service: DdaService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.GetFarmerSaleDetails();
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
  GetFarmerSaleDetails() {
    this.GetFarmerSaleData = [];
    this.invoiceItems = [];
    this.invoiceItems1 = [];

    this.service.GetFarmerSaleDetails().subscribe(data => {
      console.log(data);
      this.GetFarmerSaleData = data;
      if (this.GetFarmerSaleData.length > 0) {
        this.transformData(this.GetFarmerSaleData).subscribe((margeList) => {
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
        })
      }


    })
  }
}
