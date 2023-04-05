import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  salt: string='';
  captchaValue: any;
  captchaResult: any;
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
  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }
  CheckLogIn()
  {
    var Uid = ((document.getElementById("Uid") as HTMLInputElement).value);
    var Pas = ((document.getElementById("Pas") as HTMLInputElement).value);
    if(Uid == '')
    {
      alert("User Name can not be blank");
    }
    else
    {      
      alert(Uid.toString());      
    }

    if(Pas == '')
    {
      alert("Password can not be blank");
    }
    else
    {      
      alert(Pas.toString());      
    }
    
  }
  keyPress(event: KeyboardEvent) {
    if (event.key === 'Enter') {
      this.CheckLogIn();
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
