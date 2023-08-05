import { Component, OnInit, Inject, Input } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-dialougebox',
  templateUrl: './dialougebox.component.html',
  styleUrls: ['./dialougebox.component.css']
})
export class DialougeboxComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<DialougeboxComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) { }

  ngOnInit(): void {
    
  }

  onCloseClick(): void {
    
    this.dialogRef.close();
  }
 
}
