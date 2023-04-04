import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { FarmersaleService } from 'src/app/farmersale.service';
import { PublicService } from 'src/app/Services/public.service';
import { ActivatedRoute } from '@angular/router';

import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-transfer-of-seed-subsidy-for-groundnut',
  templateUrl: './transfer-of-seed-subsidy-for-groundnut.component.html',
  styleUrls: ['./transfer-of-seed-subsidy-for-groundnut.component.css']
})
export class TransferOfSeedSubsidyForGroundnutComponent implements OnInit {
  LicNo: any = 'ODBAL1/2014-15/0010';
  FarmerIdPre: any;
  farmerid: any='';
  bagSize: any = '';
  noOfBags: any = '';
  Quintal: any = '';
  Source: any = '';
  Amount: any = '';
  insertedBy:any=''
  constructor(private router: Router,
     private service: FarmersaleService,
      private publicservice: PublicService,
    private route: ActivatedRoute,
       private toastr: ToastrService,
       ) { }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe((params) => {
        console.log(params.userID);
        this.insertedBy= params.userID;
        localStorage.setItem('userId', params.userID);   
        if(this.insertedBy != "undefined" && this.insertedBy != undefined  ){     
        this.publicservice.getUserDetails(params.userID).subscribe(data => {
          this.FarmerIdPre = data[0].Short_Name;
        });
      }
      else{
        window.location.href='https://agrisnetodisha.ori.nic.in/stock/login.aspx';
        localStorage.removeItem('userId');
      }
      }
      );
    // this.service.GetDistCodeFromLicNo(this.LicNo).subscribe(data => {
    //   this.FarmerIdPre = data[0].SHORT_NAME;
    // });
  }
  quintalChnaged() {
    if (this.bagSize != undefined && this.bagSize != null && this.bagSize != '' && this.noOfBags != undefined && this.noOfBags != null && this.noOfBags != '') {
      this.Quintal = (this.bagSize * this.noOfBags) / 100;
      this.amountChnaged();
    }
  }
  amountChnaged() {
    if (this.Quintal != undefined && this.Quintal != null && this.Quintal != '' && this.Source != undefined && this.Source != null && this.Source != '') {
      this.Amount = (this.Quintal * this.Source);
    }
  }
  submitSeedSubsidyOfGrountnut() {
    if(this.insertedBy != null && this.insertedBy != undefined && this.insertedBy != '' ){
      console.log(this.farmerid);
      const data = {
        farmerid: this.FarmerIdPre +'/'+ this.farmerid,
        bagSize: this.bagSize,
        noOfBags: this.noOfBags,
        Quintal: this.Quintal,
        Source: this.Source,
        Amount: this.Amount,
        insertedBy:this.insertedBy
      }
      this.publicservice.submitSeedSubsidyOfGrountnut(data).subscribe((result: any) => {
        if(result == true){
          alert('')
        this.toastr.success(`Subimitted Sucessfully`);
  
        }
      }
        , (error) => this.toastr.error(error.statusText, error.status)
        // alert(error.statusText)
        );
    }
    else{
      this.toastr.error(`session time out`);
      // alert('session time out')
      // window.location.href='https://agrisnetodisha.ori.nic.in/stock/login.aspx'
    }

   


  }
}
