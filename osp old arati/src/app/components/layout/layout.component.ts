import { Component, OnInit } from '@angular/core';
import { DashboardService } from 'src/app/dashboard.service';
import { Router } from '@angular/router';
import { AnyObject } from 'chart.js/types/basic';
import * as _ from 'lodash';
import { BrowserModule } from '@angular/platform-browser'


@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.css']
})
export class LayoutComponent implements OnInit {
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
  dataArray: Array<AnyObject>;
  price: any = [];
  groupedCropData: any;
  groupedCropDataAfter: any;
  priceListAfter: any;

  // NewsO : string ='';
  // NewsE : string ='';
  // ContentO : string ='';
  // ContentE : string ='';
  constructor(private router: Router,
    private service: DashboardService,
  ) { }

  ngOnInit(): void {
    // this.service.fillLogoDetailsO().subscribe(data => {
    //   this.LogoTextO=data[0].LogoText;
    // });

    // this.service.fillLogoDetailsE().subscribe(data => {
    //   this.LogoTextE=data[0].LogoText;
    // });

    // this.service.fillMenuO().subscribe(data => {
    //   this.MenuTextO=data;      
    // });

    // this.service.fillMenuE().subscribe(data => {      
    //   this.MenuTextE=data;      
    // });

    // this.service.fillSubMenuO().subscribe(data => {      
    //   this.SubMenuTextO=data;      
    // });    

    // this.service.fillSubMenuE().subscribe(data => {            
    //   this.SubMenuTextE=data;      
    //   console.log(this.SubMenuTextE);
    // });
    this.seedPrice();
    this.seedPriceAfter();
  };
  seedPrice() {
    this.service.getStockPricelist().subscribe(async result => {
      this.priceList = result;
      // console.log(this.priceList);
      var x = await this.groupCropData();
      this.groupedCropData.forEach(a => {
        a.x={};
        a.data.forEach(b => {
          var k = b.Receive_Unitname
          if (k == "NSC Ltd."){
            k="NSC"
          }
          if (k == "Jaysankar Agro"){
            k="JA"
          }
          a.x[k] = b.All_in_cost_Price
        })
      });
      // console.log(this.groupedCropData);
    }, err => console.log(err));
  }

  
  groupCropData() {
    this.groupedCropData = _(this.priceList)
      .groupBy('Crop_Name')
      .map((groupedCrop, cropName) => {
        const allInCostAndReceiveUnits = groupedCrop.map(crop => ({
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
    this.groupedCropDataAfter.forEach(a => {
      a.y={};
      a.data.forEach(b => {
        var k = b.Receive_Unitname
        
        if (k == "NSC Ltd."){
          k="NSC"
        }
        if (k == "Jaysankar Agro"){
          k="JA"
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
    .map((groupedCrop, cropName) => {
      const allInCostAndReceiveUnits = groupedCrop.map(crop => ({
        All_in_cost_Price: crop.All_in_cost_Price,
        Receive_Unitname: crop.Receive_Unitname
      }));
      return { Crop_Name: cropName, data: allInCostAndReceiveUnits };
    })
    .value();
  // console.log(this.groupedCropDataAfter, 'data call');
}

}
