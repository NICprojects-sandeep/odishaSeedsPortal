import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-leftsidebar',
  templateUrl: './leftsidebar.component.html',
  styleUrls: ['./leftsidebar.component.css']
})
export class LeftsidebarComponent implements OnInit {
  url:any;
  stringAfter:any;
  stringAfter1:any;
  role:any;
  constructor() { }

  ngOnInit(): void {
    this.url=window.location.href;
    this.stringAfter = this.url.split('#/', 2);
    this.stringAfter1 =this.stringAfter[1].split('/', 2);
    console.log(this.stringAfter1[0]);
    this.role =this.stringAfter1[0]
    
  }

}
