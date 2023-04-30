import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FarmersaleService } from 'src/app/farmersale.service';
import { ToastrService } from 'ngx-toastr';
import * as XLSX from 'xlsx';
@Component({
  selector: 'app-saledeatils',
  templateUrl: './saledeatils.component.html',
  styleUrls: ['./saledeatils.component.css']
})
export class SaledeatilsComponent implements OnInit {
  LicNo: any = 'ODGAN6/2014-15/0004';
  selectedFromDate: any = '';
  selectedToDate: any = '';
  RptDateWiseSaleDeatails: any = [];
  sumTotalNO_OF_BAGS: any = 0;
  sumTOT_QTL: any = 0;
  sumTotalSUBSIDY_AMOUNT: any = 0;
  showDeatils: boolean = false;
  fileName: any = ''
  constructor(private router: Router,
    private service: FarmersaleService,
    private route: ActivatedRoute,
    private toastr: ToastrService) { }

  ngOnInit(): void {
  }
  RptDateWiseSale() {
    this.RptDateWiseSaleDeatails = [];
    this.sumTotalNO_OF_BAGS = 0;
    this.sumTOT_QTL = 0;
    this.sumTotalSUBSIDY_AMOUNT = 0;
    if (this.selectedFromDate != undefined && this.selectedFromDate != null && this.selectedFromDate != '' &&
      this.selectedToDate != undefined && this.selectedToDate != null && this.selectedToDate != '') {
      this.service.RptDateWiseSale(this.selectedFromDate, this.selectedToDate, this.LicNo).subscribe(data => {
        this.showDeatils = true;
        this.RptDateWiseSaleDeatails = data;
        this.RptDateWiseSaleDeatails.forEach((i: any) => {
          if (i.hasOwnProperty('NO_OF_BAGS')) {
            var a = (i.NO_OF_BAGS == undefined || i.NO_OF_BAGS == null || i.NO_OF_BAGS == '') ? 0.00 : i.NO_OF_BAGS;
            this.sumTotalNO_OF_BAGS = parseInt(this.sumTotalNO_OF_BAGS) + parseInt(a);
          }
          if (i.hasOwnProperty('TOT_QTL')) {
            var b = (i.TOT_QTL == undefined || i.TOT_QTL == null || i.TOT_QTL == '') ? 0.00 : i.TOT_QTL;
            this.sumTOT_QTL = (parseFloat(this.sumTOT_QTL) + parseFloat(b)).toFixed(2);
          }
          if (i.hasOwnProperty('SUBSIDY_AMOUNT')) {
            var b = (i.SUBSIDY_AMOUNT == undefined || i.SUBSIDY_AMOUNT == null || i.SUBSIDY_AMOUNT == '') ? 0.00 : i.SUBSIDY_AMOUNT;
            this.sumTotalSUBSIDY_AMOUNT = (parseFloat(this.sumTotalSUBSIDY_AMOUNT) + parseFloat(b)).toFixed(2);
          }
        })
      })
    }
    else {
      this.toastr.warning(`Please enter From date & To Date`);
    }

  }
  exportexcel(): void {
    if (this.RptDateWiseSaleDeatails.length > 0) {
      this.fileName = 'Stock_' + ' ' + this.selectedFromDate + '-' + this.selectedToDate + '.xlsx';
      /* table id is passed over here */
      let element = document.getElementById('downloadToExcel');
      const ws: XLSX.WorkSheet = XLSX.utils.table_to_sheet(element);

      /* generate workbook and add the worksheet */
      const wb: XLSX.WorkBook = XLSX.utils.book_new();
      XLSX.utils.book_append_sheet(wb, ws, 'Sheet1');

      /* save to file */
      XLSX.writeFile(wb, this.fileName);

    }
    else{
      this.toastr.warning(`No Record found !!!!`);
    }

  }
}
