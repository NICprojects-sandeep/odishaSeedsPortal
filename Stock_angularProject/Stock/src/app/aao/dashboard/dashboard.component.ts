import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../Services/login.service';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  username:any;
  constructor(
    private authService: LoginService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.username = this.authService.getUsername()!;
  }

}
