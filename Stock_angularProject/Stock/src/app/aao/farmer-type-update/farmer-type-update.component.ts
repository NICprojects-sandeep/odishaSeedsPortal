import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { AaoService } from 'src/app/Services/aao.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-farmer-type-update',
  templateUrl: './farmer-type-update.component.html',
  styleUrls: ['./farmer-type-update.component.css']
})
export class FarmerTypeUpdateComponent implements OnInit {

  constructor(
    private fb: FormBuilder,
    private aaoService: AaoService,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.fillfARMERiD();
  }
  fillfARMERiD(){
    this.aaoService.fillfARMERiD().subscribe(data => {      
      // this.FarmerIdPre = data.result.Short_Name;      
    });
  }
}
