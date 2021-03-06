﻿import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Property } from 'src/app/components/models/shared/property.model';
import { Municipio } from 'src/app/components/models/shared/municipio.model';
import { SharedService } from 'src/app/components/services/shared/shared.service';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-municipios',
  templateUrl: './municipios.component.html',
    styleUrls: ['./municipios.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class MunicipiosComponent implements OnInit {

    title: string = "Municípios";
    table: string = "municipio";
  
    properties: Property[] = [];
    displayedColumns: string[] = [];
    
  

    municipios: Municipio[] = [];

 
    constructor(private service: SharedService, public variable: Variables) {
       
    }

    ngOnInit(): void {
      
        this.variable.IsLoading = true;
        
        this.service.getProperties(this.table).subscribe(results => {
            this.properties = results;
            for (var i = 0; i < this.properties.length; i++) {
                if (this.properties[i].autoGenerateField === true) {
                    this.displayedColumns.push(this.properties[i].name);
                }
            }

            this.displayedColumns.push("actions");

            this.service.get(this.table).subscribe(results => {
                this.municipios = results;


                let newEntity = {} as Municipio;
                newEntity.id = 0;
                newEntity.codigo_Audesp = '';
                newEntity.descricao = '';
                this.municipios.splice(0, 0, newEntity);

                this.variable.IsLoading = false;

            },
                (err) => {
                    console.log(err)
                    this.variable.IsLoading = false;
                })

        })        
     
    }

 

    

}
