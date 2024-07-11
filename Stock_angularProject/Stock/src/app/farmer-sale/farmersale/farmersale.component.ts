import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import { promise } from 'protractor';
import { resolve } from 'dns';
import { filter, timeout } from 'rxjs/operators';
import { NgxSpinnerService } from 'ngx-spinner'
@Component({
  selector: 'app-farmersale',
  templateUrl: './farmersale.component.html',
  styleUrls: ['./farmersale.component.css']
})
export class FarmersaleComponent implements OnInit {

  CntLicDtl: any = [];
  starMobilenumber: any = '';
  starmobilenumbershow: boolean = false;
  emailid: any = '';
  mobileNo: any = '';
  LIC_NO: any = '';
  subsidyModeToERUPI: any = '';

  constructor(private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) {


  }
  ngOnInit(): void {
    // this.PrintReport()
    this.spinner.show();

    try {
      this.service.CntLic().subscribe(data => {
        this.CntLicDtl = data;
        // if (this.CntLicDtl.Cnt == 0) {
          this.service.GetDistCodeFromLicNo().subscribe(data => {
            this.subsidyModeToERUPI = data[0].subsidyModeToERUPI;
            console.log(this.subsidyModeToERUPI);
            
            this.spinner.hide();
          }
          );
        // }
        // else {
        //   this.spinner.hide();
        //   this.router.navigate(['/farmersale/UpdateAccDtls']);
        // }

      })
    } catch (e) {
      console.error(e);

    }
  }
 
}
