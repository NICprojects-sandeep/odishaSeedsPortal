import { Component, Inject, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router';
import Chart from 'chart.js/auto';
import { MatDialog} from '@angular/material/dialog';
import { DialougeboxComponent } from '../dialougebox/dialougebox.component';
import { ModalContentComponent } from 'src/app/modal-content/modal-content.component';
import {  NgbModal } from '@ng-bootstrap/ng-bootstrap';



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
  dealers: any;
  selectedDistrict : any;
  DistrictCode: any;
  blockList: any;
  distCode: any;
  constructor(
    private router: Router,
    private service: DashboardService,
    public dialog: MatDialog,
    private modalService: NgbModal
  ){
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

    openDocumentsDilog() {
      console.log('Opening dialog...');
      console.log('this.dealers:', this.dealers);
      console.log('this.blockList:', this.blockList);
    
      const dialogRef = this.dialog.open(DialougeboxComponent, {
        width: '600px',
        data: { dealers: this.dealers, blockList: this.blockList },
      });

    }
    openModal() {
      const modalRef = this.modalService.open(ModalContentComponent);
    }
      
    getDistrict(){
      this.service.getDistrict().subscribe(async result => {
        this.districtList = result;
      }, err => console.log(err));
    }

    getBlock(){
      this.distCode = this.selectedDistrict.LGDistrict
      this.service.getBlock(this.distCode).subscribe(async result => {
        this.blockList = result;
      }, err => console.log(err));
    }
    dealerList(){
      this.DistrictCode = this.selectedDistrict.LGDistrict;
        this.service.getDealerDetails(this.DistrictCode).subscribe(async result => {
          this.dealers = result;
        }, err => console.log(err));
        this.openDocumentsDilog();
      }
     
}
