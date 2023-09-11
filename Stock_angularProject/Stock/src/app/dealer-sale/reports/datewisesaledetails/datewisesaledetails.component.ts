import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';

@Component({
  selector: 'app-datewisesaledetails',
  templateUrl: './datewisesaledetails.component.html',
  styleUrls: ['./datewisesaledetails.component.css']
})
export class DatewisesaledetailsComponent implements OnInit {
  maxdate: any;
  mindate: any;
  selectedFromDate: any = '';
  selectedToDate: any = '';
  SelectedGodown: any = '';
  SelectedSeason: any = '';
  getAllGodown:any=[];
  getAlldateWiseSaleDetails:any=[];
  showpage:boolean=false;
  sumSALE_NO_OF_BAG:any=0;
  sumOfSale_Quantity:any=0.00;
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
  dateWiseSaleDetails(){
    this.showpage=false;
    if (this.SelectedSeason !== null && this.SelectedSeason !== '' && this.SelectedSeason !== undefined
    && this.SelectedGodown !== null && this.SelectedGodown !== '' && this.SelectedGodown !== undefined
    && this.selectedFromDate !== null && this.selectedFromDate !== '' && this.selectedFromDate !== undefined
    && this.selectedToDate !== null && this.selectedToDate !== '' && this.selectedToDate !== undefined) {
      this.sumSALE_NO_OF_BAG=0;
      this.sumOfSale_Quantity=0.00;
      this.spinner.show();
      let data = {
        SelectedSeason: this.SelectedSeason,
        SelectedGodown: this.SelectedGodown,
        selectedFromDate: this.selectedFromDate,
        selectedToDate: this.selectedToDate

      }
    this.service.dateWiseSaleDetails(data).subscribe(data => {
      this.getAlldateWiseSaleDetails = data;
      this.getAlldateWiseSaleDetails.forEach((i: any) => {
        if (i.hasOwnProperty('SALE_NO_OF_BAG')) {
          var n = (i.SALE_NO_OF_BAG == undefined || i.SALE_NO_OF_BAG == null || i.SALE_NO_OF_BAG == '') ? 0 : i.SALE_NO_OF_BAG;
          this.sumSALE_NO_OF_BAG = (this.sumSALE_NO_OF_BAG) + parseInt(n);
        }
        if (i.hasOwnProperty('Sale_Quantity')) {
          var n = (i.Sale_Quantity == undefined || i.Sale_Quantity == null || i.Sale_Quantity == '') ? 0 : i.Sale_Quantity;
          this.sumOfSale_Quantity = (this.sumOfSale_Quantity) + parseFloat(n);
        }
      }),
      this.showpage=true;
      this.spinner.hide();
    })
  }
  else{
    this.toastr.warning('Please select all field.');
  }
  }
}
