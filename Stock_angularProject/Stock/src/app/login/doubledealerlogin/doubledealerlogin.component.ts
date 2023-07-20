import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { LoginService } from 'src/app/Services/login.service';
enum CheckBoxType { APPLY_FOR_JOB, MODIFY_A_JOB, NONE };
@Component({
  selector: 'app-doubledealerlogin',
  templateUrl: './doubledealerlogin.component.html',
  styleUrls: ['./doubledealerlogin.component.css']
})
export class DoubledealerloginComponent implements OnInit {
  selectedIndex: any
  check_box_type = CheckBoxType;
  selectLicNumber: any = ''

  currentlyChecked: CheckBoxType | undefined;

  constructor(
    private router: Router,
    private authService: LoginService,
    public dialogRef: MatDialogRef<DoubledealerloginComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {

  }

  ngOnInit(): void {

  }
  onCloseClick(): void {
    window.location.reload();
  }
  login() {
    console.log('hi');

    console.log(this.currentlyChecked, this.selectLicNumber);
    if (this.selectLicNumber != null && this.selectLicNumber != undefined && this.selectLicNumber != '') {
      var data={licNumber:this.currentlyChecked};
      this.authService.OneDealerLogin(data).subscribe((result: any) => {
        if (result.message === true) {
          this.authService.setRole(result.role);
          this.authService.setUsername(result.username);
          alert('sucessfully log in');
          this.dialogRef.close();
          switch (result.role) {
            case 'Dealer': {
              this.router.navigate(['farmersale']);
              break;
            }

            default: {
              this.router.navigate(['']);
            }
          }
        }
        else {
          window.location.reload();
        }
      });
    }
  }
  selectCheckBox(targetType: CheckBoxType) {
    console.log(targetType, this.currentlyChecked);
    this.selectLicNumber = '';
    // If the checkbox was already checked, clear the currentlyChecked variable
    if (this.currentlyChecked === targetType) {
      this.currentlyChecked = CheckBoxType.NONE;
      this.selectLicNumber = '';
      return;
    }

    this.currentlyChecked = targetType;
    this.selectLicNumber = this.currentlyChecked
  }
}