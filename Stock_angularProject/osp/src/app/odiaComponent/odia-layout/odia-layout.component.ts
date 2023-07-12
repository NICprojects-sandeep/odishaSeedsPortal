import { Component, OnInit } from '@angular/core';

import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-odia-layout',
  templateUrl: './odia-layout.component.html',
  styleUrls: ['./odia-layout.component.css']
})
export class OdiaLayoutComponent implements OnInit {
  title : string = 'osp';
  LogoTextO : string ='';
  LogoTextE : string ='';
  MenuTextO : string ='';  
  MenuTextE : string ='';
  SubMenuTextO : string ='';
  SubMenuTextE : string ='';
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
