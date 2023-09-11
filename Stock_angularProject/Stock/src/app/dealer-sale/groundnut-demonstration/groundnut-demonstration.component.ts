import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';
@Component({
  selector: 'app-groundnut-demonstration',
  templateUrl: './groundnut-demonstration.component.html',
  styleUrls: ['./groundnut-demonstration.component.css']
})
export class GroundnutDemonstrationComponent implements OnInit {
  LicNo: any = 'ODBAL1/2014-15/0010';
  FarmerIdPre: any;
  farmerid: any = '';
  bagSize: any = '';
  noOfBags: any = '';
  Quintal: any = '';
  Source: any = '';
  Amount: any = '';
  insertedBy: any = ''
  constructor(private router: Router,
    private service: DealerService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
  ) { }

  ngOnInit(): void {
    this.service.GetDistCodeFromDist().subscribe(data => {
      this.FarmerIdPre = data.Short_Name;
    });

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
    if (this.farmerid != null && this.farmerid != undefined && this.farmerid != '' && this.bagSize != null && this.bagSize != undefined && this.bagSize != '' && this.noOfBags != null && this.noOfBags != undefined && this.noOfBags != '' && this.Source != null && this.Source != undefined && this.Source != '') {
      console.log(this.farmerid);
      const data = {
        farmerid: this.FarmerIdPre + '/' + this.farmerid,
        bagSize: this.bagSize,
        noOfBags: this.noOfBags,
        Quintal: this.Quintal,
        Source: this.Source,
        Amount: this.Amount,
        insertedBy: this.insertedBy
      }
      this.service.submitSeedSubsidyOfGrountnut(data).subscribe((result: any) => {
        if (result == true) {
          this.toastr.success(`Subimitted Sucessfully`);
          this.router.navigate(['dealer/groundnutDemonstration']);
        }
      }
        , (error) => this.toastr.error(error.statusText, error.status)
        // alert(error.statusText)
      );
    }
    else {
      this.toastr.error(`Please Select All Fields.`);
      // alert('session time out')
      // window.location.href='https://agrisnetodisha.ori.nic.in/stock/login.aspx'
    }




  }
}
