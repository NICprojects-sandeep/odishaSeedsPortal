import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { NewsComponent } from 'src/app/components/news/news.component';
import { ContainerSec1Component } from 'src/app/components/container-sec1/container-sec1.component';
import { ContainerSec2Component } from 'src/app/components/container-sec2/container-sec2.component';
import { ContainerSec3Component } from 'src/app/components/container-sec3/container-sec3.component';
import { ContainerSec4Component } from 'src/app/components/container-sec4/container-sec4.component';
import { ContainerSec5Component } from 'src/app/components/container-sec5/container-sec5.component';
import { ContainerSec6Component } from 'src/app/components/container-sec6/container-sec6.component';

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
  // NewsO : string ='';
  // NewsE : string ='';
  // ContentO : string ='';
  // ContentE : string ='';
  constructor(private router: Router,private service : DashboardService) { }

  ngOnInit(): void {
    this.service.fillLogoDetailsO().subscribe(data => {
      this.LogoTextO=data[0].LogoText;
    });

    this.service.fillLogoDetailsE().subscribe(data => {
      this.LogoTextE=data[0].LogoText;
    });

    this.service.fillMenuO().subscribe(data => {
      this.MenuTextO=data;      
    });

    this.service.fillMenuE().subscribe(data => {      
      this.MenuTextE=data;      
    });

    this.service.fillSubMenuO().subscribe(data => {      
      this.SubMenuTextO=data;      
    });    

    this.service.fillSubMenuE().subscribe(data => {            
      this.SubMenuTextE=data;      
      console.log(this.SubMenuTextE);
    });

  }; 
  
}
