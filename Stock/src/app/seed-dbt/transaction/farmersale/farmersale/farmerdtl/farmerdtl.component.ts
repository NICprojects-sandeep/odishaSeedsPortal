import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { Location } from '@angular/common';
import { FarmersaleComponent } from 'src/app/farmersale/farmersale.component';

@Component({
  selector: 'app-farmerdtl',
  templateUrl: './farmerdtl.component.html',
  styleUrls: ['./farmerdtl.component.css']
})
export class FarmerdtlComponent implements OnInit {
  FarmerCropInfo:any;  
  _AddLot:any=[];
  public show : boolean = true;

  constructor(private router: Router,private service:FarmersaleService, private location : Location) { }
  @Input() farmerid: any;
  @Input() finyr:any;
  @Input() season:any;
  ngOnInit(): void {
    
  }
  ngOnChanges(){
    this.GetFarmerStock();
    this.getLot();
   }   

  GetFarmerStock(){
    this.service.GetFarmerRecvCrop(this.farmerid,this.finyr,this.season).subscribe(data=>{
      this.FarmerCropInfo=data;
      });
  }
  async getLot()
  {
    this._AddLot=await this.service.getAddLot();
    console.log(this._AddLot);    
  }  
}
