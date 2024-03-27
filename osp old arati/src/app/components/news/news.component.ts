import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router'; 

@Component({
  selector: 'app-news',
  templateUrl: './news.component.html',
  styleUrls: ['./news.component.css']
})
export class NewsComponent implements OnInit {
  NewsO : string="";
  NewsE : string="";
  constructor(private router: Router,private service : DashboardService) { }

  ngOnInit(): void {
    
    
    // this.service.fillNewsO().subscribe(data => {
    //   this.NewsO=data;
    // });
   
    // this.service.fillNewsE().subscribe(data => {
    //   this.NewsE=data;
    // });
  }

}
