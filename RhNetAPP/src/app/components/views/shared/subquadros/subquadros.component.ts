import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Property } from 'src/app/components/models/shared/property.model';
import { Subquadro } from 'src/app/components/models/shared/subquadro.model';
import { SharedService } from 'src/app/components/services/shared/shared.service';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-subquadros',
  templateUrl: './subquadros.component.html',
    styleUrls: ['./subquadros.component.css'],
    encapsulation: ViewEncapsulation.None
})
export class SubquadrosComponent implements OnInit {

    title: string = "Subquadros";
    table: string = "subquadro";

    properties: Property[] = [];
    displayedColumns: string[] = [];



    subquadros: Subquadro[] = [];


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
                this.subquadros = results;


                let newEntity = {} as Subquadro;
                newEntity.id = 0;
                newEntity.sigla = '';
                newEntity.descricao = '';
                this.subquadros.splice(0, 0, newEntity);

                this.variable.IsLoading = false;

            },
                (err) => {
                    console.log(err)
                    this.variable.IsLoading = false;
                })

        })

    }

}
