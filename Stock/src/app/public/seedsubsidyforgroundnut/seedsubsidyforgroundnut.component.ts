// import { Component, OnInit } from '@angular/core';
import { PublicService } from 'src/app/Services/public.service';
import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
@Component({
  selector: 'app-seedsubsidyforgroundnut',
  templateUrl: './seedsubsidyforgroundnut.component.html',
  styleUrls: ['./seedsubsidyforgroundnut.component.css']
})
export class SeedsubsidyforgroundnutComponent implements OnInit {
  // SeedsubsidyforgroundnutDetails:Array<any>=[]
  SeedsubsidyforgroundnutDetails: Array<{ FarmerId: string, BagSize: string, noOfBags: string,Quintal: string, Source: string, Amount: string }>=[];

  constructor(private publicservice: PublicService,
    private fb: FormBuilder,
    ) { }

  ngOnInit(): void {
    const x: any = localStorage.getItem("userId");    
    if(x != "undefined" && x != undefined  ){
      this.publicservice.getSeedsubsidyforgroundnut(x).subscribe(data => {
        this.SeedsubsidyforgroundnutDetails= data;
        for (var i = 0; i < this.SeedsubsidyforgroundnutDetails.length; i++) {          
          this.SeedsubsidyforgroundnutDetails[i].Source = this.SeedsubsidyforgroundnutDetails[i].Source.toString().split(' ')[0];
        }
      });
    }
    else{
      window.location.href='https://agrisnetodisha.ori.nic.in/stock/login.aspx'
    }
   
    // this.SeedsubsidyforgroundnutDetails=[{name:'jjjj'},{name:'jjjj1111'}]
    
  }
  

}
