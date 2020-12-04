import { Component, OnInit, Input, ViewChild  } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { Property } from 'src/app/components/models/shared/property.model';
import { Variables } from 'src/app/components/variables';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { SharedService } from 'src/app/components/services/shared/shared.service';

@Component({
  selector: 'app-editable-table',
  templateUrl: './editable-table.component.html',
    styleUrls: ['./editable-table.component.css']
    
})
export class EditableTableComponent implements OnInit {

    @Input() ds: MatTableDataSource<any>;
    @Input() properties: Property[] ;
    @Input() displayedColumns: string[];
    @Input() objectList: any[];
    @Input() table: string;

    filterColumns = [];

    @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
    @ViewChild(MatSort, { static: false }) sort: MatSort;

    constructor(private variable: Variables, private service: SharedService) {
      
        
    }

    ngOnInit(): void {
       
        this.ds = new MatTableDataSource(this.objectList);  
       
       
    }

    getMaxLength(propertyName: string): number {
        var maxLength = 0;

        for (var i = 0; i < this.properties.length; i++) {
            if (this.properties[i].name === propertyName) {
                maxLength = this.properties[i].maximum;
                break;
            }
        }

        return maxLength;
    }

    applyFilter(event: Event, columnName: string) {

        const filterValue = (event.target as HTMLInputElement).value;

        var index = this.filterColumns.map(function (e) { return e.columnName }).indexOf(columnName);

        if (index < 0) {
            this.filterColumns.push({ columnName: columnName, value: filterValue.trim().toLowerCase() });
        } else {
            this.filterColumns[index].value = filterValue;
        }


        this.ds.filter = JSON.stringify(this.filterColumns);

        if (this.ds.paginator) {
            this.ds.paginator.firstPage();
        }
    }

    ngAfterViewInit() {
        
        this.ds.paginator = this.paginator;
        this.ds.sort = this.sort;
        this.paginator._intl.itemsPerPageLabel = "Items por página"
        this.paginator._intl.firstPageLabel = "Primeira página"
        this.paginator._intl.lastPageLabel = "Úlltima página"
        this.paginator._intl.nextPageLabel = "Próxima página"
        this.paginator._intl.previousPageLabel = "Página anterior"

        this.ds.filterPredicate = function (data: any, filter: string): boolean {

            let filterObjects = JSON.parse(filter);
            let contains = true;

            for (var i = 0; i < filterObjects.length; i++) {


                if (filterObjects[i].columnName in data) {
                    contains = contains && data[filterObjects[i].columnName].toLowerCase().includes(filterObjects[i].value.trim().toLowerCase());
                }
            }

            return contains;

        };
    }

    add(object: any): void {

        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;
                

        this.service.add(this.table, object).subscribe(results => {

            for (var i = 0; i < this.properties.length; i++) {
                if (this.properties[i].type_Description == 'string') {
                    object[this.properties[i].name] = "";
                } else {
                    if (this.properties[i].type_Description === 'int' && this.properties[i].required === true) {
                        object[this.properties[i].name] = 0;
                    } else {
                        object[this.properties[i].name] = null;
                    }
                    
                }
                
            }

            this.objectList.push(results);
            this.ds.data = this.objectList;

            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            this.variable.showMessage('Adicionado com sucesso.');
        },
            (err) => {
                this.variable.IsLoading = false;
                this.variable.IsEnabled = true;
                if (err.error.errors !== undefined) {
                    let properties = Object.getOwnPropertyNames(err.error.errors);

                    var erros = '';
                    for (var e = 0; e < properties.length; e++) {
                        if (properties[e] !== 'length') {
                            erros += '- ' + err.error.errors[properties[e]] + '\n';
                        };
                    }
                    alert(erros);
                }
                console.log(err)
            })
    }

    update(object: any): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.update(this.table, object).subscribe(results => {
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            this.variable.showMessage('Dados atualizados com sucesso');
            
        },
            (err) => {
                this.variable.IsLoading = false;
                this.variable.IsEnabled = true;
                if (err.error.errors !== undefined) {
                    let properties = Object.getOwnPropertyNames(err.error.errors);

                    var erros = '';
                    for (var e = 0; e < properties.length; e++) {
                        if (properties[e] !== 'length') {
                            erros += '- ' + err.error.errors[properties[e]] + '\n';
                        };
                    }
                    alert(erros);
                }

                console.log(err)
            })
    }

    remove(object: any): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.remove(this.table, object).subscribe(results => {
            this.objectList.splice(this.objectList.indexOf(object), 1);
            this.ds.data = this.objectList;
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            this.variable.showMessage('Removido com sucesso.');
        },
            (err) => {
                this.variable.IsLoading = false;
                this.variable.IsEnabled = true;
                if (err.error.errors !== undefined) {
                    let properties = Object.getOwnPropertyNames(err.error.errors);

                    var erros = '';
                    for (var e = 0; e < properties.length; e++) {
                        if (properties[e] !== 'length') {
                            erros += '- ' + err.error.errors[properties[e]] + '\n';
                        };
                    }
                    alert(erros);
                }
                console.log(err)
            })

    }
}
