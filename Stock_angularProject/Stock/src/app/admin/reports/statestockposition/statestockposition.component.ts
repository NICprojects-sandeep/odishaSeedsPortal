import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-statestockposition',
  templateUrl: './statestockposition.component.html',
  styleUrls: ['./statestockposition.component.css']
})
export class StatestockpositionComponent implements OnInit {
  SelectedFinancialYear: any = [];
  SelectedSeason: any = '';
  SelectedCropCatagory: any = [];
  SelectedDistrict: any = ''
  SelectedCrop: any = [];
  selectedToDate: any = ''
  maxdate: any;
  showpage: boolean = false;

  getAllCrop: any = [];
  getAllCatagory: any = [];
  getAllFinYr: any = [];
  getAllDistrict: any = [];
  stateStockPositionData: any = [];
  constructor(
    private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillCropCategory();
    this.FillDistrict();
    this.maxdate = this.getDate();
  }
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
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
  FillDistrict() {
    this.getAllDistrict = []
    this.service.FillDistrict().subscribe(data => {
      this.getAllDistrict = data;
      this.getAllDistrict.unshift({
        Dist_Code: 0, Dist_Name: 'All'
      });
    })
  }
  FillCropByCategoryId() {
    this.getAllCrop = []
    this.service.FillCropByCategoryId(this.SelectedCropCatagory).subscribe(data => {
      this.getAllCrop = data;
    })
  }
  fillStateStockPosition() {
    if (this.SelectedFinancialYear !== null && this.SelectedFinancialYear !== '' && this.SelectedFinancialYear !== undefined
      && this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
      && this.SelectedCropCatagory !== null && this.SelectedCropCatagory !== '' && this.SelectedCropCatagory !== undefined
      && this.SelectedDistrict !== null && this.SelectedDistrict !== '' && this.SelectedDistrict !== undefined
      && this.SelectedCrop !== null && this.SelectedCrop !== '' && this.SelectedCrop !== undefined) {
      this.spinner.show();
      let data = {
        SelectedFinancialYear: this.SelectedFinancialYear,
        SelectedSeason: this.SelectedSeason,
        SelectedCropCatagory: this.SelectedCropCatagory,
        SelectedCrop: this.SelectedCrop,
        selectedToDate: this.selectedToDate,
        SelectedDistrict: this.SelectedDistrict
      }
      this.service.fillStateStockPosition(data).subscribe(data => {
        // console.log(data);
        this.stateStockPositionData = data;

        this.stateStockPositionData.forEach((i: any) => {
        
            var a = (i.OSSC_SaleDealer == undefined || i.OSSC_SaleDealer == null || i.OSSC_SaleDealer == '') ? 0 : i.OSSC_SaleDealer;
            var b = (i.OSSC_SalePacks == undefined || i.OSSC_SalePacks == null || i.OSSC_SalePacks == '') ? 0 : i.OSSC_SalePacks;
            var c = (i.OSSC_GtransOwnTrPend == undefined || i.OSSC_GtransOwnTrPend == null || i.OSSC_GtransOwnTrPend == '') ? 0 : i.OSSC_GtransOwnTrPend;
            var d = (i.OSSC_OthrGtransOwnTrPend == undefined || i.OSSC_OthrGtransOwnTrPend == null || i.OSSC_OthrGtransOwnTrPend == '') ? 0 : i.OSSC_OthrGtransOwnTrPend;
            var e = (i.OAIC_SalePacks == undefined || i.OAIC_SalePacks == null || i.OAIC_SalePacks == '') ? 0 : i.OAIC_SalePacks;
            var f = (i.OSSC_Stock == undefined || i.OSSC_Stock == null || i.OSSC_Stock == '') ? 0 : i.OSSC_Stock;
            var g = (i.OAIC_Stock == undefined || i.OAIC_Stock == null || i.OAIC_Stock == '') ? 0 : i.OAIC_Stock;

console.log(i.OSSC_GtransOwnTrPend,i.OSSC_OthrGtransOwnTrPend);

            
            // i.OSSC_GtransOwnTrPend=parseFloat(c);
            // i.OSSC_OthrGtransOwnTrPend=parseFloat(d);

            i.OSSC_SaleDealerOSSC_SalePacks = parseFloat(a) + parseFloat(b);
            i.OSSC_GtransOwnTrPendOSSC_OthrGtransOwnTrPend = parseFloat(c) + parseFloat(d);
            i.OAIC_SalePacksOSSC_SalePacks=parseFloat(e)+parseFloat(b);
            i.OAIC_SalePacksOSSC_SalePacksOSSC_SaleDealer=parseFloat(e)+parseFloat(b)+parseFloat(a);
            i.OSSC_StockOAIC_Stock=parseFloat(f)+parseFloat(g);
         
        })

          this.showpage = true;
        this.spinner.hide();
        // this.getAllCrop = data;
      })
    }
    else {
      this.toastr.warning('Please select all field.');
    }
  }
}