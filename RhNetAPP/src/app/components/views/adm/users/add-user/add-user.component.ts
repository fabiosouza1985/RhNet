import { Component, OnInit } from '@angular/core';
import { Variables } from 'src/app/components/variables';
import { Router } from '@angular/router';
import { ClientService } from 'src/app/components/services/adm/client.service';
import { PermissionService } from 'src/app/components/services/adm/permission.service';
import { UserService } from 'src/app/components/services/adm/user.service';

import { ApplicationUser } from 'src/app/components/models/adm/applicationUser.model';
import { Client } from 'src/app/components/models/adm/client.model';
import { Permission } from 'src/app/components/models/adm/permission.model';
import { Profile } from 'src/app/components/models/adm/profile.model';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  styleUrls: ['./add-user.component.css']
})
export class AddUserComponent implements OnInit {

    clients: Client[] = [];
    profiles: Profile[] = [];
    permissions: Permission[] = [];

    selectedClient: Client;
    selectedProfile: Profile;
    selectedPermission: Permission;

    newUser: ApplicationUser = {
        cpf: '',
        email: '',
        userId: '',
        userName: '',
        applicationRoles: [],
        clients: [],
        permissions: []
    }

    constructor(private router: Router,
        public variable: Variables,
        private clientService: ClientService,
        private permissionService: PermissionService,
        private userService: UserService) { }

    ngOnInit(): void {

        this.clientService.getClients().subscribe(results => {
            this.clients = results;        
        },
            (err) => {
                console.log(err)
            })

        this.userService.getAllRoles().subscribe(results => {
            this.profiles = results;
        },
            (err) => {
                console.log(err)
            })

        this.permissionService.getPermissions().subscribe(results => {
            this.permissions = results;
        },
            (err) => {
                console.log(err)
            })
    }

    addClient(): void {
        this.newUser.clients.push(this.selectedClient);
        this.clients.splice(this.clients.indexOf(this.selectedClient), 1);
        
    }

    removeClient(client): void {
        this.newUser.clients.splice(this.newUser.clients.indexOf(client), 1);
        this.clients.push(client);
    }

    addProfile(): void {
        this.newUser.applicationRoles.push(this.selectedProfile);
        this.profiles.splice(this.profiles.indexOf(this.selectedProfile), 1);

    }

    removeProfile(profile): void {
        this.newUser.applicationRoles.splice(this.newUser.applicationRoles.indexOf(profile), 1);
        this.profiles.push(profile);
    }

    addPermission(): void {
        this.newUser.permissions.push(this.selectedPermission);
        this.permissions.splice(this.permissions.indexOf(this.selectedPermission), 1);

    }

    removePermission(permission): void {
        this.newUser.permissions.splice(this.newUser.permissions.indexOf(permission), 1);
        this.permissions.push(permission);
    }

    addUser(): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;
        this.userService.addUser(this.newUser).subscribe(results => {
            
            this.variable.showMessage('Usuário adicionado com sucesso');
            this.router.navigate(['/users']);
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
                        }
                        ;
                        

                    }
                    alert(erros);
                }

                
                console.log(err);
            })
    }
}
