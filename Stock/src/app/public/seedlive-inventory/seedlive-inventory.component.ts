import { Component, OnInit } from '@angular/core';
import { PublicService } from 'src/app/Services/public.service';
import { Subscription, timer } from 'rxjs';
import { switchMap } from 'rxjs/operators';
@Component({
  selector: 'app-seedlive-inventory',
  templateUrl: './seedlive-inventory.component.html',
  styleUrls: ['./seedlive-inventory.component.css']
})
export class SeedliveInventoryComponent implements OnInit {
  livedata:any=[];
  subscription !: Subscription;
  constructor(private publicservice: PublicService) {
  
   }

  // ngOnInit(): void {
  //   this.getCrop();
  // }
  // getCrop() {
  //   this.publicservice.getLivedata().subscribe(data => {
  //     this.livedata = data;
  //   })
  // }
  ngOnInit() {
    this.subscription = timer(0, 3000).pipe(
      switchMap(() => this.publicservice.getLivedata())
    ).subscribe(result => 
      this.livedata = result
    );
}

ngOnDestroy() {
    this.subscription.unsubscribe();
}
}