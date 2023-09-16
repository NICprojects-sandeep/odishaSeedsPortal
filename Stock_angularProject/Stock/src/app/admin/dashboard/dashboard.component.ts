import { Component, OnInit } from '@angular/core';
import { LoginService } from 'src/app/Services/login.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {
  username:any

  constructor(
    private authService: LoginService,

  ) { }

  ngOnInit(): void {
    this.username = this.authService.getUsername()!;
  }

}
