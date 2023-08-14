import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DashboardService } from 'src/app/dashboard.service';
import Chart from 'chart.js/auto';
@Component({
  selector: 'app-modal-content',
  templateUrl: './modal-content.component.html',
  styleUrls: ['./modal-content.component.css']
})
export class ModalContentComponent implements OnInit {
  @Input() name: string;
  cropListValue : any = [];
  graphVarietyList: any;
  labels: any;
  values: any;
  CropID: any;
  selectedItem: any;
  constructor(
    private service: DashboardService,
    public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.cropList();
   
  }


  cropList() {
    this.service.getcropList().subscribe(async result => {
      this.cropListValue = result;
      // console.log(this.cropListValue);
      // this. graphVariety();
    }, err => console.log(err));
  }
  closeModal() {
    this.activeModal.dismiss('Cross click');
  }
  graphVariety() {
    this.CropID=this.cropListValue.Crop_ID;
    console.log( this.CropID);
    this.service.graphVariety( this.CropID).subscribe(async result => {
      this.graphVarietyList = result;
      // console.log(this.graphVarietyList);
    }, err => console.log(err));
  }
  
}

