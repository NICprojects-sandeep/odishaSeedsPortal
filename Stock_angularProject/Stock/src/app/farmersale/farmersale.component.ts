import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FarmersaleService } from '../farmersale.service';
import { FormControl } from '@angular/forms';


@Component({
  selector: 'app-farmersale',
  templateUrl: './farmersale.component.html',
  styleUrls: ['./farmersale.component.css']
})
export class FarmersaleComponent implements OnInit {
  FarmerPerDtls : any;
  AccessMode:any='0';
  RefNo:any='ODSON1/2014-15/0001/E';
  LicNo:any='ODBAL1/2014-15/0010';
  FarmerIdPre:any;
  FarmerId:any;
  FarmerName:any;
  FatherName:any;
  Category:any;
  Gender:any;
  FarmerCropInfo:any;
  FinYr:any='2021-22';
  Season:any='R';
  constructor(private router: Router,private service:FarmersaleService ) {}

  ngOnInit(): void {
    
    this.service.GetDistCodeFromLicNo(this.LicNo).subscribe(data =>{
this.FarmerIdPre=data[0].SHORT_NAME;
    });
  }
  getFarmerPerDtl()
  {
    var num1 = ((document.getElementById("farmerid") as HTMLInputElement).value);
    if(num1 == '')
    {
      alert("Farmer ID Can not be Blank");
    }
    else
    {
      this.FarmerId=this.FarmerIdPre+'/'+num1.toString();
      this.service.GetFarmerInfo(this.FarmerId).subscribe(data=>{
      this.FarmerName=data[0].VCHFARMERNAME;
      this.FatherName=data[0].VCHFARMERNAME;
      this.Category=data[0].Category_Value;
      this.Gender=data[0].Gender_Value;
      });

      this.service.GetFarmerRecvCrop(this.FarmerId,this.FinYr,this.Season).subscribe(data=>{
        this.FarmerCropInfo=data;        
      })
      // alert(this.FarmerId.toString());      
    }
  }
}
