import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component, Inject } from '@angular/core';
import { FormControl, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Animal } from 'src/app/models/animal.model';
import { Enquery } from 'src/app/models/enquery.model';
import { EnqueryService } from 'src/app/services/enquery.service';
import { EnqueriesResponseModel } from 'src/app/models/enqueriesresponsemodel.model'

import { AfterViewInit, ViewChild } from '@angular/core';
import { MatPaginator, MatPaginatorModule } from '@angular/material/paginator';
import { MatSort, MatSortModule } from '@angular/material/sort';
import { MatTableDataSource, MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';

@Component({
    selector: 'app-get-all-enqueries',
    templateUrl: './getAllEnqueries.component.html',
    styleUrls: ['./getAllEnqueries.component.css']
})

export class GetAllEnqueriesComponent {

    // enqueries: Enquery[] = [];

    displayedColumns: string[] = ['id', 'timeStamp', 'phone', 'animalId', 'email'];
    listOfEnqueries: Enquery[] = [];
    dataSource: any

    @ViewChild(MatPaginator) paginator!: MatPaginator;
    @ViewChild(MatSort) sort!: MatSort;

    constructor(
        private enqueryService: EnqueryService,
        private dialogRef: MatDialogRef<GetAllEnqueriesComponent>,

    ) {
        this.enqueryService.getAllEnqueries().subscribe(response => {
            this.listOfEnqueries = response.enqueries;
            this.dataSource = new MatTableDataSource(this.listOfEnqueries);

            console.log('_debug_Foglalások listája: ', this.listOfEnqueries);
            console.log('_debug_ dataSource: ', this.dataSource);

            this.ngAfterViewInit();
        })

    }

    // minta ez alapján: https://v16.material.angular.io/components/table/examples#table-overview

    ngAfterViewInit() {
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;

        // ERROR TypeError: this.dataSource is undefined
        console.log('_debug_dataSource_ngAfterViewInit: ', this.dataSource);
    }

    applyFilter(event: Event) {
        const filterValue = (event.target as HTMLInputElement).value;
        this.dataSource.filter = filterValue.trim().toLowerCase();

        if (this.dataSource.paginator) {
            this.dataSource.paginator.firstPage();
        }
    }

    onCancelClick($event: MouseEvent){
        this.dialogRef.close();
    }


}

// console.log('_debug_listOfEnqueries: ', this.listOfEnqueries);
// console.log('_debug_dataSource: ', this.dataSource);



