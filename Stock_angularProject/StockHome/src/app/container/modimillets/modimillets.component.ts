import { Component, OnInit } from '@angular/core';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-modimillets',
  templateUrl: './modimillets.component.html',
  styleUrls: ['./modimillets.component.css']
})
export class ModimilletsComponent implements OnInit {

  constructor(
    public dialogRef: MatDialogRef<ModimilletsComponent>,
    
  ) {

  }

  ngOnInit(): void {

  }
  close() {
    this.dialogRef.close();
  }
}
