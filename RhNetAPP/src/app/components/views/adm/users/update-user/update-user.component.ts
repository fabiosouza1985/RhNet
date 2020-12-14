import { Component, OnInit } from '@angular/core';
import { Variables } from 'src/app/components/variables';
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
import { ClientService } from 'src/app/components/services/adm/client.service';
import { PermissionService } from 'src/app/components/services/adm/permission.service';
import { UserService } from 'src/app/components/services/adm/user.service';

import { ApplicationUser } from 'src/app/components/models/adm/applicationUser.model';
import { Client } from 'src/app/components/models/adm/client.model';
import { Permission } from 'src/app/components/models/adm/permission.model';
import { Profile } from 'src/app/components/models/adm/profile.model';
import { UserRole } from 'src/app/components/models/adm/userRole.model';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { UserPermission } from '../../../../models/adm/userPermission.model';

@Component({
  selector: 'app-update-user',
  templateUrl: './update-user.component.html',
  styleUrls: ['./update-user.component.css']
})
export class UpdateUserComponent implements OnInit {

    clients: Client[] = [];
    profiles: Profile[] = [];
    permissions: Permission[] = [];
    tables: string[] = [];

    selectedClient: Client;
    selectedProfile: Profile;
    selectedPermission: Permission;

    User: ApplicationUser = {
        cpf: '',
        email: '',
        userId: '',
        userName: '',
        applicationRoles: [],
        clients: [],
        permissions: []
    }

    constructor(private router: Router,
        private route: ActivatedRoute,
        public variable: Variables,
        private clientService: ClientService,
        private permissionService: PermissionService,
        private userService: UserService) { }

    ngOnInit(): void {
        
        
        
        this.clientService.getClients().subscribe(results => {
            this.clients = results;

            this.userService.getAllRoles().subscribe(results => {
                this.profiles = results;

                this.permissionService.get().subscribe(results => {
                    this.permissions = results;
                    this.tables = [];
                    for (var i = 0; i < this.permissions.length; i++) {
                        var index = this.tables.indexOf(this.permissions[i].table);

                        if (index < 0) {
                            this.tables.push(this.permissions[i].table);
                        }
                    }

                    this.route.params.subscribe(params => {

                        let id = params['userId'];

                        this.userService.getUser(id).subscribe(results => {
                            this.User = results;
                        },
                            (err) => {
                                console.log(err)
                            })

                    });

                },
                    (err) => {
                        console.log(err)
                    })

            },
                (err) => {
                    console.log(err)
                })


        },
            (err) => {
                console.log(err)
            })

        

       
    }

    addClient(): void {
        this.User.clients.push(this.selectedClient);
        this.clients.splice(this.clients.indexOf(this.selectedClient), 1);

    }

    removeClient(client): void {
        this.User.clients.splice(this.User.clients.indexOf(client), 1);
        this.clients.push(client);
    }

    addProfile(profile, clientId): void {
        let userProfile: UserRole = {
            clientId: clientId,
            roleId: profile.id,
            userId: ''
        }
        this.User.applicationRoles.push(userProfile);
    }

    removeProfile(profile, clientId): void {
        var index = this.User.applicationRoles.map(function (e) { return e.clientId, e.roleId }).indexOf(clientId, profile.id);

        this.User.applicationRoles.splice(index, 1);

    }

    addPermission(permission, clientId): void {
        let userPermission: UserPermission = {
            clientId: clientId,
            description: permission.description,
            userId: ''
        }
        this.User.permissions.push(userPermission);

    }

    removePermission(permission, clientId): void {

        var index = this.User.permissions.map(function (e) { return e.clientId, e.description }).indexOf(clientId, permission.description);

        this.User.permissions.splice(index, 1);

    }

    updateUser(): void {
        this.variable.IsLoading = true;
        this.variable.IsEnabled = false;
        this.userService.updateUser(this.User).subscribe(results => {

            this.variable.showMessage('Usuário atualizado com sucesso');
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

    getPermissions(table): Permission[] {
        var permissions = [];

        for (var i = 0; i < this.permissions.length; i++) {
            if (this.permissions[i].table === table) {
                permissions.push(this.permissions[i]);
            }
        }

        return permissions;
    }

    permissionChange(event: MatSlideToggleChange, permission: Permission, clientId: number): void {
        if (event.checked === true) {
            this.addPermission(permission, clientId);
        } else {
            this.removePermission(permission, clientId);
        }

    }

    profileChange(event: MatSlideToggleChange, profile: Profile, clientId: number): void {
        if (event.checked === true) {
            this.addProfile(profile, clientId);
        } else {
            this.removeProfile(profile, clientId);
        }

    }

    existPermission(permissionId, clientId): boolean {
        var exist: boolean = false;
        for (var i = 0; i < this.permissions.length; i++) {
            if (this.permissions[i].clientId == clientId && this.permissions[i].id == permissionId) {
                exist = true;
                break;
            }
        };

        return exist;
    }

    existProfile(profileId, clientId): boolean {
        var exist: boolean = false;
       
        for (var i = 0; i < this.profiles.length; i++) {
            if (this.profiles[i].clientId == clientId && this.profiles[i].id == profileId) {
                exist = true;
                break;
            }
        };

        return exist;
    }

}
