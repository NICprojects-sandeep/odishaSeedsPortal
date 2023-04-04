import { Component, OnInit,Input } from '@angular/core';
import { Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { Location } from '@angular/common';

@Component({
  selector: 'app-dealerstockdtl',
  templateUrl: './dealerstockdtl.component.html',
  styles: [
  ]
})
export class DealerstockdtlComponent implements OnInit {
  LOT_NO: any;
  CropId:any;
  CropName:any;
  VarietyId:any;
  VarietyName:any;
  _GetDealerStockCrop:any=[];
  _GetDealerStockVariety:any=[];
  _GetDealerStock:any=[];  
  _StockData:any;
  _AddLot:any=[];
  crop: any='';
  variety:any='';
  SeasonName:any;
  BAG_SIZE_IN_KG:any;
  AVL_BAGS:any;
  All_in_cost_Price:any;
  _AVL_BAGS:any=[];
  noofbags:any;
  noofbags_qtl:any;
  price_lot:any;
  AVL_BAGS1:any;
totAmt:any=0;
totBags:any=0;
totQty:any=0;
  constructor(private router: Router,private service:FarmersaleService, private location : Location) { }

@Input() LicNo: any;
@Input() finyr: any;
@Input() season: any;

  ngOnInit(): void {
    this.GetDealerStockCrop()
    if(this.season=='R')
    this.SeasonName='Rabi'
    else
    this.SeasonName='Kharif'
    
  }

  GetDealerStockCrop()
  { 
    this.service.GetDealerStockCrop(this.LicNo,this.finyr,this.season).subscribe(data=>{
      this._GetDealerStockCrop=data;
      });
  }

  GetDealerStockVariety()
  { 
    this._GetDealerStockVariety=[];
    this.CropId = this.crop.CROP_ID;    
    this.service.GetDealerStockVariety(this.LicNo,this.finyr,this.season,this.CropId ).subscribe(data=>{
      this._GetDealerStockVariety=data;
      });
  }

  GetDealerStock()
  { 
    this.CropId = this.crop.CROP_ID; 
    this.VarietyId = this.variety.CROP_VERID; 
       
    this.service.GetDealerStock(this.LicNo,this.finyr,this.season,this.CropId,this.VarietyId ).subscribe(data=>{
      this._GetDealerStock=data;
      this.CropName=this._GetDealerStock.Crop_Name;
      this.VarietyName=this._GetDealerStock.Variety_Name;
      console.log(this.crop.Crop_Name,this.variety.Variety_Name);
      });
  }

  ShowModal(dealerstock: any)
  {
    this._AVL_BAGS=[];
    this.VarietyName=this.variety.Variety_Name;
    this.LOT_NO = dealerstock.LOT_NO;
    this.CropId=dealerstock.CropId;
    this.VarietyId=dealerstock.VarietyId;
    this.BAG_SIZE_IN_KG=dealerstock.BAG_SIZE_IN_KG;
    this.AVL_BAGS=dealerstock.AVL_BAGS;
    this.All_in_cost_Price=dealerstock.All_in_cost_Price;
    for(let i=1;i<=this.AVL_BAGS;i++)
    {
      this._AVL_BAGS.push(i);      
    }    
    this.AVL_BAGS1=this._AVL_BAGS[0];
    this.price_lot='0';
    this.noofbags = this.AVL_BAGS1; 
    this.noofbags_qtl=this.BAG_SIZE_IN_KG*this.noofbags/100;
    this.price_lot=this.All_in_cost_Price*this.noofbags_qtl;
  }
  GetPrice()
  {
    this.price_lot='0';
    this.noofbags = this.AVL_BAGS1; 
    this.noofbags_qtl=this.BAG_SIZE_IN_KG*this.noofbags/100;
    this.price_lot=this.All_in_cost_Price*this.noofbags_qtl;
    
  }
  AddLot()
  {
    const K={Crop_Name:this.crop.Crop_Name,Variety_Name:this.variety.Variety_Name,LOT_NO:this.LOT_NO,BAG_SIZE:this.BAG_SIZE_IN_KG,NO_OF_BAGS:this.noofbags,Qty_qtl:this.noofbags_qtl,Amount:this.price_lot}
    this._AddLot.push(K);
    this.totAmt=0;
    this.totBags=0;
    this.totQty=0;
    this._AddLot.forEach((i:any) => {
      if(i.hasOwnProperty('Amount')){
        var m=(i.Amount==undefined||i.Amount==null||i.Amount=='')?0:i.Amount;
      this.totAmt=(this.totAmt)+parseInt(m);
      }
    });

    this._AddLot.forEach((i:any) => {
      if(i.hasOwnProperty('NO_OF_BAGS')){
        var m=(i.NO_OF_BAGS==undefined||i.NO_OF_BAGS==null||i.NO_OF_BAGS=='')?0:i.NO_OF_BAGS;
      this.totBags=(this.totBags)+parseInt(m);
      }
    });

    this._AddLot.forEach((i:any) => {
      if(i.hasOwnProperty('Qty_qtl')){
        var m=(i.Qty_qtl==undefined||i.Qty_qtl==null||i.Qty_qtl=='')?0:i.Qty_qtl;
      this.totQty=(this.totQty)+m;
      }
    });
  }
  RemoveLot(i:any,Amount:any,NO_OF_BAGS:any,Qty_qtl:any)
  {
    this._AddLot.splice(i,1);       
      this.totAmt=(this.totAmt)-Amount;
      this.totBags=(this.totBags)-NO_OF_BAGS;
      this.totQty=(this.totQty)-Qty_qtl;
  }
  RouteLot()
  {
    this.service.setAddLot(this._AddLot);
  }
}
