import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';
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
  existingEntry:any={};
  distinctVarietyNames:any=[]
  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

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
  fillGodownwisestock() {
    this.godownwiseStockData = []
    this.service.fillGodownwisestock(this.SelectedFinancialYear, this.SelectedSeason, this.SelectedCropCatagory, this.SelectedCrop).subscribe(data => {
      this.godownwiseStockData = data;
      console.log(data);
      // this.distinctVarietyNames = [...new Set(this.godownwiseStockData.map((item:any) => item.Variety_Name))];
      // this.alldata = [...new Set(this.godownwiseStockData.map((item:any) =>  `${item.STOCK}-${item.Crop_Verid}` ))];
    

    })
  }
}
// const array=
// [{Dist_Code: '01', Dist_Name: 'ANGUL', Crop_Verid: 'V009', Variety_Name: 'KHANDAGIRI', Godown_Name: 'Angul',value:10},
// {Dist_Code: '01', Dist_Name: 'ANGUL', Crop_Verid: 'V011', Variety_Name: 'LALAT (IET-9947)', Godown_Name: 'Angul',value:108},
// {Dist_Code: '01', Dist_Name: 'ANGUL', Crop_Verid: 'V015', Variety_Name: 'MTU 1075 (IET 18482)', Godown_Name: 'Angul',value:102}]


// array1=[{Dist_Code: '01', Dist_Name: 'ANGUL',valueofV009:10,valueofV011:108,valueofV015:102}]
