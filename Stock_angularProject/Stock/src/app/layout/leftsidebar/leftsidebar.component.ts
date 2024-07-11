import { Component, OnInit } from '@angular/core';
import { LoginService } from '../../Services/login.service';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { DealerService } from 'src/app/Services/dealer.service';
@Component({
  selector: 'app-leftsidebar',
  templateUrl: './leftsidebar.component.html',
  styleUrls: ['./leftsidebar.component.css']
})
export class LeftsidebarComponent implements OnInit {
  url:any;
  stringAfter:any;
  stringAfter1:any;
  role:any;
  username:any;
  // isActive: boolean[] = [false, false];
  activeIndex: number = -1;
  constructor(
    private authService: LoginService,
    private fb: FormBuilder,
    private toastr: ToastrService,
    private service: DealerService,
  ) { }

  ngOnInit(): void {
    this.url=window.location.href;
    this.stringAfter = this.url.split('#/', 2);
    this.stringAfter1 =this.stringAfter[1].split('/', 2);
    // this.role =this.stringAfter1[0]
    this.role = this.authService.getRole()!;
    if (this.role == 'AAOO' || this.role == 'DIST') {
      this.username = this.authService.getFullname()!;
    }

    else {
      this.username = this.authService.getUsername()!;

    }
    console.log(this.username);
    
    
  }
  getFarmeridPrefix(){
    // this.authService.CheckLogIn(data).subscribe((result: any) => {  
     
    // }, (error) => this.toastr.error(error.statusText, error.status));
  }
  osscinventory(){
    this.service.osscinventory().subscribe(data => {      
      window.open('https://osscpayment.nic.in/#/login?token=' + data.body,'_blank');
    })
  }

  onClick(index:any) {
    this.activeIndex = index === this.activeIndex ? -1 : index;
    // this.isActive[index] = !this.isActive[index];
    console.log(this.activeIndex);
    
  }
}
