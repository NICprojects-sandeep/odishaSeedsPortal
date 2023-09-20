import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-componentwisesubsidy-salecomponent',
  templateUrl: './componentwisesubsidy-salecomponent.component.html',
  styleUrls: ['./componentwisesubsidy-salecomponent.component.css']
})
export class ComponentwisesubsidySalecomponentComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

}
