import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from '../dashboard.service';

@Component({
  selector: 'app-container',
  templateUrl: './container.component.html',
  styleUrls: ['./container.component.css']
})
export class ContainerComponent {
  getSeason: any = [];
  crops: any = [];
  varieties: any = [];
  districts: any = [];
  distWiseData: any = [];
  quantityArray: any = [];
  quantity:any;
  showTable:boolean=false;
    saleData: any = [];
  pendingData: any = [];
  PacsLamps:number=0;
  DealerCount:number=0;
  year: any = '';
  season: any = '';
  crop: any = '';
  constructor(private router: Router,
    private service: DashboardService,
  ) { }
  ngOnInit(): void {
    this.loadSeason();
    this.loadDistricts();
    this.getPacsLamps();
    this.getDealerCount();
  };

  loadSeason() {
    this.service.loadSeason(this.year).subscribe(async result => {
      this.getSeason = result.cropSeason;
    }, err => console.log(err));
  }
  loadAllCrop() {
    this.service.loadAllCrop(this.year, this.season).subscribe(async result => {
      this.crops = result;
      console.log(this.crops);

    }, err => console.log(err));
  }

  loadVariety() {
    console.log(this.crop, this.year, this.season);

    this.service.loadVariety(this.crop, this.year, this.season).subscribe(async result => {
      this.varieties = result;
      this.districtWiseData();
    }, err => console.log(err));
  }
  loadDistricts() {
    this.service.loadDistricts().subscribe(async result => {
      this.districts = result;
    }, err => console.log(err));
  }
  districtWiseData() {
    this.service.districtWiseData(this.crop, this.year, this.season).subscribe(async result => {
      this.distWiseData = result.result;
      this.loadSaleData();
    }, err => console.log(err));
  }
  loadSaleData() {
    this.service.loadSaleData(this.crop, this.year, this.season).subscribe(async result => {
      this.saleData = result;      
      this.loadPendingData();
    }, err => console.log(err));
  }
  loadPendingData() {
    console.log(this.crop, this.year, this.season);

    this.service.loadPendingData(this.crop, this.year, this.season).subscribe(async result => {
      console.log(result,'pending');
      this.pendingData = result;
    }, err => console.log(err));
  }
  getMatchingQuantity(districtCode: any, varietyId: any, category: any) {
    this.quantityArray = [];

    switch (category) {
      case 'pending':
        this.quantityArray = this.pendingData;
        break;
      case 'available':
        this.quantityArray = this.distWiseData;
        break;
      case 'sold':
        this.quantityArray = this.saleData;
        break;
      default:
        return 0;
    }
    if (!this.quantityArray) {
      return 0;
    }
    else{
      this.quantity = this.quantityArray.find(
        (q:any) => q._id.Variety === varietyId && q._id.Dist === districtCode
      );
      
      return this.quantity ? this.quantity.Qty / 100 : 0;
    }

   
  };
  showtable(){
    this.showTable=true;
  }
  
  getPacsLamps() {
    this.service.getPacsLamps().subscribe(async result => {
      this.PacsLamps = result[0].seedPacsLamps;
      console.log(this.PacsLamps);
      
    }, err => console.log(err));
  }
  getDealerCount() {
    this.service.getDealerCount().subscribe(async result => {
      console.log(result[0]);
      
      this.DealerCount = result[0].seedDlrcount;
      console.log(this.DealerCount,'DealerCount');
      
    }, err => console.log(err));
  }
}
