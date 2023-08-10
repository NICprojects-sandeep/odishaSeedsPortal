import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  Variety_Name:any;
  title: string = 'osp';
  LogoTextO: string = '';
  LogoTextE: string = '';
  MenuTextO: string = '';
  MenuTextE: string = '';
  SubMenuTextO: string = '';
  SubMenuTextE: string = '';
  constructor() { }

  ngOnInit(): void {
    this.Variety_Name=localStorage.getItem('Variety_Name');
    this.Variety_Name=localStorage.getItem('FarmName');
    console.log(this.Variety_Name);
  }

}
