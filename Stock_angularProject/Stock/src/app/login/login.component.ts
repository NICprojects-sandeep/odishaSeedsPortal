import { Component, EventEmitter, Input, OnInit, Output, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, FormArray, Validators } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { LoginService } from '../Services/login.service';
import { CaptchaComponent } from './captcha/captcha.component';
import { sha512 } from 'js-sha512';
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
  get f() { return this.loginForm.controls; }
  get userID() { return this.loginForm.get('userID'); }
  get password() { return this.loginForm.get('password'); }

  signIn() {
    console.log('entry');
    
    if (this.loginForm.valid && this.captchaValue !== null && this.captchaValue !== undefined && this.captchaValue !== '') {
      // const cv = parseInt(this.captchaValue, 10);
      // console.log(this.captchaValue === this.captchaResult,this.captchaValue , this.captchaResult);
      
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
        this.authService.signIn(data).subscribe((result: any) => {          
          this.loading = false;
          if (result.message === true) {
            this.authService.setRole(result.role);
            this.authService.setUsername(result.username);            
            switch (result.role) {
              case 'AAE': {
                this.router.navigate(['aae']);
                break;
              }
              case 'Admin': {
                this.router.navigate(['admin']);
                break;
              }
              case 'AEE': {
                this.router.navigate(['aee']);
                break;
              }
              case 'AO': {
                this.router.navigate(['ao']);
                break;
              }
              case 'EE': {
                this.router.navigate(['ee']);
                break;
              }
              case 'Manufacturer': {
                this.router.navigate(['manufacturer']);
                break;
              }
              case 'OAIC': {
                this.router.navigate(['oaic']);
                break;
              }
              case 'OFMRDC': {
                this.router.navigate(['ofmrdc']);
                break;
              }
              case 'OSIC': {
                this.router.navigate(['osic']);
                break;
              }
              case 'SE': {
                this.router.navigate(['se']);
                break;
              }
              case 'SuperAdmin': {
                this.router.navigate(['superAdmin']);
                break;
              }
              case 'MSakti': {
                this.router.navigate(['wshg']);
                break;
              }
              case 'Bank': {
                this.router.navigate(['bank']);
                break;
              }
              case 'DAFP': {                
                this.router.navigate(['dafp']);
                break;
              }
              case 'MDOAIC': {
                console.log('kkkkkkkkkkkkkkkkkkkkdafp');
                this.router.navigate(['mdoaic']);
                break;
              }
              default: {
                this.router.navigate(['']);
              }
            }
          } else {
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
}
