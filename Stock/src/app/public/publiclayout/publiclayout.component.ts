import { Component, OnInit } from '@angular/core';
declare const $: any;
declare function load_Details2(): any;
declare function load_Details1(): any;
declare function loadDetails(): any;


@Component({
  selector: 'app-publiclayout',
  templateUrl: './publiclayout.component.html',
  styleUrls: ['./publiclayout.component.css']
})
export class PubliclayoutComponent implements OnInit {
  pageHeading: string;
  pageHeadingDesc: string;
  breadcrumbList: Array<string>;
  constructor() {
    this.pageHeading = 'Dashboard';
    this.pageHeadingDesc = 'Easy to customise core stylesheets';
    this.breadcrumbList = ['Dashboard'];
  }

  ngOnInit(): void {
    
    setTimeout(() => {
      $('.theme-loader').fadeOut(1000);
      loadDetails();
      load_Details1();
      load_Details2();

    })
  }


}
