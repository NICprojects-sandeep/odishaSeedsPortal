import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';

@Component({
  selector: 'app-ossclotentry',
  templateUrl: './ossclotentry.component.html',
  styleUrls: ['./ossclotentry.component.css']
})
export class OssclotentryComponent implements OnInit {
  getAllLot: any = [];
  getAllCrop: any = [];
  getAllVariety: any = [];
  SelectedCrop: any = '';
  SelectedVariety: any = '';
  SelectedLot: any = '';
  SelectedQty: any = '';
  SelectedSource: any = '';
  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillLots();
    this.FillCrop();
  }
  FillLots() {
    this.getAllLot = []
    this.service.FillLots().subscribe(data => {
      this.getAllLot = data;
    })
  }
  FillCrop() {
    this.getAllCrop = []
    this.service.FillCrop().subscribe(data => {
      this.getAllCrop = data;
    })
  }
  FillVariety() {
    this.getAllVariety = [];
    console.log(this.SelectedCrop);

    this.service.FillVariety(this.SelectedCrop).subscribe(data => {
      this.getAllVariety = data;
    })
  }
  addinClass() {

    return new Promise(async (resolve: any, reject: any) => {
      if (this.SelectedCrop != null && this.SelectedCrop != undefined && this.SelectedCrop != '' &&
        this.SelectedVariety != null && this.SelectedVariety != undefined && this.SelectedVariety != '' &&
        this.SelectedLot != null && this.SelectedLot != undefined && this.SelectedLot != '' &&
        this.SelectedQty != null && this.SelectedQty != undefined && this.SelectedQty != '' &&
        this.SelectedSource != null && this.SelectedSource != undefined && this.SelectedSource != '') {
        try {
          this.spinner.show();
          let data = {
            SelectedCrop: this.SelectedCrop,
            SelectedVariety: this.SelectedVariety,
            SelectedLot: this.SelectedLot,
            SelectedQty: this.SelectedQty,
            SelectedSource: this.SelectedSource

          }
          const result = await this.service.addinClass(data).toPromise();
          this.toastr.success(`Lot Number Added Successfully !!!`);
          this.spinner.hide();
          this.FillLots();
          // resolve(this.varietyList)
          // console.log(result);
          // if (result.result == 'True') {
          //   this.toastr.success(`Sucessfully Transfered and Cashmemo no is ${result.CASH_MEMO_NO}`);
          //   this.spinner.hide();
          // }

        } catch (e) {
          this.spinner.hide();
          console.error(e);

          reject()
        }
      }
      else {
        this.toastr.warning('Please Fill all field.');
      }

    }
    )

  }
}
