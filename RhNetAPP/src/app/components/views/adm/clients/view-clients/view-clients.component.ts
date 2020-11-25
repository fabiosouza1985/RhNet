import { Component, OnInit, ViewChild } from '@angular/core';
import { Client } from 'src/app/components/models/adm/client.model';
import { ClientService } from 'src/app/components/services/adm/client.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-view-clients',
  templateUrl: './view-clients.component.html',
  styleUrls: ['./view-clients.component.css']
})
export class ViewClientsComponent implements OnInit {

    title: string = "Clientes:";
    isFavorite: boolean = false;
    ds = new MatTableDataSource();
    clients: Client[] = [];

    displayedColumns: string[] = ["cnpj", "description", "situation", "actions"];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;


    constructor(private service: ClientService, private favoriteService: FavoriteService, private router: Router, private variable: Variables) { }

    ngOnInit(): void {

        this.favoriteService.isFavorite(this.router.url, this.variable.CurrentProfile).subscribe(results => {
            this.isFavorite = results;
        },
            (err) => {
                console.log(err)
            })



    }

    ngAfterViewInit() {
        this.service.getAllClients().subscribe(results => {
            this.clients = results;

            this.paginator._intl.itemsPerPageLabel = "Items por página"
            this.paginator._intl.firstPageLabel = "Primeira página"
            this.paginator._intl.lastPageLabel = "Úlltima página"
            this.paginator._intl.nextPageLabel = "Próxima página"
            this.paginator._intl.previousPageLabel = "Página anterior"


            let newClient = {} as Client;
            newClient.id = 0;          
            newClient.description = '';
            newClient.situation = 1;
            newClient.cnpj = '';
            this.clients.splice(0, 0, newClient);
            this.ds = new MatTableDataSource(this.clients);
            this.ds.paginator = this.paginator;
            this.ds.sort = this.sort;

        },
            (err) => {
                console.log(err)
            })
    }
    add(client: Client): void {

        let newClient = {} as Client;
        newClient.id = 0;
        newClient.cnpj = client.cnpj;
        newClient.description = client.description;
        newClient.situation = client.situation;

        this.service.add(client).subscribe(results => {
            client.cnpj = '';
            client.description = '';
            client.id = 0;
            client.situation = 1;

            this.clients.push(results);
            this.ds.data = this.clients;
            this.variable.showMessage('Cliente adicionado com sucesso');


        },
            (err) => {
                if (err.error.errors !== undefined) {
                    let properties = Object.getOwnPropertyNames(err.error.errors); 

                    var erros = '';
                    for (var e = 0; e < properties.length; e++) {
                        erros += '- ' + err.error.errors[properties[e]] + '\n';
                        
                    }    
                    alert(erros);
                    
                }
                
                

                console.log(err);
            })
    }

    update(client: Client): void {
        this.service.update(client).subscribe(results => {
            this.variable.showMessage('Cliente atualizado com sucesso');

        },
            (err) => {
                console.log(err)
            })
    }

    addRemoveFavorite(): void {
        this.variable.addRemoveFavorite(!this.isFavorite, this.router.url);
        this.isFavorite = !this.isFavorite;

    }


}
