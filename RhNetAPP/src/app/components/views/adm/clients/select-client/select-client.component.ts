import { Component, OnInit } from '@angular/core';
import { ClientService } from 'src/app/components/services/adm/client.service';
import { UserService } from 'src/app/components/services/adm/user.service';
import { Client } from 'src/app/components/models/adm/client.model';
import { Variables } from 'src/app/components/variables';
import { Router } from '@angular/router';

@Component({
  selector: 'app-select-client',
  templateUrl: './select-client.component.html',
  styleUrls: ['./select-client.component.css']
})
export class SelectClientComponent implements OnInit {

    clients: Client[] = [];

    constructor(private service: ClientService, private userService: UserService, private variable: Variables, private router: Router) { }

    ngOnInit(): void {
        this.service.getClients().subscribe(results => {
            this.clients = results;

        },
            (err) => {
                console.log(err)
            })
    }

    setClient(client): void {
        this.variable.SelectedClient = client;
        localStorage.setItem("currentClient", client.cnpj);
        this.variable.setTitle();
        this.userService.setClient(client).subscribe(results => {
            console.log(results);

        },
            (err) => {
                console.log(err)
            }) 

        this.router.navigate(['/home']);
    }

}
