import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AdminService } from 'src/app/Services/admin.service';

@Component({
  selector: 'app-resetpassword',
  templateUrl: './resetpassword.component.html',
  styleUrls: ['./resetpassword.component.css']
})
export class ResetpasswordComponent implements OnInit {
  getAllUserTypeData: any = [];
  SelectedUserType: any = '';
  SelectedUserID: any = '';
  getAllUserIdData: any = [];
  constructor(private fb: FormBuilder,
    private service: AdminService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService) { }

  ngOnInit(): void {
    this.getAllUserType();
  }
  getAllUserType() {
    this.getAllUserTypeData = []
    this.service.getAllUserType().subscribe(data => {
      this.getAllUserTypeData = data;
    })
  }
  getUserId() {
    this.getAllUserIdData = [];
    this.SelectedUserID = '';
    this.service.getUserId(this.SelectedUserType).subscribe(data => {
      this.getAllUserIdData = data;
    })
  }
  resetPassword() {
    if (this.SelectedUserType && this.SelectedUserID) {
      let data = {
        SelectedUserType: this.SelectedUserType,
        UserID: this.SelectedUserID.UserID
      }
      this.service.resetPassword(data).subscribe(data => {
        if (data == 1) {
          this.toastr.success('Password Has Been Updated Successfully and New password is <b>Test@1234</b>');
          this.SelectedUserType = '';
          this.SelectedUserID = '';
        }
        else {
          this.toastr.success('Internal sever error please retry after some time.');
          this.SelectedUserType = '';
          this.SelectedUserID = '';
        }
        // this.getAllUserIdData = data;
      })
    }
    else {
      this.toastr.success('Please select all field.');
    }
  }
}
