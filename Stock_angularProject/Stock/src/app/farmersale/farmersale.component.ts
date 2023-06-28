import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FarmersaleService } from '../farmersale.service';
import { FormControl } from '@angular/forms';


@Component({
  selector: 'app-farmersale',
  templateUrl: './farmersale.component.html',
  styleUrls: ['./farmersale.component.css']
})
export class FarmersaleComponent implements OnInit {
  FarmerPerDtls : any;
  AccessMode:any='0';
  RefNo:any='ODSON1/2014-15/0001/E';
  LicNo:any='ODBAL1/2014-15/0010';
  FarmerIdPre:any;
  FarmerId:any;
  FarmerName:any;
  FatherName:any;
  Category:any;
  Gender:any;
  FarmerCropInfo:any;
  FinYr:any='2021-22';
  Season:any='R';
  constructor(private router: Router,private service:FarmersaleService ) {}

  ngOnInit(): void {
 
  }
}
