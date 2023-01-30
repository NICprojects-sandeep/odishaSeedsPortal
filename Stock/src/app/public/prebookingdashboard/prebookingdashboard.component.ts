import { Component, OnInit } from '@angular/core';
import { PublicService } from 'src/app/Services/public.service';

@Component({
  selector: 'app-prebookingdashboard',
  templateUrl: './prebookingdashboard.component.html',
  styleUrls: ['./prebookingdashboard.component.css']
})
export class PrebookingdashboardComponent implements OnInit {
  prebookingDtl: any = [];
  totalprebookedorder: any = 0;
  totalprebookedquantity: any = 0;
  totalcollectorder: any = 0;
  totalcollectorderquantity: any = 0;
  totalpendingorder: any = 0;
  totalpendingorderquantity: any = 0;
  totalprebookingdtl: any = [];
  sumprebookedquanitybyfarmer: any = 0.00;
  sumprebookedquanitybydealer: any = 0.00;
  search: any = '';

  constructor(private publicservice: PublicService) { }

  ngOnInit(): void {
    this.getprebookingDtl();
  }
  getprebookingDtl() {
    this.sumprebookedquanitybyfarmer = 0.00
    this.prebookingDtl = [];
    this.publicservice.getprebookingDtl().subscribe(data => {
      this.prebookingDtl = data;
      // console.log(this.prebookingDtl.totalprebookingdtl);
      this.totalprebookedorder = this.prebookingDtl.totalprebookedorder[0].totalprebookedorder
      this.totalprebookedquantity = this.prebookingDtl.totalprebookedquantity[0].totalprebookedquantity
      this.totalcollectorder = this.prebookingDtl.totalcollectorder[0].totalcollectorder
      this.totalcollectorderquantity = this.prebookingDtl.totalcollectorderquantity[0].totalcollectorderquantity
      this.totalpendingorder = this.prebookingDtl.totalpendingorder[0].totalpendingorder
      this.totalpendingorderquantity = this.prebookingDtl.totalpendingorderquantity[0].totalpendingorderquantity
      this.totalprebookingdtl = this.prebookingDtl.totalprebookingdtl;

      this.totalprebookingdtl.forEach((i: any) => {
        if (i.hasOwnProperty('prebookedquanitybyfarmer')) {
          var a = (i.prebookedquanitybyfarmer == undefined || i.prebookedquanitybyfarmer == null || i.prebookedquanitybyfarmer == '') ? 0.00 : i.prebookedquanitybyfarmer;
          // this.sumprebookedquanitybyfarmer = (this.sumprebookedquanitybyfarmer) + parseFloat(a);
          this.sumprebookedquanitybyfarmer = (parseFloat(this.sumprebookedquanitybyfarmer) + parseFloat(a)).toFixed(2);
        }
        if (i.hasOwnProperty('prebookedquanitybydealer')) {
          var b = (i.prebookedquanitybydealer == undefined || i.prebookedquanitybydealer == null || i.prebookedquanitybydealer == '') ? 0.00 : i.prebookedquanitybydealer;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumprebookedquanitybydealer = (parseFloat(this.sumprebookedquanitybydealer) + parseFloat(b)).toFixed(2);

        }
      })

    })
  }
  getSearchprebookingDtl() {
    this.totalprebookingdtl = [];
    this.sumprebookedquanitybyfarmer = 0.00
    this.sumprebookedquanitybydealer = 0.00
    this.publicservice.getSearchprebookingDtl(this.search).subscribe(data => {
      this.prebookingDtl = data;
      console.log(this.prebookingDtl);

      this.totalprebookingdtl = this.prebookingDtl;

      this.totalprebookingdtl.forEach((i: any) => {
        if (i.hasOwnProperty('prebookedquanitybyfarmer')) {
          var a = (i.prebookedquanitybyfarmer == undefined || i.prebookedquanitybyfarmer == null || i.prebookedquanitybyfarmer == '') ? 0.00 : i.prebookedquanitybyfarmer;
          // this.sumprebookedquanitybyfarmer = (this.sumprebookedquanitybyfarmer) + parseFloat(a);
          this.sumprebookedquanitybyfarmer = (parseFloat(this.sumprebookedquanitybyfarmer) + parseFloat(a)).toFixed(2);
        }
        if (i.hasOwnProperty('prebookedquanitybydealer')) {
          var b = (i.prebookedquanitybydealer == undefined || i.prebookedquanitybydealer == null || i.prebookedquanitybydealer == '') ? 0.00 : i.prebookedquanitybydealer;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumprebookedquanitybydealer = (parseFloat(this.sumprebookedquanitybydealer) + parseFloat(b)).toFixed(2);

        }
      })

    })
  }
  // applyFilter(filterValue: any) {
  //   console.log(filterValue.value);
  //   const filter =filterValue.value
  //   filterValue.value = filter.trim(); // Remove whitespace
  //   filterValue.value = filter.toLowerCase(); // MatTableDataSource defaults to lowercase matches
  //   this.totalprebookingdtl = filterValue.value;
  //   console.log(this.totalprebookingdtl );

  // }
}
