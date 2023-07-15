import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-doubledealerlogin',
  templateUrl: './doubledealerlogin.component.html',
  styleUrls: ['./doubledealerlogin.component.css']
})
export class DoubledealerloginComponent implements OnInit {
  selectedIndex:any
 
  
  constructor(
    public dialogRef: MatDialogRef<DoubledealerloginComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {

  }

  ngOnInit(): void {

  }
  onCloseClick(): void {
    this.dialogRef.close();
  }
  viewfile(data: any) {
    // window.open('http://localhost:3000/admin/getBillDoucuments?fileName=' + data, '_blank');

    window.open('https://dbtmbdodisha.nic.in/admin/getBillDoucuments?fileName=' + data, '_blank');
  }

}