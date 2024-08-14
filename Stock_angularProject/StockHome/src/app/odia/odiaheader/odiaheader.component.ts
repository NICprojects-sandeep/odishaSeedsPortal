import { Component, Inject, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-odiaheader',
  templateUrl: './odiaheader.component.html',
  styleUrls: ['./odiaheader.component.css']
})
export class OdiaheaderComponent implements OnInit {
  fillnewsList: any = [];
  constructor(
    private router: Router,
    private service: DashboardService,
  ) {
  }
  ngOnInit(): void {
    this.fillnews();
  }
  fillnews() {
    this.service.fillnews().subscribe(async result => {
      this.fillnewsList = result;
      console.log(this.fillnewsList);

    }, err => console.log(err));
  }
  gotoMainContent() {
    document.getElementById('mainContent')!.scrollIntoView({
      behavior: 'smooth',
      block: 'start',
      inline: 'nearest'
    });
  }
  homepage() {
    var path = window.location.href.replace(/(\#.*)/, '#/odia');
    console.log(path);
    
    window.location.href = path;
  }}