import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { PublicService } from 'src/app/Services/public.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-incentive',
  templateUrl: './incentive.component.html',
  styleUrls: ['./incentive.component.css']
})
export class IncentiveComponent implements OnInit {

  constructor(private router: Router,
    private service: PublicService,
    private toastr: ToastrService) { }
  selectedFinancialYear: any = '2021-22';
  selectedSeasons: any = 'Rabi';
  incentiveData: any = [];
  sumSUBSIDISED_AREA: any = 0.00;
  sumFARMER_SUBSIDY: any = 0.00;
  sumpOSSC_SUBSIDY: any = 0.00;
  dvOSSC:boolean=false;
  ngOnInit(): void {
    // this.FillIncentiveOilSeed();
  }
  getIncentiveDtl() {
    this.service.GetIncentiveOilSeed().subscribe(data => {
      data[0].selectedFinancialYear = this.selectedFinancialYear;
      data[0].selectedSeasons = this.selectedSeasons;
      console.log(data);
      
    
      this.service.InsertIncentiveOilSeed(data).subscribe(data1 => {
       
        // if (data1.VAL == 1) {
          alert('Transaction Completed!!!');
          // this.FillIncentiveOilSeed();
        // }
      });
    });
  }
  FillIncentiveOilSeed() {
    this.incentiveData = []
    this.service.FillIncentiveOilSeed(this.selectedFinancialYear, this.selectedSeasons).subscribe(data => {
      this.incentiveData = data;
      this.incentiveData.forEach((i: any) => {
        if (i.hasOwnProperty('SUBSIDISED_AREA')) {
          var a = (i.SUBSIDISED_AREA == undefined || i.SUBSIDISED_AREA == null || i.SUBSIDISED_AREA == '') ? 0.00 : i.SUBSIDISED_AREA;
          this.sumSUBSIDISED_AREA = (parseFloat(this.sumSUBSIDISED_AREA) + parseFloat(a)).toFixed(2);
        }
        if (i.hasOwnProperty('FARMER_SUBSIDY')) {
          var b = (i.FARMER_SUBSIDY == undefined || i.FARMER_SUBSIDY == null || i.FARMER_SUBSIDY == '') ? 0.00 : i.FARMER_SUBSIDY;
          this.sumFARMER_SUBSIDY = (parseFloat(this.sumFARMER_SUBSIDY) + parseFloat(b)).toFixed(2);
        }
        if (i.hasOwnProperty('OSSC_SUBSIDY')) {
          var c = (i.OSSC_SUBSIDY == undefined || i.OSSC_SUBSIDY == null || i.OSSC_SUBSIDY == '') ? 0.00 : i.OSSC_SUBSIDY;
          this.sumpOSSC_SUBSIDY = (parseFloat(this.sumpOSSC_SUBSIDY) + parseFloat(c)).toFixed(2);

        }
      })
      this.FillPendingIncentiveOilSeed();
    })
  }
  FillPendingIncentiveOilSeed() {
    this.service.FillPendingIncentiveOilSeed(this.selectedFinancialYear, this.selectedSeasons).subscribe(data => {
     if(data[0].count > 0){
        this.dvOSSC= true;
      }
    })
  }
  btnApprove_Click(){
    const data1={UPDATED_BY:'',FIN_YR:'',SEASON:''};
    data1.UPDATED_BY = 'osscadmin';
    data1.FIN_YR = this.selectedFinancialYear;
    data1.SEASON = this.selectedSeasons;
    this.service.OsscAprvOilSeed(data1).subscribe(data => {
      this.dvOSSC=false;
      alert('Sucessfully Updated');
      this.FillIncentiveOilSeed();
    })
  }
}
