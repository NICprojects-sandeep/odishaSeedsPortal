import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';

@Component({
  selector: 'app-receivedetails',
  templateUrl: './receivedetails.component.html',
  styleUrls: ['./receivedetails.component.css']
})
export class ReceivedetailsComponent implements OnInit {
  maxdate: any;
  mindate: any;
  selectedFromDate: any = '';
  selectedToDate: any = '';
  SelectedGodown: any = '';
  SelectedSeason: any = '';
  getAllGodown:any=[];
  getAllReceivedetails:any=[];
  showpage:boolean=false;
  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService, private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.maxdate = this.getDate();
    this.FILL_GODOWN();
  }
  mindatecal() {
    this.selectedToDate = '';
    const today = new Date(this.selectedFromDate);
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    this.mindate = year + '-' + month + '-' + day;
    console.log(this.mindate);

  }
  private getDate(): string {
    const today = new Date();
    const year = today.getFullYear();
    const month = String(today.getMonth() + 1).padStart(2, '0');
    const day = String(today.getDate()).padStart(2, '0');
    return `${year}-${month}-${day}`;
  }
  FILL_GODOWN() {
    this.getAllGodown = []
    this.service.FILL_GODOWN('no').subscribe(data => {
      this.getAllGodown = data;
    })
  }
  receivedetails(){
    this.showpage=false;
    if (this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
    && this.SelectedGodown !== null && this.SelectedGodown !== '' && this.SelectedGodown !== undefined
    && this.selectedFromDate !== null && this.selectedFromDate !== '' && this.selectedFromDate !== undefined
    && this.selectedToDate !== null && this.selectedToDate !== '' && this.selectedToDate !== undefined) {
      this.spinner.show();
      let data = {
        SelectedSeason: this.SelectedSeason,
        SelectedGodown: this.SelectedGodown,
        selectedFromDate: this.selectedFromDate,
        selectedToDate: this.selectedToDate

      }
    this.service.receivedetails(data).subscribe(data => {
      this.getAllReceivedetails = data;
      this.showpage=true;
      this.spinner.hide();
    })
  }
  else{
    this.toastr.warning('Please select all field.');
  }
  }
}