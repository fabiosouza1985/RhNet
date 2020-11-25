import { Component, OnInit, ViewChild } from '@angular/core';
import { Profile } from 'src/app/components/models/adm/profile.model';
import { UserService } from 'src/app/components/services/adm/user.service';
import { MenuService } from 'src/app/components/services/adm/menu.service';
import { FavoriteService } from 'src/app/components/services/adm/favorite.service';
import {Menu} from 'src/app/components/models/adm/menu.model';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { Variables } from 'src/app/components/variables';

@Component({
  selector: 'app-view-menus',
  templateUrl: './view-menus.component.html',
  styleUrls: ['./view-menus.component.css']
})
export class ViewMenusComponent implements OnInit {
    title: string = "Meus Menus";
    isFavorite: boolean = false;

    ds = new MatTableDataSource();
    profiles: Profile[] = [];
  
    menus: Menu[];

    displayedColumns: string[] = ["role_Name", "header", "path", "permission_Name", 'quick_Access', "actions"];

    @ViewChild(MatPaginator) paginator: MatPaginator;
    @ViewChild(MatSort) sort: MatSort;

    constructor(private service: UserService, private menuService: MenuService, private favoriteService: FavoriteService, private router: Router, private _snackBar: MatSnackBar, private variable: Variables) { }
    
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

            this.menuService.getAllMenus().subscribe(results_1 => {

                this.menus = results_1;

                let newMenu = {} as Menu;
                newMenu.id = 0;
                newMenu.header = '';
                newMenu.path = '';
                newMenu.role_Name = '';
                newMenu.permission_Name = '';
                newMenu.quick_Access = false;
                this.menus.splice(0, 0, newMenu);
                this.ds = new MatTableDataSource(this.menus);
                this.ds.paginator = this.paginator;
                this.ds.sort = this.sort;
            },
                (err) => {
                    console.log(err)
                })
        },
            (err) => {
                console.log(err)
            })
    }
    addMenu(menu: Menu): void {
        
        let newMenu = {} as Menu;
        newMenu.id = 0;
        newMenu.header = menu.header;
        newMenu.path = menu.path;
        newMenu.role_Name = menu.role_Name;
        newMenu.permission_Name = menu.permission_Name;
        newMenu.quick_Access = menu.quick_Access;
        this.menuService.addMenu(newMenu).subscribe(results => {
            menu.header = '';
            menu.path = '';
            menu.role_Name = '';
            menu.permission_Name = '';
            menu.quick_Access = false;

            this.menus.push(results);
            this.ds.data = this.menus;
        },
            (err) => {
                console.log(err)
            })    

       
    }

    updateMenu(menu: Menu): void {
        this.menuService.updateMenu(menu).subscribe(results => {
            alert('Menu atualizado');
        },
            (err) => {
                console.log(err)
            })    
    }

    removeMenu(menu: Menu): void {
        this.menuService.removeMenu(menu).subscribe(results => {
            this.menus.splice(this.menus.indexOf(menu), 1);
            this.ds.data = this.menus;
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
