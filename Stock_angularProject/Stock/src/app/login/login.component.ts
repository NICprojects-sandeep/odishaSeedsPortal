import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../Services/login.service';
import { CaptchaComponent } from './captcha/captcha.component';
import { sha512 } from 'js-sha512';
import { DoubledealerloginComponent } from './doubledealerlogin/doubledealerlogin.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
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
  ) {
    this.salt = '';
    this.loading = false;
    this.error = '';


    this.loginForm = this.fb.group({
      userID: ['', [Validators.required, Validators.minLength(2), Validators.maxLength(50), Validators.pattern(/^([\w]+|(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,})))$/)]],
      password: ['', [Validators.required, Validators.pattern(/^(?=.*?[a-z])(?=.*?[A-Z])(?=.*?\d)(?=.*?[#?!@$%^&*.-]).{8,50}$/)]]
    });
  }

  ngOnInit(): void {
    this.getMarque();
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
  openDocumentsDilog(data:any) {
    const dialogRef = this.dialog.open(DoubledealerloginComponent, {
      height: '400px',
      width: '500px',
      data: data
    });
  }
  get f() { return this.loginForm.controls; }
  get userID() { return this.loginForm.get('userID'); }
  get password() { return this.loginForm.get('password'); }
  getMarque() {
    this.authService.getmarqueData().subscribe((result: any) => {
      if (result.result.length > 0) {
        for (let i = 0; i < result.result.length; i++) {
          this.news = this.news + result.result[i].NEWS + '                            ' + '    '
          console.log(this.news);
        }
      }
    }, (error) => this.toastr.error(error.statusText, error.status));
  }
  signIn() {
    if (this.loginForm.valid && this.captchaValue !== null && this.captchaValue !== undefined && this.captchaValue !== '') {
      if (this.captchaValue === this.captchaResult) {
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
            console.log(result);
            this.authService.setRole(result.role);
            this.authService.setUsername(result.username);
            switch (result.role) {

              case 'OSSC': {
                this.router.navigate(['aao']);
                break;
              }
              case 'ADMI': {
                this.router.navigate(['admin']);
                break;
              }
              case 'SPO': {
                this.router.navigate(['dealer']);
                break;
              }
              case 'Dealer': {
                this.router.navigate(['farmersale']);
                break;
              }

              default: {
                this.router.navigate(['']);
              }
            }
          }
          else if (result.message === 'doubleIdPresent') {
            console.log(result);
            
            this.openDocumentsDilog(result.data);

          }
          else {
            this.loading = false;
            this.error = result.message;
            this.lFormID.nativeElement[0].focus();
            this.loginForm.reset();
            this.cFormID.captchaForm.reset();
            this.cc.generateCaptchaAndSalt();
          }
        }, (error) => this.toastr.error(error.statusText, error.status));
      } else {
        this.toastr.warning(`Please enter the correct <b>Captcha</b> value to proceed with login.`);
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
    
}

