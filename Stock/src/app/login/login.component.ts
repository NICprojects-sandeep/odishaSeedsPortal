import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

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

}
