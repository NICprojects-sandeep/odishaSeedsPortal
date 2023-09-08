import { Component, OnInit } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';

@Component({
  selector: 'app-receive-details',
  templateUrl: './receive-details.component.html',
  styleUrls: ['./receive-details.component.css']
})
export class ReceiveDetailsComponent implements OnInit {
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
// SELECT SR."Godown_ID","RECVTRANSID",  "Challan_No" AS "PR_Chalan", "Lot_No", "Bag_Size_In_kg", "Recv_No_Of_Bags","Recv_Date", "Recv_Quantity",  "EntryDate",sam."AgenciesName",cm."Crop_Name",cv."Variety_Name",sg."Godown_Name"
// from "Stock_ReceiveDetails" SR    
// INNER JOIN "Stock_Agencies_Master" sam on sr."AgenciesID"=sam."AgenciesID"    
// INNER JOIN "mCrop" cm on cm."Crop_Code"=sr."Crop_ID"    
// INNER JOIN "mCropVariety" cv on cv."Variety_Code"=sr."Crop_Verid"    
// INNER JOIN "Stock_Godown_Master" sg on sg."Godown_ID"=sr."Godown_ID"    
// WHERE SR."Dist_Code"='01'    
// AND SR."User_Type"='OSSC'    
// AND "Recv_Date">='2023-05-13'   
// AND "Recv_Date"<='2023-09-13'  
// AND ('0134'=0 OR SR."Godown_ID"='0134')  
// AND ('K' is null or SR."SEASSION_NAME"='K')  
// order by "Recv_Date"   desc,"Challan_No", cm."Crop_Name",cv."Variety_Name"  
