import { Component, OnInit, Inject, Input } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DashboardService } from 'src/app/dashboard.service';
@Component({
  selector: 'app-dialougebox',
  templateUrl: './dialougebox.component.html',
  styleUrls: ['./dialougebox.component.css']
})
export class DialougeboxComponent implements OnInit {
  selectedBlock: any;
  dealers = this.data.dealers;
  blockList = this.data.blockList;
  blockCode: any;
  dealersList : any;
  constructor(
    private service: DashboardService,
    public dialogRef: MatDialogRef<DialougeboxComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any,
  ) {
    this.selectedBlock = '';
  }

  ngOnInit(): void {
    // console.log(this.blockList);
  }
  blockWiseDealer() {
    // console.log(this.selectedBlock.block_code);
    this.blockCode = this.selectedBlock.block_code;
    this.service.getblockWiseDealer(this.blockCode).subscribe(async result => {
      this.dealers = result;
      // console.log(this.dealers);
    }, err => console.log(err));
  }
  onCloseClick(): void {
    this.dialogRef.close();
  }
}


