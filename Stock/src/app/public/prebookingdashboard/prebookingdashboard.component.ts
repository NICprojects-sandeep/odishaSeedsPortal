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
  selectedSeason: any;
  selectedDistrict: any;
  selectedDealer: any;
  selectedCrop: any;
  selectedVariety: any;
  selectedDemandMonth: any;
  prebookingDistrict: any = [];
  allCrop: any = [];
  allvarity: any = [];

  constructor(private publicservice: PublicService) {
    this.selectedSeason = '0';
    this.selectedDistrict = '0';
    this.selectedDealer = '0';
    this.selectedCrop = '0';
    this.selectedVariety = '0';
    this.selectedDemandMonth = '0';

  }

  ngOnInit(): void {
    this.getprebookingDtl();
    this.getPrebookingDistrict();
    this.getCrop();
  }
  getprebookingDtl() {
    this.sumprebookedquanitybyfarmer = 0.00
    this.prebookingDtl = [];


    this.publicservice.getprebookingDtl().subscribe(data => {
      this.prebookingDtl = data;
      this.totalprebookedorder = this.prebookingDtl.totalprebookedorder[0].totalprebookedorder
      this.totalprebookedquantity = this.prebookingDtl.totalprebookedquantity[0].totalprebookedquantity
      this.totalcollectorder = this.prebookingDtl.totalcollectorder.length
      // this.totalcollectorderquantity = this.prebookingDtl.totalcollectorderquantity
      this.totalpendingorder = this.prebookingDtl.totalpendingorder[0].totalpendingorder
      this.totalpendingorderquantity = this.prebookingDtl.totalpendingorderquantity[0].totalpendingorderquantity
      this.totalprebookingdtl = this.prebookingDtl.totalprebookingdtl;

      this.prebookingDtl.totalcollectorderquantity.forEach((i: any) => {
        if (i.hasOwnProperty('collectqty')) {
          var a = (i.collectqty == undefined || i.collectqty == null || i.collectqty == '') ? 0.00 : i.collectqty;
          this.totalcollectorderquantity = (parseFloat(this.totalcollectorderquantity) + parseFloat(a)).toFixed(2);
        }
      })

      this.totalprebookingdtl.forEach((i: any) => {
        if (i.hasOwnProperty('prebookingquanity')) {
          var a = (i.prebookingquanity == undefined || i.prebookingquanity == null || i.prebookingquanity == '') ? 0.00 : i.prebookingquanity;
          // this.sumprebookedquanitybyfarmer = (this.sumprebookedquanitybyfarmer) + parseFloat(a);
          this.sumprebookedquanitybyfarmer = (parseFloat(this.sumprebookedquanitybyfarmer) + parseFloat(a)).toFixed(2);
        }
        if (i.hasOwnProperty('availableQuanity')) {
          var b = (i.availableQuanity == undefined || i.availableQuanity == null || i.availableQuanity == '') ? 0.00 : i.availableQuanity;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumprebookedquanitybydealer = (parseFloat(this.sumprebookedquanitybydealer) + parseFloat(b)).toFixed(2);

        }
      })

    })
  }
  getSearchprebookingDtl() {
    this.totalprebookingdtl = [];
    this.sumprebookedquanitybyfarmer = 0.00;
    this.sumprebookedquanitybydealer = 0.00;
    this.totalcollectorderquantity=0.00;
    const reqdata = {
      selectedSeason: this.selectedSeason,
      selectedDistrict: this.selectedDistrict,
      selectedCrop: this.selectedCrop,
      selectedVariety: this.selectedVariety,
      selectedDemandMonth: this.selectedDemandMonth,
    }
    this.publicservice.getSearchprebookingDtl(reqdata).subscribe(data => {
      this.prebookingDtl = data;
      this.totalprebookedorder = this.prebookingDtl.totalprebookedorder[0].totalprebookedorder
      this.totalprebookedquantity = this.prebookingDtl.totalprebookedquantity[0].totalprebookedquantity
      this.totalcollectorder = this.prebookingDtl.totalcollectorder.length
      // this.totalcollectorderquantity = this.prebookingDtl.totalcollectorderquantity
      this.totalpendingorder = this.prebookingDtl.totalpendingorder[0].totalpendingorder
      this.totalpendingorderquantity = this.prebookingDtl.totalpendingorderquantity[0].totalpendingorderquantity
      this.totalprebookingdtl = this.prebookingDtl.totalprebookingdtl;

      this.prebookingDtl.totalcollectorderquantity.forEach((i: any) => {
        if (i.hasOwnProperty('collectqty')) {
          var a = (i.collectqty == undefined || i.collectqty == null || i.collectqty == '') ? 0.00 : i.collectqty;
          this.totalcollectorderquantity = (parseFloat(this.totalcollectorderquantity) + parseFloat(a)).toFixed(2);
        }
      })

      this.totalprebookingdtl.forEach((i: any) => {
        if (i.hasOwnProperty('prebookingquanity')) {
          var a = (i.prebookingquanity == undefined || i.prebookingquanity == null || i.prebookingquanity == '') ? 0.00 : i.prebookingquanity;
          // this.sumprebookedquanitybyfarmer = (this.sumprebookedquanitybyfarmer) + parseFloat(a);
          this.sumprebookedquanitybyfarmer = (parseFloat(this.sumprebookedquanitybyfarmer) + parseFloat(a)).toFixed(2);
        }
        if (i.hasOwnProperty('availableQuanity')) {
          var b = (i.availableQuanity == undefined || i.availableQuanity == null || i.availableQuanity == '') ? 0.00 : i.availableQuanity;
          // this.sumprebookedquanitybydealer = (this.sumprebookedquanitybydealer) + parseFloat(b);
          this.sumprebookedquanitybydealer = (parseFloat(this.sumprebookedquanitybydealer) + parseFloat(b)).toFixed(2);

        }
      })

    })
  }
  getPrebookingDistrict() {
    this.publicservice.getPrebookingDistrict(this.selectedSeason).subscribe(data => {
      this.prebookingDistrict = data;
    })
  }
  getCrop() {
    this.publicservice.getCrop().subscribe(data => {
      this.allCrop = data;
    })
  }
  getVariey() {
    this.publicservice.getVariey(this.selectedCrop).subscribe(data => {
      this.allvarity = data;
    })
  }
  // applyFilter(filterValue: any) {
  //   const filter =filterValue.value
  //   filterValue.value = filter.trim(); // Remove whitespace
  //   filterValue.value = filter.toLowerCase(); // MatTableDataSource defaults to lowercase matches
  //   this.totalprebookingdtl = filterValue.value;

  // }
}
