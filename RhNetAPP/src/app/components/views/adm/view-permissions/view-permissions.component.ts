import { Component, OnInit, ViewChild } from '@angular/core';
import { Permission } from 'src/app/components/models/adm/permission.model';
import { Property } from 'src/app/components/models/shared/property.model';
import { SharedService } from 'src/app/components/services/shared/shared.service';


@Component({
  selector: 'app-view-permissions',
  templateUrl: './view-permissions.component.html',
  styleUrls: ['./view-permissions.component.css']
})
export class ViewPermissionsComponent implements OnInit {

    title: string = "Permissões:";
    table: string = "permission"
    
    permissions: Permission[] = [];
    properties: Property[] = [];

    displayedColumns: string[] = [];


    constructor(private service: SharedService) { }

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
                this.permissions = results;


                let newEntity = {} as Permission;
                newEntity.id = 0;
                newEntity.description = '';
                newEntity.table = ''
                this.permissions.splice(0, 0, newEntity);

            },
                (err) => {
                    console.log(err)
                })
        })

        //var currentProfile = '';
        //if (this.variable.CurrentProfile.length > 0) {
        //    currentProfile = this.variable.CurrentProfile
        //} else {
        //    currentProfile = localStorage.getItem('currentProfile');
        //}
        //this.favoriteService.isFavorite(this.router.url, currentProfile).subscribe(results => {
        //    this.isFavorite = results;
        //},
        //    (err) => {
        //        console.log(err)
        //    })



    }

   

}
