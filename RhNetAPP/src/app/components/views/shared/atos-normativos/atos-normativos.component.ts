import { Component, OnInit, ViewChild } from '@angular/core';
import { Variables } from 'src/app/components/variables';
import { Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Property } from 'src/app/components/models/shared/property.model';
import { Ato_Normativo } from 'src/app/components/models/shared/ato_normativo.model';
import { Tipo_de_Ato_Normativo } from 'src/app/components/models/shared/tipo_de_ato_normativo.model';
import { SharedService } from 'src/app/components/services/shared/shared.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';


@Component({
  selector: 'app-atos-normativos',
  templateUrl: './atos-normativos.component.html',
    styleUrls: ['./atos-normativos.component.css']
})

export class AtosNormativosComponent implements OnInit {

    title: string = "Atos Normativos";
    table: string = "ato_normativo";
    isFavorite: boolean = false;
    properties: Property[] = [];
    displayedColumns: string[] = [];
    ds = new MatTableDataSource();

    atos_normativos: Ato_Normativo[] = [];
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
            this.displayedColumns.push("tipo_de_ato_normativo");
            this.displayedColumns.push("actions");
        })

        this.service.get('tipo_de_ato_normativo').subscribe(tipos_de_ato_normativo => {
            this.tipos_de_ato_normativo = tipos_de_ato_normativo;

            this.service.get(this.table).subscribe(results => {
               
                this.atos_normativos = results;
                
                let newEntity = {} as Ato_Normativo;
                newEntity.id = 0;
                newEntity.descricao = '';
                newEntity.tipo_de_Ato_Normativo_Descricao = '';
                this.atos_normativos.splice(0, 0, newEntity);

                this.ds = new MatTableDataSource(this.atos_normativos);

                this.ds.paginator = this.paginator;
                this.ds.sort = this.sort;

                this.ds.filterPredicate = function (data: Ato_Normativo, filter: string): boolean {

                    let filterObjects = JSON.parse(filter);
                    let contains = true;

                    for (var i = 0; i < filterObjects.length; i++) {


                        if (filterObjects[i].columnName in data) {
                            contains = contains && data[filterObjects[i].columnName].toString().toLowerCase().includes(filterObjects[i].value.trim().toLowerCase());
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

    applyFilterDate(event: Event, columnName: string) {
       let filterValueDate = (event.target as HTMLInputElement).value;

        if (new Date(filterValueDate)) {
            let _data = new Date(filterValueDate);

            if (_data.getFullYear() > 1500) {

                var ano = _data.getFullYear();
                var mes = _data.getMonth() + 1;
                var dia = _data.getDate()+1;

                let filterValue = ano + '-';
                if (mes < 10) {
                    filterValue += '0' + mes;
                } else {
                    filterValue += mes;
                }
                filterValue += '-';

                if (dia < 10) {
                    filterValue += '0' + dia;
                } else {
                    filterValue += dia;
                }


                var index = this.filterColumns.map(function (e) { return e.columnName }).indexOf(columnName);

                if (index < 0) {
                    this.filterColumns.push({ columnName: columnName, value: filterValue.trim().toLowerCase() });
                } else {
                    this.filterColumns[index].value = filterValue;
                }

                console.log(filterValue);
                this.ds.filter = JSON.stringify(this.filterColumns);

                if (this.ds.paginator) {
                    this.ds.paginator.firstPage();
                }


            }
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

    add(object: Ato_Normativo): void {

        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        let newObject = {} as Ato_Normativo;
        newObject.id = 0;
        newObject.descricao = object.descricao;
        newObject.ano = object.ano;
        newObject.numero = object.numero;
        newObject.vigencia_Data = object.vigencia_Data;
        newObject.vigencia_Publicacao = object.vigencia_Publicacao;
        newObject.tipo_de_Ato_Normativo_Id = object.tipo_de_Ato_Normativo_Id;

        this.service.add(this.table, object).subscribe(results => {
          
            object.id = 0;
            object.descricao = '';
            object.ano = null;
            object.numero = null;
            object.vigencia_Data = null;
            object.vigencia_Publicacao = null;
            object.tipo_de_Ato_Normativo_Id = null;
            object.tipo_de_Ato_Normativo_Descricao = '';

            this.atos_normativos.push(results);
            this.ds.data = this.atos_normativos;

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

    update(object: Ato_Normativo): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.update(this.table, object).subscribe(results => {
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;
            alert('Ato Normativo atualizado');
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

    remove(object: Ato_Normativo): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.service.remove(this.table, object).subscribe(results => {
            this.atos_normativos.splice(this.atos_normativos.indexOf(object), 1);
            this.ds.data = this.atos_normativos;
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
