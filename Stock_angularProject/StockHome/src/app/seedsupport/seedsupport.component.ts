import { Component, Inject, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router';
import Chart from 'chart.js/auto';
import { MatDialog } from '@angular/material/dialog';
import { DialougeboxComponent } from '../dialougebox/dialougebox.component';
import { ModalContentComponent } from 'src/app/modal-content/modal-content.component';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';


@Component({
  selector: 'app-seedsupport',
  templateUrl: './seedsupport.component.html',
  styleUrls: ['./seedsupport.component.css']
})
export class SeedsupportComponent {
  Varieties: any;
  VarietyNames = [];
  StockValues = [];
  districtList: any;
  dealers: any = [];
  selectedDistrict: any;
  DistrictCode: any;
  blockList: any;
  distCode: any;
  selectedBlock: any = '';
  cropListValue: any = [];
  graphVarietyList: any = [];
  myChart: any = '';
  constructor(
    private router: Router,
    private service: DashboardService,
    public dialog: MatDialog,
    private modalService: NgbModal
  ) {
    this.selectedDistrict = '';
  }


  ngOnInit(): void {
    this.getDistrict();
  }
  // openDocumentsDilog() {
  //   const dialogRef = this.dialog.open(DialougeboxComponent, {
  //     width: '600px',
  //     data : {dealers : this.dealers, blockList :this.blockList },
  //   });
  // }

  // openDocumentsDilog() {
  //   console.log('Opening dialog...');
  //   console.log('this.dealers:', this.dealers);
  //   console.log('this.blockList:', this.blockList);

  //   const dialogRef = this.dialog.open(DialougeboxComponent, {
  //     width: '600px',
  //     data: { dealers: this.dealers, blockList: this.blockList },
  //   });

  // }
  openModal() {
    const modalRef = this.modalService.open(ModalContentComponent);
  }

  getDistrict() {
    this.service.getDistrict().subscribe(async result => {
      this.districtList = result;
    }, err => console.log(err));
  }

  getBlock() {
    this.dealers = [];
    this.blockList = [];
    this.selectedBlock = ''
    this.distCode = this.selectedDistrict.LGDistrict;
    
    this.blockWiseDealer();
    console.log(this.selectedBlock.block_code,'hhhhh');
    this.service.getBlock(this.distCode).subscribe(async result => {
      this.blockList = result;
    }, err => console.log(err));
  }
  dealerList() {
    this.DistrictCode = this.selectedDistrict.LGDistrict;
    this.service.getDealerDetails(this.DistrictCode).subscribe(async result => {
      this.dealers = result;
    }, err => console.log(err));
    // this.openDocumentsDilog();
  }
  title = 'appBootstrap';

  closeResult: string | undefined;


  open(content: any) {
    //  const modalRef = this.modalService.open(ModelComponent, { size: 'lg', backdrop: 'static' });
    this.modalService.open(content, { size: 'lg', backdrop: 'static' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason(reason)}`;
    });
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
  blockWiseDealer() {
    // console.log(this.selectedBlock.block_code);
    this.dealers = [];
    this.service.getblockWiseDealer(this.selectedBlock.block_code,this.selectedDistrict.LGDistrict).subscribe(async result => {
      this.dealers = result;
      console.log(this.dealers);

      // console.log(this.dealers);
    }, err => console.log(err));
  }


  openDocumentsDilog(content: any) {
    
    //  const modalRef = this.modalService.open(ModelComponent, { size: 'lg', backdrop: 'static' });
    this.modalService.open(content, { size: 'lg', backdrop: 'static' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason1(reason)}`;
    });
  }

  private getDismissReason1(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }

  cropList() {
    this.cropListValue = [];
    this.service.getcropList().subscribe(async result => {
      this.cropListValue = result;
    }, err => console.log(err));
  }
  onViewClick(y: any) {

  }
  // graphVariety(y:any) {
  //   this.service.graphVariety(y).subscribe(async result => {
  //     this.graphVarietyList = result;
  //     console.log(this.graphVarietyList);
  //     const labels =  this.graphVarietyList.map(item => item.Dist_Name);
  //     const avlQtys = this.graphVarietyList.map(item => parseFloat(item.avlQtyInQTL));
  //     // const distNames = this.graphVarietyList.map(item => item.Dist_Name);
  //     const ctx = (document.getElementById('myChart') as HTMLCanvasElement).getContext('2d');
  //     if (Chart.instances[0]) {
  //       Chart.instances[0].destroy();
  //   }
  //   if (this.myChart) this.myChart.destroy();
  //     this.myChart = new Chart(ctx, {
  //         type: 'bar',
  //         data: {
  //             labels: labels,
  //             datasets: [{
  //                 label: `Available Quantity`,
  //                 data: avlQtys,
  //                 backgroundColor: 'rgba(75, 192, 192, 0.2)',
  //                 borderColor: 'rgba(75, 192, 192, 1)',
  //                 borderWidth: 1
  //             }]
  //         },
  //         options: {
  //             scales: {
  //                 y: {
  //                     beginAtZero: true
  //                 }
  //             },
  //             plugins: {
  //                 title: {
  //                     display: true,
  //                     text: `Available Quantity for Different Varieties in Different Districts`
  //                 }
  //             }
  //         }
  //     });

  //   }, err => console.log(err));
  // }
  graphVariety(y: any) {
    console.log(y);
    this.graphVarietyList=[];
    this.service.graphVariety(y.Crop_ID).subscribe(
      async (result) => {
        this.graphVarietyList = result;
        console.log(this.graphVarietyList);

        // Check if the data is empty or in an unexpected format
        if (!Array.isArray(this.graphVarietyList)) {
          console.error('Data received is not in the expected format.');
          return;
        }

        const labels = this.graphVarietyList.map((item) => item.Dist_Name);
        const avlQtys = this.graphVarietyList.map((item) =>
          parseFloat(item.avlQtyInQTL)
        );

        const canvas = document.getElementById('myChart') as HTMLCanvasElement;

        if (!canvas) {
          console.error('Canvas element with id "myChart" not found.');
          return;
        }

        const ctx = canvas.getContext('2d');

        // Check if the canvas context is available
        if (!ctx) {
          console.error('Could not get canvas 2D context.');
          return;
        }

        if (this.myChart) {
          this.myChart.destroy();
        }

        this.myChart = new Chart(ctx, {
          type: 'bar',
          data: {
            labels: labels,
            datasets: [
              {
                label: 'Available Quantity',
                data: avlQtys,
                backgroundColor: 'rgba(75, 192, 192, 0.2)',
                borderColor: 'rgba(75, 192, 192, 1)',
                borderWidth: 1,
              },
            ],
          },
          options: {
            scales: {
              y: {
                beginAtZero: true,
              },
            },
            plugins: {
              title: {
                display: true,
                text: 'Available Quantity for Different Varieties in Different Districts',
              },
            },
          },
        });
      },
      (err) => {
        console.error('An error occurred:', err);
      }
    );
  }

  openGraphmodal(content: any) {
    //  const modalRef = this.modalService.open(ModelComponent, { size: 'lg', backdrop: 'static' });
    this.modalService.open(content, { size: 'lg', backdrop: 'static' }).result.then((result) => {
      this.closeResult = `Closed with: ${result}`;
    }, (reason) => {
      this.closeResult = `Dismissed ${this.getDismissReason2(reason)}`;
    });
  }

  private getDismissReason2(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
