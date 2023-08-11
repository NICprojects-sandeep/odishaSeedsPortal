import { Component, OnInit, Input } from '@angular/core';
import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';
import { DashboardService } from 'src/app/dashboard.service';
@Component({
  selector: 'app-modal-content',
  templateUrl: './modal-content.component.html',
  styleUrls: ['./modal-content.component.css']
})
export class ModalContentComponent implements OnInit {
  @Input() name: string;
  constructor(
    private service: DashboardService,
    public activeModal: NgbActiveModal) { }

  ngOnInit(): void {
    this.cropList();
  }
  cropList() {
    this.service.getcropList().subscribe(async result => {
      this.cropList = result;
      console.log(this.cropList);
    }, err => console.log(err));
  }
  closeModal() {
    this.activeModal.dismiss('Cross click');
  }
}
