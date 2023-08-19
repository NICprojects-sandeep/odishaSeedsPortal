import { Component, OnInit, Input, Output } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DashboardService } from 'src/app/dashboard.service';
import Chart from 'chart.js/auto';
import { GraphModalComponent } from '../graph-modal/graph-modal.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MatDialog } from '@angular/material/dialog';


@Component({
  selector: 'app-modal-content',
  templateUrl: './modal-content.component.html',
  styleUrls: ['./modal-content.component.css']
})

export class ModalContentComponent implements OnInit {
  cropListValue: any = [];
  graphVarietyList: any;
  labels: any;
  values: any;
  CropID: any;
  selectedItem: any;
  selectedCrop: any;
  myChart: any = '';
  constructor(
    private service: DashboardService,
    public activeModal: NgbActiveModal,
    private modalService: NgbModal,
    public dialog: MatDialog,
  ) { }

  ngOnInit(): void {
    this.cropList();
  }
  cropList() {
    this.service.getcropList().subscribe(async result => {
      this.cropListValue = result;
    }, err => console.log(err));
  }
  closeModal() {
    this.activeModal.dismiss('Cross click');
  }
  graphVariety() {
    this.CropID = this.selectedCrop.Crop_ID;
    this.service.graphVariety(this.CropID).subscribe(async result => {
      this.graphVarietyList = result;
      // console.log(this.graphVarietyList);
      const labels = this.graphVarietyList.map(item => item.Dist_Name);
      const avlQtys = this.graphVarietyList.map(item => parseFloat(item.avlQtyInQTL));
      // const distNames = this.graphVarietyList.map(item => item.Dist_Name);
      const ctx = (document.getElementById('myChart') as HTMLCanvasElement).getContext('2d');
      if (Chart.instances[0]) {
        Chart.instances[0].destroy();
      }
      if (this.myChart) this.myChart.destroy();
      this.myChart = new Chart(ctx, {
        type: 'bar',
        data: {
          labels: labels,
          datasets: [{
            label: `Available Quantity`,
            data: avlQtys,
            backgroundColor: 'rgba(75, 192, 192, 0.2)',
            borderColor: 'rgba(75, 192, 192, 1)',
            borderWidth: 1
          }]
        },
        options: {
          scales: {
            y: {
              beginAtZero: true
            }
          },
          plugins: {
            title: {
              display: true,
              text: `Available Quantity for Different Varieties in Different Districts`
            }
          }
        }
      });
 
    }, err => console.log(err));
  }

  onViewClick(y: any): void {
    // this.activeModal.dismiss('Cross click');
    this.selectedCrop = y;
    this.graphVariety();
  }
}

