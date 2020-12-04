import { Component, OnInit } from '@angular/core';
import { Property } from 'src/app/components/models/shared/property.model';
import { Tipo_de_Ato_Normativo } from 'src/app/components/models/shared/tipo_de_ato_normativo.model';
import { SharedService } from 'src/app/components/services/shared/shared.service';

@Component({
  selector: 'app-tipos-de-ato-normativo',
  templateUrl: './tipos-de-ato-normativo.component.html',
  styleUrls: ['./tipos-de-ato-normativo.component.css']
})
export class TiposDeAtoNormativoComponent implements OnInit {
    
    title: string = "Tipos de Ato Normativo";
    table: string = "tipo_de_ato_normativo";
    
    properties: Property[] = [];
    displayedColumns: string[] = [];

    tipos_de_ato_normativo: Tipo_de_Ato_Normativo[] = [];


   
    constructor(private service: SharedService) {

    }

    ngOnInit(): void {
      
        this.service.getProperties(this.table).subscribe(results => {
            this.properties = results;
            for (var i = 0; i < this.properties.length; i++) {
                if (this.properties[i].autoGenerateField === true) {
                    this.displayedColumns.push(this.properties[i].name);
                }
            }

            this.displayedColumns.push("actions");

            this.service.get(this.table).subscribe(results => {
                this.tipos_de_ato_normativo = results;


                let newEntity = {} as Tipo_de_Ato_Normativo;
                newEntity.id = 0;
                newEntity.descricao = '';
                this.tipos_de_ato_normativo.splice(0, 0, newEntity);

            },
                (err) => {
                    console.log(err)
                })
        })

    }

 


}
