import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../Services/login.service';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  url:any;
  stringAfter:any;
  stringAfter1:any;
  role:any;
  username:any
  constructor(
    private authService: LoginService,
    private fb: FormBuilder,
    private toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.url=window.location.href;
    this.stringAfter = this.url.split('#/', 2);
    this.stringAfter1 =this.stringAfter[1].split('/', 2);
    // this.role =this.stringAfter1[0]
    this.role = this.authService.getRole()!;
    this.username = this.authService.getUsername()!;
    
  }
}
