import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Property } from 'src/app/components/models/shared/property.model';
import { Quadro } from 'src/app/components/models/shared/quadro.model';
import { SharedService } from 'src/app/components/services/shared/shared.service';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-quadros',
  templateUrl: './quadros.component.html',
    styleUrls: ['./quadros.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class QuadrosComponent implements OnInit {

    title: string = "Quadros";
    table: string = "quadro";

    properties: Property[] = [];
    displayedColumns: string[] = [];



    quadros: Quadro[] = [];


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
                this.quadros = results;


                let newEntity = {} as Quadro;
                newEntity.id = 0;
                newEntity.sigla = '';
                newEntity.descricao = '';
                this.quadros.splice(0, 0, newEntity);

                this.variable.IsLoading = false;

            },
                (err) => {
                    console.log(err)
                    this.variable.IsLoading = false;
                })

        })

    }

}
