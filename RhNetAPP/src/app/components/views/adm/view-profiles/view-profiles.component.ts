import { Component, OnInit } from '@angular/core';
import { Profile } from 'src/app/components/models/adm/profile.model';
import { UserService } from 'src/app/components/services/adm/user.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';

@Component({
  selector: 'app-view-profiles',
  templateUrl: './view-profiles.component.html',
  styleUrls: ['./view-profiles.component.css']
})
export class ViewProfilesComponent implements OnInit {
    isFavorite: boolean = false;
    ds = new MatTableDataSource();
    profiles: Profile[] = [];

    displayedColumns: string[] = ["name", "description", "level", "actions"];

    constructor(private service: UserService, private favoriteService: FavoriteService, private router : Router) { }

    ngOnInit(): void {
        
        this.favoriteService.isFavorite(this.router.url).subscribe(results => {
            this.isFavorite = results;
        },
            (err) => {
                console.log(err)
            })

        this.service.getAllRoles().subscribe(results => {
            this.profiles = results;

            let newProfile = {} as Profile;
            newProfile.id = '';
            newProfile.name = '';
            newProfile.description = '';
            newProfile.level = null;
            
            this.profiles.splice(0, 0, newProfile);
            this.ds = new MatTableDataSource(this.profiles);
        },
            (err) => {
                console.log(err)
            })

    }

    add(profile: Profile): void {

        let newProfile = {} as Profile;
        newProfile.id = '';
        newProfile.name = profile.name;
        newProfile.description = profile.description;
        newProfile.level = profile.level;

        this.service.addRole(profile).subscribe(results => {
            profile.name = '';
            profile.description = '';
            profile.id = '';
            profile.level = null;

            this.profiles.push(results);
            this.ds.data = this.profiles;
        },
            (err) => {
                console.log(err)
            })
    }

    update(profile: Profile): void {
        this.service.updateRole(profile).subscribe(results => {
            alert('Perfil atualizado');
        },
            (err) => {
                console.log(err)
            })
    }

    addFavorite(): void {
        this.favoriteService.addFavorite(this.router.url).subscribe(results => {
            
        },
            (err) => {
                console.log(err)
            })
    }
}
