import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  Variety_Name:any;
  constructor() { }

  ngOnInit(): void {
    this.Variety_Name=localStorage.getItem('Variety_Name');
    this.Variety_Name=localStorage.getItem('FarmName');
    console.log(this.Variety_Name);
  }

}
