import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
@Component({
  selector: 'app-publicleftsidebar',
  templateUrl: './publicleftsidebar.component.html',
  styleUrls: ['./publicleftsidebar.component.css']
})
export class PublicleftsidebarComponent implements OnInit {
  showLi: boolean = false
  constructor(private router: Router, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.route.queryParams
      .subscribe((params) => {
        localStorage.setItem('userId', params.userID);
        const x: any = localStorage.getItem("userId");        
        if (x != "undefined" && x != undefined) {
          if(x.substr( x.length-4)=='.com'){
            this.showLi = true
          }
        
        }
        else {
          localStorage.removeItem("userId");
        }
      }
      );
   
  }

}
