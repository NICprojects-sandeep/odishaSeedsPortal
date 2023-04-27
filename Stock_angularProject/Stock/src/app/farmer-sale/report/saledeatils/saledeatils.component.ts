import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-saledeatils',
  templateUrl: './saledeatils.component.html',
  styleUrls: ['./saledeatils.component.css']
})
export class SaledeatilsComponent implements OnInit {
  LicNo: any = 'ODGAN6/2014-15/0004';
  selectedFromDate: any = '';
  selectedToDate: any = '';
  RptDateWiseSaleDeatails:any=[]
  constructor(private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  RptDateWiseSale() {
    this.RptDateWiseSaleDeatails=[];
    if(this.selectedFromDate != undefined && this.selectedFromDate != null && this.selectedFromDate!='' &&
    this.selectedToDate != undefined && this.selectedToDate != null && this.selectedToDate!=''){
      this.service.RptDateWiseSale(this.selectedFromDate,this.selectedToDate,this.LicNo).subscribe(data => {
        this.RptDateWiseSaleDeatails = data;
      })
    }
    else{
      this.toastr.warning(`Please enter From date & To Date`);
    }
   
  }
}
