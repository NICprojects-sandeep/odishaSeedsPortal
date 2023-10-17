import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { DashboardService } from '../dashboard.service';
import * as _ from 'lodash';
@Component({
  selector: 'app-feestructure',
  templateUrl: './feestructure.component.html',
  styleUrls: ['./feestructure.component.css']
})
export class FeestructureComponent {
  title: string = 'osp';
  LogoTextO: string = '';
  LogoTextE: string = '';
  MenuTextO: string = '';
  MenuTextE: string = '';
  SubMenuTextO: string = '';
  SubMenuTextE: string = '';
  priceList: any;
  error: any;
  crops: any;
  price: any = [];
  groupedCropData: any;
  groupedCropDataAfter: any;
  priceListAfter: any;
  finyear: any = '';
  season: any = '';
  constructor(private router: Router,
    private service: DashboardService,
  ) { }


  ngOnInit(): void {
    this.seedPrice();
    this.seedPriceAfter();
  };

  seedPrice() {
    this.service.getStockPricelist().subscribe(async result => {
      this.priceList = result;
      if (this.priceList.length > 0) {
        this.finyear = this.priceList[0].F_Year;
        if (this.priceList[0].seasons == 'K') {
          this.season = 'Kharif';
        }
        else {
          this.season = 'Rabi';
        }
        console.log(this.priceList);
        var x = await this.groupCropData();
        this.groupedCropData.forEach((a: { x: { [x: string]: any; }; data: any[]; }) => {
          a.x = {};
          a.data.forEach((b: { Receive_Unitname: any; All_in_cost_Price: any; }) => {
            var k = b.Receive_Unitname
            if (k == "NSC Ltd.") {
              k = "NSC"
            }
            if (k == "Jaysankar Agro") {
              k = "JA"
            }
            a.x[k] = b.All_in_cost_Price
          })
        });
      }
      // console.log(this.groupedCropData);
    }, err => console.log(err));
  }


  groupCropData() {
    this.groupedCropData = _(this.priceList)
      .groupBy('Crop_Name')
      .map((groupedCrop: any[], cropName: any) => {
        const allInCostAndReceiveUnits = groupedCrop.map((crop: { All_in_cost_Price: any; Receive_Unitname: any; }) => ({
          All_in_cost_Price: crop.All_in_cost_Price,
          Receive_Unitname: crop.Receive_Unitname
        }));
        return { Crop_Name: cropName, data: allInCostAndReceiveUnits };
      })
      .value();
    // console.log(this.groupedCropData, 'data call');
  }



  seedPriceAfter() {
    this.service.getStockPricelistAfter().subscribe(async result => {
      this.priceListAfter = result;
      // console.log(this.priceListAfter);
      var y = await this.groupCropDataAfter();
      this.groupedCropDataAfter.forEach((a: { y: { [x: string]: any; }; data: any[]; }) => {
        a.y = {};
        a.data.forEach((b: { Receive_Unitname: any; All_in_cost_Price: any; }) => {
          var k = b.Receive_Unitname

          if (k == "NSC Ltd.") {
            k = "NSC"
          }
          if (k == "Jaysankar Agro") {
            k = "JA"
          }
          a.y[k] = b.All_in_cost_Price
        })
      });
      // console.log(this.groupedCropDataAfter);
    }, err => console.log(err));
  }

  groupCropDataAfter() {
    this.groupedCropDataAfter = _(this.priceListAfter)
      .groupBy('Crop_Name')
      .map((groupedCrop: any[], cropName: any) => {
        const allInCostAndReceiveUnits = groupedCrop.map((crop: { All_in_cost_Price: any; Receive_Unitname: any; }) => ({
          All_in_cost_Price: crop.All_in_cost_Price,
          Receive_Unitname: crop.Receive_Unitname
        }));
        return { Crop_Name: cropName, data: allInCostAndReceiveUnits };
      })
      .value();
    // console.log(this.groupedCropDataAfter, 'data call');
  }

}
