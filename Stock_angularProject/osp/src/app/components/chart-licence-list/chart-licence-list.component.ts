import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-chart-licence-list',
  templateUrl: './chart-licence-list.component.html',
  styleUrls: ['./chart-licence-list.component.css']
})
export class ChartLicenceListComponent implements OnInit {
  Farm_Name:any;
  constructor() { }

  ngOnInit(): void {
    this.Farm_Name=localStorage.getItem('FarmName');
  }

}
