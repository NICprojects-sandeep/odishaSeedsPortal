import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/login.service';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ActivatedRoute, Router } from '@angular/router';
@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  username:any
  CntLicDtl: any = []
  FarmerIdPre: any;

  constructor(
    private router: Router,
    private authService: LoginService,
    private service: FarmersaleService,
  ) { }

  ngOnInit(): void {
    this.username = this.authService.getUsername()!;
    this.service.CntLic().subscribe(data => {
      this.CntLicDtl = data;
      console.log(this.CntLicDtl);
      if (this.CntLicDtl.Cnt == 0) {
        this.service.GetDistCodeFromLicNo().subscribe(data => {
          this.FarmerIdPre = data[0].SHORT_NAME;

          // });
          // this.FILLDEALERSTOCK();
          //   }
          //   else {          
          //     window.location.href = 'https://agrisnetodisha.ori.nic.in/stock/login.aspx';
          //     localStorage.removeItem('userId');
          //   }
        }
        );
      }
      else {
        this.router.navigate(['/farmersale/UpdateAccDtls']);
      }

    })
  }

}
