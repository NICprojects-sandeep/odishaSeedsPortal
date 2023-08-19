import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
@Component({
  selector: 'app-graph-modal',
  templateUrl: './graph-modal.component.html',
  styleUrls: ['./graph-modal.component.css']
})
export class GraphModalComponent implements OnInit {
  @Input() data: any;
  myChart: any = '';
  constructor(
    public activeModal: NgbActiveModal,
  ) { }
  ngOnInit(): void {
  
  }
  closeModalGraph() {
    this.activeModal.dismiss('Cross click');
  }

}



