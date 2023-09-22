import { Component, Inject, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router';
import { MatDialog} from '@angular/material/dialog';
import { ModalContentComponent } from 'src/app/modal-content/modal-content.component';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { DialougeboxComponent } from 'src/app/components/dialougebox/dialougebox.component';

@Component({
  selector: 'app-support-card',
  templateUrl: './support-card.component.html',
  styleUrls: ['./support-card.component.css']
})
export class SupportCardComponent implements OnInit {
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
  ) { 
    this.selectedDistrict = '';
  }

  ngOnInit(): void {
    this.getDistrict();
  }
  openDocumentsDilog() {
    const dialogRef = this.dialog.open(DialougeboxComponent, {
      // height: '600px',
      width: '600px',
      data : {dealers : this.dealers, blockList :this.blockList },
    });
  }
  openModal() {
    const modalRef = this.modalService.open(ModalContentComponent);
    // modalRef.componentInstance.name = 'John'; // Pass any data to the modal component
  }
    
  getDistrict(){
    this.service.getDistrict().subscribe(async result => {
      this.districtList = result;
    }, err => console.log(err));
  }
  getBlock(){
    // console.log(this.selectedDistrict.LGDistrict);
    this.distCode = this.selectedDistrict.LGDistrict
    this.service.getBlock(this.distCode).subscribe(async result => {
      this.blockList = result;
      // console.log(this.blockList);
    }, err => console.log(err));
  }
  dealerList(){
    this.DistrictCode = this.selectedDistrict.LGDistrict;
    // console.log( this.DistrictCode);
      this.service.getDealerDetails(this.DistrictCode).subscribe(async result => {
        this.dealers = result;
        // console.log(this.dealers);
        this.openDocumentsDilog();
      }, err => console.log(err));
    }
}
