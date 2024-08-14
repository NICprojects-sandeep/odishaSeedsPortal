import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../dashboard.service';
@Component({
  selector: 'app-varietysearch',
  templateUrl: './varietysearch.component.html',
  styleUrls: ['./varietysearch.component.css']
})
export class VarietysearchComponent implements OnInit {
  selectedDistrict: any = '';
  selectedCrop: any = '';
  selectedVariety: any = '';
  districtList:any=[];
  cropList:any=[];
  varietyList:any=[];
  varietysearchlist: any = [];
  showpage:boolean=false;
  constructor(
    private service: DashboardService,
  ) { }

  ngOnInit(): void {
    this.getDistrict();
    this.getcrop();
  }
  getDistrict() {
    this.service.getDistrict().subscribe(async result => {
      this.districtList = result;
    }, err => console.log(err));
  }
  getcrop() {
    this.service.getcrop().subscribe(data => {
      this.cropList = data;
    })
  }
  getvariety() {
    console.log(this.selectedCrop);
    
    this.service.getvariety(this.selectedCrop).subscribe(data => {
      this.varietyList = data;
    })
  }
  getVarietySearch() {
    this.varietysearchlist = [];
    if (this.selectedDistrict != null && this.selectedDistrict != undefined && this.selectedDistrict != '' &&
      this.selectedCrop != null && this.selectedCrop != undefined && this.selectedCrop != '' &&
      this.selectedVariety != null && this.selectedVariety != undefined && this.selectedVariety != '' 
    ){
      this.service.getVarietySearch(this.selectedDistrict,this.selectedCrop,this.selectedVariety).subscribe(async result => {
        this.varietysearchlist = result;
        this.showpage=true;
        console.log(this.varietysearchlist);
        
      })
    }
    else {
      alert('Please select all field')
    }
  }
}
