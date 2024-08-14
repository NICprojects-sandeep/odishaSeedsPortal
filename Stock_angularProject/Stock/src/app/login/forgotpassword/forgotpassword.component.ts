import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { sha512 } from 'js-sha512';
import { MatDialog } from '@angular/material/dialog';
import { NgxSpinnerService } from 'ngx-spinner';
import { CaptchaComponent } from '../captcha/captcha.component';
import { LoginService } from 'src/app/Services/login.service';
import { DoubledealerloginComponent } from '../doubledealerlogin/doubledealerlogin.component';

@Component({
  selector: 'app-forgotpassword',
  templateUrl: './forgotpassword.component.html',
  styleUrls: ['./forgotpassword.component.css']
})
export class ForgotpasswordComponent implements OnInit {
  entuserid: boolean = true;
  showotp: boolean = false;
  showpwd: boolean = false;
  STARMOBILENO: any = '';




  salt: string;
  captchaValue: any;
  captchaResult: any;
  loading: boolean;
  error: string;
  modal: boolean = true;
  news: any = ''
  captchaConfig: any = {
    type: 2,
    length: 6,
    cssClass: 'custom',
    back: {
      stroke: '#2F9688',
      solid: '#f2efd2'
    },
    font: {
      color: '#000000',
      size: '84px',
      family: 'Arial'
    }
  };
  @Input() loginForm: FormGroup;
  @ViewChild('loginFormID') lFormID: any;
  @ViewChild('captchaFormID') cFormID: any;
  @ViewChild(CaptchaComponent) private cc!: CaptchaComponent;
  constructor(
    private toastr: ToastrService,
    private fb: FormBuilder,
    private router: Router,
    private authService: LoginService,
    private dialog: MatDialog,
    private spinner: NgxSpinnerService
  ) {
    this.salt = '';
    this.loading = false;
    this.error = '';


    this.loginForm = this.fb.group({
      userID: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50), Validators.pattern(/^([\w]+|(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,})))$/)]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?\d)(?=.*?[#?!@$%^&*.-]).{8,50}$/)]],
      confirmPassword: ['', [Validators.required, Validators.pattern(/^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?\d)(?=.*?[#?!@$%^&*.-]).{8,50}$/)]],
      otp: ['']
    });
  }

  ngOnInit(): void {
    this.signOut();
    // $(() => {
    //   $('.wrap-input100 .input100').on('focusout', function success() {
    //     if ($(this).val() !== '') {
    //       $(this).addClass('has-content');
    //     } else {
    //       $(this).removeClass('has-content');
    //     }
    //   });
    // });

    // setTimeout(() => {
    //   document.addEventListener('onautocomplete', (e: any) => {
    //     e.target.hasAttribute('autocompleted'); // true or false
    //     // e.preventDefault(); // prevent autocomplete
    //     $('.wrap-input100 .input100').addClass('has-content');
    //   });
    // }, 1);
  }
  openDocumentsDilog(data: any) {
    const dialogRef = this.dialog.open(DoubledealerloginComponent, {
      // height: '400px',
      // width: '500px',
      data: data
    });
  }
  get f() { return this.loginForm.controls; }
  get userID() { return this.loginForm.get('userID'); }
  get password() { return this.loginForm.get('password'); }
  get confirmPassword() { return this.loginForm.get('confirmPassword'); }
  get otp() { return this.loginForm.get('otp'); }

  getMarque() {
    this.authService.getmarqueData().subscribe((result: any) => {
      if (result.result.length > 0) {
        for (let i = 0; i < result.result.length; i++) {
          this.news = this.news + result.result[i].NEWS + '                            ' + '    '
        }
      }
    }, (error) => this.toastr.error(error.statusText, error.status));
  }
  signIn() {
    if (this.loginForm.valid && this.captchaValue !== null && this.captchaValue !== undefined && this.captchaValue !== '') {
      if (this.captchaValue === this.captchaResult) {
        this.spinner.show();
        this.loginForm.patchValue({
          password: sha512(sha512(this.password!.value) + this.salt)
        });
        this.password!.setErrors(null);
        const data = {
          userID: this.userID!.value,
          password: this.password!.value,
          captcha: this.captchaValue
        };
        this.loading = true;
        this.authService.CheckLogIn(data).subscribe((result: any) => {
          if (result.message === true) {
            this.authService.setRole(result.role);
            this.authService.setUsername(result.username);
            this.authService.setFullname(result.fullname);

            console.log(result);

            switch (result.role) {

              case 'OSSC': {
                this.router.navigate(['aao']);
                this.spinner.hide();
                break;
              }
              case 'ADMI': {
                this.router.navigate(['admin']);
                this.spinner.hide();
                break;
              }
              case 'SEAD': {
                this.router.navigate(['admin']);
                this.spinner.hide();
                break;
              }
              case 'SPO': {
                this.router.navigate(['dealer']);
                this.spinner.hide();
                break;
              }
              case 'Dealer': {
                this.router.navigate(['farmersale']);
                this.spinner.hide();
                break;
              }
              case 'AAOO': {
                this.router.navigate(['aao']);
                this.spinner.hide();
                break;
              }
              case 'DIST': {
                this.router.navigate(['dda']);
                this.spinner.hide();
                break;
              }
              case 'jrcs': {
                this.router.navigate(['jrcs']);
                this.spinner.hide();
                break;
              }
              default: {
                this.toastr.warning(`Please enter the <b>User ID</b>, <b>Password</b> and <b>Captcha</b> to proceed with login.`);
                this.spinner.hide();
                this.router.navigate(['']);
              }
            }
          }
          else if (result.message === 'doubleIdPresent') {
            this.spinner.hide();
            this.openDocumentsDilog(result.data);
          }
          else if (result.message === 'Invalid Username or Password.') {
            this.toastr.warning(`Please enter valid userid and correct password.`);
            this.spinner.hide();
            this.loading = false;
            this.error = result.message;
            this.lFormID.nativeElement[0].focus();
            this.loginForm.reset();
            this.cFormID.captchaForm.reset();
            this.cc.generateCaptchaAndSalt();
          }
          else {
            this.toastr.warning(`Please enter the correct <b>Captcha</b> value to proceed with login.`);
            this.spinner.hide();
            this.loading = false;
            this.error = result.message;
            this.lFormID.nativeElement[0].focus();
            this.loginForm.reset();
            this.cFormID.captchaForm.reset();
            this.cc.generateCaptchaAndSalt();
          }
        }, (error) => {
          this.toastr.error('Please try after sometime or contact to administrator.');
          this.spinner.hide();
          this.lFormID.nativeElement[0].focus();
          this.loginForm.reset();
          this.cFormID.captchaForm.reset();
          this.cc.generateCaptchaAndSalt();
        });
      } else {
        this.toastr.warning(`Please enter the correct <b>Captcha</b> value to proceed with login...`);
        this.lFormID.nativeElement[0].focus();
        this.loginForm.reset();
        this.cFormID.captchaForm.reset();
        this.cc.generateCaptchaAndSalt();
      }
    } else {
      this.toastr.warning(`Please enter the <b>User ID</b>, <b>Password</b> and <b>Captcha</b> to proceed with login.`);
      this.lFormID.nativeElement[0].focus();
      this.loginForm.reset();
      this.cFormID.captchaForm.reset();
      this.cc.generateCaptchaAndSalt();
    }

  }
  keyPress(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.signIn();
    }
  }
  enteredCaptcha(e: any) {
    this.captchaValue = e.captchaInput.value;
    this.captchaResult = e.captchaResult;
  }
  generatedSalt(e: any) {
    this.salt = e;
  }
  showPassword() {
    const x = (<HTMLInputElement>document.getElementById('password'));
    if (x.type === 'password') {
      x.type = 'text';
    } else {
      x.type = 'password';
    }
  }


  signOut() {
    this.authService.signOut().subscribe((result: any) => {
      // this.cookieService.delete('auth.cookie', '/', 'localhost', true, "Strict");
      this.authService.clearLocalStorage();
      this.cc.generateCaptchaAndSalt();
      // this.router.navigate(['/']);
    }, (error) => this.toastr.error(error.statusText, error.status));
  }




  sendotp() {
    if (this.captchaValue !== null && this.captchaValue !== undefined && this.captchaValue !== '') {
      if (this.captchaValue === this.captchaResult) {
        console.log(this.userID!.value);
        const data = {
          userID: this.userID!.value,
          captcha: this.captchaValue
        };

        this.authService.sendotp(data).subscribe((result: any) => {
          console.log(result);

          if (result.message === 'Sucessfully send.') {

            this.entuserid = false;
            this.showotp = true;
            this.showpwd = false;
            this.STARMOBILENO = result.STARMOBILENO



          }

          else if (result.message === 'Mobile number is not registered.') {
            this.toastr.warning(`Please register your mobile number`);
            this.spinner.hide();
            this.loading = false;
            this.error = result.message;
            this.lFormID.nativeElement[0].focus();
            this.loginForm.reset();
            this.cFormID.captchaForm.reset();
            this.cc.generateCaptchaAndSalt();
          }
          else if (result.message === 'Invalid Username.') {

            this.entuserid = true;
            this.showotp = false;
            this.showpwd = false;
            this.toastr.warning(`Please enter Valid user name`);
            this.spinner.hide();
            this.loading = false;
            this.error = result.message;
            this.lFormID.nativeElement[0].focus();
            this.loginForm.reset();
            this.cFormID.captchaForm.reset();
            this.cc.generateCaptchaAndSalt();
          }
          else {
            this.toastr.warning(`Please enter the correct <b>Captcha</b> value to proceed with login.`);
            this.entuserid = true;
            this.showotp = false;
            this.showpwd = false;
            this.spinner.hide();
            this.loading = false;
            this.error = result.message;
            this.lFormID.nativeElement[0].focus();
            this.loginForm.reset();
            this.cFormID.captchaForm.reset();
            this.cc.generateCaptchaAndSalt();
          }
        }, (error) => {
          this.toastr.error('Please try after sometime or contact to administrator.');
          this.entuserid = true;
          this.showotp = false;
          this.showpwd = false;
          this.spinner.hide();
          this.lFormID.nativeElement[0].focus();
          this.loginForm.reset();
          this.cFormID.captchaForm.reset();
          this.cc.generateCaptchaAndSalt();
        });


      } else {

        this.toastr.warning(`Please enter the correct <b>Captcha</b> value to proceed with Forget Password`);
        this.lFormID.nativeElement[0].focus();
        this.loginForm.reset();
        this.cFormID.captchaForm.reset();
        this.cc.generateCaptchaAndSalt();
      }
    } else {
      this.toastr.warning(`Please enter the <b>User ID</b> and <b>Captcha</b> to proceed with Forget Password.`);
      this.lFormID.nativeElement[0].focus();
      this.loginForm.reset();
      this.cFormID.captchaForm.reset();
      this.cc.generateCaptchaAndSalt();
    }


  }
  confirmotp() {
    const data = {
      Userid: this.userID!.value,
      enteredOtp: this.otp!.value
    };
    this.authService.confirmotp(data).subscribe((result: any) => {
      if (result == true){
        this.entuserid = false;
        this.showotp = false;
        this.showpwd = true;
        this.toastr.success(`OTP matched sucessfully.`);
      }else{
        this.toastr.warning(`Please enter valid otp.`);
      }

    })


  }
}


