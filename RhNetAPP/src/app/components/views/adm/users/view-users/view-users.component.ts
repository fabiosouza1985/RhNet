import { Component, OnInit, ViewChild } from '@angular/core';
import { ApplicationUser } from 'src/app/components/models/adm/applicationUser.model';
import { UserService } from 'src/app/components/services/adm/user.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-view-users',
  templateUrl: './view-users.component.html',
  styleUrls: ['./view-users.component.css']
})
export class ViewUsersComponent implements OnInit {

    title: string = "Usuários:";
    isFavorite: boolean = false;
    ds = new MatTableDataSource();
    users: ApplicationUser[] = [];

    displayedColumns: string[] = ["userName", "cpf", "email", "actions"];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;


    constructor(private service: UserService, private favoriteService: FavoriteService, private router: Router, private variable: Variables) { }

    ngOnInit(): void {

        this.favoriteService.isFavorite(this.router.url).subscribe(results => {
            this.isFavorite = results;
        },
            (err) => {
                console.log(err)
            })



    }

    ngAfterViewInit() {
        this.service.getUsers().subscribe(results => {
            this.users = results;

            this.paginator._intl.itemsPerPageLabel = "Items por página"
            this.paginator._intl.firstPageLabel = "Primeira página"
            this.paginator._intl.lastPageLabel = "Úlltima página"
            this.paginator._intl.nextPageLabel = "Próxima página"
            this.paginator._intl.previousPageLabel = "Página anterior"
          
            this.ds = new MatTableDataSource(this.users);
            this.ds.paginator = this.paginator;
            this.ds.sort = this.sort;

        },
            (err) => {
                console.log(err)
            })
    }

    addRemoveFavorite(): void {
        this.variable.addRemoveFavorite(!this.isFavorite, this.router.url);
        this.isFavorite = !this.isFavorite;

    }

    addUser(): void {
        this.router.navigate(['adduser']);
    }
}
