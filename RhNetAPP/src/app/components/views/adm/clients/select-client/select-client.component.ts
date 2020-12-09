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

        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;

        this.userService.setClient(client).subscribe(results => {

            var client = JSON.parse(results.currentClient);
            this.variable.Profiles = JSON.parse(results.profiles);
            
           
            this.variable.CurrentProfile = this.variable.Profiles[0].name;
            this.variable.SelectedClient = client;
                        

            localStorage.setItem('token', results.access_token);
            
            localStorage.setItem('currentProfile', this.variable.Profiles[0].name);
            localStorage.setItem("currentClient", this.variable.SelectedClient.cnpj);







            this.variable.setTitle();

            this.router.navigate(['/home']);
            this.variable.GetMenus();
            this.variable.GetQuickAccess();
            this.variable.GetFavorites();
            this.variable.IsLoading = false;
            this.variable.IsEnabled = true;

        },
            (err) => {
                console.log(err)
            }) 

        this.router.navigate(['/home']);
    }

}
