import { Component, OnInit, ViewChild } from '@angular/core';
import { Variables } from 'src/app/components/variables';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Property } from 'src/app/components/models/shared/property.model';
import { Tipo_de_Ato_Normativo } from 'src/app/components/models/shared/tipo_de_ato_normativo.model';
import { SharedService } from 'src/app/components/services/shared/shared.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';

@Component({
  selector: 'app-tipos-de-ato-normativo',
  templateUrl: './tipos-de-ato-normativo.component.html',
  styleUrls: ['./tipos-de-ato-normativo.component.css']
})
export class TiposDeAtoNormativoComponent implements OnInit {

    title: string = "Tipos de Ato Normativo";
    table: string = "tipo_de_ato_normativo";
    isFavorite: boolean = false;
    properties: Property[] = [];
    displayedColumns: string[] = [];

    ds = new MatTableDataSource();

    tipos_de_ato_normativo: Tipo_de_Ato_Normativo[] = [];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    filterColumns = [];
    constructor(private variable: Variables, private router: Router, private service: SharedService, private favoriteService: FavoriteService) {

    }

    ngOnInit(): void {

        var currentProfile = '';
        if (this.variable.CurrentProfile.length > 0) {
            currentProfile = this.variable.CurrentProfile
        } else {
            currentProfile = localStorage.getItem('currentProfile');
        }
        this.favoriteService.isFavorite(this.router.url, currentProfile).subscribe(results => {
            this.isFavorite = results;
        },
            (err) => {
                console.log(err)
            })

        this.service.getProperties(this.table).subscribe(results => {
            this.properties = results;
            for (var i = 0; i < this.properties.length; i++) {
                if (this.properties[i].autoGenerateField === true) {
                    this.displayedColumns.push(this.properties[i].name);
                }
            }

            this.displayedColumns.push("actions");
        })

        this.service.get(this.table).subscribe(results => {
            this.tipos_de_ato_normativo = results;


            let newEntity = {} as Tipo_de_Ato_Normativo;
            newEntity.id = 0;
            newEntity.descricao = '';
            this.tipos_de_ato_normativo.splice(0, 0, newEntity);

            this.ds = new MatTableDataSource(this.tipos_de_ato_normativo);

            this.ds.paginator = this.paginator;
            this.ds.sort = this.sort;

            this.ds.filterPredicate = function (data: Tipo_de_Ato_Normativo, filter: string): boolean {

                let filterObjects = JSON.parse(filter);
                let contains = true;

                for (var i = 0; i < filterObjects.length; i++) {


                    if (filterObjects[i].columnName in data) {
                        contains = contains && data[filterObjects[i].columnName].toLowerCase().includes(filterObjects[i].value.trim().toLowerCase());
                    }
                }

                return contains;

            };

        },
            (err) => {
                console.log(err)
            })


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
        this.paginator._intl.itemsPerPageLabel = "Items por página"
        this.paginator._intl.firstPageLabel = "Primeira página"
        this.paginator._intl.lastPageLabel = "Úlltima página"
        this.paginator._intl.nextPageLabel = "Próxima página"
        this.paginator._intl.previousPageLabel = "Página anterior"

    }

    addRemoveFavorite(): void {
        this.variable.addRemoveFavorite(!this.isFavorite, this.router.url);
        this.isFavorite = !this.isFavorite;

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

    add(object: Tipo_de_Ato_Normativo): void {

        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        let newObject = {} as Tipo_de_Ato_Normativo;
        newObject.id = 0;
        newObject.descricao = object.descricao;

        this.service.add(this.table, object).subscribe(results => {
          
            object.descricao = '';

            this.tipos_de_ato_normativo.push(results);
            this.ds.data = this.tipos_de_ato_normativo;

            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;

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

    update(object: Tipo_de_Ato_Normativo): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.update(this.table, object).subscribe(results => {
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            alert('Município atualizado');
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

    remove(object: Tipo_de_Ato_Normativo): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.remove(this.table, object).subscribe(results => {
            this.tipos_de_ato_normativo.splice(this.tipos_de_ato_normativo.indexOf(object), 1);
            this.ds.data = this.tipos_de_ato_normativo;
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
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
