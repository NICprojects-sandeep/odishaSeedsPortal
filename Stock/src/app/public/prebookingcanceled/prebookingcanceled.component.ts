import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { FarmersaleService } from 'src/app/farmersale.service';
import { PublicService } from 'src/app/Services/public.service';
import { ActivatedRoute } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
@Component({
  selector: 'app-prebookingcanceled',
  templateUrl: './prebookingcanceled.component.html',
  styleUrls: ['./prebookingcanceled.component.css']
})
export class PrebookingcanceledComponent implements OnInit {
  insertedBy: any = '';
  FarmerIdPre: any;
  prebookingDtlDelerWise: any = [];
  selectedApplicationID: any = '';
  remark: any = '';
  constructor(private router: Router,
    private service: FarmersaleService,
    private publicservice: PublicService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
  ) { }


  ngOnInit(): void {
    localStorage.removeItem('userId');
    this.route.queryParams
      .subscribe((params) => {
        this.insertedBy = params.userID;
        localStorage.setItem('userId', params.userID);
        if (this.insertedBy != "undefined" && this.insertedBy != undefined) {
          const dealerId = { dealerId: params.userID }
          this.prebookingDtlDelerWise = [];
          this.publicservice.getprebookingDtlDelerWise(dealerId).subscribe(data => {
            this.prebookingDtlDelerWise = data;
            // this.FarmerIdPre = data[0].Short_Name;
          });
        }
        else {
          window.location.href = 'https://agrisnetodisha.ori.nic.in/stock/login.aspx';
          localStorage.removeItem('userId');
        }
      }
      );
  }
  databind(t: any) {
    this.remark = ''
    this.selectedApplicationID = t.applicationID;
  }
  dataDeletePrebookingDtl(y: any) {
    
    const details = {
      selectedApplicationID: y,
      delerID: this.insertedBy,
      remark: this.remark
    }
    this.publicservice.dataDeletePrebookingDtl(details).subscribe(data => {
      this.toastr.success('Prebooking Canceled Successfully.')
      this.ngOnInit();
    });
  }

}
