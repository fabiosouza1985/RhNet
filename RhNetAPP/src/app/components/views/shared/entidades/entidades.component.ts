import { Component, OnInit, ViewChild } from '@angular/core';
import { Variables } from 'src/app/components/variables';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Property } from 'src/app/components/models/shared/property.model';
import { Entidade } from 'src/app/components/models/shared/entidade.model';
import { Municipio } from 'src/app/components/models/shared/municipio.model';
import { SharedService } from 'src/app/components/services/shared/shared.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';

@Component({
  selector: 'app-entidades',
  templateUrl: './entidades.component.html',
  styleUrls: ['./entidades.component.css']
})
export class EntidadesComponent implements OnInit {

    title: string = "Entidades";
    table: string = "entidade";
    isFavorite: boolean = false;
    properties: Property[] = [];
    displayedColumns: string[] = [];

    ds = new MatTableDataSource();

    entidades: Entidade[] = [];
    municipios: Municipio[] = [];

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
            this.displayedColumns.push("municipio");
            this.displayedColumns.push("actions");
        })

        this.service.get('municipio').subscribe(municipios => {
            this.municipios = municipios;

            this.service.get(this.table).subscribe(results => {
                this.entidades = results;


                let newEntity = {} as Entidade;
                newEntity.id = 0;
                newEntity.codigo_Audesp = '';
                newEntity.descricao = '';
                newEntity.municipio_Descricao = '';
                newEntity.municipio_Id = null;
                this.entidades.splice(0, 0, newEntity);

                this.ds = new MatTableDataSource(this.entidades);

                this.ds.paginator = this.paginator;
                this.ds.sort = this.sort;

                this.ds.filterPredicate = function (data: Entidade, filter: string): boolean {

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

    add(object: Entidade): void {

        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        let newObject = {} as Entidade;
        newObject.id = 0;
        newObject.codigo_Audesp = object.codigo_Audesp;
        newObject.descricao = object.descricao;
        newObject.municipio_Id = object.municipio_Id;
        this.service.add(this.table, object).subscribe(results => {
            object.codigo_Audesp = '';
            object.descricao = '';
            object.municipio_Descricao = '';
            object.municipio_Id = null;

            this.entidades.push(results);
            this.ds.data = this.entidades;

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

    update(object: Entidade): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.update(this.table, object).subscribe(results => {
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            alert('Entidade atualizada');
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

    remove(object: Entidade): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.remove(this.table, object).subscribe(results => {
            this.entidades.splice(this.entidades.indexOf(object), 1);
            this.ds.data = this.entidades;
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
