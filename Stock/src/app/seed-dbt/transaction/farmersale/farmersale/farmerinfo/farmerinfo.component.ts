import { Component, OnInit,Input } from '@angular/core';
import { Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';

@Component({
  selector: 'app-farmerinfo',
  templateUrl: './farmerinfo.component.html',
  styleUrls: ['./farmerinfo.component.css']
})
export class FarmerinfoComponent implements OnInit {
  Farmer_Id:any;
  FarmerName:any;
  FatherName:any;
  Category:any;
  Gender:any;
  MobileNo:any;

  constructor(private router: Router,private service:FarmersaleService) { }
@Input() farmerid1: any;
  ngOnInit(): void {   
   }

   ngOnChanges(){
    this.GetFarmer()
   }
        
        GetFarmer(){
          console.log(this.farmerid1);
          
          this.service.GetFarmerInfo(this.farmerid1).subscribe(data=>{

            this.Farmer_Id=data[0].NICFARMERID;
            this.FarmerName=data[0].VCHFARMERNAME;
            this.FatherName=data[0].VCHFARMERNAME;
            this.Category=data[0].Category_Value;
            this.Gender=data[0].Gender_Value;
            this.MobileNo=data[0].STARMOBILENO;
            });
        }

      }
