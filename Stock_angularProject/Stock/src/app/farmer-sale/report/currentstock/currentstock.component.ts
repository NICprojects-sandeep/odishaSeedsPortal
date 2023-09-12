import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import * as XLSX from 'xlsx';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-currentstock',
  templateUrl: './currentstock.component.html',
  styleUrls: ['./currentstock.component.css']
})
export class CurrentstockComponent implements OnInit {
  cropCodeArrays: any = [];
  constructor(private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private toastr: ToastrService) { }

  ngOnInit(): void {
    this.getCurrentstockDetails();
  }
  getCurrentstockDetails() {
    this.service.getCurrentstockDetails().subscribe(data => {
      console.log(data);
      this.cropCodeArrays = [];

      data.forEach((item:any) => {
        const cropCode = item.Crop_Code;
    
        let found = false;
        this.cropCodeArrays.forEach((cropArray:any) => {
            if (cropArray[0]?.Crop_Code === cropCode) {
                cropArray.push(item);
                found = true;
            }
        });
    
        if (!found) {
            this.cropCodeArrays.push([item]);
        }
    });
    console.log(this.cropCodeArrays);
    
  })
}
}
