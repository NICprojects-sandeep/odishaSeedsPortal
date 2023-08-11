import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';

@Component({
  selector: 'app-chart-variety-list',
  templateUrl: './chart-variety-list.component.html',
  styleUrls: ['./chart-variety-list.component.css']
})
export class ChartVarietyListComponent implements OnInit {
  Variety_Name:any;
  Variety_Code:any;
  VarietyDtls = [];
  total=0;    
  private value;
  public spinner=false;
  constructor(private service: DashboardService) { }

  ngOnInit(): void {
     this.spinner=true;
    this.Variety_Name=localStorage.getItem('Variety_Name');    
    // this.service.GraphVarietiesList(this.Variety_Name).subscribe(data => {
    //   this.VarietyDtls = data;
    //   this.findsum(this.VarietyDtls);
    //   this.spinner=false;
    //   console.log(this.VarietyDtls);
    // });
  }
  
  findsum(data){    
    debugger  
    this.value=data    
    console.log(this.value);  
    for(let j=0;j<data.length;j++){   
         this.total+= this.value[j].STOCK_QTY         
         console.log(this.total)  
    }  
  } 

}
