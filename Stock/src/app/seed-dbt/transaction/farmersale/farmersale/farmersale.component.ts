import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { FormControl } from '@angular/forms';

@Component({
  selector: 'app-farmersale',
  templateUrl: './farmersale.component.html',
  styleUrls: ['./farmersale.component.css']
})
export class FarmersaleComponent implements OnInit {
  UserId    : any;
  Firstname : any;
  Lastname  : any;
  Email     : any;
  FarmerPerDtls : any;
  AccessMode:any='0';
  RefNo:any='ODSON1/2014-15/0001/E';
  LicNo:any='ODBAL1/2014-15/0013';
  FarmerIdPre:any;
  FarmerId:any;
  FarmerName:any;
  FatherName:any;
  Category:any;
  Gender:any;
  MobileNo:any;
  FarmerCropInfo:any;
  FinYr:any='2021-22';
  SeasonFullName:any;
  Season:any='R';
  farmerinfo: boolean = false;
  farmerdtl: boolean = false;
  FarmerSale:boolean=true;
  dealerstockdtl:boolean=false;
  hide: boolean = false;
  next:boolean=false;
  constructor(private router: Router,private service:FarmersaleService ) { }

  ngOnInit(): void {
    this.FillFinYear();
    this.service.GetDistCodeFromLicNo(this.LicNo).subscribe(data =>{
      this.FarmerIdPre=data[0].SHORT_NAME;
          });
        }
        getFarmerPerDtl()
        {
         
          this.farmerinfo=false;
          this.farmerdtl=false; 

          var num1 = ((document.getElementById("farmerid") as HTMLInputElement).value);
          if(num1 == '')
          {
            alert("Farmer ID Can not be Blank");
          }
          else
          {

            this.FarmerId=this.FarmerIdPre+'/'+num1.toString();
            this.farmerinfo=true;
            this.farmerdtl=true;
            this.FarmerSale=false;
            this.hide = true;
            this.next=true;
            // this.service.GetFarmerInfo(this.FarmerId).subscribe(data=>{
            // this.FarmerName=data[0].VCHFARMERNAME;
            // this.FatherName=data[0].VCHFARMERNAME;
            // this.Category=data[0].Category_Value;
            // this.Gender=data[0].Gender_Value;
            // this.MobileNo=data[0].STARMOBILENO;
            // });
      
            // this.service.GetFarmerRecvCrop(this.FarmerId,this.FinYr,this.Season).subscribe(data=>{
            //   this.FarmerCropInfo=data;        
            // });
            // alert(this.FarmerId.toString());      
          }
        }
        FillFinYear = async() => {
          const result = await this.service.FillFinYr('T').toPromise()
          this.FinYr=result[0].FIN_YR;     
          this.FillSeason();
        } 
      FillSeason()
        {
          this.service.FillSeason(this.FinYr,'T').subscribe(data=>{
            this.Season=data[0].SHORT_NAME; 
            this.SeasonFullName=data[0].SEASSION_NAME 
        });
    }
    ShowHide() {
      this.FarmerSale= true;
      this.farmerinfo = false;
      this.farmerdtl = false;
      this.hide=false;
      this.next=false;
   }
   ShowNext()
   {
     this.hide=false;
     this.next=false;
     this.dealerstockdtl=true;

   }
   
  }
  
      

