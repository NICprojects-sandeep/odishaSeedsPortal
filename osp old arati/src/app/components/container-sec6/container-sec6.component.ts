import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-container-sec6',
  templateUrl: './container-sec6.component.html',
  styleUrls: ['./container-sec6.component.css']
})
export class ContainerSec6Component implements OnInit {
  Licences: any;
  FarmName = [];
  StockValues = [];
  constructor(private router: Router, private service: DashboardService) { }

  ngOnInit(): void {

    // this.service.Graph10Lic().subscribe(data => {
    //   this.Licences = data;
    //   this.Licences.forEach(element => {
    //     this.FarmName.push(element.APP_FIRMNAME)
    //     this.StockValues.push(element.SOLD)
    //     if (this.Licences.length == this.FarmName.length) {
    //       this.loadchart();
    //     }
    //   });
    // });
  }

  loadchart()
  {
    new Chart("myChart1", {
      type: 'bar',
      data: {
           //labels: ['POOJA (IET-12241)','MTU-7029(SWARNA)','MTU-1001(VIJETHA)','Kalachampa','SWARANA-SUB 1 (CR 2539-1) IET-20266','MTU 1153','Tarangini (MTU 1156)','CIHERANG SUB-1(BINA-11)','SAHABHAGI DHAN','PRATIKSHYA (ORS 201-5)(IET-15191)'],
        labels: this.FarmName,
          datasets: [{
                 label: '',
              data: this.StockValues,
              backgroundColor: [
                  'rgba(56, 255, 0, 0.2)',
                  'rgba(99, 82, 0, 0.2)',
                  'rgba(99, 82, 148, 0.2)',
                  'rgba(224, 82, 148, 0.2)',
                  'rgba(255, 99, 132, 0.2)',
                  'rgba(54, 162, 235, 0.2)',
                  'rgba(255, 206, 86, 0.2)',
                  'rgba(75, 192, 192, 0.2)',
                  'rgba(153, 102, 255, 0.2)',
                  'rgba(255, 159, 64, 0.2)'
              ],
              borderColor: [
                  'rgba(56, 255, 0, 1)',
                  'rgba(99, 82, 0, 1)',
                  'rgba(99, 82, 148, 1)',
                  'rgba(224, 82, 148, 1)',
                  'rgba(255, 99, 132, 1)',
                  'rgba(54, 162, 235, 1)',
                  'rgba(255, 206, 86, 1)',
                  'rgba(75, 192, 192, 1)',
                  'rgba(153, 102, 255, 1)',
                  'rgba(255, 159, 64, 1)'
              ],
              borderWidth: 1
          }]
      },
      options: {
          scales: {
              y: {
                  beginAtZero: true
              }
          },
          onClick: function (context: any) {
            if (context.chart.tooltip.title[0] != 'undefined') {
              localStorage.setItem('FarmName',context.chart.tooltip.title[0]);
              window.location.href = "/chartLicence";
            }
            else {
              console.log("NA");
            }            
          }
      }
  });
  }
}
