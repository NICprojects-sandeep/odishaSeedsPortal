import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {
  title : string = 'osp';
  LogoTextO : string ='';
  LogoTextE : string ='';
  MenuTextO : string ='';  
  MenuTextE : string ='';
  SubMenuTextO : string ='';
  SubMenuTextE : string ='';
  priceList: any;
  error: any;
  // NewsO : string ='';
  // NewsE : string ='';
  // ContentO : string ='';
  // ContentE : string ='';
  constructor(private router: Router,
    private service : DashboardService,
    ) { }

  ngOnInit(): void {
    // this.service.fillLogoDetailsO().subscribe(data => {
    //   this.LogoTextO=data[0].LogoText;
    // });

    // this.service.fillLogoDetailsE().subscribe(data => {
    //   this.LogoTextE=data[0].LogoText;
    // });

    // this.service.fillMenuO().subscribe(data => {
    //   this.MenuTextO=data;      
    // });

    // this.service.fillMenuE().subscribe(data => {      
    //   this.MenuTextE=data;      
    // });

    // this.service.fillSubMenuO().subscribe(data => {      
    //   this.SubMenuTextO=data;      
    // });    

    // this.service.fillSubMenuE().subscribe(data => {            
    //   this.SubMenuTextE=data;      
    //   console.log(this.SubMenuTextE);
    // });
    this.seedPrice();

  }; 
  seedPrice(){
    this.service.getStockPricelist().subscribe(result => {            
      this.priceList=result;      
      console.log(this.priceList);
    },err => console.log(err));
  }
}
