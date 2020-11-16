import { Component, OnInit, ViewChild, AfterViewInit  } from '@angular/core';
import { Profile } from 'src/app/components/models/adm/profile.model';
import { UserService } from 'src/app/components/services/adm/user.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-view-profiles',
  templateUrl: './view-profiles.component.html',
  styleUrls: ['./view-profiles.component.css']
})
export class ViewProfilesComponent implements OnInit {
    title: string = "Perfis de Usuário:";
    isFavorite: boolean = false;
    ds = new MatTableDataSource();
    profiles: Profile[] = [];

    displayedColumns: string[] = ["name", "description", "level", "actions"];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;


    constructor(private service: UserService, private favoriteService: FavoriteService, private router: Router, private _snackBar: MatSnackBar, private variable: Variables) { }

    ngOnInit(): void {
        
        this.favoriteService.isFavorite(this.router.url).subscribe(results => {
            this.isFavorite = results;
        },
            (err) => {
                console.log(err)
            })

       

    }

    ngAfterViewInit() {
        this.service.getAllRoles().subscribe(results => {
            this.profiles = results;

            this.paginator._intl.itemsPerPageLabel = "Items por página"
            this.paginator._intl.firstPageLabel = "Primeira página"
            this.paginator._intl.lastPageLabel = "Úlltima página"
            this.paginator._intl.nextPageLabel = "Próxima página"
            this.paginator._intl.previousPageLabel = "Página anterior"


            let newProfile = {} as Profile;
            newProfile.id = '';
            newProfile.name = '';
            newProfile.description = '';
            newProfile.level = null;

            this.profiles.splice(0, 0, newProfile);
            this.ds = new MatTableDataSource(this.profiles);
            this.ds.paginator = this.paginator;
            this.ds.sort = this.sort;

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
            
            this._snackBar.open('Perfil adicionado com sucesso', 'X', {
                duration: 2000,
            });

        },
            (err) => {
                console.log(err)
            })
    }

    update(profile: Profile): void {
        this.service.updateRole(profile).subscribe(results => {
            this._snackBar.open('Perfil atualizado com sucesso', 'X',  {
                duration: 2000,
            });
            
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
