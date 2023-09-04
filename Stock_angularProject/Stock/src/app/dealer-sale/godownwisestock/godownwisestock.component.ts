import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';
// import { Enumerable } from 'linq';
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
  godownwiseStockData:any=[];
  abcdata:any=[];
  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.FillFinYr();
    this.FillCropCategory();
  }
  FillFinYr(){
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
  FillCropByCategoryId(){
    this.getAllCrop = []
    this.service.FillCropByCategoryId(this.SelectedCropCatagory).subscribe(data => {
      this.getAllCrop = data;
    })
  }
  fillGodownwisestock() {
    this.godownwiseStockData = []
    this.service.fillGodownwisestock(this.SelectedFinancialYear,this.SelectedSeason,this.SelectedCropCatagory,this.SelectedCrop).subscribe(data => {
      this.godownwiseStockData = data;
      console.log(data);
      // var margeList = Enumerable.From(this.godownwiseStockData)
      // .GroupBy(function (item:any) { return item.LICENCE_NO })
      // .Select(function (item:any) {
      //     return item.source;
      // })
      // .ToArray();

  // this.abcdata = margeList;

      
    })
  }
}
