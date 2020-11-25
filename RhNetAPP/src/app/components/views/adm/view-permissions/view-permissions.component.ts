import { Component, OnInit, ViewChild } from '@angular/core';
import { Permission } from 'src/app/components/models/adm/permission.model';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';
import { PermissionService } from 'src/app/components/services/adm/permission.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Variables } from 'src/app/components/variables';
@Component({
  selector: 'app-view-permissions',
  templateUrl: './view-permissions.component.html',
  styleUrls: ['./view-permissions.component.css']
})
export class ViewPermissionsComponent implements OnInit {

    title: string = "Permissões:";
    isFavorite: boolean = false;
    ds = new MatTableDataSource();
    permissions: Permission[] = [];

    displayedColumns: string[] = ["table", "description", "actions"];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    constructor(private service: PermissionService, private favoriteService: FavoriteService, private router: Router, private variable: Variables) { }

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



    }

    ngAfterViewInit() {
        this.service.getAllPermissions().subscribe(results => {
            this.permissions = results;

            this.paginator._intl.itemsPerPageLabel = "Items por página"
            this.paginator._intl.firstPageLabel = "Primeira página"
            this.paginator._intl.lastPageLabel = "Úlltima página"
            this.paginator._intl.nextPageLabel = "Próxima página"
            this.paginator._intl.previousPageLabel = "Página anterior"


            let newPermisson = {} as Permission;
            newPermisson.id = 0;            
            newPermisson.description = '';
            newPermisson.table = '';

            this.permissions.splice(0, 0, newPermisson);
            this.ds = new MatTableDataSource(this.permissions);
            this.ds.paginator = this.paginator;
            this.ds.sort = this.sort;

        },
            (err) => {
                console.log(err)
            })
    }

    add(permission: Permission): void {

        let newPermission = {} as Permission;
        newPermission.id = 0;
        
        newPermission.description = permission.description;
        newPermission.table = permission.table;

        this.service.addPermission(permission).subscribe(results => {
            permission.table = '';
            permission.description = '';
            permission.id = 0;

            this.permissions.push(results);
            this.ds.data = this.permissions;
            this.variable.showMessage('Permissão adicionado com sucesso');


        },
            (err) => {
                console.log(err)
            })
    }

    update(permission: Permission): void {
        this.service.updatePermission(permission).subscribe(results => {
            this.variable.showMessage('Permissão atualizada com sucesso');

        },
            (err) => {
                console.log(err)
            })
    }

    remove(permission: Permission): void {
        this.service.removePermission(permission).subscribe(results => {
            this.permissions.splice(this.permissions.indexOf(permission), 1);
            this.ds.data = this.permissions;
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
