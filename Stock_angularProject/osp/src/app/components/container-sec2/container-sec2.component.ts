import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-container-sec2',
  templateUrl: './container-sec2.component.html',
  styleUrls: ['./container-sec2.component.css']
})
export class ContainerSec2Component implements OnInit {
  ContentO : string ='';
  ContentE : string ='';
  constructor(private router: Router,private service : DashboardService) { }

  ngOnInit(): void {

    // this.service.fillContentO().subscribe(data => {
    //   this.ContentO=data;
    // });
    
    // this.service.fillContentE().subscribe(data => {
    //   this.ContentE=data;
    // });
  }

}
